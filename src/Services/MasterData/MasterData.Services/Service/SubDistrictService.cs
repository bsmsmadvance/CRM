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
    public class SubDistrictService : ISubDistrictService
    {
        private readonly DatabaseContext DB;

        public SubDistrictService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<SubDistrictListDTO> FindSubDistrictAsync(Guid districtID, string name)
        {
            var model = await DB.SubDistricts.FirstOrDefaultAsync(o => o.DistrictID == districtID && (o.NameTH == name || o.NameEN == name));
            return SubDistrictListDTO.CreateFromModel(model);
        }

        public async Task<List<SubDistrictListDTO>> GetSubDistrictDropdownListAsync(string name, Guid? districtID)
        {
            IQueryable<SubDistrict> query = DB.SubDistricts;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (districtID != null)
            {
                query = query.Where(o => o.DistrictID == districtID);
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => SubDistrictListDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<SubDistrictPaging> GetSubDistrictListAsync(SubDistrictFilter filter, PageParam pageParam, SubDistrictSortByParam sortByParam)
        {
            IQueryable<SubDistrictQueryResult> query = DB.SubDistricts.Select(o => new SubDistrictQueryResult
            {
                SubDistrict = o,
                District = o.District,
                LandOffice = o.LandOffice,
                UpdatedBy = o.UpdatedBy
            });
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.SubDistrict.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.SubDistrict.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.PostalCode))
            {
                query = query.Where(x => x.SubDistrict.PostalCode.Contains(filter.PostalCode));
            }
            if (filter.DistrictID != Guid.Empty && filter.DistrictID != null)
            {
                query = query.Where(x => x.SubDistrict.DistrictID == filter.DistrictID);
            }
            if (!string.IsNullOrEmpty(filter.LandOffice))
            {
                query = query.Where(x => x.LandOffice.NameTH.Contains(filter.LandOffice));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.SubDistrict.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.SubDistrict.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.SubDistrict.Updated >= filter.UpdatedFrom && x.SubDistrict.Updated <= filter.UpdatedTo);
            }

            SubDistrictDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<SubDistrictQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => SubDistrictDTO.CreateFromQueryResult(o)).ToList();

            return new SubDistrictPaging()
            {
                PageOutput = pageOutput,
                SubDistricts = results
            };
        }

        public async Task<SubDistrictDTO> GetSubDistrictAsync(Guid id)
        {
            var model = await DB.SubDistricts.Where(o => o.ID == id)
                                             .Include(o => o.District)
                                             .Include(o => o.LandOffice)
                                             .Include(o => o.UpdatedBy)
                                             .FirstAsync();
            var result = SubDistrictDTO.CreateFromModel(model);
            return result;
        }

        public async Task<SubDistrictDTO> CreateSubDistrictAsync(SubDistrictDTO input)
        {
            await input.ValidateAsync(DB);
            SubDistrict model = new SubDistrict();
            input.ToModel(ref model);

            if (input.LandOffice != null && input.LandOffice?.Id == null)
            {
                LandOffice landOffice = new LandOffice()
                {
                    NameTH = input.LandOffice.NameTH
                };
                await DB.AddAsync(landOffice);
                await DB.SaveChangesAsync();
                model.LandOfficeID = landOffice.ID;
            }

            await DB.SubDistricts.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetSubDistrictAsync(model.ID);
            return result;
        }

        public async Task<SubDistrictDTO> UpdateSubDistrictAsync(Guid id, SubDistrictDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.SubDistricts.Where(o => o.ID == id).FirstAsync();

            input.ToModel(ref model);

            if (input.LandOffice != null && input.LandOffice?.Id == null)
            {
                LandOffice landOffice = new LandOffice()
                {
                    NameTH = input.LandOffice.NameTH
                };
                await DB.AddAsync(landOffice);
                await DB.SaveChangesAsync();
                model.LandOfficeID = landOffice.ID;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetSubDistrictAsync(model.ID);
            return result;
        }

        public async Task<SubDistrict> DeleteSubDistrictAsync(Guid id)
        {
            var model = await DB.SubDistricts.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
