using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationBookingPromotionItem", Schema = Schema.PROMOTION)]
    public class QuotationBookingPromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationBookingPromotionID { get; set; }
        [ForeignKey("QuotationBookingPromotionID")]
        public QuotationBookingPromotion QuotationBookingPromotion { get; set; }

        [Description("ผูกสิ่งของ")]
        public Guid? MasterBookingPromotionItemID { get; set; }
        [ForeignKey("MasterBookingPromotionItemID")]
        public MasterBookingPromotionItem MasterPromotionItem { get; set; }


        [Description("จำนวน")]
        public int Quantity { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainQuotationBookingPromotionID { get; set; }

    }
}
