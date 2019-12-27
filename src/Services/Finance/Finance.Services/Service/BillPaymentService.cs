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
using static Base.DTOs.FIN.BillPaymentHeaderDTO;
using System.IO;
using System.Net;
using System.Globalization;
using System.Threading;
using ErrorHandling;
using Database.Models.MasterKeys;
using Microsoft.Extensions.Configuration;
using Base.DTOs.MST;

namespace Finance.Services.Service
{
    public class BillPaymentService : IBillPaymentService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private readonly IPaymentService PaymentService;
        public BillPaymentService(DatabaseContext db, IConfiguration configuration, IPaymentService paymentService)
        {
            DB = db;
            this.PaymentService = paymentService;
            this.Configuration = configuration;
        }

        public async Task<BillPaymentHeaderPaging> GetBillPaymentDetailAsync(Guid id, BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {

            var StatusWait = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
            IQueryable<BillPaymentQueryResult> query = from o in DB.BillPaymentDetails.Where(x => x.BillPaymentHeaderID == id)
                                                       .Include(x => x.BillPayment)
                                                               .ThenInclude(x => x.BankAccount)
                                                                   .ThenInclude(x => x.Bank)
                                                        .Include(x => x.BillPayment)
                                                                .ThenInclude(x => x.BankAccount)
                                                                    .ThenInclude(x => x.Company)
                                                        .Include(x => x.BillPaymentStatus)
                                                        .Include(x => x.BillPaymentDeleteReason)
                                                        .Include(x => x.CreatedBy)
                                                        .Include(x => x.UpdatedBy)

                                                       let countWiat = (o.BillPayment.TotalRecord - (from countWiat in DB.BillPaymentDetails
                                                                                         .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID == StatusWait.ID)
                                                                                                     select countWiat.ID).Count())

                                                       let countDone = (o.BillPayment.TotalRecord - (from countWiat in DB.BillPaymentDetails
                                                                       .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID != StatusWait.ID)
                                                                                                     select countWiat.ID).Count())
                                                       select new BillPaymentQueryResult
                                                       {

                                                           BillPaymentDetail = o,
                                                           BillPaymentHeader = o.BillPayment,
                                                           BankAccount = o.BillPayment.BankAccount,
                                                           Bank = o.BillPayment.BankAccount.Bank,
                                                           countDone = countDone,
                                                           countWiat = countWiat
                                                       };

            var queryResults = query.ToList();
            var results = queryResults.Select(o => BillPaymentHeaderDTO.CreateFromModel(o, DB)).ToList();

