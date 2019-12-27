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
using Database.Models.DbQueries;

namespace MasterData.Services
{
    public interface IMasterCenterService
    {
        Task<List<MasterCenterDropdownDTO>> GetMasterCenterDropdownListAsync(string masterCenterGroupKey, string name);
        Task<MasterCenterPaging> GetMasterCenterListAsync(MasterCenterFilter filter, PageParam pageParam, MasterCenterSortByParam sortByParam);
        Task<MasterCenterDropdownDTO> GetFindMasterCenterDropdownItemAsync(string masterCenterGroupKey, string key);
        Task<MasterCenterDTO> GetMasterCenterAsync(Guid id);
        Task<MasterCenterDTO> CreateMasterCenterAsync(MasterCenterDTO input);
        Task<MasterCenterDTO> UpdateMasterCenterAsync(Guid id, MasterCenterDTO input);
        Task<MasterCenter> DeleteMasterCenterAsync(Guid id);

        Task<List<MasterCenterResult>> GetMasterCenterUsingDbQueryAsync(string masterCenterGroupKey);
    }
}
