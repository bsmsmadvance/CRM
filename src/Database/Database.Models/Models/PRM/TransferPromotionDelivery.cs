using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("ใบส่งมอบโปรโมชั่นโอน")]
    [Table("TransferPromotionDelivery", Schema = Schema.PROMOTION)]
    public class TransferPromotionDelivery : BaseEntity
    {
        [Description("โปรโมชั่นโอน")]
        public Guid TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }
        [Description("เลขที่ส่งมอบ")]
        [MaxLength(100)]
        public string DeliveryNo { get; set; }
        [Description("วันที่ส่งมอบ")]
        public DateTime? DeliveryDate { get; set; }

    }
}
