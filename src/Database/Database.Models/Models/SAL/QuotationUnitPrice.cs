using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ราคาของใบเสนอราคา")]
    [Table("QuotationUnitPrice", Schema = Schema.SALE)]
    public class QuotationUnitPrice : BaseEntity
    {
        [Description("ใบเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public Quotation Quotation { get; set; }

        [Description("Clone ค่ามาจาก Price List")]
        public Guid? FromPriceListID { get; set; }
        [ForeignKey("FromPriceListID")]
        public PRJ.PriceList FromPriceList { get; set; }
    }
}
