using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นขาย (ค่าธรรมเนียมบัตรเครดิต)")]
    [Table("BookingCreditCardItem", Schema = Schema.PROMOTION)]
    public class BookingCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขาย")]
        public Guid BookingPromotionID { get; set; }
        [ForeignKey("BookingPromotionID")]
        public BookingPromotion BookingPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterBookingCreditCardItemID { get; set; }
        [ForeignKey("MasterBookingCreditCardItemID")]
        public MasterBookingCreditCardItem MasterBookingCreditCardItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationBookingCreditCardItemID { get; set; }
        [ForeignKey("QuotationBookingPromotionFreeItemID")]
        public QuotationBookingPromotionFreeItem QuotationBookingCreditCardItem { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
    }
}
