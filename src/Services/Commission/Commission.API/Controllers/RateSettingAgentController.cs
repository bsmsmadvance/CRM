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
using Commission.Params.Inputs;

namespace Commission.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RateSettingAgentController : BaseController
    {
        private IRateSettingAgentService RateSettingAgentService;
        private readonly DatabaseContext DB;

        public RateSettingAgentController(IRateSettingAgentService service, DatabaseContext db)
        {
            this.RateSettingAgentService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของAgent Rate
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<RateSettingAgentDTO>))]
        public async Task<IActionResult> GetRateSettingAgentList([FromQuery]RateSettingAgentFilter filter, [FromQuery]PageParam pageParam, [FromQuery]RateSettingAgentSortByParam sortByParam)
        {
            try
            {
                var result = await RateSettingAgentService.GetRateSettingAgentListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.RateSettingAgents);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายการ Agent Rate ตามโครงการ สำหรับสร้างใหม่
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRateSettingAgentProjectListForNew")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingAgentDTO>))]
        public async Task<IActionResult> GetRateSettingAgentProjectListForNew()
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingAgentService.GetRateSettingAgentProjectListForNewAsync();
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
        /// ดึงรายการ Agent Rate ตามโครงการ สำหรับแก้ไข
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="ActiveDate"></param>
        /// <returns></returns>
        [HttpGet("GetRateSettingAgentProjectListForUpdate")]
        [ProducesResponseType(200, Type = typeof(List<RateSettingAgentDTO>))]
        public async Task<IActionResult> GetRateSettingAgentProjectListForUpdate([FromQuery]Guid? ProjectID, [FromQuery]DateTime? ActiveDate)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingAgentService.GetRateSettingAgentProjectListForUpdateAsync(ProjectID, ActiveDate);
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
        /// สร้างข้อมูล Agent Rate พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost("CreateRateSettingAgentList")]
        public async Task<IActionResult> CreateRateSettingAgentList([FromBody]RateSettingAgentInput inputModel)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingAgentService.CreateRateSettingAgentListAsync(inputModel);
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
        /// แก้ไขข้อมูล Agent Rate พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>
        /// <returns></returns>
        [HttpPost("UpdateRateSettingAgentList")]
        public async Task<IActionResult> UpdateRateSettingAgentList([FromBody]List<RateSettingAgentDTO> ListInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingAgentService.UpdateRateSettingAgentListAsync(ListInput);
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
        /// ข้อมูลAgent Rate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingAgentDTO))]
        public async Task<IActionResult> GetRateSettingAgent([FromRoute] Guid id)
        {
            try
            {
                var result = await RateSettingAgentService.GetRateSettingAgentAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างAgent Rate
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RateSettingAgentDTO))]
        public async Task<IActionResult> CreateRateSettingAgent([FromBody]RateSettingAgentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingAgentService.CreateRateSettingAgentAsync(input);
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
        /// แก้ไขAgent Rate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RateSettingAgentDTO))]
        public async Task<IActionResult> EditRateSettingAgent([FromRoute] Guid id, [FromBody]RateSettingAgentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await RateSettingAgentService.UpdateRateSettingAgentAsync(id, input);
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
        /// ลบAgent Rate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateSettingAgent([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await RateSettingAgentService.DeleteRateSettingAgentAsync(id);
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
    }
}
