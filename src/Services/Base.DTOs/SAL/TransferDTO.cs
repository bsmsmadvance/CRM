using Database.Models;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        /// <summary>
        /// เลขที่โอนกรรมสิทธิ์
        /// </summary>
        public string TransferNo { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// พื้นที่ (ตร.ว/ตร.ม)
        /// </summary>
        public double? StandardArea { get; set; }
        /// <summary>
        /// พื้นที่ที่ใช้คำนวนราคาประเมิณ
        /// </summary>
        public double? LandArea { get; set; }
        /// <summary>
        /// ราคาประเมิณ
        /// </summary>
        public decimal? LandEstimatePrice { get; set; }
        /// <summary>
        /// LC โอน
        /// </summary>
        public USR.UserListDTO TransferSale { get; set; }
        /// <summary>
        /// วันที่นัดโอนกรรมสิทธื์
        /// </summary>
        public DateTime? ScheduleTransferDate { get; set; }
        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        public DateTime? ActualTransferDate { get; set; }
        /// <summary>
        /// ภาษีเงินได้นิติบุคคล
        /// </summary>
        public decimal? CompanyIncomeTax { get; set; }
        /// <summary>
        /// ภาษีเงินได้ธุรกิจเฉพาะ
        /// </summary>
        public decimal? BusinessTax { get; set; }
        /// <summary>
        /// ภาษีท้องถิ่น
        /// </summary>
        public decimal? LocalTax { get; set; }
        /// <summary>
        /// รูดบัตร P.Card กระทรวงการคลัง
        /// </summary>
        public decimal? MinistryPCard { get; set; }
        /// <summary>
        /// เงินสดหรือเช็คกระทรวงการคลัง
        /// </summary>
        public decimal? MinistryCashOrCheque { get; set; }
        /// <summary>
        /// เช็คค่ามิเตอร์
        /// </summary>
        public MST.MasterCenterDropdownDTO MeterCheque { get; set; }
        /// <summary>
        /// ลูกค้าจ่ายค่าจดจำนอง
        /// </summary>
        public decimal? CustomerPayMortgage { get; set; }
        /// <summary>
        /// ลูกค้าจ่ายค่าธรรมเนียม
        /// </summary>
        public decimal? CustomerPayFee { get; set; }
        /// <summary>
        /// บริษัทจ่ายค่าธรรมเนียม
        /// </summary>
        public decimal? CompanyPayFee { get; set; }
        /// <summary>
        /// ฟรีค่าธรรมเนียม
        /// </summary>
        public decimal? FreeFee { get; set; }
        /// <summary>
        /// ค่าดำเนินการเอกสาร (ขาด/เกิน)
        /// </summary>
        public decimal? DocumentFee { get; set; }
        /// <summary>
        /// ยอดคงเหลือ AP
        /// </summary>
        public decimal? APBalance { get; set; }
        /// <summary>
        /// ยกยอดไปนิติบุคคล
        /// </summary>
        public bool? IsAPBalanceTransfer { get; set; }
        /// <summary>
        /// ยอดคงเหลือ AP ยกยอดไปนิติบุคคล
        /// </summary>
        public decimal? APBalanceTransfer { get; set; }
        /// <summary>
        /// เงินทอนก่อนโอน
        /// </summary>
        public decimal? APChangeAmountBeforeTransfer { get; set; }
        /// <summary>
        /// รวมเงินทอน
        /// </summary>
        public decimal? APChangeAmount { get; set; }
        /// <summary>
        /// การทอนคืน AP
        /// </summary>
        public bool? IsAPGiveChange { get; set; }
        /// <summary>
        /// จ่ายด้วย
        /// </summary>
        public bool? IsAPPayWithMemo { get; set; }
        /// <summary>
        /// ยอดคงเหลือนิติบุคคล
        /// </summary>
        public decimal? LegalEntityBalance { get; set; }
        /// <summary>
        /// ยกยอดไป AP
        /// </summary>
        public bool? IsLegalEntityBalanceTransfer { get; set; }
        /// <summary>
        /// ยอดคงเหลือนิติบุคคล ยกยอดไป AP
        /// </summary>
        public decimal? LegalEntityBalanceTransfer { get; set; }
        /// <summary>
        /// การทอนคืนนิติบุคคล
        /// </summary>
        public bool? IsLegalEntityGiveChange { get; set; }
        /// <summary>
        /// จ่ายด้วย
        /// </summary>
        public bool? IsLegalEntityPayWithMemo { get; set; }
        /// <summary>
        /// รวมรับเงินสดย่อย
        /// </summary>
        public decimal? PettyCashAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางไป
        /// </summary>
        public decimal? GoTransportAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางกลับ
        /// </summary>
        public decimal? ReturnTransportAmount { get; set; }
        /// <summary>
        /// ค่าเดินทางระหว่าง สนง. ที่ดิน
        /// </summary>
        public decimal? LandOfficeTransportAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนไป
        /// </summary>
        public decimal? GoTollWayAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนกลับ
        /// </summary>
        public decimal? ReturnTollWayAmount { get; set; }
        /// <summary>
        /// ค่าทางด่วนระหว่าง สนง. ที่ดิน
        /// </summary>
        public decimal? LandOfficeTollWayAmount { get; set; }
        /// <summary>
        /// รับรองเจ้าหน้าที่
        /// </summary>
        public decimal? SupportOfficerAmount { get; set; }
        /// <summary>
        /// ค่าถ่ายเอกสาร
        /// </summary>
        public decimal? CopyDocumentAmount { get; set; }
        /// <summary>
        /// พร้อมโอน
        /// </summary>
        public bool? IsReadyToTransfer { get; set; }
        /// <summary>
        /// วันที่พร้อมโอน
        /// </summary>
        public DateTime? ReadyToTransferDate { get; set; }
        /// <summary>
        /// ยืนยันโอนจริง
        /// </summary>
        public bool? IsTransferConfirmed { get; set; }
        /// <summary>
        /// ผู้กดยืนยันโอนจริง
        /// </summary>
        public Guid? TransferConfirmedUserID { get; set; }
        /// <summary>
        /// วันที่ยืนยันโอนจริง
        /// </summary>
        public DateTime? TransferConfirmedDate { get; set; }
        /// <summary>
        /// นำส่งการเงิน
        /// </summary>
        public bool? IsSentToFinance { get; set; }
        /// <summary>
        /// วันที่นำส่งการเงิน
        /// </summary>
        public DateTime? SentToFinanceDate { get; set; }
        /// <summary>
        /// ยืนยันชำระเงิน
        /// </summary>
        public bool? IsPaymentConfirmed { get; set; }
        /// <summary>
        /// วันที่ยืนยันชำระเงิน
        /// </summary>
        public DateTime? PaymentConfirmedDate { get; set; }
        /// <summary>
        /// บัญชีอนุมัติ
        /// </summary>
        public bool? IsAccountApproved { get; set; }
        /// <summary>
        /// วันที่บัญชีอนุมัติ
        /// </summary>
        public DateTime? AccountApprovedDate { get; set; }

        ///// <summary>
        ///// มีข้อมูลโฉนดแล้ว
        ///// </summary>
        //public bool IsTitledeedNo { get; set; }
        ///// <summary>
        ///// มีข้อมูลขอสินเชื่อ
        ///// </summary>
        //public bool IsCreditBanking { get; set; }
        ///// <summary>
        ///// มีข้อมูล QC Pass
        ///// </summary>
        //public bool IsWaiveQC { get; set; }
        ///// <summary>
        ///// มีข้อมูลตรวจรับบ้านแล้ว
        ///// </summary>
        //public bool IsWaiveSign { get; set; }
        ///// <summary>
        ///// ยังไม่มีตั้งเรื่องโอน
        ///// </summary>
        //public bool IsNotTransfer { get; set; }


        //public class TransferQueryResult
        //{
        //    public Transfer Transfer { get; set; }
        //    public UnitPrice UnitPrice { get; set; }
        //    public UnitPriceItem UnitPriceItem { get; set; }
        //    public AgreementOwner AgreementOwner { get; set; }
        //    public TransferOwner TransferOwner { get; set; }
        //    public List<TransferOwner> TransferOwnerList { get; set; }
        //}

        public static TransferDTO CreateFromModel(Transfer model)
        {
            var result = new TransferDTO();

            result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
            result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit);
            result.TransferNo = model.TransferNo;
            result.Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement);
            result.StandardArea = model.StandardArea;
            result.LandArea = model.LandArea;
            result.LandEstimatePrice = model.LandEstimatePrice;
            result.TransferSale = USR.UserListDTO.CreateFromModel(model.TransferSale);
            result.ScheduleTransferDate = model.ScheduleTransferDate;
            result.ActualTransferDate = model.ActualTransferDate;
            result.CompanyIncomeTax = model.CompanyIncomeTax;
            result.BusinessTax = model.BusinessTax;
            result.LocalTax = model.LocalTax;
            result.MinistryPCard = model.MinistryPCard;
            result.MinistryCashOrCheque = model.MinistryCashOrCheque;
            result.MeterCheque = MST.MasterCenterDropdownDTO.CreateFromModel(model.MeterCheque);
            result.CustomerPayMortgage = model.CustomerPayMortgage;
            result.CustomerPayFee = model.CustomerPayFee;
            result.CompanyPayFee = model.CompanyPayFee;
            result.FreeFee = model.FreeFee;
            result.DocumentFee = model.DocumentFee;
            result.APBalance = model.APBalance;
            result.IsAPBalanceTransfer = model.IsAPBalanceTransfer;
            result.APBalanceTransfer = model.APBalanceTransfer;
            result.APChangeAmountBeforeTransfer = model.APChangeAmountBeforeTransfer;
            result.APChangeAmount = model.APChangeAmount;
            result.IsAPGiveChange = model.IsAPGiveChange;
            result.IsAPPayWithMemo = model.IsAPPayWithMemo;
            result.LegalEntityBalance = model.LegalEntityBalance;
            result.IsLegalEntityBalanceTransfer = model.IsLegalEntityBalanceTransfer;
            result.LegalEntityBalanceTransfer = model.LegalEntityBalanceTransfer;
            result.IsLegalEntityGiveChange = model.IsLegalEntityGiveChange;
            result.IsLegalEntityPayWithMemo = model.IsLegalEntityPayWithMemo;
            result.PettyCashAmount = model.PettyCashAmount;
            result.GoTransportAmount = model.GoTransportAmount;
            result.ReturnTransportAmount = model.ReturnTransportAmount;
            result.LandOfficeTransportAmount = model.LandOfficeTransportAmount;
            result.GoTollWayAmount = model.GoTollWayAmount;
            result.ReturnTollWayAmount = model.ReturnTollWayAmount;
            result.LandOfficeTollWayAmount = model.LandOfficeTollWayAmount;
            result.SupportOfficerAmount = model.SupportOfficerAmount;
            result.CopyDocumentAmount = model.CopyDocumentAmount;
            result.IsReadyToTransfer = model.IsReadyToTransfer;
            result.ReadyToTransferDate = model.ReadyToTransferDate;
            result.IsTransferConfirmed = model.IsTransferConfirmed;
            result.TransferConfirmedUserID = model.TransferConfirmedUserID;
            result.TransferConfirmedDate = model.TransferConfirmedDate;
            result.IsSentToFinance = model.IsSentToFinance;
            result.SentToFinanceDate = model.SentToFinanceDate;
            result.IsPaymentConfirmed = model.IsPaymentConfirmed;
            result.PaymentConfirmedDate = model.PaymentConfirmedDate;
            result.IsAccountApproved = model.IsAccountApproved;
            result.AccountApprovedDate = model.AccountApprovedDate;

            return result;

        }

        public async static Task<TransferDTO> CreateFromAgreementModelAsync(Agreement model, DatabaseContext DB)
        {
            model = model ?? new Agreement();
            model.Project = model.Project ?? new Database.Models.PRJ.Project();
            model.Unit = model.Unit ?? new Database.Models.PRJ.Unit();
            model.Booking = model.Booking ?? new Booking();

            //var titledeed = await DB.TitledeedDetails.Where(o => o.UnitID == model.UnitID).FirstOrDefaultAsync() ?? new Database.Models.PRJ.TitledeedDetail();

            var result = new TransferDTO();

            result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project);
            result.Unit = PRJ.UnitDTO.CreateFromModel(model.Unit);
            result.TransferNo = "";
            result.Agreement = AgreementDropdownDTO.CreateFromModel(model);
            //result.LandEstimatePrice = titledeed.EstimatePrice;
            result.TransferSale = USR.UserListDTO.CreateFromModel(model.Booking.SaleUser);
            result.ScheduleTransferDate = model.TransferOwnershipDate;
            result.ActualTransferDate = null;

            var transferFee = await DB.TransferFeeResults.Where(o => o.AgreementID == model.ID).FirstOrDefaultAsync();

            if (transferFee != null)
            {
                result.CompanyIncomeTax = transferFee.CompanyIncomeTax; //ภาษีเงินได้นิติบุคคล
                result.BusinessTax = transferFee.BusinessTax;
                result.LocalTax = transferFee.LocalTax;
                result.StandardArea = transferFee.SaleArea;
                result.LandArea = transferFee.UsedArea;
                result.LandEstimatePrice = transferFee.EstimateTotalPrice;
            } 

            result.MinistryPCard = 0;
            result.MinistryCashOrCheque = 0;//เงินสดหรือเช็คกระทรวงการคลัง

            //result.MeterCheque = MST.MasterCenterDropdownDTO.CreateFromModel(model.MeterCheque);
            //result.CustomerPayMortgage = model.CustomerPayMortgage;
            //result.CustomerPayFee = model.CustomerPayFee;
            //result.CompanyPayFee = model.CompanyPayFee;

            //result.FreeFee = model.FreeFee;
            //result.DocumentFee = model.DocumentFee;
            //result.APBalance = model.APBalance;
            //result.IsAPBalanceTransfer = model.IsAPBalanceTransfer;
            //result.APBalanceTransfer = model.APBalanceTransfer;
            //result.APChangeAmountBeforeTransfer = model.APChangeAmountBeforeTransfer;
            //result.APChangeAmount = model.APChangeAmount;
            //result.IsAPGiveChange = model.IsAPGiveChange;
            //result.IsAPPayWithMemo = model.IsAPPayWithMemo;
            //result.LegalEntityBalance = model.LegalEntityBalance;
            //result.IsLegalEntityBalanceTransfer = model.IsLegalEntityBalanceTransfer;
            //result.LegalEntityBalanceTransfer = model.LegalEntityBalanceTransfer;
            //result.IsLegalEntityGiveChange = model.IsLegalEntityGiveChange;
            //result.IsLegalEntityPayWithMemo = model.IsLegalEntityPayWithMemo;
            //result.PettyCashAmount = model.PettyCashAmount;
            //result.GoTransportAmount = model.GoTransportAmount;
            //result.ReturnTransportAmount = model.ReturnTransportAmount;
            //result.LandOfficeTransportAmount = model.LandOfficeTransportAmount;
            //result.GoTollWayAmount = model.GoTollWayAmount;
            //result.ReturnTollWayAmount = model.ReturnTollWayAmount;
            //result.LandOfficeTollWayAmount = model.LandOfficeTollWayAmount;
            //result.SupportOfficerAmount = model.SupportOfficerAmount;
            //result.CopyDocumentAmount = model.CopyDocumentAmount;
            result.IsReadyToTransfer = false;
            //result.ReadyToTransferDate = model.ReadyToTransferDate;
            result.IsTransferConfirmed = false;
            //result.TransferConfirmedUserID = model.TransferConfirmedUserID;
            //result.TransferConfirmedDate = model.TransferConfirmedDate;
            result.IsSentToFinance = false;
            //result.SentToFinanceDate = model.SentToFinanceDate;
            result.IsPaymentConfirmed = false;
            //result.PaymentConfirmedDate = model.PaymentConfirmedDate;
            result.IsAccountApproved = false;
            //result.AccountApprovedDate = model.AccountApprovedDate;

            return result;

        }

        public async Task ValidateAsync(DatabaseContext db)
        {

            ValidateException ex = new ValidateException();
            if (!this.ScheduleTransferDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferDTO.ScheduleTransferDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else if (this.TransferSale == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferDTO.ScheduleTransferDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Transfer model)
        {
            model = model ?? new Transfer();

            model.ScheduleTransferDate = this.ScheduleTransferDate;
            model.TransferSaleUserID = this.TransferSale?.Id;

            model.ProjectID = this.Project?.Id;
            model.UnitID = this.Unit?.Id;
            model.AgreementID = this.Agreement?.Id;
            model.MeterChequeMasterCenterID = this.MeterCheque?.Id;

        }

        public class api_GetUnitEndProductDate_Param
        {
            public string ProjectId { get; set; }
            public string UnitNumber { get; set; }


        }

        public class api_GetUnitEndProductDate_Response
        {
            public bool? success { get; set; }
            public string messages { get; set; }
            public List<EndProductUnit> data { get; set; }
        }
        public class EndProductUnit
        {
            public string ProductID { get; set; }
            public string UnitNumber { get; set; }
            public string WBSNumber { get; set; }
            public string SAPCode { get; set; }
            public DateTime? NProductDate { get; set; }
        }

        public class api_GetUnitReceiveDate_Param
        {
            public string ProjectId { get; set; }
            public string UnitNumber { get; set; }

        }

        public class api_GetUnitReceiveDate_Response
        {
            public bool? success { get; set; }
            public string messages { get; set; }
            public List<DefectReceiveUnit> data { get; set; }
        }
        public class DefectReceiveUnit
        {
            public string ProjectNo { get; set; }
            public string SerialNo { get; set; }
            public string RAuditNo { get; set; }
            public DateTime? DocOpenDate { get; set; }
            public string TDefectDocNo { get; set; }
            public DateTime? DocReceiveUnitDate { get; set; }
            public string ContactID { get; set; }
            public string ContactName { get; set; }
            public bool? DocIsActive { get; set; }
        }

    }
}