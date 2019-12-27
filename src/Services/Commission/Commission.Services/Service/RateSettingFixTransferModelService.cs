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
    public class RateSettingFixTransferModelService : IRateSettingFixTransferModelService
    {
        private readonly DatabaseContext DB;

        public RateSettingFixTransferModelService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RateSettingFixTransferModelPaging> GetRateSettingFixTransferModelListAsync(RateSettingFixTransferModelFilter filter, PageParam pageParam, RateSettingFixTransferModelSortByParam sortByParam)
        {
            IQueryable<RateSettingFixTransferModelQueryResult> query = DB.RateSettingFixTransferModels
                                                    .Include(o => o.Project)
                                                    .Include(o => o.Model)
                                                  .Select(o => new RateSettingFixTransferModelQueryResult()
                                                  {
                                                      RateSettingFixTransferModel = o,
                                                      Project = o.Project,
                                                      Model = o.Model,
                                                      CreatedByUser = o.CreatedBy
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingFixTransferModel.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingFixTransferModel.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.Created >= filter.CreateDateFrom && x.RateSettingFixTransferModel.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingFixSaleTransferModelDTO.SortByFixTransferModel(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingFixTransferModelQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingFixSaleTransferModelDTO.CreateFromFixTransferModelQueryResult(o)).ToList();

            return new RateSettingFixTransferModelPaging()
            {
                PageOutput = pageOutput,
                RateSettingFixTransferModels = results
            };
        }

        public async Task<List<RateSettingFixSaleTransferModelDTO>> GetRateSettingFixTransferModelProjectListAsync(Guid? ProjectID, DateTime? ActiveDate)
        {
            IQueryable<RateSettingFixTransferModelQueryResult> query = from m in DB.Models
                                                                    .Include(x => x.Project)
                                                                join rm in DB.RateSettingFixTransferModels on m.ID equals rm.ModelID into g
                                                                from o in g.DefaultIfEmpty()
                                                                where (o == null
                                                                    || o.ActiveDate == (DB.RateSettingFixTransferModels.Where(n => n.ModelID == m.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                                select new RateSettingFixTransferModelQueryResult()
                                                                {
                                                                    RateSettingFixTransferModel = o ?? new RateSettingFixTransferModel(),
                                                                    Project = m.Project,
                                                                    Model = m
                                                                };

            #region Filter
            if (ProjectID != null)
            {
                query = query.Where(x => x.Model.ProjectID == ProjectID);
            }
            if (ActiveDate != null)
            {
                query = query.Where(x => x.RateSettingFixTransferModel.ActiveDate == ActiveDate);
            }
            #endregion

            var results = await query.Select(o => RateSettingFixSaleTransferModelDTO.CreateFromFixTransferModelQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task CreateRateSettingFixTransferModelListAsync(List<RateSettingFixSaleTransferModelDTO> ListInput)
        {
            if (ListInput.Count() > 0)
            {
                var lstRateSettingFixTransferModel = new List<RateSettingFixTransferModel>();
                var lstUpdateRateSettingFixTransferModel = new List<RateSettingFixTransferModel>();

                foreach (var input in ListInput)
                {
                    var model = new RateSettingFixTransferModel();
                    model.ActiveDate = input.ActiveDate;
                    model.ModelID = input.Model.Id;
                    model.ProjectID = input.Project.Id;
                    model.Amount = input.Amount;
                    model.IsActive = true;
                    lstRateSettingFixTransferModel.Add(model);


                    var lstUpdate = await DB.RateSettingFixTransferModels.Where(o => o.ProjectID == input.Project.Id 
                                                                                    && o.ModelID == input.Model.Id 
                                                                                    && o.ActiveDate <= input.ActiveDate 
                                                                                    && o.IsActive == true).ToListAsync();
                    foreach (var update in lstUpdate)
                    {
                        update.IsActive = false;
                        update.IsDeleted = false;

                        lstUpdateRateSettingFixTransferModel.Add(update);
                    }
                }

                DB.RateSettingFixTransferModels.UpdateRange(lstUpdateRateSettingFixTransferModel);
                await DB.RateSettingFixTransferModels.AddRangeAsync(lstRateSettingFixTransferModel);
                await DB.SaveChangesAsync();
            }
        }

        public async Task<RateSettingFixSaleTransferModelDTO> GetRateSettingFixTransferModelAsync(Guid id)
        {
            var model = await DB.RateSettingFixTransferModels.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixTransferModelModel(model);
            return result;
        }

        public async Task<RateSettingFixSaleTransferModelDTO> CreateRateSettingFixTransferModelAsync(RateSettingFixSaleTransferModelDTO input)
        {
            await input.FixTransferModelValidateAsync(DB);

            RateSettingFixTransferModel model = new RateSettingFixTransferModel();
            input.ToFixTransferModelModel(ref model);

            await DB.RateSettingFixTransferModels.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixTransferModelModel(model);
            return result;
        }

        public async Task<RateSettingFixSaleTransferModelDTO> UpdateRateSettingFixTransferModelAsync(Guid id, RateSettingFixSaleTransferModelDTO input)
        {
            await input.FixTransferModelValidateAsync(DB);

            var model = await DB.RateSettingFixTransferModels.Where(o => o.ID == id).FirstAsync();
            input.ToFixTransferModelModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixTransferModelModel(model);
            return result;
        }

        public async Task<RateSettingFixTransferModel> DeleteRateSettingFixTransferModelAsync(Guid id)
        {
            var model = await DB.RateSettingFixTransferModels.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
