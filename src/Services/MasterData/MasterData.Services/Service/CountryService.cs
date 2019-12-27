using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace MasterData.Services
{
    public class CountryService : ICountryService
    {
        private readonly DatabaseContext DB;

        public CountryService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<CountryDTO>> GetCountryDropdownListAsync(CountryFilter filter)
        {
            var query = DB.Countries.AsQueryable();
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(o => o.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(o => o.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.Code.Contains(filter.Code));
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();
            var results = queryResults.Select(o => CountryDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<CountryPaging> GetCountryListAsync(CountryFilter filter, PageParam pageParam, CountrySortByParam sortByParam)
        {
            IQueryable<CountryQueryResult> query = DB.Countries.Select(o => new CountryQueryResult
            {
                Country = o,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(o => o.Country.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(o => o.Country.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(o => o.Country.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.Country.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.Country.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(o => o.Country.Updated >= filter.UpdatedFrom && o.Country.Updated <= filter.UpdatedTo);
            }
            #endregion

            CountryDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CountryQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CountryDTO.CreateFromQueryResult(o)).ToList();

            return new CountryPaging()
            {
                PageOutput = pageOutput,
                Countries = results
            };
        }

        public async Task<CountryDTO> GetCountryAsync(Guid id)
        {
            var model = await DB.Countries.Include(o => o.UpdatedBy).Where(o => o.ID == id).FirstOrDefaultAsync();
            var result = CountryDTO.CreateFromModel(model);
            return result;

        }

        public async Task<CountryDTO> FindCountryAsync(string code)
        {
            var model = await DB.Countries.Include(o => o.UpdatedBy).FirstOrDefaultAsync(o => o.Code == code);
            var result = CountryDTO.CreateFromModel(model);
            return result;
        }

        public async Task<CountryDTO> CreateCountryAsync(CountryDTO input)
        {
            await input.ValidateAsync(DB);
            Country model = new Country();
            input.ToModel(ref model);
            await DB.Countries.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetCountryAsync(model.ID);
            return result;
        }

        public async Task<CountryDTO> UpdateCountryAsync(Guid id, CountryDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Countries.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetCountryAsync(model.ID);
            return result;
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            var model = await DB.Countries.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
