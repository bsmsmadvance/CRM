using Base.DTOs.CTM;
using Customer.Params.Filters;
using Database.Models;
using Database.Models.CTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Base.DTOs;
using Customer.Params.Outputs;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;
using Database.Models.USR;
using Database.Models.MasterKeys;

namespace Customer.Services.OpportunityService
{
    public class OpportunityService : IOpportunityService
    {
        private readonly DatabaseContext DB;

        public OpportunityService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<OpportunityPaging> GetOpportunityListAsync(OpportunityFilter filter, PageParam pageParam, OpportunityListSortByParam sortByParam)
        {
            var query = from opp in DB.Opportunities
                        join phone in DB.ContactPhones on opp.ContactID equals phone.ContactID into g
                        from t in g.Where(o => o.IsMain == true).DefaultIfEmpty()
                        select new OpportunityQueryResult
                        {
                            Opportunity = opp,
                            SalesOpportunity = opp.SalesOpportunity,
                            StatusQuestionaire = opp.StatusQuestionaire,
                            Contact = opp.Contact,
                            ContactPhone = t,
                            Project = opp.Project,
                            OpportunityActivityType = opp.LastOpportunityActivity.OpportunityActivityType,
                            Owner = opp.Owner
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.SalesOpportunityKey))
            {
                var SalesOpportunityMasterCenterID = await DB.MasterCenters
                   .Where(x => x.Key == filter.SalesOpportunityKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity)
                   .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Opportunity.SalesOpportunityMasterCenterID == SalesOpportunityMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.StatusQuestionaireKey))
            {
                var StatusQuestionaireMasterCenterID = await DB.MasterCenters
                   .Where(x => x.Key == filter.StatusQuestionaireKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire)
                   .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Opportunity.SalesOpportunityMasterCenterID == StatusQuestionaireMasterCenterID);
            }

            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.ContactNo))
                query = query.Where(q => q.Contact.ContactNo.Contains(filter.ContactNo));

            if (filter.ContactID != null)
                query = query.Where(q => q.Contact.ID == filter.ContactID);

            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(q => (q.Contact.FirstNameTH + " " + q.Contact.LastNameTH).Contains(filter.FullName));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(q => q.ContactPhone.PhoneNumber.Contains(filter.PhoneNumber));

            if (filter.ArriveDateFrom != null && filter.ArriveDateTo != null)
                query = query.Where(q => q.Opportunity.ArriveDate >= filter.ArriveDateFrom && q.Opportunity.ArriveDate <= filter.ArriveDateTo);

            if (filter.UpdatedDateFrom != null && filter.UpdatedDateTo != null)
                query = query.Where(q => q.Opportunity.Updated >= filter.UpdatedDateFrom && q.Opportunity.Updated <= filter.UpdatedDateTo);

            if (filter.OwnerID != null)
            {
                if (filter.OwnerID == Guid.Empty)
                {
                    query = query.Where(q => q.Opportunity.OwnerID == null);
                }
                else
                {
                    query = query.Where(q => q.Opportunity.OwnerID == filter.OwnerID);
                }
            }

            if (!string.IsNullOrEmpty(filter.ExcludeIDs))
            {
                var excludeIDs = filter.ExcludeIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(o => !excludeIDs.Contains(o.Opportunity.ID));
            }
            #endregion

            OpportunityListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<OpportunityQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var result = queryResults.Select(o => OpportunityListDTO.CreateFromQueryResult(o, DB)).ToList();

            return new OpportunityPaging()
            {
                PageOutput = pageOutput,
                Opportunities = result
            };
        }

        public async Task<OpportunityDTO> GetOpportunityAsync(Guid id)
        {
            var model = await DB.Opportunities
                .Include(o => o.Contact)
                .Include(o => o.Project)
                .Include(o => o.EstimateSalesOpportunity)
                .Include(o => o.StatusQuestionaire)
                .Include(o => o.SalesOpportunity)
                .Include(o => o.Owner)
                .Where(c => c.ID == id).FirstOrDefaultAsync();

            var result = OpportunityDTO.CreateFromModel(model, DB);

            return result;
        }

        public async Task<OpportunityDTO> CreateOpportunityAsync(OpportunityDTO input, Guid? userID, Guid? fromVisitorID = null)
        {
            var opportunity = new Guid();
            if (fromVisitorID == null)
            {
                await input.ValidateAsync(DB);

                #region Validate
                ValidateException ex = new ValidateException();

                var isExitsProject = await DB.Opportunities.Where(o => o.ProjectID == input.Project.Id && o.ContactID == input.Contact.Id).AnyAsync();
                if (isExitsProject)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                    string desc = typeof(OpportunityDTO).GetProperty(nameof(OpportunityDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (ex.HasError)
                {
                    throw ex;
                }
                #endregion

                var model = new Opportunity();
                input.ToModel(ref model);
                model.ProductQTY = 0; // set default value

                if (input.Contact != null)
                    model.ContactID = input.Contact.Id.Value;
                if (input.Project != null)
                    model.ProjectID = input.Project.Id;

                if (userID != null)
                {
                    model.OwnerID = userID;
                }

                model.StatusQuestionaireMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire && o.Key == "1").Select(o => o.ID).FirstAsync();

                await DB.Opportunities.AddAsync(model);
                await DB.SaveChangesAsync();

                await UpdateSalesOpportunityInLatestVisitor(model);

                var activityModel = new OpportunityActivity();
                activityModel.OpportunityID = model.ID;
                activityModel.OpportunityActivityTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "1").Select(o => o.ID).FirstAsync();
                activityModel.ActualDate = DateTime.Now;
                activityModel.DueDate = DateTime.Now;
                activityModel.IsCompleted = true;

                await DB.OpportunityActivities.AddAsync(activityModel);
                await DB.SaveChangesAsync();

                opportunity = model.ID;
            }
            else
            {
                var visitorModel = await DB.Visitors.Where(o => o.ID == fromVisitorID).FirstAsync();
                var model = new Opportunity();
                model.ContactID = visitorModel.ContactID.Value;
                model.ProjectID = visitorModel.ProjectID;
                model.ArriveDate = visitorModel.VisitDateIn;
                model.ProductQTY = 0;
                model.StatusQuestionaireMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire && o.Key == "1").Select(o => o.ID).FirstAsync();

                if (userID != null)
                {
                    model.OwnerID = userID;
                }

                await DB.Opportunities.AddAsync(model);
                await DB.SaveChangesAsync();

                await UpdateSalesOpportunityInLatestVisitor(model);

                var activityModel = new OpportunityActivity();
                activityModel.OpportunityID = model.ID;
                activityModel.OpportunityActivityTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "1").Select(o => o.ID).FirstAsync();
                activityModel.ActualDate = DateTime.Today;
                activityModel.DueDate = DateTime.Today;
                activityModel.IsCompleted = true;

                await DB.OpportunityActivities.AddAsync(activityModel);
                await DB.SaveChangesAsync();

                opportunity = model.ID;
            }

            var result = await this.GetOpportunityAsync(opportunity);
            return result;
        }

        public async Task<OpportunityDTO> UpdateOpportunityAsync(Guid id, OpportunityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Opportunities.Where(o => o.ID == id).FirstOrDefaultAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            await UpdateSalesOpportunityInLatestVisitor(model);

            var result = await this.GetOpportunityAsync(model.ID);

            return result;
        }

        private async Task UpdateSalesOpportunityInLatestVisitor(Opportunity opportunity)
        {
            var latestVisitor = await (from v in DB.Visitors
                                       where v.ContactID == opportunity.ContactID
                                       orderby v.Created descending
                                       select v).FirstOrDefaultAsync();
            if (latestVisitor != null)
            {
                latestVisitor.SalesOpportunityMasterCenterID = opportunity.SalesOpportunityMasterCenterID;
                DB.Update(latestVisitor);
                await DB.SaveChangesAsync();
            }
        }

        public async Task DeleteOpportunityAsync(Guid id)
        {
            var model = await DB.Opportunities.Where(o => o.ID == id).FirstAsync();
            var activities = await DB.OpportunityActivities.Where(o => o.OpportunityID == id).ToListAsync();
            if (activities.Count > 0)
            {
                foreach (var item in activities)
                {
                    item.IsDeleted = true;
                }

                DB.UpdateRange(activities);
                await DB.SaveChangesAsync();
            }

            var revisits = await DB.RevisitActivities.Where(o => o.OpportunityID == id).ToListAsync();
            if (revisits.Count > 0)
            {
                foreach (var item in revisits)
                {
                    item.IsDeleted = true;
                }

                DB.UpdateRange(revisits);
                await DB.SaveChangesAsync();
            }

            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<List<OpportunityActivityListDTO>> GetOpportunityActivityListAsync(Guid opportunityID)
        {
            var model = await DB.OpportunityActivities
                .Include(o => o.OpportunityActivityType)
                .Include(o => o.UpdatedBy)
                .Where(o => o.OpportunityID == opportunityID)
                .OrderByDescending(o => o.OpportunityActivityType.Order)
                .ToListAsync();
            var result = model.Select(o => OpportunityActivityListDTO.CreateFromModel(o)).ToList();

            return result;
        }

        public async Task<OpportunityActivityDTO> GetOpportunityActivityAsync(Guid id)
        {
            var model = await DB.OpportunityActivities
                .Include(o => o.OpportunityActivityType)
                .Include(o => o.ConvenientTime)
                .Include(o => o.UpdatedBy)
                .Include(o => o.CreatedBy)
                .Where(o => o.ID == id)
                .FirstOrDefaultAsync();

            if (model == null)
                return null;

            var result = await OpportunityActivityDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<OpportunityActivityDTO> CreateOpportunityActivityAsync(Guid opportunityID, OpportunityActivityDTO input)
        {
            await input.ValidateAsync(DB);

            #region Validate
            ValidateException ex = new ValidateException();
            if (input.ActivityType != null)
            {
                var activityTypeKey = await DB.MasterCenters.Where(o => o.ID == input.ActivityType.Id && o.MasterCenterGroupKey == "OpportunityActivityType").Select(o => o.Key).FirstAsync();
                if (activityTypeKey == "1")
                {
                    var isPlanActivity = await DB.OpportunityActivities.Include(o => o.OpportunityActivityType).Where(o => o.OpportunityID == input.OpportunityID && o.OpportunityActivityType.Key == "1").AnyAsync();
                    if (isPlanActivity)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                        string desc = typeof(OpportunityActivityDTO).GetProperty(nameof(OpportunityActivityDTO.ActivityType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            var model = new OpportunityActivity();
            input.ToModel(ref model);
            await DB.OpportunityActivities.AddAsync(model);
            await DB.SaveChangesAsync();

            var trackModelList = new List<OpportunityActivityResult>();
            foreach (var track in input.ActivityStatuses)
            {
                if (track.IsSelected)
                {
                    var trackModel = new OpportunityActivityResult
                    {
                        OpportunityAcitivityID = model.ID,
                        StatusID = track.Id,
                        OtherReasons = track.OtherReason
                    };
                    trackModelList.Add(trackModel);
                }
            }
            if (trackModelList.Count > 0)
            {
                await DB.OpportunityActivityResults.AddRangeAsync(trackModelList);
            }

            await DB.SaveChangesAsync();

            var result = await this.GetOpportunityActivityAsync(model.ID);
            return result;
        }

        public async Task<OpportunityActivityDTO> UpdateOpportunityActivityAsync(Guid id, OpportunityActivityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.OpportunityActivities.Where(c => c.ID == id).FirstAsync();

            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var trackModel = await DB.OpportunityActivityResults.Where(e => e.OpportunityAcitivityID == id).ToListAsync();
            List<OpportunityActivityResult> trackAddModel = new List<OpportunityActivityResult>();
            List<OpportunityActivityResult> trackUpdateModel = new List<OpportunityActivityResult>();
            List<OpportunityActivityResult> trackDeleteModel = new List<OpportunityActivityResult>();

            var activityStatuses = input.ActivityStatuses.Where(o => o.IsSelected == true).ToList();
            foreach (var track in activityStatuses)
            {
                if (trackModel.Any(e => e.StatusID == track.Id))
                {
                    var tracklItemModel = await DB.OpportunityActivityResults.Where(e => e.OpportunityAcitivityID == id && e.StatusID == track.Id).FirstAsync();
                    tracklItemModel.OtherReasons = track.OtherReason;
                    trackUpdateModel.Add(tracklItemModel);
                }
                else
                {
                    trackAddModel.Add(new OpportunityActivityResult
                    {
                        OpportunityAcitivityID = id,
                        StatusID = track.Id,
                        OtherReasons = track.OtherReason
                    });
                }
            }

            foreach (var tracklItem in trackModel)
            {
                var existed = activityStatuses.Where(e => e.Id == tracklItem.StatusID).FirstOrDefault();
                if (existed == null)
                {
                    trackDeleteModel.Add(tracklItem);
                }
            }

            if (trackAddModel.Count() > 0)
            {
                await DB.OpportunityActivityResults.AddRangeAsync(trackAddModel);
                await DB.SaveChangesAsync();
            }
            if (trackUpdateModel.Count() > 0)
            {
                foreach (var trackItem in trackUpdateModel)
                {
                    DB.Entry(trackItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }
            if (trackDeleteModel.Count() > 0)
            {
                foreach (var trackItem in trackDeleteModel)
                {
                    trackItem.IsDeleted = true;
                    DB.Entry(trackItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }

            await DB.SaveChangesAsync();

            var result = await this.GetOpportunityActivityAsync(model.ID);
            return result;
        }

        public async Task DeleteOpportunityActivityAsync(Guid id)
        {
            var model = await DB.OpportunityActivities.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<OpportunityActivityDTO> GetOpportunityActivityDraftAsync(Guid opportunityID)
        {
            var model = await DB.OpportunityActivities
                .Include(o => o.OpportunityActivityType)
                .Include(o => o.ConvenientTime)
                .Where(o => o.OpportunityID == opportunityID && o.OpportunityActivityType.Order != 7)
                .OrderByDescending(o => o.OpportunityActivityType.Order)
                .FirstOrDefaultAsync();

            var result = await OpportunityActivityDTO.CreateDraftFromModelAsync(model, DB);
            result.OpportunityID = opportunityID;
            return result;
        }

        public async Task<List<RevisitActivityListDTO>> GetRevisitActivityListAsync(Guid opportunityID)
        {
            var model = await DB.RevisitActivities
                .Include(o => o.RevisitActivityType)
                .Where(o => o.OpportunityID == opportunityID)
                .OrderBy(o => o.Created)
                .ToListAsync();

            var result = model.Select(o => RevisitActivityListDTO.CreateFromModel(o, DB)).ToList();

            return result;
        }

        public async Task<RevisitActivityDTO> GetRevisitActivityAsync(Guid id)
        {
            var model = await DB.RevisitActivities
            .Include(o => o.RevisitActivityType)
            .Include(o => o.ConvenientTime)
            .Include(o => o.UpdatedBy)
            .Include(o => o.CreatedBy)
            .Where(o => o.ID == id)
            .FirstOrDefaultAsync();

            if (model == null)
                return null;

            var result = await RevisitActivityDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<RevisitActivityDTO> CreateRevisitActivityAsync(Guid opportunityID, RevisitActivityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = new RevisitActivity();
            input.ToModel(ref model);
            model.IsCompleted = true;

            await DB.RevisitActivities.AddAsync(model);
            await DB.SaveChangesAsync();

            var resultModelList = new List<RevisitActivityResult>();
            foreach (var activityResult in input.ActivityStatuses)
            {
                if (activityResult.IsSelected)
                {
                    var trackModel = new RevisitActivityResult
                    {
                        RevisitAcitivityID = model.ID,
                        StatusID = activityResult.Id,
                        OtherReasons = activityResult.OtherReason
                    };
                    resultModelList.Add(trackModel);
                }
            }
            if (resultModelList.Count > 0)
            {
                await DB.RevisitActivityResults.AddRangeAsync(resultModelList);
            }

            await DB.SaveChangesAsync();

            var result = await this.GetRevisitActivityAsync(model.ID);
            return result;
        }

        public async Task<RevisitActivityDTO> UpdateRevisitActivityAsync(Guid id, RevisitActivityDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.RevisitActivities.Where(c => c.ID == id).FirstAsync();
            if(model.IsCompleted == false)
            {
                model.IsCompleted = true;
            }
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var activityResults = await DB.RevisitActivityResults.Where(e => e.RevisitAcitivityID == id).ToListAsync();
            List<RevisitActivityResult> activityResultAddModel = new List<RevisitActivityResult>();
            List<RevisitActivityResult> activityResultUpdateModel = new List<RevisitActivityResult>();
            List<RevisitActivityResult> activityResultDeleteModel = new List<RevisitActivityResult>();

            var activityStatuses = input.ActivityStatuses.Where(o => o.IsSelected == true).ToList();
            foreach (var activityResult in activityStatuses)
            {
                if (activityResults.Any(e => e.StatusID == activityResult.Id))
                {
                    var resultItemModel = await DB.RevisitActivityResults.Where(e => e.RevisitAcitivityID == id && e.StatusID == activityResult.Id).FirstAsync();
                    resultItemModel.OtherReasons = activityResult.OtherReason;
                    activityResultUpdateModel.Add(resultItemModel);
                }
                else
                {
                    activityResultAddModel.Add(new RevisitActivityResult
                    {
                        RevisitAcitivityID = id,
                        StatusID = activityResult.Id,
                        OtherReasons = activityResult.OtherReason
                    });
                }
            }

            foreach (var resultItem in activityResults)
            {
                var existed = activityStatuses.FirstOrDefault(e => e.Id == resultItem.StatusID);
                if (existed == null)
                {
                    activityResultDeleteModel.Add(resultItem);
                }
            }

            if (activityResultAddModel.Any())
            {
                await DB.RevisitActivityResults.AddRangeAsync(activityResultAddModel);
                await DB.SaveChangesAsync();
            }
            if (activityResultUpdateModel.Any())
            {
                foreach (var trackItem in activityResultUpdateModel)
                {
                    DB.Entry(trackItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }
            if (activityResultDeleteModel.Any())
            {
                foreach (var trackItem in activityResultDeleteModel)
                {
                    trackItem.IsDeleted = true;
                    DB.Entry(trackItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }

            await DB.SaveChangesAsync();

            var result = await this.GetRevisitActivityAsync(model.ID);
            return result;
        }

        public async Task DeleteRevisitActivityAsync(Guid id)
        {
            var model = await DB.RevisitActivities.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<RevisitActivityDTO> GetRevisitActivityDraftAsync(Guid opportunityID)
        {
            var model = await DB.RevisitActivities
                .Include(o => o.RevisitActivityType)
                .Include(o => o.ConvenientTime)
                .Where(o => o.OpportunityID == opportunityID)
                .OrderByDescending(o => o.RevisitActivityType.Order)
                .FirstOrDefaultAsync();

            var result = await RevisitActivityDTO.CreateDraftFromModelAsync(model, DB);
            result.OpportunityID = opportunityID;
            return result;
        }

        public async Task<OpportunityAssignDTO> AssignOpportunityListAsync(OpportunityAssignDTO input)
        {
            var user = await DB.Users.Where(o => o.ID == input.User.Id).FirstAsync();
            List<Opportunity> opportunities = new List<Opportunity>();
            List<OpportunityAssign> opportunityAssigns = new List<OpportunityAssign>();
            foreach (var item in input.Opportunities)
            {
                var opportunity = await DB.Opportunities
                    .Include(o => o.Contact)
                    .Include(o => o.Project)
                    .Include(o => o.EstimateSalesOpportunity)
                    .Include(o => o.StatusQuestionaire)
                    .Include(o => o.SalesOpportunity)
                    .Include(o => o.Owner)
                    .Where(o => o.ID == item.Id).FirstAsync();
                opportunity.OwnerID = input.User.Id;

                OpportunityAssign opportunityAssign = new OpportunityAssign()
                {
                    OpportunityID = opportunity.ID,
                    OwnerID = input.User.Id
                };

                opportunities.Add(opportunity);
                opportunityAssigns.Add(opportunityAssign);
            }

            if (opportunities.Count > 0)
            {
                DB.Opportunities.UpdateRange(opportunities);
            }

            if (opportunityAssigns.Count > 0)
            {
                await DB.OpportunityAssigns.AddRangeAsync(opportunityAssigns);
            }

            await DB.SaveChangesAsync();
            var result = OpportunityAssignDTO.CreateFromModel(opportunities, user, DB);
            return result;
        }

        public async Task<List<OpportunityListDTO>> AssignOpportunityListRandomAsync(Guid projectID, List<OpportunityListDTO> inputs)
        {
            var lcRoleID = await DB.Roles.Where(o => o.Code == "LC").Select(o => o.ID).FirstAsync();
            var lcUsers = await DB.Users
                .Where(o => o.UserAuthorizeProjects.Where(m => m.ProjectID == projectID).Any() &&
                o.UserRoles.Where(n => n.RoleID == lcRoleID).Any())
                .ToListAsync();
            var latestOpAssign = await DB.OpportunityAssigns.Include(o => o.Opportunity).ThenInclude(o => o.Owner)
                .Where(o => o.Opportunity.ProjectID == projectID).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            lcUsers = lcUsers.OrderByDescending(o => o.EmployeeNo).ToList();
            User startLC = null;
            if (latestOpAssign != null)
            {
                startLC = lcUsers.Where(o => o.EmployeeNo == latestOpAssign.Opportunity?.Owner?.EmployeeNo).FirstOrDefault();
            }
            if (startLC == null)
            {
                var random = new Random();
                int index = random.Next(lcUsers.Count);
                startLC = lcUsers[index];
            }
            int startIndex = lcUsers.IndexOf(startLC);
            var opIDs = inputs.Select(o => o.Id).ToList();
            var ops = await DB.Opportunities
                .Include(o => o.Contact)
                    .Include(o => o.Project)
                    .Include(o => o.EstimateSalesOpportunity)
                    .Include(o => o.StatusQuestionaire)
                    .Include(o => o.SalesOpportunity)
                    .Include(o => o.Owner)
                .Where(o => opIDs.Contains(o.ID)).ToListAsync();
            for (int i = 0; i < ops.Count; i++)
            {
                var op = ops[i];
                op.OwnerID = lcUsers[startIndex++].ID;

                OpportunityAssign opAssign = new OpportunityAssign()
                {
                    OpportunityID = op.ID,
                    OwnerID = op.OwnerID
                };
                await DB.AddAsync(opAssign);
                DB.Update(op);

                if (startIndex >= lcUsers.Count)
                {
                    startIndex = 0;
                }
            }

            await DB.SaveChangesAsync();

            var results = ops.Select(o => OpportunityListDTO.CreateFromModel(o, DB)).ToList();
            return results;
        }

    }
}
