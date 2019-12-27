using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการเบิกโปรโมชั่นขาย")]
    [Table("BookingPromotionRequestItem", Schema = Schema.PROMOTION)]
    public class BookingPromotionRequestItem : BaseEntity
    {
        [Description("ผูกการเบิกโปรโมชั่นขาย")]
        public Guid? BookingPromotionRequestID { get; set; }
        [ForeignKey("BookingPromotionRequestID")]
        public BookingPromotionRequest BookingPromotionRequest { get; set; }
        [Description("ผูกสิ่งของโปรโมชั่นขาย")]
        public Guid? BookingPromotionItemID { get; set; }
        [ForeignKey("BookingPromotionItemID")]
        public BookingPromotionItem BookingPromotionItem { get; set; }
        [Description("เบิกแล้ว")]
        public int RequestQuantity { get; set; }
        [Description("คงเหลือเบิก")]
        public int RemainingRequestQuantity { get; set; }
        [Description("จำนวนที่เบิก")]
        public int Quantity { get; set; }
        [Description("วันที่คาดว่าจะได้รับ")]
        public DateTime? EstimateRequestDate { get; set; }
        [Description("เลขที่ PR")]
        [MaxLength(100)]
        public string PRNo { get; set; }
        [Description("หมายเหตุไม่เบิกโปร")]
        [MaxLength(5000)]
        public string DenyRemark { get; set; }

    }
}
