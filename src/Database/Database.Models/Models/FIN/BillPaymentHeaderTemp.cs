using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("การนำเข้า Bill Payment จากธนาคาร")]
    [Table("BillPaymentTemp", Schema = Schema.FINANCE)]
    public class BillPaymentHeaderTemp : BaseEntity
    {
        [Description("รหัสนำเข้า")]
        [MaxLength(50)]
        public string BatchID { get; set; }

        [Description("ID บัญชีบริษัท")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ชื่อ Text file ที่ Import")]
        [MaxLength(100)]
        public string ImportFileName { get; set; }

        //[Description("วันที่-เวลา Import File")]//ใช้ CreateDate
        //public DateTime? ImportDate { get; set; }

        [Description("วิธีการ Import Bill Payment")]
        public Guid? BillPaymentImportTypeMasterCenterID { get; set; }
        [ForeignKey("BillPaymentImportTypeMasterCenterID")]
        public MST.MasterCenter BillPaymentImportType { get; set; }

        [Description("จำนวนเงินรวม")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("รวมจำนวนรายการ")]
        public int TotalRecord { get; set; }

        [Description("วันที่เงินเข้า/วันที่ชำระเงิน")]
        public DateTime ReceiveDate { get; set; }
    }
}
