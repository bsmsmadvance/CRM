using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models.SAL;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;

namespace Sale.Services
{
    /// <summary>
    /// การขออนุมัติ Price List
    /// </summary>
    public interface IPriceListWorkflowService
    {
        /// <summary>
        /// สร้างการขออนุมัติ Price List
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<PriceListWorkflow> CreatePriceListWorkflowAsync(Guid quotationID, Guid bookingID, Guid priceListWorkflowStageMasterCenterID);

        /// <summary>
        /// ดึงข้อมูลรายการการของอนุมัติ PriceList (Paging, Sorting, Filter)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447141/preview
        /// </summary>
        /// <returns></returns>
        Task<PriceListWorkflowPaging> GetPriceListWorkflowListAsync(PriceListWorkflowFilter filter, PageParam pageParam, PriceListWorkflowSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลขออนุมัติ PriceList
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447142/preview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PriceListWorkflowDTO> GetPriceListWorkflowAsync(Guid id);

        /// <summary>
        /// อนุมัติ PriceList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PriceListWorkflowDTO> ApproveAsync(Guid id, Guid userID);

        /// <summary>
        /// ไม่อนุมัติ PriceList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PriceListWorkflowDTO> RejectAsync(Guid id, Guid userID, RejectParam input);
    }
}
