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
    public interface IBankBranchService
    {
        Task<List<BankBranchDropdownDTO>> GetBankBrachDropdownListAsync(Guid bankID, string name, Guid? provinceID = null);
        Task<BankBranchPaging> GetBankBranchListAsync(BankBranchFilter filter, PageParam pageParam, BankBranchSortByParam sortByParam);
        Task<BankBranchDTO> GetBankBranchAsync(Guid id);
        Task<BankBranchDTO> CreateBankBranchAsync(BankBranchDTO input);
        Task<BankBranchDTO> UpdateBankBranchAsync(Guid id, BankBranchDTO input);
        Task<BankBranch> DeleteBankBranchAsync(Guid id);
    }
}
