using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class WaterElectricMeterPriceService : IWaterElectricMeterPriceService
    {
        private readonly DatabaseContext DB;
        public WaterElectricMeterPriceService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<WaterMeterPriceDropdownDTO>> GetWaterMeterPriceDropdownListAsync(Guid unitID)
        {
            var modelID = await DB.Units.Where(x => x.ID == unitID).Select(x => x.ModelID).FirstAsync();
            IQueryable<WaterElectricMeterPrice> query = DB.WaterElectricMeterPrices.Include(o => o.UpdatedBy).Where(x => x.ModelID == modelID).OrderByDescending(o => o.Version);

            var queryResults = await query.OrderBy(o => o.Version).ToListAsync();
            var results = queryResults.Select(o => WaterMeterPriceDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
        public async Task<List<ElectricMeterPriceDropdownDTO>> GetElectricMeterPriceDropdownListAsync(Guid unitID)
        {
            var modelID = await DB.Units.Where(x => x.ID == unitID).Select(x => x.ModelID).FirstAsync();
            IQueryable<WaterElectricMeterPrice> query = DB.WaterElectricMeterPrices.Include(o => o.UpdatedBy).Where(x => x.ModelID == modelID).OrderByDescending(o => o.Version);

            var queryResults = await query.ToListAsync();
            var results = queryResults.Select(o => ElectricMeterPriceDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
        public async Task<WaterElectricMeterPricePaging> GetWaterElectricMeterPriceListAsync(Guid modelID, WaterElectricMeterPriceFilter request, PageParam pageParam, SortByParam sortByParam)
        {
            IQueryable<WaterElectricMeterPriceQueryResult> query = DB.WaterElectricMeterPrices.Where(x => x.ModelID == modelID)
                                                                      .Select(x => new WaterElectricMeterPriceQueryResult
                                                                      {
                                                                          WaterElectricMeterPrice = x,
                                                                          UpdatedBy = x.UpdatedBy
                                                                      });


            #region Filter
            if (request.Version != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Version == request.Version);
            }
            if (request.ElectricMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= request.ElectricMeterPriceFrom);
            }
            if (request.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= request.ElectricMeterPriceTo);
            }
            if (request.ElectricMeterPriceFrom != null && request.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= request.ElectricMeterPriceTo
                                   && x.WaterElectricMeterPrice.ElectricMeterPrice >= request.ElectricMeterPriceFrom);
            }
            if (!string.IsNullOrEmpty(request.ElectricMeterSize))
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterSize.Contains(request.ElectricMeterSize));
            }

            if (request.WaterMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= request.WaterMeterPriceFrom);
            }
            if (request.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= request.WaterMeterPriceTo);
            }
            if (request.WaterMeterPriceFrom != null && request.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= request.WaterMeterPriceTo
                                   && x.WaterElectricMeterPrice.WaterMeterPrice >= request.WaterMeterPriceFrom);
            }

            if (!string.IsNullOrEmpty(request.WaterMeterSize))
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterSize.Contains(request.WaterMeterSize));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.Updated >= request.UpdatedFrom && x.WaterElectricMeterPrice.Updated <= request.UpdatedTo);
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            #endregion

            WaterElectricMeterPriceDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<WaterElectricMeterPriceQueryResult>(pageParam, ref query);

            var results = await query.Select(o => WaterElectricMeterPriceDTO.CreateFromQueryResult(o)).ToListAsync();

            return new WaterElectricMeterPricePaging()
            {
                PageOutput = pageOutput,
                WaterElectricMeterPrices = results
            };
        }

        public async Task<WaterElectricMeterPriceDTO> GetWaterElectricMeterPriceAsync(Guid modelID, Guid id)
        {
            var model = await DB.WaterElectricMeterPrices.Where(x => x.ModelID == modelID && x.ID == id).FirstAsync();

            var result = WaterElectricMeterPriceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<WaterElectricMeterPriceDTO> CreateWaterElectricMeterPriceAsync(Guid modelID, WaterElectricMeterPriceDTO input)
        {
            var version = await DB.WaterElectricMeterPrices.Where(x => x.ModelID == modelID).Select(x => x.Version).OrderByDescending(x => x.Value).FirstOrDefaultAsync();
            WaterElectricMeterPrice model = new WaterElectricMeterPrice();

            input.ToModel(ref model);
            model.ModelID = modelID;
            model.Version = version == null ? 1 : version + 1;
            await DB.WaterElectricMeterPrices.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetWaterElectricMeterPriceAsync(modelID, model.ID);
            return result;
        }

        public async Task<WaterElectricMeterPriceDTO> UpdateWaterElectricMeterPriceAsync(Guid modelID, Guid id, WaterElectricMeterPriceDTO input)
        {
            var model = await DB.WaterElectricMeterPrices.Where(x => x.ModelID == modelID && x.ID == id).FirstAsync();

            input.ToModel(ref model);
            model.ModelID = modelID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = WaterElectricMeterPriceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<WaterElectricMeterPrice> DeleteWaterElectricMeterPriceAsync(Guid modelID, Guid id)
        {
            var model = await DB.WaterElectricMeterPrices.Where(x => x.ModelID == modelID && x.ID == id).FirstAsync();

            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
