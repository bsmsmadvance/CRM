using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;

namespace MasterData.Services
{
    public interface IBrandService
    {
        Task<List<BrandDropdownDTO>> GetBrandDropdownListAsync(string name);
        Task<BrandPaging> GetBrandListAsync(BrandFilter filter, PageParam pageParam, BrandSortByParam sortByParam);
        Task<BrandDTO> GetBrandAsync(Guid id);
        Task<BrandDTO> CreateBrandAsync(BrandDTO input);
        Task<BrandDTO> UpdateBrandAsync(Guid id, BrandDTO input);
        Task<Brand> DeleteBrandAsync(Guid id);
    }
}
