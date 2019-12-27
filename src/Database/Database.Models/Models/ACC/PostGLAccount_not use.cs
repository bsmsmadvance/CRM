using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("คู่บัญชีเงินรับและ DocCode")]
    [Table("PostGLAccount", Schema = Schema.ACCOUNT)]
    public class PostGLAccount : BaseEntity
    {
        [Description("ประเภท")]
        public string GLType { get; set; }

        [Description("รหัสเอกสาร")]
        public string DocCode { get; set; }

        [Description("คำอธิบายอื่นๆ")]
        public string Description { get; set; }

        [Description("เลขที่บัญชี GL")]
        public string GLAccountNo { get; set; }

        [Description("ID Format การ Gen text file ส่งไป SAP")]
        public Guid? FormatTextFileID { get; set; }

        [ForeignKey("FormatTextFileID")]
        public PostGLFormatTextFileHeader FormatTextFile { get; set; }

    }
}
