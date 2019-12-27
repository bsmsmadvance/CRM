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
    public class CalculatePerMonthHighRiseSaleService : ICalculatePerMonthHighRiseSaleService
    {
        private readonly DatabaseContext DB;

        public CalculatePerMonthHighRiseSaleService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CalculatePerMonthHighRiseSalePaging> GetCalculatePerMonthHighRiseSaleListAsync(CalculatePerMonthHighRiseSaleFilter filter, PageParam pageParam, CalculatePerMonthHighRiseSaleSortByParam sortByParam)
        {
            IQueryable<CalculatePerMonthHighRiseSaleQueryResult> query = DB.CalculatePerMonthHighRiseSales
                                                 .Select(o => new CalculatePerMonthHighRiseSaleQueryResult()
                                                 {
                                                     CalculatePerMonthHighRiseSale = o,
                                                     Project = o.Project,
                                                     CalculateUserName = o.CreatedBy
                                                 });

            #region Filter
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            if (filter.PeriodMonthForm.HasValue)
            {
                query = query.Where(x => ((x.CalculatePerMonthHighRiseSale.PeriodMonth >= filter.PeriodMonthForm.Value.Month && x.CalculatePerMonthHighRiseSale.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthHighRiseSale.PeriodYear > filter.PeriodMonthForm.Value.Year));
            }
            if (filter.PeriodMonthTo.HasValue)
            {
                query = query.Where(x => ((x.CalculatePerMonthHighRiseSale.PeriodMonth <= filter.PeriodMonthTo.Value.Month && x.CalculatePerMonthHighRiseSale.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthHighRiseSale.PeriodYear < filter.PeriodMonthForm.Value.Year));
            }
            if (!string.IsNullOrEmpty(filter.CalculateUserName))
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseSale.CreatedBy.DisplayName.Contains(filter.CalculateUserName));
            }
            if (filter.CalculateDateForm.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseSale.Created >= filter.CalculateDateForm);
            }
            if (filter.CalculateDateTo.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseSale.Created <= filter.CalculateDateTo);
            }
            if (filter.IsApprove.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseSale.IsApprove == filter.IsApprove);
            }
            #endregion

            CalculatePerMonthHighRiseSaleDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CalculatePerMonthHighRiseSaleQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CalculatePerMonthHighRiseSaleDTO.CreateFromQueryResult(o)).ToList();

            return new CalculatePerMonthHighRiseSalePaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthHighRiseSales = results
            };
        }

        public bool CalculatePerMonthHighRiseSale(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID)
        {
            string str = "";
            str += "exec [CMS].[sp_CMS_CommissionHightRiseContract_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculatePerMonthHighRiseSale_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculateHighRiseSale_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculateIncreaseDeductMoney_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";

            DB.Database.ExecuteSqlCommand(str,
                    new
                    {
                        ProjectID = ProjectID,
                        CalculateMonth = CalculateMonth,
                        CalculateUserID = CalculateUserID
                    });
            DB.SaveChanges();
            return true;
        }

        public async Task<CalculatePerMonthHighRiseSaleDTO> ApproveCalculatePerMonthHighRiseSaleAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthHighRiseSales.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = true;
            model.ApproveDate = DateTime.Now;
            model.ApproveUserBy = ApproveUserID;
            model.CancelApproveDate = null;
            model.CancelApproveUserBy = null;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthHighRiseSaleDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CalculatePerMonthHighRiseSaleDTO> CancelApproveCalculatePerMonthHighRiseSaleAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthHighRiseSales.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = false;
            model.ApproveDate = null;
            model.ApproveUserBy = null;
            model.CancelApproveDate = DateTime.Now;
            model.CancelApproveUserBy = ApproveUserID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthHighRiseSaleDTO.CreateFromModel(model);
            return result;
        }
    }
}
