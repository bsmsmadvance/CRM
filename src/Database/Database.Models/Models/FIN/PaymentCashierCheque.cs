using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.USR;

namespace Database.Models.FIN
{
    [Description("การรับเงินผ่านแคชเชียร์เช็ค")]
    [Table("PaymentCashierCheque", Schema = Schema.FINANCE)]
    public class PaymentCashierCheque : BaseEntity
    {
        [Description("ผูกช่องทางการชำระเงิน")]
        public Guid PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("วันที่หน้าเช็ค")]
        public DateTime ChequeDate { get; set; }

        [Description("เลขที่เช็ค")]
        [MaxLength(100)]
        public string ChequeNo { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม")]
        public decimal Fee { get; set; }

        [Description("สั่งจ่ายให้บริษัท")]
        public Guid? PayToCompanyID { get; set; }
        [ForeignKey("PayToCompanyID")]
        public MST.Company PayToCompany { get; set; }
        [Description("สั่งจ่ายผิดบริษัท")]
        public bool IsWrongCompany { get; set; }
        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }
        [Description("สาขาธนาคาร")]
        public Guid? BankBranchID { get; set; }
        [ForeignKey("BankBranchID")]
        public MST.BankBranch BankBranch { get; set; }

        //สถานะตรวจสอบค่าธรรมเนียม
        [Description("สถานะตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public bool IsFeeConfirm { get; set; }

        [Description("วันที่ ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public DateTime? FeeConfirmDate { get; set; }

        [Description("ผู้ตรวจสอบค่าธรรมเนียม ก่อนการนำฝาก")]
        public Guid? FeeConfirmByUserID { get; set; }
        [ForeignKey("FeeConfirmByUserID")]
        public User FeeConfirmByUser { get; set; }

        [Column(TypeName = "Money")]
        [Description("เปอร์เซ็นต์ธรรมเนียม")]
        public decimal FeePercent { get; set; }

        [Column(TypeName = "Money")]
        [Description("ค่าธรรมเนียม (หลัง Vat)")]
        public decimal FeeIncludingVat { get; set; }
    }
}
