using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Sale.Params.Inputs;

namespace Sale.Services
{
    public interface IChangeUnitWorkflowService
    {
        //ย้ายแปลงจอง > PriceList > LCM อนุมัติตั้งเรื่อง > Min Price
        //ย้ายแปลงสัญญา > PriceList > LCM อนุมัติตั้งเรื่อง > Min Price > LC Upload File (optional) > นิติกรรม

        /// <summary>
        /// สร้าง Workflow การย้ายแปลงจอง
        /// UI: https://projects.invisionapp.com/d/?origin=v7?origin=v7#/console/17482068/369187180/preview
        /// </summary>
        /// <param name="fromBookingID"></param>
        /// <param name="toUnitID"></param>
        /// <returns></returns>
        Task<BookingChangeUnitWorkflowDTO> CreateBookingChangeUnitWorkflowAsync(Guid fromBookingID, Guid toUnitID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, QuotationTransferPromotionDTO transferPromotion, List<QuotationPromotionExpenseDTO> expenses, MinPriceBudgetReasonInput minpriceReason = null, Guid? userID = null);

        /// <summary>
        /// ดึงข้อมูลการตั้งเรื่องย้ายแปลงจองในปัจจุบันของใบจองใบนี้
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<BookingChangeUnitWorkflowDTO> GetBookingChangeUnitWorkflowAsync(Guid bookingID, Guid? userID = null);

        /// <summary>
        /// อนุมัติตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <returns></returns>
        Task BookingRequestApproveAsync(Guid changeUnitWorkflowID, Guid? userID = null);

        /// <summary>
        /// ไม่อนุมัติตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task BookingRequestRejectAsync(Guid changeUnitWorkflowID, RejectParam input, Guid? userID = null);

        /// <summary>
        /// ยกเลิกอนุมัติย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CancelChangeUnitAsync(Guid changeUnitWorkflowID);

        /// <summary>
        /// อนุมัติย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <returns></returns>
        Task<BookingChangeUnitWorkflowDTO> AgreementApproveAsync(Guid changeUnitWorkflowID);

        /// <summary>
        /// ไม่อนุมัติย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookingChangeUnitWorkflowDTO> AgreementRejectAsync(Guid changeUnitWorkflowID, RejectParam input);

        /// <summary>
        /// ดึงรายการไฟล์แนบของการตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkflowID"></param>
        /// <returns></returns>
        Task<List<ChangeUnitFileDTO>> GetChangeUnitFilesAsync(Guid changeUnitWorkflowID);
        /// <summary>
        /// สร้างไฟล์แนบการตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeUnitFileDTO> AddChangeUnitFileAsync(ChangeUnitFileDTO input);
        /// <summary>
        /// แก้ไขไฟล์แนบการตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitWorkdlowID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ChangeUnitFileDTO> UpdateChangeUnitFileAsync(Guid changeUnitWorkdlowID, ChangeUnitFileDTO input);
        /// <summary>
        /// ลบไฟล์แนบการตั้งเรื่องย้ายแปลง
        /// </summary>
        /// <param name="changeUnitFileID"></param>
        /// <returns></returns>
        Task DeleteChangeUnitFileAsync(Guid changeUnitFileID);
    }
}
