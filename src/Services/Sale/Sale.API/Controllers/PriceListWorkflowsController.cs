using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.SAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PriceListWorkflowsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IPriceListWorkflowService PriceListWorkflowService;
        public PriceListWorkflowsController(IPriceListWorkflowService priceListWorkflowService, DatabaseContext db)
        {
            this.DB = db;
            this.PriceListWorkflowService = priceListWorkflowService;
        }

        /// <summary>
        /// ดึงข้อมูลรายการการของอนุมัติ PriceList (Paging, Sorting, Filter)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447141/preview
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<PriceListWorkflowDTO>))]
        public async Task<IActionResult> GetPriceListWorkflowListAsync([FromQuery]PriceListWorkflowFilter filter, [FromQuery]PageParam pageParam, [FromQuery]PriceListWorkflowSortByParam sortByParam)
        {
            var result = await PriceListWorkflowService.GetPriceListWorkflowListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.PriceListWorkflowDTOs);
        }

        /// <summary>
        /// ดึงข้อมูลขออนุมัติ PriceList
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447142/preview
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PriceListWorkflowDTO))]
        public async Task<IActionResult> GetPriceListWorkflowAsync([FromRoute]Guid id)
        {
            var result = await PriceListWorkflowService.GetPriceListWorkflowAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// อนุมัติ PriceList
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Approve")]
        [ProducesResponseType(200, Type = typeof(PriceListWorkflowDTO))]
        public async Task<IActionResult> ApproveAsync([FromRoute]Guid id)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PriceListWorkflowService.ApproveAsync(id, userID.Value);
                    tran.Commit();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ไม่อนุมัติ PriceList
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Reject")]
        [ProducesResponseType(200, Type = typeof(PriceListWorkflowDTO))]
        public async Task<IActionResult> RejectAsync([FromRoute]Guid id, [FromBody]RejectParam input)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PriceListWorkflowService.RejectAsync(id, userID.Value, input);
                    tran.Commit();

                    return Ok(result);
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
