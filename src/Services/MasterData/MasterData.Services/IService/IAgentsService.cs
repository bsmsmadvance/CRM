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
    public interface IAgentsService
    {
        Task<List<AgentDropdownDTO>> GetAgentDropdownListAsync(string name);
        Task<AgentPaging> GetAgentListAsync(AgentFilter filter, PageParam pageParam, AgentSortByParam sortByParam);
        Task<AgentDTO> GetAgentAsync(Guid id);
        Task<AgentDTO> CreateAgentAsync(AgentDTO input);
        Task<AgentDTO> UpdateAgentAsync(Guid id, AgentDTO input);
        Task<Agent> DeleteAgentAsync(Guid id);
    }
}
