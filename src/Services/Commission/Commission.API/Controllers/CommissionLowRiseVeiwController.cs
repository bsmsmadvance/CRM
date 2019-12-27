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
    public class CommissionLowRiseVeiwController : BaseController
    {
        private ICommissionLowRiseVeiwService CommissionLowRiseVeiwService;
        private readonly DatabaseContext DB;

        public CommissionLowRiseVeiwController(ICommissionLowRiseVeiwService service, DatabaseContext db)
        {
            this.CommissionLowRiseVeiwService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการผลการคำนวณ Commission ขาย/โอน (โครงการแนวราบ)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CommissionLowRiseVeiwDTO>))]
        public async Task<IActionResult> GetCommissionLowRiseVeiwList([FromQuery]CommissionLowRiseVeiwFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CommissionLowRiseVeiwSortByParam sortByParam)
        {
            try
            {
                var result = await CommissionLowRiseVeiwService.GetCommissionLowRiseVeiwListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthLowRise);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Export Excel
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CommissionMonth"></param>
        /// <returns></returns>
        [HttpGet("CommissionLowRiseVeiw/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportExcelCommissionLowRiseVeiw([FromRoute]Guid? ProjectID, [FromQuery]DateTime? CommissionMonth)
        {
            try
            {
                var result = await CommissionLowRiseVeiwService.ExportExcelCommissionLowRiseAsync(ProjectID, CommissionMonth);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
