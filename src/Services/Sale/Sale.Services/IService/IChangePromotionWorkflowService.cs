using Base.DTOs.SAL;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services.IService
{
    public interface IChangePromotionWorkflowService
    {
        /// <summary>
        /// ดึงข้อมูลเปลี่ยนแปลงโปรโมชัน
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        Task<ChangePromotionWorkflowDTO> GetChangeBookingPromotionAsync(Guid bookingID);

        /// <summary>
        /// ตั้งเรื่องเปลี่ยนแปลงโปรโมชัน
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="changePromotionWorkflow"></param>
        /// <returns></returns>
        Task<ChangePromotionWorkflowDTO> CreateChangePromotionWorkflow(Guid bookingID, ChangePromotionWorkflowDTO changePromotionWorkflow);

        /// <summary>
        /// ตรวจสอบ MinPrice
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="bookingPromotion"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangePromotionAsync(Guid bookingID, UnitInfoBookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses);
    }
}
