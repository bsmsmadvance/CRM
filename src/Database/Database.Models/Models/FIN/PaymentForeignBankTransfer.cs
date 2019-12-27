using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงินโอนเงินผ่านธนาคารต่างประเทศ")]
    [Table("PaymentForeignBankTransfer", Schema = Schema.FINANCE)]
    public class PaymentForeignBankTransfer : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal Fee { get; set; }
        [Description("บัญชีที่โอนเข้า")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }
        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

        [Description("ธนาคารที่รับเงินก่อนเข้า AP")]
        public Guid? ForeignBankID { get; set; }
        [ForeignKey("ForeignBankID")]
        public MST.Bank ForeignBank { get; set; }
        [Description("ประเภทการโอนเงินต่างประเทศ")]
        public Guid? ForeignTransferTypeMasterCenterID { get; set; }
        [ForeignKey("ForeignTransferTypeMasterCenterID")]
        public MST.MasterCenter ForeignTransferType { get; set; }
        [Description("IR")]
        [MaxLength(100)]
        public string IR { get; set; }
        [Description("ชื่อผู้โอน")]
        [MaxLength(1000)]
        public string TransferorName { get; set; }
        [Description("ต้องขอ FET หรือไม่")]
        public bool IsRequestFET { get; set; }
        [Description("แจ้งแก้ไข FET")]
        public bool IsNotifyFET { get; set; }
        [Description("ข้อความแจ้งเตือน")]
        [MaxLength(5000)]
        public string NotifyFETMemo { get; set; }

        //ถ้ามาจากเงินโอนไม่ทราบผู้โอน
        [Description("ผูกเงินโอนไม่ทราบผู้โอน (ถ้ามี)")]
        public Guid? UnknownPaymentID { get; set; }
        [ForeignKey("UnknownPaymentID")]
        public UnknownPayment UnknownPayment { get; set; }

    }
}
