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
    public class DeductMoneyService : IDeductMoneyService
    {
        private readonly DatabaseContext DB;

        public DeductMoneyService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<DeductMoneyPaging> GetDeductMoneyListAsync(DeductMoneyFilter filter, PageParam pageParam, DeductMoneySortByParam sortByParam)
        {
            IQueryable<DeductMoneyQueryResult> query = DB.DeductMoneys
                                                  .Where(o => o.IsDeleted == false)
                                                  .Select(o => new DeductMoneyQueryResult()
                                                  {
                                                      DeductMoney = o,
                                                      Project = o.Project,
                                                      SaleUser = o.SaleUser
                                                  });

            #region Filter
            if (filter.ActiveDateForm.HasValue)
            {
                query = query.Where(x => x.DeductMoney.ActiveDate >= filter.ActiveDateForm);
            }
            if (filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.DeductMoney.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.ActiveDateForm.HasValue && filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.DeductMoney.ActiveDate >= filter.ActiveDateForm && x.DeductMoney.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.DeductMoney.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.SaleUserID))
            {
                query = query.Where(x => x.DeductMoney.SaleUser.EmployeeNo.Contains(filter.SaleUserID));
            }
            if (!string.IsNullOrEmpty(filter.SaleUserName))
            {
                query = query.Where(x => x.DeductMoney.SaleUser.FirstName.Contains(filter.SaleUserName) || x.DeductMoney.SaleUser.LastName.Contains(filter.SaleUserName));
            }
            if (filter.AmountForm.HasValue)
            {
                query = query.Where(x => x.DeductMoney.Amount >= filter.AmountForm);
            }
            if (filter.AmountTo.HasValue)
            {
                query = query.Where(x => x.DeductMoney.Amount <= filter.AmountTo);
            }
            if (filter.AmountForm.HasValue && filter.AmountTo.HasValue)
            {
                query = query.Where(x => x.DeductMoney.Amount >= filter.AmountForm && x.DeductMoney.Amount <= filter.AmountTo);
            }
            if (!string.IsNullOrEmpty(filter.Remark))
            {
                query = query.Where(x => x.DeductMoney.Remark.Contains(filter.Remark));
            }
            #endregion

            IncreaseDeductMoneyDTO.SortByDeductMoney(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<DeductMoneyQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => IncreaseDeductMoneyDTO.CreateFromQueryResultDeductMoney(o)).ToList();

            return new DeductMoneyPaging()
            {
                PageOutput = pageOutput,
                DeductMoneys = results
            };
        }

        public async Task<IncreaseDeductMoneyDTO> GetDeductMoneyAsync(Guid id)
        {
            var model = await DB.DeductMoneys.Where(o => o.ID == id).FirstAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromDeductMoneyModel(model);
            return result;
        }

        public async Task<IncreaseDeductMoneyDTO> CreateDeductMoneyAsync(IncreaseDeductMoneyDTO input)
        {
            await input.DeductMoneyValidateAsync(DB);

            DeductMoney model = new DeductMoney();
            input.ToDeductMoneyModel(ref model);

            await DB.DeductMoneys.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromDeductMoneyModel(model);
            return result;
        }

        public async Task<IncreaseDeductMoneyDTO> UpdateDeductMoneyAsync(Guid id, IncreaseDeductMoneyDTO input)
        {
            await input.DeductMoneyValidateAsync(DB);

            var model = await DB.DeductMoneys.Where(o => o.ID == id).FirstAsync();
            input.ToDeductMoneyModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromDeductMoneyModel(model);
            return result;
        }

        public async Task<DeductMoney> DeleteDeductMoneyAsync(Guid id)
        {
            var model = await DB.DeductMoneys.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
