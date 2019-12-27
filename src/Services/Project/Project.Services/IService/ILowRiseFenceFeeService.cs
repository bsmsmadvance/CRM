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
    public interface ILowRiseFenceFeeService
    {
        Task<LowRiseFenceFeePaging> GetLowRiseFenceFeeListAsync(Guid projectID, LowRiseFenceFeeFilter filter, PageParam pageParam, LowRiseFenceFeeSortByParam sortByParam);
        Task<LowRiseFenceFeeDTO> GetLowRiseFenceFeeAsync(Guid projectID, Guid id);
        Task<LowRiseFenceFeeDTO> CreateLowRiseFenceFeeAsync(Guid projectID, LowRiseFenceFeeDTO input);
        Task<LowRiseFenceFeeDTO> UpdateLowRiseFenceFeeAsync(Guid projectID, Guid id, LowRiseFenceFeeDTO input);
        Task<LowRiseFenceFee> DeleteLowRiseFenceFeeAsync(Guid projectID, Guid id);
    }
}
