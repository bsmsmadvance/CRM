using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationBookingPromotion", Schema = Schema.PROMOTION)]
    public class QuotationBookingPromotion : BaseEntity
    {

        [Description("ผูกใบเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ผูกโปรโมชั่น")]
        public Guid? MasterBookingPromotionID { get; set; }
        [ForeignKey("MasterBookingPromotionID")]
        public MasterBookingPromotion MasterPromotion { get; set; }

    }
}
