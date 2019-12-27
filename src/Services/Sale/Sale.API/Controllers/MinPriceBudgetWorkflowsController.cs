using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MinPriceBudgetWorkflowsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IMinPriceBudgetWorkflowService MinPriceBudgetWorkflowService;

        public MinPriceBudgetWorkflowsController(IMinPriceBudgetWorkflowService minPriceBudgetWorkflowService, DatabaseContext db)
        {
            this.DB = db;
            this.MinPriceBudgetWorkflowService = minPriceBudgetWorkflowService;
        }

        /// <summary>
        /// ดึงข้อมูล Budget Quarterly
        /// https://projects.invisionapp.com/d/?origin=v7?origin=v7#/console/17482068/366447194/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("BudgetQuarterly")]
        [ProducesResponseType(200, Type = typeof(BudgetQuarterlyDTO))]
        public async Task<IActionResult> GetBudgetQuarterlyAsync([FromQuery]BudgetMinPriceWorkflowFilter filter)
        {
            var result = await MinPriceBudgetWorkflowService.GetBudgetQuarterlyAsync(filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูล Budget Adhoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("BudgetAdhoc")]
        [ProducesResponseType(200, Type = typeof(AdhocDTO))]
        public async Task<IActionResult> GetAdhocAsync([FromQuery]BudgetMinPriceWorkflowFilter filter)
        {
            var result = await MinPriceBudgetWorkflowService.GetAdhocAsync(filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงรายการรออนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MinPriceBudgetWorkflowDTO>))]
        public async Task<IActionResult> GetMinPriceBudgetWorkFlowsAsync([FromQuery]BudgetMinPriceWorkflowFilter filter)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            var results = await MinPriceBudgetWorkflowService.GetMinPriceBudgetWorkFlowsAsync(filter, userID);
            return Ok(results);
        }

        /// <summary>
        /// ดึงข้อมูลผู้ร้องขอ และผู้อนุมัติของ MinPriceBudgetWorkflow
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("Approvals")]
        [ProducesResponseType(200, Type = typeof(List<MinPriceBudgetApprovalDTO>))]
        public async Task<IActionResult> GetMinPriceBudgetApprovalAsync([FromQuery]MinPriceBudgetApprovalFilter filter)
        {
            var results = await MinPriceBudgetWorkflowService.GetMinPriceBudgetApprovalAsync(filter);
            return Ok(results);
        }

        /// <summary>
        /// อนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="minPriceBudgetWorkFlows"></param>
        /// <returns></returns>
        [HttpPost("Approve")]
        public async Task<IActionResult> ApproveMinPriceBudgetWorkFlowAsync([FromBody]List<MinPriceBudgetWorkflowDTO> minPriceBudgetWorkFlows)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MinPriceBudgetWorkflowService.ApproveMinPriceBudgetWorkFlowAsync(minPriceBudgetWorkFlows);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ไม่อนุมัติ MinPrice Budget
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Reject")]
        public async Task<IActionResult> RejectMinPriceBudgetWorkFlowAsync([FromBody]MinPriceBudgetRejectInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MinPriceBudgetWorkflowService.RejectMinPriceBudgetWorkFlowAsync(input.MinPriceBudgetWorkFlows, input.RejectComment);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
    }
}
