using Base.DTOs.FIN;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Services.IService;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using PagingExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.FIN.FETDTO;
using Base.DTOs;
using FileStorage;
using Base.DTOs.PRJ;
using Database.Models.SAL;
using Database.Models.PRJ;
using Microsoft.Extensions.Configuration;
using static Base.DTOs.FIN.FeeChequeDTO;
using ErrorHandling;

namespace Finance.Services.Service
{
    public class FeeChequeService : IFeeChequeService
    {
        private readonly DatabaseContext DB;
        public FeeChequeService(DatabaseContext db)
        {
            DB = db;
        }

        public async Task<FeeChequePaging> GetFeeChequeListAsync(FeeChequeFilter filter, PageParam pageParam, FeeChequeSortByParam sortByParam)
        {
            #region GetData
            var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentMethod").ToList();
            var PaymentCashierCheque = PaymentMethod.Where(x => x.Key == "2").FirstOrDefault();
            var PaymentPersonalCheque = PaymentMethod.Where(x => x.Key == "4").FirstOrDefault();
           // var PaymentPostDateCheque = PaymentMethod.Where(x => x.Key == "5").FirstOrDefault();

            IQueryable<FFeeChequeQueryResult> queryCashier = from o in DB.PaymentCashierCheques
                                                                           .Include(x => x.PaymentMethod)
                                                                                   .ThenInclude(x => x.Payment)
                                                                                       .ThenInclude(x => x.Booking)
                                                                                            .ThenInclude(x => x.Unit)
                                                                                                .ThenInclude(x => x.Project)
                                                                                                    .ThenInclude(x => x.Company)
                                                                           .Include(x => x.BankBranch)
                                                                           .Include(x => x.PayToCompany)
                                                                           .Include(x => x.UpdatedBy)
                                                                           .Include(x => x.FeeConfirmByUser)
                                                                           .Include(x => x.Bank)

                                                             join Receipt in DB.ReceiptTempHeaders on o.PaymentMethod.PaymentID equals Receipt.PaymentID into ReceiptGroup
                                                             from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                             join Deposit in DB.DepositDetails.Include(x => x.DepositHeader) on o.PaymentMethodID equals Deposit.PaymentMethodID into DepositGroup
                                                             from DepositModel in DepositGroup.DefaultIfEmpty()

                                                             join PostGL in DB.PostGLHeaders.Where(x => x.ReferentType == "DepositHeader") on DepositModel.ID equals PostGL.ReferentID into PostGLGroup
                                                             from PostGLModel in PostGLGroup.DefaultIfEmpty()

                                                             select new FFeeChequeQueryResult
                                                             {
                                                                 Company = o.PaymentMethod.Payment.Booking.Unit.Project.Company,
                                                                 Project = o.PaymentMethod.Payment.Booking.Unit.Project,
                                                                 Unit = o.PaymentMethod.Payment.Booking.Unit,
                                                                 Bank = o.Bank,
                                                                 BankBranch = o.BankBranch,
                                                                 PayToCompany = o.PayToCompany,
                                                                 ChequeDate = o.ChequeDate,
                                                                 ChequeNo = o.ChequeNo,
                                                                 ChequeType = PaymentCashierCheque ?? new MasterCenter(),
                                                                 Fee = o.Fee,
                                                                 PaymentMethod = o.PaymentMethod,
                                                                 FeeConfirmByUser = o.FeeConfirmByUser,
                                                                 FeeConfirmDate = o.FeeConfirmDate,
                                                                 IsFeeConfirm = o.IsFeeConfirm,
                                                                 IsWrongCompany = o.IsWrongCompany,
                                                                 ReceiptHeader = ReceiptModel ?? new ReceiptTempHeader(),
                                                                 DepositStatus = DepositModel != null ? true : false,
                                                                 DepositNo = DepositModel != null ? DepositModel.DepositHeader.DepositNo : null,
                                                                 FeePercent = o.FeePercent,
                                                                 FeeIncludingVat = o.FeeIncludingVat == 0 ? (o.PaymentMethod.PayAmount - o.Fee) : o.FeeIncludingVat,
                                                                 ID = o.ID,
                                                                 Updated = o.Updated,
                                                                 UpdatedBy = o.UpdatedBy,
                                                                 PostPI = PostGLModel != null ? PostGLModel.DocumentNo : null,
                                                                 PostPIStatus = PostGLModel != null ? true : false
                                                             };

            IQueryable<FFeeChequeQueryResult> queryPersonalCheques = from o in DB.PaymentPersonalCheques
                                                                          .Include(x => x.PaymentMethod)
                                                                                  .ThenInclude(x => x.Payment)
                                                                                      .ThenInclude(x => x.Booking)
                                                                                           .ThenInclude(x => x.Unit)
                                                                                               .ThenInclude(x => x.Project)
                                                                                                   .ThenInclude(x => x.Company)
                                                                          .Include(x => x.BankBranch)
                                                                          .Include(x => x.PayToCompany)
                                                                          .Include(x => x.UpdatedBy)
                                                                          .Include(x => x.FeeConfirmByUser)
                                                                          .Include(x => x.Bank)


                                                                     join Receipt in DB.ReceiptTempHeaders on o.PaymentMethod.PaymentID equals Receipt.PaymentID into ReceiptGroup
                                                                     from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                                     join Deposit in DB.DepositDetails.Include(x => x.DepositHeader) on o.PaymentMethodID equals Deposit.PaymentMethodID into DepositGroup
                                                                     from DepositModel in DepositGroup.DefaultIfEmpty()

                                                                     select new FFeeChequeQueryResult
                                                                     {
                                                                         Company = o.PaymentMethod.Payment.Booking.Unit.Project.Company,
                                                                         Project = o.PaymentMethod.Payment.Booking.Unit.Project,
                                                                         Unit = o.PaymentMethod.Payment.Booking.Unit,
                                                                         Bank = o.Bank,
                                                                         BankBranch = o.BankBranch,
                                                                         PayToCompany = o.PayToCompany,
                                                                         ChequeDate = o.ChequeDate,
                                                                         ChequeNo = o.ChequeNo,
                                                                         ChequeType = PaymentPersonalCheque ?? new MasterCenter(),
                                                                         Fee = o.Fee,
                                                                         PaymentMethod = o.PaymentMethod,
                                                                         FeeConfirmByUser = o.FeeConfirmByUser,
                                                                         FeeConfirmDate = o.FeeConfirmDate,
                                                                         IsFeeConfirm = o.IsFeeConfirm,
                                                                         IsWrongCompany = o.IsWrongCompany,
                                                                         ReceiptHeader = ReceiptModel ?? new ReceiptTempHeader(),
                                                                         DepositStatus = DepositModel != null ? true : false,
                                                                         DepositNo = DepositModel != null ? DepositModel.DepositHeader.DepositNo : null,
                                                                         FeePercent = o.FeePercent,
                                                                         FeeIncludingVat = o.FeeIncludingVat == 0 ? (o.PaymentMethod.PayAmount - o.Fee) : o.FeeIncludingVat,
                                                                         ID = o.ID,
                                                                         Updated = o.Updated,
                                                                         UpdatedBy = o.UpdatedBy
                                                                     };

            var query = queryCashier.Union(queryPersonalCheques);
            #endregion
            #region filter
            if (filter.CompanyID != null)
            {
                query = query.Where(x => x.Company.ID == filter.CompanyID);
            }
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            if (filter.FeeConfirmStatus != null)
            {
                query = query.Where(x => x.IsFeeConfirm == filter.FeeConfirmStatus);
            }
            if (filter.ReceiveDateFrom != null)
            {
                query = query.Where(x => x.ReceiptHeader.ReceiveDate >= filter.ReceiveDateFrom);
            }
            if (filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.ReceiptHeader.ReceiveDate <= filter.ReceiveDateFrom);
            }
            if (filter.DepositNo != null)
            {
                query = query.Where(x => (x.DepositNo ?? "").Contains(filter.DepositNo));
            }
            if (filter.BankID != null)
            {
                query = query.Where(x => x.Bank.ID == filter.BankID);
            }
            if (filter.ChequeTypeMasterCenterID != null)
            {
                query = query.Where(x => x.ChequeType.ID == filter.ChequeTypeMasterCenterID);
            }
            if (filter.ChequeNo != null)
            {
                query = query.Where(x => (x.ChequeNo ?? "").Contains(filter.ChequeNo));
            }
            //if (filter.EDCID != null)
            //{
            //    query = query.Where(x => x.EDC.ID == filter.EDCID);
            //}
            if (filter.ReceiveNo != null)
            {
                query = query.Where(x => (x.ReceiptHeader.ReceiptTempNo ?? "").Contains(filter.ReceiveNo));
            }
            //if (filter.CreditCardTypeMasterCenterID != null)
            //{
            //    query = query.Where(x => x.CreditCardType.ID == filter.CreditCardTypeMasterCenterID);
            //}

