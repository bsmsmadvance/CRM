using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LET
{
    [Description("จดหมายเตือนชำระเงินดาวน์")]
    [Table("DownPaymentLetter", Schema = Schema.LETTER)]
    public class DownPaymentLetter : BaseEntity
    {
        [Description("ผูกกับสัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("จำนวนงวดที่เหลือ")]
        public int RemainDownPeriodCount { get; set; }
        [Description("จำนวนเงินที่เหลือทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal TotalRemainAmount { get; set; }
        [Description("งวดที่แจ้งเตือน")]
        public int RemainDownPeriod { get; set; }
        [Description("รูปแบบจดหมาย")]
        public string LetterType { get; set; }
        [Description("สถานะจดหมาย")]
        public string LetterStatus { get; set; }
        [Description("วันที่ตอบรับ")]
        public DateTime? ResponseDate { get; set; }
        [Description("บันทึกข้อความ")]
        public string Remark { get; set; }
        //เลขที่พัสดุ
        [Description("เลขที่พัสดุ")]
        public string PostTrackingNo { get; set; }

    }
}
