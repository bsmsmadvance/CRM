﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Item งานขอสร้าง PR จาก SAP")]
    [Table("PRRequestJobItem", Schema = Schema.PROMOTION)]
    public class PRRequestJobItem : BaseEntity
    {
        [Description("สถานะของการขอ PR")]
        public Guid? PRRequestJobStatusMasterCenterID { get; set; }
        [ForeignKey("PRRequestJobStatusMasterCenterID")]
        public MST.MasterCenter PRRequestJobStatus { get; set; }

        [Description("รายการโปรก่อนขายที่ขอเบิก")]
        public Guid? PreSalePromotionRequestItemID { get; set; }
        [ForeignKey("PreSalePromotionRequestItemID")]
        public PreSalePromotionRequestItem PreSalePromotionRequestItem { get; set; }

        [Description("งานขอ PR")]
        public Guid? PRRequestJobID { get; set; }
        [ForeignKey("PRRequestJobID")]
        public PRRequestJob PRRequestJob { get; set; }

        [Description("ครั้งที่ Retry")]
        public int Retry { get; set; }

        [Description("User Name from Web")]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Description("รหัสโปรโมชั่น")]
        [MaxLength(100)]
        public string PromotionNo { get; set; }
        [MaxLength(100)]
        public string DocType { get; set; }
        [MaxLength(100)]
        public string PurchasingGroup { get; set; }
        [MaxLength(100)]
        public string PurchasingOrg { get; set; }
        [MaxLength(100)]
        public string Requester { get; set; }
        [MaxLength(100)]
        public string Plant { get; set; }
        [MaxLength(100)]
        public string AccountAssignmentCategory { get; set; }
        [MaxLength(100)]
        public string MaterialNo { get; set; }
        [Description("จำนวน")]
        public int Quantity { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        [Description("Price Unit (Fix value = 1)")]
        [MaxLength(100)]
        public string PriceUnit { get; set; }
        [MaxLength(100)]
        public string AgreementNo { get; set; }
        [MaxLength(100)]
        public string ItemNo { get; set; }
        [MaxLength(100)]
        public string GoodReceiptIndicator { get; set; }
        [MaxLength(100)]
        public string InvoiceReceiptIndicator { get; set; }
        [MaxLength(100)]
        public string CreatedByDisplayName { get; set; }
        [Description("Price Unit (Fix value = 01)")]
        [MaxLength(100)]
        public string SerialNo { get; set; }
        [MaxLength(100)]
        public string GoodRecipient { get; set; }
        [Description("เลขบัญชี GL")]
        [MaxLength(100)]
        public string GLAccountNo { get; set; }
        [Description("Object number (forecast) กิ่ง P")]
        [MaxLength(100)]
        public string SAPWBSObject_P { get; set; }
        [Description("SAP WBS Number สำหรับ Budget Promotion")]
        [MaxLength(100)]
        public string SAPWBSNo_P { get; set; }
        [Description("ชื่อโปรโมชั่น")]
        [MaxLength(100)]
        public string PromotionName { get; set; }
        [Description("Item Text")]
        [MaxLength(100)]
        public string TextB01 { get; set; }
        [Description("Sub ย่อยของรายการ")]
        [MaxLength(100)]
        public string TextB02 { get; set; }
        [Description("สถานที่ส่งสินค้า")]
        [MaxLength(100)]
        public string TextB03 { get; set; }
        [Description("รายละเอียดและค่าบริการ")]
        [MaxLength(100)]
        public string TextB04 { get; set; }
        [MaxLength(100)]
        public string ShortText { get; set; }
        public DateTime DeliveryDate { get; set; }
        [MaxLength(100)]
        public string ApproveName { get; set; }

    }
}
