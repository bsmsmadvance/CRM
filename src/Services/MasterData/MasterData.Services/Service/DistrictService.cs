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
    public class DistrictService : IDistrictService
    {
        private readonly DatabaseContext DB;

        public DistrictService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<DistrictListDTO> FindDistrictAsync(Guid provinceID, string name)
        {
            var model = await DB.Districts.FirstOrDefaultAsync(o => o.ProvinceID == provinceID && (o.NameTH == name || o.NameEN == name));
            return DistrictListDTO.CreateFromModel(model);
        }

        public async Task<List<DistrictListDTO>> GetDistrictDropdownListAsync(string name, Guid? provinceID)
        {
            IQueryable<District> query = DB.Districts;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (provinceID != null)
            {
                query = query.Where(o => o.ProvinceID == provinceID);
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => DistrictListDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<DistrictPaging> GetDistrictListAsync(DistrictFilter filter, PageParam pageParam, DistrictSortByParam sortByParam)
        {
            IQueryable<DistrictQueryResult> query = DB.Districts.Include(o => o.SubDistricts).Select(o => new DistrictQueryResult
            {
                District = o,
                Province = o.Province,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.District.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.District.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                query = query.Where(x => x.District.SubDistricts.Where(o => o.PostalCode.Contains(filter.PostalCode)).Any());
            }
            if (filter.ProvinceID != Guid.Empty && filter.ProvinceID != null)
            {
                query = query.Where(x => x.District.ProvinceID == filter.ProvinceID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.District.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.District.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.District.Updated >= filter.UpdatedFrom && x.District.Updated <= filter.UpdatedTo);
            }
            #endregion

            DistrictDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<DistrictQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => DistrictDTO.CreateFromQueryResult(o)).ToList();

            return new DistrictPaging()
            {
                PageOutput = pageOutput,
                Districts = results
            };
        }

        public async Task<DistrictDTO> GetDistrictAsync(Guid id)
        {
            var model = await DB.Districts.Where(o => o.ID == id)
                                          .Include(o => o.Province)
                                          .Include(o => o.UpdatedBy)
                                          .FirstAsync();
            var result = DistrictDTO.CreateFromModel(model);
            return result;
        }

        public async Task<DistrictDTO> CreateDistrictAsync(DistrictDTO input)
        {
            await input.ValidateAsync(DB);
            District model = new District();
            input.ToModel(ref model);

            await DB.Districts.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetDistrictAsync(model.ID);
            return result;
        }

        public async Task<DistrictDTO> UpdateDistrictAsync(Guid id, DistrictDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Districts.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetDistrictAsync(model.ID);
            return result;
        }

        public async Task<District> DeleteDistrictAsync(Guid id)
        {
            var model = await DB.Districts.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
