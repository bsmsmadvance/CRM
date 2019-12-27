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
    public class CancelReturnSettingsController : BaseController
    {
        private ICancelReturnSettingService CancelReturnSettingService;
        private readonly DatabaseContext DB;

        public CancelReturnSettingsController(ICancelReturnSettingService cancelReturnSettingService, DatabaseContext db)
        {
            this.CancelReturnSettingService = cancelReturnSettingService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูล ตั้งค่าการยกเลิกคืนเงิน
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CancelReturnSettingDTO))]
        public async Task<IActionResult> GetCancelReturnSetting()
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CancelReturnSettingService.GetCancelReturnSettingAsync();
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
        /// แก้ไขข้อมูล ตั้งค่าการยกเลิกคืนเงิน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CancelReturnSettingDTO))]
        public async Task<IActionResult> EditCancelReturnSetting([FromRoute] Guid id, [FromBody]CancelReturnSettingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CancelReturnSettingService.UpdateCancelReturnSettingAsync(id, input);
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

    }
}