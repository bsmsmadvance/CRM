using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using Database.Models;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Services.VisitorService
{
    public class VisitorService : IVisitorService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public VisitorService(DatabaseContext db, IConfiguration configuration)
        {
            this.DB = db;
            this.Configuration = configuration;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<VisitorPaging> GetVisitorListAsync(VisitorFilter filter, PageParam pageParam, VisitorListSortByParam sortByParam)
        {
            var query = from v in DB.Visitors
                        join phone in DB.ContactPhones on v.ContactID equals phone.ContactID into g
                        from t in g.Where(o => o.IsMain == true).DefaultIfEmpty()
                        select new VisitorQueryResult
                        {
                            Visitor = v,
                            Contact = v.Contact,
                            Project = v.Project,
                            VisitBy = v.VisitBy,
                            Vehicle = v.Vehicle,
                            WalkStatus = v.WalkStatus,
                            ContactStatus = v.ContactStatus,
                            Owner = v.Owner,
                            ContactPhone = t
                        };

            #region Filter
            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.VisitByKey))
            {
                var VisitMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.VisitByKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.VisitBy)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Visitor.VisitByMasterCenterID == VisitMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.VehicleKey))
            {
                var VehicleMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.VehicleKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.Vehicle)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Visitor.VehicleMasterCenterID == VehicleMasterCenterID);
            }

            if (filter.IsContact != null)
            {
                if (filter.IsContact == true)
                {
                    var contactStatusMasterCenterIDs = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactStatus && (o.Key == ContactStatusKeys.New || o.Key == ContactStatusKeys.Old))
                        .Select(x => x.ID)
                        .ToListAsync();
                    query = query.Where(q => contactStatusMasterCenterIDs.Contains(q.Visitor.ContactStatusMasterCenterID ?? Guid.Empty));
                }
                else
                {
                    var noneContactStatusMasterCenterIDs = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactStatus && o.Key != ContactStatusKeys.New && o.Key != ContactStatusKeys.Old)
                        .Select(x => x.ID)
                        .ToListAsync();
                    query = query.Where(q => noneContactStatusMasterCenterIDs.Contains(q.Visitor.ContactStatusMasterCenterID ?? Guid.Empty) || q.Visitor.ContactStatusMasterCenterID == null);
                }
            }

            if (filter.VisitDateInFrom != null && filter.VisitDateInTo != null)
                query = query.Where(q => q.Visitor.VisitDateIn >= filter.VisitDateInFrom && q.Visitor.VisitDateIn <= filter.VisitDateInTo);

            if (!string.IsNullOrEmpty(filter.ReceiveNumber))
            {
                query = query.Where(q => q.Visitor.VisitorRunning.Contains(filter.ReceiveNumber));
            }

            if (!string.IsNullOrEmpty(filter.ContactNo))
            {
                query = query.Where(q => q.Contact.ContactNo.Contains(filter.ContactNo));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(q => string.Format("{0}{1}", (!string.IsNullOrEmpty(q.Contact.FirstNameTH)) ? q.Contact.FirstNameTH : q.Visitor.FirstNameTH, (!string.IsNullOrEmpty(q.Contact.LastNameTH)) ? q.Contact.LastNameTH : q.Visitor.LastNameTH).Contains(filter.FullName.Trim()));
            }

            if (!string.IsNullOrEmpty(filter.VehicleDescription))
            {
                query = query.Where(q => q.Visitor.VehicleDescription.Contains(filter.VehicleDescription));
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(q => q.ContactPhone.PhoneNumber.Contains(filter.PhoneNumber));
            }

            if (filter.OwnerID != null)
            {
                query = query.Where(q => q.Visitor.OwnerID == filter.OwnerID);
            }
            #endregion

            VisitorListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<VisitorQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var result = queryResults.Select(o => VisitorListDTO.CreateFromModelAsync(o, FileHelper, DB)).Select(x => x.Result).ToList();

            return new VisitorPaging()
            {
                PageOutput = pageOutput,
                Visitors = result
            };
        }

        public async Task<VisitorDTO> GetVisitorAsync(Guid id)
        {
            var model = await DB.Visitors
                .Include(o => o.Contact)
                .Include(o => o.Project)
                .Include(o => o.ContactStatus)
                .Include(o => o.VisitBy)
                .Include(o => o.Vehicle)
                .Include(o => o.WalkStatus)
                .Where(c => c.ID == id)
                .FirstOrDefaultAsync();
            if (model == null)
                return null;

            var result = await VisitorDTO.CreateFromModelAsync(model, DB);

            return result;
        }

        public async Task<VisitorDTO> CreateVisitorAsync(VisitorCreateDTO input)
        {
            var existVisitor = await DB.Visitors.Where(o => o.VisitKioskTransactionID == input.VisitKioskTransactionID && o.VisitKioskDeviceID == input.VisitKioskDeviceID).AnyAsync();
            if (!existVisitor)
            {
                var model = new Visitor();
                input.ToModel(ref model);
                model.IsWelcome = false;
                model.ProjectID = await DB.Projects.Where(o => o.ProjectNo == input.ProjectNo).Select(o => o.ID).FirstAsync();

                var contactStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactStatus && o.Key == input.ContactStatusKey).Select(o => o.ID).FirstAsync();
                var PersonalVisitCardTypeID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PersonalVisitCardType && o.Key == input.PersonalVisitCardTypeKey).Select(o => o.ID).FirstAsync();
                var VisitByID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.VisitBy && o.Key == input.VisitByKey).Select(o => o.ID).FirstAsync();
                var VehicleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Vehicle && o.Key == input.VehicleKey).Select(o => o.ID).FirstAsync();

                model.ContactStatusMasterCenterID = contactStatusID;
                model.VisitByMasterCenterID = VisitByID;
                model.VehicleMasterCenterID = VehicleID;
                model.PersonalVisitCardTypeMasterCenterID = PersonalVisitCardTypeID;

                var contact = await DB.Contacts.Where(o => o.CitizenIdentityNo == input.CitizenIdentityNo).FirstOrDefaultAsync();
                if (contact != null)
                {
                    model.ContactID = contact.ID;

                    var visitorRelated = await DB.Visitors.Include(o => o.WalkStatus).Where(o => o.ContactID == contact.ID).ToListAsync();
                    if (visitorRelated.Count > 0)
                    {
                        var latestVisitor = visitorRelated.OrderByDescending(o => o.Created).First();
                        var walkStatus = await DB.MasterCenters
                            .Where(x => x.Order == (latestVisitor.WalkStatus.Order + 1) && x.MasterCenterGroupKey == MasterCenterGroupKeys.VisitorWalkStatus)
                            .FirstOrDefaultAsync();

                        if (walkStatus != null)
                        {
                            model.VisitorWalkStatusMasterCenterID = walkStatus.ID;
                        }
                        else
                        {
                            model.VisitorWalkStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.VisitorWalkStatus).OrderByDescending(o => o.Order).Select(o => o.ID).FirstAsync();
                        }
                    }
                    else
                    {
                        model.VisitorWalkStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.VisitorWalkStatus && o.Key == "F").Select(o => o.ID).FirstAsync();
                    }
                }
                else
                {
                    model.VisitorWalkStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.VisitorWalkStatus && o.Key == "F").Select(o => o.ID).FirstAsync();
                }

                await DB.Visitors.AddAsync(model);
                await DB.SaveChangesAsync();

                var result = await this.GetVisitorAsync(model.ID);

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<VisitorDTO> UpdateVisitorTypeAsync(Guid id, VisitorDTO input)
        {

            var model = await DB.Visitors.Where(c => c.ID == id).FirstAsync();
            model.ContactStatusMasterCenterID = input.ContactStatus?.Id;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetVisitorAsync(model.ID);
            return result;
        }

        public async Task<VisitorProjectDTO> GetVisitorProjectAsync(VisitorFilter filter)
        {
            var query = from v in DB.Visitors
                        join phone in DB.ContactPhones on v.ContactID equals phone.ContactID into g
                        from t in g.Where(o => o.IsMain == true).DefaultIfEmpty()
                        select new VisitorQueryResult
                        {
                            Visitor = v,
                            Contact = v.Contact,
                            Project = v.Project,
                            VisitBy = v.VisitBy,
                            Vehicle = v.Vehicle,
                            WalkStatus = v.WalkStatus,
                            ContactStatus = v.ContactStatus,
                            Owner = v.Owner,
                            ContactPhone = t
                        };

            #region Filter
            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.VisitByKey))
            {
                var VisitMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.VisitByKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.VisitBy)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Visitor.VisitByMasterCenterID == VisitMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.VehicleKey))
            {
                var VehicleMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.VehicleKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.Vehicle)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Visitor.VehicleMasterCenterID == VehicleMasterCenterID);
            }

            if (filter.IsContact != null)
            {
                if (filter.IsContact == true)
                {
                    var contactStatusMasterCenterIDs = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactStatus && (o.Key == ContactStatusKeys.New || o.Key == ContactStatusKeys.Old))
                        .Select(x => x.ID)
                        .ToListAsync();
                    query = query.Where(q => contactStatusMasterCenterIDs.Contains(q.Visitor.ContactStatusMasterCenterID ?? Guid.Empty));
                }
                else
                {
                    var noneContactStatusMasterCenterIDs = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactStatus && o.Key != ContactStatusKeys.New && o.Key != ContactStatusKeys.Old)
                        .Select(x => x.ID)
                        .ToListAsync();
                    query = query.Where(q => noneContactStatusMasterCenterIDs.Contains(q.Visitor.ContactStatusMasterCenterID ?? Guid.Empty) || q.Visitor.ContactStatusMasterCenterID == null);
                }
            }

            if (filter.VisitDateInFrom != null && filter.VisitDateInTo != null)
                query = query.Where(q => q.Visitor.VisitDateIn >= filter.VisitDateInFrom && q.Visitor.VisitDateIn <= filter.VisitDateInTo);

            if (!string.IsNullOrEmpty(filter.ReceiveNumber))
            {
                query = query.Where(q => q.Visitor.VisitorRunning.Contains(filter.ReceiveNumber));
            }

            if (!string.IsNullOrEmpty(filter.ContactNo))
            {
                query = query.Where(q => q.Contact.ContactNo.Contains(filter.ContactNo));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(q => string.Format("{0}{1}", (!string.IsNullOrEmpty(q.Visitor.FirstNameTH)) ? q.Visitor.FirstNameTH : q.Contact.FirstNameTH, (!string.IsNullOrEmpty(q.Visitor.LastNameTH)) ? q.Visitor.LastNameTH : q.Contact.LastNameTH).Contains(filter.FullName.Trim()));
            }

            if (!string.IsNullOrEmpty(filter.VehicleDescription))
            {
                query = query.Where(q => q.Visitor.VehicleDescription.Contains(filter.VehicleDescription));
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(q => q.ContactPhone.PhoneNumber.Contains(filter.PhoneNumber));
            }

            if (filter.OwnerID != null)
            {
                query = query.Where(q => q.Visitor.OwnerID == filter.OwnerID);
            }
            #endregion

            var result = new VisitorProjectDTO();
            result.VisitInOutCount = query.Count();
            result.VisitorWelcomeCount = query.Where(o => o.Visitor.IsWelcome == true).Count();

            return result;
        }

        public async Task<VisitorDTO> SubmitOrUnSubmitVisitorWelcomeAsync(Guid id, bool isWelcome, Guid? UserID)
        {
            var model = await DB.Visitors.Where(c => c.ID == id).FirstAsync();

            model.IsWelcome = isWelcome;
            if (isWelcome)
            {
                if (UserID != null)
                {
                    model.OwnerID = UserID;
                }
            }

            DB.Entry(model).State = EntityState.Modified;

            if (model.ContactID != null && isWelcome)
            {
                var opportunity = await DB.Opportunities.Where(o => o.ContactID == model.ContactID && o.ProjectID == model.ProjectID).FirstOrDefaultAsync();
                if (opportunity != null)
                {
                    var revisitOldModel = await DB.RevisitActivities
                        .Include(o => o.RevisitActivityType)
                        .Include(o => o.ConvenientTime)
                        .Where(o => o.OpportunityID == opportunity.ID)
                        .OrderByDescending(o => o.RevisitActivityType.Order)
                        .FirstOrDefaultAsync();

                    var revisitDraft = await RevisitActivityDTO.CreateDraftFromModelAsync(revisitOldModel, DB);

                    var revisitModel = new RevisitActivity()
                    {
                        RevisitActivityTypeMasterCenterID = revisitDraft.ActivityType.Id,
                        ActualDate = DateTime.Now,
                        IsCompleted = false,
                        OpportunityID = opportunity.ID
                    };

                    await DB.RevisitActivities.AddAsync(revisitModel);
                }
            }

            await DB.SaveChangesAsync();

            var result = await this.GetVisitorAsync(model.ID);
            return result;
        }

        public async Task<List<VisitorHistoryDTO>> GetVisitorHistoryListAsync(Guid id, VisitorHistoryListSortByParam sortByParam)
        {
            var model = await DB.Visitors.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (model.ContactID != null)
            {
                var query = from v in DB.Visitors.Include(o => o.Project).Include(o => o.SalesOpportunity)
                            where v.ContactID == model.ContactID && v.ID != model.ID
                            select new VisitorHistoryQueryResult()
                            {
                                Project = v.Project,
                                SalesOpportunity = v.SalesOpportunity,
                                Visitor = v
                            };

                VisitorHistoryDTO.SortBy(sortByParam, ref query);

                var queryResults = await query.ToListAsync();
                var result = queryResults.Select(o => VisitorHistoryDTO.CreateFromQueryResult(o)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<LeadListDTO>> GetVisitorLeadListAsync(Guid id, VisitorLeadListSortByParam sortByParam)
        {
            var model = await DB.Visitors.Where(c => c.ID == id).FirstAsync();
            IQueryable<LeadQueryResult> query = DB.Leads.Where(o => o.ContactID == model.ContactID && o.ContactID != null).Select(o => new LeadQueryResult
            {
                Lead = o,
                Contact = o.Contact,
                Project = o.Project,
                LeadType = o.LeadType,
                Advertisement = o.Advertisement
            });

            LeadListDTO.VisitorSortBy(sortByParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => LeadListDTO.CreateFromQueryResult(o)).ToList();

            return result;
        }

        /// <summary>
        /// ประวัติการซื้อโครงการ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        public async Task<List<VisitorPurchaseHistoryDTO>> GetVisitorPurchaseHistoryListAsync(Guid id, VisitorPurchaseHistoryListSortByParam sortByParam)
        {
            //TODO: [Palm] [Visitor] GetVisitorPurchaseHistoryListAsync
            throw new NotImplementedException();
        }

        /// <summary>
        /// ประวัติการตอบแบบสอบถาม
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        public async Task<List<VisitorQuestionnaireHistoryDTO>> GetVisitorQuestionnaireHistoryListAsync(Guid id, VisitorQuestionnaireHistoryListSortByParam sortByParam)
        {
            //TODO: [Palm] [Visitor] GetVisitorQuestionnaireHistoryListAsync
            throw new NotImplementedException();
        }


    }
}
