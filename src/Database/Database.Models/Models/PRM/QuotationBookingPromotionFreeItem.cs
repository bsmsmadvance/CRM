using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationBookingPromotionFreeItem", Schema = Schema.PROMOTION)]
    public class QuotationBookingPromotionFreeItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationBookingPromotionID { get; set; }
        [ForeignKey("QuotationBookingPromotionID")]
        public QuotationBookingPromotion QuotationBookingPromotion { get; set; }

        [Description("ผูกสิ่งของ (ที่ไม่ต้องจัดซื้อ)")]
        public Guid? MasterBookingPromotionFreeItemID { get; set; }
        [ForeignKey("MasterBookingPromotionFreeItemID")]
        public MasterBookingPromotionFreeItem MasterPromotionFreeItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

    }
}
