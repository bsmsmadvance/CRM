using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Database.Models;
using Finance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService PaymentService;
        private readonly DatabaseContext DB;

        public PaymentsController(IPaymentService paymentService, DatabaseContext db)
        {
            this.DB = db;
            this.PaymentService = paymentService;
        }

        /// <summary>
        /// ดึง Form สำหรับชำระเงิน โดยต้องดึงรายการค่าใช้จ่ายต่างๆ ของใบจองนี้ออกมาด้วย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367416/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="formType"></param>
        /// <param name="refID"></param>
        /// <param name="payAmount"></param>
        /// <returns></returns>
        [HttpGet("PaymentForm")]
        [ProducesResponseType(200, Type = typeof(PaymentFormDTO))]
        public async Task<IActionResult> GetPaymentFormAsync([FromQuery]Guid bookingID, [FromQuery]PaymentFormType formType = PaymentFormType.Normal, [FromQuery]Guid? refID = null, [FromQuery]decimal payAmount = 0)
        {
            var result = await PaymentService.GetPaymentFormAsync(bookingID, formType, refID, payAmount);
            return Ok(result);
        }

        /// <summary>
        /// Submit การชำระเงิน
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367416/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Submit")]
        [ProducesResponseType(200, Type = typeof(PaymentFormDTO))]
        public async Task<IActionResult> SubmitPaymentFormAsync([FromQuery]Guid bookingID, [FromBody]PaymentFormDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await PaymentService.SubmitPaymentFormAsync(bookingID, input);
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
        /// ประวัติการรับชำระเงิน
        /// UI : https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367411/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("Histories")]
        [ProducesResponseType(200, Type = typeof(List<PaymentHistoryDTO>))]
        public async Task<IActionResult> GetPaymentHistoryListAsync([FromQuery]Guid bookingID)
        {
            var results = await PaymentService.GetPaymentHistoryListAsync(bookingID);
            return Ok(results);
        }


        /// <summary>
        /// Price List
        /// UI : https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367412/preview?scrollOffset=10788
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        [HttpGet("UnitPriceItems")]
        [ProducesResponseType(200, Type = typeof(List<PaymentUnitPriceItemDTO>))]
        public async Task<IActionResult> GetPaymentUnitPriceItemsAsync([FromQuery]Guid bookingID)
        {
            var results = await PaymentService.GetPaymentUnitPriceItemsAsync(bookingID);
            return Ok(results);
        }
    }
}
