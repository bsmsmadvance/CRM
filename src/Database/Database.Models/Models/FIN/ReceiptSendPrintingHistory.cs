using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ประวัติการส่งใบเสร็จเข้าโรงพิมพ์")]
    [Table("ReceiptSendPrintingHistory", Schema = Schema.FINANCE)]
    public class ReceiptSendPrintingHistory : BaseEntity
    {
        public Guid ReceiptHeaderID { get; set; }
        [ForeignKey("ReceiptHeaderID")]
        public ReceiptHeader ReceiptHeader { get; set; }

        [Description("เลขที่ Lot")]
        [MaxLength(50)]
        public string LotNo { get; set; }

        [Description("วันที่ Export = Createdate")]
        public DateTime ExportDate { get; set; }

        [Description("วันที่ ส่งให้โรงพิมพ์ User เลือกเอง")]
        public DateTime SendDate { get; set; }

        [Description("จำนวนใบเสร็จที่ส่งให้โรงพิมพ์ ใน Lot นี้")]
        public int? TotalRecord { get; set; }

        [Description("ระงับการส่ง")]
        public bool? IsLock { get; set; }

        [Description("วันที่ ระงับการส่ง")]
        public DateTime LockDate { get; set; }

        [Description("ระงับการส่ง โดย")]
        public Guid? LockByUserID { get; set; }
        [ForeignKey("LockByUserID")]
        public USR.User LockByUser { get; set; }
    }
}
