using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("การอนุมัติเปลี่ยนแปลงโปรโมชั่น")]
    [Table("ChangePromotionWorkflow", Schema = Schema.PROMOTION)]
    public class ChangePromotionWorkflow : BaseEntity
    {
        [Description("ผู้ขอเปลี่ยนแปลง")]
        public Guid? RequestByUserID { get; set; }
        [ForeignKey("RequestByUserID")]
        public USR.User RequestByUser { get; set; }
        [Description("วันที่ขอ")]
        public DateTime RequestDate { get; set; }

        [Description("ชนิดของโปรโมชั่นที่ขอเปลี่ยนแปลง")]
        public Guid PromotionTypeMasterCenterID { get; set; }
        [ForeignKey("PromotionTypeMasterCenterID")]
        public MST.MasterCenter PromotionType { get; set; }


        [Description("วันที่อนุมัติ")]
        public DateTime ApproveDate { get; set; }
        [Description("สถานะอนุมัติ")]
        public bool? IsApproved { get; set; }
    }
}
