using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LET
{
    [Description("จดหมายนัดโอน")]
    [Table("TransferLetter", Schema = Schema.LETTER)]
    public class TransferLetter : BaseEntity
    {
        [Description("ผูกสัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        //วันนัดโอนกรรมสิทธิ์
        [Description("วันนัดโอนกรรมสิทธิ์")]
        public DateTime? AppointmentTransferDate { get; set; }
        [Description("สถานะโอน")]
        public string TransferStatus { get; set; }
        [Description("ประเภทจดหมาย")]
        public string LetterType { get; set; }
        //วันที่นัดโอน (จดหมาย)
        [Description("วันที่นัดโอน (จดหมาย)")]
        public DateTime? LetterTransferDate { get; set; }
        [Description("เลขที่พัสดุ")]
        public string PostTrackingNo { get; set; }


    }
}
