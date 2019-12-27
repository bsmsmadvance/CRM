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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterCenterGroupsController : BaseController
    {
        private IMasterCenterGroupService MasterCenterGroupService;
        private readonly DatabaseContext DB;

        public MasterCenterGroupsController(IMasterCenterGroupService masterCenterGroupService, DatabaseContext db)
        {
            this.MasterCenterGroupService = masterCenterGroupService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterCenterGroupDTO>))]
        public async Task<IActionResult> GetMasterCenterGroupList([FromQuery]MasterCenterGroupFilter filter, [FromQuery]PageParam pageParam, [FromQuery]MasterCenterGroupSortByParam sortByParam)
        {
            try
            {
                var result = await MasterCenterGroupService.GetMasterCenterGroupListAsync(filter, pageParam,sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterCenterGroups);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}")]
        [ProducesResponseType(200, Type = typeof(MasterCenterGroupDTO))]
        public async Task<IActionResult> GetMasterCenterGroup([FromRoute] string key)
        {
            try
            {
                var result = await MasterCenterGroupService.GetMasterCenterGroupAsync(key);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// เพิ่ม กลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MasterCenterGroupDTO))]
        public async Task<IActionResult> CreateMasterCenterGroup([FromBody]MasterCenterGroupDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterCenterGroupService.CreateMasterCenterGroupAsync(input);
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
        /// แก้ไขกลุ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{key}")]
        [ProducesResponseType(200, Type = typeof(MasterCenterGroupDTO))]
        public async Task<IActionResult> EditMasterCenterGroup([FromRoute] string key,[FromBody]MasterCenterGroupDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterCenterGroupService.UpdateMasterCenterGroupAsync(key, input);
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
        /// ลบ กลุ่มข้อมูลกลุ่มพื้นฐานทั่วไป
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteMasterCenterGroup([FromRoute] string key)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterCenterGroupService.DeleteMasterCenterGroupAsync(key);
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