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
    public class CalculatePerMonthHighRiseSaleController : BaseController
    {
        private ICalculatePerMonthHighRiseSaleService CalculatePerMonthHighRiseSaleService;
        private readonly DatabaseContext DB;

        public CalculatePerMonthHighRiseSaleController(ICalculatePerMonthHighRiseSaleService service, DatabaseContext db)
        {
            this.CalculatePerMonthHighRiseSaleService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการ Commission ขาย โครงการแนวสูง 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CalculatePerMonthHighRiseSaleDTO>))]
        public async Task<IActionResult> GetCalculatePerMonthHighRiseSaleList([FromQuery]CalculatePerMonthHighRiseSaleFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CalculatePerMonthHighRiseSaleSortByParam sortByParam)
        {
            try
            {
                var result = await CalculatePerMonthHighRiseSaleService.GetCalculatePerMonthHighRiseSaleListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthHighRiseSales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// คำนวณ Commission ขาย โครงการแนวสูง 
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <param name="CalculateUserID"></param>
        /// <returns></returns>
        [HttpPost("CalculatePerMonthHighRiseSale")]
        public IActionResult CalculatePerMonthHighRiseSale([FromQuery]Guid? ProjectID, [FromQuery]DateTime? CalculateMonth, [FromQuery]Guid? CalculateUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = CalculatePerMonthHighRiseSaleService.CalculatePerMonthHighRiseSale(ProjectID, CalculateMonth, CalculateUserID);
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
        /// Approve Commission ขาย โครงการแนวสูง
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// <returns></returns>
        [HttpPost("ApproveCalculatePerMonthHighRiseSale")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthHighRiseSaleDTO))]
        public async Task<IActionResult> ApproveCalculatePerMonthHighRiseSale([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthHighRiseSaleService.ApproveCalculatePerMonthHighRiseSaleAsync(id, ApproveUserID);
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
        /// Cancel Approve Commission ขาย โครงการแนวสูง
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// <returns></returns>
        [HttpPost("CancelApproveCalculatePerMonthHighRiseSale")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthHighRiseSaleDTO))]
        public async Task<IActionResult> CancelApproveCalculatePerMonthHighRiseSale([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthHighRiseSaleService.CancelApproveCalculatePerMonthHighRiseSaleAsync(id, ApproveUserID);
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
