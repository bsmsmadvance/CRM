using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ข้อมูลการรับชำระเงินจากระบบ Offline (Header)")]
    [Table("OfflinePaymentHeader", Schema = Schema.FINANCE)]
    public class OfflinePaymentHeader
    {
        [Description("เลขที่ใบเสร็จ จาก Offline")]
        [MaxLength(50)]
        public string ReceiptNo { get; set; }

        [Description("วันที่ชำระเงิน จาก Offline")]
        public DateTime ReceiveDate { get; set; }

        [Description("เลขที่จอง Offline")]
        [MaxLength(50)]
        public string BookingNo { get; set; }

        [Description("เลขที่ Project")]
        [MaxLength(50)]
        public string ProjectNo { get; set; }

        [Description("เลขที่ Unit")]
        [MaxLength(50)]
        public string UnitNo { get; set; }

        [Description("รายการ BOoking ในระบบ map จาก BookingNo")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("ประเภทการรับชำระเงิน จอง,สัญญา,ดาวน์")]
        public Guid? OfflinePaymentItemMasterCenterID { get; set; }
        [ForeignKey("OfflinePaymentItemMasterCenterID")]
        public MST.MasterCenter OfflinePaymentItemMasterCenter { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        [Description("สถานะรายการเงิน Offline")]
        public Guid? OfflinePaymentStatusMasterCenterID { get; set; }
        [ForeignKey("OfflinePaymentStatusMasterCenterID")]
        public MST.MasterCenter OfflinePaymentStatusMasterCenter { get; set; }

        [Description("วันที่พิมพ์ใบเสร็จ")]
        public DateTime PrintDate { get; set; }

        [Description("ครั้งที่พิมพ์")]
        public int PrintCount { get; set; }

        [Description("วันที่ยืนยันเงิน")]
        public DateTime? ConfirmedDate { get; set; }

        [Description("ผู้ยืนยันเงิน")]
        public Guid? ConfirmedByID { get; set; }
        [ForeignKey("ConfirmedByID")]
        public USR.User ConfirmedBy { get; set; }

        [Description("สาเหตุการยกเลิก")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }

        [Description("PaymentID ของรายการที่ยืนยันการรับเงินแล้ว")]
        public Guid? PaymentID { get; set; }
        [ForeignKey("PaymentID")]
        public FIN.Payment Payment { get; set; }

        [Description("ชื่อผู้สร้างรายการจากระบบ Offline")]
        [MaxLength(100)]
        public string CreatedByName { get; set; }

        [Description("User ID ของ CreatedByName")]
        public Guid? CreatedByID { get; set; }
        [ForeignKey("CreatedByID")]
        public USR.User CreatedBy { get; set; }

        [Description("")]
        public DateTime CreatedDate { get; set; }

        [Description("ชื่อ ผู้แก้ไขรายการจากระบบ Offline")]
        [MaxLength(100)]
        public string UpdatedByName { get; set; }

        [Description("User ID ของ UpdatedByName")]
        public Guid? UpdatedByID { get; set; }
        [ForeignKey("UpdatedByID")]
        public USR.User UpdatedBy { get; set; }

        [Description("วันที่แก้ไข/วันที่ยกเลิก")]
        public DateTime UpdatedDate { get; set; }

        [Description("")]
        [MaxLength(100)]
        public string UploadBy { get; set; }
    }
}
