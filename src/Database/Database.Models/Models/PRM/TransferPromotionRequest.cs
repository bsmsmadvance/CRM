using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Database.Models.SAL;

namespace Database.Models.PRM
{
    [Description("ใบเบิกโปรโมชั่นโอน")]
    [Table("TransferPromotionRequest", Schema = Schema.PROMOTION)]
    public class TransferPromotionRequest : BaseEntity
    {
        [Description("โปรโอน")]
        public Guid? TransferPromotionID { get; set; }
        [ForeignKey("TransferPromotionID")]
        public TransferPromotion TransferPromotion { get; set; }
        [Description("เลขที่เบิก")]
        [MaxLength(100)]
        public string RequestNo { get; set; }
        [Description("วันที่เบิก")]
        public DateTime? RequestDate { get; set; }

    }
}
