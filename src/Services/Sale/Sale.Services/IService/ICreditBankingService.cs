using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Database.Models.SAL;
using Sale.Params.Inputs;

namespace Sale.Services
{
    public interface ICreditBankingService
    {
        /// <summary>
        /// ดึงข้อมูลขอสินเชื่อ
        /// </summary>
        /// <param name="BookingId">
        /// เลขที่ใบจอง
        /// </param>
        /// <returns></returns>
        Task<BookingDTO> GetCreditBankingTypeAsync(Guid BookingId);

        /// <summary>
        /// ดึงข้อมูลจองหรือสัญญา (ข้อมูลทั่วไป)  
        /// </summary>
        /// <param name="unitId">
        /// เลขที่ใบจอง
        /// </param>
        /// <returns></returns>
        Task<MortgageInfoDTO> GetAgreementDataAsync(Guid unitId);


        /// <summary>
        /// ดึงข้อมูลธนาคารที่ขอสินเชื่อ
        /// </summary>
        /// <param name="BookingId">
        /// เลขที่ใบจอง
        /// </param>
        /// <returns></returns>
        Task<List<CreditBankingDTO>> GetCreditBankingListAsync(Guid BookingId);

        /// <summary>
        /// บันทึกข้อมูลขอสินเชื่อ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookingDTO> UpdateCreditBankingTypeAsync(Guid bookingId, BookingDTO input);

        /// <summary>
        /// บันทึกข้อมูลธนาคารขอสินเชื่อ 1รายการ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreditBankingDTO> CreateCreditBankingAsync(CreditBankingDTO input);

        /// <summary>
        /// บันทึกข้อมูลธนาคารขอสินเชื่อหลายรายการ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void CreateManyCreditBankingBankAsync(List<CreditBankingDTO> listInput);


        /// <summary>
        /// แก้ไขข้อมูลธนาคารขอสินเชื่อ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreditBankingDTO> UpdateCreditBankingAsync(Guid id, CreditBankingDTO input);

        /// <summary>
        /// ลบข้อมูลหัก Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<CreditBanking> DeleteCreditBankingAsync(Guid id);
        /// <summary>
        /// ลบข้อมูลหัก Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCreditBankingsAsync(Guid id);


        /// <summary>
        /// บันทึกข้อมูลประวัติการพิมพ์เอกสารประกอบสินเชื่อ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreditBankingPrintingHistory> CreateCreditBankingPrintingHistoryAsync(CreditBankingPrintingHistory input);

    }
}
