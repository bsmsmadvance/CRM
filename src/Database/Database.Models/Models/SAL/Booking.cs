using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ใบจอง")]
    [Table("Booking", Schema = Schema.SALE)]
    public class Booking : BaseEntity
    {
        [Description("ใบเสนอราคา")]
        public Guid? QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public Quotation Quotation { get; set; }

        [Description("เลขที่ใบจอง")]
        [MaxLength(100)]
        public string BookingNo { get; set; }
        [Description("สถานะใบจอง")]
        public Guid? BookingStatusMasterCenterID { get; set; }
        [ForeignKey("BookingStatusMasterCenterID")]
        public MST.MasterCenter BookingStatus { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        public PRJ.Model Model { get; set; }

        [Description("พื้นที่ขาย")]
        public double? SaleArea { get; set; }

        [Description("วันที่จอง")]
        public DateTime? BookingDate { get; set; }
        [Description("วันที่นัดทำสัญญา")]
        public DateTime? ContractDueDate { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApproveDate { get; set; }
        [Description("วันที่ทำสัญญา")]
        public DateTime? ContractDate { get; set; }
        [Description("วันที่โอนกรรมสิทธิ์")]
        public DateTime? TransferOwnershipDate { get; set; }

        [Description("ประเภทพนักงานปิดการขาย")]
        public Guid? SaleOfficerTypeMasterCenterID { get; set; }
        [ForeignKey("SaleOfficerTypeMasterCenterID")]
        public MST.MasterCenter SaleOfficerType { get; set; }

        [Description("รหัส Sale")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("รหัส Agent")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public MST.Agent Agent { get; set; }

        [Description("รหัสพนักงาน Agent")]
        public Guid? AgentEmployeeID { get; set; }
        [ForeignKey("AgentEmployeeID")]
        public MST.AgentEmployee AgentEmployee { get; set; }

        [Description("รหัส Sale ประจำโครงการ")]
        public Guid? ProjectSaleUserID { get; set; }
        [ForeignKey("ProjectSaleUserID")]
        public USR.User ProjectSaleUser { get; set; }

        [Description("ผู้แนะนำ")]
        public Guid? ReferContactID { get; set; }
        [ForeignKey("ReferContactID")]
        public CTM.Contact ReferContact { get; set; }
        [Description("ชื่อผู้แนะนำ (สำหรับเก็บข้อมูล Free Text)")]
        [MaxLength(1000)]
        public string ReferContactName { get; set; }

        //Memo ยกเลิกใบจอง
        [Description("ยกเลิกใบจอง")]
        public bool IsCancelled { get; set; }
        [Description("การยกเลิกใบจอง")]
        public BookingCancelType? CancelType { get; set; }
        [Description("วันที่ยกเลิกใบจอง")]
        public DateTime? CancelDate { get; set; }


        [Description("ถูกสร้างจากการย้ายแปลง")]
        public bool IsFromChangeUnit { get; set; }
        [Description("ย้ายแปลงมาจาก")]
        public Guid? ChangeFromBookingID { get; set; }
        [Description("ย้ายแปลงไปหา")]
        public Guid? ChangeToBookingID { get; set; }
        [Description("วันที่ย้ายแปลง")]
        public DateTime? ChangeUnitDate { get; set; }
        [Description("ย้ายแปลงโดย")]
        public Guid? ChangeUnitByUserID { get; set; }
        [ForeignKey("ChangeUnitByUserID")]
        public USR.User ChangeUnitByUser { get; set; }

        [Description("ชำระเงินจองครบแล้ว")]
        public bool? IsPaid { get; set; }
        [Description("พร้อมรับชำระเงิน (รองรับการชำระเงิน)")]
        public bool IsReadyToPayment { get; set; }

        [Description("สร้างใบจองจากระบบไหน?")]
        public Guid? CreateBookingFromMasterCenterID { get; set; }
        [Description("CreateBookingFromMasterCenterID")]
        public MST.MasterCenter CreateBookingFrom { get; set; }

        [Description("สถานะการขอสินเชื่อ")]
        public Guid? CreditBankingTypeMasterCenterID { get; set; }
        [ForeignKey("CreditBankingTypeMasterCenterID")]
        public MST.MasterCenter CreditBankingType { get; set; }

        [Description("ราคาแนะนำ ณ วันจอง")]
        [Column(TypeName = "Money")]
        public decimal? MasterMinPrice { get; set; }

        [Description("Budget Pro ขาย ณ วันจอง")]
        [Column(TypeName = "Money")]
        public decimal? MasterSaleBudgetPromotion { get; set; }
        
        [Description("ยืนยันโดย")]
        public Guid? ConfirmByUserID { get; set; }
        [Description("ConfirmByUserID")]
        public USR.User ConfirmBy { get; set; }
        [Description("วันที่ยืนยัน")]
        public DateTime? ConfirmDate{ get; set; }

        [Description("ตัวแทนอื่น")]
        [MaxLength(1000)]
        public string AgentOther { get; set; }
    }
}
