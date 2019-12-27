using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Sale.Services;
using Sale.Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sale.API.Controllers
{
    [Route("api/[controller]")]
    public class BookingOwnersController : Controller
    {
        private readonly IBookingOwnerService BookingOwnerService;
        private readonly DatabaseContext DB;

        public BookingOwnersController(IBookingOwnerService bookingOwnerService, DatabaseContext db)
        {
            this.BookingOwnerService = bookingOwnerService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงผู้จองหลักลง dropdown (IsMain=true)
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BookingOwnerDropdownDTO>))]
        public async Task<IActionResult> GetBookingOwnerDropdownAsync([FromQuery]Guid bookingID)
        {
            var results = await this.BookingOwnerService.GetBookingOwnerDropdownAsync(bookingID);
            return Ok(results);
        }

        /// <summary>
        /// ลิสรายการผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookingOwnerDTO>))]
        public async Task<IActionResult> GetBookingOwnersAsync([FromQuery]Guid bookingID)
        {
            var results = await this.BookingOwnerService.GetBookingOwnersAsync(bookingID);
            return Ok(results);
        }

        /// <summary>
        /// Get ข้อมูลผู้จอง Draft สำหรับการเพิ่มผู้จอง
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("Draft")]
        [ProducesResponseType(200, Type = typeof(BookingOwnerDTO))]
        public async Task<IActionResult> GetBookingOwnersDraftAsync([FromQuery]Guid bookingID, [FromQuery]Guid contactID)
        {
            var result = await this.BookingOwnerService.GetBookingOwnersDraftAsync(bookingID, contactID);
            return Ok(result);
        }

        /// <summary>
        /// เพิ่มผู้จอง
        /// โดยถ้าส่ง FromContactID เข้ามา ถือเป็นการเพิ่มผู้จองจากการเลือก Contact ที่มีอยู่
        /// แต่ถ้า ไม่ได้ส่ง ContactID มา จะต้องส่ง BookingOwnerDTO มาเพื่อทำการสร้าง Contact ใหม่พร้อมกันเพิ่มผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookingOwnerDTO))]
        public async Task<IActionResult> CreateBookingOwnerAsync([FromQuery]Guid bookingID, [FromBody]BookingOwnerDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.BookingOwnerService.CreateBookingOwnerAsync(bookingID, input);
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
        /// แก้ไขชื่อผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366318/preview
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BookingOwnerDTO))]
        public async Task<IActionResult> EditBookingOwnerAsync([FromRoute]Guid id, [FromBody]BookingOwnerDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.BookingOwnerService.EditBookingOwnerAsync(id, input);
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
        /// ตั้งผู้จองหลัก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/SetMain")]
        [ProducesResponseType(200, Type = typeof(BookingOwnerDTO))]
        public async Task<IActionResult> SetMainBookingOwnerAsync([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.BookingOwnerService.SetMainBookingOwnerAsync(id);
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
        /// เรียงลำดับผู้จอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366310/preview
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/ReOrder")]
        [ProducesResponseType(200, Type = typeof(BookingOwnerDTO))]
        public async Task<IActionResult> ReOrderBookingOwnerAsync([FromRoute]Guid id, [FromBody]BookingOwnerDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.BookingOwnerService.ReOrderBookingOwnerAsync(id, input);
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
        /// ลบผู้จอง
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingOwnerAsync([FromRoute]Guid id, [FromQuery]string reason = null)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await this.BookingOwnerService.DeleteBookingOwnerAsync(id, reason);
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
