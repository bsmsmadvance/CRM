using Database.Models;
using Database.Models.MST;
using Base.DTOs.MST;
using MasterData.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Filters;

namespace MasterData.Services
{
    /// <summary>
    /// เหตุผลยกเลิกการจองหรือสัญญา
    /// CancelReason
    /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17484404/367792587/preview
    /// </summary>
    public class CancelReasonService : ICancelReasonService
    {
        private readonly DatabaseContext DB;
        public CancelReasonService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<CancelReasonDropdownDTO>> GetCancelReasonDropdownListAsync()
        {
            var query = DB.CancelReasons.AsQueryable();

            var queryResults = await query.ToListAsync();
            var results = queryResults.Select(o => CancelReasonDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<CancelReasonPaging> GetCancelReasonListAsync(CancelReasonFilter filter, PageParam pageParam, CancelReasonSortByParam sortByParam)
        {
            IQueryable<CancelReasonQueryResult> query = DB.CancelReasons.Select(o => new CancelReasonQueryResult()
            {
                CancelReason = o,
                GroupOfCancelReason = o.GroupOfCancelReason,
                CancelApproveFlow = o.CancelApproveFlow,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Key))
            {
                query = query.Where(o => o.CancelReason.Key.Contains(filter.Key));
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(o => o.CancelReason.Description.Contains(filter.Description));
            }
            if (!string.IsNullOrEmpty(filter.GroupOfCancelReasonKey))
            {
                var groupOfCancelReasonKeyMasterCenterID = await DB.MasterCenters.Where(o => o.Key == filter.GroupOfCancelReasonKey
                                                      && o.MasterCenterGroupKey == "GroupOfCancelReason")
                                                     .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.GroupOfCancelReason.ID == groupOfCancelReasonKeyMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.CancelApproveFlowKey))
            {
                var cancelApproveFlowKeyMasterCenterID = await DB.MasterCenters.Where(o => o.Key == filter.CancelApproveFlowKey
                                                    && o.MasterCenterGroupKey == "CancelApproveFlow")
                                                   .Select(o => o.ID).FirstAsync();
                query = query.Where(o => o.CancelApproveFlow.ID == cancelApproveFlowKeyMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(o => o.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(o => o.CancelReason.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(o => o.CancelReason.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(o => o.CancelReason.Updated >= filter.UpdatedFrom && o.CancelReason.Updated <= filter.UpdatedTo);
            }
            #endregion

            CancelReasonDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<CancelReasonQueryResult>(pageParam, ref query);
            var queryResults = await query.ToListAsync();
            var results = queryResults.Select(o => CancelReasonDTO.CreateFromQueryResult(o)).ToList();
            return new CancelReasonPaging()
            {
                PageOutput = pageOutput,
                CancelReasons = results
            };
        }
        public async Task<CancelReasonDTO> GetCancelReasonAsync(Guid id)
        {
            var model = await DB.CancelReasons.Where(o => o.ID == id)
                                            .Include(o => o.GroupOfCancelReason)
                                            .Include(o => o.CancelApproveFlow)
                                            .Include(o => o.UpdatedBy)
                                            .FirstAsync();
            var result = CancelReasonDTO.CreateFromModel(model);
            return result;
        }
        public async Task<CancelReasonDTO> CreateCancelReasonAsync(CancelReasonDTO input)
        {
            await input.ValidateAsync(DB);
            CancelReason model = new CancelReason();
            input.ToModel(ref model);
            await DB.CancelReasons.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetCancelReasonAsync(model.ID);
            return result;
        }
        public async Task<CancelReasonDTO> UpdateCancelReasonAsync(Guid id, CancelReasonDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.CancelReasons.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetCancelReasonAsync(model.ID);
            return result;
        }
        public async Task<CancelReason> DeleteCancelReasonAsync(Guid id)
        {
            var model = await DB.CancelReasons.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
