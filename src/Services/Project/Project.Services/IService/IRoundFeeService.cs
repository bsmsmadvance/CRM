using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IRoundFeeService
    {
        Task<RoundFeePaging> GetRoundFeeListAsync(Guid projectID, RoundFeeFilter filter, PageParam pageParam, RoundFeeSortByParam sortByParam);
        Task<RoundFeeDTO> GetRoundFeeAsync(Guid projectID, Guid id);
        Task<RoundFeeDTO> CreateRoundFeeAsync(Guid projectID, RoundFeeDTO input);
        Task<RoundFeeDTO> UpdateRoundFeeAsync(Guid projectID, Guid id, RoundFeeDTO input);
        Task<RoundFee> DeleteRoundFeeAsync(Guid projectID, Guid id);
    }
}
