using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Report.Integration;
using Report.Integration.Reports.FI;
using Report.Integration.Reports.LC;

namespace MasterData.Services
{
    public class EDCService : IEDCService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;

        public EDCService(DatabaseContext db, IConfiguration configuration)
        {
            this.DB = db;
            this.Configuration = configuration;
        }

        /// <summary>
        /// ดึง dropdown เครื่องรูดบัตร
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public async Task<List<EDCDropdownDTO>> GetEDCDropdownListUrlAsync(Guid? projectID, string bankName)
        {
            var query = DB.EDCs.Include(o => o.Bank).AsQueryable();
            if (projectID != null)
            {
                query = query.Where(o => o.ProjectID == projectID);
            }
            if (!string.IsNullOrEmpty(bankName))
            {
                query = query.Where(o => o.Bank.NameTH.Contains(bankName));
            }

            var results = await query.OrderBy(o => o.Bank.NameTH).Take(100).Select(o => EDCDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        /// <summary>
        /// ดึงเครื่องรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367402/preview
        /// </summary>
        /// <returns>The EDCL ist async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCPaging> GetEDCListAsync(EDCFilter filter, PageParam pageParam, EDCSortByParam sortByParam)
        {
            IQueryable<EDCQueryResult> query = DB.EDCs
                .Include(o => o.Project.ProjectStatus)
                .Include(o => o.BankAccount)
                .ThenInclude(o => o.Bank)
                .Include(o => o.BankAccount)
                .ThenInclude(o => o.BankAccountType)
                .Select(o =>
                new EDCQueryResult
                {
                    EDC = o,
                    Bank = o.Bank,
                    BankAccount = o.BankAccount,
                    CardMachineStatus = o.CardMachineStatus,
                    CardMachineType = o.CardMachineType,
                    Company = o.Company,
                    Project = o.Project,
                    UpdatedBy = o.UpdatedBy
                });

            #region Filter

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.EDC.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.CardMachineTypeKey))
            {
                var cardMachineTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CardMachineTypeKey
                                                                       && x.MasterCenterGroupKey == "CardMachineType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.CardMachineType.ID == cardMachineTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.ProjectStatusKey))
            {
                var projectStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ProjectStatusKey
                                                                       && x.MasterCenterGroupKey == "ProjectStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Project.ProjectStatusMasterCenterID == projectStatusMasterCenterID);
            }
            if (filter.BankAccountID != null && filter.BankAccountID != Guid.Empty)
            {
                query = query.Where(o => o.BankAccount.ID == filter.BankAccountID);
            }
            if (filter.CompanyID != null && filter.CompanyID != Guid.Empty)
            {
                query = query.Where(o => o.Company.ID == filter.CompanyID);
            }
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.ReceiveBy))
            {
                query = query.Where(o => o.EDC.ReceiveBy.Contains(filter.ReceiveBy));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.EDC.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.EDC.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.EDC.Updated >= filter.UpdatedFrom
                                    && x.EDC.Updated <= filter.UpdatedTo);
            }

            if (filter.ReceiveDateFrom != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate >= filter.ReceiveDateFrom);
            }
            if (filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate <= filter.ReceiveDateTo);
            }
            if (filter.ReceiveDateFrom != null && filter.ReceiveDateTo != null)
            {
                query = query.Where(x => x.EDC.ReceiveDate >= filter.ReceiveDateFrom
                                    && x.EDC.ReceiveDate <= filter.ReceiveDateTo);
            }
            if (!string.IsNullOrEmpty(filter.CardMachineStatusKey))
            {
                var cardMachineStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CardMachineStatusKey
                                                                       && x.MasterCenterGroupKey == "CardMachineStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.CardMachineStatus.ID == cardMachineStatusMasterCenterID);
            }


            #endregion

            EDCDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<EDCQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => EDCDTO.CreateFromQueryResult(o)).ToList();

            return new EDCPaging()
            {
                PageOutput = pageOutput,
                EDCs = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDOD etail async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<EDCDTO> GetEDCDetailAsync(Guid id)
        {
            var model = await DB.EDCs.Where(o => o.ID == id)
                                     .Include(o => o.Bank)
                                     .Include(o => o.Company)
                                     .Include(o => o.BankAccount)
                                     .Include(o => o.CardMachineType)
                                     .Include(o => o.CardMachineStatus)
                                     .Include(o => o.Project)
                                     .Include(o => o.UpdatedBy)
                                     .FirstAsync();
            var result = EDCDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="input">Input.</param>
        public async Task<EDCDTO> CreateEDCAsync(EDCDTO input)
        {
            await input.ValidateAsync(DB);
            EDC model = new EDC();
            input.ToModel(ref model);
            await DB.EDCs.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetEDCDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// https://projects.invisionapp.com/d/main#/console/17482171/362367403/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<EDCDTO> UpdateEDCAsync(Guid id, EDCDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.EDCs.Where(x => x.ID == id)
                                             .FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetEDCDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367402/preview
        /// </summary>
        /// <returns>The EDCA sync.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteEDCAsync(Guid id)
        {
            var model = await DB.EDCs.Where(x => x.ID == id)
                                     .FirstAsync();
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// ดึงธนาคารจากข้อมูลเครื่องรูดบัตร (Group By ธนาคาร)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367404/preview
        /// </summary>
        /// <returns>The EDCB ank list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCBankPaging> GetEDCBankListAsync(EDCBankFilter filter, PageParam pageParam, EDCBankSortByParam sortByParam)
        {
            IQueryable<EDCBankQueryResult> query = DB.EDCs.Include(o => o.UpdatedBy).GroupBy(o => o.Bank).Select(o =>
                                                                     new EDCBankQueryResult
                                                                     {
                                                                         Bank = o.Key,
                                                                         EDC = o.Select(p => p).OrderByDescending(p => p.Updated).FirstOrDefault(),
                                                                     });
            #region Filter

            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.BankName))
            {
                query = query.Where(o => o.Bank.NameTH.Contains(filter.BankName));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.EDC.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.EDC.Updated <= filter.UpdatedTo);
            }


            #endregion

            var queryResults = await query.ToListAsync();

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                queryResults = queryResults.Where(x => x.EDC?.UpdatedBy?.DisplayName?.ToLower().Contains(filter.UpdatedBy?.ToLower()) ?? false).ToList();
            }


            EDCBankDTO.SortBy(sortByParam, ref queryResults);

            var pageOutput = PagingHelper.PagingList<EDCBankQueryResult>(pageParam, ref queryResults);

            var results = queryResults.Select(o => EDCBankDTO.CreateFromQueryResult(o)).ToList();

            return new EDCBankPaging()
            {
                PageOutput = pageOutput,
                EDCBanks = results
            };

        }

        /// <summary>
        /// ดึงข้อมูลค่าธรรมเนียมเครื่องรูดบัตร (จากธนาคารที่เลือก)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee list async.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<EDCFeePaging> GetEDCFeeListAsync(EDCFeeFilter filter, PageParam pageParam, EDCFeeSortByParam sortByParam)
        {
            IQueryable<EDCFeeQueryResult> query = DB.EDCFees.Include(o => o.UpdatedBy).Select(o =>
                                                                     new EDCFeeQueryResult
                                                                     {
                                                                         EDCFee = o,
                                                                         Bank = o.Bank,
                                                                         CreditCardPaymentType = o.CreditCardPaymentType,
                                                                         CreditCardType = o.CreditCardType,
                                                                         PaymentCardType = o.PaymentCardType
                                                                     });
            #region Filter

            if (filter.BankID != null && filter.BankID != Guid.Empty)
            {
                query = query.Where(o => o.Bank.ID == filter.BankID);
            }
            if (!string.IsNullOrEmpty(filter.PaymentCardTypeKey))
            {
                var paymentCardTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PaymentCardTypeKey
                                                                       && x.MasterCenterGroupKey == "PaymentCardType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.PaymentCardType.ID == paymentCardTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.CreditCardTypeKey))
            {
                var creditCardTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CreditCardTypeKey
                                                                       && x.MasterCenterGroupKey == "CreditCardType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.CreditCardType.ID == creditCardTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.CreditCardPaymentTypeKey))
            {
                var creditCardPaymentTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.CreditCardPaymentTypeKey
                                                                       && x.MasterCenterGroupKey == "CreditCardPaymentType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.CreditCardPaymentType.ID == creditCardPaymentTypeMasterCenterID);
            }

            if (filter.FeeFrom != null)
            {
                query = query.Where(o => o.EDCFee.Fee >= filter.FeeFrom);
            }
            if (filter.FeeTo != null)
            {
                query = query.Where(o => o.EDCFee.Fee <= filter.FeeTo);
            }
            if (filter.IsEDCBankCreditCard != null)
            {
                query = query.Where(o => o.EDCFee.IsEDCBankCreditCard == filter.IsEDCBankCreditCard);
            }
            #endregion

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => EDCFeeDTO.CreateFromQueryResult(o)).ToList();

            if (!string.IsNullOrEmpty(filter.CreditCardPromotionName))
            {
                results = results.Where(o => o.CreditCardPromotionName.Contains(filter.CreditCardPromotionName)).ToList();
            }

            EDCFeeDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<EDCFeeDTO>(pageParam, ref results);

            return new EDCFeePaging()
            {
                PageOutput = pageOutput,
                EDCFees = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="input">Input.</param>
        public async Task<EDCFeeDTO> CreateEDCFeeAsync(EDCFeeDTO input)
        {
            await input.ValidateAsync(DB);
            EDCFee model = new EDCFee();
            input.ToModel(ref model);
            await DB.EDCFees.AddAsync(model);
            await DB.SaveChangesAsync();

            var dataresult = await DB.EDCFees.Where(o => o.ID == model.ID)
                                             .Include(o => o.PaymentCardType)
                                             .Include(o => o.Bank)
                                             .Include(o => o.CreditCardType)
                                             .Include(o => o.CreditCardPaymentType)
                                             .FirstOrDefaultAsync();
            var result = EDCFeeDTO.CreateFromModel(dataresult);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<EDCFeeDTO> UpdateEDCFeeAsync(Guid id, EDCFeeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.EDCFees.Where(x => x.ID == id)
                                             .FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataresult = await DB.EDCFees.Where(o => o.ID == model.ID)
                                             .Include(o => o.PaymentCardType)
                                             .Include(o => o.Bank)
                                             .Include(o => o.CreditCardType)
                                             .Include(o => o.CreditCardPaymentType)
                                             .FirstOrDefaultAsync();

            var result = EDCFeeDTO.CreateFromModel(dataresult);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482171/362367405/preview
        /// </summary>
        /// <returns>The EDCF ee async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteEDCFeeAsync(Guid id)
        {
            var model = await DB.EDCFees.Where(o => o.ID == id)
                                        .FirstAsync();
            model.IsDeleted = true;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<decimal> GetFeeAsync(Guid edcID, Guid creditCardBankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID, decimal payAmount)
        {
            var edc = await DB.EDCs.FirstAsync(o => o.ID == edcID);
            bool isSameBank = edc.BankID == creditCardBankID;
            var fee = await DB.EDCFees.Where(o => o.BankID == edc.BankID
                                            && o.CreditCardTypeMasterCenterID == creditCardTypeMasterCenterID
                                            && o.CreditCardPaymentTypeMasterCenterID == creditCardPaymentTypeMasterCenterID
                                            && o.PaymentCardTypeMasterCenterID == paymentCardTypeMasterCenterID
                                            && o.IsEDCBankCreditCard == isSameBank
                                            )
                                            .Select(o => o.Fee)
                                            .FirstOrDefaultAsync();
            decimal feeAmount = (Convert.ToDecimal(fee) * payAmount) / 100;
            return feeAmount;
        }

        public async Task<double> GetFeePercentAsync(Guid edcID, Guid creditCardBankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID)
        {
            var edc = await DB.EDCs.FirstAsync(o => o.ID == edcID);
            bool isSameBank = edc.BankID == creditCardBankID;
            var fee = await DB.EDCFees.Where(o => o.BankID == edc.BankID
                                            && o.CreditCardTypeMasterCenterID == creditCardTypeMasterCenterID
                                            && o.CreditCardPaymentTypeMasterCenterID == creditCardPaymentTypeMasterCenterID
                                            && o.PaymentCardTypeMasterCenterID == paymentCardTypeMasterCenterID
                                            && o.IsEDCBankCreditCard == isSameBank
                                            )
                                            .Select(o => o.Fee)
                                            .FirstOrDefaultAsync();
            return fee;
        }

        public async Task<ReportResult> ExportEDCListUrlAsync(EDCFilter filter, ShowAs showAs)
        {
            ReportFactory reportFactory = new ReportFactory(Configuration, ReportFolder.FI, "RP_FI_035");
            reportFactory.AddParameter("BankAccountID", filter.BankAccountID);
            reportFactory.AddParameter("CardMachineStatusKey", filter.CardMachineStatusKey);
            reportFactory.AddParameter("CardMachineTypeKey", filter.CardMachineTypeKey);
            reportFactory.AddParameter("Code", filter.Code);
            reportFactory.AddParameter("CompanyID", filter.CompanyID);
            reportFactory.AddParameter("ProjectID", filter.ProjectID);
            reportFactory.AddParameter("ProjectStatusKey", filter.ProjectStatusKey);
            reportFactory.AddParameter("ReceiveBy", filter.ReceiveBy);
            reportFactory.AddParameter("ReceiveBy", filter.ReceiveBy);
            reportFactory.AddParameter("ReceiveBy", filter.ReceiveBy);
            reportFactory.AddParameter("ReceiveDateFrom", filter.ReceiveDateFrom?.Ticks);
            reportFactory.AddParameter("ReceiveDateTo", filter.ReceiveDateTo?.Ticks);
            return reportFactory.CreateUrl();

            //ReportFactory reportFactory = new ReportFactory(Configuration["Report:Url"], Configuration["Report:SecretKey"]);
            //var reportUrl = reportFactory.CreateUrl<RP_FI_035>(new RP_FI_035()
            //{
            //    BankAccountID = filter.BankAccountID,
            //    CardMachineStatusKey = filter.CardMachineStatusKey,
            //    CardMachineTypeKey = filter.CardMachineTypeKey,
            //    Code = filter.Code,
            //    CompanyID = filter.CompanyID,
            //    ProjectID = filter.ProjectID,
            //    ProjectStatusKey = filter.ProjectStatusKey,
            //    ReceiveBy = filter.ReceiveBy,
            //    ReceiveDateFrom = filter.ReceiveDateFrom?.Ticks,
            //    ReceiveDateTo = filter.ReceiveDateTo?.Ticks
            //}, downloadAs);

            //return new StringResult()
            //{
            //    Result = reportUrl
            //};


        }
    }
}
