using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.Params.Filters;
using Accounting.Services.IService;
using Base.DTOs.ACC;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Accounting.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CalendarLockController : Controller
    {
        private readonly ICalendarLockService CalendarLockService;
        private readonly DatabaseContext DB;

        public CalendarLockController(ICalendarLockService calendarLockService, DatabaseContext db)
        {
            this.DB = db;
            this.CalendarLockService = calendarLockService;
        }
        /// <summary>
        /// /CalendarLock/GetCalendar
        /// เรียก Calendar มาแสดง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        [HttpGet("GetCalendar")]
        //roducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> GetCalendarLocktAsync([FromQuery]CalendarLockFilter input)
        {
            var result = await CalendarLockService.GetCalendarLockListAsync(input);
            return Ok(result);
        }


        /// <summary>
        /// /CalendarLock/GetCalendarHistory
        /// เรียก Calendar History มาแสดง
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview

        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        [HttpGet("GetCalendarHistory")]
        [ProducesResponseType(200, Type = typeof(List<CalendarLockHistoryDTO>))]
        public async Task<IActionResult> GetCalendarLockHistorytAsync([FromQuery]CalendarLockReq input)
        {
            var result = await CalendarLockService.GetCalendarLockHistoryAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลวันที่ปิดบัญชีมาแสดงบนหน้าจอ
        /// Table : ACC.CalendarLock
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AddUpdateCalendar")] 
        public async Task<IActionResult> AddUpdateCalendarLockAsync([FromBody]List<CalendarLockReq> input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalendarLockService.AddUpdateCalendarLockAsync(input);
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
