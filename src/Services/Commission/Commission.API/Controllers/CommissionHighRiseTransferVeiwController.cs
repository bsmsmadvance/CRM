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
    public class CommissionHighRiseTransferVeiwController : BaseController
    {
        private ICommissionHighRiseTransferVeiwService CommissionHighRiseTransferVeiwService;
        private readonly DatabaseContext DB;

        public CommissionHighRiseTransferVeiwController(ICommissionHighRiseTransferVeiwService service, DatabaseContext db)
        {
            this.CommissionHighRiseTransferVeiwService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการผลการคำนวณ Commission โอน (โครงการแนวสูง)
        /// </summary>
        /// test test
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CommissionHighRiseTransferVeiwDTO>))]
        public async Task<IActionResult> GetCommissionHighRiseTransferVeiwList([FromQuery]CommissionHighRiseTransferVeiwFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CommissionHighRiseTransferVeiwSortByParam sortByParam)
        {
            try
            {
                var result = await CommissionHighRiseTransferVeiwService.GetCommissionHighRiseTransferVeiwListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthHighRiseTransfer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
