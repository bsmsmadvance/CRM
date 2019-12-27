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
    public interface ICompanyService
    {
        Task<List<CompanyDropdownDTO>> GetCompanyDropdownListAsync(CompanyDropdownFilter filter);
        Task<CompanyPaging> GetCompanyListAsync(CompanyFilter filter, PageParam pageParam, CompanySortByParam sortByParam);
        Task<CompanyDTO> GetCompanyAsync(Guid id);
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO input);
        Task<CompanyDTO> UpdateCompanyAsync(Guid id, CompanyDTO input);
        Task<Company> DeleteCompanyAsync(Guid id);
    }
}
