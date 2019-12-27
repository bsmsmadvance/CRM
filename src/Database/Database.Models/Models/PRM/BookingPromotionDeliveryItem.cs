using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{

    [Description("รายการการส่งมอบโปรโมชั่นขาย")]
    [Table("BookingPromotionDeliveryItem", Schema = Schema.PROMOTION)]
    public class BookingPromotionDeliveryItem : BaseEntity
    {
        [Description("ผูกการส่งมอบโปรโมชั่นขาย")]
        public Guid? BookingPromotionDeliveryID { get; set; }
        [ForeignKey("BookingPromotionDeliveryID")]
        public BookingPromotionDelivery BookingPromotionDelivery { get; set; }
        [Description("โปรโมชั่นที่ส่งมอบ")]
        public Guid? BookingPromotionItemID { get; set; }
        [ForeignKey("BookingPromotionItemID")]
        public BookingPromotionItem BookingPromotionItem { get; set; }
        [Description("เบิกแล้ว")]
        public int ReceiveQuantity { get; set; }
        [Description("ส่งมอบแล้ว")]
        public int DeliveryQuantity { get; set; }
        [Description("คงเหลือเบิก")]
        public int RemainingReceiveQuantity { get; set; }
        [Description("จำนวนที่ส่งมอบ")]
        public int Quantity { get; set; }
        [Description("เลขที่ Serial")]
        [MaxLength(100)]
        public string SerialNo { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

    }
}
