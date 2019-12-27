using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AgreementOwnerController : BaseController
    {
        private readonly IAgreementOwnerService AgreementOwnerService;
        private readonly DatabaseContext DB;

        public AgreementOwnerController(IAgreementOwnerService AgreementOwnerService, DatabaseContext db)
        {
            this.AgreementOwnerService = AgreementOwnerService;
            this.DB = db;
        }

        /// <summary>
        /// แสดงข้อมูลผู้ทำสัญญา
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAgreementOwnersAsync")]
        [ProducesResponseType(200, Type = typeof(List<AgreementOwnerDTO>))]
        public async Task<IActionResult> GetAgreementOwnersAsync([FromQuery]Guid id)
        {
            var result = await AgreementOwnerService.GetAgreementOwnersAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// การเปลี่ยนแปลงผู้ทำสัญญาหลัก
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("SetMainAgreementOwnerAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementOwnerDTO))]
        public async Task<IActionResult> SetMainAgreementOwnerAsync([FromQuery]Guid id)
        {
            var result = await AgreementOwnerService.SetMainAgreementOwnerAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// การแก้ไขข้อมูลผู้ทำสัญญา
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EditAgreementOwnerAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementOwnerDTO))]
        public async Task<IActionResult> EditAgreementOwnerAsync([FromQuery]Guid id,[FromBody]AgreementOwnerDTO input)
        {
            var result = await AgreementOwnerService.EditAgreementOwnerAsync(id, input);
            return Ok(result);
        }

    }
}
