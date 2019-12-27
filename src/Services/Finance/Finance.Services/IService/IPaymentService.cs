using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Database.Models.FIN;
using Finance.Services.Common;

namespace Finance.Services
{
    public interface IPaymentService
    {
        /// <summary>
        /// ดึง Form สำหรับชำระเงิน โดยต้องดึงรายการค่าใช้จ่ายต่างๆ ของใบจองนี้ออกมาด้วย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367416/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<PaymentFormDTO> GetPaymentFormAsync(Guid bookingID, PaymentFormType formType = PaymentFormType.Normal, Guid? refID = null, decimal payAmount = 0);
        /// <summary>
        /// Submit การชำระเงิน
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367416/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Guid?> SubmitPaymentFormAsync(Guid bookingID, PaymentFormDTO input);
        /// <summary>
        /// ประวัติการรับชำระเงิน
        /// UI : https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367411/preview
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        Task<List<PaymentHistoryDTO>> GetPaymentHistoryListAsync(Guid bookingID);
        /// <summary>
        /// รายการ PriceList การชำระเเงิน
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<List<PaymentUnitPriceItemDTO>> GetPaymentUnitPriceItemsAsync(Guid bookingID);
        /// <summary>
        /// คำนวณ UnitPrice ใหม่
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="ReceiveDate"></param>
        /// <returns></returns>
        Task RecalculateUnitPriceAsync(Guid bookingID, DateTime? ReceiveDate);
        /// <summary>
        /// สร้าง ReceiptTempHeader
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="paymentID"></param>
        /// <returns></returns>
        Task<ReceiptTempHeader> CreateReciptTempHeaderAsync(Guid bookingID, Guid paymentID);
        /// <summary>
        /// สร้าง ReceiptTempDetail
        /// </summary>
        /// <param name="paymentItems"></param>
        /// <param name="receiptTempHeaderID"></param>
        /// <returns></returns>
        Task CreateReceiptTempDetailAsync(List<PaymentItem> paymentItems, Guid receiptTempHeaderID);
        /// <summary>
        /// ชำระเงิน
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="payAmount"></param>
        /// <param name="paymentID"></param>
        /// <param name="paymentMethodID"></param>
        /// <returns></returns>
        Task<PaymentLists> PayToUnitAsync(Guid bookingID, decimal payAmount, Guid paymentID, Guid paymentMethodID);

    }
}
