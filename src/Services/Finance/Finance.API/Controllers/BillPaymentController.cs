using Base.DTOs;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using Finance.Services.IService;
using Finance.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BillPaymentController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IBillPaymentService BillPaymentService;
        /// private readonly IConfiguration Configuration;
        public BillPaymentController(IBillPaymentService billPaymentService, DatabaseContext db)
        {
            this.BillPaymentService = billPaymentService;
            this.DB = db;
        }



        /// <summary>
        /// BillPayment/GetBillPaymentList
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetBillPaymentList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BillPaymentHeaderDTO>))]
        public async Task<IActionResult> GetBillPaymentList(BillPaymentHeaderFilter filter, PageParam pageParam, BillPaymentHeaderSortByParam sortByParam)
        {
            var result = await BillPaymentService.GetBillPaymentListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.BillPayments);
        }
        /// <summary>
        /// BillPayment/GetBillPaymentDetail
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetBillPaymentDetail")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BillPaymentHeaderDTO>))]
        public async Task<IActionResult> GetBillPaymentDetail(Guid id, BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {
            var result = await BillPaymentService.GetBillPaymentDetailAsync(id, filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.BillPayments);

        }

        /// <summary>
        /// BillPayment/GetBillPaymentDetail
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetBillPaymentDetailTemp")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BillPaymentHeaderDTO>))]
        public async Task<IActionResult> GetBillPaymentDetailTemp(Guid id, BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {
            var result = await BillPaymentService.GetBillPaymentDetailTempAsync(id, filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.BillPayments);
        }

        /// <summary>
        /// BillPayment/GetWaitingBillPaymentList
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetWaitingBillPaymentList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BillPaymentHeaderDTO>))]
        public async Task<IActionResult> GetWaitingBillPaymentList(BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {
            var result = await BillPaymentService.GetWaitingBillPaymentListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.BillPayments);
        }

        /// <summary>
        /// BillPayment/ImportBillPayment
        /// 
        /// UI: 
        /// </summary>
        [HttpPut("ImportBillPayment")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        public async Task<IActionResult> ImportBillPayment([FromBody]FileWithBoolDTO fileDTO)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BillPaymentService.ImportBillPaymentAsync(fileDTO);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// /BillPayment/UpdateBillPaymentSplit
        /// ID = Headder ID
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpPut("CreateBillPayment")]
        public async Task<IActionResult> CreateBillPayment([FromQuery]Guid ID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BillPaymentService.CreateBillPaymentAsync(ID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// BillPayment/GetWaitingBillPaymentList
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetBillPaymentSplit")]
        [ProducesResponseType(200, Type = typeof(BookingForBillPaymentDTO))]
        public async Task<IActionResult> GetBillPaymentSplit(Guid id)
        {
            var result = await BillPaymentService.GetBillPaymentSplitAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// /BillPayment/UpdateBillPaymentSplit
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpPut("UpdateBillPaymentSplit")]
        public async Task<IActionResult> UpdateBillPaymentSplit([FromBody]BookingForBillPaymentDTO Input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //var result = await BillPaymentService.CreateBillPaymentAsync(id);
                    await BillPaymentService.UpdateBillPaymentSplitAsync(Input);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// /BillPayment/UpdateBillPaymentDetail
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpPut("UpdateBillPaymentDetail")]
        public async Task<IActionResult> UpdateBillPaymentDetail([FromBody]BillPaymentHeaderDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BillPaymentService.UpdateBillPaymentDetailAsync(input);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// /BillPayment/UpdateBillPaymentDetailTemp
        /// 
        /// UI: https:///projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpPut("UpdateBillPaymentDetailTemp")]
        public async Task<IActionResult> UpdateBillPaymentDetailTemp([FromBody]BillPaymentHeaderDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BillPaymentService.UpdateBillPaymentDetailTempAsync(input);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

    }
}
