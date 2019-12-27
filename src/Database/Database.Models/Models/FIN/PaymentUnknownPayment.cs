using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("กลับรายการ เงินโอนไม่ทราบผู้โอน")]
    [Table("PaymentUnknownPayment", Schema = Schema.FINANCE)]
    public class PaymentUnknownPayment : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ID เงินโอนไม่ทราบผู้โอน")]
        public Guid? UnknownPaymentID { get; set; }
        [ForeignKey("UnknownPaymentID")]
        public UnknownPayment UnknownPayment { get; set; }

        [Description("หมายเหตุยกเลิก")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }
    }
}
