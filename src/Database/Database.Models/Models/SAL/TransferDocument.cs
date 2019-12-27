using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("รับเอกสารหลังโอนกรรมสิทธิ์")]
    [Table("TransferDocument", Schema = Schema.SALE)]
    public class TransferDocument : BaseEntity
    {

        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("เหตุผล")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("Reject หรือไม่")]
        public bool IsRejected { get; set; }

    }
}
