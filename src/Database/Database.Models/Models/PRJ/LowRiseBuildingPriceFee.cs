using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ค่าทำเนียมโอน-ค่าพื้นที่สิ่งปลูกสร้างแนวราบ")]
    [Table("LowRiseBuildingPriceFee", Schema = Schema.PROJECT)]
    public class LowRiseBuildingPriceFee : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("รหัสแบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }
        [Description("รหัสแปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        [Description("ราคา")]
        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }

    }
}
