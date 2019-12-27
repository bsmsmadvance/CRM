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

namespace Finance.Services.Service
{
    public class DepositService : IDepositService //#คิม
    {
        private readonly DatabaseContext DB;

        public DepositService(DatabaseContext db)
        {
            DB = db;
        }


        // Get Data
        public async Task<List<DepositDetailDTO>> GetDepositAsync(Guid id)
        {
            var depositQuery = GetDepositDetailQueryResult();

            var queryResults = await depositQuery.Where(o => o.DepositHeader.ID == id).ToListAsync();

            var result = queryResults.Select(o => DepositDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return result;
        }

        public async Task<DepositPaging> GetDepositListAsync(DepositFilter filter, PageParam pageParam, DepositSortByParam sortByParam)
        {
            var depositQuery = GetDepositDetailQueryResult();

            // Filter
            DepositFilter(filter, ref depositQuery);

            DepositDetailDTO.SortBy(sortByParam, ref depositQuery);

            var pageOuput = PagingHelper.Paging(pageParam, ref depositQuery);

            var queryResults = await depositQuery.ToListAsync();

            var result = queryResults.Select(o => DepositDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return new DepositPaging()
            {
                DepositDetails = result,
                PageOutput = pageOuput
            };
        }

        // Insert
        public async Task<List<DepositDetailDTO>> CreateDepositAsync(List<DepositDetailDTO> input)
        {
            await DepositDetailDTO.ValidateAsync(DB, input);

            DepositHeader DepositHeader = new DepositHeader();
            List<DepositDetail> DepositDetail = new List<DepositDetail>();

            CreateDepositModelAsync(ref DepositHeader, ref DepositDetail, input, Guid.Empty);

            await DB.DepositHeaders.AddAsync(DepositHeader);
            await DB.DepositDetails.AddRangeAsync(DepositDetail);
            await DB.SaveChangesAsync();

            var depositQuery = GetDepositDetailQueryResult();
            var queryResults = await depositQuery.Where(o => o.DepositHeader.ID == DepositHeader.ID).ToListAsync();

            var result = queryResults.Select(o => DepositDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return result;
        }

        // Delete
        public async Task DeleteDepositAsync(Guid id)
        {
            var modelHeader = await DB.DepositHeaders.Where(c => c.ID == id).FirstOrDefaultAsync();
            if (modelHeader != null)
            {
                modelHeader.IsDeleted = true;
                DB.Entry(modelHeader).State = EntityState.Modified;

                var modelDetail = await DB.DepositDetails.Where(c => c.DepositHeaderID == id).ToListAsync();

                if (modelDetail.Any())
                {
                    modelDetail = modelDetail.Select(c => { c.IsDeleted = true; return c; }).ToList();

                    foreach (var item in modelDetail)
                    {
                        DB.Entry(item).State = EntityState.Modified;
                    }

                    await DB.SaveChangesAsync();
                }
            }
        }


        // Update
        public async Task<List<DepositDetailDTO>> UpdateDepositAsync(Guid id, List<DepositDetailDTO> input)
        {
            // Flag Delete Old Data
            await DeleteDepositAsync(id);

            // Insert New Data
            DepositHeader DepositHeader = new DepositHeader();
            List<DepositDetail> DepositDetail = new List<DepositDetail>();
            CreateDepositModelAsync(ref DepositHeader, ref DepositDetail, input, id);



            await DB.DepositHeaders.AddAsync(DepositHeader);
            await DB.DepositDetails.AddRangeAsync(DepositDetail);
            await DB.SaveChangesAsync();

            var depositQuery = GetDepositDetailQueryResult();
            var queryResults = await depositQuery.Where(o => o.DepositHeader.ID == DepositHeader.ID).ToListAsync();

            var result = queryResults.Select(o => DepositDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return result;
        }

        public async Task<DepositPaging> GetDepositListForUpdateAsync(Guid id, List<Guid> listNewID, DepositFilter filter, PageParam pageParam, DepositSortByParam sortByParam)
        {
            var depositQuery = GetDepositDetailQueryResult();

            var OldData = depositQuery.ToList() ?? new List<DepositDetailQueryResult>();

            OldData = depositQuery.Where(e => e.DepositHeader.ID == id).ToList() ?? new List<DepositDetailQueryResult>();

            if (OldData.Any())
            {
                filter.PaymentMethodTypeID = OldData.FirstOrDefault().PaymentMethodType.ID;

                filter.CompanyID = OldData.FirstOrDefault().Payment.Booking.Project.Company.ID;
                filter.BankAccountID = OldData.FirstOrDefault().BankAccount.ID;
                filter.ProjectID = OldData.FirstOrDefault().Project.ID;

                filter.IsDeposit = false;
                filter.IsPostGL = false;
            }

            // Filter
            DepositFilter(filter, ref depositQuery);

            depositQuery = depositQuery.Where(p => p.DepositHeader.ID != id || !listNewID.Any(p2 => p2 == p.PaymentMethod.ID));

            DepositDetailDTO.SortBy(sortByParam, ref depositQuery);

            var pageOuput = PagingHelper.Paging(pageParam, ref depositQuery);

            var queryResults = await depositQuery.ToListAsync();

            var result = queryResults.Select(o => DepositDetailDTO.CreateFromQueryResult(o, DB)).ToList();

            return new DepositPaging()
            {
                DepositDetails = result,
                PageOutput = pageOuput
            };
        }

        private void CreateDepositModelAsync(ref DepositHeader DepositHeader, ref List<DepositDetail> DepositDetail, List<DepositDetailDTO> input, Guid? ReferentDepositHeaderID)
        {
            var HeaderModel = input.Select(o => o.DepositHeader).FirstOrDefault();

            #region Running

            /*
                Format : <PI><ProjectID><yy><MM><NNNN>
                Table  : MST.RunningNumberCounters
                Column : Key = "FIN.DepositHeader"
            */

            string year = Convert.ToString(DateTime.Today.Year);
            string month = DateTime.Today.ToString("MM");
            var runningKey = "PI" + HeaderModel.Company.SAPCompanyID + year[2] + year[3] + month;
            var DepositNo = string.Empty;

            var runningNumber = DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "FIN.DepositHeader").FirstOrDefault();
            if (runningNumber == null)
            {
                var runningModel = new RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = "FIN.DepositHeader",
                    Count = 1
                };

                DB.RunningNumberCounters.AddAsync(runningModel);
                DepositNo = runningKey + runningModel.Count.ToString("0000");
            }
            else
            {
                runningNumber.Count = runningNumber.Count + 1;
                DepositNo = runningKey + runningNumber.Count.ToString("0000");
                DB.Entry(runningNumber).State = EntityState.Modified;
            }

            #endregion

            DepositHeader = new DepositHeader
            {
                //CreatedByUserID = input.Select(o => new { o.UpdatedBy }).FirstOrDefault()
                //Created = DateTime.Now,
                DepositNo = DepositNo,
                DepositDate = HeaderModel.DepositDate,
                BankAccountID = HeaderModel.BankAccount.Id,
                TotalAmount = input.Sum(o => o.PayAmount),
                TotalFee = input.Sum(o => o.Fee),
                TotalRecord = input.Count,
                Remark = HeaderModel.Remark,
                ReferentID = (ReferentDepositHeaderID ?? Guid.Empty) == Guid.Empty ? null : ReferentDepositHeaderID
            };

            DepositDetail = new List<DepositDetail>();

            foreach (var item in input)
            {
                var DetailModel = new DepositDetail();
                DetailModel.DepositHeaderID = DepositHeader.ID;
                //Created
                //CreatedByUserID
                DetailModel.PaymentMethodID = item.PaymentMethod.Id;
                DetailModel.PayAmount = item.PayAmount;
                DetailModel.Fee = item.Fee;

                DepositDetail.Add(DetailModel);
            }
        }

        private void DepositFilter(DepositFilter filter, ref IQueryable<DepositDetailQueryResult> depositQuery)
        {

            var paymentMethodTypeKey = depositQuery.Select(o => o.PaymentMethodType.Key).FirstOrDefault();

            if ((filter.PaymentMethodTypeID ?? Guid.Empty) != Guid.Empty)
                depositQuery = depositQuery.Where(x => x.PaymentMethodType.ID == filter.PaymentMethodTypeID);

            if (filter.DepositDateFrom != null)
                depositQuery = depositQuery.Where(o => o.DepositHeader.DepositDate >= filter.DepositDateFrom);
            if (filter.DepositDateTo != null)
                depositQuery = depositQuery.Where(o => o.DepositHeader.DepositDate <= filter.DepositDateTo);

            if ((filter.CompanyID ?? Guid.Empty) != Guid.Empty)
                depositQuery = depositQuery.Where(o => o.Payment.Booking.Project.Company.ID == filter.CompanyID);

            if ((filter.ProjectID ?? Guid.Empty) != Guid.Empty)
                depositQuery = depositQuery.Where(o => o.Payment.Booking.Project.ID == filter.ProjectID);

            if ((filter.UnitNo ?? "") != "")
                depositQuery = depositQuery.Where(o => o.Payment.Booking.Unit.UnitNo.Contains(filter.UnitNo));

            if (filter.PaymentDateFrom != null)
                depositQuery = depositQuery.Where(o => o.Payment.Created >= filter.PaymentDateFrom);
            if (filter.PaymentDateTo != null)
                depositQuery = depositQuery.Where(o => o.Payment.Created <= filter.PaymentDateTo);

            if ((filter.BankAccountID ?? Guid.Empty) != Guid.Empty)
            {
                depositQuery = depositQuery.Where(o => o.BankAccount.ID == filter.BankAccountID || o.DepositHeader.DepositNo == null || o.DepositHeader.IsDeleted == true);

                if (paymentMethodTypeKey == "6")    // BankTransfer
                    depositQuery = depositQuery.Where(o => o.PaymentBankTransfer.ID == filter.BankAccountID);

                if (paymentMethodTypeKey == "14")    // QRCode	
                    depositQuery = depositQuery.Where(o => o.PaymentQRCode.ID == filter.BankAccountID);
            }

            if (filter.ReceiveDateFrom != null)
                depositQuery = depositQuery.Where(o => o.ReceiptTempHeader.ReceiveDate >= filter.ReceiveDateFrom);
            if (filter.ReceiveDateTo != null)
                depositQuery = depositQuery.Where(o => o.ReceiptTempHeader.ReceiveDate <= filter.ReceiveDateTo);

            if (filter.ReceiptTempNo != null)
                depositQuery = depositQuery.Where(o => o.ReceiptTempHeader.ReceiptTempNo == filter.ReceiptTempNo);

            if (filter.MethodTypeID != null)
                depositQuery = depositQuery.Where(o => o.PaymentMethod.PaymentMethodTypeMasterCenterID == filter.MethodTypeID);

            if (filter.PayAmountFrom != null)
                depositQuery = depositQuery.Where(o => o.Payment.TotalAmount >= filter.PayAmountFrom);
            if (filter.PayAmountTo != null)
                depositQuery = depositQuery.Where(o => o.Payment.TotalAmount <= filter.PayAmountFrom);

            /*
                เงินสด           1
                แคชเชียร์เช็ค      2
                บัตรเครดิต        3
                เช็คส่วนตัว        4
                เช็ค(ล่วงหน้า)     5
                โอนผ่านธนาคาร    6
                เงินโอนต่างประเทศ	12
                บัตรเดบิต         13
                QR Code         14
            */

            if (paymentMethodTypeKey == "3")    // Credit
            {
                if (filter.FeeAmountFrom != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCreditCard.Fee >= filter.FeeAmountFrom);
                if (filter.FeeAmountTo != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCreditCard.Fee <= filter.FeeAmountTo);

                if (filter.VATAmountFrom != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCreditCard.FeeIncludingVat >= filter.VATAmountFrom);
                if (filter.VATAmountTo != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCreditCard.FeeIncludingVat <= filter.VATAmountTo);
            }

            if (paymentMethodTypeKey == "13")   // Debit
            {
                if (filter.FeeAmountFrom != null)
                    depositQuery = depositQuery.Where(o => o.PaymentDebitCard.Fee >= filter.FeeAmountFrom);
                if (filter.FeeAmountTo != null)
                    depositQuery = depositQuery.Where(o => o.PaymentDebitCard.Fee <= filter.FeeAmountTo);
            }

            if (paymentMethodTypeKey == "2")   // แคชเชียร์เช็ค
            {
                if (filter.FeeAmountFrom != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCashierCheque.Fee >= filter.FeeAmountFrom);
                if (filter.FeeAmountTo != null)
                    depositQuery = depositQuery.Where(o => o.PaymentCashierCheque.Fee <= filter.FeeAmountTo);
            }

            if (paymentMethodTypeKey == "4")   // เช็คส่วนตัว
            {
                if (filter.FeeAmountFrom != null)
                    depositQuery = depositQuery.Where(o => o.PaymentPersonalCheque.Fee >= filter.FeeAmountFrom);
                if (filter.FeeAmountTo != null)
                    depositQuery = depositQuery.Where(o => o.PaymentPersonalCheque.Fee <= filter.FeeAmountTo);
            }

            if (filter.IsDeposit != null)
            {
                if (filter.IsDeposit == true)
                    depositQuery = depositQuery.Where(o => (o.DepositHeader.DepositNo ?? "") != "");
                if (filter.IsDeposit == false)
                    depositQuery = depositQuery.Where(o => (o.DepositHeader.DepositNo ?? "") == "" || o.DepositHeader.IsDeleted == true);
            }

            //if (filter.IsPostGL != null)
            //{
            //    depositQuery = depositQuery.Where(o => o.ReceiptTempHeader.ReceiveDate <= filter.ReceiveDateTo);
            //}

            //if (filter.BatchID != null)
            //{
            //    depositQuery = depositQuery.Where(o => o.ReceiptTempHeader.ReceiveDate <= filter.ReceiveDateTo);
            //}

        }

        private IQueryable<DepositDetailQueryResult> GetDepositDetailQueryResult()
        {
            var depositQuery = from o in DB.PaymentMethods
                       .Include(o => o.PaymentMethodType)
                       .Include(o => o.Payment)
                            .ThenInclude(o => o.Booking)
                            .ThenInclude(o => o.Project)
                            .ThenInclude(o => o.Company)
                       .Include(o => o.Payment)
                            .ThenInclude(o => o.Booking)
                            .ThenInclude(o => o.Unit)

                               join pcc in DB.PaymentCreditCards on o.ID equals pcc.PaymentMethodID into pccg
                               from pcco in pccg.DefaultIfEmpty()

                               join pch in DB.PaymentCashierCheques on o.ID equals pch.PaymentMethodID into pchg
                               from pcho in pchg.DefaultIfEmpty()

                               join ppc in DB.PaymentPersonalCheques on o.ID equals ppc.PaymentMethodID into ppcg
                               from ppco in ppcg.DefaultIfEmpty()

                               join pcd in DB.PaymentDebitCards on o.ID equals pcd.PaymentMethodID into pcdg
                               from pcdo in pcdg.DefaultIfEmpty()

                               join pbt in DB.PaymentBankTransfers on o.ID equals pbt.PaymentMethodID into pbtg
                               from pbto in pbtg.DefaultIfEmpty()

                               join pqc in DB.PaymentQRCodes on o.ID equals pqc.PaymentMethodID into pqcg
                               from pqco in pqcg.DefaultIfEmpty()

                               join rth in DB.ReceiptTempHeaders on o.PaymentID equals rth.PaymentID into rthg
                               from rtho in rthg.DefaultIfEmpty()

                               join dpd in DB.DepositDetails on o.ID equals dpd.PaymentMethodID into dpdg
                               from dpdo in dpdg.DefaultIfEmpty()

                               join dph in DB.DepositHeaders on dpdo.DepositHeaderID equals dph.ID into dphg
                               from dpho in dphg.DefaultIfEmpty()

                               join ba in DB.BankAccounts on dpho.BankAccountID equals ba.ID into bag
                               from bao in bag.DefaultIfEmpty()

                               select new DepositDetailQueryResult
                               {
                                   PaymentMethod = o,
                                   PaymentMethodType = o.PaymentMethodType,

                                   Payment = o.Payment,
                                   Booking = o.Payment.Booking,
                                   Project = o.Payment.Booking.Project,
                                   Company = o.Payment.Booking.Project.Company,
                                   Unit = o.Payment.Booking.Unit,

                                   PaymentCreditCard = pcco ?? new PaymentCreditCard(),
                                   PaymentCashierCheque = pcho ?? new PaymentCashierCheque(),
                                   PaymentPersonalCheque = ppco ?? new PaymentPersonalCheque(),
                                   PaymentDebitCard = pcdo ?? new PaymentDebitCard(),
                                   PaymentBankTransfer = pbto ?? new PaymentBankTransfer(),
                                   PaymentQRCode = pqco ?? new PaymentQRCode(),
                                   ReceiptTempHeader = rtho ?? new ReceiptTempHeader(),

                                   DepositHeader = dpho ?? new DepositHeader(),
                                   DepositDetail = dpdo ?? new DepositDetail(),

                                   BankAccount = bao ?? new BankAccount()
                               };

            return depositQuery;
        }

    }
}
