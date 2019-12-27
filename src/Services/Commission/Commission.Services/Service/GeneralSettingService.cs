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
    public class GeneralSettingService : IGeneralSettingService
    {
        private readonly DatabaseContext DB;

        public GeneralSettingService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<GeneralSettingPaging> GetGeneralSettingListAsync(GeneralSettingFilter filter, PageParam pageParam, GeneralSettingSortByParam sortByParam)
        {
            IQueryable<GeneralSettingQueryResult> query = DB.GeneralSettings
                                                    .Include(o => o.Project)
                                                  .Select(o => new GeneralSettingQueryResult()
                                                  {
                                                      GeneralSetting = o,
                                                      Project = o.Project,
                                                      CreatedByUser = o.CreatedBy
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.GeneralSetting.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.GeneralSetting.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.GeneralSetting.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.GeneralSetting.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.GeneralSetting.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.GeneralSetting.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.GeneralSetting.Created >= filter.CreateDateFrom && x.GeneralSetting.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.GeneralSetting.IsActive == filter.IsActive);
            }
            #endregion

            GeneralSettingDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<GeneralSettingQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => GeneralSettingDTO.CreateFromQueryResult(o)).ToList();

            return new GeneralSettingPaging()
            {
                PageOutput = pageOutput,
                GeneralSettings = results
            };
        }

        public async Task<GeneralSettingDTO> GetGeneralSettingAsync(Guid id)
        {
            var model = await DB.GeneralSettings.Where(o => o.ID == id).FirstAsync();
            var result = GeneralSettingDTO.CreateFromModel(model);
            return result;
        }

        public async Task CreateGeneralSettingAsync(GeneralSettingDTO input)
        {
            if (input.ListProjectId.Count() > 0)
            {
                var lstGeneralSetting = new List<GeneralSetting>();
                var lstUpdateGeneralSetting = new List<GeneralSetting>();

                foreach (var pjId in input.ListProjectId)
                {
                    var model = new GeneralSetting();
                    model.ActiveDate = DateTime.Now.Date; //input.ActiveDate;
                    model.ProjectID = pjId;
                    model.AfterLaunchAmount = input.AfterLaunchAmount;
                    model.LaunchStartDate = input.LaunchStartDate;
                    model.LaunchEndDate = input.LaunchEndDate;
                    model.IsActive = true;
                    lstGeneralSetting.Add(model);


                    var lstUpdate = await DB.GeneralSettings.Where(o => o.ProjectID == pjId && o.IsActive == true).ToListAsync();
                    foreach (var update in lstUpdate)
                    {
                        update.IsActive = false;

                        lstUpdateGeneralSetting.Add(update);
                    }
                }

                DB.GeneralSettings.UpdateRange(lstUpdateGeneralSetting);
                await DB.GeneralSettings.AddRangeAsync(lstGeneralSetting);
                await DB.SaveChangesAsync();
            }
        }

        public async Task<GeneralSettingDTO> UpdateGeneralSettingAsync(Guid id, GeneralSettingDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.GeneralSettings.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = GeneralSettingDTO.CreateFromModel(model);
            return result;
        }

        public async Task<GeneralSetting> DeleteGeneralSettingAsync(Guid id)
        {
            var model = await DB.GeneralSettings.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
    }
}
