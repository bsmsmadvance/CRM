using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ข้อมูลช่องทางของการชำระเงิน")]
    [Table("PaymentMethod", Schema = Schema.FINANCE)]
    public class PaymentMethod : BaseEntity
    {
        [Description("ผูกกับการชำระเงิน")]
        public Guid PaymentID { get; set; }
        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }

        [Description("ชนิดของช่องทางการชำระเงิน")]
        public Guid? PaymentMethodTypeMasterCenterID { get; set; }
        [ForeignKey("PaymentMethodTypeMasterCenterID")]
        public MST.MasterCenter PaymentMethodType { get; set; }
        //เงินที่จ่าย
        [Description("จำนวนเงินที่จ่าย")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        public List<DepositDetail> DepositDetails { get; set; }

    }
}
