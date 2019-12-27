using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ราคาโครงการ")]
    [Table("UnitPrice", Schema = Schema.SALE)]
    public class UnitPrice : BaseEntity
    {
        [Description("ผูกใบจอง")]
        public Guid BookingID { get; set; }

        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }

        [Description("ขั้นตอนของการซื้อแปลง")]
        public Guid UnitPriceStageMasterCenterID { get; set; }
        [ForeignKey("UnitPriceStageMasterCenterID")]
        public MST.MasterCenter UnitPriceStage { get; set; }

        [Description("ราคาที่ใช้จริง")]
        public bool IsActive { get; set; }

        [Description("ลำดับ")]
        public int Order { get; set; }

        /*
        [Description("ราคาขาย")]
        [Column(TypeName = "Money")]
        public decimal? SellingPrice { get; set; }

        [Description("ราคาขายสุทธิ")]
        [Column(TypeName = "Money")]
        public decimal? NetSellingPrice { get; set; }

        [Description("เงินจอง")]
        [Column(TypeName = "Money")]
        public decimal? BookingAmount { get; set; }

        [Description("เงินสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractAmount { get; set; }

        [Description("เงินดาวน์")]
        [Column(TypeName = "Money")]
        public decimal? DownAmount { get; set; }

        [Description("ส่วนลดเงินสด")]
        [Column(TypeName = "Money")]
        public decimal? CashDiscount { get; set; }

        [Description("ส่วนลด ณ​ วันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }

        [Description("ส่วนลด Friend get Friend")]
        [Column(TypeName = "Money")]
        public decimal? FGFDiscount { get; set; }

        [Description("ส่วนลด Freedown")]
        [Column(TypeName = "Money")]
        public decimal? FreedownAmount { get; set; }
        */
    }
}
