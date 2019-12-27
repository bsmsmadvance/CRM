using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นค่าธรรมเนียมบัตรในใบเสนอราคา")]
    [Table("QuotationBookingCreditCardItem", Schema = Schema.PROMOTION)]
    public class QuotationBookingCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นขายในใบเสนอราคา")]
        public Guid QuotationBookingPromotionID { get; set; }
        [ForeignKey("QuotationBookingPromotionID")]
        public QuotationBookingPromotion QuotationBookingPromotion { get; set; }

        [Description("ผูกสิ่ง Master")]
        public Guid? MasterBookingCreditCardItemID { get; set; }
        [ForeignKey("MasterBookingCreditCardItemID")]
        public MasterBookingCreditCardItem MasterBookingCreditCardItem { get; set; }
    }
}
