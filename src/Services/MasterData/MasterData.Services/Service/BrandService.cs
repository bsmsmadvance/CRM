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
    public class BrandService : IBrandService
    {
        private readonly DatabaseContext DB;

        public BrandService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BrandDropdownDTO>> GetBrandDropdownListAsync(string name)
        {
            IQueryable<Brand> query = DB.Brands;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.Name).Take(100).ToListAsync();

            var results = queryResults.Select(o => BrandDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<BrandPaging> GetBrandListAsync(BrandFilter filter, PageParam pageParam, BrandSortByParam sortByParam)
        {
            IQueryable<BrandQueryResult> query = DB.Brands
                                                   .Select(o => new BrandQueryResult
                                                   {
                                                       Brand = o,
                                                       UnitNumberFormat = o.UnitNumberFormat,
                                                       UpdatedBy = o.UpdatedBy
                                                   });

            #region Filter
            if (!string.IsNullOrEmpty(filter.BrandNo))
            {
                query = query.Where(x => x.Brand.BrandNo.Contains(filter.BrandNo));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Brand.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.UnitNumberFormatKey))
            {
                var unitNumberFormatMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitNumberFormatKey
                                                                     && x.MasterCenterGroupKey == "UnitNumberFormat")
                                                                    .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.UnitNumberFormat.ID == unitNumberFormatMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.Brand.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Brand.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Brand.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Brand.Updated >= filter.UpdatedFrom && x.Brand.Updated <= filter.UpdatedTo);
            }
            #endregion

            BrandDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<BrandQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => BrandDTO.CreateFromQueryResult(o)).ToList();

            return new BrandPaging()
            {
                PageOutput = pageOutput,
                Brands = results
            };
        }

        public async Task<BrandDTO> GetBrandAsync(Guid id)
        {

            var model = await DB.Brands.Where(o => o.ID == id)
                                       .Include(o => o.UnitNumberFormat)
                                       .Include(o => o.UpdatedBy)
                                       .FirstAsync();
            var result = BrandDTO.CreateFromModel(model);
            return result;

        }

        public async Task<BrandDTO> CreateBrandAsync(BrandDTO input)
        {
            await input.ValidateAsync(DB);
            Brand model = new Brand();
            input.ToModel(ref model);

            await DB.Brands.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetBrandAsync(model.ID);

            return result;

        }

        public async Task<BrandDTO> UpdateBrandAsync(Guid id, BrandDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Brands.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetBrandAsync(model.ID);
            return result;

        }

        public async Task<Brand> DeleteBrandAsync(Guid id)
        {

            var model = await DB.Brands.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
