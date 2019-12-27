using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class FloorService : IFloorService
    {
        private readonly DatabaseContext DB;

        public FloorService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<FloorDropdownDTO>> GetFloorDropdownListAsync(Guid projectID, Guid? towerID, string name)
        {
            IQueryable<Floor> query = DB.Floors.Where(x => x.ProjectID == projectID);
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }
            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();
            var results = queryResults.Select(o => FloorDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
        public async Task<FloorPaging> GetFloorListAsync(Guid projectID, Guid towerID, FloorsFilter filter, PageParam pageParam, FloorSortByParam sortByParam)
        {
            IQueryable<FloorQueryResult> query = DB.Floors.Where(o => o.TowerID == towerID).Select(o => new FloorQueryResult
            {
                Floor = o,
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrWhiteSpace(filter.NameTH))
            {
                query = query.Where(x => x.Floor.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEN))
            {
                query = query.Where(x => x.Floor.NameEN.Contains(filter.NameEN));
            }
            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(x => x.Floor.Description.Contains(filter.Description));
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Floor.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Floor.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Floor.Updated >= filter.UpdatedFrom && x.Floor.Updated <= filter.UpdatedTo);
            }
            #endregion

            FloorDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<FloorQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => FloorDTO.CreateFromQueryResult(o)).ToList();

            return new FloorPaging()
            {
                PageOutput = pageOutput,
                Floors = results
            };
        }

        public async Task<FloorDTO> GetFloorAsync(Guid projectID, Guid towerID, Guid id)
        {
            var model = await DB.Floors.Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id).FirstAsync();
            var result = FloorDTO.CreateFromModel(model);
            return result;
        }

        public async Task<FloorDTO> CreateFloorAsync(Guid projectID, Guid towerID, FloorDTO input)
        {
            await this.ValidateFloor(projectID, towerID, input);

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            Floor model = new Floor();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.TowerID = towerID;

            await DB.Floors.AddAsync(model);
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetFloorAsync(projectID, towerID, model.ID);
            return result;
        }

        public async Task<List<FloorDTO>> CreateMultipleFloorAsync(Guid projectID, Guid towerID, CreateMultipleFloorInput input)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            List<FloorDTO> result = new List<FloorDTO>();
            for (var i = input.From; i <= input.To; i++)
            {
                Floor model = new Floor();
                model.ProjectID = projectID;
                model.TowerID = towerID;
                model.NameEN = i.ToString("00");
                model.NameTH = i.ToString("00");
                await DB.Floors.AddAsync(model);
                await DB.SaveChangesAsync();
                result.Add(await this.GetFloorAsync(projectID, towerID, model.ID));
            }
            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return result;
        }

        public async Task<FloorDTO> UpdateFloorAsync(Guid projectID, Guid towerID, Guid id, FloorDTO input)
        {
            await this.ValidateFloor(projectID, towerID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id).FirstAsync();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.TowerID = towerID;

            DB.Entry(model).State = EntityState.Modified;

            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            var result = FloorDTO.CreateFromModel(model);
            return result;
        }

        public async Task<Floor> DeleteFloorAsync(Guid projectID, Guid towerID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();

            var towerDataStatusMasterCenterID = await this.TowerDataStatus(projectID);
            project.TowerDataStatusMasterCenterID = towerDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> TowerDataStatus(Guid projectID)
        {
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var towerDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var towerDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();

            var towerDataStatusMasterCenterID = towerDataStatusPrepareMasterCenterID;

            if (towers.Count() == 0 || floors.Count() == 0)
            {
                return towerDataStatusMasterCenterID;
            }

            if (towers.TrueForAll(o =>
                     !string.IsNullOrEmpty(o.TowerCode)
                     && !string.IsNullOrEmpty(o.TowerNoTH)
                     && !string.IsNullOrEmpty(o.TowerNoEN)
                     && !string.IsNullOrEmpty(o.CondominiumName)
                     && !string.IsNullOrEmpty(o.CondominiumNo)
                    )
                    && floors.TrueForAll(o =>
                        !string.IsNullOrEmpty(o.NameTH)
                      && !string.IsNullOrEmpty(o.NameEN)
                       )
              )
            {

                towerDataStatusMasterCenterID = towerDataStatusSaleMasterCenterID;
            }
            return towerDataStatusMasterCenterID;
        }

        private async Task ValidateFloor(Guid projectID, Guid towerID, FloorDTO input)
        {
            ValidateException ex = new ValidateException();
            //validate unique
            if (!string.IsNullOrEmpty(input.NameTH))
            {
                if (!input.NameTH.CheckLang(true, true, false, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0031").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameTH = input.Id != (Guid?)null
               ? await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID != input.Id && o.NameTH == input.NameTH).CountAsync() > 0
               : await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.NameTH == input.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NameEN))
            {
                if (!input.NameEN.CheckLang(false, true, false, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameEN = input.Id != (Guid?)null
               ? await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.ID != input.Id && o.NameTH == input.NameEN).CountAsync() > 0
               : await DB.Floors.Where(o => o.ProjectID == projectID && o.TowerID == towerID && o.NameTH == input.NameEN).CountAsync() > 0;
                if (checkUniqueNameEN)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(FloorDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameEN);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}
