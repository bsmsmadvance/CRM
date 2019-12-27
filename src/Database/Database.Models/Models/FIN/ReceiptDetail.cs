using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("รายละเอียด ใบเสร็จรับเงินตัวจริง")]
    [Table("ReceiptDetail", Schema = Schema.FINANCE)]
    public class ReceiptDetail : BaseEntity
    {
        public Guid ReceiptHeaderID { get; set; }
        [ForeignKey("ReceiptHeaderID")]
        public FIN.ReceiptHeader ReceiptHeader { get; set; }

        [Description("รับชำระเงินค่าอะไร")]
        public Guid? PaymentItemID { get; set; }
        [ForeignKey("PaymentItemID")]
        public PaymentItem PaymentItem { get; set; }

        [Description("รายละเอียด")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Description("รายละเอียด (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string DescriptionEN { get; set; }

        [Description("จำนวนเงินที่ชำระ")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

    }
}
