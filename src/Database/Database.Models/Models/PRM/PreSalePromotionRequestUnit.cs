﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Unit ที่เบิกโปรก่อนขาย")]
    [Table("PreSalePromotionRequestUnit", Schema = Schema.PROMOTION)]
    public class PreSalePromotionRequestUnit : BaseEntity
    {
        [Description("ใบเบิกโปรโมชั่นก่อนขาย")]
        public Guid PreSalePromotionRequestID { get; set; }
        [ForeignKey("PreSalePromotionRequestID")]
        public PreSalePromotionRequest PreSalePromotionRequest { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        public PRJ.Unit Unit { get; set; }

        [Description("สถานะการสร้าง/ยกเลิก PR")]
        public Guid? SAPPRStatusMasterCenterID { get; set; }
        [ForeignKey("SAPPRStatusMasterCenterID")]
        public MST.MasterCenter SAPPRStatus { get; set; }

        [Description("ชนิดของ Job สร้าง/ยกเลิก PR")]
        public Guid? PromotionRequestPRJobTypeMasterCenterID { get; set; }
        [ForeignKey("PromotionRequestPRJobTypeMasterCenterID")]
        public MST.MasterCenter PromotionRequestPRJobType { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        public List<PreSalePromotionRequestItem> PreSalePromotionRequestItems { get; set; }

    }
}
