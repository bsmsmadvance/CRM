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
    public class RateSettingFixSaleService : IRateSettingFixSaleService
    {
        private readonly DatabaseContext DB;

        public RateSettingFixSaleService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RateSettingFixSalePaging> GetRateSettingFixSaleListAsync(RateSettingFixSaleFilter filter, PageParam pageParam, RateSettingFixSaleSortByParam sortByParam)
        {
            IQueryable<RateSettingFixSaleQueryResult> query = DB.RateSettingFixSales
                                                  .Include(x => x.Project)
                                                  .Select(o => new RateSettingFixSaleQueryResult()
                                                  {
                                                      RateSettingFixSale = o,
                                                      Project = o.Project
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingFixSale.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.ProjectID == filter.ProjectID);
            }
            if (filter.Amount.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.Amount == filter.Amount);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingFixSale.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSale.Created >= filter.CreateDateFrom && x.RateSettingFixSale.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingFixSale.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingFixSaleTransferDTO.SortByFixSale(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingFixSaleQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingFixSaleTransferDTO.CreateFromFixSaleQueryResult(o)).ToList();

            return new RateSettingFixSalePaging()
            {
                PageOutput = pageOutput,
                RateSettingFixSales = results
            };
        }

        public async Task<RateSettingFixSaleTransferDTO> GetRateSettingFixSaleAsync(Guid id)
        {
            var model = await DB.RateSettingFixSales.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingFixSaleTransferDTO.CreateFromFixSaleModel(model);
            return result;
        }

        public async Task CreateRateSettingFixSaleAsync(RateSettingFixSaleTransferDTO input)
        {
            if (input.ListProjectId.Count() > 0)
            {
                var lstRateSettingFixSale = new List<RateSettingFixSale>();
                var lstUpdateRateSettingFixSale = new List<RateSettingFixSale>();

                foreach (var pjId in input.ListProjectId)
                {
                    var model = new RateSettingFixSale();
                    model.ActiveDate = input.ActiveDate;
                    model.ProjectID = pjId;
                    model.Amount = input.Amount;
                    model.IsActive = true;
                    lstRateSettingFixSale.Add(model);


                    var lstUpdate = await DB.RateSettingFixSales.Where(o => o.ProjectID == pjId 
                                                                        && o.ActiveDate <= input.ActiveDate 
                                                                        && o.IsActive == true).ToListAsync();
                    foreach (var update in lstUpdate)
                    {
                        update.IsActive = false;

                        lstUpdateRateSettingFixSale.Add(update);
                    }
                }

                DB.RateSettingFixSales.UpdateRange(lstUpdateRateSettingFixSale);
                await DB.RateSettingFixSales.AddRangeAsync(lstRateSettingFixSale);
                await DB.SaveChangesAsync();
            }
        }

        public async Task<RateSettingFixSaleTransferDTO> UpdateRateSettingFixSaleAsync(Guid id, RateSettingFixSaleTransferDTO input)
        {
            await input.FixSaleValidateAsync(DB);

            var model = await DB.RateSettingFixSales.Where(o => o.ID == id).FirstAsync();
            input.ToFixSaleModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferDTO.CreateFromFixSaleModel(model);
            return result;
        }

        public async Task<RateSettingFixSale> DeleteRateSettingFixSaleAsync(Guid id)
        {
            var model = await DB.RateSettingFixSales.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
