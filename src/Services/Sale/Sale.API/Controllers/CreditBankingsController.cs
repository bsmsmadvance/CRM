using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.SAL;
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
    public class CreditBankingsController : BaseController
    {
        private readonly ICreditBankingService CreditBankingService;
        private readonly DatabaseContext DB;

        public CreditBankingsController(ICreditBankingService creditBankingService, DatabaseContext db)
        {
            this.CreditBankingService = creditBankingService;
            this.DB = db;
        }


        /// <summary>
        /// ดึงข้อมูลจองหรือสัญญา (ข้อมูลทั่วไป)  
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [HttpGet("GetAgreementData")]
        [ProducesResponseType(200, Type = typeof(MortgageInfoDTO))]
        public async Task<IActionResult> GetAgreementDataAsync([FromQuery]Guid unitId)
        {
            var result = await CreditBankingService.GetAgreementDataAsync(unitId);
            return Ok(result);
        }

        /// <summary>
        ///  ดึงข้อมูลธนาคารที่ขอสินเชื่อ
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("GetCreditBankingList")]
        [ProducesResponseType(200, Type = typeof(List<CreditBankingDTO>))]
        public async Task<IActionResult> GetCreditBankingListAsync([FromQuery]Guid bookingID)
        {
            var result = await CreditBankingService.GetCreditBankingListAsync(bookingID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลสถานะขอสินเชื่อ  
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("GetCreditBankingType")]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> GetCreditBankingTypeAsync([FromQuery]Guid bookingID)
        {
            var result = await CreditBankingService.GetCreditBankingTypeAsync(bookingID);
            return Ok(result);
        }

        /// <summary>
        /// บันทึกข้อมูลธนาคารขอสินเชื่อ 1รายการ  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateCreditBanking")]
        [ProducesResponseType(200, Type = typeof(CreditBankingDTO))]
        public async Task<IActionResult> CreateCreditBankingAsync([FromBody]CreditBankingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.CreditBankingService.CreateCreditBankingAsync(input);
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
        /// บันทึกข้อมูลประวัติการพิมพ์เอกสารประกอบสินเชื่อ    
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("CreateCreditBankingPrintingHistory")]
        [ProducesResponseType(200, Type = typeof(CreditBankingDTO))]
        public async Task<IActionResult> CreateCreditBankingPrintingHistoryAsync([FromBody]CreditBankingPrintingHistory input )
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.CreditBankingService.CreateCreditBankingPrintingHistoryAsync(input);
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
        /// บันทึกข้อมูลธนาคารขอสินเชื่อหลายรายการ   
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        [HttpPost("CreateManyCreditBankingBank")]
        [ProducesResponseType(200, Type = typeof(List<CreditBankingDTO>))]
        public IActionResult CreateManyCreditBankingBankAsync([FromBody]List<CreditBankingDTO> listInput)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                     this.CreditBankingService.CreateManyCreditBankingBankAsync(listInput);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }


        /// <summary>
        /// แก้ไขข้อมูลธนาคารขอสินเชื่อ     
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("UpdateCreditBanking/{id}")]
        [ProducesResponseType(200, Type = typeof(CreditBankingDTO))]
        public async Task<IActionResult> UpdateCreditBankingAsync([FromRoute] Guid id, [FromBody]CreditBankingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.CreditBankingService.UpdateCreditBankingAsync(id,input);
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
        /// แก้ไขข้อมูลสถานะขอสินเชื่อ       
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateCreditBankingType/{id}")]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> UpdateCreditBankingTypeAsync([FromRoute] Guid id, [FromBody]BookingDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.CreditBankingService.UpdateCreditBankingTypeAsync(id, input);
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
        /// ลบข้อมูลธนาคารขอสินเชื่อ       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditBankingsAsync([FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await CreditBankingService.DeleteCreditBankingsAsync(id);
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

    }
}
