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
    public interface ISubDistrictService
    {
        Task<SubDistrictListDTO> FindSubDistrictAsync(Guid districtID, string name);
        Task<List<SubDistrictListDTO>> GetSubDistrictDropdownListAsync(string name, Guid? districtID);
        Task<SubDistrictPaging> GetSubDistrictListAsync(SubDistrictFilter filter, PageParam pageParam, SubDistrictSortByParam sortByParam);
        Task<SubDistrictDTO> GetSubDistrictAsync(Guid id);
        Task<SubDistrictDTO> CreateSubDistrictAsync(SubDistrictDTO input);
        Task<SubDistrictDTO> UpdateSubDistrictAsync(Guid id, SubDistrictDTO input);
        Task<SubDistrict> DeleteSubDistrictAsync(Guid id);
    }
}
