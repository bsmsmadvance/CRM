using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขาย")]
    [Table("BookingPromotion", Schema = Schema.PROMOTION)]
    public class BookingPromotion : BaseEntity
    {

        [Description("ใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("โปรโมชั่น")]
        public Guid? MasterBookingPromotionID { get; set; }
        [ForeignKey("MasterBookingPromotionID")]
        public MasterBookingPromotion MasterPromotion { get; set; }

        [Description("เลขที่โปรโมชั่นขาย")]
        [MaxLength(100)]
        public string BookingPromotionNo { get; set; }

        [Description("โอนกรรมสิทธิ์ภายในวันที่...")]
        public DateTime? TransferDateBefore { get; set; }
        [Description("รวมมูลค่าโปรโมชั่น")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
        [Description("ส่วนลดวันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }
        [Description("ส่วนลดหน้าสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractDiscount { get; set; }
        [Description("ส่วนลด FGF")]
        [Column(TypeName = "Money")]
        public decimal? FGFDiscount { get; set; }
        [Description("รวมใช้ Budget Promotion")]
        [Column(TypeName = "Money")]
        public decimal BudgetAmount { get; set; }
        [Description("ผู้แนะนำ")]
        public Guid? PresentByUserID { get; set; }
        [ForeignKey("PresentByUserID")]
        public USR.User PresentByUser { get; set; }

        [Description("ขั้นตอนของโปรขาย (จอง, ทำสัญญา)")]
        public Guid? BookingPromotionStageMasterCenterID { get; set; }
        [ForeignKey("BookingPromotionStageMasterCenterID")]
        public MST.MasterCenter BookingPromotionStage { get; set; }
        [Description("Active หรือไม่")]
        public bool IsActive { get; set; }
        [Description("Flow เปลี่ยนแปลงโปร")]
        public Guid? ChangePromotionWorkflowID { get; set; }
        [ForeignKey("ChangePromotionWorkflowID")]
        public ChangePromotionWorkflow ChangePromotionWorkflow { get; set; }

    }
}
