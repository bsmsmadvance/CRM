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
    public class CommissionHighRiseSaleVeiwController : BaseController
    {
        private ICommissionHighRiseSaleVeiwService CommissionHighRiseSaleVeiwService;
        private readonly DatabaseContext DB;

        public CommissionHighRiseSaleVeiwController(ICommissionHighRiseSaleVeiwService service, DatabaseContext db)
        {
            this.CommissionHighRiseSaleVeiwService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการผลการคำนวณ Commission ขาย (โครงการแนวสูง)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CommissionHighRiseSaleVeiwDTO>))]
        public async Task<IActionResult> GetCommissionHighRiseSaleVeiwList([FromQuery]CommissionHighRiseSaleVeiwFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CommissionHighRiseSaleVeiwSortByParam sortByParam)
        {
            try
            {
                var result = await CommissionHighRiseSaleVeiwService.GetCommissionHighRiseSaleVeiwListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthHighRiseSale);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
