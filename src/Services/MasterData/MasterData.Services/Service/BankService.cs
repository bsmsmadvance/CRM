using Database.Models;
using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterData.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MasterData.Services
{
    public class BankService : IBankService
    {
        private readonly DatabaseContext DB;

        public BankService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BankDropdownDTO>> GetBankDropdownListAsync(string name)
        {
            IQueryable<Bank> query = DB.Banks;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => BankDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<BankPaging> GetBankListAsync(BankFilter filter, PageParam pageParam, BankSortByParam sortByParam)
        {
            IQueryable<BankQueryResult> query = DB.Banks
                                                  .Select(o => new BankQueryResult()
                                                  {
                                                      Bank = o,
                                                      UpdatedBy = o.UpdatedBy
                                                  });

            #region Filter
            if (!string.IsNullOrEmpty(filter.BankNo))
            {
                query = query.Where(x => x.Bank.BankNo.Contains(filter.BankNo));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Bank.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Bank.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.Alias))
            {
                query = query.Where(x => x.Bank.Alias.Contains(filter.Alias));
            }
            if (filter.IsCreditCard != null)
            {
                query = query.Where(x => x.Bank.IsCreditCard == filter.IsCreditCard);
            }
            if (filter.IsNonBank != null)
            {
                query = query.Where(x => x.Bank.IsNonBank == filter.IsNonBank);
            }
            if (filter.IsCoorperative != null)
            {
                query = query.Where(x => x.Bank.IsCoorperative == filter.IsCoorperative);
            }
            if (filter.IsFreeMortgage != null)
            {
                query = query.Where(x => x.Bank.IsFreeMortgage == filter.IsFreeMortgage);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Bank.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Bank.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Bank.Updated >= filter.UpdatedFrom && x.Bank.Updated <= filter.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(filter.SwiftCode))
            {
                query = query.Where(x => x.Bank.SwiftCode.Contains(filter.SwiftCode));
            }
            #endregion

            BankDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BankQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => BankDTO.CreateFromQueryResult(o)).ToList();

            return new BankPaging()
            {
                PageOutput = pageOutput,
                Banks = results
            };
        }

        public async Task<BankDTO> GetBankAsync(Guid id)
        {
            var model = await DB.Banks.Include(o => o.UpdatedBy).Where(o => o.ID == id).FirstAsync();
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BankDTO> CreateBankAsync(BankDTO input)
        {
            await input.ValidateAsync(DB);

            Bank model = new Bank();
            input.ToModel(ref model);

            await DB.Banks.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BankDTO> UpdateBankAsync(Guid id, BankDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Banks.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = BankDTO.CreateFromModel(model);
            return result;
        }

        public async Task<Bank> DeleteBankAsync(Guid id)
        {
            var model = await DB.Banks.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
