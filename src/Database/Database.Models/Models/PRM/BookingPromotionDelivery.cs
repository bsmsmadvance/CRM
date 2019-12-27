using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("ใบส่งมอบโปรโมชั่นขาย")]
    [Table("BookingPromotionDelivery", Schema = Schema.PROMOTION)]
    public class BookingPromotionDelivery : BaseEntity
    {
        [Description("โปรโมชั่นขาย")]
        public Guid BookingPromotionID { get; set; }
        [ForeignKey("BookingPromotionID")]
        public BookingPromotion BookingPromotion { get; set; }
        [Description("เลขที่ส่งมอบ")]
        [MaxLength(100)]
        public string DeliveryNo { get; set; }
        [Description("วันที่ส่งมอบ")]
        public DateTime? DeliveryDate { get; set; }

    }
}
