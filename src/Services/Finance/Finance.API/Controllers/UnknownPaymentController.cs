using Base.DTOs.FIN;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Finance.Params.Filters;
using Finance.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UnknownPaymentController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IUnknownPaymentService UnknownPaymentService;
        public UnknownPaymentController(IUnknownPaymentService unknownPaymentService, DatabaseContext db)
        {
            UnknownPaymentService = unknownPaymentService;
            DB = db;
        }

        /// <summary>
        /// โครงการ filter บริษัท
        /// </summary>
        [HttpGet("ProjectDropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdownListAsync([FromQuery]string displayName = null, [FromQuery]Guid? companyID = null)
        {
            try
            {
                var result = await UnknownPaymentService.GetProjectDropdownListAsync(displayName, companyID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unit ที่จองแล้ว แต่ยังไม่โอน และ filter โครงการ
        /// </summary>
        [HttpGet("SoldUnitDropdowList")]
        [ProducesResponseType(200, Type = typeof(List<SoldUnitDropdownDTO>))]
        public async Task<IActionResult> GetSoldUnitDropdowListAsync([FromQuery]string displayName = null, [FromQuery]Guid? projectID = null)
        {
            try
            {
                var result = await UnknownPaymentService.GetSoldUnitDropdowListAsync(displayName, projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// /UnknowPayment/GetUnknownPaymentAsync
        /// ดึงข้อมูลรายละเอียดเงินตั้งพัก
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdateType"></param>
        [HttpGet("GetUnknownPaymentAsync")]
        [ProducesResponseType(200, Type = typeof(UnknownPaymentDTO))]
        public async Task<IActionResult> GetUnknownPaymentAsync([FromQuery]Guid id)
        {
            var result = await UnknownPaymentService.GetUnknownPaymentAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// /UnknowPayment/GetUnknowPaymentListAsync
        /// ดึงข้อมูลรายละเอียดเงินตั้งพักและการกลับรายการ
        /// </summary>
        [HttpGet("UnknowPaymentList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<UnknownPaymentDTO>))]
        public async Task<IActionResult> GetUnknowPaymentListAsync([FromQuery]UnknownPaymentFilter filter, [FromQuery]PageParam pageParam, [FromQuery]UnknownPaymentSortByParam sortByParam)
        {
            var result = await UnknownPaymentService.GetUnknownPaymentListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.UnknownPayments);
        }

        /// <summary>
        /// /UnknowPayment/CreateUnknownPaymentAsync
        /// เพิ่มเงินตั้งพัก
        /// </summary>
        /// <param name="input">UnknownPaymentDTO</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UnknownPayment))]
        public async Task<IActionResult> CreateUnknownPaymentAsync([FromBody]UnknownPaymentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnknownPaymentService.CreateUnknownPaymentAsync(input);
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

        [HttpGet("ValidateBeforeUpdate")]
        [ProducesResponseType(200, Type = typeof(UnknownPaymentDTO))]
        public async Task<IActionResult> ValidateBeforeUpdateAsync([FromQuery]Guid id, [FromQuery]int UpdateType)
        {
            var result = await UnknownPaymentService.ValidateBeforeUpdateAsync(id, UpdateType);
            return Ok(result);
        }

        /// <summary>
        /// /UnknowPayment/UpdateUnknownPayment
        /// บันทึกการแก้ไข เงินตั้งพัก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        [HttpPut("UpdateUnknownPayment")]
        public async Task<IActionResult> UpdateUnknownPaymentAsync([FromBody]UnknownPaymentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnknownPaymentService.UpdateUnknownPaymentAsync(input);
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
        /// /UnknowPayment/UpdateUnknownPaymentForSAP
        /// บันทึกการแก้ไข เงินตั้งพักข้อมูลฝั่ง SAP
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        [HttpPut("UpdateUnknownPaymentForSAP")]
        public async Task<IActionResult> UpdateUnknownPaymentForSAPAsync([FromBody]UnknownPaymentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnknownPaymentService.UpdateUnknownPaymentForSAPAsync(input);
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
        /// /UnknowPayment/DeleteUnknownPaymentAsync
        /// ลบข้อมูล เงินตั้งพัก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232402/preview
        /// </summary>
        [HttpPut("DeleteUnknownPaymentAsync")]
        public async Task<IActionResult> DeleteUnknownPaymentAsync([FromBody]UnknownPaymentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await UnknownPaymentService.DeleteUnknownPaymentAsync(input);
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
        /// /UnknowPayment/UnknownPaymentForReverse
        /// ดึงข้อมูลรายละเอียดการกลับรายการ
        /// </summary>
        /// <param name="UnknownPaymenID"></param>
        [HttpGet("UnknownPaymentForReverse")]
        [ProducesResponseType(200, Type = typeof(UnknownPaymentDetailDTO))]
        public async Task<IActionResult> GetUnknownPaymentForReverseAsync([FromQuery]Guid id)
        {
            var result = await UnknownPaymentService.GetUnknownPaymentForReverseAsync(id);
            return Ok(result);
        }


    }
}
