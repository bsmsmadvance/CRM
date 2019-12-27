using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นโอน")]
    [Table("TransferPromotion", Schema = Schema.PROMOTION)]
    public class TransferPromotion : BaseEntity
    {

        [Description("ใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("โปรโมชั่น")]
        public Guid? MasterTransferPromotionID { get; set; }
        [ForeignKey("MasterTransferPromotionID")]
        public MasterTransferPromotion MasterPromotion { get; set; }

        [Description("เลขที่โปรโมชั่นโอน")]
        [MaxLength(100)]
        public string TransferPromotionNo { get; set; }

        [Description("โอนกรรมสิทธิ์ภายในวันที่...")]
        public DateTime? TransferDateBefore { get; set; }
        [Description("รวมมูลค่าโปรโมชั่น")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
        [Description("ส่วนลดวันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }
        [Description("รวมใช้ Budget Promotion")]
        [Column(TypeName = "Money")]
        public decimal BudgetAmount { get; set; }
        [Description("ผู้แนะนำ")]
        public Guid? PresentByUserID { get; set; }
        [ForeignKey("PresentByUserID")]
        public USR.User PresentByUser { get; set; }

        [Description("หมายเหตุโปรโมชั่นโอน")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("ปลดล็อคส่วนลด ณ​ วันโอน (โดยการเงิน)")]
        public bool IsUnlockedTransferDiscount { get; set; }
        [Description("ผู้ปลดล็อคส่วนลด ณ​ วันโอน")]
        public Guid? UnlockedTransferDiscountByUserID { get; set; }
        [ForeignKey("UnlockedTransferDiscountByUserID")]
        public USR.User UnlockedTransferDiscountBy { get; set; }
        [Description("วันที่ปลดล็อคส่วนลด ณ​ วันโอน")]
        public DateTime? UnlockedTransferDiscountDate { get; set; }

        [Description("ปลดล็อคส่วนลด ณ​ วันโอน มากกว่า 3% ของราคาขายสุทธิ (โดยการเงิน)")]
        public bool IsUnlocked3PercentTransferDiscount { get; set; }
        [Description("ผู้ปลดล็อคส่วนลด ณ​ วันโอน มากกว่า 3% ของราคาขายสุทธิ")]
        public Guid? Unlocked3PercentTransferDiscountByUserID { get; set; }
        [ForeignKey("Unlocked3PercentTransferDiscountByUserID")]
        public USR.User Unlocked3PercentTransferDiscountBy { get; set; }
        [Description("วันที่ปลดล็อคส่วนลด ณ​ วันโอน มากกว่า 3% ของราคาขายสุทธิ")]
        public DateTime? Unlocked3PercentTransferDiscountDate { get; set; }

        [Description("ฟรีค่าจดจำนอง")]
        public bool IsFreeMortgageCharge { get; set; }

        [Description("Active หรือไม่")]
        public bool IsActive { get; set; }
        [Description("Flow เปลี่ยนแปลงโปร")]
        public Guid? ChangePromotionWorkflowID { get; set; }
        [ForeignKey("ChangePromotionWorkflowID")]
        public ChangePromotionWorkflow ChangePromotionWorkflow { get; set; }
    }
}
