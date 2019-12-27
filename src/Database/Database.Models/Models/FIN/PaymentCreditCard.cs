using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.MST;
using Database.Models.USR;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่านบัตรเครดิต")]
    [Table("PaymentCreditCard", Schema = Schema.FINANCE)]
    public class PaymentCreditCard : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("เป็นบัตรต่างประเทศหรือไม่")]
        public bool IsForeignCreditCard { get; set; }

        [Column(TypeName = "Money")]
        [Description("เปอร์เซ็นต์ธรรมเนียม")]
        public decimal FeePercent { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม")]
        public decimal Fee { get; set; }
        [Description("ภาษีมูลค่าเพิ่ม (จาก BO Configuration)")]
        public double Vat { get; set; }
        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม (หลัง Vat)")]
        public decimal FeeIncludingVat { get; set; }
        [Description("เลขที่บัตร")]
        [MaxLength(50)]
        public string CardNo { get; set; }
        //รูดเต็ม หรือ ผ่อน
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน)")]
        public Guid? CreditCardPaymentTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardPaymentTypeMasterCenterID")]
        public MST.MasterCenter CreditCardPaymentType { get; set; }
        //VISA, MASTER, JCB
        [Description("ประเภทบัตร (Visa, Master, JCB)")]
        public Guid? CreditCardTypeMasterCenterID { get; set; }
        [ForeignKey("CreditCardTypeMasterCenterID")]
        public MST.MasterCenter CreditCardType { get; set; }
        [Description("ธนาคารเจ้าของบัตร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("เครื่องรูดบัตร")]
        public Guid? EDCID { get; set; }
        [ForeignKey("EDCID")]
        public EDC EDC { get; set; }
        [Description("ผิดบัญชี")]
        public bool IsWrongAccount { get; set; }

        //สถานะตรวจสอบค่าธรรมเนียม
        [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public bool IsFeeConfirm { get; set; }

        [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public DateTime? FeeConfirmDate { get; set; }

        [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public Guid? FeeConfirmByUserID { get; set; }
        [ForeignKey("FeeConfirmByUserID")]
        public User FeeConfirmByUser { get; set; }
    }
}
