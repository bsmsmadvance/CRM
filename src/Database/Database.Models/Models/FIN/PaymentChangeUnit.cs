using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("รายละเอียดการชำระเงินจากสัญญาเก่า")]
    [Table("PaymentChangeUnit", Schema = Schema.FINANCE)]
    public class PaymentChangeUnit : BaseEntity
    {
        public Guid? PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public PaymentMethod PaymentMethod { get; set; }
        
        [Description("สร้างจาก Payment Method ไหน?")]
        public Guid? FromPaymentMethodID { get; set; }
        [ForeignKey("FromPaymentMethodID")]
        public PaymentMethod FromPaymentMethod { get; set; }

        [Description("สร้างจาก Payment Method (ตัวเริ่มต้นที่มีใบเสร็จ) ไหน?")]
        public Guid? BasePaymentMethodID { get; set; }
        [ForeignKey("BasePaymentMethodID")]
        public PaymentMethod BasePaymentMethod { get; set; }

    }
}
