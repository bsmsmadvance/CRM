using Database.Models;
using Database.Models.CMS;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public class CommissionHighRiseTransferVeiwService : ICommissionHighRiseTransferVeiwService
    {
        private readonly DatabaseContext DB;
        
        public CommissionHighRiseTransferVeiwService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CommissionHighRiseTransferVeiwPaging> GetCommissionHighRiseTransferVeiwListAsync(CommissionHighRiseTransferVeiwFilter filter, PageParam pageParam, CommissionHighRiseTransferVeiwSortByParam sortByParam)
        {
            #region Header
            IQueryable<CalculatePerMonthHighRiseTransferQueryResult> queryHeader = DB.CalculatePerMonthHighRiseTransfers
                                              .Select(o => new CalculatePerMonthHighRiseTransferQueryResult()
                                              {
                                                  CalculatePerMonthHighRiseTransfer = o,
                                                  Project = o.Project,
                                                  CalculateUserName = o.CreatedBy
                                              });

            #region Filter
            if (filter.ProjectID != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthHighRiseTransfer.ProjectID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthHighRiseTransfer.PeriodYear == filter.PeriodYear && x.CalculatePerMonthHighRiseTransfer.PeriodMonth == filter.PeriodMonth);
            }
            #endregion

            var resultHeader = await queryHeader.Select(o => CalculatePerMonthHighRiseTransferDTO.CreateFromQueryResult(o)).FirstOrDefaultAsync();
            #endregion

            //โอนในเดือนนี้
            IQueryable<CommissionHighRiseTransferVeiwQueryResult> query = from cht in DB.CalculateHighRiseTransfers
                                                                          join a in DB.CommissionContracts on cht.Transfer.AgreementID equals a.AgreementID into g2
                                                                          from ag in g2.DefaultIfEmpty()
                                                                          join t in DB.CommissionTransfers on cht.TransferID equals t.TransferID into g3
                                                                          from tf in g3.DefaultIfEmpty()
                                                                          select new CommissionHighRiseTransferVeiwQueryResult()
                                                                          {
                                                                              CalculateHighRiseTransfer = cht,
                                                                              Contract = ag,
                                                                              Transfer = tf,
                                                                              Project = cht.Transfer.Project,
                                                                              Unit = cht.Transfer.Unit,
                                                                              LCTransferName = cht.LCTransfer
                                                                          };


            #region Filter
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.PeriodYear == filter.PeriodYear && x.CalculateHighRiseTransfer.PeriodMonth == filter.PeriodMonth);
            }
            if (filter.UnitID != null)
            {
                query = query.Where(x => x.Unit.ID == filter.UnitID);
            }
            if (filter.LCTransferID != null)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.LCTransferID == filter.LCTransferID);
            }
            if (filter.CommissionPercentRate != null)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.CommissionPercentRate == filter.CommissionPercentRate);
            }
            if (filter.CommissionPercentType != null)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.CommissionPercentType == filter.CommissionPercentType);
            }
            if (filter.NetSellPriceForm.HasValue)
            {
                query = query.Where(x => (x.Transfer.NetSellPrice ?? 0) >= filter.NetSellPriceForm);
            }
            if (filter.NetSellPriceTo.HasValue)
            {
                query = query.Where(x => (x.Transfer.NetSellPrice ?? 0) <= filter.NetSellPriceTo);
            }
            if (filter.TransferDateForm.HasValue)
            {
                query = query.Where(x => x.Transfer.TransferDate >= filter.TransferDateForm);
            }
            if (filter.TransferDateTo.HasValue)
            {
                query = query.Where(x => x.Transfer.TransferDate <= filter.TransferDateTo);
            }
            if (filter.LCTransferPaidForm.HasValue)
            {
                query = query.Where(x => (x.CalculateHighRiseTransfer.LCTransferPaid ?? 0) >= filter.LCTransferPaidForm);
            }
            if (filter.LCTransferPaidTo.HasValue)
            {
                query = query.Where(x => (x.CalculateHighRiseTransfer.LCTransferPaid ?? 0) <= filter.LCTransferPaidTo);
            }
            if (filter.CommissionForThisMonthForm.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.TotalCommissionPaid >= filter.CommissionForThisMonthForm);
            }
            if (filter.CommissionForThisMonthTo.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseTransfer.TotalCommissionPaid <= filter.CommissionForThisMonthTo);
            }
            #endregion

            CommissionHighRiseTransferVeiwDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CommissionHighRiseTransferVeiwQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CommissionHighRiseTransferVeiwDTO.CreateFromQueryResult(o)).ToList();

            resultHeader.CommissionHighRiseTransferVeiws = results;

            return new CommissionHighRiseTransferVeiwPaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthHighRiseTransfer = resultHeader
            };
        }
    }
}
