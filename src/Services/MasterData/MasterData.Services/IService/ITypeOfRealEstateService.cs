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
    public interface ITypeOfRealEstateService
    {
        Task<List<TypeOfRealEstateDropdownDTO>> GetTypeOfRealEstateDropdownListAsync(string name);
        Task<TypeOfRealEstatePaging> GetTypeOfRealEstateListAsync(TypeOfRealEstateFilter filter, PageParam pageParam, TypeOfRealEstateSortByParam sortByParam);
        Task<TypeOfRealEstateDTO> GetTypeOfRealEstateAsync(Guid id);
        Task<TypeOfRealEstateDTO> CreateTypeOfRealEstateAsync(TypeOfRealEstateDTO input);
        Task<TypeOfRealEstateDTO> UpdateTypeOfRealEstateAsync(Guid id, TypeOfRealEstateDTO input);
        Task<TypeOfRealEstate> DeleteTypeOfRealEstateAsync(Guid id);
    }
}
