using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
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
    public class QuotationsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IQuotationService QuotationService;

        public QuotationsController(DatabaseContext db, IQuotationService quotationService)
        {
            this.DB = db;
            this.QuotationService = quotationService;
        }

        /// <summary>
        /// ดึงรายการ Quotation
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366221/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<QuotationListDTO>))]
        public async Task<IActionResult> GetQuotationListAsync([FromQuery]QuotationListFilter filter, [FromQuery]PageParam pageParam, [FromQuery]QuotationListSortByParam sortByParam)
        {
            var result = await QuotationService.GetQuotationListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.Quotations);
        }

        /// <summary>
        /// ดึงรายละเอียด Quotation
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362366223/preview
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}")]
        [ProducesResponseType(200, Type = typeof(QuotationDTO))]
        public async Task<IActionResult> GetQuotationAsync([FromRoute]Guid quotationID)
        {
            var result = await QuotationService.GetQuotationAsync(quotationID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูล Price List ของใบเสนอราคา (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("PriceLists/Draft")]
        [ProducesResponseType(200, Type = typeof(QuotationPriceListDTO))]
        public async Task<IActionResult> GetPriceListDraftAsync([FromQuery]Guid unitID)
        {
            var result = await QuotationService.GetPriceListDraftAsync(unitID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูล Price List ของใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/PriceLists")]
        [ProducesResponseType(200, Type = typeof(QuotationPriceListDTO))]
        public async Task<IActionResult> GetPriceListAsync([FromRoute]Guid quotationID)
        {
            var result = await QuotationService.GetPriceListAsync(quotationID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรขาย (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("BookingPromotions/Draft")]
        [ProducesResponseType(200, Type = typeof(QuotationBookingPromotionDTO))]
        public async Task<IActionResult> GetBookingPromotionDraftAsync([FromQuery]Guid unitID, [FromQuery]QuotationBookingPromotionFilter filter)
        {
            var results = await QuotationService.GetBookingPromotionDraftAsync(unitID, filter);
            return Ok(results);
        }

        /// <summary>
        /// ดึงข้อมูลโปรขาย
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/BookingPromotions")]
        [ProducesResponseType(200, Type = typeof(QuotationBookingPromotionDTO))]
        public async Task<IActionResult> GetBookingPromotionAsync([FromRoute]Guid quotationID, [FromQuery]QuotationBookingPromotionFilter filter)
        {
            var result = await QuotationService.GetBookingPromotionAsync(quotationID, filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรโอน (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("TransferPromotions/Draft")]
        [ProducesResponseType(200, Type = typeof(QuotationTransferPromotionDTO))]
        public async Task<IActionResult> GetTransferPromotionDraftAsync([FromQuery]Guid unitID, [FromQuery]QuotationTransferPromotionFilter filter)
        {
            var result = await QuotationService.GetTransferPromotionDraftAsync(unitID, filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรโอน
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/TransferPromotions")]
        [ProducesResponseType(200, Type = typeof(QuotationTransferPromotionDTO))]
        public async Task<IActionResult> GetTransferPromotionAsync([FromRoute]Guid quotationID, [FromQuery]QuotationTransferPromotionFilter filter)
        {
            var result = await QuotationService.GetTransferPromotionAsync(quotationID, filter);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลค่าใช้จ่าย (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("PromotionExpenses/Draft")]
        [ProducesResponseType(200, Type = typeof(List<QuotationPromotionExpenseDTO>))]
        public async Task<IActionResult> GetPromotionExpensesDraftAsync(Guid unitID)
        {
            var results = await QuotationService.GetPromotionExpensesDraftAsync(unitID);
            return Ok(results);
        }

        /// <summary>
        /// ดึงข้อมูลค่าใช้จ่าย
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/PromotionExpenses")]
        [ProducesResponseType(200, Type = typeof(List<QuotationPromotionExpenseDTO>))]
        public async Task<IActionResult> GetPromotionExpensesAsync([FromRoute]Guid quotationID)
        {
            var results = await QuotationService.GetPromotionExpensesAsync(quotationID);
            return Ok(results);
        }

        /// <summary>
        /// สร้างใบเสนอราคา
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(QuotationDTO))]
        public async Task<IActionResult> CreateQuotationAsync([FromBody]SaveQuotationInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await QuotationService.CreateQuotationAsync(input.UnitID.Value, input.PriceList, input.BookingPromotion, input.TransferPromotion, input.Expenses);
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
        /// บันทึกใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{quotationID}")]
        [ProducesResponseType(200, Type = typeof(QuotationDTO))]
        public async Task<IActionResult> SaveQuotationAsync([FromRoute]Guid quotationID, [FromBody]SaveQuotationInput input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await QuotationService.SaveQuotationAsync(quotationID, input.PriceList, input.BookingPromotion, input.TransferPromotion, input.Expenses);
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
        /// ลบใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpDelete("{quotationID}")]
        public async Task<IActionResult> DeleteQuotationAsync(Guid quotationID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await QuotationService.DeleteQuotationAsync(quotationID);
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
        /// มีการเปลี่ยนแปลง PriceList หรือไม่
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("IsPriceListChanged")]
        [ProducesResponseType(200, Type = typeof(BooleanResult))]
        public async Task<IActionResult> IsPriceListChangedUsingUnitAsync([FromQuery]Guid unitID, [FromBody]QuotationPriceListDTO input)
        {
            var result = await QuotationService.IsPriceListChangedAsync(unitID, input);
            return Ok(result);
        }

        /// <summary>
        /// เปลี่ยนเป็นใบจอง
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/ConvertToBookingAsync")]
        [ProducesResponseType(200, Type = typeof(BookingDTO))]
        public async Task<IActionResult> ConvertToBookingAsync([FromRoute]Guid quotationID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await QuotationService.ConvertToBookingAsync(quotationID);
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
        /// พิมพ์ใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        [HttpGet("{quotationID}/PrintUrl")]
        [ProducesResponseType(200, Type = typeof(StringResult))]
        public async Task<IActionResult> GetPrintQuotationUrlAsync([FromRoute]Guid quotationID)
        {
            var result = await QuotationService.GetPrintQuotationUrlAsync(quotationID);

            return Ok(result);
        }
        
        /// <summary>
        /// ตรวจสอบ min price
        /// </summary>
        /// <returns></returns>
        [HttpPost("IsMinPriceWorkflow")]
        [ProducesResponseType(200, Type = typeof(MinPriceBudgetWorkflowTypeDTO))]
        public async Task<IActionResult> IsMinPriceChangedAsync([FromBody]SaveQuotationInput input)
        {
            var result = await QuotationService.IsMinPriceChangedAsync(input.UnitID.Value, input.PriceList, input.BookingPromotion,
                input.Expenses);

            return Ok(result);
        }

    }
}
