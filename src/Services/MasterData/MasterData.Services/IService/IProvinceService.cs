using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using MasterData.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MasterData.Services
{
    public interface IProvinceService
    {
        Task<ProvinceListDTO> FindProvinceAsync(string name);
        Task<List<ProvinceListDTO>> GetProvinceDropdownListAsync(string name);
        Task<ProvincePaging> GetProvinceListAsync(ProvinceFilter filter, PageParam pageParam, ProvinceSortByParam sortByParam);
        Task<ProvinceDTO> GetProvinceAsync(Guid id);
        Task<ProvinceDTO> CreateProvinceAsync(ProvinceDTO input);
        Task<ProvinceDTO> UpdateProvinceAsync(Guid id, ProvinceDTO input);
        Task<ProvinceDTO> GetProvincePostalCodeAsync(string postalCode);
        Task<Province> DeleteProvinceAsync(Guid id);
    }
}
