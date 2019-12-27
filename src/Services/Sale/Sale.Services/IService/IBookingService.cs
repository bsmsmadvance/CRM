using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;

namespace Sale.Services
{
    public interface IBookingService
    {
        /// <summary>
        /// ดึงรายการจอง
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362366306/preview
        /// </summary>
        /// <returns></returns>
        Task<BookingListPaging> GetBookingListAsync(BookingListFilter filter, PageParam pageParam, BookingListSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลใบจอง
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BookingDTO> GetBookingAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูล Price List ของใบจอง
        /// </summary>
        /// <param name="bookingID">ID ใบจอง</param>
        /// <returns></returns>
        Task<BookingPriceListDTO> GetPriceListAsync(Guid bookingID);

        /// <summary>
        /// ดึงข้อมูลโปรโมชันก่อนขาย ของใบจอง
        /// </summary>
        /// <param name="bookingID">ID ใบจอง</param>
        /// <returns></returns>
        Task<BookingPreSalePromotionDTO> GetBookingPreSalePromotionAsync(Guid bookingID);

        /// <summary>
        /// ดึงข้อมูลโปรโมชัน ของใบจอง
        /// </summary>
        /// <param name="bookingID">ID ใบจอง</param>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        Task<BookingPromotionDTO> GetBookingPromotionAsync(Guid bookingID, BookingPromotionFilter filter = null);

        /// <summary>
        /// ดึงข้อมูลค่าใช้จ่าย
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<List<BookingPromotionExpenseDTO>> GetPromotionExpensesAsync(Guid bookingID);

        /// <summary>
        /// แก้ไขใบจอง
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="booking"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<BookingDTO> UpdateBookingAsync(Guid bookingID, BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses, bool isBookingNo = true, bool isMinPriceWorkflow = true);

        /// <summary>
        /// ดึง Form ยกเลิกใบจอง
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<CancelMemoDTO> GetCancelMemoFormAsync(Guid bookingID);

        /// <summary>
        /// ยกเลิกใบจอง
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task CancelBookingAsync(Guid bookingID, CancelMemoDTO input, Guid userID);

        /// <summary>
        /// ตรวสอบ Min Price Workflow ที่ค้างอยู่ (Recall)
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<BooleanResult> IsWaitingMinPriceApproveAsync(Guid bookingID);

        /// ลบใบจอง
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task DeleteBookingAsync(Guid bookingID);

        /// <summary>
        /// ตรวจสอบ Min Price
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="booking"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses);

        /// <summary>
        /// ยืนยันใบจอง
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task ConfirmBookingAsync(List<BookingListDTO> input, Guid? userID);

        /// <summary>
        /// สร้างใบจองจากระบบอื่น
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="priceList"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<BookingDTO> CreateBookingAsync(BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses);
    }
}