            #region filter
            if (filter.CompanyID != null)
            {
                results = results.Where(x => x.Company.Id == filter.CompanyID).ToList();
            }
            if (filter.ReceiveDateFrom != null)
            {
                results = results.Where(x => x.ReceiveDate >= filter.ReceiveDateFrom).ToList();
            }
            if (filter.ReceiveDateTo != null)
            {
                results = results.Where(x => x.ReceiveDate <= filter.ReceiveDateTo).ToList();
            }
            if (filter.CustomerName != null)
            {
                results = results.Where(x => x.BillPaymentDetails.CustomerName.Contains(filter.CustomerName)).ToList();
            }
            if (filter.BankRef1 != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BankRef1.Contains(filter.BankRef1)).ToList();
            }
            if (filter.BankRef2 != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BankRef2.Contains(filter.BankRef2)).ToList();
            }

            //if (filter.BankRef3 != null)
            //{
            //    results = results.Where(x => x.BillPaymentDetails.BankRef3.Contains(filter.BankRef3));
            //}

            if (filter.PayType != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayType.Contains(filter.PayType)).ToList();
            }
            if (filter.PayAmountFrom != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayAmount >= filter.PayAmountFrom).ToList();
            }
            if (filter.PayAmountTo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayAmount <= filter.PayAmountTo).ToList();
            }
            if (filter.BillPaymentStatusMasterCenterID != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BillPaymentStatus.Id == filter.BillPaymentStatusMasterCenterID).ToList();
            }

            if (filter.AgreementNo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strAgreementNo.Contains(filter.AgreementNo)).ToList();
            }
            if (filter.ProjectID != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strProjectID.Contains(filter.ProjectID.ToString())).ToList();
            }
            if (filter.UnitNo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strUnit.Contains(filter.UnitNo)).ToList();
            }
            #endregion

            PageOutput pageOuput = null;
            pageOuput = PagingHelper.PagingList<BillPaymentHeaderDTO>(pageParam, ref results);

            return new BillPaymentHeaderPaging()
            {
                BillPayments = results,
                PageOutput = pageOuput
            };
        }

        public async Task<BillPaymentHeaderPaging> GetBillPaymentListAsync(BillPaymentHeaderFilter filter, PageParam pageParam, BillPaymentHeaderSortByParam sortByParam)
        {
            var StatusWait = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
            IQueryable<BillPaymentQueryResult> query = from o in DB.BillPayments
                .Include(x => x.BankAccount)
                    .ThenInclude(x => x.Bank)
                .Include(x => x.BankAccount)
                    .ThenInclude(x => x.Company)
            .Include(x => x.CreatedBy)
                                                       let countWiat = (o.TotalRecord - (from countWiat in DB.BillPaymentDetails
                                                                                         .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID == StatusWait.ID)
                                                                                         select countWiat.ID).Count())

                                                       let countDone = (o.TotalRecord - (from countWiat in DB.BillPaymentDetails
                                                                       .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID != StatusWait.ID)
                                                                                         select countWiat.ID).Count())

                                                       select new BillPaymentQueryResult
                                                       {
                                                           BillPaymentHeader = o,
                                                           BankAccount = o.BankAccount,
                                                           Bank = o.BankAccount.Bank,
                                                           countDone = countDone,
                                                           countWiat = countWiat
                                                       };



            var queryResults = query.ToList();
            var results = queryResults.Select(o => BillPaymentHeaderDTO.CreateFromModel(o, DB)).ToList();

            #region filter
            if (filter.BatchID != null)
            {
                results = results.Where(x => x.BatchID.Contains(filter.BatchID)).ToList();
            }
            if (filter.CreateDateFrom != null)
            {
                results = results.Where(x => x.CreateDate >= filter.CreateDateFrom).ToList();
            }
            if (filter.CreateDateTo != null)
            {
                results = results.Where(x => x.CreateDate <= filter.CreateDateTo).ToList();
            }
            if (filter.ReceiveDateFrom != null)
            {
                results = results.Where(x => x.ReceiveDate >= filter.ReceiveDateFrom).ToList();
            }
            if (filter.ReceiveDateTo != null)
            {
                results = results.Where(x => x.ReceiveDate <= filter.ReceiveDateTo).ToList();
            }
            if (filter.BankID != null)
            {
                results = results.Where(x => x.Bank.Id == filter.BankID).ToList();
            }
            if (filter.TotalRecordFrom != null)
            {
                results = results.Where(x => x.TotalRecord >= filter.TotalRecordFrom).ToList();
            }
            if (filter.TotalRecordTo != null)
            {
                results = results.Where(x => x.TotalRecord <= filter.TotalRecordTo).ToList();
            }
            if (filter.TotalSucessRecordFrom != null)
            {
                results = results.Where(x => x.TotalRecordDone >= filter.TotalSucessRecordFrom).ToList();
            }
            if (filter.TotalSucessRecordTo != null)
            {
                results = results.Where(x => x.TotalRecordDone <= filter.TotalSucessRecordTo).ToList();
            }
            if (filter.TotalWaitingRecordFrom != null)
            {
                results = results.Where(x => x.TotalRecordWiat >= filter.TotalWaitingRecordFrom).ToList();
            }
            if (filter.TotalWaitingRecordTo != null)
            {
                results = results.Where(x => x.TotalRecordWiat <= filter.TotalWaitingRecordTo).ToList();
            }
            if (filter.AmountFrom != null)
            {
                results = results.Where(x => x.TotalAmount >= filter.AmountFrom).ToList();
            }
            if (filter.AmountTo != null)
            {
                results = results.Where(x => x.TotalAmount <= filter.AmountTo).ToList();
            }
            if (filter.CreatedUserText != null)
            {
                results = results.Where(x => x.CreatedBy.Contains(filter.CreatedUserText)).ToList();
            }
            #endregion

            PageOutput pageOuput = null;
            pageOuput = PagingHelper.PagingList<BillPaymentHeaderDTO>(pageParam, ref results);

            return new BillPaymentHeaderPaging()
            {
                BillPayments = results,
                PageOutput = pageOuput
            };
        }


        public async Task<BillPaymentHeaderPaging> GetWaitingBillPaymentListAsync(BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {

            var StatusWait = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
            IQueryable<BillPaymentQueryResult> query = from o in DB.BillPaymentDetails.Where(x => x.BillPaymentStatusMasterCenterID == StatusWait.ID)
                                                       .Include(x => x.BillPayment)
                                                               .ThenInclude(x => x.BankAccount)
                                                                   .ThenInclude(x => x.Bank)
                                                        //.Include(x => x.Booking)
                                                        //        .ThenInclude(x => x.Unit)
                                                        //            .ThenInclude(x => x.Project)
                                                        //.ThenInclude(x => x.Company)
                                                        .Include(x => x.BillPayment)
                                                                .ThenInclude(x => x.BankAccount)
                                                                    .ThenInclude(x => x.Company)
                                                        //.ThenInclude(x => x.Company)
                                                        .Include(x => x.BillPaymentStatus)
                                                        .Include(x => x.BillPaymentDeleteReason)
                                                        .Include(x => x.CreatedBy)
                                                        .Include(x => x.UpdatedBy)


                                                       select new BillPaymentQueryResult
                                                       {
                                                           BillPaymentDetail = o,
                                                           BillPaymentHeader = o.BillPayment,
                                                           BankAccount = o.BillPayment.BankAccount,
                                                           Bank = o.BillPayment.BankAccount.Bank
                                                       };


            var queryResults = query.ToList();
            var results = queryResults.Select(o => BillPaymentHeaderDTO.CreateFromModel(o, DB)).ToList();


            #region filter
            if (filter.CompanyID != null)
            {
                results = results.Where(x => x.Company.Id == filter.CompanyID).ToList();
            }
            if (filter.ReceiveDateFrom != null)
            {
                results = results.Where(x => x.ReceiveDate >= filter.ReceiveDateFrom).ToList();
            }
            if (filter.ReceiveDateTo != null)
            {
                results = results.Where(x => x.ReceiveDate <= filter.ReceiveDateTo).ToList();
            }
            if (filter.CustomerName != null)
            {
                results = results.Where(x => x.BillPaymentDetails.CustomerName.Contains(filter.CustomerName)).ToList();
            }
            if (filter.BankRef1 != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BankRef1.Contains(filter.BankRef1)).ToList();
            }
            if (filter.BankRef2 != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BankRef2.Contains(filter.BankRef2)).ToList();
            }

            //if (filter.BankRef3 != null)
            //{
            //    results = results.Where(x => x.BillPaymentDetails.BankRef3.Contains(filter.BankRef3));
            //}

            if (filter.PayType != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayType.Contains(filter.PayType)).ToList();
            }
            if (filter.PayAmountFrom != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayAmount >= filter.PayAmountFrom).ToList();
            }
            if (filter.PayAmountTo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.PayAmount <= filter.PayAmountTo).ToList();
            }
            if (filter.BillPaymentStatusMasterCenterID != null)
            {
                results = results.Where(x => x.BillPaymentDetails.BillPaymentStatus.Id == filter.BillPaymentStatusMasterCenterID).ToList();
            }

            if (filter.AgreementNo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strAgreementNo.Contains(filter.AgreementNo)).ToList();
            }
            if (filter.ProjectID != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strProjectID.Contains(filter.ProjectID.ToString())).ToList();
            }
            if (filter.UnitNo != null)
            {
                results = results.Where(x => x.BillPaymentDetails.strUnit.Contains(filter.UnitNo)).ToList();
            }
            #endregion

            PageOutput pageOuput = null;
            pageOuput = PagingHelper.PagingList<BillPaymentHeaderDTO>(pageParam, ref results);

            return new BillPaymentHeaderPaging()
            {
                BillPayments = results,
                PageOutput = pageOuput
            };
        }

        private List<listRef> getTextFlie(string bankCode, StreamReader reader)
        {
            string line;
            List<listRef> listRef = new List<listRef>();

            switch (bankCode)
            {
                case "004": // KBank
                    while ((line = reader.ReadLine()) != null)
                    {
                        listRef newList = new listRef();
                        if (line.Substring(0, 1) != "T")
                        {
                            DateTime dDueDate = DateTime.ParseExact(line.Substring(20, 8) + " " + line.Substring(28, 6), "ddMMyyyy HHmmss", CultureInfo.GetCultureInfo("en-US"));
                            newList.Date = dDueDate;
                            //newList.Time = line.Substring(28, 6);
                            newList.Ref1 = line.Substring(84, 20).Replace(" ", "");
                            newList.Ref2 = line.Substring(104, 20).Replace(" ", "");

                            string ChkName = line.Substring(34, 50);
                            if (line.Substring(34, 50).Replace(" ", "").Length > 0)
                            {
                                for (int i = ChkName.Length; i >= 0; i--)
                                {
                                    string chkNull = ChkName.Substring(ChkName.Length - 1, 1);
                                    if (chkNull == null || chkNull == " ")
                                    {
                                        ChkName = ChkName.Remove(i - 1);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            newList.CustomerName = ChkName;
                            string tDecimal = line.Substring(163, 13).LeadingDigit(20, "0");
                            tDecimal = tDecimal.Substring(0, 18) + "." + tDecimal.Substring(18, 2);
                            newList.Amount = Convert.ToDecimal(tDecimal);
                            listRef.Add(newList);
                        }
                    }
                    break;
                case "002’ ": // BBL
                    while ((line = reader.ReadLine()) != null)
                    {
                        listRef newList = new listRef();
                        if (line.Substring(0, 1) != "T")
                        {
                            DateTime dDueDate = DateTime.ParseExact(line.Substring(20, 8) + " " + line.Substring(28, 6), "ddMMyyyy HHmmss", CultureInfo.GetCultureInfo("en-US"));
                            newList.Date = dDueDate;
                            //newList.Time = line.Substring(28, 6);
                            newList.Ref1 = line.Substring(84, 20).Replace(" ", "");
                            newList.Ref2 = line.Substring(104, 20).Replace(" ", "");

                            string ChkName = line.Substring(34, 50);
                            if (line.Substring(34, 50).Replace(" ", "").Length > 0)
                            {
                                for (int i = ChkName.Length; i >= 0; i--)
                                {
                                    string chkNull = ChkName.Substring(ChkName.Length - 1, 1);
                                    if (chkNull == null || chkNull == " ")
                                    {
                                        ChkName = ChkName.Remove(i - 1);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            newList.CustomerName = ChkName;
                            string tDecimal = line.Substring(163, 13).LeadingDigit(20, "0");
                            tDecimal = tDecimal.Substring(0, 18) + "." + tDecimal.Substring(18, 2);
                            newList.Amount = Convert.ToDecimal(tDecimal);
                            listRef.Add(newList);
                        }
                    }
                    break;
                case "014   ": // SCB
                    while ((line = reader.ReadLine()) != null)
                    {
                        listRef newList = new listRef();
                        if (line.Substring(0, 1) != "T")
                        {
                            DateTime dDueDate = DateTime.ParseExact(line.Substring(20, 8) + " " + line.Substring(28, 6), "ddMMyyyy HHmmss", CultureInfo.GetCultureInfo("en-US"));
                            newList.Date = dDueDate;
                            //newList.Time = line.Substring(28, 6);
                            newList.Ref1 = line.Substring(84, 20).Replace(" ", "");
                            newList.Ref2 = line.Substring(104, 20).Replace(" ", "");

                            string ChkName = line.Substring(34, 50);
                            if (line.Substring(34, 50).Replace(" ", "").Length > 0)
                            {
                                for (int i = ChkName.Length; i >= 0; i--)
                                {
                                    string chkNull = ChkName.Substring(ChkName.Length - 1, 1);
                                    if (chkNull == null || chkNull == " ")
                                    {
                                        ChkName = ChkName.Remove(i - 1);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            newList.CustomerName = ChkName;
                            string tDecimal = line.Substring(163, 13).LeadingDigit(20, "0");
                            tDecimal = tDecimal.Substring(0, 18) + "." + tDecimal.Substring(18, 2);
                            newList.Amount = Convert.ToDecimal(tDecimal);
                            listRef.Add(newList);
                        }
                    }
                    break;
            }

            return listRef;
        }

        public async Task<Guid> ImportBillPaymentAsync(FileWithBoolDTO fileDTO)
        {
            Guid NewIDHeader = Guid.NewGuid();

            List<listRef> listRef = new List<listRef>();
            List<BillPaymentDetailTemp> BillPaymentDetailModel = new List<BillPaymentDetailTemp>();
            string chkAccountNo = null;
            if (fileDTO.FileDTO.Url != null)
            {
                StreamReader reader = null;
                try
                {
                    StreamReader GetTextFile = new StreamReader(WebRequest.Create(fileDTO.FileDTO.Url).GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding(874));
                    reader = GetTextFile;
                }
                catch
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                    var msg = errMsg.Message.Replace("[message]", "ไม่สามารถอ่าน URL ได้");
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }

                chkAccountNo = reader.ReadLine().ToString().Substring(10, 10);
                var chkAccount = DB.BankAccounts.Where(x => x.BankAccountNo == chkAccountNo).Include(x => x.Bank).FirstOrDefault();
                if (chkAccount == null)
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0099").FirstAsync();
                    var msg = errMsg.Message.Replace("[message]", chkAccountNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }
                var GetStatus = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus).ToList();
                var StatusWait = GetStatus.Where(x => x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
                var StatusNotFound = GetStatus.Where(x => x.Key == BillPaymentStatusKey.NotFound).FirstOrDefault();
                var StatusUnitTransfer = GetStatus.Where(x => x.Key == BillPaymentStatusKey.UnitTransfered).FirstOrDefault();
                var StatusUnitCancel = GetStatus.Where(x => x.Key == BillPaymentStatusKey.Cancel).FirstOrDefault();
                var StatusDuplicate = GetStatus.Where(x => x.Key == BillPaymentStatusKey.Duplicate).FirstOrDefault();
       
                listRef = getTextFlie(chkAccount.Bank.BankNo ?? null, reader);
                if (listRef.Count > 0)
                {
                    // Check Duplicate 

                    IQueryable<importBillPaymentQueryResult> query = from o in DB.Bookings.IgnoreQueryFilters()
                                         .Include(x => x.Unit)
                                            .ThenInclude(x => x.Project)
                                                   .ThenInclude(x => x.Company)

                                                                     join Agreement in DB.Agreements.IgnoreQueryFilters() on o.ID equals Agreement.BookingID into AgreementGroup
                                                                     from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                                         //join Agreement in DB.Agreements on o.ID equals Agreement.BookingID into AgreementGroup
                                                                         //from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                                     join AgreementOwner in DB.AgreementOwners.Where(x => x.IsMainOwner == true) on AgreementModel.ID equals AgreementOwner.AgreementID into AgreementOwnerGroup
                                                                     from AgreementOwnerModel in AgreementOwnerGroup.DefaultIfEmpty()

                                                                     join Transfer in DB.Transfers.Where(x => x.IsDeleted == false) on AgreementModel.ID equals Transfer.AgreementID into trng
                                                                     from TransferModel in trng.DefaultIfEmpty()
                                                                     select new importBillPaymentQueryResult
                                                                     {
                                                                         Booking = o,
                                                                         Agreement = AgreementModel ?? new Agreement(),
                                                                         AgreementOwner = AgreementOwnerModel ?? new AgreementOwner(),
                                                                         Transfer = TransferModel ?? new Transfer()
                                                                     };
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    DateTime ReceiveDate = listRef.Select(z => z.Date).FirstOrDefault();

                    var oBatchDateilID = DB.BillPayments.Where(x => x.ReceiveDate == ReceiveDate).OrderByDescending(x => x.CreatedBy).Select(x => x.BatchID).ToList();
                    int numBatchDateil = 1;
                    if (oBatchDateilID.Count > 0)
                    {
                        var tBatchID = oBatchDateilID.FirstOrDefault();
                        numBatchDateil = Convert.ToInt32(tBatchID.Substring(8));
                        numBatchDateil = numBatchDateil + 1;
                    }

                    foreach (var item in listRef)
                    {
                        BillPaymentDetailTemp newTamp = new BillPaymentDetailTemp();
                        newTamp.ID = Guid.NewGuid();
                        newTamp.BillPaymentHeaderID = NewIDHeader;
                        newTamp.ReceiveDate = item.Date;
                        newTamp.BankRef1 = item.Ref1;
                        newTamp.BankRef2 = item.Ref2;
                        newTamp.PayAmount = item.Amount;
                        newTamp.IsDeleted = false;
                        newTamp.CustomerName = item.CustomerName;
                        string runDateDateil = ReceiveDate.Year.ToString("0000") + ReceiveDate.Month.ToString("00") + ReceiveDate.Day.ToString("00") + numBatchDateil.ToString("000000");
                        newTamp.DetailBatchID = runDateDateil;
                        importBillPaymentQueryResult chkAgreement = null;

                        if (item.Ref2.Length == 12 || item.Ref2.Length == 14)
                        {
                            chkAgreement = query.Where(x => (x.Agreement.AgreementNo ?? "").Replace("AA", "00").Replace("RN", "01") == item.Ref2 && x.AgreementOwner.ContactNo == item.Ref1).FirstOrDefault() ?? null;
                        }
                        else if (item.Ref2.Length == 17)
                        {
                            chkAgreement = query.Where(x => (x.Agreement.AgreementNo ?? "").Replace("AG", "00") == item.Ref2 && x.AgreementOwner.ContactNo == item.Ref1).FirstOrDefault() ?? null;
                        }
                        if (chkAgreement != null)
                        {

                            if (chkAgreement.Booking.IsDeleted)
                            {
                                newTamp.BookingID = chkAgreement.Booking.ID;
                                newTamp.BillPaymentStatusMasterCenterID = StatusUnitCancel.ID;
                            }
                            else if (!chkAgreement.Booking.IsPaid ?? false)
                            {
                                newTamp.BookingID = chkAgreement.Booking.ID;
                                newTamp.BillPaymentStatusMasterCenterID = StatusUnitCancel.ID;
                            }
                            else if (chkAgreement.Agreement.IsDeleted)
                            {
                                newTamp.BookingID = chkAgreement.Booking.ID;
                                newTamp.BillPaymentStatusMasterCenterID = StatusUnitCancel.ID;
                            }
                            else if (chkAgreement.Transfer.AgreementID != null)
                            {
                                newTamp.BookingID = chkAgreement.Booking.ID;
                                newTamp.BillPaymentStatusMasterCenterID = StatusUnitTransfer.ID;
                            }
                            else
                            {
                                newTamp.BookingID = chkAgreement.Booking.ID;
                                newTamp.BillPaymentStatusMasterCenterID = StatusWait.ID;
                            }

                        }
                        else
                        {
                            newTamp.BookingID = null;
                            newTamp.BillPaymentStatusMasterCenterID = StatusNotFound.ID;
                        }
                        // Check Duplicate 
                        var CheckDuplicate = DB.BillPaymentDetails.Where(x => x.ReceiveDate == item.Date && x.PayAmount == item.Amount).FirstOrDefault() ?? null;
                        if (CheckDuplicate != null)
                        {
                            newTamp.BillPaymentStatusMasterCenterID = StatusDuplicate.ID;
                        }
                        numBatchDateil++;
                        BillPaymentDetailModel.Add(newTamp);
                    }


                    BillPaymentHeaderTemp BillPaymentHeaderModel = new BillPaymentHeaderTemp();


                    int numBatch = 1;

                    var oBatchID = DB.BillPayments.Where(x => x.ReceiveDate == ReceiveDate).OrderByDescending(x => x.CreatedBy).Select(x => x.BatchID).ToList();
                    if (oBatchID.Count > 0)
                    {
                        var tBatchID = oBatchID.FirstOrDefault();
                        numBatch = Convert.ToInt32(tBatchID.Substring(8));
                        numBatch = numBatch + 1;
                    }

                    string runDate = ReceiveDate.Year.ToString("0000") + ReceiveDate.Month.ToString("00") + ReceiveDate.Day.ToString("00");
                    string BatchID = runDate + numBatch.ToString("000000");

                    BillPaymentHeaderModel.ID = NewIDHeader;
                    BillPaymentHeaderModel.BatchID = BatchID;
                    BillPaymentHeaderModel.BankAccountID = chkAccount.ID; // รอแก้ไข
                    BillPaymentHeaderModel.TotalAmount = listRef.Sum(x => x.Amount);
                    BillPaymentHeaderModel.IsDeleted = false;
                    BillPaymentHeaderModel.ReceiveDate = ReceiveDate;
                    BillPaymentHeaderModel.ImportFileName = fileDTO.FileDTO.Name;

                    if (fileDTO.Host2host)
                    {
                        var TypeKBankHostToHost = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentImportType && x.Key == BillPaymentImportTypeKey.KBankHostToHost && x.IsDeleted == false).Select(x => x.ID).FirstOrDefault();
                        BillPaymentHeaderModel.BillPaymentImportTypeMasterCenterID = TypeKBankHostToHost;
                    }
                    else
                    {
                        var TypeManualUpload = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentImportType && x.Key == BillPaymentImportTypeKey.ManualUpload && x.IsDeleted == false).Select(x => x.ID).FirstOrDefault();
                        BillPaymentHeaderModel.BillPaymentImportTypeMasterCenterID = TypeManualUpload;

                    }

                    BillPaymentHeaderModel.TotalRecord = BillPaymentDetailModel.Count();
                    await DB.BillPaymentTemps.AddAsync(BillPaymentHeaderModel);
                    await DB.BillPaymentDetailTemps.AddRangeAsync(BillPaymentDetailModel);
                    await DB.SaveChangesAsync();

                }
                return NewIDHeader;
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "ไม่สามารถอ่าน URL ได้");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task<BillPaymentHeaderPaging> GetBillPaymentDetailTempAsync(Guid id, BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam)
        {
            var StatusWait = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
            IQueryable<BillPaymentTampQueryResult> query = from o in DB.BillPaymentDetailTemps.Where(x => x.BillPaymentHeaderID == id)
                                                       .Include(x => x.BillPayment)
                                                            .ThenInclude(x => x.BankAccount)
                                                              .ThenInclude(x => x.Bank)
                                                        .Include(x => x.Booking)
                                                            .ThenInclude(x => x.Unit)
                                                               .ThenInclude(x => x.Project)
                                                         .Include(x => x.BillPayment)
                                                            .ThenInclude(x => x.BankAccount)
                                                               .ThenInclude(x => x.Company)
                                                        .Include(x => x.BillPaymentStatus)
                                                        .Include(x => x.BillPaymentDeleteReason)
                                                        .Include(x => x.CreatedBy)
                                                        .Include(x => x.UpdatedBy)

                                                           let countWiat = (o.BillPayment.TotalRecord - (from countWiat in DB.BillPaymentDetailTemps
                                                                                             .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID == StatusWait.ID)
                                                                                                         select countWiat.ID).Count())

                                                           let countDone = (o.BillPayment.TotalRecord - (from countWiat in DB.BillPaymentDetailTemps
                                                                           .Where(x => x.BillPaymentHeaderID == o.ID && x.BillPaymentStatusMasterCenterID != StatusWait.ID)
                                                                                                         select countWiat.ID).Count())

                                                           join Agreement in DB.Agreements on o.BookingID equals Agreement.BookingID into AgreementGroup
                                                           from AgreementModel in AgreementGroup.DefaultIfEmpty()

                                                               //let FullName = (from lname in DB.AgreementOwners
                                                               //             .Where(x => x.AgreementID == AgreementModel.ID && x.IsMainOwner == true)
                                                               //                select lname.FirstNameTH + " " + lname.LastNameTH).FirstOrDefault()

                                                           select new BillPaymentTampQueryResult
                                                           {
                                                               BillPaymentDetail = o,
                                                               BillPaymentHeader = o.BillPayment,
                                                               BankAccount = o.BillPayment.BankAccount,
                                                               Bank = o.BillPayment.BankAccount.Bank,
                                                               countDone = countDone,
                                                               countWiat = countWiat,
                                                               Agreement = AgreementModel ?? new Agreement(),
                                                               //AgreementOwner = AgreementOwnerModel ?? new AgreementOwner(),
                                                               //CustomerName = FullName //AgreementOwnerNameModel + " " + AgreementLastNameModel
                                                           };
            #region filter
            //if (filter.CompanyID != null)
            //{
            //    query = query.Where(x => x.BillPaymentHeader.BankAccount.Company.ID == filter.CompanyID);
            //}
            if (filter.CustomerName != null)
            {
                query = query.Where(x => x.BillPaymentDetail.CustomerName.Contains(filter.CustomerName));
            }
            if (filter.BankRef1 != null)
            {
                query = query.Where(x => x.BillPaymentDetail.BankRef1.Contains(filter.BankRef1));
            }
            if (filter.BankRef2 != null)
            {
                query = query.Where(x => x.BillPaymentDetail.BankRef2.Contains(filter.BankRef2));
            }
            if (filter.BankRef3 != null)
            {
                query = query.Where(x => x.BillPaymentDetail.BankRef3.Contains(filter.BankRef3));
            }
            if (filter.AgreementNo != null)
            {
                query = query.Where(x => x.Agreement.AgreementNo.Contains(filter.AgreementNo));
            }
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.BillPaymentDetail.Booking.Unit.Project.ID == filter.ProjectID);
            }
            if (filter.UnitNo != null)
            {
                query = query.Where(x => x.BillPaymentDetail.Booking.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (filter.PayType != null)
            {
                query = query.Where(x => x.BillPaymentDetail.PayType.Contains(filter.PayType));
            }
            if (filter.PayAmountFrom != null)
            {
                query = query.Where(x => x.BillPaymentDetail.PayAmount >= filter.PayAmountFrom);
            }
            if (filter.PayAmountTo != null)
            {
                query = query.Where(x => x.BillPaymentDetail.PayAmount <= filter.PayAmountTo);
            }
            if (filter.BillPaymentStatusMasterCenterID != null)
            {
                query = query.Where(x => x.BillPaymentDetail.BillPaymentStatusMasterCenterID == filter.BillPaymentStatusMasterCenterID);
            }
            #endregion

            PageOutput pageOuput = null;
            pageOuput = PagingHelper.Paging<BillPaymentTampQueryResult>(pageParam, ref query);

            var queryResults = query.ToList();
            var results = queryResults.Select(o => BillPaymentHeaderDTO.CreateFromModelTamp(o, DB)).ToList();
            return new BillPaymentHeaderPaging()
            {
                BillPayments = results,
                PageOutput = pageOuput
            };
        }

        public async Task UpdateBillPaymentSplitAsync(BookingForBillPaymentDTO input)
        {
            //await input.ValidateAsync(DB);
            var getBillPaymentDetail = DB.BillPaymentDetails.Where(x => x.ID == input.Id).FirstOrDefault() ?? null;
            if (getBillPaymentDetail != null)
            {
                var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentMethod" && x.Key == "7").FirstOrDefault();
                var PaymentMethodDTO = MasterCenterDropdownDTO.CreateFromModel(PaymentMethod);
                foreach (var item in input.SplitForBillPayment)
                {
                    if (item.Id == null && item.BookingID != null)
                    {
                        Guid tGuidBooking = item.BookingID;
                        var paymentNewBooking = await PaymentService.GetPaymentFormAsync(tGuidBooking, PaymentFormType.BillPayment, getBillPaymentDetail.ID, item.Amount);

                        paymentNewBooking.ReceiveDate = getBillPaymentDetail.ReceiveDate;
                        paymentNewBooking.PaymentFormType = PaymentFormType.BillPayment;
                        paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                                                {
                                                    new PaymentMethodDTO
                                                            {
                                                                PayAmount = item.Amount,
                                                                PaymentMethodType =  PaymentMethodDTO
                                                            },
                                                };
                        var xxx = await PaymentService.SubmitPaymentFormAsync(tGuidBooking, paymentNewBooking);
                    }
                    else if (item.IsDelete)
                    {
                        //getBillPaymentDetail.BillPaymentDeleteReasonMasterCenterID = item.DeleteReason.Id;
                        //getBillPaymentDetail.Remark = item.RemarkDelete;
                        //var StatusCancel = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "BillPaymentStatus" && x.Key == "7").FirstOrDefault();
                        //getBillPaymentDetail.BillPaymentStatusMasterCenterID = StatusCancel.ID;
                        //getBillPaymentDetail.BillPaymentStatusMasterCenterID = input.BillPaymentStatus.Id;

                        //TODO: [Bell OS] Add Remark cancel payment

                    }
                }
                var amount = input.SplitForBillPayment.Where(x => x.IsDelete == false).Select(x => x.Amount).Sum();
                if (getBillPaymentDetail.PayAmount == amount)
                {
                    var StatusComplete = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Complete).FirstOrDefault();
                    getBillPaymentDetail.BillPaymentStatusMasterCenterID = StatusComplete.ID;
                }
                else if (amount > getBillPaymentDetail.PayAmount)
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                    var msg = errMsg.Message.Replace("[message]", "มีการใส่ยอดเงินเกิน");
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }
                else
                {
                    var StatusSplit = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Split).FirstOrDefault();
                    getBillPaymentDetail.BillPaymentStatusMasterCenterID = StatusSplit.ID;
                }
                DB.Entry(getBillPaymentDetail).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                var msg = errMsg.Message.Replace("[field]", "");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task<BookingForBillPaymentDTO> GetBillPaymentSplitAsync(Guid id)
        {
            IQueryable<BillPaymentQueryResult> query = from o in DB.BillPaymentDetails.Where(x => x.ID == id)
                                                      .Include(x => x.BillPayment)
                                                              .ThenInclude(x => x.BankAccount)
                                                                  .ThenInclude(x => x.Bank)
                                                       .Include(x => x.BillPayment)
                                                               .ThenInclude(x => x.BankAccount)
                                                                   .ThenInclude(x => x.Company)
                                                       .Include(x => x.BillPaymentStatus)
                                                       .Include(x => x.BillPaymentDeleteReason)
                                                       .Include(x => x.CreatedBy)
                                                       .Include(x => x.UpdatedBy)

                                                       select new BillPaymentQueryResult
                                                       {
                                                           BillPaymentDetail = o,
                                                           BankAccount = o.BillPayment.BankAccount,
                                                           Bank = o.BillPayment.BankAccount.Bank,
                                                       };
            var queryResults = query.FirstOrDefault();
            var results = BookingForBillPaymentDTO.CreateFromSplitModel(queryResults.BillPaymentDetail, DB);
            return results;
        }

        public async Task CreateBillPaymentAsync(Guid PaymentHeaderID)
        {
            var chkBillPaymentHeader = DB.BillPaymentTemps.Where(x => x.ID == PaymentHeaderID).FirstOrDefault() ?? null;
            if (chkBillPaymentHeader != null)
            {
                var chkBillPaymentDetail = DB.BillPaymentDetailTemps.Include(x => x.Booking).Where(x => x.BillPaymentHeaderID == PaymentHeaderID).ToList();
                var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod && x.Key == PaymentMethodKeys.BillPayment).FirstOrDefault();
                var PaymentMethodDTO = MasterCenterDropdownDTO.CreateFromModel(PaymentMethod);

                var StatusWait = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Wait).FirstOrDefault();
                var StatusComplete = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Complete).FirstOrDefault();

                var resultPaymentDetails = chkBillPaymentDetail.Select(o => BillPaymentDetailDTO.ToModel(o, chkBillPaymentHeader.ID)).ToList();

                var resultPaymentHeader = BillPaymentHeaderDTO.ToModel(chkBillPaymentHeader);
                await DB.BillPayments.AddAsync(resultPaymentHeader);
                await DB.BillPaymentDetails.AddRangeAsync(resultPaymentDetails);
                await DB.SaveChangesAsync();

                foreach (var model in chkBillPaymentDetail)
                {
                    if (model.BillPaymentStatusMasterCenterID == StatusWait.ID && model.BookingID != null)
                    {
                        model.BillPaymentStatusMasterCenterID = StatusComplete.ID;
                        Guid tGuidBooking = model.BookingID ?? Guid.NewGuid();
                        // var paymentNewBooking = await PaymentService.GetPaymentFormAsync(tGuidBooking);
                        var paymentNewBooking = await PaymentService.GetPaymentFormAsync(tGuidBooking, PaymentFormType.BillPayment, model.ID, model.PayAmount);

                        paymentNewBooking.ReceiveDate = model.ReceiveDate;
                        paymentNewBooking.PaymentFormType = PaymentFormType.BillPayment;
                        //paymentNewBooking.RefID = model.BillPaymentDetails.Id;
                        paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                                                {
                                                    new PaymentMethodDTO
                                                            {
                                                                PayAmount = model.PayAmount,
                                                                PaymentMethodType =  PaymentMethodDTO
                                                            },
                                                };
                        var xxx = await PaymentService.SubmitPaymentFormAsync(tGuidBooking, paymentNewBooking);
                    }
                }
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                var msg = errMsg.Message.Replace("[field]", "");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task UpdateBillPaymentDetailAsync(BillPaymentHeaderDTO input)
        {

            await input.ValidateAsync(DB);
            var getBillPaymentDetail = DB.BillPaymentDetails.Where(x => x.ID == input.BillPaymentDetails.Id).FirstOrDefault() ?? null;
            if (getBillPaymentDetail != null)
            {
                getBillPaymentDetail.BillPaymentStatusMasterCenterID = input.BillPaymentDetails.BillPaymentStatus.Id;

                if (input.BillPaymentDetails.DeleteReason != null)
                {
                    getBillPaymentDetail.BillPaymentDeleteReasonMasterCenterID = input.BillPaymentDetails.DeleteReason.Id;
                    getBillPaymentDetail.Remark = input.BillPaymentDetails.Remark;
                    var StatusCancel = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus&& x.Key == BillPaymentStatusKey.Cancel).FirstOrDefault();
                    getBillPaymentDetail.BillPaymentStatusMasterCenterID = StatusCancel.ID;

                    //TODO: [Bell OS] Wait cancel payment
                }
                else if (input.BillPaymentDetails.Booking.Id != null)
                {
                    getBillPaymentDetail.BookingID = input.BillPaymentDetails.Booking.Id;
                    Guid tGuidBooking = input.BillPaymentDetails.Booking.Id ?? Guid.NewGuid();
                    var paymentNewBooking = await PaymentService.GetPaymentFormAsync(tGuidBooking, PaymentFormType.BillPayment, getBillPaymentDetail.ID, getBillPaymentDetail.PayAmount);

                    paymentNewBooking.ReceiveDate = getBillPaymentDetail.ReceiveDate;
                    paymentNewBooking.PaymentFormType = PaymentFormType.BillPayment;

                    var PaymentMethod = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentMethod" && x.Key == "7").FirstOrDefault();
                    var PaymentMethodDTO = MasterCenterDropdownDTO.CreateFromModel(PaymentMethod);

                    paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                                                {
                                                    new PaymentMethodDTO
                                                            {
                                                                PayAmount = getBillPaymentDetail.PayAmount,
                                                                PaymentMethodType =  PaymentMethodDTO
                                                            },
                                                };
                    var xxx = await PaymentService.SubmitPaymentFormAsync(tGuidBooking, paymentNewBooking);
                }

                DB.Entry(getBillPaymentDetail).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                var msg = errMsg.Message.Replace("[field]", "");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            //throw new NotImplementedException();
        }

        public async Task UpdateBillPaymentDetailTempAsync(BillPaymentHeaderDTO input)
        {
            await input.ValidateTempAsync(DB);
            var getBillPaymentDetail = DB.BillPaymentDetailTemps.Where(x => x.ID == input.BillPaymentDetails.Id).FirstOrDefault() ?? null;
            if (getBillPaymentDetail != null)
            {
                getBillPaymentDetail.BookingID = input.BillPaymentDetails.Booking.Id ?? null;
                getBillPaymentDetail.BillPaymentStatusMasterCenterID = input.BillPaymentDetails.BillPaymentStatus.Id;

                if (input.BillPaymentDetails.DeleteReason != null)
                {
                    getBillPaymentDetail.BillPaymentDeleteReasonMasterCenterID = input.BillPaymentDetails.DeleteReason.Id;
                    getBillPaymentDetail.Remark = input.BillPaymentDetails.Remark;
                    var StatusCancel = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.BillPaymentStatus && x.Key == BillPaymentStatusKey.Cancel).FirstOrDefault();
                    getBillPaymentDetail.BillPaymentStatusMasterCenterID = StatusCancel.ID;
                }
                DB.Entry(getBillPaymentDetail).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                var msg = errMsg.Message.Replace("[field]", "");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            //throw new NotImplementedException();
        }
    }
}
