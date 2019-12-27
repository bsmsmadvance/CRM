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
    public class RateSettingFixSaleModelController : BaseController
    {
        private IRateSettingFixSaleModelService RateSettingFixSaleModelService;
        private readonly DatabaseContext DB;

        public RateSettingFixSaleModelController(IRateSettingFixSaleModelService service, DatabaseContext db)
        {
            this.RateSettingFixSaleModelService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของFix Rating ตามแบบบ้านขาย
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferModelDTO>))]
        public async Task<IActionResult> GetRateSettingFixSaleModelList([FromQuery]RateSettingFixSaleModelFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingFixSaleModelSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingFixSaleModelService.GetRateSettingFixSaleModelListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingFixSaleModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ดึงรายการ Fix Rating ตามแบบบ้านขาย ตามโครงการ
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="ActiveDate"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingFixSaleModelProjectList")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingFixSaleTransferModelDTO>))]
        public async Task<IActionResult> GetRateSettingFixSaleModelProjectList([FromQuery]Guid? ProjectID, [FromQuery]DateTime? ActiveDate)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixSaleModelService.GetRateSettingFixSaleModelProjectListAsync(ProjectID, ActiveDate);
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
        /// แก้ไขข้อมูล Fix Rating ตามแบบบ้านขาย พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>
        /// <returns></returns>
        [HttpPost("CreateRateSettingFixSaleModelList")]       
        public async Task<IActionResult> CreateRateSettingFixSaleModelList([FromBody]List<RateSettingFixSaleTransferModelDTO> ListInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixSaleModelService.CreateRateSettingFixSaleModelListAsync(ListInput);
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
        /// ข้อมูลFix Rating ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> GetRateSettingFixSaleModel([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingFixSaleModelService.GetRateSettingFixSaleModelAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างFix Rating ตามแบบบ้านขาย
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> CreateRateSettingFixSaleModel([FromBody]RateSettingFixSaleTransferModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixSaleModelService.CreateRateSettingFixSaleModelAsync(input);
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
        /// แก้ไขFix Rating ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingFixSaleTransferModelDTO))]
        public async Task<IActionResult> EditRateSettingFixSaleModel([FromRoute] Guid id, [FromBody]RateSettingFixSaleTransferModelDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingFixSaleModelService.UpdateRateSettingFixSaleModelAsync(id, input);
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
        /// ลบFix Rating ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingFixSaleModel([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingFixSaleModelService.DeleteRateSettingFixSaleModelAsync(id);
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
