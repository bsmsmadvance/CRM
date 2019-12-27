using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commission.Params.Filters;
using Commission.Params.Inputs;
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
    public class RateSettingSaleController : BaseController
    {
        private IRateSettingSaleService RateSettingSaleService;
        private readonly DatabaseContext DB;

        public RateSettingSaleController(IRateSettingSaleService service, DatabaseContext db)
        {
            this.RateSettingSaleService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของRating% ขาย
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingSaleList([FromQuery]RateSettingSaleFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingSaleSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingSaleService.GetRateSettingSaleListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingSales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ดึงรายการ Rating% ขาย ตามโครงการ สำรหรับสร้างรายการใหม่
        /// </summary>
        /// <param name="BGNo"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingSaleProjectListForNew")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingSaleProjectListForNew([FromQuery]string BGNo)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingSaleService.GetRateSettingSaleProjectListForNewAsync(BGNo);
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
        /// ดึงรายการ Rating% ขาย ตามโครงการ สำหรับ update
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="ActiveDate"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingSaleProjectListForUpdate")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingSaleProjectListForUpdate([FromQuery]Guid? ProjectID, [FromQuery]DateTime? ActiveDate)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingSaleService.GetRateSettingSaleProjectListForUpdateAsync(ProjectID, ActiveDate);
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
        /// สร้างข้อมูล Rating% ขาย พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost("CreateRateSettingSaleList")]
        public async Task<IActionResult> CreateRateSettingSaleList([FromBody]RateSettingSaleInput inputModel)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingSaleService.CreateRateSettingSaleListAsync(inputModel);
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
        /// แก้ไขข้อมูล Rating% ขาย พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>
        /// <returns></returns>
        [HttpPost("UpdateRateSettingSaleList")]
        public async Task<IActionResult> UpdateRateSettingSaleList([FromBody]List<RateSettingSaleTransferDTO> ListInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingSaleService.UpdateRateSettingSaleListAsync(ListInput);
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
        /*
        /// <summary>
        /// ข้อมูลRating% ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> GetRateSettingSale([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingSaleService.GetRateSettingSaleAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างRating% ขาย
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> CreateRateSettingSale([FromBody]RateSettingSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingSaleService.CreateRateSettingSaleAsync(input);
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
        /// แก้ไขRating% ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> EditRateSettingSale([FromRoute] Guid id, [FromBody]RateSettingSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingSaleService.UpdateRateSettingSaleAsync(id, input);
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
        /// ลบRating% ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingSale([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingSaleService.DeleteRateSettingSaleAsync(id);
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
        */
        /// <summary>
        /// Import Excel
        /// </summary>
        /// <param name="BGID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferExcelDTO))]
        [HttpPost("RateSettingSales/Import")]
        public async Task<IActionResult> ImportProjectRateSettingSale([FromRoute]Guid BGID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingSaleService.ImportRateSettingSaleAsync(BGID, input);
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
        /// Export Excel
        /// </summary>
        /// <param name="BGID"></param>
        /// <param name="filter"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("RateSettingSales/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectRateSettingSale([FromRoute]Guid BGID, [FromQuery]RateSettingSaleFilter filter, [FromQuery] RateSettingSaleSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingSaleService.ExportExcelRateSettingSaleAsync(BGID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
