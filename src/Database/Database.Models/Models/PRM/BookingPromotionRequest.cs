using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.SAL;

namespace Database.Models.PRM
{
    [Description("ใบเบิกโปรโมชั่นขาย")]
    [Table("BookingPromotionRequest", Schema = Schema.PROMOTION)]
    public class BookingPromotionRequest : BaseEntity
    {
        [Description("โปรขาย")]
        public Guid? BookingPromotionID { get; set; }
        [ForeignKey("BookingPromotionID")]
        public BookingPromotion BookingPromotion { get; set; }
        [Description("เลขที่เบิก")]
        [MaxLength(100)]
        public string RequestNo { get; set; }
        [Description("วันที่เบิก")]
        public DateTime? RequestDate { get; set; }

    }
}
