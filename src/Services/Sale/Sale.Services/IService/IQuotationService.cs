using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using PagingExtensions;
using Report.Integration;
using Sale.Params.Filters;
using Sale.Params.Outputs;

namespace Sale.Services
{
    public interface IQuotationService
    {
        /// <summary>
        /// ดึงรายการ Quotation
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366221/preview
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        Task<QuotationListPaging> GetQuotationListAsync(QuotationListFilter filter, PageParam pageParam, QuotationListSortByParam sortByParam);

        /// <summary>
        /// ดึงรายละเอียด Quotation
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362366223/preview
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<QuotationDTO> GetQuotationAsync(Guid quotationID);

        /// <summary>
        /// ดึงข้อมูล Price List ของใบเสนอราคา (Draft)
        /// </summary>
        /// <param name="unitID">ID แปลง</param>
        /// <returns></returns>
        Task<QuotationPriceListDTO> GetPriceListDraftAsync(Guid unitID);

        /// <summary>
        /// ดึงข้อมูล Price List ของใบเสนอราคา
        /// </summary>
        /// <param name="quotationID">ID ใบเสนอราคา</param>
        /// <returns></returns>
        Task<QuotationPriceListDTO> GetPriceListAsync(Guid quotationID);

        /// <summary>
        /// ดึงข้อมูลโปรขาย (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        Task<QuotationBookingPromotionDTO> GetBookingPromotionDraftAsync(Guid unitID, QuotationBookingPromotionFilter filter = null);

        /// <summary>
        /// ดึงข้อมูลโปรขาย
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<QuotationBookingPromotionDTO> GetBookingPromotionAsync(Guid quotationID, QuotationBookingPromotionFilter filter = null);

        /// <summary>
        /// ดึงข้อมูลโปรโอน (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        Task<QuotationTransferPromotionDTO> GetTransferPromotionDraftAsync(Guid unitID, QuotationTransferPromotionFilter filter = null);

        /// <summary>
        /// ดึงข้อมูลโปรโอน
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<QuotationTransferPromotionDTO> GetTransferPromotionAsync(Guid quotationID, QuotationTransferPromotionFilter filter = null);

        /// <summary>
        /// ดึงข้อมูลค่าใช้จ่าย (Draft)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        Task<List<QuotationPromotionExpenseDTO>> GetPromotionExpensesDraftAsync(Guid unitID);

        /// <summary>
        /// ดึงข้อมูลค่าใช้จ่าย
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<List<QuotationPromotionExpenseDTO>> GetPromotionExpensesAsync(Guid quotationID);

        /// <summary>
        /// สร้างใบเสนอราคา
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="transferPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<QuotationDTO> CreateQuotationAsync(Guid unitID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, QuotationTransferPromotionDTO transferPromotion, List<QuotationPromotionExpenseDTO> expenses);

        /// <summary>
        /// บันทึกใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="transferPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<QuotationDTO> SaveQuotationAsync(Guid quotationID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, QuotationTransferPromotionDTO transferPromotion, List<QuotationPromotionExpenseDTO> expenses);

        /// <summary>
        /// ลบใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task DeleteQuotationAsync(Guid quotationID);

        /// <summary>
        /// มีการเปลี่ยนแปลง PriceList หรือไม่
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BooleanResult> IsPriceListChangedAsync(Guid unitID, QuotationPriceListDTO input);

        /// <summary>
        /// เปลี่ยนเป็นใบจอง
        /// </summary>
        /// <param name="quotationID"></param>
        /// <param name="isPricelistWorkflow"></param>
        /// <returns></returns>
        Task<BookingDTO> ConvertToBookingAsync(Guid quotationID, bool isPricelistWorkflow = true);

        /// <summary>
        /// พิมพ์ใบเสนอราคา
        /// </summary>
        /// <param name="quotationID"></param>
        /// <returns></returns>
        Task<ReportResult> GetPrintQuotationUrlAsync(Guid quotationID);

        /// <summary>
        /// มีการเปลี่ยนแปลง PriceList หรือไม่ กรณีส่ง DTO
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        Task<BooleanResult> IsWaitingPriceListChangedAsync(QuotationPriceListDTO priceList);

        /// <summary>
        /// ตรวจสอบ Minprice Workflow
        /// </summary>
        /// <param name="quotation"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(Guid unitID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, List<QuotationPromotionExpenseDTO> expenses);
    }
}
