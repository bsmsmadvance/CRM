using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using MasterData.API.Extensions;
using Base.DTOs.MST;
using MasterData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BOConfigurationsController : ControllerBase
    {
        private DatabaseContext DB;
        private readonly IBOConfigurationService BOConfigurationService;

        public BOConfigurationsController(IBOConfigurationService bOConfigurationService, DatabaseContext db)
        {
            this.BOConfigurationService = bOConfigurationService;
            this.DB = db;
        }
        /// <summary>
        /// ดึงข้อมูล BOConfig
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BOConfigurationDTO))]
        public async Task<IActionResult> GetBOConfiguration()
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BOConfigurationService.GetBOConfigurationAsync();
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
        /// แก้ไขข้อมูล BOConfig
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BOConfigurationDTO))]
        public async Task<IActionResult> EditBOConfiguration([FromRoute] Guid id, [FromBody]BOConfigurationDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BOConfigurationService.UpdateBOConfigurationAsync(id, input);
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