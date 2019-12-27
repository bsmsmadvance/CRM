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
    public class CalculatePerMonthHighRiseTransferService : ICalculatePerMonthHighRiseTransferService
    {
        private readonly DatabaseContext DB;

        public CalculatePerMonthHighRiseTransferService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<CalculatePerMonthHighRiseTransferPaging> GetCalculatePerMonthHighRiseTransferListAsync(CalculatePerMonthHighRiseTransferFilter filter, PageParam pageParam, CalculatePerMonthHighRiseTransferSortByParam sortByParam)
        {
            IQueryable<CalculatePerMonthHighRiseTransferQueryResult> query = DB.CalculatePerMonthHighRiseTransfers
                                                 .Select(o => new CalculatePerMonthHighRiseTransferQueryResult()
                                                 {
                                                     CalculatePerMonthHighRiseTransfer = o,
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
                query = query.Where(x => ((x.CalculatePerMonthHighRiseTransfer.PeriodMonth >= filter.PeriodMonthForm.Value.Month && x.CalculatePerMonthHighRiseTransfer.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthHighRiseTransfer.PeriodYear > filter.PeriodMonthForm.Value.Year));
            }
            if (filter.PeriodMonthTo.HasValue)
            {
                query = query.Where(x => ((x.CalculatePerMonthHighRiseTransfer.PeriodMonth <= filter.PeriodMonthTo.Value.Month && x.CalculatePerMonthHighRiseTransfer.PeriodYear == filter.PeriodMonthForm.Value.Year)) || (x.CalculatePerMonthHighRiseTransfer.PeriodYear < filter.PeriodMonthForm.Value.Year));
            }
            if (!string.IsNullOrEmpty(filter.CalculateUserName))
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseTransfer.CreatedBy.DisplayName.Contains(filter.CalculateUserName));
            }
            if (filter.CalculateDateForm.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseTransfer.Created >= filter.CalculateDateForm);
            }
            if (filter.CalculateDateTo.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseTransfer.Created <= filter.CalculateDateTo);
            }
            if (filter.IsApprove.HasValue)
            {
                query = query.Where(x => x.CalculatePerMonthHighRiseTransfer.IsApprove == filter.IsApprove);
            }
            #endregion

            CalculatePerMonthHighRiseTransferDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CalculatePerMonthHighRiseTransferQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CalculatePerMonthHighRiseTransferDTO.CreateFromQueryResult(o)).ToList();

            return new CalculatePerMonthHighRiseTransferPaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthHighRiseTransfers = results
            };
        }

        public bool CalculatePerMonthHighRiseTransfer(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID)
        {
            string str = "";
            str += "exec [CMS].[sp_CMS_CommissionHightRiseTransfer_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculatePerMonthHighRiseTransfer_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
            str += "exec [CMS].[sp_CMS_CalculateHighRiseTransfer_CAL] @ProjectID,@CalculateMonth,@CalculateUserID;";
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

        public async Task<CalculatePerMonthHighRiseTransferDTO> ApproveCalculatePerMonthHighRiseTransferAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthHighRiseTransfers.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = true;
            model.ApproveDate = DateTime.Now;
            model.ApproveUserBy = ApproveUserID;
            model.CancelApproveDate = null;
            model.CancelApproveUserBy = null;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthHighRiseTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CalculatePerMonthHighRiseTransferDTO> CancelApproveCalculatePerMonthHighRiseTransferAsync(Guid id, Guid? ApproveUserID)
        {
            var model = await DB.CalculatePerMonthHighRiseTransfers.Where(o => o.ID == id).FirstAsync();

            model.IsApprove = false;
            model.ApproveDate = null;
            model.ApproveUserBy = null;
            model.CancelApproveDate = DateTime.Now;
            model.CancelApproveUserBy = ApproveUserID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = CalculatePerMonthHighRiseTransferDTO.CreateFromModel(model);
            return result;
        }
    }
}
