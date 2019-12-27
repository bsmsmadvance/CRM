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
using Sale.Services.IService;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ChangePromotionWorkflowsController : Controller
    {
        private readonly DatabaseContext DB;
        private readonly IChangePromotionWorkflowService ChangePromotionWorkflowService;

        public ChangePromotionWorkflowsController(IChangePromotionWorkflowService changePromotionWorkflowService, DatabaseContext db)
        {
            this.DB = db;
            this.ChangePromotionWorkflowService = changePromotionWorkflowService;
        }

        /// <summary>
        /// ดึงข้อมูลโปรโมชั่น กรณีเปลี่ยนแปลงโปรยังไม่อนุมัติ
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("BookingPromotions")]
        [ProducesResponseType(200, Type = typeof(ChangePromotionWorkflowDTO))]
        public async Task<IActionResult> GetChangeBookingPromotionAsync([FromQuery]Guid bookingID)
        {
            var result = await ChangePromotionWorkflowService.GetChangeBookingPromotionAsync(bookingID);
            return Ok(result);
        }

        /// <summary>
        /// ตั้งเรื่องเปลี่ยนแปลงโปรโมชัน
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateChangePromotionWorkflow([FromQuery]Guid bookingID, [FromBody]ChangePromotionWorkflowDTO input)
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
                    await ChangePromotionWorkflowService.CreateChangePromotionWorkflow(bookingID, input);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            
            return Ok();
        }

        /// <summary>
        /// ตรวจสอบ MinPrice
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("IsMinPriceWorkflow")]
        [ProducesResponseType(200, Type = typeof(MinPriceBudgetWorkflowTypeDTO))]
        public async Task<IActionResult> IsMinPriceChangePromotionAsync([FromQuery]Guid bookingID, [FromBody]CreateChangePromotionWorkflowInput input)
        {
            var result = await ChangePromotionWorkflowService.IsMinPriceChangePromotionAsync(bookingID, input.BookingPromotion, input.Expenses);
            return Ok(result);
        }

    }
}
