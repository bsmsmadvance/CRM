﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("งานขอสร้าง PR จาก SAP")]
    [Table("PRRequestJob", Schema = Schema.PROMOTION)]
    public class PRRequestJob : BaseEntity
    {
        [Description("ชื่อไฟล์ที่ส่งให้ SAP")]
        [MaxLength(1000)]
        public string FileName { get; set; }
        [Description("ชื่อไฟล์ที่รับผลจาก SAP")]
        [MaxLength(1000)]
        public string SAPResultFileName { get; set; }

        [Description("สถานะการทำงานของ Job")]
        public BackgroundJobStatus Status { get; set; }

        [Description("ข้อผิดพลาด")]
        public string ErrorMessage { get; set; }
    }
}
