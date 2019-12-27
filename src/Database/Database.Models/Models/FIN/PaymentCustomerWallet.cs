using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่านกระเป๋าเงินลูกค้า")]
    [Table("PaymentCustomerWallet", Schema = Schema.FINANCE)]
    public class PaymentCustomerWallet : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ผูกรายการเดินบัญชีของกระเป๋าเงินลูกค้า")]
        public Guid? CustomerWalletTransactionID { get; set; }
        [ForeignKey("CustomerWalletTransactionID")]
        public CustomerWalletTransaction CustomerWalletTransaction { get; set; }

    }
}
