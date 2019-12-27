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
    public interface ILowRiseBuildingPriceFeeService
    {
        Task<LowRiseBuildingPriceFeePaging> GetLowRiseBuildingPriceFeeListAsync(Guid projectID, LowRiseBuildingPriceFeeFilter filter, PageParam pageParam, LowRiseBuildingPriceFeeSortByParam sortByParam);
        Task<LowRiseBuildingPriceFeeDTO> GetLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id);
        Task<LowRiseBuildingPriceFeeDTO> CreateLowRiseBuildingPriceFeeAsync(Guid projectID, LowRiseBuildingPriceFeeDTO input);
        Task<LowRiseBuildingPriceFeeDTO> UpdateLowRiseBuildingPriceFeesync(Guid projectID, Guid id, LowRiseBuildingPriceFeeDTO input);
        Task<LowRiseBuildingPriceFee> DeleteLowRiseBuildingPriceFeeAsync(Guid projectID, Guid id);
    }
}
