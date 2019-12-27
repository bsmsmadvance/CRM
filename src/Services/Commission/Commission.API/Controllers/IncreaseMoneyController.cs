using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using Commission.API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commission.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Commission.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class IncreaseMoneyController : BaseController
    {
        private IIncreaseMoneyService IncreaseMoneyService;
        private readonly DatabaseContext DB;

        public IncreaseMoneyController(IIncreaseMoneyService service, DatabaseContext db)
        {
            this.IncreaseMoneyService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<IncreaseDeductMoneyDTO>))]
        public async Task<IActionResult> GetIncreaseMoneyList([FromQuery]IncreaseMoneyFilter filter, [FromQuery]PageParam pageParam, [FromQuery]IncreaseMoneySortByParam sortByParam)
        {
            try
            {
                var result = await IncreaseMoneyService.GetIncreaseMoneyListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.IncreaseMoneys);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> GetIncreaseMoney([FromRoute] Guid id)
        {
            try
            {
                var result = await IncreaseMoneyService.GetIncreaseMoneyAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> CreateIncreaseMoney([FromBody]IncreaseDeductMoneyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await IncreaseMoneyService.CreateIncreaseMoneyAsync(input);
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
        /// แก้ไขเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> EditIncreaseMoney([FromRoute] Guid id, [FromBody]IncreaseDeductMoneyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await IncreaseMoneyService.UpdateIncreaseMoneyAsync(id, input);
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
        /// ลบเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncreaseMoney([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await IncreaseMoneyService.DeleteIncreaseMoneyAsync(id);
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
