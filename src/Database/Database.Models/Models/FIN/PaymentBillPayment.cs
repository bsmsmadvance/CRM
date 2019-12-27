using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่าน Bill Payment")]
    [Table("PaymentBillPayment", Schema = Schema.FINANCE)]
    public class PaymentBillPayment : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ผูกข้อมูลรายการผลการชำระเงิน")]
        public Guid? BillPaymentDetailID { get; set; }
        [ForeignKey("BillPaymentDetailID")]
        public BillPaymentDetail BillPaymentDetail { get; set; }

        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

    }
}
