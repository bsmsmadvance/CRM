using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย")]
    [Table("BookingPromotionItem", Schema = Schema.PROMOTION)]
    public class BookingPromotionItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid BookingPromotionID { get; set; }
        [ForeignKey("BookingPromotionID")]
        public BookingPromotion BookingPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterBookingPromotionItemID { get; set; }
        [ForeignKey("MasterBookingPromotionItemID")]
        public MasterBookingPromotionItem MasterPromotionItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationBookingPromotionItemID { get; set; }
        [ForeignKey("QuotationBookingPromotionItemID")]
        public QuotationBookingPromotionItem QuotationBookingPromotionItem { get; set; }

        [Description("ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)")]
        public Guid? MainBookingPromotionItemID { get; set; }

        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

    }
}
