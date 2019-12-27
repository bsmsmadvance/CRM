using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using MasterData.API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MasterData.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class CancelReasonsController : BaseController
    {
        private ICancelReasonService CancelReasonService;
        private readonly DatabaseContext DB;

        public CancelReasonsController(ICancelReasonService cancelReasonService, DatabaseContext db)
        {
            this.CancelReasonService = cancelReasonService;
            this.DB = db;
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<CancelReasonDropdownDTO>))]
        public async Task<IActionResult> GetCancelReasonDropdownList()
        {
            var results = await CancelReasonService.GetCancelReasonDropdownListAsync();
            return Ok(results);
        }

        /// <summary>
        /// ลิสของเหตุผลการยกเลิก
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CancelReasonDTO>))]
        public async Task<IActionResult> GetCancelReasonList([FromQuery]CancelReasonFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CancelReasonSortByParam sortByParam)
        {
            try
            {
                var result = await CancelReasonService.GetCancelReasonListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CancelReasons);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>s
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CancelReasonDTO))]
        public async Task<IActionResult> GetCancelReason([FromRoute] Guid id)
        {
            try
            {
                var result = await CancelReasonService.GetCancelReasonAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างเหตุผลการยกเลิก
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CancelReasonDTO))]
        public async Task<IActionResult> CreateCancelReason([FromBody]CancelReasonDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CancelReasonService.CreateCancelReasonAsync(input);
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
        /// แก้ไขเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CancelReasonDTO))]
        public async Task<IActionResult> EditCancelReason([FromRoute] Guid id, [FromBody]CancelReasonDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CancelReasonService.UpdateCancelReasonAsync(id, input);
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
        /// ลบเหตุผลการยกเลิก
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelReason([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CancelReasonService.DeleteCancelReasonAsync(id);
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