            if (filter.UnitNo != null)
            {
                query = query.Where(x => (x.Unit.UnitNo ?? "").Contains(filter.UnitNo));
            }
            if (filter.FeePercentFrom != null)
            {
                query = query.Where(x => x.FeePercent >= filter.FeePercentFrom);
            }
            if (filter.FeePercentTo != null)
            {
                query = query.Where(x => x.FeePercent <= filter.FeePercentTo);
            }
            if (filter.FeeAmountFrom != null)
            {
                query = query.Where(x => x.Fee >= filter.FeeAmountFrom);
            }
            if (filter.FeeAmountTo != null)
            {
                query = query.Where(x => x.Fee <= filter.FeeAmountTo);
            }
            //if (filter.VatFrom != null)
            //{
            //    query = query.Where(x => x.Vat >= filter.VatFrom);
            //}
            //if (filter.VatTo != null)
            //{
            //    query = query.Where(x => x.Vat <= filter.VatTo);
            //}
            if (filter.PayAmountFrom != null)
            {
                query = query.Where(x => x.PaymentMethod.PayAmount >= filter.PayAmountFrom);
            }
            if (filter.PayAmountTo != null)
            {
                query = query.Where(x => x.PaymentMethod.PayAmount <= filter.PayAmountTo);
            }
            if (filter.FeeIncludingVatFrom != null)
            {
                query = query.Where(x => x.FeeIncludingVat >= filter.FeeIncludingVatFrom);
            }
            if (filter.FeeIncludingVatTo != null)
            {
                query = query.Where(x => x.FeeIncludingVat <= filter.FeeIncludingVatTo);
            }
            if (filter.UpdatedDateFrom != null)
            {
                query = query.Where(x => x.Updated >= filter.UpdatedDateFrom);
            }
            if (filter.UpdatedDateTo != null)
            {
                query = query.Where(x => x.Updated <= filter.UpdatedDateTo);
            }
            if (filter.UpdatedByUser != null)
            {
                query = query.Where(x => (x.UpdatedBy.DisplayName ?? "").Contains(filter.UpdatedByUser));
            }

