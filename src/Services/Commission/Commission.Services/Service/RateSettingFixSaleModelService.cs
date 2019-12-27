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
    public class RateSettingFixSaleModelService : IRateSettingFixSaleModelService
    {
        private readonly DatabaseContext DB;

        public RateSettingFixSaleModelService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<RateSettingFixSaleModelPaging> GetRateSettingFixSaleModelListAsync(RateSettingFixSaleModelFilter filter, PageParam pageParam, RateSettingFixSaleModelSortByParam sortByParam)
        {
            IQueryable<RateSettingFixSaleModelQueryResult> query = DB.RateSettingFixSaleModels
                                                    .Include(o => o.Project)
                                                    .Include(o => o.Model)
                                                  .Select(o => new RateSettingFixSaleModelQueryResult()
                                                  {
                                                      RateSettingFixSaleModel = o,
                                                      Project = o.Project,
                                                      Model = o.Model,
                                                      CreatedByUser = o.CreatedBy
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingFixSaleModel.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingFixSaleModel.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.Created >= filter.CreateDateFrom && x.RateSettingFixSaleModel.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingFixSaleModel.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingFixSaleTransferModelDTO.SortByFixSaleModel(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingFixSaleModelQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingFixSaleTransferModelDTO.CreateFromFixSaleModelQueryResult(o)).ToList();

            return new RateSettingFixSaleModelPaging()
            {
                PageOutput = pageOutput,
                RateSettingFixSaleModels = results
            };
        }

        public async Task<List<RateSettingFixSaleTransferModelDTO>> GetRateSettingFixSaleModelProjectListAsync(Guid? ProjectID, DateTime? ActiveDate)
        {
            IQueryable<RateSettingFixSaleModelQueryResult> query = from m in DB.Models
                                                                    .Include(x => x.Project)
                                                                   join rm in DB.RateSettingFixSaleModels on m.ID equals rm.ModelID into g
                                                                   from o in g.DefaultIfEmpty()
                                                                   where (o == null
                                                                                || o.ActiveDate == (DB.RateSettingFixSaleModels.Where(n => n.ModelID == m.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                                   select new RateSettingFixSaleModelQueryResult()
                                                                   {
                                                                       RateSettingFixSaleModel = o ?? new RateSettingFixSaleModel(),
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
                query = query.Where(x => x.RateSettingFixSaleModel.ActiveDate == ActiveDate);
            }
            #endregion

            var results = await query.Select(o => RateSettingFixSaleTransferModelDTO.CreateFromFixSaleModelQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task CreateRateSettingFixSaleModelListAsync(List<RateSettingFixSaleTransferModelDTO> ListInput)
        {
            if (ListInput.Count() > 0)
            {
                var lstRateSettingFixSaleModel = new List<RateSettingFixSaleModel>();
                var lstUpdateRateSettingFixSaleModel = new List<RateSettingFixSaleModel>();

                foreach (var input in ListInput)
                {
                    var model = new RateSettingFixSaleModel();
                    model.ActiveDate = input.ActiveDate;
                    model.ModelID = input.Model.Id;
                    model.ProjectID = input.Project.Id;
                    model.Amount = input.Amount;
                    model.IsActive = true;
                    lstRateSettingFixSaleModel.Add(model);


                    var lstUpdate = await DB.RateSettingFixSaleModels.Where(o => o.ProjectID == input.Project.Id 
                                                                                && o.ModelID == input.Model.Id 
                                                                                && o.ActiveDate <= input.ActiveDate 
                                                                                && o.IsActive == true).ToListAsync();
                    foreach (var update in lstUpdate)
                    {
                        update.IsActive = false;
                        update.IsDeleted = false;

                        lstUpdateRateSettingFixSaleModel.Add(update);
                    }
                }

                DB.RateSettingFixSaleModels.UpdateRange(lstUpdateRateSettingFixSaleModel);
                await DB.RateSettingFixSaleModels.AddRangeAsync(lstRateSettingFixSaleModel);
                await DB.SaveChangesAsync();
            }
        }

        public async Task<RateSettingFixSaleTransferModelDTO> GetRateSettingFixSaleModelAsync(Guid id)
        {
            var model = await DB.RateSettingFixSaleModels.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixSaleModelModel(model);
            return result;
        }

        public async Task<RateSettingFixSaleTransferModelDTO> CreateRateSettingFixSaleModelAsync(RateSettingFixSaleTransferModelDTO input)
        {
            await input.FixSaleModelValidateAsync(DB);

            RateSettingFixSaleModel model = new RateSettingFixSaleModel();
            input.ToFixSaleModelModel(ref model);

            await DB.RateSettingFixSaleModels.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixSaleModelModel(model);
            return result;
        }

        public async Task<RateSettingFixSaleTransferModelDTO> UpdateRateSettingFixSaleModelAsync(Guid id, RateSettingFixSaleTransferModelDTO input)
        {
            await input.FixSaleModelValidateAsync(DB);

            var model = await DB.RateSettingFixSaleModels.Where(o => o.ID == id).FirstAsync();
            input.ToFixSaleModelModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingFixSaleTransferModelDTO.CreateFromFixSaleModelModel(model);
            return result;
        }

        public async Task<RateSettingFixSaleModel> DeleteRateSettingFixSaleModelAsync(Guid id)
        {
            var model = await DB.RateSettingFixSaleModels.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
