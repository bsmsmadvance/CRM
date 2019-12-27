using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("โอนกรรมสิทธิ์")]
    [Table("Transfer", Schema = Schema.SALE)]
    public class Transfer : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("เลขที่โอนกรรมสิทธิ์")]
        [MaxLength(100)]
        public string TransferNo { get; set; }

        [Description("เลขที่สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("พื้นที่ (ตร.ว/ตร.ม)")]
        public double? StandardArea { get; set; }
        [Description("พื้นที่ที่ใช้คำนวนราคาประเมิณ")]
        public double? LandArea { get; set; }
        [Description("ราคาประเมิณ")]
        [Column(TypeName = "Money")]
        public decimal? LandEstimatePrice { get; set; }

        [Description("LC โอน")]
        public Guid? TransferSaleUserID { get; set; }
        [ForeignKey("TransferSaleUserID")]
        public USR.User TransferSale { get; set; }

        [Description("วันที่นัดโอนกรรมสิทธื์")]
        public DateTime? ScheduleTransferDate { get; set; }
        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }

        //รายละเอียดค่าธรรมเนียม
        [Description("ภาษีเงินได้นิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? CompanyIncomeTax { get; set; }
        [Description("ภาษีเงินได้ธุรกิจเฉพาะ")]
        [Column(TypeName = "Money")]
        public decimal? BusinessTax { get; set; }
        [Description("ภาษีท้องถิ่น")]
        [Column(TypeName = "Money")]
        public decimal? LocalTax { get; set; }
        [Description("รูดบัตร P.Card กระทรวงการคลัง")]
        [Column(TypeName = "Money")]
        public decimal? MinistryPCard { get; set; }
        [Description("เงินสดหรือเช็คกระทรวงการคลัง")]
        [Column(TypeName = "Money")]
        public decimal? MinistryCashOrCheque { get; set; }
        [Description("เช็คค่ามิเตอร์")]
        public Guid? MeterChequeMasterCenterID { get; set; }
        [ForeignKey("MeterChequeMasterCenterID")]
        public MST.MasterCenter MeterCheque { get; set; }
        [Description("ลูกค้าจ่ายค่าจดจำนอง")]
        [Column(TypeName = "Money")]
        public decimal? CustomerPayMortgage { get; set; }
        [Description("ลูกค้าจ่ายค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? CustomerPayFee { get; set; }
        [Description("บริษัทจ่ายค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? CompanyPayFee { get; set; }
        [Description("ฟรีค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? FreeFee { get; set; }
        [Description("ค่าดำเนินการเอกสาร (ขาด/เกิน)")]
        [Column(TypeName = "Money")]
        public decimal? DocumentFee { get; set; }

        //ยอดคงเหลือ
        [Description("ยอดคงเหลือ AP")]
        [Column(TypeName = "Money")]
        public decimal? APBalance { get; set; }
        [Description("ยกยอดไปนิติบุคคล")]
        public bool IsAPBalanceTransfer { get; set; }
        [Description("ยอดคงเหลือ AP ยกยอดไปนิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? APBalanceTransfer { get; set; }
        [Description("เงินทอนก่อนโอน")]
        [Column(TypeName = "Money")]
        public decimal? APChangeAmountBeforeTransfer { get; set; }
        [Description("รวมเงินทอน")]
        [Column(TypeName = "Money")]
        public decimal? APChangeAmount { get; set; }
        [Description("การทอนคืน AP")]
        public bool? IsAPGiveChange { get; set; }
        [Description("จ่ายด้วย")]
        public bool? IsAPPayWithMemo { get; set; }
        [Description("ยอดคงเหลือนิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityBalance { get; set; }
        [Description("ยกยอดไป AP")]
        public bool IsLegalEntityBalanceTransfer { get; set; }
        [Description("ยอดคงเหลือนิติบุคคล ยกยอดไป AP")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityBalanceTransfer { get; set; }
        [Description("การทอนคืนนิติบุคคล")]
        public bool? IsLegalEntityGiveChange { get; set; }
        [Description("จ่ายด้วย")]
        public bool? IsLegalEntityPayWithMemo { get; set; }

        //เงินสดย่อย
        [Description("รวมรับเงินสดย่อย")]
        [Column(TypeName = "Money")]
        public decimal? PettyCashAmount { get; set; }
        [Description("ค่าเดินทางไป")]
        [Column(TypeName = "Money")]
        public decimal? GoTransportAmount { get; set; }
        [Description("ค่าเดินทางกลับ")]
        [Column(TypeName = "Money")]
        public decimal? ReturnTransportAmount { get; set; }
        [Description("ค่าเดินทางระหว่าง สนง. ที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? LandOfficeTransportAmount { get; set; }
        [Description("ค่าทางด่วนไป")]
        [Column(TypeName = "Money")]
        public decimal? GoTollWayAmount { get; set; }
        [Description("ค่าทางด่วนกลับ")]
        [Column(TypeName = "Money")]
        public decimal? ReturnTollWayAmount { get; set; }
        [Description("ค่าทางด่วนระหว่าง สนง. ที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? LandOfficeTollWayAmount { get; set; }
        [Description("รับรองเจ้าหน้าที่")]
        [Column(TypeName = "Money")]
        public decimal? SupportOfficerAmount { get; set; }
        [Description("ค่าถ่ายเอกสาร")]
        [Column(TypeName = "Money")]
        public decimal? CopyDocumentAmount { get; set; }

        [Description("พร้อมโอน")]
        public bool IsReadyToTransfer { get; set; }
        [Description("ผู้กดพร้อมโอน")]
        public Guid? ReadyToTransferUserID { get; set; }
        [Description("วันที่พร้อมโอน")]
        public DateTime? ReadyToTransferDate { get; set; }

        [Description("ยืนยันโอนจริง")]
        public bool IsTransferConfirmed { get; set; }
        [Description("ผู้กดยืนยันโอนจริง")]
        public Guid? TransferConfirmedUserID { get; set; }
        [Description("วันที่ยืนยันโอนจริง")]
        public DateTime? TransferConfirmedDate { get; set; }

        [Description("นำส่งการเงิน")]
        public bool IsSentToFinance { get; set; }
        [Description("ผู้กดนำส่งการเงิน")]
        public Guid? SentToFinanceUserID { get; set; }
        [Description("วันที่นำส่งการเงิน")]
        public DateTime? SentToFinanceDate { get; set; }

        [Description("ยืนยันชำระเงิน")]
        public bool IsPaymentConfirmed { get; set; }
        [Description("ผู้กดยืนยันชำระเงิน")]
        public Guid? PaymentConfirmedUserID { get; set; }
        [Description("วันที่ยืนยันชำระเงิน")]
        public DateTime? PaymentConfirmedDate { get; set; }

        [Description("บัญชีอนุมัติ")]
        public bool IsAccountApproved { get; set; }
        [Description("ผู้กดบัญชีอนุมัติ")]
        public Guid? AccountApprovedUserID { get; set; }
        [Description("วันที่บัญชีอนุมัติ")]
        public DateTime? AccountApprovedDate { get; set; }
    }
}
