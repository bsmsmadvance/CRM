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
    public interface IDistrictService
    {
        Task<DistrictListDTO> FindDistrictAsync(Guid provinceID, string name);
        Task<List<DistrictListDTO>> GetDistrictDropdownListAsync(string name, Guid? provinceID);
        Task<DistrictPaging> GetDistrictListAsync(DistrictFilter filter, PageParam pageParam, DistrictSortByParam sortByParam);
        Task<DistrictDTO> GetDistrictAsync(Guid id);
        Task<DistrictDTO> CreateDistrictAsync(DistrictDTO input);
        Task<DistrictDTO> UpdateDistrictAsync(Guid id, DistrictDTO input);
        Task<District> DeleteDistrictAsync(Guid id);
    }
}
