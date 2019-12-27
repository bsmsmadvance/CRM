using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models.PRM;

namespace Database.Models.PRM
{
    [Description("รายการโปรโมชั่นโอน (ค่าธรรมเนียมบัตรเครดิต)")]
    [Table("TransferCreditCardItem", Schema = Schema.PROMOTION)]
    public class TransferCreditCardItem : BaseEntity
    {
        [Description("ผูกโปรโมชั่นโอน")]
        public Guid TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }
        [Description("ผูก Promotion Item")]
        public Guid? MasterTransferCreditCardItemID { get; set; }
        [ForeignKey("MasterTransferCreditCardItemID")]
        public MasterTransferCreditCardItem MasterTransferCreditCardItem { get; set; }
        [Description("ผูก Quotation Promotion Item")]
        public Guid? QuotationTransferCreditCardItemID { get; set; }
        [ForeignKey("QuotationTransferPromotionFreeItemID")]
        public QuotationTransferPromotionFreeItem QuotationTransferCreditCardItem { get; set; }

        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
    }
}
