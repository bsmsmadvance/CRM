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
    public class BGService : IBGService
    {
        private readonly DatabaseContext DB;

        public BGService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BGDropdownDTO>> GetBGDropdownListAsync(string productTypeKey, string name)
        {
            IQueryable<BG> query = DB.BGs.AsQueryable();
            if (!string.IsNullOrEmpty(productTypeKey))
            {
                var productTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == productTypeKey
                                                                       && x.MasterCenterGroupKey == "ProductType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.ProductTypeMasterCenterID == productTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.Name).Take(100).ToListAsync();

            var results = queryResults.Select(o => BGDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<BGPaging> GetBGListAsync(BGFilter filter, PageParam pageParam, BGSortByParam sortByParam)
        {
            IQueryable<BGQueryResult> query = DB.BGs
                                                .Select(o => new BGQueryResult
                                                {
                                                    BG = o,
                                                    ProductType = o.ProductType,
                                                    UpdatedBy = o.UpdatedBy
                                                });

            #region filter
            if (!string.IsNullOrEmpty(filter.BgNo))
            {
                query = query.Where(x => x.BG.BGNo.Contains(filter.BgNo));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.BG.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.ProductTypeKey))
            {
                var productTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ProductTypeKey
                                                                       && x.MasterCenterGroupKey == "ProductType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.BG.ProductTypeMasterCenterID == productTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.BG.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.BG.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.BG.Updated >= filter.UpdatedFrom && x.BG.Updated <= filter.UpdatedTo);
            }
            #endregion

            BGDTO.SortBy(sortByParam, ref query);

            var pageOuput = PagingHelper.Paging<BGQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => BGDTO.CreateFromQueryResult(o)).ToList();

            return new BGPaging()
            {
                BGs = results,
                PageOutput = pageOuput
            };
        }

        public async Task<BGDTO> GetBGAsync(Guid id)
        {
            var model = await DB.BGs.Where(o => o.ID == id)
                .Include(o => o.ProductType)
                .Include(o => o.UpdatedBy)
                .FirstAsync();
            var result = BGDTO.CreateFromModel(model);
            return result;
        }

        public async Task<BGDTO> CreateBGAsync(BGDTO input)
        {
            await input.ValidateAsync(DB);

            BG model = new BG();
            input.ToModel(ref model);

            await DB.BGs.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetBGAsync(model.ID);
            return result;
        }

        public async Task<BGDTO> UpdateBGAsync(Guid id, BGDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.BGs.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetBGAsync(model.ID);
            return result;
        }

        public async Task<BG> DeleteBGAsync(Guid id)
        {

            var model = await DB.BGs.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
