﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.MST;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นรูดบัตรเครดิตที่มีให้เลือก (ขาย)")]
    [Table("MasterBookingCreditCardItem", Schema = Schema.PROMOTION)]
    public class MasterBookingCreditCardItem : BaseEntity
    {
        public Guid MasterBookingPromotionID { get; set; }
        [ForeignKey("MasterBookingPromotionID")]
        public MasterBookingPromotion MasterBookingPromotion { get; set; }

        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }
        [Description("ชื่อโปรโมชั่น (TH)")]
        public string NameTH { get; set; }
        [Description("ชื่อโปรโมชั่น (EN)")]
        public string NameEN { get; set; }
        [Description("ค่าธรรมเนียม (%)")]
        public double Fee { get; set; }
        [Description("หน่วย (TH)")]
        public string UnitTH { get; set; }
        [Description("หน่วย (EN)")]
        public string UnitEN { get; set; }
        [Description("สถานะ")]
        public Guid? PromotionItemStatusMasterCenterID { get; set; }
        [ForeignKey("PromotionItemStatusMasterCenterID")]
        public MST.MasterCenter PromotionItemStatus { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }

        [Description("สร้างมาจากค่าธรรมเนียมบัตรเคดิตไหน?")]
        public Guid? EDCFeeID { get; set; }
        [ForeignKey("EDCFeeID")]
        public EDCFee EDCFee { get; set; }

        [Description("ลำดับของ Item")]
        public int Order { get; set; }
        [Description("รหัส Item")]
        public string PromotionItemNo { get; set; }

    }
}
