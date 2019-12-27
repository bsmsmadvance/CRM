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
    public class RateSettingFixTransferModelController : BaseController
    {
        private IRateSettingFixTransferModelService RateSettingFixTransferModelService;
        private readonly DatabaseContext DB;

        public RateSettingFixTransferModelController(IRateSettingFixTransferModelService service, DatabaseContext db)
        {
            this.RateSettingFixTransferModelService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของFix Rating ตามแบบบ้านโอน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferModelDTO>))]
        public async Task<IActionResult> GetRateSettingFixTransferModelList([FromQuery]RateSettingFixTransferModelFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingFixTransferModelSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingFixTransferModelService.GetRateSettingFixTransferModelListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingFixTransferModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ดึงรายการ Fix Rating ตามแบบบ้านโอน ตามโครงการ
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="ActiveDate"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingFixTransferModelProjectList")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferModelDTO>))]
        public async Task<IActionResult> GetRateSettingFixTransferModelProjectList([FromQuery]Guid? ProjectID, [FromQuery]DateTime? ActiveDate)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixTransferModelService.GetRateSettingFixTransferModelProjectListAsync(ProjectID, ActiveDate);
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
        /// แก้ไขข้อมูล Fix Rating ตามแบบบ้านโอน พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>
        /// <returns></returns>
        [HttpPost("CreateRateSettingFixTransferModelList")]       
        public async Task<IActionResult> CreateRateSettingFixTransferModelList([FromBody]List<RateSettingFixSaleTransferModelDTO> ListInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixTransferModelService.CreateRateSettingFixTransferModelListAsync(ListInput);
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
        /// ข้อมูลFix Rating ตามแบบบ้านโอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> GetRateSettingFixTransferModel([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingFixTransferModelService.GetRateSettingFixTransferModelAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างFix Rating ตามแบบบ้านโอน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> CreateRateSettingFixTransferModel([FromBody]RateSettingFixSaleTransferModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixTransferModelService.CreateRateSettingFixTransferModelAsync(input);
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
        /// แก้ไขFix Rating ตามแบบบ้านโอน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> EditRateSettingFixTransferModel([FromRoute] Guid id, [FromBody]RateSettingFixSaleTransferModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixTransferModelService.UpdateRateSettingFixTransferModelAsync(id, input);
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
        /// ลบFix Rating ตามแบบบ้านโอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingFixTransferModel([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixTransferModelService.DeleteRateSettingFixTransferModelAsync(id);
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
