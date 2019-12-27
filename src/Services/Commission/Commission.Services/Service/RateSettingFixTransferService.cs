using Database.Models;
using Database.Models.CMS;
using Database.Models.PRJ;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public class RateSettingFixTransferService : IRateSettingFixTransferService
    {
        private readonly DatabaseContext DB;

        public RateSettingFixTransferService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RateSettingFixTransferPaging> GetRateSettingFixTransferListAsync(RateSettingFixTransferFilter filter, PageParam pageParam, RateSettingFixTransferSortByParam sortByParam)
        {
            IQueryable<RateSettingFixTransferQueryResult> query = DB.RateSettingFixTransfers
                                                  .Include(x => x.Project)
                                                  .Select(o => new RateSettingFixTransferQueryResult()
                                                  {
                                                      RateSettingFixTransfer = o,
                                                      Project = o.Project
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingFixTransfer.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.ProjectID == filter.ProjectID);
            }
            if (filter.Amount.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.Amount == filter.Amount);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingFixTransfer.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransfer.Created >= filter.CreateDateFrom && x.RateSettingFixTransfer.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingFixTransfer.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingFixSaleTransferDTO.SortByFixTransfer(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingFixTransferQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingFixSaleTransferDTO.CreateFromFixTransferQueryResult(o)).ToList();

            return new RateSettingFixTransferPaging()
            {
                PageOutput = pageOutput,
                RateSettingFixTransfers = results
            };
        }

        public async Task<RateSettingFixSaleTransferDTO> GetRateSettingFixTransferAsync(Guid id)
        {
            var model = await DB.RateSettingFixTransfers.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingFixSaleTransferDTO.CreateFromFixTransferModel(model);
            return result;
        }

        public async Task CreateRateSettingFixTransferAsync(RateSettingFixSaleTransferDTO input)
        {
            if (input.ListProjectId.Count() > 0)
            {
                var lstRateSettingFixTransfer = new List<RateSettingFixTransfer>();
                var lstUpdateRateSettingFixTransfer = new List<RateSettingFixTransfer>();

                foreach (var pjId in input.ListProjectId)
                {
                    var model = new RateSettingFixTransfer();
                    model.ActiveDate = input.ActiveDate;
                    model.ProjectID = pjId;
                    model.Amount = input.Amount;
                    model.IsActive = true;
                    lstRateSettingFixTransfer.Add(model);


                    var lstUpdate = await DB.RateSettingFixTransfers.Where(o => o.ProjectID == pjId 
                                                                                && o.ActiveDate <= input.ActiveDate 
                                                                                && o.IsActive == true).ToListAsync();
                    foreach (var update in lstUpdate)
                    {
                        update.IsActive = false;

                        lstUpdateRateSettingFixTransfer.Add(update);
                    }
                }

                DB.RateSettingFixTransfers.UpdateRange(lstUpdateRateSettingFixTransfer);
                await DB.RateSettingFixTransfers.AddRangeAsync(lstRateSettingFixTransfer);
                await DB.SaveChangesAsync();
            }
        }

        public async Task<RateSettingFixSaleTransferDTO> UpdateRateSettingFixTransferAsync(Guid id, RateSettingFixSaleTransferDTO input)
        {
            await input.FixTransferValidateAsync(DB);

            var model = await DB.RateSettingFixTransfers.Where(o => o.ID == id).FirstAsync();
            input.ToFixTransferModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferDTO.CreateFromFixTransferModel(model);
            return result;
        }

        public async Task<RateSettingFixTransfer> DeleteRateSettingFixTransferAsync(Guid id)
        {
            var model = await DB.RateSettingFixTransfers.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
