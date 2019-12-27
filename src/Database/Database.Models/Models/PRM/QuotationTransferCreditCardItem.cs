using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นค่าธรรมเนียมบัตรในใบเสนอราคา")]
    [Table("QuotationTransferCreditCardItem", Schema = Schema.PROMOTION)]
    public class QuotationTransferCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอนในใบเสนอราคา")]
        public Guid QuotationTransferPromotionID { get; set; }
        [ForeignKey("QuotationTransferPromotionID")]
        public QuotationTransferPromotion QuotationTransferPromotion { get; set; }

        [Description("ผูกสิ่งของ Master")]
        public Guid? MasterTransferCreditCardItemID { get; set; }
        [ForeignKey("MasterTransferCreditCardItemID")]
        public MasterTransferCreditCardItem MasterTransferCreditCardItem { get; set; }
    }
}
