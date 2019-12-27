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
    public class MasterCenterGroupService : IMasterCenterGroupService
    {
        private readonly DatabaseContext DB;

        public MasterCenterGroupService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<MasterCenterGroupPaging> GetMasterCenterGroupListAsync(MasterCenterGroupFilter request, PageParam pageParam, MasterCenterGroupSortByParam sortByParam)
        {
            IQueryable<MasterCenterGroupQueryResult> query = DB.MasterCenterGroups
                                                               .Select(o => new MasterCenterGroupQueryResult
                                                               {
                                                                   MasterCenterGroup = o,
                                                                   UpdatedBy = o.UpdatedBy
                                                               });

            #region Filter
            if (!string.IsNullOrEmpty(request.Key))
            {
                query = query.Where(x => x.MasterCenterGroup.Key.Contains(request.Key));
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.MasterCenterGroup.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(request.UpdatedBy));
            }
            if (request.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated >= request.UpdatedFrom);
            }
            if (request.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated <= request.UpdatedTo);
            }
            if (request.UpdatedFrom != null && request.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterCenterGroup.Updated >= request.UpdatedFrom && x.MasterCenterGroup.Updated <= request.UpdatedTo);
            }
            #endregion

            MasterCenterGroupDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterCenterGroupQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterCenterGroupDTO.CreateFromQueryResult(o)).ToList();

            return new MasterCenterGroupPaging()
            {
                MasterCenterGroups = results,
                PageOutput = pageOutput
            };
        }

        public async Task<MasterCenterGroupDTO> GetMasterCenterGroupAsync(string key)
        {
            var model = await DB.MasterCenterGroups.Include(o => o.UpdatedBy).Where(o => o.Key == key).FirstOrDefaultAsync();
            var result = MasterCenterGroupDTO.CreateFromModel(model);
            return result;
        }

        public async Task<MasterCenterGroupDTO> CreateMasterCenterGroupAsync(MasterCenterGroupDTO input)
        {
            try
            {
                //await input.ValidateAsync(DB);
                MasterCenterGroup model = new MasterCenterGroup();
                input.ToModel(ref model);
                await DB.MasterCenterGroups.AddAsync(model);
                await DB.SaveChangesAsync();
                var result = await this.GetMasterCenterGroupAsync(model.Key);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MasterCenterGroupDTO> UpdateMasterCenterGroupAsync(string key, MasterCenterGroupDTO input)
        {
            try
            {
                //await input.ValidateAsync(DB,true);
                var model = await DB.MasterCenterGroups.Where(o => o.Key == key).FirstOrDefaultAsync();
                input.ToModel(ref model);

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                var result = await this.GetMasterCenterGroupAsync(model.Key);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MasterCenterGroup> DeleteMasterCenterGroupAsync(string key)
        {
            try
            {
                var model = await DB.MasterCenterGroups.Where(o => o.Key == key).FirstOrDefaultAsync();
                model.IsDeleted = true;
                await DB.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
