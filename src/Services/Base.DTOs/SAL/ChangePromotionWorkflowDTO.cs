using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class ChangePromotionWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// ผู้ขอเปลี่ยนแปลง
        /// </summary>
        public USR.UserListDTO RequestByUser { get; set; }
        /// <summary>
        /// ชนิดของโปรโมชั่นที่ขอเปลี่ยนแปลง
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionType { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime ApproveDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// โปรโมชั่นที่ขอเปลี่ยนแปลง
        /// </summary>
        public UnitInfoBookingPromotionDTO BookingPromotion { get; set; }
        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public List<BookingPromotionExpenseDTO> Expenses { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }

        public async static Task<ChangePromotionWorkflowDTO> CreateFromModelAsync(ChangePromotionWorkflow model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new ChangePromotionWorkflowDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestByUser = USR.UserListDTO.CreateFromModel(model.RequestByUser),
                    PromotionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionType),
                    ApproveDate = model.ApproveDate,
                    IsApproved = model.IsApproved
                };

                var bookingPromotion = await DB.BookingPromotions
                    .Include(o => o.MasterPromotion)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.ChangePromotionWorkflowID != model.ID).FirstOrDefaultAsync();
                result.BookingPromotion = await UnitInfoBookingPromotionDTO.CreateFromModelAsync(bookingPromotion, DB);

                result.Expenses = new List<BookingPromotionExpenseDTO>();
                if (bookingPromotion != null)
                {
                    var bookingID = await DB.Bookings.Where(o => o.ID == bookingPromotion.BookingID).Select(o => o.ID).FirstAsync();
                    var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID).Select(o => o.ID).FirstAsync();
                    var bookingExpense = await DB.BookingPromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                    result.Expenses = bookingExpense.Select(o => BookingPromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
