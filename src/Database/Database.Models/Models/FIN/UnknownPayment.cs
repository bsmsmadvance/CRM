using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("เงินโอนไม่ทราบผู้โอน")]
    [Table("UnknownPayment", Schema = Schema.FINANCE)]
    public class UnknownPayment : BaseEntity
    {
        [Description("เลขที่ตั้งพัก/เลขที่ PI")]
        public string UnknownPaymentCode { get; set; }

        [Description("บัญชีธนาคารที่รับเงิน")]
        public Guid? BankAccountID { get; set; }

        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ID ของ Booking กรณีที่รู้ว่าเงินนี้เป็นของห้องไหน แต่ต้องการบันทึกลงบัญชีพัก สามารถใช้ Wallet แทนได้แต่ Phase 1 ยังไม่มี Wallet")]
        public Guid? BookingID { get; set; }

        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("วันที่รับเงิน")]
        public DateTime ReceiveDate { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        
        [Description("สถานะรายการ เงินโอนไม่ทราบผู้โอน")]
        public Guid? UnknownPaymentStatusID { get; set; }

        [ForeignKey("UnknownPaymentStatusID")]
        public MST.MasterCenter UnknownPaymentStatus { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("หมายเหตุยกเลิก")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }

        [Description("หมายเหตุรายการด้าน SAP")]
        [MaxLength(1000)]
        public string SAPRemark { get; set; }

    }
}
