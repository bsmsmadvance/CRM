using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterData.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MasterData.Services
{
    public interface IMasterCenterGroupService
    {
        Task<MasterCenterGroupPaging> GetMasterCenterGroupListAsync(MasterCenterGroupFilter filter, PageParam pageParam, MasterCenterGroupSortByParam sortByParam);
        Task<MasterCenterGroupDTO> GetMasterCenterGroupAsync(string id);
        Task<MasterCenterGroupDTO> CreateMasterCenterGroupAsync(MasterCenterGroupDTO input);
        Task<MasterCenterGroupDTO> UpdateMasterCenterGroupAsync(string key, MasterCenterGroupDTO input);
        Task<MasterCenterGroup> DeleteMasterCenterGroupAsync(string id);
    }
}
