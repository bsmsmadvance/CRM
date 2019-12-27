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
    public class IncreaseMoneyService : IIncreaseMoneyService
    {
        private readonly DatabaseContext DB;

        public IncreaseMoneyService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<IncreaseMoneyPaging> GetIncreaseMoneyListAsync(IncreaseMoneyFilter filter, PageParam pageParam, IncreaseMoneySortByParam sortByParam)
        {
            IQueryable<IncreaseMoneyQueryResult> query = DB.IncreaseMoneys
                                                  .Where(o => o.IsDeleted == false)
                                                  .Select(o => new IncreaseMoneyQueryResult()
                                                  {
                                                      IncreaseMoney = o,
                                                      Project = o.Project,
                                                      SaleUser = o.SaleUser
                                                  });

            #region Filter
            if (filter.ActiveDateForm.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.ActiveDate >= filter.ActiveDateForm);
            }
            if (filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.ActiveDateForm.HasValue && filter.ActiveDateTo.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.ActiveDate >= filter.ActiveDateForm && x.IncreaseMoney.ActiveDate <= filter.ActiveDateTo);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.SaleUserID))
            {
                query = query.Where(x => x.IncreaseMoney.SaleUser.EmployeeNo.Contains(filter.SaleUserID));
            }
            if (!string.IsNullOrEmpty(filter.SaleUserName))
            {
                query = query.Where(x => x.IncreaseMoney.SaleUser.FirstName.Contains(filter.SaleUserName) || x.IncreaseMoney.SaleUser.LastName.Contains(filter.SaleUserName));
            }
            if (filter.AmountForm.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.Amount >= filter.AmountForm);
            }
            if (filter.AmountTo.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.Amount <= filter.AmountTo);
            }
            if (filter.AmountForm.HasValue && filter.AmountTo.HasValue)
            {
                query = query.Where(x => x.IncreaseMoney.Amount >= filter.AmountForm && x.IncreaseMoney.Amount <= filter.AmountTo);
            }
            if (!string.IsNullOrEmpty(filter.Remark))
            {
                query = query.Where(x => x.IncreaseMoney.Remark.Contains(filter.Remark));
            }
            #endregion

            IncreaseDeductMoneyDTO.SortByIncreaseMoney(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<IncreaseMoneyQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => IncreaseDeductMoneyDTO.CreateFromQueryResultIncreaseMoney(o)).ToList();

            return new IncreaseMoneyPaging()
            {
                PageOutput = pageOutput,
                IncreaseMoneys = results
            };
        }

        public async Task<IncreaseDeductMoneyDTO> GetIncreaseMoneyAsync(Guid id)
        {
            var model = await DB.IncreaseMoneys.Where(o => o.ID == id).FirstAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromIncreaseMoneyModel(model);
            return result;
        }

        public async Task<IncreaseDeductMoneyDTO> CreateIncreaseMoneyAsync(IncreaseDeductMoneyDTO input)
        {
            await input.IncreaseMoneyValidateAsync(DB);

            IncreaseMoney model = new IncreaseMoney();
            input.ToIncreaseMoneyModel(ref model);

            await DB.IncreaseMoneys.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromIncreaseMoneyModel(model);
            return result;
        }

        public async Task<IncreaseDeductMoneyDTO> UpdateIncreaseMoneyAsync(Guid id, IncreaseDeductMoneyDTO input)
        {
            await input.IncreaseMoneyValidateAsync(DB);

            var model = await DB.IncreaseMoneys.Where(o => o.ID == id).FirstAsync();
            input.ToIncreaseMoneyModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = IncreaseDeductMoneyDTO.CreateFromIncreaseMoneyModel(model);
            return result;
        }

        public async Task<IncreaseMoney> DeleteIncreaseMoneyAsync(Guid id)
        {
            var model = await DB.IncreaseMoneys.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
