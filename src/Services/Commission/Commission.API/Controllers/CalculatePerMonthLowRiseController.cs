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
    public class CalculatePerMonthLowRiseController : BaseController
    {
        private ICalculatePerMonthLowRiseService CalculatePerMonthLowRiseService;
        private readonly DatabaseContext DB;

        public CalculatePerMonthLowRiseController(ICalculatePerMonthLowRiseService service, DatabaseContext db)
        {
            this.CalculatePerMonthLowRiseService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการ Commission ขาย/โอน โครงการแนวราบ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CalculatePerMonthLowRiseDTO>))]
        public async Task<IActionResult> GetCalculatePerMonthLowRiseList([FromQuery]CalculatePerMonthLowRiseFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CalculatePerMonthLowRiseSortByParam sortByParam)
        {
            try
            {
                var result = await CalculatePerMonthLowRiseService.GetCalculatePerMonthLowRiseListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthLowRises);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// คำนวณ Commission ขาย/โอน โครงการแนวราบ
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <param name="CalculateUserID"></param>
        /// <returns></returns>
        [HttpPost("CalculatePerMonthLowRise")]
        public IActionResult CalculatePerMonthLowRise([FromQuery]Guid? ProjectID, [FromQuery]DateTime? CalculateMonth, [FromQuery]Guid? CalculateUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = CalculatePerMonthLowRiseService.CalculatePerMonthLowRise(ProjectID, CalculateMonth, CalculateUserID);
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
        /// Approve Commission โอน โครงการแนวสูง
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// <returns></returns>
        [HttpPost("ApproveCalculatePerMonthLowRise")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthLowRiseDTO))]
        public async Task<IActionResult> ApproveCalculatePerMonthLowRise([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthLowRiseService.ApproveCalculatePerMonthLowRiseAsync(id, ApproveUserID);
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
        /// Cancel Approve Commission โอน โครงการแนวสูง
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// <returns></returns>
        [HttpPost("CancelApproveCalculatePerMonthLowRise")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthLowRiseDTO))]
        public async Task<IActionResult> CancelApproveCalculatePerMonthLowRise([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthLowRiseService.CancelApproveCalculatePerMonthLowRiseAsync(id, ApproveUserID);
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
