using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Database.Models.CTM;
using System.Collections.Generic;
using Database.Models.USR;
using Database.Models.ACC;

namespace Base.DTOs.FIN
{
    public class FeeCreditDebitCardDTO : BaseDTO
    {
        ///// <summary>
        ///// บริษัท
        ///// </summary>
        //public MST.CompanyDropdownDTO Company { get; set; }

        ///// <summary>
        ///// โครงการ
        ///// </summary>
        //public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// สถานะตรวจสอบ
        /// </summary>
        public bool FeeConfirmStatus { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// บัตรเครดิต,บัตรเดบิต
        /// </summary>
        public MST.MasterCenterDropdownDTO ReceiveType { get; set; }

        /// <summary>
        /// เครื่องรูดบัตร
        /// </summary>
        [Description("เครื่องรูดบัตร")]
        public MST.EDCDropdownDTO EDC { get; set; }

        /// <summary>
        /// ธนาคารเจ้าของบัตร/บัตรที่รูด
        /// </summary>
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// ประเภทบัตร (Visa, Master, JCB)
        /// </summary>
        [Description("ประเภทบัตร (Visa, Master, JCB")]
        public MST.MasterCenterDropdownDTO CreditCardType { get; set; }

        /// <summary>
        /// เลขที่บัตร
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string ReceiveNo { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// % ค่าธรรมเนียม
        /// </summary>
        [Description("เปอร์เซ็นต์ธรรมเนียม")]
        public decimal? FeePercent { get; set; }

        /// <summary>
        /// มูลค่าธรรมเนียม
        /// </summary>
        [Description("ค่าธรรมเนียม")]
        public decimal? FeeAmount { get; set; }

        /// <summary>
        /// มูลค่า Vat
        /// </summary>
        [Description("ภาษีมูลค่าเพิ่ม")]
        public double? Vat { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmount { get; set; }
        /// <summary>
        /// มูลค่าสุทธิ
        /// </summary>
        [Description("มูลค่าสุทธิ")]
        public decimal? FeeIncludingVat { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        [Description("สถานะนำฝาก")]
        public bool DepositStatus { get; set; }

        /// <summary>
        /// Key Type + ID
        /// </summary>
        public string KeyID { get; set; }
        public PostPIFeeDTO PostPI { get; set; }
        public class FeeCreditDebitQueryResult
        {
            // public PaymentCreditCard PaymentCreditCard { get; set; }
            //public PaymentDebitCard PaymentDebitCard { get; set; }
            public Company Company { get; set; }
            public Project Project { get; set; }
            public Unit Unit { get; set; }
            public MasterCenter CardType { get; set; }
            public ReceiptTempHeader ReceiptHeader { get; set; }
            //public bool Status { get; set; }


            public bool FeeConfirmStatus { get; set; }
            public string DepositNo { get; set; }
            public bool DepositStatus { get; set; }
            // public DateTime ReceiveDate { get; set; }
            //public MST.MasterCenterDTO ReceiveType { get; set; }
            public EDC EDC { get; set; }
            public Bank Bank { get; set; }
            public MasterCenter CreditCardType { get; set; }
            public string CardNo { get; set; }
            //public string ReceiveNo { get; set; }
            // public string UnitNo { get; set; }
            public decimal? FeePercent { get; set; }
            public decimal? FeeAmount { get; set; }
            public double? Vat { get; set; }
            public decimal? PayAmount { get; set; }
            public decimal? FeeIncludingVat { get; set; }
            public DateTime? Updated { get; set; }
            // public User UpdatedBy { get; set; }
            public User UpdatedBy { get; set; }
            //public DepositDetail Deposit { get; set; }
            public Guid ID { get; set; }

            public string PostPI { get; set; }
            public bool PostPIStatus { get; set; }
        }

        public static void SortBy(FeeCreditDebitCardSortByParam sortByParam, ref IQueryable<FeeCreditDebitQueryResult> query)
        {

            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case FeeCreditDebitCardSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNameTH);
                        else query = query.OrderByDescending(o => o.Project.ProjectNameTH);
                        break;
                    case FeeCreditDebitCardSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate);
                        else query = query.OrderByDescending(o => o.ReceiptHeader.ReceiveDate);
                        break;
                    case FeeCreditDebitCardSortBy.EDC:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.EDC.Bank.NameTH);
                        break;
                    case FeeCreditDebitCardSortBy.CreditDebitCardType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CreditCardType);
                        else query = query.OrderByDescending(o => o.CreditCardType);
                        break;
                    case FeeCreditDebitCardSortBy.CreditDebitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CardNo);
                        else query = query.OrderByDescending(o => o.CardNo);
                        break;
                    case FeeCreditDebitCardSortBy.ReceiveNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ReceiptHeader.ReceiptTempNo);
                        else query = query.OrderByDescending(o => o.CardNo);
                        break;
                    case FeeCreditDebitCardSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case FeeCreditDebitCardSortBy.FeePercent:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FeePercent);
                        else query = query.OrderByDescending(o => o.FeePercent);
                        break;
                    case FeeCreditDebitCardSortBy.FeeAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FeeAmount);
                        else query = query.OrderByDescending(o => o.FeeAmount);
                        break;
                    case FeeCreditDebitCardSortBy.PayAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PayAmount);
                        else query = query.OrderByDescending(o => o.PayAmount);
                        break;
                    case FeeCreditDebitCardSortBy.NetAmount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.FeeIncludingVat);
                        else query = query.OrderByDescending(o => o.FeeIncludingVat);
                        break;
                    case FeeCreditDebitCardSortBy.UpdatedDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Updated);
                        else query = query.OrderByDescending(o => o.Updated);
                        break;
                    case FeeCreditDebitCardSortBy.UpdatedByName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case FeeCreditDebitCardSortBy.DepositStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositStatus);
                        else query = query.OrderByDescending(o => o.DepositStatus);
                        break;
                    case FeeCreditDebitCardSortBy.DepositNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.DepositNo);
                        else query = query.OrderByDescending(o => o.DepositNo);
                        break;
                    default:
                        query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate).ThenBy(x => x.ReceiptHeader.ReceiptTempNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.ReceiptHeader.ReceiveDate).ThenBy(x => x.ReceiptHeader.ReceiptTempNo);
            }
        }

        public async Task ValidateAsync(DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();
            var newGuid = Guid.NewGuid();


            var PaymentCardType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentCardType" && x.Key == "1").FirstOrDefault();
            if (this.ReceiveType.Id == PaymentCardType.ID)
            {
                if (this.CreditCardType == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    var masterCenterModel = DB.MasterCenters.Where(o => o.ID == this.CreditCardType.Id).ToList() ?? new List<MasterCenter>();
                    if (!masterCenterModel.Any())
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            if (this.EDC != null)
            {
                var EDC = DB.EDCs.Where(o => o.ID == this.EDC.Id).ToList() ?? new List<EDC>();
                if (!EDC.Any())
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.EDC)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            else
            {
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.EDC)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.FeePercent == null || this.FeePercent < 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.FeePercent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.FeeAmount == null || this.FeeAmount < 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.FeeAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Vat == null || this.Vat < 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.Vat)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //if (this.FeeIncludingVat == null || this.FeePercent == 0)
            //{
            //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.FeeIncludingVat)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            if (this.DepositStatus == true)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(FeeCreditDebitCardDTO.DepositStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static FeeCreditDebitCardDTO CreateFromModel(FeeCreditDebitQueryResult model)
        {
            if (model != null)
            {
                FeeCreditDebitCardDTO result = new FeeCreditDebitCardDTO()
                {
                    //ReceiveDate = model.ReceiptHeader.ReceiptTempNo != null ? model.ReceiptHeader.ReceiveDate : null,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    EDC = EDCDropdownDTO.CreateFromModel(model.EDC),
                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType),
                    CardNo = model.CardNo ?? null,
                    UnitNo = model.Unit.UnitNo ?? null,
                    FeePercent = model.FeePercent,
                    FeeAmount = model.FeeAmount,
                    Vat = model.Vat,
                    PayAmount = model.PayAmount,
                    FeeIncludingVat = model.FeeIncludingVat,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy != null ? model.UpdatedBy.DisplayName : null,
                    DepositStatus = model.DepositStatus,
                    DepositNo = model.DepositNo,
                    FeeConfirmStatus = model.FeeConfirmStatus,
                    ReceiveNo = model.ReceiptHeader.ReceiptTempNo,
                    ReceiveType = MasterCenterDropdownDTO.CreateFromModel(model.CardType),
                    Id = model.ID,
                    KeyID = model.CardType.Key + "-" + model.ID.ToString(),
                    PostPI = new PostPIFeeDTO(),

                };

                if (model.PostPIStatus)
                {
                    result.PostPI.PostPIStatus = true;
                    result.PostPI.PostPINo = model.PostPI;
                }

                if (model.ReceiptHeader.ReceiptTempNo != null)
                    result.ReceiveDate = model.ReceiptHeader.ReceiveDate;
                else
                    result.ReceiveDate = null;

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
