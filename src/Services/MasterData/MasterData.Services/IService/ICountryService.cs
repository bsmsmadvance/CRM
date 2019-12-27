using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using PagingExtensions;

namespace MasterData.Services
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetCountryDropdownListAsync(CountryFilter filter);
        Task<CountryPaging> GetCountryListAsync(CountryFilter filter, PageParam pageParam, CountrySortByParam sortByParam);
        Task<CountryDTO> GetCountryAsync(Guid id);
        Task<CountryDTO> FindCountryAsync(string code);
        Task<CountryDTO> CreateCountryAsync(CountryDTO input);
        Task<CountryDTO> UpdateCountryAsync(Guid id, CountryDTO input);
        Task<Country> DeleteCountryAsync(Guid id);
    }
}