            if (filter.DepositStatus != null)
            {
                query = query.Where(x => x.DepositStatus == filter.DepositStatus);
            }
            if (filter.PostPIStatus != null)
            {
                query = query.Where(x => x.PostPIStatus == filter.PostPIStatus);
            }
            #endregion

            FeeChequeDTO.SortBy(sortByParam, ref query);
            var pageOuput = PagingHelper.Paging(pageParam, ref query);
            var queryResults = query.ToList();

            var result = queryResults.Select(o => FeeChequeDTO.CreateFromModel(o)).ToList();

            return new FeeChequePaging()
            {
                FeeCheques = result,
                PageOutput = pageOuput
            };
        }

        public async Task CancelFeeConfirmAsync(List<FeeChequeDTO> input, Guid? userID)
        {
            if (input.Count == 0)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "ไม่พบข้อมูลที่ต้องการแก้ไข");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }

            var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentMethod").ToList();
            var PaymentCashierCheque = PaymentMethod.Where(x => x.Key == "2").FirstOrDefault();
            var PaymentPersonalCheque = PaymentMethod.Where(x => x.Key == "4").FirstOrDefault();
            //var PaymentPostDateCheque = PaymentMethod.Where(x => x.Key == "5").FirstOrDefault();

            var CashierCheque = input.Where(x => x.ReceiveType.Id == PaymentCashierCheque.ID).ToList();
            var PersonalCheque = input.Where(x => x.ReceiveType.Id == PaymentPersonalCheque.ID).ToList();

            var queryCashierCheque = DB.PaymentCashierCheques.Where(x => CashierCheque.Any(z => x.ID == z.Id)).ToList();
            var queryPersonalCheque = DB.PaymentPersonalCheques.Where(x => PersonalCheque.Any(z => x.ID == z.Id)).ToList();

            if (input.Count == (queryCashierCheque.Count + queryPersonalCheque.Count))
            {
                foreach (var item in queryCashierCheque)
                {
                    if (item.IsFeeConfirm)
                    {
                        var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                        item.IsFeeConfirm = false;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }
                foreach (var item in queryPersonalCheque)
                {
                    if (item.IsFeeConfirm)
                    {
                        var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                        item.IsFeeConfirm = false;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }
                await DB.SaveChangesAsync();
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "ข้อมูลไม่ถูกต้อง");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task UpdateFeeChequeAsync(List<FeeChequeDTO> input, Guid? userID)
        {
            if (input.Count == 0)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "wม่พบข้อมูลที่ต้องการแก้ไข");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            foreach (var item in input)
            {
                await item.ValidateAsync(DB);
            }

            var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentMethod").ToList();
            var PaymentCashierCheque = PaymentMethod.Where(x => x.Key == "2").FirstOrDefault();
            var PaymentPersonalCheque = PaymentMethod.Where(x => x.Key == "4").FirstOrDefault();
            //var PaymentPostDateCheque = PaymentMethod.Where(x => x.Key == "5").FirstOrDefault();

            var CashierCheque = input.Where(x => x.ReceiveType.Id == PaymentCashierCheque.ID).ToList();
            var PersonalCheque = input.Where(x => x.ReceiveType.Id == PaymentPersonalCheque.ID).ToList();

            var queryCashierCheque = DB.PaymentCashierCheques.Where(x => CashierCheque.Any(z => x.ID == z.Id)).ToList();
            var queryPersonalCheque = DB.PaymentPersonalCheques.Where(x => PersonalCheque.Any(z => x.ID == z.Id)).ToList();

            if (input.Count == (queryCashierCheque.Count + queryPersonalCheque.Count))
            {
                foreach (var item in queryCashierCheque)
                {
                    var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                    if (!item.IsFeeConfirm)
                    { 
                        item.ChequeNo = newData.ChequeNo;
                        item.BankID = newData.Bank.Id;
                        item.FeePercent = newData.FeePercent ?? 0;
                        item.Fee = newData.FeeAmount ?? 0;
                        item.FeeIncludingVat = (newData.PayAmount - newData.FeeAmount) ?? 0;
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
                        item.FeeConfirmDate = DateTime.Now;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }
                foreach (var item in queryPersonalCheque)
                {
                    var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                    if (!item.IsFeeConfirm)
                    {
                        item.ChequeNo = newData.ChequeNo;
                        item.BankID = newData.Bank.Id;
                        item.FeePercent = newData.FeePercent ?? 0;
                        item.Fee = newData.FeeAmount ?? 0;
                        item.FeeIncludingVat = (newData.PayAmount - newData.FeeAmount) ?? 0;
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
                        item.FeeConfirmDate = DateTime.Now;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }
                await DB.SaveChangesAsync();
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "ข้อมูลไม่ถูกต้อง");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }
    }
}
