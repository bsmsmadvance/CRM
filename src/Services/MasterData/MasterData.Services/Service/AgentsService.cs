using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public class AgentsService : IAgentsService
    {
        private readonly DatabaseContext DB;

        public AgentsService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<List<AgentDropdownDTO>> GetAgentDropdownListAsync(string name)
        {
            IQueryable<Agent> query = DB.Agents;

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            #endregion
            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => AgentDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<AgentPaging> GetAgentListAsync(AgentFilter request, PageParam pageParam, AgentSortByParam sortByParam)
        {
            IQueryable<AgentQueryResult> query = DB.Agents.Select(o => new AgentQueryResult
                                                         {
                                                             Agent = o,
                                                             SubDistrict = o.SubDistrict,
                                                             District = o.District,
                                                             Province = o.Province,
                                                             UpdatedBy = o.UpdatedBy
                                                         });

            #region filter
            if (!string.IsNullOrEmpty(request.NameTH))
            {
                query = query.Where(x => x.Agent.NameTH.Contains(request.NameTH));
            }
            if (!string.IsNullOrEmpty(request.NameEN))
            {
                query = query.Where(x => x.Agent.NameEN.Contains(request.NameEN));
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.Agent.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.Agent.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.Agent.Updated >= request.UpdatedFrom && x.Agent.Updated <= request.UpdatedTo);
            }
            #endregion

            AgentDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<AgentQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => AgentDTO.CreateFromQueryResult(o)).ToList();

            return new AgentPaging()
            {
                Agents = results,
                PageOutput = pageOutput
            };
        }

        public async Task<AgentDTO> GetAgentAsync(Guid id)
        {
            var model = await DB.Agents.Where(o => o.ID == id)
                                       .Include(o => o.Province)
                                       .Include(o => o.District)
                                       .Include(o => o.SubDistrict)
                                       .Include(o => o.UpdatedBy)
                                       .FirstAsync();
            var result = AgentDTO.CreateFromModel(model);
            return result;
        }

        public async Task<AgentDTO> CreateAgentAsync(AgentDTO input)
        {
            await input.ValidateAsync(DB);

            Agent model = new Agent();
            input.ToModel(ref model);

            await DB.Agents.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetAgentAsync(model.ID);
            return result;
        }

        public async Task<AgentDTO> UpdateAgentAsync(Guid id, AgentDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Agents.Where(x => x.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetAgentAsync(model.ID);
            return result;
        }

        public async Task<Agent> DeleteAgentAsync(Guid id)
        {
            var model = await DB.Agents.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
