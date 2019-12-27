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
    public class LandOfficeService : ILandOfficeService
    {
        private readonly DatabaseContext DB;

        public LandOfficeService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<LandOfficeListDTO>> GetLandOfficeDropdownListAsync(string name, Guid? provinceID = null)
        {
            IQueryable<LandOffice> query = DB.LandOffices;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            if (provinceID != null)
            {
                query = query.Where(o => o.SubDistricts.Where(m => m.District.ProvinceID == provinceID).Any());
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();

            var results = queryResults.Select(o => LandOfficeListDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<LandOfficePaging> GetLandOfficeListAsync(LandOfficeFilter filter, PageParam pageParam, LandOfficeSortByParam sortByParam)
        {
            IQueryable<LandOfficeQueryResult> query = DB.LandOffices
                                                        .Select(x => new LandOfficeQueryResult
                                                        {
                                                            LandOffice = x,
                                                            UpdatedBy = x.UpdatedBy
                                                        });

            #region Filter
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.LandOffice.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.LandOffice.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.LandOffice.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LandOffice.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.LandOffice.Updated >= filter.UpdatedFrom && x.LandOffice.Updated <= filter.UpdatedTo);
            }
            #endregion

            var results = await query.ToListAsync();

            var resultDTOs = results.Select(async o => await LandOfficeDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            #region List Filter
            if (filter.ProvinceID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.Province?.Id == filter.ProvinceID).ToList();
            }
            if (filter.DistrictID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.District?.Id == filter.DistrictID).ToList();
            }
            if (filter.SubDistrictID != null)
            {
                resultDTOs = resultDTOs.Where(o => o.SubDistrict?.Id == filter.SubDistrictID).ToList();
            }
            #endregion

            LandOfficeDTO.SortByList(sortByParam, ref resultDTOs);
            var pageOutput = PagingHelper.PagingList<LandOfficeDTO>(pageParam, ref resultDTOs);

            return new LandOfficePaging()
            {
                LandOffices = resultDTOs,
                PageOutput = pageOutput
            };
        }

        public async Task<LandOfficeDTO> GetLandOfficeAsync(Guid id)
        {
            var model = await DB.LandOffices.Include(o => o.UpdatedBy).Where(x => x.ID == id).FirstAsync();
            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<LandOfficeDTO> CreateLandOfficeAsync(LandOfficeDTO input)
        {
            await input.ValidateAsync(DB);
            LandOffice model = new LandOffice();
            input.ToModel(ref model);
            
            await DB.LandOffices.AddAsync(model);
            if (input.SubDistrict != null)
            {
                var subDistrict = await DB.SubDistricts.Where(o => o.ID == input.SubDistrict.Id).FirstOrDefaultAsync();
                if (subDistrict != null)
                {
                    subDistrict.LandOfficeID = model.ID;
                    DB.Update(subDistrict);
                }
            }
            await DB.SaveChangesAsync();
            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<LandOfficeDTO> UpdateLandOfficeAsync(Guid id, LandOfficeDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.LandOffices.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;

            if (input.SubDistrict != null)
            {
                var subDistrict = await DB.SubDistricts.Where(o => o.ID == input.SubDistrict.Id).FirstOrDefaultAsync();
                if (subDistrict != null)
                {
                    subDistrict.LandOfficeID = model.ID;
                    DB.Update(subDistrict);
                }
            }

            await DB.SaveChangesAsync();
            var result = await LandOfficeDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<LandOffice> DeleteLandOfficeAsync(Guid id)
        {
            var model = await DB.LandOffices.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
