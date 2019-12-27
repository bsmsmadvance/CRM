using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class DepositDetailDTO : BaseDTO
    {
        /// <summary>
        /// สถานะเลือกรายการ
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// ข้อมูล DepositHeader
        /// </summary>
        public DepositHeaderDTO DepositHeader { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// ข้อมูลใบเสร็จ
        /// </summary>
        public PaymentMethodDTO PaymentMethod { get; set; }

        /// <summary>
        /// ประเภทการชำระ
        /// </summary>
        public string PaymenyMethodItemText { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// จำนวนเงินที่นำฝาก
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Vat
        /// </summary>
        public decimal Vat { get; set; }


        public static void SortBy(DepositSortByParam sortByParam, ref IQueryable<DepositDetailQueryResult> query)
        {
            IOrderedQueryable<DepositDetailQueryResult> orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);

            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case DepositSortBy.Project:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case DepositSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case DepositSortBy.ReceiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);
                        else orderQuery = query.OrderByDescending(o => o.ReceiptTempHeader.ReceiveDate);
                        break;
                    case DepositSortBy.ReceiptTempNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiptTempNo);
                        else orderQuery = query.OrderByDescending(o => o.ReceiptTempHeader.ReceiptTempNo);
                        break;
                    //case DepositSortBy.Paymentype:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.BG.Updated);
                    //    else query = query.OrderByDescending(o => o.BG.Updated);
                    //    break;
                    case DepositSortBy.TotalAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Payment.TotalAmount);
                        else orderQuery = query.OrderByDescending(o => o.Payment.TotalAmount);
                        break;
                    case DepositSortBy.Fee:

                        if (query.Where(e => e.PaymentMethodType.Key == "3").FirstOrDefault() != null)   // Credit
                        {
                            if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentCreditCard.Fee);
                            else orderQuery = query.OrderByDescending(o => o.PaymentCreditCard.Fee);
                        }

                        if (query.Where(e => e.PaymentMethodType.Key == "13").FirstOrDefault() != null) // Debit   
                        {
                            if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentDebitCard.Fee);
                            else orderQuery = query.OrderByDescending(o => o.PaymentDebitCard.Fee);
                        }

                        if (query.Where(e => e.PaymentMethodType.Key == "2").FirstOrDefault() != null)  // แคชเชียร์เช็ค
                        {
                            if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentCashierCheque.Fee);
                            else orderQuery = query.OrderByDescending(o => o.PaymentCashierCheque.Fee);
                        }

                        if (query.Where(e => e.PaymentMethodType.Key == "4").FirstOrDefault() != null)  // เช็คส่วนตัว
                        {
                            if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentPersonalCheque.Fee);
                            else orderQuery = query.OrderByDescending(o => o.PaymentPersonalCheque.Fee);
                        }

                        break;
                    case DepositSortBy.Vat:
                        if (query.Where(e => e.PaymentMethod.PaymentMethodType.Key == "3").FirstOrDefault() != null)
                        {
                            if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PaymentCreditCard.FeeIncludingVat);
                            else orderQuery = query.OrderByDescending(o => o.PaymentCreditCard.FeeIncludingVat);
                        }
                        break;

                    //case DepositSortBy.DepositStatus:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                    //    else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                    //    break;
                    //case DepositSortBy.PostStatus:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                    //    else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                    //    break;
                    //case DepositSortBy.PINumber:
                    //    if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                    //    else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                    //    break;
                    default:
                        orderQuery = query.OrderBy(o => o.ReceiptTempHeader.ReceiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Project.ProjectNo);
            }

            query = orderQuery;
        }

        public static DepositDetailDTO CreateFromQueryResult(DepositDetailQueryResult model, DatabaseContext DB)
        {
            if (model != null)
            {
                DepositDetailDTO result = new DepositDetailDTO()
                {
                    IsSelected = false,
                    DepositHeader = DepositHeaderDTO.CreateFromDepositDetailQueryResult(model),
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    PaymentMethod = new PaymentMethodDTO
                    {
                        Id = model.PaymentMethod.ID,
                        PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(model.PaymentMethodType),
                        PayAmount = model.PaymentMethod.PayAmount
                    },

                    PayAmount = model.Payment.TotalAmount,
                    ReceiptTempNo = model.ReceiptTempHeader.ReceiptTempNo,
                    ReceiveDate = model.ReceiptTempHeader.ReceiveDate
                };

                var PaymentItemText = "";

                var PaymentItemModel = DB.PaymentItems.Where(o => o.PaymentID == model.Payment.ID)
                        .Include(o => o.MasterPriceItem).ToList() ?? new List<PaymentItem>();

                if (PaymentItemModel.Any())
                {
                    foreach (var item in PaymentItemModel)
                    {
                        PaymentItemText += "," + item.MasterPriceItem.Detail;
                    }
                }

                PaymentItemText = (PaymentItemText.Length > 2) ? PaymentItemText.Substring(1, PaymentItemText.Length - 1) : PaymentItemText;

                result.PaymenyMethodItemText = PaymentItemText;

                if (model.PaymentMethodType.Key == "3") // Credit
                {
                    result.Fee = model.PaymentCreditCard.Fee;
                    result.Vat = model.PaymentCreditCard.FeeIncludingVat;
                }

                if (model.PaymentMethodType.Key == "13")  // Debit
                    result.Fee = model.PaymentDebitCard.Fee;
                if (model.PaymentMethodType.Key == "2")  // แคชเชียร์เช็ค
                    result.Fee = model.PaymentCashierCheque.Fee;
                if (model.PaymentMethodType.Key == "4")  // เช็คส่วนตัว
                    result.Fee = model.PaymentPersonalCheque.Fee;

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task ValidateAsync(DatabaseContext DB, List<DepositDetailDTO> Model)
        {
            ValidateException ex = new ValidateException();

            var PaymentMethodType = Model.Select(o => o.PaymentMethod.PaymentMethodType).GroupBy(o => o.Id).ToList() ?? new List<IGrouping<Guid, MasterCenterDropdownDTO>>();

            var Company = Model.Select(o => o.DepositHeader.Company).GroupBy(o => o.Id).ToList() ?? new List<IGrouping<Guid?, CompanyDTO>>();
            var BankAccount = Model.Select(o => o.DepositHeader.BankAccount).GroupBy(o => o.Id).ToList() ?? new List<IGrouping<Guid?, BankAccountDropdownDTO>>();

            if (PaymentMethodType.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().PaymentMethod.GetType().GetProperty(nameof(PaymentMethodDTO.PaymentMethodType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (Company.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().DepositHeader.GetType().GetProperty(nameof(Database.Models.MST.Company)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (BankAccount.Count > 1)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0090").FirstAsync();
                string desc = Model.FirstOrDefault().DepositHeader.GetType().GetProperty(nameof(Database.Models.MST.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            /* ** วันที่ปิดบัญชี ** */

            if (ex.HasError)
            {
                throw ex;
            }
        }

    }

    public class DepositDetailQueryResult
    {
        public MasterCenter PaymentMethodType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Payment Payment { get; set; }
        public Booking Booking { get; set; }
        public Project Project { get; set; }
        public Unit Unit { get; set; }
        public Company Company { get; set; }

        public PaymentCashierCheque PaymentCashierCheque { get; set; }
        public PaymentPersonalCheque PaymentPersonalCheque { get; set; }
        public PaymentCreditCard PaymentCreditCard { get; set; }
        public PaymentDebitCard PaymentDebitCard { get; set; }
        public PaymentBankTransfer PaymentBankTransfer { get; set; }
        public PaymentQRCode PaymentQRCode { get; set; }

        public ReceiptTempHeader ReceiptTempHeader { get; set; }

        public DepositHeader DepositHeader { get; set; }
        public DepositDetail DepositDetail { get; set; }

        public BankAccount BankAccount { get; set; }

    }

    public class PaymentMethodToItemQueryResult
    {
        public Payment Payment { get; set; }
        public PaymentItem PaymentItem { get; set; }
        public MasterPriceItem MasterPriceItem { get; set; }
    }
}