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
    public class RateSettingTransferController : BaseController
    {
        private IRateSettingTransferService RateSettingTransferService;
        private readonly DatabaseContext DB;

        public RateSettingTransferController(IRateSettingTransferService service, DatabaseContext db)
        {
            this.RateSettingTransferService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของRating% โอน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingTransferList([FromQuery]RateSettingTransferFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingTransferSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingTransferService.GetRateSettingTransferListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingTransfers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายการ Rating% โอน ตามโครงการ สำรหรับสร้างรายการใหม่
        /// </summary>
        /// <param name="BGNo"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingTransferProjectListForNew")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingTransferProjectListForNew([FromQuery]string BGNo)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingTransferService.GetRateSettingTransferProjectListForNewAsync(BGNo);
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
        /// ดึงรายการ Rating% โอน ตามโครงการ สำหรับ update
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="ActiveDate"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingTransferProjectListForUpdate")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingSaleTransferDTO>))]
        public async Task<IActionResult> GetRateSettingTransferProjectListForUpdate([FromQuery]Guid? ProjectID, [FromQuery]DateTime? ActiveDate)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingTransferService.GetRateSettingTransferProjectListForUpdateAsync(ProjectID, ActiveDate);
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
        /// สร้างข้อมูล Rating% โอน พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost("CreateRateSettingTransferList")]
        public async Task<IActionResult> CreateRateSettingTransferList([FromBody]RateSettingTransferInput inputModel)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingTransferService.CreateRateSettingTransferListAsync(inputModel);
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
        /// แก้ไขข้อมูล Rating% โอน พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>
        /// <returns></returns>
        [HttpPost("UpdateRateSettingTransferList")]
        public async Task<IActionResult> UpdateRateSettingTransferList([FromBody]List<RateSettingSaleTransferDTO> ListInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingTransferService.UpdateRateSettingTransferListAsync(ListInput);
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
        /// ข้อมูลRating% โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> GetRateSettingTransfer([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingTransferService.GetRateSettingTransferAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างRating% โอน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> CreateRateSettingTransfer([FromBody]RateSettingSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingTransferService.CreateRateSettingTransferAsync(input);
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
        /// แก้ไขRating% โอน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingSaleTransferDTO))]
        public async Task<IActionResult> EditRateSettingTransfer([FromRoute] Guid id, [FromBody]RateSettingSaleTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingTransferService.UpdateRateSettingTransferAsync(id, input);
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
        /// ลบRating% โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingTransfer([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingTransferService.DeleteRateSettingTransferAsync(id);
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
        [HttpPost("RateSettingTransfers/Import")]
        public async Task<IActionResult> ImportProjectRateSettingTransfer([FromRoute]Guid BGID, [FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingTransferService.ImportRateSettingTransferAsync(BGID, input);
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
        [HttpGet("RateSettingTransfers/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportProjectRateSettingTransfer([FromRoute]Guid BGID, [FromQuery]RateSettingTransferFilter filter, [FromQuery] RateSettingTransferSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingTransferService.ExportExcelRateSettingTransferAsync(BGID, filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
