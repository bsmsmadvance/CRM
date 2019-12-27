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
    public interface IBankService
    {
        Task<List<BankDropdownDTO>> GetBankDropdownListAsync(string name);
        Task<BankPaging> GetBankListAsync(BankFilter filter, PageParam pageParam, BankSortByParam sortByParam);
        Task<BankDTO> GetBankAsync(Guid id);
        Task<BankDTO> CreateBankAsync(BankDTO input);
        Task<BankDTO> UpdateBankAsync(Guid id, BankDTO input);
        Task<Bank> DeleteBankAsync(Guid id);
    }
}
