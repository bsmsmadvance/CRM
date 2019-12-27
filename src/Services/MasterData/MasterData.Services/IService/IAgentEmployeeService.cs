using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public interface IAgentEmployeeService
    {
        Task<List<AgentEmployeeDropdownDTO>> GetAgentEmployeeDropdownListAsync(string name, Guid? agentID = null);
        Task<AgentEmployeePaging> GetAgentEmployeeListAsync(AgentEmployeeFilter filter, PageParam pageParam, AgentEmployeeSortByParam sortByParam);
        Task<AgentEmployeeDTO> GetAgentEmployeeAsync(Guid id);
        Task<AgentEmployeeDTO> CreateAgentEmployeeAsync(AgentEmployeeDTO input);
        Task<AgentEmployeeDTO> UpdateAgentEmployeeAsync(Guid id, AgentEmployeeDTO input);
        Task<AgentEmployee> DeleteAgentEmployeeAsync(Guid id);
    }
}
