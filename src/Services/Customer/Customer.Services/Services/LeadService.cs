using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.USR;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using Database.Models;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace Customer.Services.LeadService
{
    public class LeadService : ILeadService
    {
        private readonly DatabaseContext DB;

        public LeadService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<LeadPaging> GetLeadListAsync(LeadFilter filter, PageParam pageParam, LeadListSortByParam sortByParam)
        {
            IQueryable<LeadQueryResult> query = DB.Leads.Select(o => new LeadQueryResult
            {
                Lead = o,
                Contact = o.Contact,
                Project = o.Project,
                LeadType = o.LeadType,
                Owner = o.Owner
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(q => q.Lead.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(q => q.Lead.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(q => q.Lead.PhoneNumber.Contains(filter.PhoneNumber));

            if (!string.IsNullOrEmpty(filter.LeadTypeKey))
            {
                var LeadTypeMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.LeadTypeKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Lead.LeadTypeMasterCenterID == LeadTypeMasterCenterID);
            }

            if (filter.OwnerID != null)
            {
                if (filter.OwnerID == Guid.Empty)
                {
                    query = query.Where(q => q.Lead.OwnerID == null);
                }
                else
                {
                    query = query.Where(q => q.Lead.OwnerID == filter.OwnerID);
                }
            }

            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.LeadStatusKey))
            {
                var LeadTypeMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.LeadTypeKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Lead.LeadStatusMasterCenterID == LeadTypeMasterCenterID);
            }
            else
            {
                var LeadTypeMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == LeadStatusKeys.InProgress && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Lead.LeadStatusMasterCenterID == LeadTypeMasterCenterID);
            }

            if (filter.CreatedDateFrom != null && filter.CreatedDateTo != null)
                query = query.Where(q => q.Lead.Created >= filter.CreatedDateFrom && q.Lead.Created <= filter.CreatedDateTo);

            if (!string.IsNullOrEmpty(filter.ExcludeIDs))
            {
                var excludeIDs = filter.ExcludeIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(o => !excludeIDs.Contains(o.Lead.ID));
            }
            #endregion

            LeadListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<LeadQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => LeadListDTO.CreateFromQueryResult(o)).ToList();

            return new LeadPaging()
            {
                PageOutput = pageOutput,
                Leads = result
            };
        }

        public async Task<LeadDTO> GetLeadAsync(Guid id)
        {
            var model = await DB.Leads
                .Include(o => o.LeadType)
                .Include(o => o.Project)
                .Include(o => o.Advertisement)
                .Include(o => o.LeadStatus)
                .Include(o => o.Owner)
                .Where(c => c.ID == id).FirstOrDefaultAsync();
            if (model == null)
                return null;

            var result = await LeadDTO.CreateFromModel(model, DB);

            return result;
        }

        public async Task<LeadDTO> CreateLeadAsync(LeadDTO input, Guid? userID)
        {
            await input.ValidateAsync(DB);

            var model = new Lead();
            input.ToModel(ref model);
            model.SubLeadType = "Call In";
            model.IsEmailConfirmed = false;
            model.IsPhoneNumberConfirmed = false;
            model.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus && o.Key == LeadStatusKeys.InProgress).Select(o => o.ID).FirstAsync();
            model.LeadTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType && o.Key == LeadTypeKeys.Call).Select(o => o.ID).FirstAsync();

            if (userID != null)
            {
                model.OwnerID = userID;
            }


            #region Lead Scoring
            var leadScoringTypeList = await DB.LeadScoringTypes.ToListAsync();
            var callType = leadScoringTypeList.Where(o => o.Key == "1").First();
            var emailType = leadScoringTypeList.Where(o => o.Key == "2").First();
            var contactType = leadScoringTypeList.Where(o => o.Key == "3").First();
            var bookType = leadScoringTypeList.Where(o => o.Key == "4").First();
            var projectType = leadScoringTypeList.Where(o => o.Key == "5").First();

            List<LeadScoring> leadSoreList = new List<LeadScoring>();
            var leadCallModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = callType.ID,
                Score = callType.Score,
                IsGetScore = false, // LCM ไม่มีการยืนยันเบอร์โทรติดต่อ
                IsLatestScore = true
            };

            var leadEmailModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = emailType.ID,
                Score = emailType.Score,
                IsGetScore = false, // LCM ไม่มีการยืนยันอีเมล
                IsLatestScore = true
            };

            var isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == model.PhoneNumber).AnyAsync();
            if (!isContact)
            {
                if (model.Telephone != null)
                {
                    isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == model.Telephone).AnyAsync();
                }
                if (!isContact)
                {
                    if (model.Email != null)
                    {
                        isContact = await DB.ContactEmails.Where(o => o.Email == model.Email).AnyAsync();
                    }
                }
            }

            var leadContactModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = contactType.ID,
                Score = contactType.Score,
                IsGetScore = isContact,
                IsLatestScore = true
            };

            var leadBookingModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = bookType.ID,
                Score = bookType.Score, // TODO: [Palm][Lead] การนับคะแนนจากการจอง (รอส่วนการจอง)
                IsGetScore = false,
                IsLatestScore = true
            };

            var isProject = await DB.Leads.Where(o => o.ProjectID == model.ProjectID && o.PhoneNumber == model.PhoneNumber).AnyAsync();
            if (!isProject)
            {
                if (model.Telephone != null)
                {
                    isProject = await DB.Leads.Where(o => o.ProjectID == model.ProjectID && o.Telephone == model.Telephone).AnyAsync();
                }
            }

            var leadProjectModel = new LeadScoring()
            {
                LeadID = model.ID,
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

            model.LeadScore = leadSoreList.Where(o => o.IsGetScore == true).Sum(o => o.Score);

            await DB.Leads.AddAsync(model);
            await DB.LeadScorings.AddRangeAsync(leadSoreList);
            await DB.SaveChangesAsync();

            var leadModel = await DB.Leads
            .Include(o => o.LeadType)
            .Include(o => o.Project)
            .Include(o => o.Advertisement)
            .Include(o => o.LeadStatus)
            .Include(o => o.Owner)
            .Where(c => c.ID == model.ID).FirstOrDefaultAsync();
            var result = await LeadDTO.CreateFromModel(leadModel, DB);

            return result;
        }

        public async Task<LeadDTO> UpdateLeadAsync(Guid id, LeadDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Leads.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            if (input.Owner != null)
            {
                if (input.Owner.Id != model.OwnerID)
                {
                    model.OwnerID = input.Owner.Id;
                    LeadAssign leadAssign = new LeadAssign()
                    {
                        LeadID = id,
                        OwnerID = input.Owner.Id
                    };

                    await DB.LeadAssigns.AddAsync(leadAssign);
                }
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Lead Scoring
            var leadSoreModel = await DB.LeadScorings.Where(o => o.LeadID == model.ID).ToListAsync();
            foreach (var item in leadSoreModel)
            {
                item.IsLatestScore = false;
                DB.Entry(item).State = EntityState.Modified;
            }
            await DB.SaveChangesAsync();

            var leadScoringTypeList = await DB.LeadScoringTypes.ToListAsync();
            var callType = leadScoringTypeList.Where(o => o.Key == "1").First();
            var emailType = leadScoringTypeList.Where(o => o.Key == "2").First();
            var contactType = leadScoringTypeList.Where(o => o.Key == "3").First();
            var bookType = leadScoringTypeList.Where(o => o.Key == "4").First();
            var projectType = leadScoringTypeList.Where(o => o.Key == "5").First();

            var isOldBooking = false; // รอการจอง
            if (leadSoreModel.Count > 0)
            {
                isOldBooking = leadSoreModel.Where(o => o.LeadScoringTypeID == emailType.ID).Select(o => o.IsGetScore).First(); // รอการจอง
            }

            List<LeadScoring> leadSoreList = new List<LeadScoring>();
            var leadCallModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = callType.ID,
                Score = callType.Score,
                IsGetScore = (model.IsPhoneNumberConfirmed) ? true : false,
                IsLatestScore = true
            };

            var leadEmailModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = emailType.ID,
                Score = emailType.Score,
                IsGetScore = (model.IsEmailConfirmed) ? true : false,
                IsLatestScore = true
            };

            var isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == model.PhoneNumber).AnyAsync();
            if (!isContact)
            {
                if (model.Telephone != null)
                {
                    isContact = await DB.ContactPhones.Where(o => o.PhoneNumber == model.Telephone).AnyAsync();
                }
                if (!isContact)
                {
                    if (model.Email != null)
                    {
                        isContact = await DB.ContactEmails.Where(o => o.Email == model.Email).AnyAsync();
                    }
                }
            }

            var leadContactModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = contactType.ID,
                Score = contactType.Score,
                IsGetScore = isContact,
                IsLatestScore = true
            };

            var leadBookingModel = new LeadScoring()
            {
                LeadID = model.ID,
                LeadScoringTypeID = bookType.ID,
                Score = bookType.Score,
                IsGetScore = isOldBooking, // รอการจอง
                IsLatestScore = true
            };

            var isProject = await DB.Leads.Where(o => o.ProjectID == model.ProjectID && o.PhoneNumber == model.PhoneNumber && o.ID != id).AnyAsync();
            if (!isProject)
            {
                if (model.Telephone != null)
                {
                    isProject = await DB.Leads.Where(o => o.ProjectID == model.ProjectID && o.Telephone == model.Telephone && o.ID != id).AnyAsync();
                }
            }

            var leadProjectModel = new LeadScoring()
            {
                LeadID = model.ID,
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

            model.LeadScore = leadSoreList.Where(o => o.IsGetScore == true).Sum(o => o.Score);

            DB.Entry(model).State = EntityState.Modified;
            await DB.LeadScorings.AddRangeAsync(leadSoreList);
            await DB.SaveChangesAsync();

            var leadModel = await DB.Leads
            .Include(o => o.LeadType)
            .Include(o => o.Project)
            .Include(o => o.Advertisement)
            .Include(o => o.LeadStatus)
            .Include(o => o.Owner)
            .Where(c => c.ID == model.ID).FirstOrDefaultAsync();
            var result = await LeadDTO.CreateFromModel(leadModel, DB);

            return result;
        }

        public async Task<LeadListDTO> AssignLeadAsync(Guid id, UserListDTO input)
        {
            var model = await DB.Leads.Where(o => o.ID == id).FirstAsync();

            #region Validate Authorize Project
            bool canAuthorizedProject = await DB.UserAuthorizeProjects.Where(o => o.ProjectID == model.ProjectID).AnyAsync();
            bool isLC = await DB.UserRoles.Include(o => o.Role).Where(o => o.Role.Code == "LC").AnyAsync();
            if (!(canAuthorizedProject && isLC))
            {
                ValidateException ex = new ValidateException();
                ex.AddError("API", "Cannot assign lead to unauthorized owner.", (int)ErrorMessageType.PopupAlert);
                throw ex;
            }
            #endregion

            model.OwnerID = input.Id;
            LeadAssign leadAssign = new LeadAssign()
            {
                LeadID = id,
                OwnerID = input.Id,
                IsAssignByUser = true
            };

            await DB.LeadAssigns.AddAsync(leadAssign);
            await DB.SaveChangesAsync();
            var leadModel = await DB.Leads
            .Include(o => o.LeadType)
            .Include(o => o.Project)
            .Include(o => o.Advertisement)
            .Include(o => o.LeadStatus)
            .Include(o => o.Owner)
            .Where(c => c.ID == model.ID).FirstOrDefaultAsync();
            var result = LeadListDTO.CreateFromModel(leadModel);

            return result;
        }

        public async Task DeleteLeadAsync(Guid id)
        {
            var model = await DB.Leads.Where(c => c.ID == id).FirstAsync();
            var activities = await DB.LeadActivities.Where(o => o.LeadID == id).ToListAsync();
            if (activities.Count > 0)
            {
                foreach (var item in activities)
                {
                    item.IsDeleted = true;
                }

                DB.UpdateRange(activities);
                await DB.SaveChangesAsync();
            }

            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<List<LeadActivityListDTO>> GetLeadActivityListAsync(Guid leadID)
        {
            var model = await DB.LeadActivities
                .Include(o => o.Lead)
                .Include(o => o.LeadActivityType)
                .Include(o => o.UpdatedBy)
                .Where(o => o.LeadID == leadID)
                .OrderByDescending(o => o.LeadActivityType.Order)
                .ToListAsync();

            var result = model.Select(o => LeadActivityListDTO.CreateFromModel(o)).ToList();

            return result;
        }

        public async Task<LeadActivityDTO> GetLeadActivityAsync(Guid id)
        {
            var model = await DB.LeadActivities
                .Include(o => o.LeadActivityType)
                .Include(o => o.ConvenientTime)
                .Include(o => o.LeadActivityStatus)
                .Include(o => o.UpdatedBy)
                .Include(o => o.CreatedBy)
                .Where(c => c.ID == id).FirstAsync();

            var result = await LeadActivityDTO.CreateFromModelAsync(model, DB);

            return result;
        }

        public async Task<LeadActivityDTO> CreateLeadActivityAsync(Guid leadID, LeadActivityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = new LeadActivity();
            input.ToModel(ref model);
            model.LeadID = leadID;
            model.IsFollowUpActivity = false;

            await DB.LeadActivities.AddAsync(model);
            await DB.SaveChangesAsync();

            var leadActivityModel = await DB.LeadActivities
                .Include(o => o.LeadActivityType)
                .Include(o => o.ConvenientTime)
                .Include(o => o.LeadActivityStatus)
                .Include(o => o.UpdatedBy)
                .Include(o => o.CreatedBy)
                .Where(c => c.ID == model.ID).FirstOrDefaultAsync();

            if (model.StatusID != null)
            {
                var correctNumberStatusID = await DB.LeadActivityStatuses.Where(o => o.Code == "1").Select(o => o.ID).FirstAsync();
                var activityStatus = await DB.LeadActivityStatuses
                    .Include(o => o.LeadActivityFollowUpType)
                    .Include(o => o.LeadActivityStatusType)
                    .Where(o => o.ID == model.StatusID).FirstAsync();

                if (activityStatus.ID != correctNumberStatusID)
                {
                    var leadActivityFollowUpTypeID = await DB.MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.FollowUp && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstAsync();
                    var leadActivityDisqualifyTypeID = await DB.MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.Disqualify && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstAsync();

                    if (activityStatus.LeadActivityFollowUpType.ID == leadActivityFollowUpTypeID)
                    {
                        var activityTypeOrder = await DB.MasterCenters
                            .Where(x => x.Key == leadActivityModel.LeadActivityType.Key && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                            .Select(x => x.Order).FirstOrDefaultAsync();

                        var activityType = await DB.MasterCenters
                            .Where(x => x.Order == (activityTypeOrder + 1) && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                            .FirstOrDefaultAsync();

                        var lastActivityTypeId = new Guid();
                        if (activityType == null)
                        {
                            lastActivityTypeId = await DB.MasterCenters
                                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                                .OrderByDescending(o => o.Order)
                                .Select(o => o.ID)
                                .FirstOrDefaultAsync();
                        }
                        else
                        {
                            lastActivityTypeId = activityType.ID;
                        }

                        #region Validate
                        ValidateException ex = new ValidateException();
                        if (input.FollowUpDueDate == null)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(LeadActivityDTO).GetProperty(nameof(LeadActivityDTO.FollowUpDueDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            throw ex;
                        }
                        #endregion

                        var secondModel = new LeadActivity()
                        {
                            LeadActivityTypeMasterCenterID = lastActivityTypeId,
                            DueDate = input.FollowUpDueDate,
                            LeadID = leadID,
                            IsFollowUpActivity = true
                        };

                        await DB.LeadActivities.AddAsync(secondModel);
                        await DB.SaveChangesAsync();
                    }
                    else if (activityStatus.LeadActivityFollowUpType.ID == leadActivityDisqualifyTypeID)
                    {
                        var leadModel = await DB.Leads.Where(o => o.ID == leadID).FirstAsync();
                        leadModel.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus && o.Key == LeadStatusKeys.Disqualify).Select(o => o.ID).FirstAsync();

                        DB.Entry(leadModel).State = EntityState.Modified;
                        await DB.SaveChangesAsync();
                    }
                }
            }

            var result = await LeadActivityDTO.CreateFromModelAsync(leadActivityModel, DB);

            return result;
        }

        public async Task<LeadActivityDTO> UpdateLeadActivityAsync(Guid id, LeadActivityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.LeadActivities.Include(o => o.UpdatedBy).Where(o => o.ID == id).FirstOrDefaultAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var leadActivityModel = await DB.LeadActivities
                .Include(o => o.LeadActivityType)
                .Include(o => o.ConvenientTime)
                .Include(o => o.LeadActivityStatus)
                .Include(o => o.UpdatedBy)
                .Include(o => o.CreatedBy)
                .Where(c => c.ID == model.ID).FirstOrDefaultAsync();

            if (model.StatusID != null)
            {
                var correctNumberStatusID = await DB.LeadActivityStatuses.Where(o => o.Code == "1").Select(o => o.ID).FirstAsync();
                var activityStatus = await DB.LeadActivityStatuses
                    .Include(o => o.LeadActivityFollowUpType)
                    .Include(o => o.LeadActivityStatusType)
                    .Where(o => o.ID == model.StatusID).FirstAsync();

                if (activityStatus.ID != correctNumberStatusID)
                {
                    var leadActivityFollowUpTypeID = await DB.MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.FollowUp && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstAsync();
                    var leadActivityDisqualifyTypeID = await DB.MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.Disqualify && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstAsync();

                    if (activityStatus.LeadActivityFollowUpType.ID == leadActivityFollowUpTypeID)
                    {
                        var activityTypeOrder = await DB.MasterCenters
                                    .Where(x => x.Key == leadActivityModel.LeadActivityType.Key && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                                    .Select(x => x.Order).FirstOrDefaultAsync();

                        var activityType = await DB.MasterCenters
                            .Where(x => x.Order == (activityTypeOrder + 1) && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                            .FirstOrDefaultAsync();

                        var lastActivityTypeId = new Guid();
                        if (activityType == null)
                        {
                            lastActivityTypeId = await DB.MasterCenters
                                .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType)
                                .OrderByDescending(o => o.Order)
                                .Select(o => o.ID)
                                .FirstOrDefaultAsync();
                        }
                        else
                        {
                            lastActivityTypeId = activityType.ID;
                        }

                        #region Validate
                        ValidateException ex = new ValidateException();
                        if (input.FollowUpDueDate == null)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(LeadActivityDTO).GetProperty(nameof(LeadActivityDTO.FollowUpDueDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            throw ex;
                        }
                        #endregion

                        var secondModel = new LeadActivity()
                        {
                            LeadActivityTypeMasterCenterID = lastActivityTypeId,
                            DueDate = DateTime.Now,
                            LeadID = leadActivityModel.LeadID,
                            IsFollowUpActivity = true
                        };

                        await DB.LeadActivities.AddAsync(secondModel);
                        await DB.SaveChangesAsync();
                    }
                    else if (activityStatus.LeadActivityFollowUpType.ID == leadActivityDisqualifyTypeID)
                    {
                        var leadModel = await DB.Leads.Where(o => o.ID == model.LeadID).FirstAsync();
                        leadModel.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus && o.Key == LeadStatusKeys.Disqualify).Select(o => o.ID).FirstAsync();

                        DB.Entry(leadModel).State = EntityState.Modified;
                        await DB.SaveChangesAsync();
                    }
                }
            }

            var result = await LeadActivityDTO.CreateFromModelAsync(leadActivityModel, DB);

            return result;
        }

        public async Task DeleteLeadActivityAsync(Guid id)
        {
            var model = await DB.LeadActivities.Where(o => o.ID == id).FirstAsync();

            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<List<LeadQualifyDTO>> GetLeadQualify(Guid id)
        {
            var leadModel = await DB.Leads.Where(o => o.ID == id).FirstAsync();

            List<Contact> model = new List<Contact>();
            List<LeadQualifyDTO> results = new List<LeadQualifyDTO>();

            var contactModel = await DB.ContactPhones
                .Include(o => o.Contact)
                .Where(o => o.Contact.FirstNameTH == leadModel.FirstName && o.Contact.LastNameTH == leadModel.LastName && o.PhoneNumber == leadModel.PhoneNumber)
                .FirstOrDefaultAsync();

            if (contactModel != null)
            {
                // กรณีพบ Contact ที่ตรงทั้งชื่อ นามสกุล และเบอร์โทรศัพท์
                var result = await LeadQualifyDTO.CreateFromModelAsync(contactModel.Contact, DB);
                result.HasExactContact = true;
                results.Add(result);
            }
            else
            {
                model = await DB.Contacts.Where(o => o.FirstNameTH == leadModel.FirstName && o.LastNameTH == leadModel.LastName).ToListAsync();

                var phoneModel = await DB.ContactPhones
                    .Include(o => o.Contact)
                    .Where(o => o.PhoneNumber == leadModel.PhoneNumber).ToListAsync();
                if (phoneModel.Count > 0)
                {
                    foreach (var phone in phoneModel)
                    {
                        var isContact = model.Where(o => o.ID == phone.ContactID).Any();
                        if (isContact != true)
                        {
                            model.Add(phone.Contact);
                        }
                    }
                }

                results = model.Select(o => LeadQualifyDTO.CreateFromModelAsync(o, DB)).Select(x => x.Result).ToList();
            }

            return results;
        }

        public async Task<LeadDTO> SubmitLeadQualify(Guid id, Guid? contactID)
        {
            var model = await DB.Leads
                .Include(o => o.LeadType)
                .Include(o => o.Project)
                .Include(o => o.Advertisement)
                .Include(o => o.LeadStatus)
                .Where(o => o.ID == id).FirstOrDefaultAsync();

            if (contactID != null)
            {
                model.ContactID = contactID;
                model.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).FirstAsync();

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                var contactModel = new Contact();

                contactModel.FirstNameTH = model.FirstName;
                contactModel.LastNameTH = model.LastName;
                contactModel.ContactTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).FirstAsync();
                contactModel.IsVIP = false;

                string year = Convert.ToString(DateTime.Today.Year);
                var runningKey = "10" + year[2] + year[3];
                var runningNumber = await DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "CTM.Contact").FirstOrDefaultAsync();
                if (runningNumber == null)
                {
                    var runningModel = new Database.Models.MST.RunningNumberCounter()
                    {
                        Key = runningKey,
                        Type = "CTM.Contact",
                        Count = 1
                    };

                    await DB.RunningNumberCounters.AddAsync(runningModel);
                    contactModel.ContactNo = runningKey + runningModel.Count.ToString("00000");
                }
                else
                {
                    runningNumber.Count = runningNumber.Count + 1;
                    contactModel.ContactNo = runningKey + runningNumber.Count.ToString("00000");
                    DB.Entry(runningNumber).State = EntityState.Modified;
                }

                await DB.Contacts.AddAsync(contactModel);
                await DB.SaveChangesAsync();

                var phoneModel = new ContactPhone()
                {
                    ContactID = contactModel.ID,
                    IsMain = true,
                    PhoneNumber = model.PhoneNumber,
                    PhoneTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "0").Select(o => o.ID).FirstAsync(),
                };
                await DB.ContactPhones.AddAsync(phoneModel);

                if (!string.IsNullOrEmpty(model.Telephone))
                {
                    var homePhoneModel = new ContactPhone()
                    {
                        ContactID = contactModel.ID,
                        IsMain = false,
                        PhoneNumber = model.Telephone,
                        PhoneTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "1").Select(o => o.ID).FirstAsync(),
                    };
                    await DB.ContactPhones.AddAsync(homePhoneModel);
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    var emailModel = new ContactEmail()
                    {
                        ContactID = contactModel.ID,
                        IsMain = true,
                        Email = model.Email
                    };
                    await DB.ContactEmails.AddAsync(emailModel);
                }

                await DB.SaveChangesAsync();

                model.ContactID = contactModel.ID;
                model.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "1").Select(o => o.ID).FirstAsync();

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            var result = await LeadDTO.CreateFromModel(model, DB);

            return result;
        }

        public async Task<LeadDTO> UnSubmitLeadQualify(Guid id)
        {
            try
            {
                var model = await DB.Leads
                    .Include("LeadType")
                    .Include("Project")
                    .Include("Advertisement")
                    .Where(o => o.ID == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.LeadStatusMasterCenterID = await DB.MasterCenters.Where(o => o.Key == "2" && o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).FirstAsync();

                    DB.Entry(model).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                    var result = await LeadDTO.CreateFromModel(model, DB);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LeadActivityDTO> GetLeadActivityDraftAsync(Guid leadID)
        {
            var model = await DB.LeadActivities
                .Include(o => o.LeadActivityType)
                .Where(o => o.LeadID == leadID).OrderByDescending(o => o.LeadActivityType.Order).FirstOrDefaultAsync();
            var result = await LeadActivityDTO.CreateDraftFromModelAsync(model, DB);
            result.LeadID = leadID;
            return result;
        }

        public async Task<LeadAssignDTO> AssignLeadListAsync(LeadAssignDTO input)
        {
            var user = await DB.Users.Where(o => o.ID == input.User.Id).FirstAsync();
            List<Lead> leads = new List<Lead>();
            List<LeadAssign> leadAssigns = new List<LeadAssign>();
            foreach (var item in input.Leads)
            {
                var lead = await DB.Leads
                    .Include(o => o.LeadType)
                    .Include(o => o.Project)
                    .Include(o => o.Advertisement)
                    .Include(o => o.LeadStatus)
                    .Include(o => o.Owner)
                    .Where(o => o.ID == item.Id).FirstAsync();
                lead.OwnerID = input.User.Id;

                LeadAssign leadAssign = new LeadAssign()
                {
                    LeadID = lead.ID,
                    OwnerID = input.User.Id,
                    IsAssignByUser = true
                };

                leads.Add(lead);
                leadAssigns.Add(leadAssign);
            }

            if (leads.Count > 0)
            {
                DB.Leads.UpdateRange(leads);
            }

            if (leadAssigns.Count > 0)
            {
                await DB.LeadAssigns.AddRangeAsync(leadAssigns);
            }

            await DB.SaveChangesAsync();
            var result = LeadAssignDTO.CreateFromModel(leads, user);
            return result;
        }

        public async Task<List<LeadListDTO>> AssignLeadListRandomAsync(Guid projectID, List<LeadListDTO> inputs)
        {
            var lcRoleID = await DB.Roles.Where(o => o.Code == "LC").Select(o => o.ID).FirstAsync();
            var lcUsers = await DB.Users
                .Where(o => o.UserAuthorizeProjects.Where(m => m.ProjectID == projectID).Any() &&
                o.UserRoles.Where(n => n.RoleID == lcRoleID).Any())
                .ToListAsync();
            var latestLeadAssign = await DB.LeadAssigns.Include(o => o.Lead).ThenInclude(o => o.Owner)
                .Where(o => o.Lead.ProjectID == projectID).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            lcUsers = lcUsers.OrderByDescending(o => o.EmployeeNo).ToList();
            User startLC = null;
            if (latestLeadAssign != null)
            {
                startLC = lcUsers.Where(o => o.EmployeeNo == latestLeadAssign.Lead?.Owner?.EmployeeNo).FirstOrDefault();
            }
            if (startLC == null)
            {
                var random = new Random();
                int index = random.Next(lcUsers.Count);
                startLC = lcUsers[index];
            }
            int startIndex = lcUsers.IndexOf(startLC);
            var leadIDs = inputs.Select(o => o.Id).ToList();
            var leads = await DB.Leads
                .Include(o => o.LeadType)
                .Include(o => o.Project)
                .Include(o => o.Advertisement)
                .Include(o => o.LeadStatus)
                .Include(o => o.Owner)
                .Where(o => leadIDs.Contains(o.ID)).ToListAsync();
            for (int i = 0; i < leads.Count; i++)
            {
                var lead = leads[i];
                lead.OwnerID = lcUsers[startIndex++].ID;

                LeadAssign leadAssign = new LeadAssign()
                {
                    LeadID = lead.ID,
                    OwnerID = lead.OwnerID,
                    IsAssignByUser = true
                };
                await DB.AddAsync(leadAssign);
                DB.Update(lead);

                if (startIndex >= lcUsers.Count)
                {
                    startIndex = 0;
                }
            }

            await DB.SaveChangesAsync();

            var results = leads.Select(o => LeadListDTO.CreateFromModel(o)).ToList();
            return results;
        }
    }
}
