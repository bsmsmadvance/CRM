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
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;
using Database.Models.DbQueries;

namespace MasterData.Services
{
    public class MasterCenterService : IMasterCenterService
    {
        private readonly DatabaseContext DB;
        private readonly DbQueryContext DBQuery;

        public MasterCenterService(DatabaseContext db, DbQueryContext dbQuery)
        {
            this.DB = db;
            this.DBQuery = dbQuery;
        }

        public async Task<List<MasterCenterDropdownDTO>> GetMasterCenterDropdownListAsync(string masterCenterGroupKey, string name)
        {
            IQueryable<MasterCenter> query = DB.MasterCenters;
            if (!string.IsNullOrEmpty(masterCenterGroupKey))
            {
                query = query.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            var queryResults = await query.Where(o => o.IsActive).OrderBy(o => o.Order).Take(100).ToListAsync();

            var results = queryResults.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<MasterCenterPaging> GetMasterCenterListAsync(MasterCenterFilter filter, PageParam pageParam, MasterCenterSortByParam sortByParam)
        {
            IQueryable<MasterCenterQueryResult> query = DB.MasterCenters
                                                          .Select(o => new MasterCenterQueryResult
                                                          {
                                                              MasterCenter = o,
                                                              MasterCenterGroup = o.MasterCenterGroup,
                                                              UpdatedBy = o.UpdatedBy
                                                          });

            #region Filter
            if (!string.IsNullOrEmpty(filter.MasterCenterGroupKey))
            {
                query = query.Where(x => x.MasterCenter.MasterCenterGroupKey == filter.MasterCenterGroupKey);
            }
            if (!string.IsNullOrEmpty(filter.Key))
            {
                query = query.Where(x => x.MasterCenter.Key.Contains(filter.Key));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.MasterCenter.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.MasterCenter.NameEN.Contains(filter.NameEN));
            }
            if (filter.Order != null)
            {
                query = query.Where(x => x.MasterCenter.Order == filter.Order);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.MasterCenter.IsActive == filter.IsActive);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterCenter.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenter.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenter.Updated >= filter.UpdatedFrom && x.MasterCenter.Updated <= filter.UpdatedTo);
            }
            #endregion

            MasterCenterDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterCenterQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterCenterDTO.CreateFromQueryResult(o)).ToList();

            return new MasterCenterPaging()
            {
                PageOutput = pageOutput,
                MasterCenters = results
            };
        }

        public async Task<MasterCenterDropdownDTO> GetFindMasterCenterDropdownItemAsync(string masterCenterGroupKey, string key)
        {
            var model = await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key);
            var result = MasterCenterDropdownDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterDTO> GetMasterCenterAsync(Guid id)
        {
            var model = await DB.MasterCenters.Where(o => o.ID == id)
                                              .Include(o => o.MasterCenterGroup)
                                              .Include(o => o.UpdatedBy)
                                              .FirstAsync();
            var result = MasterCenterDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterDTO> CreateMasterCenterAsync(MasterCenterDTO input)
        {
            await input.ValidateAsync(DB);

            MasterCenter model = new MasterCenter();
            input.ToModel(ref model);
            await DB.MasterCenters.AddAsync(model);
            await DB.SaveChangesAsync();


            var result = await this.GetMasterCenterAsync(model.ID);
            return result;
        }

        public async Task<MasterCenterDTO> UpdateMasterCenterAsync(Guid id, MasterCenterDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.MasterCenters.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetMasterCenterAsync(model.ID);
            return result;
        }

        public async Task<MasterCenter> DeleteMasterCenterAsync(Guid id)
        {
            var model = await DB.MasterCenters.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        // TODO : Query แบบ string
        public async Task<List<MasterCenterResult>> GetMasterCenterUsingDbQueryAsync(string masterCenterGroupKey)
        {
            var query = DBQuery.MasterCenterResults.FromSql(@"SELECT mg.[Key] as GroupKey, mg.Name as GroupName, mc.ID, mc.[Key], mc.Name FROM MST.MasterCenter mc
                LEFT JOIN MST.MasterCenterGroup mg ON mg.[Key]=mc.MasterCenterGroupKey WHERE mg.[Key]={0}", masterCenterGroupKey);
            var results = await query.ToListAsync();
            
            return results;
        }
    }
}
