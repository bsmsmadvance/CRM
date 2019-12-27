using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ไฟล์แนบสัญญา")]
    [Table("AgreementFile", Schema = Schema.SALE)]
    public class AgreementFile : BaseEntity
    {
        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public Agreement Agreement { get; set; }

        [Description("ชื่อไฟล์")]
        [MaxLength(1000)]
        public string FileName { get; set; }
    }
}
