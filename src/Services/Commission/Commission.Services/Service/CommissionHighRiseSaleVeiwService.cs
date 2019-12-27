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
    public class CommissionHighRiseSaleVeiwService : ICommissionHighRiseSaleVeiwService
    {
        private readonly DatabaseContext DB;

        public CommissionHighRiseSaleVeiwService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CommissionHighRiseSaleVeiwPaging> GetCommissionHighRiseSaleVeiwListAsync(CommissionHighRiseSaleVeiwFilter filter, PageParam pageParam, CommissionHighRiseSaleVeiwSortByParam sortByParam)
        {
            #region Header
            IQueryable<CalculatePerMonthHighRiseSaleQueryResult> queryHeader = DB.CalculatePerMonthHighRiseSales
                                              .Select(o => new CalculatePerMonthHighRiseSaleQueryResult()
                                              {
                                                  CalculatePerMonthHighRiseSale = o,
                                                  Project = o.Project,
                                                  CalculateUserName = o.CreatedBy
                                              });

            #region Filter
            if (filter.ProjectID != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthHighRiseSale.ProjectID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthHighRiseSale.PeriodYear == filter.PeriodYear && x.CalculatePerMonthHighRiseSale.PeriodMonth == filter.PeriodMonth);
            }
            #endregion

            var resultHeader = await queryHeader.Select(o => CalculatePerMonthHighRiseSaleDTO.CreateFromQueryResult(o)).FirstOrDefaultAsync();
            #endregion

            //ทำสัญญากับโอนในเดือนเดียวกัน
            IQueryable<CommissionHighRiseSaleVeiwQueryResult> query1 = from chs in DB.CalculateHighRiseSales
                                                                       join cht in DB.CalculateHighRiseTransfers on chs.AgreementID equals cht.Transfer.AgreementID into g1
                                                                       from c in g1.Where(x => x.TransferID != null && !x.IsDeleted).DefaultIfEmpty()
                                                                       join a in DB.CommissionContracts on chs.AgreementID equals a.AgreementID into g2
                                                                       from ag in g2.DefaultIfEmpty()
                                                                           //join t in DB.CommissionTransfers on chs.AgreementID equals t.Transfer.AgreementID into g3
                                                                           //from tf in g3.DefaultIfEmpty()
                                                                       select new CommissionHighRiseSaleVeiwQueryResult()
                                                                       {
                                                                           CalculateHighRiseSale = chs,
                                                                           Contract = ag,
                                                                           //Transfer = tf,
                                                                           Project = chs.Agreement.Project,
                                                                           Unit = chs.Agreement.Unit,
                                                                           SaleUserName = chs.SaleUser,
                                                                           ProjectSaleUserName = chs.ProjectSaleUser
                                                                       };

            //ทำสัญญาแต่ยังไม่โอน
            IQueryable<CommissionHighRiseSaleVeiwQueryResult> query2 = from chs in DB.CalculateHighRiseSales
                                                                       join cht in DB.CalculateHighRiseTransfers on chs.AgreementID equals cht.Transfer.AgreementID into g1
                                                                       from c in g1.Where(x => x.TransferID == null).DefaultIfEmpty()
                                                                       join a in DB.CommissionContracts on chs.AgreementID equals a.AgreementID into g2
                                                                       from ag in g2.DefaultIfEmpty()
                                                                           //join t in DB.CommissionTransfers on chs.AgreementID equals t.Transfer.AgreementID into g3
                                                                           //from tf in g3.DefaultIfEmpty()
                                                                       select new CommissionHighRiseSaleVeiwQueryResult()
                                                                       {
                                                                           CalculateHighRiseSale = chs,
                                                                           Contract = ag,
                                                                           //Transfer = tf,
                                                                           Project = chs.Agreement.Project,
                                                                           Unit = chs.Agreement.Unit,
                                                                           SaleUserName = chs.SaleUser,
                                                                           ProjectSaleUserName = chs.ProjectSaleUser
                                                                       };


            //ทำสัญญาและโอนแล้ว แต่ sign contract หลังโอน
            IQueryable<CommissionHighRiseSaleVeiwQueryResult> query3 = from chs in DB.CalculateHighRiseSales
                                                                       join cht in DB.CalculateHighRiseTransfers on chs.AgreementID equals cht.Transfer.AgreementID into g1
                                                                       from c in g1.Where(x => x.TransferID != null && !x.IsDeleted).DefaultIfEmpty()
                                                                       join a in DB.CommissionContracts on chs.AgreementID equals a.AgreementID into g2
                                                                       from ag in g2.DefaultIfEmpty()
                                                                           //join t in DB.CommissionTransfers on chs.TransferID equals t.TransferID into g3
                                                                           //from tf in g3.DefaultIfEmpty()
                                                                       select new CommissionHighRiseSaleVeiwQueryResult()
                                                                       {
                                                                           CalculateHighRiseSale = chs,
                                                                           Contract = ag,
                                                                           //Transfer = tf,
                                                                           Project = chs.Agreement.Project,
                                                                           Unit = chs.Agreement.Unit,
                                                                           SaleUserName = chs.SaleUser,
                                                                           ProjectSaleUserName = chs.ProjectSaleUser
                                                                       };

            var query = query1.Union(query2).Union(query3);

            #region Filter
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                query = query.Where(x => x.CalculateHighRiseSale.PeriodYear == filter.PeriodYear && x.CalculateHighRiseSale.PeriodMonth == filter.PeriodMonth);
            }
            if (filter.UnitID != null)
            {
                query = query.Where(x => x.Unit.ID == filter.UnitID);
            }
            if (filter.SaleUserID != null)
            {
                query = query.Where(x => x.CalculateHighRiseSale.SaleUserID == filter.SaleUserID);
            }
            if (filter.ProjectSaleUserID != null)
            {
                query = query.Where(x => x.CalculateHighRiseSale.ProjectSaleUserID == filter.ProjectSaleUserID);
            }
            if (filter.CommissionPercentRate != null)
            {
                query = query.Where(x => x.CalculateHighRiseSale.CommissionPercentRate == filter.CommissionPercentRate);
            }
            if (filter.CommissionPercentType != null)
            {
                query = query.Where(x => x.CalculateHighRiseSale.CommissionPercentType == filter.CommissionPercentType);
            }
            if (filter.TotalContractNetAmountForm.HasValue)
            {
                query = query.Where(x => (x.Contract.SellingPrice - x.Contract.TransferDiscount ?? 0 - x.Contract.FreeDownAmount ?? 0) >= filter.TotalContractNetAmountForm);
            }
            if (filter.TotalContractNetAmountTo.HasValue)
            {
                query = query.Where(x => (x.Contract.SellingPrice - x.Contract.TransferDiscount ?? 0 - x.Contract.FreeDownAmount ?? 0) <= filter.TotalContractNetAmountTo);
            }
            if (filter.SignAgreementDateForm.HasValue)
            {
                query = query.Where(x => x.Contract.ApproveDate >= filter.SignAgreementDateForm);
            }
            if (filter.SignAgreementDateTo.HasValue)
            {
                query = query.Where(x => x.Contract.ApproveDate <= filter.SignAgreementDateTo);
            }
            if (filter.SaleUserSalePaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.SaleUserSalePaid >= filter.SaleUserSalePaidForm);
            }
            if (filter.SaleUserSalePaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.SaleUserSalePaid <= filter.SaleUserSalePaidTo);
            }
            if (filter.ProjectSaleSalePaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.ProjectSaleSalePaid >= filter.ProjectSaleSalePaidForm);
            }
            if (filter.ProjectSaleSalePaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.ProjectSaleSalePaid <= filter.ProjectSaleSalePaidTo);
            }
            if (filter.TotalSalePaidForm.HasValue)
            {
                query = query.Where(x => (x.CalculateHighRiseSale.SaleUserSalePaid + x.CalculateHighRiseSale.ProjectSaleSalePaid) >= filter.TotalSalePaidForm);
            }
            if (filter.TotalSalePaidTo.HasValue)
            {
                query = query.Where(x => (x.CalculateHighRiseSale.SaleUserSalePaid + x.CalculateHighRiseSale.ProjectSaleSalePaid) <= filter.TotalSalePaidTo);
            }
            if (filter.CommissionForThisMonthForm.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.TotalCommissionPaid >= filter.CommissionForThisMonthForm);
            }
            if (filter.CommissionForThisMonthTo.HasValue)
            {
                query = query.Where(x => x.CalculateHighRiseSale.TotalCommissionPaid <= filter.CommissionForThisMonthTo);
            }
            #endregion

            CommissionHighRiseSaleVeiwDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CommissionHighRiseSaleVeiwQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CommissionHighRiseSaleVeiwDTO.CreateFromQueryResult(o)).ToList();

            resultHeader.CommissionHighRiseSaleVeiws = results;

            return new CommissionHighRiseSaleVeiwPaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthHighRiseSale = resultHeader
            };
        }
    }
}