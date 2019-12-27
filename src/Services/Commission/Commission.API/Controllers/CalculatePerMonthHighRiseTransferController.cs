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
    public class CalculatePerMonthHighRiseTransferController : BaseController
    {
        private ICalculatePerMonthHighRiseTransferService CalculatePerMonthHighRiseTransferService;
        private readonly DatabaseContext DB;

        public CalculatePerMonthHighRiseTransferController(ICalculatePerMonthHighRiseTransferService service, DatabaseContext db)
        {
            this.CalculatePerMonthHighRiseTransferService = service;
            this.DB = db;
        }

        /// <summary>
        ///  ดึงรายการ Commission โอน โครงการแนวสูง 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CalculatePerMonthHighRiseTransferDTO>))]
        public async Task<IActionResult> GetCalculatePerMonthHighRiseTransferList([FromQuery]CalculatePerMonthHighRiseTransferFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CalculatePerMonthHighRiseTransferSortByParam sortByParam)
        {
            try
            {
                var result = await CalculatePerMonthHighRiseTransferService.GetCalculatePerMonthHighRiseTransferListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CalculatePerMonthHighRiseTransfers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// คำนวณ Commission โอน โครงการแนวสูง 
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <param name="CalculateUserID"></param>
        /// <returns></returns>
        [HttpPost("CalculatePerMonthHighRiseTransfer")]
        public IActionResult CalculatePerMonthHighRiseTransfer([FromQuery]Guid? ProjectID, [FromQuery]DateTime? CalculateMonth, [FromQuery]Guid? CalculateUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = CalculatePerMonthHighRiseTransferService.CalculatePerMonthHighRiseTransfer(ProjectID, CalculateMonth, CalculateUserID);
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
        [HttpPost("ApproveCalculatePerMonthHighRiseTransfer")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthHighRiseTransferDTO))]
        public async Task<IActionResult> ApproveCalculatePerMonthHighRiseTransfer([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthHighRiseTransferService.ApproveCalculatePerMonthHighRiseTransferAsync(id, ApproveUserID);
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
        [HttpPost("CancelApproveCalculatePerMonthHighRiseTransfer")]
        [ProducesResponseType(200, Type = typeof(CalculatePerMonthHighRiseTransferDTO))]
        public async Task<IActionResult> CancelApproveCalculatePerMonthHighRiseTransfer([FromQuery]Guid id, [FromQuery]Guid? ApproveUserID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CalculatePerMonthHighRiseTransferService.CancelApproveCalculatePerMonthHighRiseTransferAsync(id, ApproveUserID);
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
