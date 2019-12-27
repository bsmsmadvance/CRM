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
    public class RateSettingFixTransferController : BaseController
    {
        private IRateSettingFixTransferService RateSettingFixTransferService;
        private readonly DatabaseContext DB;

        public RateSettingFixTransferController(IRateSettingFixTransferService service, DatabaseContext db)
        {
            this.RateSettingFixTransferService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของFix Rating โอน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingFixTransferList([FromQuery]RateSettingFixTransferFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingFixTransferSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingFixTransferService.GetRateSettingFixTransferListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingFixTransfers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลFix Rating โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferDTO))]
        public async Task<IActionResult> GetRateSettingFixTransfer([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingFixTransferService.GetRateSettingFixTransferAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างFix Rating โอน
        /// </summary>
        /// <param name="ListProjectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRateSettingFixTransfer([FromBody]RateSettingFixSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixTransferService.CreateRateSettingFixTransferAsync(input);
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
        /// แก้ไขFix Rating โอน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferDTO))]
        public async Task<IActionResult> EditRateSettingFixTransfer([FromRoute] Guid id, [FromBody]RateSettingFixSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixTransferService.UpdateRateSettingFixTransferAsync(id, input);
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
        /// ลบFix Rating โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingFixTransfer([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixTransferService.DeleteRateSettingFixTransferAsync(id);
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
