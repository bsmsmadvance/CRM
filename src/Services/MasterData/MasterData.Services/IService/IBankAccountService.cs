using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using PagingExtensions;

namespace MasterData.Services
{
    public interface IBankAccountService
    {
        Task<List<BankAccountDropdownDTO>> GetBankAccountDropdownListAsync(string displayName, string bankAccountTypeKey, Guid? companyID);
        Task<BankAccountPaging> GetBankAccountListAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam);
        Task<BankAccountDTO> GetBankAccountDetailAsync(Guid id);
        Task<BankAccountDTO> CreateBankAccountAsync(BankAccountDTO input);
        Task<BankAccountDTO> CreateChartOfAccountAsync(BankAccountDTO input);
        Task<BankAccountDTO> UpdateBankAccountAsync(Guid id, BankAccountDTO input);
        Task<BankAccountDTO> UpdateChartOfAccountAsync(Guid id, BankAccountDTO input);
        Task DeleteBankAccountAsync(Guid id);
        Task DeleteBankAccountListAsync(List<BankAccountDTO> inputs);
    }
}
