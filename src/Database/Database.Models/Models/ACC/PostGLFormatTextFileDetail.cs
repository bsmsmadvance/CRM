using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Format การ Gen text file")]
    [Table("PostGLFormatTextFileDetail", Schema = Schema.ACCOUNT)]
    public class PostGLFormatTextFileDetail : BaseEntity
    {
        [Description("Debit/Credit")]
        [MaxLength(10)]
        public string PostingType { get; set; }

        [Description("เจ้าหนี้ 21 = DR, 31 = CR / รายได้ , ค่าใช้จ่าย  40 = DR, 50 = CR")]
        [MaxLength(10)]
        public string PostingKey { get; set; }        

        [Description("ลำดับในการ Gen Text file")]
        public int Seq { get; set; }

        [Description("ชื่อ Column ใน Table PostGLDetail ใช้สำหรับดึงข้อมูลมาสร้างเป็น Text file")]
        [MaxLength(50)]
        public string ColumnName { get; set; }

        [Description("ชื่อ Column ใน Table PostGLDetail ใช้สำหรับดึงข้อมูลมาสร้างเป็น Text file")]
        [MaxLength(50)]
        public string FixValue { get; set; }
    }
}
