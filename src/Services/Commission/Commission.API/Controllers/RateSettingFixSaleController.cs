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
    public class RateSettingFixSaleController : BaseController
    {
        private IRateSettingFixSaleService RateSettingFixSaleService;
        private readonly DatabaseContext DB;

        public RateSettingFixSaleController(IRateSettingFixSaleService service, DatabaseContext db)
        {
            this.RateSettingFixSaleService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของFix Rating ขาย
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingFixSaleList([FromQuery]RateSettingFixSaleFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingFixSaleSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingFixSaleService.GetRateSettingFixSaleListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingFixSales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลFix Rating ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferDTO))]
        public async Task<IActionResult> GetRateSettingFixSale([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingFixSaleService.GetRateSettingFixSaleAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างFix Rating ขาย
        /// </summary>
        /// <param name="ListProjectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRateSettingFixSale([FromBody]RateSettingFixSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixSaleService.CreateRateSettingFixSaleAsync(input);
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
        /// แก้ไขFix Rating ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferDTO))]
        public async Task<IActionResult> EditRateSettingFixSale([FromRoute] Guid id, [FromBody]RateSettingFixSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixSaleService.UpdateRateSettingFixSaleAsync(id, input);
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
        /// ลบFix Rating ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingFixSale([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixSaleService.DeleteRateSettingFixSaleAsync(id);
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
