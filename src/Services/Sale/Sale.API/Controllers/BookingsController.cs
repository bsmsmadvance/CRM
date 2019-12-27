using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
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
    public class BookingsController : BaseController
    {
        private readonly IBookingService BookingService;
        private readonly DatabaseContext DB;

        public BookingsController(IBookingService bookingService, DatabaseContext db)
        {
            this.BookingService = bookingService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงรายการจอง
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366306/preview
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BookingListDTO>))]
        public async Task<IActionResult> GetBookingListAsync([FromQuery]BookingListFilter filter, [FromQuery]PageParam pageParam, [FromQuery]BookingListSortByParam sortByParam)
        {
            var result = await BookingService.GetBookingListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.Bookings);
        }

        /// <summary>
        /// ดึงข้อมูลใบจอง
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> GetBookingAsync([FromRoute]Guid id)
        {
            var result = await BookingService.GetBookingAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูล Price List ของใบจอง
        /// </summary>
        /// <param name="id">ID ใบจอง</param>
        /// <returns></returns>
        [HttpGet("{id}/PriceLists")]
        [ProducesResponseType(200, Type = typeof(BookingPriceListDTO))]
        public async Task<IActionResult> GetPriceListAsync([FromRoute]Guid id)
        {
            var result = await BookingService.GetPriceListAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรโมชันก่อนขาย ของใบจอง
        /// </summary>
        /// <param name="id">ID ใบจอง</param>
        /// <returns></returns>
        [HttpGet("{id}/BookingPreSalePromotions")]
        [ProducesResponseType(200, Type = typeof(BookingPreSalePromotionDTO))]
        public async Task<IActionResult> GetBookingPreSalePromotionAsync([FromRoute]Guid id)
        {
            var result = await BookingService.GetBookingPreSalePromotionAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรโมชัน ของใบจอง
        /// </summary>
        /// <param name="id">ID ใบจอง</param>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        [HttpGet("{id}/BookingPromotions")]
        [ProducesResponseType(200, Type = typeof(BookingPromotionDTO))]
        public async Task<IActionResult> GetBookingPromotionAsync([FromRoute]Guid id, [FromQuery]BookingPromotionFilter filter = null)
        {
            var result = await BookingService.GetBookingPromotionAsync(id, filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงรายการค่าใช้จ่าย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/PromotionExpenses")]
        [ProducesResponseType(200, Type = typeof(List<BookingPromotionExpenseDTO>))]
        public async Task<IActionResult> GetPromotionExpensesAsync([FromRoute]Guid id)
        {
            var results = await BookingService.GetPromotionExpensesAsync(id);
            return Ok(results);
        }

        /// <summary>
        /// แก้ไขใบจอง (บันทึกใบจอง)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{id}/Save")]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> UpdateBookingAsync([FromRoute]Guid id, [FromBody]SaveBookingInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BookingService.UpdateBookingAsync(id, input.Booking, input.PriceList, input.BookingPromotion, input.Expenses);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// ดึง Form ยกเลิกใบจอง
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/368011238/preview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/CancelMemoForm")]
        [ProducesResponseType(200, Type = typeof(CancelMemoDTO))]
        public async Task<IActionResult> GetCancelMemoFormAsync([FromRoute]Guid id)
        {
            var result = await BookingService.GetCancelMemoFormAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// ยกเลิกใบจอง
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/368011238/preview
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{id}/CancelMemoForm")]
        public async Task<IActionResult> CancelBookingAsync(Guid id, [FromBody]CancelMemoDTO input)
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
                    await BookingService.CancelBookingAsync(id, input, userID.Value);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// ลบใบจอง
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BookingService.DeleteBookingAsync(id);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        
        /// <summary>
        /// ตรวจสอบ min price
        /// </summary>
        /// <returns></returns>
        [HttpPost("IsMinPriceWorkflows")]
        [ProducesResponseType(200, Type = typeof(MinPriceBudgetWorkflowTypeDTO))]
        public async Task<IActionResult> IsMinPriceChangedAsync([FromBody]SaveBookingInput input)
        {
            var result = await BookingService.IsMinPriceChangedAsync(input.Booking, input.PriceList, input.BookingPromotion,
                input.Expenses);

            return Ok(result);
        }

        /// <summary>
        /// ยืนยันใบจอง
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Confirms")]
        public async Task<IActionResult> ConfirmBookingAsync([FromBody]List<BookingListDTO> input)
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

                    await BookingService.ConfirmBookingAsync(input, userID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// สร้างใบจอง (จากระบบอื่น)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> CreateBookingAsync([FromBody]SaveBookingInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BookingService.CreateBookingAsync(input.Booking, input.PriceList, input.BookingPromotion, input.Expenses);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
