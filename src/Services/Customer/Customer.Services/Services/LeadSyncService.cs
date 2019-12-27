using Customer.Params.Outputs;
using Customer.Services.JsonModels;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Customer.Services.LeadSyncService
{
    public class LeadSyncService : ILeadSyncService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;

        public LeadSyncService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูล Lead จาก CRM After Sale API ตามช่วงเวลาที่กำหนด
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<LeadSyncResponse> SyncLeadsFromCRMAfterSale(DateTime startTime, DateTime endTime)
        {
            Console.WriteLine("Start Sync Lead Data from CRM After Sale.");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("APIHeader", Configuration["CRM3API:Header"]);
            client.DefaultRequestHeaders.Add("APIKey", Configuration["CRM3API:Key"]);
            var requestUrl = $"{Configuration["CRM3API:Host"]}/api/APILead/getlead?StartDate=" + startTime.ToString("yyyy-MM-dd") + "&EndDate=" + endTime.ToString("yyyy-MM-dd");
            var response = await client.GetAsync(requestUrl);
            var data = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<LeadJsonModel>>(data);

            var leadJsonList = results.Where(o => o.ISlead == "1").ToList();
            foreach (var item in leadJsonList)
            {
                var isExistLead = await DB.Leads.Where(o => o.RefID == item.refID).AnyAsync();
                if (!isExistLead)
                {
                    var leadModel = new Database.Models.CTM.Lead();
                    item.ToLeadModel(ref leadModel);
                    leadModel.IsDeleted = false;
                    leadModel.SubLeadType = "Call Center";
                    leadModel.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "0" && o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(item.ProjectID))
                        leadModel.ProjectID = await DB.Projects.Where(o => o.ProjectNo == item.ProjectID).Select(o => o.ID).FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(item.AdvertisementID))
                        leadModel.AdvertisementMasterCenterID = await DB.MasterCenters.Where(o => o.Key == item.AdvertisementID && o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(item.LeadsTypeID))
                        leadModel.LeadTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == item.LeadsTypeID && o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).FirstOrDefaultAsync();

                    #region Lead Scoring
                    var leadScoringTypeList = await DB.LeadScoringTypes.ToListAsync();
                    var callType = leadScoringTypeList.Where(o => o.Key == "1").First();
                    var emailType = leadScoringTypeList.Where(o => o.Key == "2").First();
                    var contactType = leadScoringTypeList.Where(o => o.Key == "3").First();
                    var bookType = leadScoringTypeList.Where(o => o.Key == "4").First();
                    var projectType = leadScoringTypeList.Where(o => o.Key == "5").First();

                    List<Database.Models.CTM.LeadScoring> leadSoreList = new List<Database.Models.CTM.LeadScoring>();
                    var leadCallModel = new Database.Models.CTM.LeadScoring()
                    {
                        LeadID = leadModel.ID,
                        LeadScoringTypeID = callType.ID,
                        Score = callType.Score,
                        IsGetScore = (!string.IsNullOrEmpty(item.Mobile)) ? true : false,
                        IsLatestScore = true
                    };

                    var leadEmailModel = new Database.Models.CTM.LeadScoring()
                    {
                        LeadID = leadModel.ID,
                        LeadScoringTypeID = emailType.ID,
                        Score = emailType.Score,
                        IsGetScore = false, // ไม่มีการยืนยันอีเมล
                        IsLatestScore = true
                    };

                    var isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == leadModel.PhoneNumber).AnyAsync();
                    var leadContactModel = new Database.Models.CTM.LeadScoring()
                    {
                        LeadID = leadModel.ID,
                        LeadScoringTypeID = contactType.ID,
                        Score = contactType.Score,
                        IsGetScore = isContact,
                        IsLatestScore = true
                    };

                    var leadBookingModel = new Database.Models.CTM.LeadScoring()
                    {
                        LeadID = leadModel.ID,
                        LeadScoringTypeID = bookType.ID,
                        Score = bookType.Score,
                        IsGetScore = false,
                        IsLatestScore = true
                    };

                    var isProject = await DB.Leads.Where(o => o.ProjectID == leadModel.ProjectID && o.PhoneNumber == leadModel.PhoneNumber).AnyAsync();
                    var leadProjectModel = new Database.Models.CTM.LeadScoring()
                    {
                        LeadID = leadModel.ID,
                        LeadScoringTypeID = projectType.ID,
                        Score = projectType.Score,
                        IsGetScore = isProject,
                        IsLatestScore = true
                    };

                    leadSoreList.Add(leadCallModel);
                    leadSoreList.Add(leadEmailModel);
                    leadSoreList.Add(leadContactModel);
                    leadSoreList.Add(leadBookingModel);
                    leadSoreList.Add(leadProjectModel);
                    #endregion

                    List<Database.Models.CTM.LeadAssign> leadAssigns = new List<Database.Models.CTM.LeadAssign>(); 
                    if (item.CallBack == true)
                    {
                        var exitsLead = await DB.Leads.Where(o => o.PhoneNumber == leadModel.PhoneNumber && o.ProjectID == leadModel.ProjectID).FirstOrDefaultAsync();
                        if (exitsLead != null)
                        {
                            leadModel.OwnerID = exitsLead.OwnerID;

                            Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                            {
                                LeadID = leadModel.ID,
                                OwnerID = exitsLead.OwnerID,
                                IsAssignByUser = true
                            };

                            leadAssigns.Add(leadAssign);
                        }
                        else
                        {
                            var latestUser = await DB.LeadAssigns
                                .Include(o => o.Lead)
                                .Where(o => o.Lead.ProjectID == leadModel.ProjectID && o.IsAssignByUser == false)
                                .OrderByDescending(o => o.Created)
                                .FirstOrDefaultAsync();
                            if(latestUser != null)
                            {
                                var allUsers = await DB.UserAuthorizeProjects.Include(o => o.User).Where(o => o.ProjectID == leadModel.ProjectID).OrderBy(o => o.User.EmployeeNo).ToListAsync();
                                var countUser = allUsers.Count;
                                if (countUser > 0)
                                {
                                    var index = allUsers.FindIndex(o => o.UserID == latestUser.OwnerID);
                                    if(index == (countUser - 1))
                                    {
                                        var user = await DB.UserAuthorizeProjects.Include(o => o.User).Where(o => o.ProjectID == leadModel.ProjectID).OrderBy(o => o.User.EmployeeNo).FirstOrDefaultAsync();
                                        leadModel.OwnerID = user.UserID;

                                        Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                                        {
                                            LeadID = leadModel.ID,
                                            OwnerID = user.UserID,
                                            IsAssignByUser = false
                                        };

                                        leadAssigns.Add(leadAssign);
                                    }
                                    else
                                    {
                                        var nextUser = allUsers[index + 1];
                                        leadModel.OwnerID = nextUser.UserID;

                                        Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                                        {
                                            LeadID = leadModel.ID,
                                            OwnerID = nextUser.UserID,
                                            IsAssignByUser = false
                                        };

                                        leadAssigns.Add(leadAssign);
                                    }
                                }
                            }
                        }
                    }
                    
                    leadModel.LeadScore = leadSoreList.Where(o => o.IsGetScore == true).Sum(o => o.Score);
                    await DB.Leads.AddAsync(leadModel);
                    await DB.LeadScorings.AddRangeAsync(leadSoreList);

                    if(leadAssigns.Count > 0)
                    {
                        await DB.LeadAssigns.AddRangeAsync(leadAssigns);
                    }

                    await DB.SaveChangesAsync();

                    #region Activity
                    var project = await DB.Projects.Include(o => o.ProductType).Where(o => o.ProjectNo == item.ProjectID).FirstAsync();
                    var duedate = DateTime.Now;

                    if (project.ProductType != null)
                    {
                        switch (project.ProductType.Key)
                        {
                            case "1":
                                duedate.AddDays(2);
                                break;
                            case "2":
                                duedate.AddDays(1);
                                break;
                        }
                    }

                    var leadActivity = new Database.Models.CTM.LeadActivity()
                    {
                        LeadID = leadModel.ID,
                        LeadActivityTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "LeadActivityType").Select(o => o.ID).FirstAsync(),
                        DueDate = duedate,
                        IsFollowUpActivity = false
                    };
                    #endregion

                    await DB.LeadActivities.AddAsync(leadActivity);
                    await DB.SaveChangesAsync();
                }
            }

            return new LeadSyncResponse()
            {
                Created = leadJsonList.Count
            };
        }

        /// <summary>
        /// ดึงข้อมูล Lead จาก AP Thai Web API ตามช่วงเวลาที่กำหนด
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<LeadSyncResponse> SyncLeadsFromAPThaiWeb(DateTime startTime, DateTime endTime)
        {
            var tokenClient = new HttpClient();
            var tokenRequestUrl = $"{Configuration["APThaiAPI:Host"]}/api/crm_leads/getToken";
            var parameters = new Dictionary<string, string> { { "username", $"{Configuration["APThaiAPI:username"]}" }, { "password", $"{Configuration["APThaiAPI:password"]}" }, { "command", $"{Configuration["APThaiAPI:command"]}" } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var tokenResponse = await tokenClient.PostAsync(tokenRequestUrl, encodedContent);
            var tokenData = await tokenResponse.Content.ReadAsStringAsync();
            var tokenResults = JsonConvert.DeserializeObject<Token>(tokenData);

            var client = new HttpClient();
            var requestUrl = $"{Configuration["APThaiAPI:Host"]}/api/crm_leads/getLeads?command=get_leads_data&start_date=" + startTime.ToString("yyyy-MM-dd-HH-mm") + "&end_date=" + endTime.ToString("yyyy-MM-dd-HH-mm") + "&token=" + tokenResults.d.token;
            var response = await client.GetAsync(requestUrl);
            var data = await response.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(Response));
            StringReader dataReader = new StringReader(data);
            Response results = (Response)serializer.Deserialize(dataReader);

            var count = 0;
            if(results != null)
            {
                count = results.Transaction.Count;
                foreach (var item in results.Transaction)
                {
                    var isExistLead = await DB.Leads.Where(o => o.RefID == item.LeadsID).AnyAsync();
                    if (!isExistLead)
                    {
                        var leadModel = new Database.Models.CTM.Lead()
                        {
                            RefID = item.LeadsID,
                            FirstName = item.Firstname,
                            LastName = item.Lastname,
                            Email = item.Email,
                            PhoneNumber = item.Tel,
                            HouseNo = item.House_id,
                            Moo = item.House_Moo,
                            Village = item.House_Village,
                            Road = item.House_Road,
                            Soi = item.House_Soi,
                            SubDistrict = item.House_Subdistrict,
                            District = item.House_District,
                            Country = item.House_Country,
                            PostalCode = item.House_Postalcode,
                            DistrictOfWorking = item.Work_Ampher,
                            ProvinceOfWorking = item.Work_Province,
                            RoomType = item.Room_Type,
                            Remark = item.Remark,
                            UTMCampaign = item.Utm_Campaign,
                            UTMMedium = item.Utm_Source,
                            UTMSource = item.Utm_Source,
                            LeadVisitTime = item.Visit_Time
                        };

                        try
                        {
                            leadModel.LeadVisitDate = DateTime.Parse(item.Visit_Date);
                        }
                        catch { }

                        leadModel.IsDeleted = false;
                        leadModel.SubLeadType = "Web";
                        leadModel.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "0" && o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).FirstOrDefaultAsync();
                        leadModel.LeadTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "W" && o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).FirstOrDefaultAsync();
                        leadModel.IsPhoneNumberConfirmed = false;
                        leadModel.IsEmailConfirmed = false;

                        if (!string.IsNullOrEmpty(item.ProductID))
                            leadModel.ProjectID = await DB.Projects.Where(o => o.ProjectNo == item.ProductID).Select(o => o.ID).FirstOrDefaultAsync();

                        #region Lead Scoring
                        var leadScoringTypeList = await DB.LeadScoringTypes.ToListAsync();
                        var callType = leadScoringTypeList.Where(o => o.Key == "1").First();
                        var emailType = leadScoringTypeList.Where(o => o.Key == "2").First();
                        var contactType = leadScoringTypeList.Where(o => o.Key == "3").First();
                        var bookType = leadScoringTypeList.Where(o => o.Key == "4").First();
                        var projectType = leadScoringTypeList.Where(o => o.Key == "5").First();

                        List<Database.Models.CTM.LeadScoring> leadSoreList = new List<Database.Models.CTM.LeadScoring>();
                        var leadCallModel = new Database.Models.CTM.LeadScoring()
                        {
                            LeadID = leadModel.ID,
                            LeadScoringTypeID = callType.ID,
                            Score = callType.Score,
                            IsLatestScore = true
                        };

                        if (!string.IsNullOrEmpty(item.SMS_Status))
                        {
                            leadCallModel.IsGetScore = (Int32.Parse(item.SMS_Status) == 1) ? true : false;
                            if (leadCallModel.IsGetScore)
                            {
                                leadModel.IsPhoneNumberConfirmed = true;
                            }
                        }
                        else
                        {
                            leadCallModel.IsGetScore = false;
                        }

                        var leadEmailModel = new Database.Models.CTM.LeadScoring()
                        {
                            LeadID = leadModel.ID,
                            LeadScoringTypeID = emailType.ID,
                            Score = emailType.Score,
                            IsLatestScore = true
                        };

                        if (!string.IsNullOrEmpty(item.Email_Status))
                        {
                            leadEmailModel.IsGetScore = (Int32.Parse(item.Email_Status) == 1) ? true : false;
                            if (leadEmailModel.IsGetScore)
                            {
                                leadModel.IsEmailConfirmed = true;
                            }
                        }
                        else
                        {
                            leadEmailModel.IsGetScore = false;
                        }

                        var isContact = false;
                        if(leadModel.PhoneNumber != null)
                        {
                            isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == leadModel.PhoneNumber).AnyAsync();
                        }

                        if(leadModel.Email != null)
                        {
                            isContact = await DB.ContactEmails.Where(o => o.Email == leadModel.Email).AnyAsync();
                        }

                        var leadContactModel = new Database.Models.CTM.LeadScoring()
                        {
                            LeadID = leadModel.ID,
                            LeadScoringTypeID = contactType.ID,
                            Score = contactType.Score,
                            IsGetScore = isContact,
                            IsLatestScore = true
                        };

                        var leadBookingModel = new Database.Models.CTM.LeadScoring()
                        {
                            LeadID = leadModel.ID,
                            LeadScoringTypeID = bookType.ID,
                            Score = bookType.Score,
                            IsGetScore = false,
                            IsLatestScore = true
                        };

                        var isProject = await DB.Leads.Where(o => o.ProjectID == leadModel.ProjectID && o.PhoneNumber == leadModel.PhoneNumber).AnyAsync();
                        var leadProjectModel = new Database.Models.CTM.LeadScoring()
                        {
                            LeadID = leadModel.ID,
                            LeadScoringTypeID = projectType.ID,
                            Score = projectType.Score,
                            IsGetScore = isProject,
                            IsLatestScore = true
                        };

                        leadSoreList.Add(leadCallModel);
                        leadSoreList.Add(leadEmailModel);
                        leadSoreList.Add(leadContactModel);
                        leadSoreList.Add(leadBookingModel);
                        leadSoreList.Add(leadProjectModel);
                        #endregion

                        #region Lead Assign
                        List<Database.Models.CTM.LeadAssign> leadAssigns = new List<Database.Models.CTM.LeadAssign>();
                        var exitsLead = await DB.Leads.Where(o => o.PhoneNumber == leadModel.PhoneNumber && o.ProjectID == leadModel.ProjectID).FirstOrDefaultAsync();
                        if (exitsLead != null)
                        {
                            leadModel.OwnerID = exitsLead.OwnerID;

                            Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                            {
                                LeadID = leadModel.ID,
                                OwnerID = exitsLead.OwnerID,
                                IsAssignByUser = true
                            };

                            leadAssigns.Add(leadAssign);
                        }
                        else
                        {
                            var latestUser = await DB.LeadAssigns
                                .Include(o => o.Lead)
                                .Where(o => o.Lead.ProjectID == leadModel.ProjectID && o.IsAssignByUser == false)
                                .OrderByDescending(o => o.Created)
                                .FirstOrDefaultAsync();
                            if (latestUser != null)
                            {
                                var allUsers = await DB.UserAuthorizeProjects.Include(o => o.User).Where(o => o.ProjectID == leadModel.ProjectID).OrderBy(o => o.User.EmployeeNo).ToListAsync();
                                var countUser = allUsers.Count;
                                if (countUser > 0)
                                {
                                    var index = allUsers.FindIndex(o => o.UserID == latestUser.OwnerID);
                                    if (index == (countUser - 1))
                                    {
                                        var user = await DB.UserAuthorizeProjects.Include(o => o.User).Where(o => o.ProjectID == leadModel.ProjectID).OrderBy(o => o.User.EmployeeNo).FirstOrDefaultAsync();
                                        leadModel.OwnerID = user.UserID;

                                        Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                                        {
                                            LeadID = leadModel.ID,
                                            OwnerID = user.UserID,
                                            IsAssignByUser = false
                                        };

                                        leadAssigns.Add(leadAssign);
                                    }
                                    else
                                    {
                                        var nextUser = allUsers[index + 1];
                                        leadModel.OwnerID = nextUser.UserID;

                                        Database.Models.CTM.LeadAssign leadAssign = new Database.Models.CTM.LeadAssign()
                                        {
                                            LeadID = leadModel.ID,
                                            OwnerID = nextUser.UserID,
                                            IsAssignByUser = false
                                        };

                                        leadAssigns.Add(leadAssign);
                                    }
                                }
                            }
                        }
                        #endregion

                        leadModel.LeadScore = leadSoreList.Where(o => o.IsGetScore == true).Sum(o => o.Score);
                        await DB.Leads.AddAsync(leadModel);
                        await DB.LeadScorings.AddRangeAsync(leadSoreList);

                        if (leadAssigns.Count > 0)
                        {
                            await DB.LeadAssigns.AddRangeAsync(leadAssigns);
                        }

                        await DB.SaveChangesAsync();

                        #region Activity
                        var project = await DB.Projects.Include(o => o.ProductType).Where(o => o.ID == leadModel.ProjectID).FirstAsync();
                        var duedate = DateTime.Now;

                        if (project.ProductType != null)
                        {
                            switch (project.ProductType.Key)
                            {
                                case "1":
                                    duedate.AddDays(2);
                                    break;
                                case "2":
                                    duedate.AddDays(1);
                                    break;
                            }
                        }

                        var leadActivity = new Database.Models.CTM.LeadActivity()
                        {
                            LeadID = leadModel.ID,
                            LeadActivityTypeMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "LeadActivityType").Select(o => o.ID).FirstAsync(),
                            DueDate = duedate,
                            IsFollowUpActivity = false
                        };
                        #endregion

                        await DB.LeadActivities.AddAsync(leadActivity);
                        await DB.SaveChangesAsync();
                    }
                }
            }

            return new LeadSyncResponse()
            {
                Created = count
            };
        }

    }
}
