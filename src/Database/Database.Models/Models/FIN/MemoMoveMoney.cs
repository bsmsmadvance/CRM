using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("Memo ย้ายเงินระหว่างบัญชี")]
    [Table("MemoMoveMoney", Schema = Schema.FINANCE)]
    public class MemoMoveMoney:BaseEntity
    {
        [Description("ข้อมูลการรับชำระเงิน")]
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }

        [Description("ID บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo")]
        public Guid DestinationCompanyID { get; set; }
        [ForeignKey("DestinationCompanyID")]
        public MST.Company Company { get; set; }

        [Description("วัตถุประสงค์")]
        public Guid MoveMoneyReasonMasterCenterID { get; set; }
        [ForeignKey("MoveMoneyReasonMasterCenterID")]
        public MST.MasterCenter MoveMoneyReason { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }

        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public USR.User PrintBy { get; set; }

        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }
    }
}
