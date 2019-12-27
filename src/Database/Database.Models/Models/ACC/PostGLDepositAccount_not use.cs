using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("บัญชีนำฝากและคู่บัญชี")]
    [Table("PostGLDepositAccount", Schema = Schema.ACCOUNT)]
    public class PostGLDepositAccount : BaseEntity
    {
        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("อำเภอ")]
        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public MST.District District { get; set; }

        [Description("สาขาธนาคาร")]
        public Guid? BankBranchID { get; set; }
        [ForeignKey("BankBranchID")]
        public MST.BankBranch BankBranch { get; set; }

        [Description("ประเภทบัญชี")]
        public string Type { get; set; }
        [Description("เลขที่บัญชี")]
        public string BankAccountNo { get; set; }

        [Description("เลขที่บัญชี GL")]
        public string GLAccountID { get; set; }

        [Description("บัญชีเงินโอนผ่านธนาคาร")]
        public bool isBankTransfer { get; set; }
        [Description("บัญชี Direct Debit")]
        public bool isDirectDebit { get; set; }
        [Description("บัญชี Direct Credit")]
        public bool isDirectCredit { get; set; }
        [Description("บัญชีนำฝาก")]
        public bool isDepositAccount { get; set; }

    }
}
