using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นที่ไม่ต้องจัดซื้อผูกกับแบบบ้าน (ขาย)")]
    [Table("MasterBookingHouseModelFreeItem", Schema = Schema.PROMOTION)]
    public class MasterBookingHouseModelFreeItem : BaseEntity
    {
        public Guid MasterBookingPromotionFreeItemID { get; set; }
        [ForeignKey("MasterBookingPromotionFreeItemID")]
        public MasterBookingPromotionFreeItem MasterBookingPromotionFreeItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
