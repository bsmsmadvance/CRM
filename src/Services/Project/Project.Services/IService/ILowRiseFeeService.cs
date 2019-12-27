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
    public interface ILowRiseFeeService
    {
        Task<LowRiseFeePaging> GetLowRiseFeeListAsync(Guid projectID, LowRiseFeeFilter filter, PageParam pageParam, LowRiseFeeSortByParam sortByParam);
        Task<LowRiseFeeDTO> GetLowRiseFeeAsync(Guid projectID, Guid id);
        Task<LowRiseFeeDTO> CreateLowRiseFeeAsync(Guid projectID, LowRiseFeeDTO input);
        Task<LowRiseFeeDTO> UpdateLowRiseFeeAsync(Guid projectID, Guid id, LowRiseFeeDTO input);
        Task<LowRiseFee> DeleteLowRiseFeeAsync(Guid projectID, Guid id);
    }
}
