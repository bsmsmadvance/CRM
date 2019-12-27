using Database.Models;
using Database.Models.CMS;
using Database.Models.PRJ;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;
using Commission.Params.Inputs;

namespace Commission.Services
{
    public class RateSettingAgentService : IRateSettingAgentService
    {
        private readonly DatabaseContext DB;

        public RateSettingAgentService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RateSettingAgentPaging> GetRateSettingAgentListAsync(RateSettingAgentFilter filter, PageParam pageParam, RateSettingAgentSortByParam sortByParam)
        {
            IQueryable<RateSettingAgentQueryResult> query = DB.RateSettingAgents
                                                  .Select(o => new RateSettingAgentQueryResult()
                                                  {
                                                      RateSettingAgent = o,
                                                      Project = o.Project,
                                                      Agent = o.Agent
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingAgent.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingAgent.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingAgent.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingAgent.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingAgent.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingAgent.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingAgent.Created >= filter.CreateDateFrom && x.RateSettingAgent.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingAgent.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingAgentDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingAgentQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingAgentDTO.CreateFromQueryResult(o)).ToList();

            return new RateSettingAgentPaging()
            {
                PageOutput = pageOutput,
                RateSettingAgents = results
            };
        }

        public async Task<List<RateSettingAgentDTO>> GetRateSettingAgentProjectListForNewAsync()
        {
            IQueryable<RateSettingAgentQueryResult> query = from a in DB.Agents
                                                                //join rm in DB.RateSettingAgents on a.ID equals rm.AgentID into g
                                                                //from o in g.DefaultIfEmpty()
                                                            where a.IsDeleted == false
                                                            select new RateSettingAgentQueryResult()
                                                            {
                                                                RateSettingAgent = new RateSettingAgent(),
                                                                Project = new Project(),
                                                                Agent = a
                                                            };

            var results = await query.Select(o => RateSettingAgentDTO.CreateFromQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task<List<RateSettingAgentDTO>> GetRateSettingAgentProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate)
        {
            IQueryable<RateSettingAgentQueryResult> query = from a in DB.Agents
                                                            join rm in DB.RateSettingAgents on a.ID equals rm.AgentID into g
                                                            from o in g.DefaultIfEmpty()
                                                                //where o.IsActive
                                                            select new RateSettingAgentQueryResult()
                                                            {
                                                                RateSettingAgent = o ?? new RateSettingAgent(),
                                                                Project = o.Project ?? new Project(),
                                                                Agent = a
                                                            };

            #region Filter
            if (ProjectID != null)
            {
                query = query.Where(x => x.RateSettingAgent.ProjectID == ProjectID);
            }
            if (ActiveDate != null)
            {
                query = query.Where(x => x.RateSettingAgent.ActiveDate == ActiveDate);
            }
            #endregion

            var results = await query.Select(o => RateSettingAgentDTO.CreateFromQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task CreateRateSettingAgentListAsync(RateSettingAgentInput inputModel)
        {
            var lstProject = inputModel.ListProject;
            var ListInput = inputModel.ListRateSettingAgent;

            if (lstProject.Count > 0 && ListInput.Count() > 0)
            {

                var lstRateSettingAgent = new List<RateSettingAgent>();
                var lstUpdateRateSettingAgent = new List<RateSettingAgent>();
                foreach (var pr in lstProject)
                {
                    foreach (var input in ListInput)
                    {
                        var model = new RateSettingAgent();
                        model.ActiveDate = input.ActiveDate;
                        model.AgentID = input.Agent.Id;
                        model.ProjectID = pr.ProjectID;
                        model.Amount = input.Amount;
                        model.IsActive = true;
                        lstRateSettingAgent.Add(model);


                        var lstUpdate = await DB.RateSettingAgents.Where(o => o.ProjectID == pr.ProjectID
                                                                            && o.AgentID == input.Agent.Id
                                                                            && o.ActiveDate <= input.ActiveDate
                                                                            && o.IsActive == true).ToListAsync();
                        foreach (var update in lstUpdate)
                        {
                            update.IsActive = false;

                            lstUpdateRateSettingAgent.Add(update);
                        }
                    }
                }

                DB.RateSettingAgents.UpdateRange(lstUpdateRateSettingAgent);
                await DB.RateSettingAgents.AddRangeAsync(lstRateSettingAgent);
                await DB.SaveChangesAsync();
            }
        }

        public async Task UpdateRateSettingAgentListAsync(List<RateSettingAgentDTO> ListInput)
        {
            if (ListInput.Count() > 0)
            {

                foreach (var input in ListInput)
                {
                    await input.ValidateAsync(DB);

                    var model = await DB.RateSettingAgents.Where(o => o.ID == input.Id).FirstAsync();
                    input.ToModel(ref model);

                    DB.Entry(model).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
        }

        /*
        public async Task<RateSettingAgentDTO> GetRateSettingAgentAsync(Guid id)
        {
            var model = await DB.RateSettingAgents.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingAgentDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingAgentDTO> CreateRateSettingAgentAsync(RateSettingAgentDTO input)
        {
            await input.ValidateAsync(DB);

            RateSettingAgent model = new RateSettingAgent();
            input.ToModel(ref model);

            await DB.RateSettingAgents.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = RateSettingAgentDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingAgentDTO> UpdateRateSettingAgentAsync(Guid id, RateSettingAgentDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.RateSettingAgents.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingAgentDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingAgent> DeleteRateSettingAgentAsync(Guid id)
        {
            var model = await DB.RateSettingAgents.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
        */
    }
}
