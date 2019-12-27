using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Base.DTOs.SAL;
using Promotion.Params.Outputs;

namespace Promotion.Services.IService
{
    public interface ITransferPromotionService
    {
        /// <summary>
        /// ดึงข้อมูลเบื้องต้นและราคาโปรโมชั่นส่งเสริมการโอน/โครงการ/ลูกค้า/พนักงานขาย
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<TransferPromotionDTO> GetTransferPromotionDataAsync(Guid transferPromotionId);

        /// <summary>
        /// ดึงข้อมูลเบื้องต้นและราคาโปรโมชั่นส่งเสริมการโอน/โครงการ/ลูกค้า/พนักงานขาย  (ก่อนตั้งเรื่อง)
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<TransferPromotionDTO> GetTransferPromotionDrafDataAsync(Guid agreementId);

        /// <summary>
        /// ดึงข้อมูลรายการโปรโมชั่นส่งเสริมการโอน
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<List<TransferPromotionItemDTO>> GetTransferPromotionItemListAsync(Guid transferPromotionId);

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<List<TransferPromotionExpenseDTO>> GetTransferPromotionExpenseListAsync(Guid transferPromotionId);

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย (ก่อนตั้งเรื่อง)
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<List<TransferPromotionExpenseDTO>> GetTransferPromotionExpensesDraftAsync(Guid agreementId);

        /// <summary>
        /// ขออนุมัติการเสนอโปรโมชั่นโอน
        /// </summary>
        /// <param name="transferPromotionDTO"></param>
        /// <param name="transferPromotionItemDTO"></param>
        /// <param name="transferPromotionFreeItemDTO"></param>
        /// <param name="transferPromotionExpenseDTO"></param>
        /// <param name="transferCreditCardItemDTO"></param>
        /// <returns></returns>
        Task<TransferPromotionDTO> CreateTransferPromotionDataAsync(TransferPromotionDTO input, List<TransferPromotionExpenseDTO> expenses);

        /// <summary>
        /// ปลดล๊อคส่วนลด ณ วันโอน
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TransferPromotionDTO> UpdateAllowTransferDiscountAsync(Guid transferPromotionId,TransferPromotionDTO input);

        /// <summary>
        /// ปลดล๊อคส่วนลดมากกว่า 3%
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TransferPromotionDTO> UpdateAllowTransferDiscountOver3PercentAsync(Guid transferPromotionId, TransferPromotionDTO input);

        /// <summary>
        /// ตรวจสอบ Min Price
        /// </summary>
        /// <param name="transferPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(TransferPromotionDTO transferPromotion, List<TransferPromotionExpenseDTO> expenses);

        /// <summary>
        /// ตรวสอบ Min Price Workflow ที่ค้างอยู่ (Recall)
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        Task<BooleanResult> IsWaitingMinPriceApproveAsync(Guid transferPromotionId);
    }
}
