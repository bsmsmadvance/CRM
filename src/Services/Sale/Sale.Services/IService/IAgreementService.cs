using Base.DTOs;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public interface IAgreementService
    {
        /// <summary>
        /// ดึงข้อมูลสัญญา
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<AgreementDTO> GetAgreementAsync(Guid agreementID);

        /// <summary>
        /// แปลงเป็นใบสัญญา
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<AgreementDTO> ConvertToAgreementAsync(Guid bookingID);

        /// <summary>
        /// บันทึกใบสัญญา
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<AgreementDTO> CreateAgreementAsync(Guid agreementID);

        /// <summary>
        /// การอัพโหลดเอกสารประกอบการทำสัญญา 
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<List<AgreementFileDTO>> CreateAgreementFileAsync(Guid agreementID, List<FileDTO> input,Guid? userID);

        /// <summary>
        /// การลบไฟล์อัพโหลดเอกสารประกอบการทำสัญญา 
        /// </summary>
        /// <param name="agreementFileID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        void DeleteAgreementFileAsync(Guid agreementFileID, Guid? userID);

        /// <summary>
        /// การดูรายการเอกสารที่อัพโหลด 
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<List<AgreementFileDTO>> GetAgreementFileListAsync(Guid agreementID);

        /// <summary>
        /// ดึงข้อมูลสัญญาโดย Unit
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        Task<AgreementDTO> GetAgreementByUnitAsync(Guid unitID);
        
        /// <summary>
        /// สร้างการแจ้งเตือนการอัพโหลดเอกสารสัญญา  
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        void CreateNotificationAgreementFileAsync(Guid agreementID, string Message,Guid? userID);

        /// <summary>
        /// เก็บข้อมูลจำนวนครั้งที่พิมพ์ 
        /// </summary>
        /// <param name="agreementID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        void CreateAgreementPrintingHistoryDataAsync(Guid agreementID, Guid? userID);

        /// <summary>
        /// การแสดงข้อมูลรายการงวดดาวน์
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<List<AgreementInstallmentDTO>> GetAgreementInstallmentDataAsync(Guid agreementID);

        /// <summary>
        /// การคำนวนงวดเงินดาวน์ตามข้อมูลที่ระบุ
        /// </summary>
        /// <param name="agreementPriceListDTO"></param>
        /// <returns></returns>
        Task<AgreementPriceListDTO> CalculateAgreementInstallmentDataAsync(AgreementPriceListDTO agreementPriceListDTO);

        /// <summary>
        /// การคืนค่างวดดาวน์ตาม Price List
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<AgreementPriceListDTO> GetPriceListDataAsync(Guid agreementID);

        /// <summary>
        /// การระบุข้อมูลเงื่อนไขการชำระเงิน
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns></returns>
        Task<AgreementPriceListDTO> GetAgreementPriceListAsync(Guid agreementID);

        /// <summary>
        /// ดึงรายการสัญญา
        /// Paging, Sort, Filter
        /// </summary>
        /// <returns></returns>
        Task<AgreementListPaging> GetAgreementListAsync(AgreementListFilter filter, PageParam pageParam, AgreementListSortByParam sortByParam);

        /// <summary>
        /// แก้ไขใบสัญญา
        /// </summary>
        /// <param name="agreementID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AgreementDTO> UpdateAgreementAsync(Guid agreementID, AgreementDTO agreement, BookingPriceListDTO priceList, BookingPromotionDTO agreementPromotion, List<BookingPromotionExpenseDTO> agreementExpenses, bool isMinPriceWorkflow = true);
    }
}
