using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ประวัติการส่งใบเสร็จทาง Email")]
    [Table("ReceiptSendEmailHistory", Schema = Schema.FINANCE)]
    public class ReceiptSendEmailHistory : BaseEntity
    {
        public Guid ReceiptHeaderID { get; set; }
        [ForeignKey("ReceiptHeaderID")]
        public ReceiptHeader ReceiptHeader { get; set; }

        [Description("Email ผู้รับ")]
        public string Email { get; set; }

        [Description("วันที่ ส่ง Email")]
        public DateTime SendDate { get; set; }

        [Description("ระงับการส่ง")]
        public bool IsLock { get; set; }

        [Description("วันที่ ระงับการส่ง")]
        public DateTime LockDate { get; set; }

        [Description("ระงับการส่ง โดย")]
        public Guid? LockByUserID { get; set; }
        [ForeignKey("LockByUserID")]
        public USR.User LockByUser { get; set; }
    }
}
