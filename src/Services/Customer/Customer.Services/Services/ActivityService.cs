using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using Customer.Services.ContactServices;
using Database.Models;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Database.Models.MasterKeys;

namespace Customer.Services.ActivityService
{
    public class ActivityService : IActivityService
    {
        private readonly DatabaseContext DB;

        public ActivityService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ActivityPaging> GetActivityListAsync(ActivityFilter filter, PageParam pageParam, ActivityListSortByParam sortByParam)
        {
            IQueryable<ActivityQueryResult> query = DB.ActivityTasks.Include(o => o.LeadActivity.Lead.LeadType).Select(o => new ActivityQueryResult
            {
                ActivityTask = o,
                OverdueStatus = o.OverdueStatus,
                ActivityTaskStatus = o.Status,
                ActivityTaskTopic = o.Topic,
                ActivityTaskType = o.Type,
                Project = o.Project,
                Owner = o.Owner,
                LeadActivity = o.LeadActivity,
                OpportunityActivity = o.OpportunityActivity,
                RevisitActivity = o.RevisitActivity,
                LeadType = o.LeadActivity.Lead.LeadType
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.ActivityTaskTopicKey))
            {
                var topicMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.ActivityTaskTopicKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.ActivityTask.ActivityTaskTopicMasterCenterID == topicMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.ActivityTaskTopicKeys))
            {
                var keys = filter.ActivityTaskTopicKeys.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.ActivityTask.ActivityTaskTopicMasterCenterID ?? Guid.Empty));
            }

            if (!string.IsNullOrEmpty(filter.LeadTypeKey))
            {
                var LeadTypeMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.LeadTypeKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.LeadActivity.Lead.LeadTypeMasterCenterID == LeadTypeMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.ActivityTaskTypeKey))
            {
                var typeMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.ActivityTaskTypeKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.ActivityTask.ActivityTaskTypeMasterCenterID == typeMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.ActivityTaskTypeKeys))
            {
                var keys = filter.ActivityTaskTypeKeys.Split(',').ToList();
                var typeMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => typeMasterCenterIDs.Contains(q.ActivityTask.ActivityTaskTypeMasterCenterID ?? Guid.Empty));
            }

            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(q => string.Format("{0}{1}", q.ActivityTask.ContactFirstName, q.ActivityTask.ContactLastName).Contains(filter.FullName.Trim()));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(q => q.ActivityTask.PhoneNumber.Contains(filter.PhoneNumber));

            if (filter.DueDateFrom != null && filter.DueDateTo != null)
                query = query.Where(q => q.ActivityTask.DueDate >= filter.DueDateFrom && q.ActivityTask.DueDate <= filter.DueDateTo);

            if (filter.OwnerID != null)
            {
                if (filter.OwnerID == Guid.Empty)
                {
                    query = query.Where(q => q.Owner.ID == null);
                }
                else
                {
                    query = query.Where(q => q.Owner.ID == filter.OwnerID);
                }
            } 

            if (!string.IsNullOrEmpty(filter.OverdueStatusKey))
            {
                var overdueMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.OverdueStatusKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.ActivityTask.ActivityTaskOverdueStatusMasterCenterID == overdueMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.ActivityTaskStatusKey))
            {
                var statusMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.ActivityTaskStatusKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus)
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.ActivityTask.ActivityTaskStatusMasterCenterID == statusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIDs = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(q => projectIDs.Contains(q.ActivityTask.ProjectID.Value));
            }
            #endregion

            ActivityListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<ActivityQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => ActivityListDTO.CreateFromQueryResult(o, DB)).ToList();

            return new ActivityPaging()
            {
                PageOutput = pageOutput,
                Activities = result
            };
        }

        /// <summary>
        /// Update สถานะและเลข Overdue ให้กับ Activity Task ที่ยังไม่ Completed
        /// </summary>
        /// <returns></returns>
        public async Task<UpdateOverdueResponse> UpdateActivityTaskOverdueAsync()
        {
            var inCompleteId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "3").Select(o => o.ID).FirstAsync();
            var tasks = await DB.ActivityTasks.Where(o => o.ActivityTaskStatusMasterCenterID == inCompleteId).ToListAsync();

            int overdue = 0;
            int onplan = 0;
            int today = 0;
            foreach(var task in tasks)
            {
                int overdueDay = Convert.ToInt32((task.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                Guid? overdueStatusId = null;
                if (overdueDay == 0)
                {
                    overdueStatusId = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                    today = today + 1;
                }
                else if (overdueDay < 0)
                {
                    overdueStatusId = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                    overdue = overdue + 1;
                }
                else if (overdueDay > 0)
                {
                    overdueStatusId = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                    onplan = onplan + 1;
                }

                task.OverdueDays = overdueDay;
                task.ActivityTaskOverdueStatusMasterCenterID = overdueStatusId;

                DB.ActivityTasks.Update(task);
            }

            await DB.SaveChangesAsync();

            var response = new UpdateOverdueResponse()
            {
                ActivityTaskUpdated = tasks.Count,
                Overdue = overdue,
                OnPlan = onplan,
                Today = today
            };

            return response;
        }
    }
}
