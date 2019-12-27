using Database.Models.SAL;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.MST;
using Database.Models;
using ErrorHandling;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    public class CreditBankingDTO : BaseDTO
    {
        [Description("ใบจอง")]
        public BookingDropdownDTO Booking { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }


        [Description("สถาบันการเงิน")]
        public MasterCenterDropdownDTO FinancialInstitution { get; set; }
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        [Description("สาขา")]
        public BankBranchDropdownDTO BankBranch { get; set; }
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }

        [Description("ธนาคารอื่นๆ")]
        public string OtherBank { get; set; }

        [Description("สาขาอื่นๆ")]
        public string OtherBankBranch { get; set; }


        [Description("วันที่ขอสินเชื่อ")]
        public DateTime? LoanSubmitDate { get; set; }


        [Description("ยอดขอกู้")]
        public decimal LoanAmount { get; set; }
        [Description("ยอดอนุมัติ AP")]
        public decimal ApprovedLoanAPAmount { get; set; }
        [Description("เบี้ยประกัน")]
        public decimal InsuranceAmount { get; set; }
        [Description("เบี้ยประกันอัคคีภัย")]
        public decimal InsuranceOnFireAmount { get; set; }
        [Description("เงินหักล่วงหน้างวดแรก")]
        public decimal FirstDeductAmount { get; set; }
        [Description("เงินคืนลูกค้า")]
        public decimal ReturnCustomerAmount { get; set; }
        [Description("ยอดอนุมัติรวม (ธนาคาร)")]
        public decimal ApprovedAmount { get; set; }


        [Description("สถานะสินเชื่อ")]
        public MasterCenterDropdownDTO LoanStatus { get; set; }
        [Description("วันที่ทราบผล")]
        public DateTime? ResultDate { get; set; }


        [Description("สถานะการเลือกใช้ธนาคาร")]
        public bool? IsUseBank { get; set; }

        [Description("เหตุผลธนาคาร")]
        public Guid? BankReasonMasterCenterID { get; set; }
        public MasterCenterDropdownDTO BankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคาร")]
        public MasterCenterDropdownDTO UseBankReason { get; set; }
        [Description("เหตุผลการเลือกใช้ธนาคารอื่นๆ")]
        public string UseBankOtherReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคาร")]
        public MasterCenterDropdownDTO NotUseBankReason { get; set; }
        [Description("เหตุผลการเลือกไม่ใช้ธนาคารอื่นๆ")]
        public string NotUseBankOtherReason { get; set; }
        [Description("เหตุผลการปฏิเสธสถานะสินเชื่อ")]
        public MasterCenterDropdownDTO BankRejectReason { get; set; }
        [Description("เหตุผลการรอผลสถานะสินเชื่อ")]
        public MasterCenterDropdownDTO BankWaitingReason { get; set; }

        public static CreditBankingDTO CreateFromModel(CreditBanking model)
        {
            if (model != null)
            {
                var result = new CreditBankingDTO()
                {
                    Id = model.ID,
                    Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
                    Remark = model.Remark,
                    FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution),
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
                    OtherBank = model.OtherBank,
                    LoanSubmitDate = model.LoanSubmitDate,
                    LoanAmount = model.LoanAmount,
                    ApprovedLoanAPAmount = model.ApprovedLoanAPAmount,
                    InsuranceAmount = model.InsuranceAmount,
                    InsuranceOnFireAmount = model.InsuranceOnFireAmount,
                    FirstDeductAmount = model.FirstDeductAmount,
                    ReturnCustomerAmount = model.ReturnCustomerAmount,
                    ApprovedAmount = model.ApprovedAmount,
                    LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus),
                    ResultDate = model.ResultDate,
                    IsUseBank = model.IsUseBank,
                    BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason),
                    UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason),
                    UseBankOtherReason = model.UseBankOtherReason,
                    NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason),
                    NotUseBankOtherReason = model.NotUseBankOtherReason,
                    BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason),
                    BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason),
                    Province = ProvinceListDTO.CreateFromModel(model.BankBranch?.Province),
                    OtherBankBranch = model.OtherBankBranch
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<CreditBankingDTO> CreateFromModelAsync(models.SAL.CreditBanking model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                CreditBankingDTO result = new CreditBankingDTO()
                {
                    Id = model.ID,
                    Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
                    Remark = model.Remark,
                    FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution),
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
                    OtherBank = model.OtherBank,
                    LoanSubmitDate = model.LoanSubmitDate,
                    LoanAmount = model.LoanAmount,
                    ApprovedLoanAPAmount = model.ApprovedLoanAPAmount,
                    InsuranceAmount = model.InsuranceAmount,
                    InsuranceOnFireAmount = model.InsuranceOnFireAmount,
                    FirstDeductAmount = model.FirstDeductAmount,
                    ReturnCustomerAmount = model.ReturnCustomerAmount,
                    ApprovedAmount = model.ApprovedAmount,
                    LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus),
                    ResultDate = model.ResultDate,
                    IsUseBank = model.IsUseBank,
                    BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason),
                    UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason),
                    UseBankOtherReason = model.UseBankOtherReason,
                    NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason),
                    NotUseBankOtherReason = model.NotUseBankOtherReason,
                    BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason),
                    BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason),
                    Province = ProvinceListDTO.CreateFromModel(model.BankBranch?.Province),
                    OtherBankBranch = model.OtherBankBranch
                };

                return result;
            }
            else
            {
                return new CreditBankingDTO();
            }
        }
        //public static CreditBankingDTO CreateFromQueryResult(CreditBankingQueryResult model)
        //{
        //    if (model != null)
        //    {
        //        var result = new CreditBankingDTO()
        //        {
        //            Id = model.CreditBanking?.ID,
        //            Booking = SAL.BookingDropdownDTO.CreateFromModel(model.Booking),
        //            Remark = model.CreditBanking.Remark,
        //            FinancialInstitution = MST.MasterCenterDropdownDTO.CreateFromModel(model.FinancialInstitution),
        //            Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
        //            BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch),
        //            OtherBank = model.CreditBanking.OtherBank,
        //            LoanSubmitDate = model.CreditBanking.LoanSubmitDate,
        //            LoanAmount = model.CreditBanking.LoanAmount,
        //            ApprovedLoanAPAmount = model.CreditBanking.ApprovedLoanAPAmount,
        //            InsuranceAmount = model.CreditBanking.InsuranceAmount,
        //            InsuranceOnFireAmount = model.CreditBanking.InsuranceOnFireAmount,
        //            FirstDeductAmount = model.CreditBanking.FirstDeductAmount,
        //            ReturnCustomerAmount = model.CreditBanking.ReturnCustomerAmount,
        //            ApprovedAmount = model.CreditBanking.ApprovedAmount,
        //            LoanStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.LoanStatus),
        //            ResultDate = model.CreditBanking.ResultDate,
        //            IsUseBank = model.CreditBanking.IsUseBank,
        //            BankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankReason),
        //            UseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.UseBankReason),
        //            UseBankOtherReason = model.CreditBanking.UseBankOtherReason,
        //            NotUseBankReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.NotUseBankReason),
        //            NotUseBankOtherReason = model.CreditBanking.NotUseBankOtherReason,
        //            BankRejectReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankRejectReason),
        //            BankWaitingReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.BankWaitingReason)
        //        };

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            if (this.Bank == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CreditBankingDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref CreditBanking model)
        {
            model.BookingID = this.Booking.Id;
            model.Remark = this.Remark;
            model.FinancialInstitutionMasterCenterID = this.FinancialInstitution?.Id;
            model.BankID = this.Bank?.Id;
            model.BankBranchID = this.BankBranch?.Id;
            model.OtherBank = this.OtherBank;
            model.LoanSubmitDate = this.LoanSubmitDate;
            model.LoanAmount = this.LoanAmount;
            model.ApprovedLoanAPAmount = this.ApprovedLoanAPAmount;
            model.InsuranceAmount = this.InsuranceAmount;
            model.InsuranceOnFireAmount = this.InsuranceOnFireAmount;
            model.FirstDeductAmount = this.FirstDeductAmount;
            model.ReturnCustomerAmount = this.ReturnCustomerAmount;
            model.ApprovedAmount = this.ApprovedAmount;
            model.LoanStatusMasterCenterID = this.LoanStatus?.Id;
            model.ResultDate = this.ResultDate;
            model.IsUseBank = this.IsUseBank;
            model.BankReasonMasterCenterID = this.BankReason?.Id;
            model.UseBankReasonMasterCenterID = this.UseBankReason?.Id;
            model.UseBankOtherReason = this.UseBankOtherReason;
            model.NotUseBankReasonMasterCenterID = this.NotUseBankReason?.Id;
            model.NotUseBankOtherReason = this.NotUseBankOtherReason;
            model.BankRejectReasonMasterCenterID = this.BankRejectReason?.Id;
            model.BankWaitingReasonMasterCenterID = this.BankWaitingReason?.Id;
            model.OtherBankBranch = this.OtherBankBranch;
        }
    }

    //public class CreditBankingQueryResult
    //{
    //    public CreditBanking CreditBanking { get; set; }
    //    public Booking Booking { get; set; }
    //    public Bank Bank { get; set; }
    //    public BankBranch BankBranch { get; set; }
    //    public MasterCenter FinancialInstitution { get; set; }
    //    public MasterCenter LoanStatus { get; set; }
    //    public MasterCenter BankReason { get; set; }
    //    public MasterCenter UseBankReason { get; set; }
    //    public MasterCenter NotUseBankReason { get; set; }
    //    public MasterCenter BankRejectReason { get; set; }
    //    public MasterCenter BankWaitingReason { get; set; }
    //}
}
