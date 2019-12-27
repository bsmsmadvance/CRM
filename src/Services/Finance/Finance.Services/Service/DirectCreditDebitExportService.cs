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
using static Base.DTOs.FIN.DirectCreditDebitExportDetailDTO;
using static Base.DTOs.FIN.DirectCreditDebitExportHeaderDTO;
using Base.DTOs.MST;
using System.Globalization;
using Base.DTOs;
using ErrorHandling;
using System.IO;
using FileStorage;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IO.Compression;
using System.Data;
using System.Net;
using Database.Models.MasterKeys;

namespace Finance.Services.Service
{
    public class DirectCreditDebitExportService : IDirectCreditDebitExportService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private readonly IPaymentService PaymentService;
        private FileHelper FileHelper;

        public DirectCreditDebitExportService(DatabaseContext db, IConfiguration configuration, IPaymentService paymentService)
        {
            DB = db;
            this.PaymentService = paymentService;
            this.Configuration = configuration;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:TempBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }
        
        private IQueryable<DirectCreditDebitExportHeaderQueryResult> GetDirectCreditDebitExport(DirectCreditDebitExportHeaderFilter filter)
        {
            IQueryable<DirectCreditDebitExportHeaderQueryResult> query = from o in DB.DirectCreditDebitExportHeaders

                                                                         join dded in DB.DirectCreditDebitExportDetails.Where(x => x.Seq == 1) on o.ID equals dded.DirectCreditDebitExportHeaderID into ddedg
                                                                         from ddedo in ddedg.DefaultIfEmpty()


                                                                             // var oBacthID = DB.DirectCreditDebitExportDetails.Where(x => x.DirectCreditDebitExportHeaderID == model.DirectCreditDebitExportHeader.ID).OrderBy(x => x.Seq).Select(x => x.BatchID).FirstOrDefault();

                                                                             //.Select(o => new DirectCreditDebitExportHeaderQueryResult
                                                                             //               {
                                                                         select new DirectCreditDebitExportHeaderQueryResult
                                                                         {
                                                                             DirectCreditDebitExportHeader = o,
                                                                             Bank = o.BankAccount.Bank,
                                                                             BankAccount = o.BankAccount,
                                                                             Company = o.BankAccount.Company,
                                                                             DirectFormType = o.DirectFormType,
                                                                             BacthID = ddedo.BatchID
                                                                         };
            #region filter
            if (filter.BatchID != null)
            {
                query = query.Where(x => x.BacthID.Contains(filter.BatchID));
            }
            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(filter.BankID.ToString(), out newGuid))
            {
                query = query.Where(x => x.Bank.ID == filter.BankID);
            }
            if ((filter.BankAccountNo ?? "") != "")
            {
                query = query.Where(o => o.BankAccount.BankAccountNo.Contains(filter.BankAccountNo));
            }
            if (Guid.TryParse(filter.CompanyID.ToString(), out newGuid))
            {
                query = query.Where(x => x.Company.ID == filter.CompanyID);
            }
            if (filter.PeriodDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.PeriodDate >= filter.PeriodDateFrom);
            }
            if (filter.PeriodDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.PeriodDate <= filter.PeriodDateTo);
            }
            if (filter.ReceiveDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.ReceiveDate >= filter.ReceiveDateFrom);
            }
            if (filter.PeriodDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.ReceiveDate <= filter.PeriodDateTo);
            }
            if (Guid.TryParse(filter.DirectFormTypeMasterCenterID.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectFormType.ID == filter.DirectFormTypeMasterCenterID);
            }
            if (filter.TotalRecord != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.TotalRecord == filter.TotalRecord);
            }
            if (filter.TotalErrorRecord != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.TotalErrorRecord == filter.TotalErrorRecord);
            }
            if (filter.TotalAmountFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.TotalAmount >= filter.TotalAmountFrom);
            }
            if (filter.TotalAmountTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.TotalAmount <= filter.TotalAmountTo);
            }
            if (filter.ImportDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.ImportDate >= filter.ImportDateFrom);
            }
            if (filter.ImportDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportHeader.ImportDate <= filter.ImportDateTo);
            }
            if ((filter.ImportBy ?? "") != "")
            {
                query = query.Where(o => o.DirectCreditDebitExportHeader.UpdatedBy.DisplayName.Contains(filter.ImportBy));
            }
            #endregion
            return query;
        }

        public async Task<DirectCreditDebitExportHeaderPaging> GetDirectCreditDebitExportListAsync(DirectCreditDebitExportHeaderFilter filter, PageParam pageParam, DirectCreditDebitExportHeaderSortByParam sortByParam)
        {

            IQueryable<DirectCreditDebitExportHeaderQueryResult> query = GetDirectCreditDebitExport(filter);


            PageOutput pageOuput = null;
            List<DirectCreditDebitExportHeaderQueryResult> queryResults = new List<DirectCreditDebitExportHeaderQueryResult>();
            DirectCreditDebitExportHeaderDTO.SortBy(sortByParam, ref query);

            pageOuput = PagingHelper.Paging<DirectCreditDebitExportHeaderQueryResult>(pageParam, ref query);

            queryResults = query.ToList();
            var results = queryResults.Select(o => DirectCreditDebitExportHeaderDTO.CreateFromQueryResult(o, DB)).ToList();

            return new DirectCreditDebitExportHeaderPaging()
            {
                DirectCreditDebitExportHeaders = results,
                PageOutput = pageOuput
            };

        }

        public async Task<DirectCreditDebitExportDetailPaging> GetDirectCreditDebitExportAsync(Guid id, DirectCreditDebitExportDetailFilter filter, PageParam pageParam, DirectCreditDebitExportDetailSortByParam sortByParam)
        {
            IQueryable<DirectCreditDebitExportHeaderQueryResult> query = from o in DB.DirectCreditDebitExportDetails.Where(x => x.DirectCreditDebitExportHeaderID == id)
                .Include(x => x.UnitPriceInstallment)
                    .ThenInclude(x => x.InstallmentOfUnitPriceItem)
                .Include(x => x.DirectCreditDebitApprovalForm)
                    .ThenInclude(x => x.Booking)
                        .ThenInclude(x => x.Unit)
                           .ThenInclude(x => x.Project)
                .Include(x => x.DirectCreditDebitExportDetailStatus)


                                                                         join arg in DB.Agreements.Where(x => x.IsDeleted == false) on o.DirectCreditDebitApprovalForm.BookingID equals arg.BookingID into argg
                                                                         from argModel in argg.DefaultIfEmpty()

                                                                         select new DirectCreditDebitExportHeaderQueryResult
                                                                         {
                                                                             DirectCreditDebitExportDetail = o,
                                                                             DirectCreditDebitExportHeader = o.DirectCreditDebitExportHeader,
                                                                             Bank = o.DirectCreditDebitExportHeader.BankAccount.Bank,
                                                                             BankAccount = o.DirectCreditDebitExportHeader.BankAccount,
                                                                             Company = o.DirectCreditDebitExportHeader.BankAccount.Company,
                                                                             DirectFormType = o.DirectCreditDebitExportHeader.DirectFormType,
                                                                             Agreement = argModel ?? new Database.Models.SAL.Agreement()

                                                                         };

            #region filter

            if (filter.BatchID != null)
            {
                query = query.Where(x => x.BacthID.Contains(filter.BatchID));
            }
            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(filter.ProjectID.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.Booking.Project.ID == filter.ProjectID);
            }
            if (filter.BatchID != null)
            {
                query = query.Where(x => x.BacthID.Contains(filter.BatchID));
            }
            if (filter.UnitNo != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.Booking.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (filter.PeriodDate != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.DirectPeriod.ToString().Contains(filter.PeriodDate.ToString()));
            }
            if (filter.DueDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate >= filter.DueDateFrom);
            }
            if (filter.DueDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitExportDetail.UnitPriceInstallment.DueDate <= filter.DueDateTo);
            }
            if (filter.AgreementNo != null)
            {
                query = query.Where(x => x.Agreement.AgreementNo.Contains(filter.AgreementNo));
            }
            if (filter.CustomerName != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.OwnerName.Contains(filter.CustomerName));
            }
            if (filter.AmountFrom != null)
            {
                query = query.Where(x => (x.DirectCreditDebitExportDetail.UnitPriceInstallment.Amount - x.DirectCreditDebitExportDetail.UnitPriceInstallment.PaidAmount) >= filter.AmountFrom);
            }
            if (filter.AmountTo != null)
            {
                query = query.Where(x => (x.DirectCreditDebitExportDetail.UnitPriceInstallment.Amount - x.DirectCreditDebitExportDetail.UnitPriceInstallment.PaidAmount) <= filter.AmountTo);
            }
            if (Guid.TryParse(filter.DirectCreditDebitExportDetailStatus.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.ID == filter.DirectCreditDebitExportDetailStatus);
            }
            #endregion
            DirectCreditDebitExportHeaderDTO.SortByDetail(sortByParam, ref query);

            var queryResults = query.ToList();
            if (queryResults.Count > 0)
            {
                PageOutput pageOuput = null;
                pageOuput = PagingHelper.PagingList<DirectCreditDebitExportHeaderQueryResult>(pageParam, ref queryResults);
                var results = queryResults.Select(o => DirectCreditDebitExportHeaderDTO.CreateFromByIDQueryResult(o, DB)).ToList();

                return new DirectCreditDebitExportDetailPaging()
                {
                    DirectCreditDebitExportHeader = results,
                    PageOutput = pageOuput
                };
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

        public async Task<FileDTO> CreateDirectCreditDebitExportAsync(DirectCreditDebitExportDTO input)
        {

            await input.ValidateAsync(DB);

            string tDate = null;

            if (input.PeriodDay.ToString().Length == 1)
                tDate = "0" + input.PeriodDay.ToString() + "/";
            else
                tDate = input.PeriodDay.ToString() + "/";

            if (input.PeriodMounth.ToString().Length == 1)
                tDate = tDate + "0" + input.PeriodMounth.ToString() + "/";
            else
                tDate = tDate + input.PeriodMounth.ToString() + "/";

            tDate = tDate + input.PeriodYear.ToString();

            DateTime dDueDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));

            var DirectStatusApprovedMasterCenterID = DB.MasterCenters
                .Where(x => x.Key == DirectApprovalFormStatusKey.Approved && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus).Select(x => x.ID).FirstOrDefault();

            var MasterPriceItems = DB.MasterPriceItems
                   .Where(x => x.Key == "DownAmount").Select(x => x.ID).FirstOrDefault();

            //var UnitStatusMasterCenterID = DB.MasterCenters
            //  .Where(x => x.Key == UnitStatusKeys.Transfer && x.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus).Select(x => x.ID).FirstOrDefault();

            IQueryable<DirectCreditDebitApprovalFormExportQueryResult> query = from o in DB.DirectCreditDebitApprovalForms
                                                                               .Include(x => x.BankAccount)
                                                                               .Include(x => x.Booking)
                                                                                    .ThenInclude(x => x.Unit)
                                                                                        .ThenInclude(x => x.UnitStatus)
                                                                               .Where(x => x.DirectApprovalFormStatusMasterCenterID == DirectStatusApprovedMasterCenterID)

                                                                               join amt in DB.Agreements.Where(x => x.IsDeleted == false) on o.BookingID equals amt.BookingID into amtg
                                                                               from amto in amtg.DefaultIfEmpty()


                                                                               join tf in DB.Transfers on amto.ID equals tf.AgreementID into tfData
                                                                               from tfModel in tfData.DefaultIfEmpty()


                                                                               join up in DB.UnitPrices on o.BookingID equals up.BookingID into upg
                                                                               from upo in upg.DefaultIfEmpty()

                                                                               join ups in DB.UnitPriceItems.Where(x => x.MasterPriceItemID == MasterPriceItems)
                                                                               on upo.ID equals ups.UnitPriceID into upsg
                                                                               from upso in upsg.DefaultIfEmpty()

                                                                               join upi in DB.UnitPriceInstallments.Where(x => x.DueDate <= dDueDate && (x.Amount - x.PaidAmount) > 0) on upso.ID equals upi.InstallmentOfUnitPriceItemID into upig
                                                                               from upio in upig.DefaultIfEmpty()

                                                                               select new DirectCreditDebitApprovalFormExportQueryResult
                                                                               {
                                                                                   DirectCreditDebitApprovalForm = o,
                                                                                   Agreement = amto ?? new Database.Models.SAL.Agreement(),
                                                                                   Project = o.Booking.Project,
                                                                                   Unit = o.Booking.Unit,
                                                                                   BankAccount = o.BankAccount,
                                                                                   UnitPrice = upo ?? new Database.Models.SAL.UnitPrice(),
                                                                                   UnitPriceItem = upso ?? new Database.Models.SAL.UnitPriceItem(),
                                                                                   UnitPriceInstallment = upio ?? new Database.Models.SAL.UnitPriceInstallment(),
                                                                                   Transfer = tfModel ?? new Database.Models.SAL.Transfer()
                                                                               };

            #region filter
           
            query = query.Where(x => x.Transfer.IsReadyToTransfer == false);

            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(input.Company.Id.ToString(), out newGuid))
            {
                query = query.Where(x => x.Project.CompanyID == input.Company.Id);
            }
            if (Guid.TryParse(input.BankAccount.Id.ToString(), out newGuid))
            {
                query = query.Where(x => x.BankAccount.ID == input.BankAccount.Id);
            }
            if (Guid.TryParse(input.DirectFormType.Id.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.DirectApprovalFormTypeMasterCenterID == input.DirectFormType.Id);
            }
            #endregion
            var DataQuery = query.ToList();

            if (DataQuery.Count > 0)
            {
                //สร้าง Header
                //////////////////////////////////////////////////////////////////////////////////////////
                Guid idHeader = Guid.NewGuid();
                var modelHeader = new DirectCreditDebitExportHeader();
                modelHeader.ID = idHeader;
                modelHeader.BankAccountID = input.BankAccount.Id;
                modelHeader.PeriodDate = dDueDate;
                modelHeader.TotalRecord = DataQuery.Select(x => x.DirectCreditDebitApprovalForm).ToList().Count();
                modelHeader.TotalAmount = DataQuery.Select(t => t.UnitPriceInstallment.Amount - t.UnitPriceInstallment.PaidAmount).Sum();
                modelHeader.DirectFormTypeMasterCenterID = input.DirectFormType.Id;
                modelHeader.ReceiveDate = input.ReceiveDate;
                await DB.DirectCreditDebitExportHeaders.AddAsync(modelHeader);
                await DB.SaveChangesAsync();
                //สร้าง Detail
                int Seq = 1;
                var oBatchID = DB.DirectCreditDebitExportDetails.Include(x => x.DirectCreditDebitExportHeader).Where(x => x.DirectCreditDebitExportHeader.PeriodDate == dDueDate).OrderByDescending(x => x.BatchID).Select(x => x.BatchID).ToList();
                int numBatch = 1;

                var oDirectCreditDebitExportDetailStatusMasterCenterID = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectCreditDebitExportDetailStatus && x.Key == DirectCreditDebitExportDetailStatusKeys.Wait && x.IsDeleted == false).Select(x => x.ID).FirstOrDefault();

                if (oBatchID.Count > 0)
                {
                    var tBatchID = oBatchID.FirstOrDefault();

                    //runDate = tBatchID.Substring(0, 8);
                    numBatch = Convert.ToInt32(tBatchID.Substring(8));
                    numBatch = numBatch + 1;
                    //runBatch = numBatch.ToString();
                }

                int RunningID = 1;
                Decimal sumAmount = 0;
                string runDate = null;
                var DirectCredit = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "DirectApprovalFormType" && x.Key == "1").FirstOrDefault();
                var DirectDebit = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "DirectApprovalFormType" && x.Key == "2").FirstOrDefault();

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                using (StreamWriter output = new StreamWriter(stream))
                {
                    var ChkBank = DB.BankAccounts
                              .Include(x => x.Bank)
                              .Include(x => x.Company)
                          .Where(x => x.ID == input.BankAccName.BankAccountID).FirstOrDefault();

                    switch (ChkBank.Bank.BankNo)
                    {
                        case "004": // K Bank
                            runDate = dDueDate.Year.ToString("0000") + dDueDate.Month.ToString("00") + dDueDate.Day.ToString("00");

                            break;
                        case "002": // SCB
                            runDate = dDueDate.Year.ToString("00") + dDueDate.Month.ToString("00") + dDueDate.Day.ToString("00");

                            if (DirectCredit.ID == input.DirectFormType.Id)
                            {
                                output.WriteLine(
                                 "00" +
                                 dDueDate.ToString("dd") + dDueDate.ToString("MM") + dDueDate.ToString("yy") + ChkBank.ServiceCode.LeadingDigit(6, " ") +
                                 "454852" + ChkBank.MerchantID + "".LeadingDigit(43, " ") + "0000" + RunningID.ToString("00000") + "".LeadingDigit(17, " ") + "0000"
                                        );
                                RunningID++;
                            }
                            else if (DirectDebit.ID == input.DirectFormType.Id)
                            {
                                if (ChkBank.Company.NameEN.Length > 30)
                                {
                                    ChkBank.Company.NameEN = ChkBank.Company.NameEN.Substring(0, 30);
                                }

                                string tAmount = DataQuery.Select(t => t.UnitPriceInstallment.Amount - t.UnitPriceInstallment.PaidAmount).Sum().ToString("0.00");
                                tAmount = tAmount.Replace(".", "");

                                output.WriteLine(
                                         "".LeadingDigit(8, " ") + "M" + ChkBank.ServiceCode.LeadingDigit(6, " ") + ChkBank.Company.NameEN + "C" + "A" +
                                         input.ReceiveDate.ToString("dd") + input.ReceiveDate.ToString("MM") + input.ReceiveDate.ToString("yy") +
                                         DataQuery.Select(x => x.DirectCreditDebitApprovalForm).ToList().Count().ToString().LeadingDigit(5, "0") +
                                         tAmount.LeadingDigit(11, "0") + "I"
                                 );
                            }
                            break;
                    }

                    //string BatchID = runDate + numBatch.ToString("000000");
                    foreach (var item in DataQuery)
                    {
                        decimal chkAmount = item.UnitPriceInstallment.Amount - item.UnitPriceInstallment.PaidAmount;

                        string tAmount = chkAmount.ToString("0.00");
                        tAmount = tAmount.Replace(".", "");
                        string tBatchID = runDate + numBatch.ToString("000000");
                        var modelDetail = new DirectCreditDebitExportDetail();
                        modelDetail.ID = Guid.NewGuid();
                        modelDetail.DirectCreditDebitExportHeaderID = idHeader;
                        modelDetail.DirectCreditDebitApprovalFormID = item.DirectCreditDebitApprovalForm.ID;
                        modelDetail.Seq = Seq;
                        modelDetail.BatchID = tBatchID;
                        modelDetail.UnitPriceInstallmentID = item.UnitPriceInstallment.ID;
                        modelDetail.Amount = chkAmount;
                        modelDetail.DirectCreditDebitExportDetailStatusMasterCenterID = oDirectCreditDebitExportDetailStatusMasterCenterID;
                        sumAmount = sumAmount + chkAmount;



                        //text file

                        switch (ChkBank.Bank.BankNo)
                        {
                            case "004": // K Bank
                                output.WriteLine(

                                                 RunningID.ToString("000000") + " " + "7441" + " " +
                                                item.DirectCreditDebitApprovalForm.BankAccount.MerchantID.LeadingDigit(7, "0") + " " +
                                                item.DirectCreditDebitApprovalForm.AccountNO + " " + tAmount.LeadingDigit(15, "0") + " " +
                                                dDueDate.ToString("yy") + dDueDate.ToString("MM") + dDueDate.ToString("dd") + " " +
                                                tBatchID.LeadingDigit(23, "0") + " " +
                                                " ".LeadingDigit(50, " ")
                                                );
                                break;
                            case "002": // SCB
                                if (DirectCredit.ID == input.DirectFormType.Id)
                                {
                                    output.WriteLine(
                                           "05" + item.DirectCreditDebitApprovalForm.AccountNO + "".LeadingDigit(3, " ") +
                                            dDueDate.ToString("dd") + dDueDate.ToString("MM") + dDueDate.ToString("yy") + "".LeadingDigit(6, " ") +
                                            tAmount.LeadingDigit(11, "0") +
                                            tBatchID.LeadingDigit(12, "0") +
                                            item.DirectCreditDebitApprovalForm.BankAccount.MerchantID.LeadingDigit(7, "0") + "".LeadingDigit(7, " ") + "".LeadingDigit(4, " ") +
                                            RunningID.ToString("00000") + "".LeadingDigit(17, " ") + "0000"
                                               );
                                }
                                else if (DirectDebit.ID == input.DirectFormType.Id)
                                {
                                    output.WriteLine(

                                        item.DirectCreditDebitApprovalForm.AccountNO + dDueDate.ToString("dd") + dDueDate.ToString("MM") + dDueDate.ToString("yy") + "1"+
                                         tAmount.LeadingDigit(11, "0") + "".LeadingDigit(2, " ") + tBatchID.LeadingDigit(35, "0")
                                              );
                                }
                                break;
                        }

                        /////////////////////////////////////////////////////////////////////

                        await DB.DirectCreditDebitExportDetails.AddAsync(modelDetail);
                        await DB.SaveChangesAsync();
                        Seq++;
                        RunningID++;
                        numBatch++;
                    }
                    string tsumAmount = sumAmount.ToString("0.00");
                    tsumAmount = tsumAmount.Replace(".", "");
                    var MerchantID = DataQuery.Select(x => x.DirectCreditDebitApprovalForm.BankAccount.MerchantID).FirstOrDefault();


                    switch (ChkBank.Bank.BankNo)
                    {
                        case "004": // K Bank
                            output.WriteLine(
                                            RunningID.ToString("000000") + " " + "9100" + " " +
                                            MerchantID.LeadingDigit(7, "0") + " " +
                                            "0000000000" + " " + tsumAmount + " " + "000000"
                                            );
                            break;
                        case "002": // SCB
                            if (DirectCredit.ID == input.DirectFormType.Id)
                            {
                                output.WriteLine(
                                    "92" + "1" + DataQuery.Select(x => x.DirectCreditDebitApprovalForm).ToList().Count().ToString().LeadingDigit(6, "0") + tsumAmount.LeadingDigit(11, "0") +
                                    "".LeadingDigit(45, " ") + "0000" + RunningID.ToString("000000") + "".LeadingDigit(17, " ") + "".LeadingDigit(4, " ")
                                    );
                            }
                            else if (DirectDebit.ID == input.DirectFormType.Id)
                            {

                            }
                            break;
                    }
                    output.Flush();

                    Stream fileStream = new MemoryStream(stream.ToArray());
                    string fileName = "TextExport" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".txt";
                    string filePath = $"DirectCreditDebitExport/Export/{idHeader}";
                    string contentType = "text/*";

                    var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, Configuration["Minio:TempBucket"], filePath, fileName, contentType);

                    return new FileDTO()
                    {
                        Name = fileName,
                        Url = uploadResult.Url
                    };
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

        public async Task DeleteDirectCreditDebitExportAsync(Guid ID)
        {

            var DirectCreditDebitExportHeader = DB.DirectCreditDebitExportHeaders.Where(x => x.ID == ID).FirstOrDefault() ?? null;
            if (DirectCreditDebitExportHeader != null)
            {
                DirectCreditDebitExportHeader.IsDeleted = true;

                DB.Entry(DirectCreditDebitExportHeader).State = EntityState.Modified;
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

        private List<listBatch> getDataByBatchID(Guid ID, StreamReader reader)
        {
            string line;
            var DirectCreditDebitExportHeader = DB.DirectCreditDebitExportHeaders
                            .Include(x => x.BankAccount)
                                .ThenInclude(x => x.Bank)
                            .Where(x => x.ID == ID).FirstOrDefault();


            var ChkBank = DirectCreditDebitExportHeader.BankAccount.Bank.BankNo;
            var ChkType = DirectCreditDebitExportHeader.DirectFormTypeMasterCenterID;
            List<listBatch> listBatchID = new List<listBatch>();

            switch (ChkBank)
            {
                case "004": // K Bank
                    while ((line = reader.ReadLine()) != null)
                    {
                        listBatch newList = new listBatch();
                        if (line.Length >= 77)
                        {
                            newList.BatchID = line.Substring(63, 14);


                            if (line.Length >= 79)
                            {
                                string chkStr = line.Substring(79).Replace(" ", null) ?? null;
                                if (chkStr != null && chkStr != "")
                                {
                                    newList.TransCode = line.Substring(79);
                                }
                            }
                            listBatchID.Add(newList);
                        }
                    }
                    break;
                case "002":
                    var DirectCredit = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "DirectApprovalFormType" && x.Key == "1").FirstOrDefault();
                    var DirectDebit = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == "DirectApprovalFormType" && x.Key == "2").FirstOrDefault();
                    if (ChkType == DirectCredit.ID)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            listBatch newList = new listBatch();
                            if (line.Length >= 57)
                            {
                                newList.BatchID = line.Substring(44, 12);


                                if (line.Length >= 101)
                                {
                                    string chkStr = line.Substring(79).Replace(" ", null) ?? null;
                                    if (chkStr != null && chkStr != "")
                                    {
                                        newList.TransCode = line.Substring(96, 4);
                                    }
                                }
                                listBatchID.Add(newList);
                            }
                        }
                    }
                    else if (ChkType == DirectDebit.ID)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            listBatch newList = new listBatch();
                            if (line.Length >= 35)
                            {
                                newList.BatchID = line.Substring(35, 20);

                                if (line.Length >= 315)
                                {
                                    string chkStr = line.Substring(79).Replace(" ", null) ?? null;
                                    if (chkStr != null && chkStr != "")
                                    {
                                        newList.TransCode = line.Substring(314, 1);
                                    }
                                }
                                listBatchID.Add(newList);
                            }
                        }
                    }
                    break;

            }

            return listBatchID;
        }
        public async Task<List<DirectCreditDebitExportHeaderDTO>> ImportDirectCreditDebitAsync(FileWithIDDTO fileDTO)
        {
            List<listBatch> listBatchID = new List<listBatch>();
            try
            {
                if (fileDTO == null)
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0089").FirstAsync();
                    var msg = errMsg.Message.Replace("[field]", "");
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }
                StreamReader reader = new StreamReader(WebRequest.Create(fileDTO.FileDTO.Url).GetResponse().GetResponseStream());

                listBatchID = getDataByBatchID(fileDTO.ID, reader);

                var StatusFail = DB.MasterCenters.Where(x => x.Key == DirectCreditDebitExportDetailStatusKeys.Fail && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectCreditDebitExportDetailStatus).FirstOrDefault();
                var StatusTransferUnit = DB.MasterCenters.Where(x => x.Key == DirectCreditDebitExportDetailStatusKeys.TransferUnit && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectCreditDebitExportDetailStatus).FirstOrDefault();
                var StatusComplete = DB.MasterCenters.Where(x => x.Key == DirectCreditDebitExportDetailStatusKeys.Complete && x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectCreditDebitExportDetailStatus).FirstOrDefault();

                IQueryable<DirectCreditDebitExportHeaderQueryResult> query = from o in DB.DirectCreditDebitExportDetails
                                                                                   .Include(x => x.DirectCreditDebitApprovalForm)
                                                                                        .ThenInclude(x => x.Booking)
                                                                                            .ThenInclude(x => x.Unit)
                                                                                            .ThenInclude(x => x.Project)
                                                                                    .Include(x => x.UnitPriceInstallment)
                                                                                    .Include(x => x.DirectCreditDebitExportDetailStatus)
                                                                                 //.Where(x => x.DirectCreditDebitExportHeaderID == fileDTO.ID)
                                                                                 .Where(x => x.DirectCreditDebitExportHeaderID == fileDTO.ID && listBatchID.Any(z => z.BatchID == x.BatchID))
                                                                             join amt in DB.Agreements.Where(x => x.IsDeleted == false) on o.DirectCreditDebitApprovalForm.BookingID equals amt.BookingID into amtg
                                                                             from amto in amtg.DefaultIfEmpty()

                                                                             join trn in DB.Transfers.Where(x => x.IsDeleted == false) on amto.ID equals trn.AgreementID into trng
                                                                             from trno in trng.DefaultIfEmpty()

                                                                             join btc in listBatchID on o.BatchID equals btc.BatchID into btcg
                                                                             from btco in btcg.ToList()


                                                                                 //join upi in DB.UnitPriceInstallments on o.UnitPriceInstallmentID equals upi.InstallmentOfUnitPriceItemID into upig
                                                                                 //from upio in upig.DefaultIfEmpty()

                                                                             select new DirectCreditDebitExportHeaderQueryResult
                                                                             {
                                                                                 DirectCreditDebitExportDetail = o,
                                                                                 DirectCreditDebitExportHeader = o.DirectCreditDebitExportHeader,
                                                                                 DirectCreditDebitApprovalForm = o.DirectCreditDebitApprovalForm,
                                                                                 Bank = o.DirectCreditDebitExportHeader.BankAccount.Bank,
                                                                                 BankAccount = o.DirectCreditDebitExportHeader.BankAccount,
                                                                                 Company = o.DirectCreditDebitExportHeader.BankAccount.Company,
                                                                                 DirectFormType = o.DirectCreditDebitExportHeader.DirectFormType,
                                                                                 Agreement = amto ?? new Database.Models.SAL.Agreement(),
                                                                                 Transfer = trno ?? new Database.Models.SAL.Transfer(),

                                                                                 BatchStatus = btco ?? new listBatch()
                                                                             };
                var ChkDetail = query.ToList();


                foreach (var item in ChkDetail)
                {
                    if (item.Transfer.AgreementID != null)
                    {
                        if (item.Transfer.AgreementID != null)
                            item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus = StatusTransferUnit;
                    }
                    else if (item.BatchStatus.BatchID != null)
                    {
                        if (item.BatchStatus.TransCode != null)
                        {
                            item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus = StatusFail;
                            item.DirectCreditDebitExportDetail.TransCode = item.BatchStatus.TransCode;
                        }
                        else
                        {
                            item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus = StatusComplete;
                        }
                    }
                    else
                    {
                        item.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus = StatusFail;
                        item.DirectCreditDebitExportDetail.TransCode = "ไม่พบข้อมูล";
                    }
                }
                if (fileDTO.FileDTO.IsTemp)
                {
                    string pathName = $"DirectCreditDebitExport/Import/{fileDTO.ID}/{fileDTO.FileDTO.Name}";
                    await FileHelper.MoveTempFileAsync(fileDTO.FileDTO.Name, pathName);
                }
                var results = ChkDetail.Select(o => DirectCreditDebitExportHeaderDTO.CreateFromByIDQueryResult(o, DB)).ToList();
                return results;
            }
            catch (Exception ex)
            {
                var tException = ex.ToString();
                return new List<DirectCreditDebitExportHeaderDTO>();
            }
        }

        public async Task<List<DirectCreditDebitExportHeaderDTO>> GetDirectCreditDebitDetailAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task ConfirmPaymentAsync(List<DirectCreditDebitExportHeaderDTO> input)
        {
            if (input.Count > 0)
            {
                var tGuid = input.Select(x => x.DirectCreditDebitExportDetail.Id).ToList();
                var query = DB.DirectCreditDebitExportDetails.Where(x => tGuid.Any(a => a == x.ID)).Include(x => x.DirectCreditDebitExportHeader).ThenInclude(x => x.DirectFormType).ToList();
                //List<DirectCreditDebitExportDetail> newDetail = new List<DirectCreditDebitExportDetail>();

                foreach (var model in input)
                {
                    var newModel = query.Where(x => x.ID == model.DirectCreditDebitExportDetail.Id).FirstOrDefault();

                    newModel.DirectCreditDebitExportDetailStatusMasterCenterID = model.DirectCreditDebitExportDetail.DirectCreditDebitExportDetailStatus.Id;
                    newModel.TransCode = model.DirectCreditDebitExportDetail.TransCode;

                    //newDetail.Add(newModel);
                    DB.Entry(newModel).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                    var paymentNewBooking = await PaymentService.GetPaymentFormAsync(newModel.DirectCreditDebitApprovalForm.BookingID);

                    paymentNewBooking.ReceiveDate = newModel.DirectCreditDebitExportHeader.PeriodDate;

                    if (newModel.DirectCreditDebitExportHeader.DirectFormType.Key == "1")
                    {
                        paymentNewBooking.PaymentFormType = PaymentFormType.DirectCredit;
                    }
                    else
                    {
                        paymentNewBooking.PaymentFormType = PaymentFormType.DirectDebit;
                    }

                    paymentNewBooking.RefID = newModel.ID;
                    var Amount = newModel.Amount;
                    paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                                            {
                                                new PaymentMethodDTO
                                                        {
                                                            PayAmount =Amount,
                                                            PaymentMethodType = model.DirectFormType,
                                                        },
                                            };
                    await PaymentService.SubmitPaymentFormAsync(newModel.DirectCreditDebitApprovalForm.BookingID, paymentNewBooking);
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
            ///////////////////////////////////////////////////////
            //throw new NotImplementedException();
        }

        public async Task<string> PrintAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        //public async Task DeleteDirectCreditDebitExportAsync(Guid id)
        //     //public async Task<DirectCreditDebitExportHeader> DeleteDirectCreditDebitExportAsync(Guid id)
        //{
        //    var model = DB.DirectCreditDebitExportHeaders.Where(o => o.ID == id).FirstOrDefault();
        //    model.IsDeleted = true;
        //    await DB.SaveChangesAsync();
        //    //return model;
        //    throw new NotImplementedException();
        //}

        public async Task<List<BankAccountDropdownDTO>> GetBankAccountDropdowListAsync(Guid ComID, Guid BankID, string ChkKeyDirectCreditDebit)
        {

            IQueryable<BankAccount> query = DB.BankAccounts;

            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(ComID.ToString(), out newGuid))
            {
                query = query.Where(x => x.CompanyID == ComID);
            }
            if (Guid.TryParse(BankID.ToString(), out newGuid))
            {
                query = query.Where(x => x.Bank.ID == BankID);
            }

            switch (ChkKeyDirectCreditDebit)
            {
                case "1":
                    query = query.Where(o => o.IsDirectCredit == true && o.Bank.IsDeleted == false);
                    break;

                case "2":
                    query = query.Where(o => o.IsDirectDebit == true && o.Bank.IsDeleted == false);
                    break;
            }

            if (query.ToList().Count() > 0)
            {
                var results = query.Select(o => BankAccountDropdownDTO.CreateFromModel(o)).ToList();

                return results;
            }
            else
            {
                return new List<BankAccountDropdownDTO>();
            }
        }

    }
}
