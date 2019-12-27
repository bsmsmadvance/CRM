using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่าน QR Code")]
    [Table("PaymentQRCode", Schema = Schema.FINANCE)]
    public class PaymentQRCode : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("บัญชีที่โอนเข้า")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }
        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

    }
}
