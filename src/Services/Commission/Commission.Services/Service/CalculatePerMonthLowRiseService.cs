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
    public class CalculatePerMonthLowRiseService : ICalculatePerMonthLowRiseService
    {
        private readonly DatabaseContext DB;

        public CalculatePerMonthLowRiseService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CalculatePerMonthLowRisePaging> GetCalculatePerMonthLowRiseListAsync(CalculatePerMonthLowRiseFilter filter, PageParam pageParam, CalculatePerMonthLowRiseSortByParam sortByParam)
        {
            IQueryable<CalculatePerMonthLowRiseQueryResult> query = DB.CalculatePerMonthLowRises
                                                 .Select(o => new CalculatePerMonthLowRiseQueryResult()
                                                 {
                                                     CalculatePerMonthLowRise = o,
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
                query = query.Where(x => ((x.CalculatePerMonthLowRise.PeriodMonth >= filter.PeriodMonthForm.Value.Month && x.CalculatePerMonthLowRise.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthLowRise.PeriodYear > filter.PeriodMonthForm.Value.Year));
            }
            if (filter.PeriodMonthTo.HasValue)
            {
                query = query.Where(x => ((x.CalculatePerMonthLowRise.PeriodMonth <= filter.PeriodMonthTo.Value.Month && x.CalculatePerMonthLowRise.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthLowRise.PeriodYear < filter.PeriodMonthForm.Value.Year));
            }
            if (!string.IsNullOrEmpty(filter.CalculateUserName))
            {
                query = query.Where(x => x.CalculatePerMonthLowRise.CreatedBy.DisplayName.Contains(filter.CalculateUserName));
            }
            if (filter.CalculateDateForm.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthLowRise.Created >= filter.CalculateDateForm);
            }
            if (filter.CalculateDateTo.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthLowRise.Created <= filter.CalculateDateTo);
            }
            if (filter.IsApprove.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthLowRise.IsApprove == filter.IsApprove);
            }
            #endregion

            CalculatePerMonthLowRiseDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CalculatePerMonthLowRiseQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CalculatePerMonthLowRiseDTO.CreateFromQueryResult(o)).ToList();

            return new CalculatePerMonthLowRisePaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthLowRises = results
            };
        }

        public bool CalculatePerMonthLowRise(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID)
        {
            string str = "";
            str += "exec [CMS].[sp_CMS_CommissionLowRiseContractTransfer_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculatePerMonthLowRise_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculateLowRiseSaleTransfer_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
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

        public async Task<CalculatePerMonthLowRiseDTO> ApproveCalculatePerMonthLowRiseAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthLowRises.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = true;
            model.ApproveDate = DateTime.Now;
            model.ApproveUserBy = ApproveUserID;
            model.CancelApproveDate = null;
            model.CancelApproveUserBy = null;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthLowRiseDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CalculatePerMonthLowRiseDTO> CancelApproveCalculatePerMonthLowRiseAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthLowRises.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = false;
            model.ApproveDate = null;
            model.ApproveUserBy = null;
            model.CancelApproveDate = DateTime.Now;
            model.CancelApproveUserBy = ApproveUserID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthLowRiseDTO.CreateFromModel(model);
            return result;
        }
    }
}
