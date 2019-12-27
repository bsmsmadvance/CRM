using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sale.Params.Inputs;
using Sale.Services;

namespace Sale.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    public class ChangeUnitWorkflowsController : BaseController
    {
        private readonly IChangeUnitWorkflowService ChangeUnitWorkflowService;
        private readonly DatabaseContext DB;

        public ChangeUnitWorkflowsController(IChangeUnitWorkflowService changeUnitWorkflowService, DatabaseContext db)
        {
            this.ChangeUnitWorkflowService = changeUnitWorkflowService;
            this.DB = db;
        }

        /// <summary>
        /// สร้าง Workflow การย้ายแปลงจอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7?origin=v7#/console/17482068/369187180/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookingChangeUnitWorkflowDTO))]
        public async Task<IActionResult> CreateBookingChangeUnitWorkflowAsync([FromBody]CreateBookingChangeUnitWorkflowInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    var result = await ChangeUnitWorkflowService.CreateBookingChangeUnitWorkflowAsync(input.FromBookingID, input.ToUnitID, input.PriceList, input.BookingPromotion, input.TransferPromotion, input.Expenses, input.MinPriceBudgetReason, userID);
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
        /// ยกเลิกอนุมัติย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{changeUnitWorkflowID}/Cancel")]
        public async Task<IActionResult> CancelChangeUnitAsync([FromRoute]Guid changeUnitWorkflowID)
        {
            await ChangeUnitWorkflowService.CancelChangeUnitAsync(changeUnitWorkflowID);

            return Ok();
        }

        /// <summary>
        /// ดึงข้อมูลการตั้งเรื่องย้ายแปลงจองในปัจจุบันของใบจองใบนี้
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BookingChangeUnitWorkflowDTO))]
        public async Task<IActionResult> GetBookingChangeUnitWorkflowAsync([FromQuery]Guid bookingID)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
            {
                userID = parsedUserID;
            }
            var result = await ChangeUnitWorkflowService.GetBookingChangeUnitWorkflowAsync(bookingID, userID);
            return Ok(result);
        }

        /// <summary>
        /// อนุมัติตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <returns></returns>
        [HttpPost("{changeUnitWorkflowID}/Approve")]
        public async Task<IActionResult> BookingRequestApproveAsync([FromRoute]Guid changeUnitWorkflowID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    await ChangeUnitWorkflowService.BookingRequestApproveAsync(changeUnitWorkflowID, userID);
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
        /// ไม่อนุมัติตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{changeUnitWorkflowID}/Reject")]
        public async Task<IActionResult> BookingRequestRejectAsync([FromRoute]Guid changeUnitWorkflowID, [FromBody]RejectParam input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    await ChangeUnitWorkflowService.BookingRequestRejectAsync(changeUnitWorkflowID, input, userID);
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
