using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การรับเงิน")]
    [Table("Payment", Schema = Schema.FINANCE)]
    public class Payment : BaseEntity
    {
        [Description("ผูกข้อมูลใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }
        [Description("บันทึกข้อความ")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string AttachFile { get; set; }
        [Description("วันที่รับเงิน")]
        public DateTime ReceiveDate { get; set; }
        [Description("จำนวนเงินทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        public Guid? ChangeUnitWorkflowID { get; set; }
        [ForeignKey("ChangeUnitWorkflowID")]
        public SAL.ChangeUnitWorkflow ChangeUnitWorkflow { get; set; }

        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }
    }
}
