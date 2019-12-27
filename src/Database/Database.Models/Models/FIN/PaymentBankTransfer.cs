using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงินโอนผ่านธนาคาร")]
    [Table("PaymentBankTransfer", Schema = Schema.FINANCE)]
    public class PaymentBankTransfer : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("บัญชีธนาคารที่โอนเข้า")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }
    }
}
