using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Chart of Account")]
    [Table("PostGLChartOfAccount", Schema = Schema.ACCOUNT)]
    public class PostGLChartOfAccount : BaseEntity
    {
        [Description("เลขที่บัญชี GL")]
        public string GLAccountID { get; set; }

        [Description("ประเภทของบัญชี")]
        public string AccountType { get; set; }

        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }

        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("บัญชีธนาคาร")]
        public Guid? BankAccountID { get; set; }

        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ชื่อบัญชี")]
        public string AccountName { get; set; }

        [Description("กลุ่มของรูปแบบ")]
        public string AccountTypeGroup { get; set; }

        [Description("ยังใช้งานอยู่หรือไม่")]
        public bool IsActive { get; set; }


    }
}
