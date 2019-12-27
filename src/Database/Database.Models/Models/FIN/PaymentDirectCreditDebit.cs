using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่านการตัดเงินอัตโนมัติ")]
    [Table("PaymentDirectCreditDebit", Schema = Schema.FINANCE)]
    public class PaymentDirectCreditDebit : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ผูกกับรายการผลการตัดเงินอัตโนมัติ")]
        public Guid? DirectCreditDebitExportDetailID { get; set; }
        [ForeignKey("DirectCreditDebitExportDetailID")]
        public DirectCreditDebitExportDetail DirectCreditDebitExportDetail { get; set; }

    }
}
