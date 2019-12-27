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
    public class AgentEmployeeService : IAgentEmployeeService
    {
        private readonly DatabaseContext DB;

        public AgentEmployeeService(DatabaseContext db)
        {
            this.DB = db;
        }
        public async Task<List<AgentEmployeeDropdownDTO>> GetAgentEmployeeDropdownListAsync(string name, Guid? agentID = null)
        {
            IQueryable<AgentEmployee> query = DB.AgentEmployees;

            #region Filter
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }
            if (agentID != null)
            {
                query = query.Where(o => o.AgentID == agentID);
            }
            #endregion

            var queryResults = await query.OrderBy(o => o.FirstName).ThenBy(o => o.LastName).Take(100).ToListAsync();

            var results = queryResults.Select(o => AgentEmployeeDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<AgentEmployeePaging> GetAgentEmployeeListAsync(AgentEmployeeFilter filter, PageParam pageParam, AgentEmployeeSortByParam sortByParam)
        {
            IQueryable<AgentEmployeeQueryResult> query = DB.AgentEmployees
                                                         .Select(o => new AgentEmployeeQueryResult
                                                         {
                                                             AgentEmployee = o,
                                                             UpdatedBy = o.UpdatedBy
                                                         });

            #region filter
            if (filter.AgentID != null)
            {
                query = query.Where(o => o.AgentEmployee.AgentID == filter.AgentID);
            }
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(x => x.AgentEmployee.FirstName.Contains(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(x => x.AgentEmployee.LastName.Contains(filter.LastName));
            }
            if (!string.IsNullOrEmpty(filter.TelNo))
            {
                query = query.Where(x => x.AgentEmployee.TelNo.Contains(filter.TelNo));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.AgentEmployee.Updated >= filter.UpdatedFrom
                                    && x.AgentEmployee.Updated <= filter.UpdatedTo);
            }
            #endregion

            AgentEmployeeDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<AgentEmployeeQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => AgentEmployeeDTO.CreateFromQueryResult(o)).ToList();

            return new AgentEmployeePaging()
            {
                AgentEmployees = results,
                PageOutput = pageOutput
            };
        }

        public async Task<AgentEmployeeDTO> GetAgentEmployeeAsync(Guid id)
        {
            try
            {
                var model = await DB.AgentEmployees.Include(o => o.UpdatedBy)
                    .Where(x => x.ID == id).FirstAsync();
                var result = AgentEmployeeDTO.CreateFromModel(model);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AgentEmployeeDTO> CreateAgentEmployeeAsync(AgentEmployeeDTO input)
        {
            await input.ValidateAsync(DB);
            AgentEmployee model = new AgentEmployee();
            input.ToModel(ref model);

            await DB.AgentEmployees.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetAgentEmployeeAsync(model.ID);
            return result;
        }

        public async Task<AgentEmployeeDTO> UpdateAgentEmployeeAsync(Guid id, AgentEmployeeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.AgentEmployees.Where(x => x.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetAgentEmployeeAsync(model.ID);
            return result;
        }

        public async Task<AgentEmployee> DeleteAgentEmployeeAsync(Guid id)
        {
            var model = await DB.AgentEmployees.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
