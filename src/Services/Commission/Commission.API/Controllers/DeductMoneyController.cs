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
    public class DeductMoneyController : BaseController
    {
        private IDeductMoneyService DeductMoneyService;
        private readonly DatabaseContext DB;

        public DeductMoneyController(IDeductMoneyService service, DatabaseContext db)
        {
            this.DeductMoneyService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของหักเงินคอมมิสชั่น
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<IncreaseDeductMoneyDTO>))]
        public async Task<IActionResult> GetDeductMoneyList([FromQuery]DeductMoneyFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DeductMoneySortByParam sortByParam)
        {
            try
            {
                var result = await DeductMoneyService.GetDeductMoneyListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.DeductMoneys);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลหักเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> GetDeductMoney([FromRoute] Guid id)
        {
            try
            {
                var result = await DeductMoneyService.GetDeductMoneyAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างหักเงินคอมมิสชั่น
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> CreateDeductMoney([FromBody]IncreaseDeductMoneyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DeductMoneyService.CreateDeductMoneyAsync(input);
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
        /// แก้ไขหักเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(IncreaseDeductMoneyDTO))]
        public async Task<IActionResult> EditDeductMoney([FromRoute] Guid id, [FromBody]IncreaseDeductMoneyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DeductMoneyService.UpdateDeductMoneyAsync(id, input);
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
        /// ลบหักเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeductMoney([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await DeductMoneyService.DeleteDeductMoneyAsync(id);
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
