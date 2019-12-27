using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("เงินโอนค่าโอนกรรมสิทธิ์")]
    [Table("TransferBankTransfer", Schema = Schema.SALE)]
    public class TransferBankTransfer : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("จ่ายให้กับ")]
        public Guid? BankTransferPayToMasterCenterID { get; set; }
        [ForeignKey("BankTransferPayToMasterCenterID")]
        public MST.MasterCenter BankTransferPayToPayTo { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("วันที่เงินเข้า")]
        public DateTime? PayDate { get; set; }

        [Description("สั่งจ่ายผิดบริษัท")]
        public bool IsWrongTransferDate { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }


    }
}
