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
using static Base.DTOs.FIN.FeeCreditDebitCardDTO;
using ErrorHandling;

namespace Finance.Services.Service
{
    public class FeeCreditDebitCardService : IFeeCreditDebitCardService
    {
        private readonly DatabaseContext DB;
        public FeeCreditDebitCardService(DatabaseContext db)
        {
            DB = db;
        }
        public async Task<FeeCreditDebitCardPaging> GetFeeCreditDebitCardListAsync(FeeCreditDebitCardFilter filter, PageParam pageParam, FeeCreditDebitCardSortByParam sortByParam)
        {
            #region GetData
            var PaymentCardType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentCardType").ToList();
            var PaymentCardTypeCredit = PaymentCardType.Where(x => x.Key == "1").FirstOrDefault();
            var PaymentCardTypeDebit = PaymentCardType.Where(x => x.Key == "2").FirstOrDefault();
            IQueryable<FeeCreditDebitQueryResult> queryCredit = from o in DB.PaymentCreditCards
                                                                           .Include(x => x.PaymentMethod)
                                                                                   .ThenInclude(x => x.Payment)
                                                                                       .ThenInclude(x => x.Booking)
                                                                                            .ThenInclude(x => x.Unit)
                                                                                                .ThenInclude(x => x.Project)
                                                                                                    .ThenInclude(x => x.Company)
                                                                           .Include(x => x.EDC)
                                                                               .ThenInclude(x => x.Bank)
                                                                           //.Include(x => x.CreditCardPaymentType)
                                                                           .Include(x => x.CreditCardType)
                                                                           .Include(x => x.UpdatedBy)

                                                                           .Include(x => x.Bank)


                                                                join Receipt in DB.ReceiptTempHeaders on o.PaymentMethod.PaymentID equals Receipt.PaymentID into ReceiptGroup
                                                                from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                                join Deposit in DB.DepositDetails.Include(x => x.DepositHeader) on o.PaymentMethodID equals Deposit.PaymentMethodID into DepositGroup
                                                                from DepositModel in DepositGroup.DefaultIfEmpty()

                                                                join PostGL in DB.PostGLHeaders.Where(x => x.ReferentType == "DepositHeader") on DepositModel.ID equals PostGL.ReferentID into PostGLGroup
                                                                from PostGLModel in PostGLGroup.DefaultIfEmpty()

                                                                select new FeeCreditDebitQueryResult
                                                                {
                                                                    Company = o.PaymentMethod.Payment.Booking.Unit.Project.Company,
                                                                    Project = o.PaymentMethod.Payment.Booking.Unit.Project,
                                                                    Unit = o.PaymentMethod.Payment.Booking.Unit,
                                                                    // PaymentCreditCard = o,
                                                                    CardType = PaymentCardTypeCredit ?? new MasterCenter(),
                                                                    ReceiptHeader = ReceiptModel ?? new ReceiptTempHeader(),
                                                                    FeeConfirmStatus = o.IsFeeConfirm,
                                                                    ID = o.ID,
                                                                    FeeIncludingVat = o.FeeIncludingVat == 0 ? (o.PaymentMethod.PayAmount - (o.Fee + Convert.ToDecimal(o.Vat))) : o.FeeIncludingVat,
                                                                    EDC = o.EDC,
                                                                    Bank = o.Bank,
                                                                    CreditCardType = o.CreditCardType,
                                                                    CardNo = o.CardNo,
                                                                    FeePercent = o.FeePercent,
                                                                    FeeAmount = o.Fee,
                                                                    Vat = o.Vat,
                                                                    PayAmount = o.PaymentMethod.PayAmount,
                                                                    Updated = o.Updated,
                                                                    UpdatedBy = o.UpdatedBy,
                                                                    //Deposit = DepositModel
                                                                    DepositStatus = DepositModel != null ? true : false,
                                                                    DepositNo = DepositModel != null ? DepositModel.DepositHeader.DepositNo : null,

                                                                    PostPI = PostGLModel != null ? PostGLModel.DocumentNo : null,
                                                                    PostPIStatus = PostGLModel != null ? true : false
                                                                };

            IQueryable<FeeCreditDebitQueryResult> queryDebit = from o in DB.PaymentDebitCards
                                                                           .Include(x => x.PaymentMethod)
                                                                                   .ThenInclude(x => x.Payment)
                                                                                       .ThenInclude(x => x.Booking)
                                                                                            .ThenInclude(x => x.Unit)
                                                                                                .ThenInclude(x => x.Project)
                                                                                                    .ThenInclude(x => x.Company)
                                                                           .Include(x => x.EDC)
                                                                                .ThenInclude(x => x.Bank)
                                                                           .Include(x => x.UpdatedBy)
                                                                           .Include(x => x.Bank)

                                                               join Receipt in DB.ReceiptTempHeaders on o.PaymentMethod.PaymentID equals Receipt.PaymentID into ReceiptGroup
                                                               from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                               join Deposit in DB.DepositDetails.Include(x => x.DepositHeader) on o.PaymentMethodID equals Deposit.PaymentMethodID into DepositGroup
                                                               from DepositModel in DepositGroup.DefaultIfEmpty()

                                                               join PostGL in DB.PostGLHeaders.Where(x => x.ReferentType == "DepositHeader") on DepositModel.ID equals PostGL.ReferentID into PostGLGroup
                                                               from PostGLModel in PostGLGroup.DefaultIfEmpty()

                                                               select new FeeCreditDebitQueryResult
                                                               {
                                                                   Company = o.PaymentMethod.Payment.Booking.Unit.Project.Company,
                                                                   Project = o.PaymentMethod.Payment.Booking.Unit.Project,
                                                                   Unit = o.PaymentMethod.Payment.Booking.Unit,
                                                                   //PaymentDebitCard = o,
                                                                   CardType = PaymentCardTypeDebit ?? new MasterCenter(),
                                                                   ReceiptHeader = ReceiptModel ?? new ReceiptTempHeader(),
                                                                   FeeConfirmStatus = o.IsFeeConfirm,
                                                                   FeeIncludingVat = o.FeeIncludingVat == 0 ? (o.PaymentMethod.PayAmount - (o.Fee + Convert.ToDecimal(o.Vat))) : o.FeeIncludingVat,
                                                                   EDC = o.EDC,
                                                                   Bank = o.Bank,
                                                                   CreditCardType = new MasterCenter(),
                                                                   CardNo = o.CardNo,
                                                                   FeePercent = o.FeePercent,
                                                                   FeeAmount = o.Fee,
                                                                   Vat = o.Vat,
                                                                   PayAmount = o.PaymentMethod.PayAmount,
                                                                   Updated = o.Updated,
                                                                   UpdatedBy = o.UpdatedBy,
                                                                   // Deposit = DepositModel
                                                                   DepositStatus = DepositModel != null ? true : false,
                                                                   DepositNo = DepositModel != null ? DepositModel.DepositHeader.DepositNo : null,
                                                                   PostPI = PostGLModel != null ? PostGLModel.DocumentNo : null,
                                                                   PostPIStatus = PostGLModel != null ? true : false
                                                               };


            var query = queryCredit.Union(queryDebit);
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
                query = query.Where(x => x.FeeConfirmStatus == filter.FeeConfirmStatus);
            }
            if (filter.ReceiveNo != null)
            {
                query = query.Where(x => (x.ReceiptHeader.ReceiptTempNo ?? "").Contains(filter.ReceiveNo));
            }
            if (filter.ReceiveDateFrom != null)
            {
                query = query.Where(x => x.ReceiptHeader.ReceiveDate >= filter.ReceiveDateFrom);
            }
            if (filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.ReceiptHeader.ReceiveDate <= filter.ReceiveDateFrom);
            }
            if (filter.EDCID != null)
            {
                query = query.Where(x => x.EDC.ID == filter.EDCID);
            }
            if (filter.BankID != null)
            {
                query = query.Where(x => x.Bank.ID == filter.BankID);
            }
            if (filter.CreditCardTypeMasterCenterID != null)
            {
                query = query.Where(x => x.CreditCardType.ID == filter.CreditCardTypeMasterCenterID);
            }
            if (filter.CardNo != null)
            {
                query = query.Where(x => (x.CardNo ?? "").Contains(filter.CardNo));
            }
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
                query = query.Where(x => x.FeeAmount >= filter.FeeAmountFrom);
            }
            if (filter.FeeAmountTo != null)
            {
                query = query.Where(x => x.FeeAmount <= filter.FeeAmountTo);
            }
            if (filter.VatFrom != null)
            {
                query = query.Where(x => x.Vat >= filter.VatFrom);
            }
            if (filter.VatTo != null)
            {
                query = query.Where(x => x.Vat <= filter.VatTo);
            }
            if (filter.PayAmountFrom != null)
            {
                query = query.Where(x => x.PayAmount >= filter.PayAmountFrom);
            }
            if (filter.PayAmountTo != null)
            {
                query = query.Where(x => x.PayAmount <= filter.PayAmountTo);
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
            if (filter.DepositNo != null)
            {
                query = query.Where(x => (x.DepositNo ?? "").Contains(filter.DepositNo));
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

            FeeCreditDebitCardDTO.SortBy(sortByParam, ref query);
            var pageOuput = PagingHelper.Paging(pageParam, ref query);

            var queryResults = query.ToList();
            var result = queryResults.Select(o => FeeCreditDebitCardDTO.CreateFromModel(o)).ToList();

            return new FeeCreditDebitCardPaging()
            {
                FeeCreditDebitCards = result,
                PageOutput = pageOuput
            };
        }

        public async Task UpdateFeeConfirmAsync(List<FeeCreditDebitCardDTO> input, Guid? userID)
        {
            if (input.Count == 0)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "ไม่พบข้อมูลที่ต้องการแก้ไข");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            foreach (var item in input)
            {
                await item.ValidateAsync(DB);
            }

            var PaymentCardType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentCardType").ToList();
            var PaymentCardTypeCredit = PaymentCardType.Where(x => x.Key == "1").FirstOrDefault();
            var PaymentCardTypeDebit = PaymentCardType.Where(x => x.Key == "2").FirstOrDefault();

            var Credit = input.Where(x => x.ReceiveType.Id == PaymentCardTypeCredit.ID).ToList();
            var Debit = input.Where(x => x.ReceiveType.Id == PaymentCardTypeDebit.ID).ToList();

            var queryCredit = DB.PaymentCreditCards.Where(x => Credit.Any(z => x.ID == z.Id)).ToList();
            var queryDebit = DB.PaymentDebitCards.Where(x => Debit.Any(z => x.ID == z.Id)).ToList();
            List<PaymentCreditCard> newListCreditCard = new List<PaymentCreditCard>();
            List<PaymentDebitCard> newListDebitCard = new List<PaymentDebitCard>();
            if (input.Count == (queryCredit.Count + queryDebit.Count))
            {
                foreach (var item in queryCredit)
                {
                    var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                    if (!item.IsFeeConfirm)
                    {
                        var chkAmount = (newData.PayAmount - (newData.FeeAmount ?? 0 + Convert.ToDecimal(newData.Vat ?? 0))) ?? 0;
                        if (chkAmount < 0)
                        {
                            ValidateException ex = new ValidateException();
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                            var msg = errMsg.Message.Replace("[message]", "เนื่องจากใส่ค่า Fee หรือ Vat เกินจำนวนเงิน เลขที่บัตร " + newData.CardNo);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            throw ex;
                        }
                        item.EDCID = newData.EDC.Id;
                        item.BankID = newData.Bank.Id;
                        item.CreditCardTypeMasterCenterID = newData.CreditCardType.Id;
                        item.FeePercent = newData.FeePercent ?? 0;
                        item.Fee = newData.FeeAmount ?? 0;
                        item.Vat = newData.Vat ?? 0;
                        item.FeeIncludingVat = chkAmount;
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
                        item.FeeConfirmDate = DateTime.Now;
                        item.FeeConfirmByUserID = userID;
                        
                        DB.Entry(item).State = EntityState.Modified;
                        await DB.SaveChangesAsync();
                    }
                }

                foreach (var item in queryDebit)
                {
                    if (!item.IsFeeConfirm)
                    {
                        var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                        var chkAmount = (newData.PayAmount - (newData.FeeAmount ?? 0 + Convert.ToDecimal(newData.Vat ?? 0))) ?? 0;
                        if (chkAmount < 0)
                        {
                            ValidateException ex = new ValidateException();
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                            var msg = errMsg.Message.Replace("[message]", "เนื่องจากใส่ค่า Fee หรือ Vat เกินจำนวนเงิน เลขที่บัตร " + newData.CardNo);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            throw ex;
                        }
                        item.EDCID = newData.EDC.Id;
                        item.BankID = newData.Bank.Id;
                        //item.CreditCardTypeMasterCenterID = newData.CreditCardType.Id;
                        item.FeePercent = newData.FeePercent ?? 0;
                        item.Fee = newData.FeeAmount ?? 0;
                        item.Vat = newData.Vat ?? 0;
                        item.FeeIncludingVat = chkAmount;
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
                        item.FeeConfirmDate = DateTime.Now;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                        await DB.SaveChangesAsync();
                    }
                }
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

        public async Task CancelFeeConfirmAsync(List<FeeCreditDebitCardDTO> input, Guid? userID)
        {
            if (input.Count == 0)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "wม่พบข้อมูลที่ต้องการแก้ไข");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            var PaymentCardType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentCardType").ToList();
            var PaymentCardTypeCredit = PaymentCardType.Where(x => x.Key == "1").FirstOrDefault();
            var PaymentCardTypeDebit = PaymentCardType.Where(x => x.Key == "2").FirstOrDefault();
            var Credit = input.Where(x => x.ReceiveType.Id == PaymentCardTypeCredit.ID).ToList();
            var Debit = input.Where(x => x.ReceiveType.Id == PaymentCardTypeDebit.ID).ToList();

            var queryCredit = DB.PaymentCreditCards.Where(x => Credit.Any(z => x.ID == z.Id)).ToList();
            var queryDebit = DB.PaymentDebitCards.Where(x => Debit.Any(z => x.ID == z.Id)).ToList();

            if (input.Count == (queryCredit.Count + queryDebit.Count))
            {
                foreach (var item in queryCredit)
                {
                    if (item.IsFeeConfirm)
                    {
                        var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
                        item.FeeConfirmByUserID = userID;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }
                foreach (var item in queryDebit)
                {
                    if (item.IsFeeConfirm)
                    {
                        var newData = input.Where(x => x.Id == item.ID).FirstOrDefault();
                        item.IsFeeConfirm = newData.FeeConfirmStatus;
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
