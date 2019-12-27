using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UnitDocumentController : BaseController
    {
        private DatabaseContext DB;
        private IUnitDocumentService UnitDocumentService;

        public UnitDocumentController(IUnitDocumentService unitDocumentService, DatabaseContext db)
        {
            this.DB = db;
            this.UnitDocumentService = unitDocumentService;
        }

        /// <summary>
        /// ดึงรายการสถานะแปลง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("UnitDocumentList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<UnitDocumentDTO>))]
        public async Task<IActionResult> GetUnitDocumentListAsync([FromQuery]UnitDocumentFilter filter, [FromQuery] PageParam pageParam, [FromQuery]UnitDocumentSortByParam sortByParam)
        {
            var result = await UnitDocumentService.GetUnitDocumentDropdownListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.UnitDocuments);
        }

    }
}
