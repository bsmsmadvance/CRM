using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Master รายการโปรโมชั่นผูกกับแบบบ้าน (ขาย)")]
    [Table("MasterBookingHouseModelItem", Schema = Schema.PROMOTION)]
    public class MasterBookingHouseModelItem : BaseEntity
    {
        public Guid MasterBookingPromotionItemID { get; set; }
        [ForeignKey("MasterBookingPromotionItemID")]
        public MasterBookingPromotionItem MasterBookingPromotionItem { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
    }
}
