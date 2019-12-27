using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;

namespace MasterData.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly DatabaseContext DB;

        public ProvinceService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ProvinceListDTO> FindProvinceAsync(string name)
        {
            var model = await DB.Provinces.FirstOrDefaultAsync(o => o.NameTH == name || o.NameEN == name);
            return ProvinceListDTO.CreateFromModel(model);
        }

        public async Task<List<ProvinceListDTO>> GetProvinceDropdownListAsync(string name)
        {
            IQueryable<Province> query = DB.Provinces;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => ProvinceListDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<ProvincePaging> GetProvinceListAsync(ProvinceFilter filter, PageParam pageParam, ProvinceSortByParam sortByParam)
        {
            IQueryable<ProvinceQueryResult> query = DB.Provinces.Select(o => new ProvinceQueryResult
            {
                Province = o,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Province.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Province.NameEN.Contains(filter.NameEN));
            }
            if (filter.IsShow != null)
            {
                query = query.Where(x => x.Province.IsShow == filter.IsShow);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Province.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Province.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Province.Updated >= filter.UpdatedFrom && x.Province.Updated <= filter.UpdatedTo);
            }
            #endregion

            ProvinceDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ProvinceQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => ProvinceDTO.CreateFromQueryResult(o)).ToList();

            return new ProvincePaging()
            {
                PageOutput = pageOutput,
                Provinces = results
            };
        }

        public async Task<ProvinceDTO> GetProvincePostalCodeAsync(string postalCode)
        {
            var model = await (from province in DB.Provinces
                               join district in DB.Districts on province.ID equals district.ProvinceID
                               join subdistrict in DB.SubDistricts on district.ID equals subdistrict.DistrictID
                               where district.PostalCode == postalCode || subdistrict.PostalCode == postalCode
                               select province
                               ).FirstOrDefaultAsync();

            var result = ProvinceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ProvinceDTO> GetProvinceAsync(Guid id)
        {
            var model = await DB.Provinces.Include(o => o.UpdatedBy).Where(o => o.ID == id).FirstAsync();
            var result = ProvinceDTO.CreateFromModel(model);
            return result;
        }

        public async Task<ProvinceDTO> CreateProvinceAsync(ProvinceDTO input)
        {
            await input.ValidateAsync(DB);
            Province model = new Province();
            input.ToModel(ref model);

            await DB.Provinces.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await GetProvinceAsync(model.ID);
            return result;
        }

        public async Task<ProvinceDTO> UpdateProvinceAsync(Guid id, ProvinceDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.Provinces.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await GetProvinceAsync(model.ID);
            return result;
        }

        public async Task<Province> DeleteProvinceAsync(Guid id)
        {
            var model = await DB.Provinces.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
