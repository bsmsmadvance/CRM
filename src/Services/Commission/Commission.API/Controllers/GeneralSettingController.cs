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
    public class GeneralSettingController : BaseController
    {
        private IGeneralSettingService GeneralSettingService;
        private readonly DatabaseContext DB;

        public GeneralSettingController(IGeneralSettingService service, DatabaseContext db)
        {
            this.GeneralSettingService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<GeneralSettingDTO>))]
        public async Task<IActionResult> GetGeneralSettingList([FromQuery]GeneralSettingFilter filter, [FromQuery]PageParam pageParam, [FromQuery]GeneralSettingSortByParam sortByParam)
        {
            try
            {
                var result = await GeneralSettingService.GetGeneralSettingListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.GeneralSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GeneralSettingDTO))]
        public async Task<IActionResult> GetGeneralSetting([FromRoute] Guid id)
        {
            try
            {
                var result = await GeneralSettingService.GetGeneralSettingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="ListProjectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateGeneralSetting([FromBody]GeneralSettingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await GeneralSettingService.CreateGeneralSettingAsync(input);
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
        /// แก้ไขเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(GeneralSettingDTO))]
        public async Task<IActionResult> EditGeneralSetting([FromRoute] Guid id, [FromBody]GeneralSettingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await GeneralSettingService.UpdateGeneralSettingAsync(id, input);
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
        /// ลบเพิ่มเงินคอมมิสชั่น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralSetting([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await GeneralSettingService.DeleteGeneralSettingAsync(id);
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
