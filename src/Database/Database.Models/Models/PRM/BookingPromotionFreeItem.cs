using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย (ที่ไม่ต้องจัดซื้อ)")]
    [Table("BookingPromotionFreeItem", Schema = Schema.PROMOTION)]
    public class BookingPromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid BookingPromotionID { get; set; }
        [ForeignKey("BookingPromotionID")]
        public BookingPromotion BookingPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterBookingPromotionFreeItemID { get; set; }
        [ForeignKey("MasterBookingPromotionFreeItemID")]
        public MasterBookingPromotionFreeItem MasterBookingPromotionFreeItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationBookingPromotionFreeItemID { get; set; }
        [ForeignKey("QuotationBookingPromotionFreeItemID")]
        public QuotationBookingPromotionFreeItem QuotationBookingPromotionFreeItem { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
    }
}
