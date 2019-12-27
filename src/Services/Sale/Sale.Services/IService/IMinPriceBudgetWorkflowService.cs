using Base.DTOs.SAL;
using Sale.Params.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public interface IMinPriceBudgetWorkflowService
    {
        /// <summary>
        /// ดึงข้อมูล Budget Quarterly
        /// https://projects.invisionapp.com/d/?origin=v7?origin=v7#/console/17482068/366447194/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<BudgetQuarterlyDTO> GetBudgetQuarterlyAsync(BudgetMinPriceWorkflowFilter filter);
        /// <summary>
        /// ดึงข้อมูล Budget Adhoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<AdhocDTO> GetAdhocAsync(BudgetMinPriceWorkflowFilter filter);
        /// <summary>
        /// ดึงรายการรออนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<MinPriceBudgetWorkflowDTO>> GetMinPriceBudgetWorkFlowsAsync(BudgetMinPriceWorkflowFilter filter, Guid? userID);
        /// <summary>
        /// ดึงข้อมูลผู้ร้องขอ และผู้อนุมัติของ MinPriceBudgetWorkflow
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<MinPriceBudgetApprovalDTO>> GetMinPriceBudgetApprovalAsync(MinPriceBudgetApprovalFilter filter);
        /// <summary>
        /// อนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="minPriceBudgetWorkFlows"></param>
        /// <returns></returns>
        Task ApproveMinPriceBudgetWorkFlowAsync(List<MinPriceBudgetWorkflowDTO> minPriceBudgetWorkFlows);
        /// <summary>
        /// ไม่อนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="minPriceBudgetWorkFlows"></param>
        /// <returns></returns>
        Task RejectMinPriceBudgetWorkFlowAsync(List<MinPriceBudgetWorkflowDTO> minPriceBudgetWorkFlows, string rejectComment);
    }
}
