using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Project.Params.Filters;
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
    public class ModelService : IModelService
    {
        private readonly DatabaseContext DB;

        public ModelService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null)
        {
            IQueryable<Database.Models.PRJ.Model> query = DB.Models.AsQueryable();
            if (projectID != null)
            {
                query = query.Where(x => x.ProjectID == projectID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var queryResults = await query.OrderBy(o => o.NameTH).Take(100).ToListAsync();
            var results = queryResults.Select(o => ModelDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<ModelPaging> GetModelListAsync(Guid projectID, ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam)
        {
            IQueryable<ModelQueryResult> query = DB.Models.Where(o => o.ProjectID == projectID).Select(o => new ModelQueryResult
            {
                Model = o,
                ModelShortName = o.ModelShortName,
                ModelType = o.ModelType,
                ModelUnitType = o.ModelUnitType,
                TypeOfRealEstate = o.TypeOfRealEstate,
                WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(b => b.ModelID == o.ID).OrderByDescending(b => b.Version).FirstOrDefault(),
                UpdatedBy = o.UpdatedBy
            });

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Model.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Model.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Model.NameEN.Contains(filter.NameEN));
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(x => x.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }
            if (!string.IsNullOrEmpty(filter.ModelShortNameKey))
            {
                var modelShortNameKeyID = await DB.MasterCenters.Where(x => x.Key == filter.ModelShortNameKey
                                                                 && x.MasterCenterGroupKey == "ModelShortName")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Model.ModelShortNameMasterCenterID == modelShortNameKeyID);
            }
            if (!string.IsNullOrEmpty(filter.ModelTypeKey))
            {
                var modelTypeID = await DB.MasterCenters.Where(x => x.Key == filter.ModelTypeKey
                                                                && x.MasterCenterGroupKey == "ModelType")
                                                               .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Model.ModelTypeMasterCenterID == modelTypeID);
            }
            if (!string.IsNullOrEmpty(filter.ModelUnitTypeKey))
            {
                var modelUnitTypeID = await DB.MasterCenters.Where(x => x.Key == filter.ModelUnitTypeKey
                                                                 && x.MasterCenterGroupKey == "ModelUnitType")
                                                                .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Model.ModelUnitTypeMasterCenterID == modelUnitTypeID);
            }
            if (filter.ElectricMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom);
            }
            if (filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.ElectricMeterPriceFrom != null && filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom
                                   && x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom);
            }
            if (filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null && filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom
                                   && x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom && x.Model.Updated <= filter.UpdatedTo);
            }
            #endregion

            ModelListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ModelQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => ModelListDTO.CreateFromQueryResult(o)).ToList();

            return new ModelPaging()
            {
                PageOutput = pageOutput,
                Models = results
            };
        }

        public async Task<ModelDTO> GetModelAsync(Guid projectID, Guid id)
        {
            var model = await DB.Models.Where(o => o.ID == id)
                                       .Include(o => o.ModelShortName)
                                       .Include(o => o.ModelUnitType)
                                       .Include(o => o.ModelType)
                                       .Include(o => o.TypeOfRealEstate)
                                       .Include(o => o.UpdatedBy)
                                       .FirstAsync();

            var waterElectricMeterPriceList = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID)
                                             .OrderBy(o => o.Version)
                                             .Include(o => o.UpdatedBy)
                                             .Select(o => WaterElectricMeterPriceDTO.CreateFromModel(o))
                                             .ToListAsync();

            waterElectricMeterPriceList = waterElectricMeterPriceList.Count == 0 ? null : waterElectricMeterPriceList;

            var result = ModelDTO.CreateFromModel(model, waterElectricMeterPriceList);
            return result;
        }

        public async Task<ModelDTO> CreateModelAsync(Guid projectID, ModelDTO input)
        {
            await input.ValidateAsync(DB);
            await this.ValidateModel(projectID, input);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            Model model = new Model();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            var key = project.ProjectNo;
            var type = "PRJ.Model";
            var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                model.Code = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                model.Code = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }

            var listWaterElectricMeterPriceAdd = new List<WaterElectricMeterPrice>();

            if (input.WaterElectricMeterPrices != null)
            {
                if (input.WaterElectricMeterPrices.Count > 0)
                {
                    var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                    foreach (var item in input.WaterElectricMeterPrices)
                    {
                        var existingItem = allWaterElectricMeterPrice.Where(o => o.ID == item.Id).FirstOrDefault();
                        if (existingItem == null)
                        {
                            var version = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefaultAsync();
                            WaterElectricMeterPrice newmodel = new WaterElectricMeterPrice();
                            item.ToModel(ref newmodel);
                            newmodel.ModelID = model.ID;
                            if (version == null && listWaterElectricMeterPriceAdd.Count == 0)
                            {
                                newmodel.Version = version == null ? 1 : version + 1;
                            }
                            else if (listWaterElectricMeterPriceAdd.Count != 0)
                            {
                                var newversion = listWaterElectricMeterPriceAdd.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefault();
                                newmodel.Version = newversion == null ? 1 : newversion + 1;
                            }
                            else
                            {
                                newmodel.Version = version + 1;
                            }
                            listWaterElectricMeterPriceAdd.Add(newmodel);
                        }
                    }
                }
            }

            await DB.Models.AddAsync(model);
            await DB.AddRangeAsync(listWaterElectricMeterPriceAdd);
            await DB.SaveChangesAsync();

            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetModelAsync(projectID, model.ID);
            return result;
        }

        public async Task<ModelDTO> UpdateModelAsync(Guid projectID, Guid id, ModelDTO input)
        {
            await input.ValidateAsync(DB);
            await this.ValidateModel(projectID, input);

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Models.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);
            model.ProjectID = projectID;



            var listWaterElectricMeterPriceAdd = new List<WaterElectricMeterPrice>();
            var listWaterElectricMeterPriceUpdate = new List<WaterElectricMeterPrice>();
            var listWaterElectricMeterPriceDelete = new List<WaterElectricMeterPrice>();
            if (input.WaterElectricMeterPrices != null)
            {
                if (input.WaterElectricMeterPrices.Count > 0)
                {
                    var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                    foreach (var item in input.WaterElectricMeterPrices)
                    {
                        var existingItem = allWaterElectricMeterPrice.Where(o => o.ID == item.Id).FirstOrDefault();
                        if (existingItem == null)
                        {
                            var version = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefaultAsync();
                            WaterElectricMeterPrice newmodel = new WaterElectricMeterPrice();
                            item.ToModel(ref newmodel);
                            newmodel.ModelID = model.ID;
                            if (version == null && listWaterElectricMeterPriceAdd.Count == 0)
                            {
                                newmodel.Version = version == null ? 1 : version + 1;
                            }
                            else if (listWaterElectricMeterPriceAdd.Count != 0)
                            {
                                var newversion = listWaterElectricMeterPriceAdd.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefault();
                                newmodel.Version = newversion == null ? 1 : newversion + 1;
                            }
                            else
                            {
                                newmodel.Version = version + 1;
                            }
                            listWaterElectricMeterPriceAdd.Add(newmodel);
                        }
                        else
                        {
                            item.ToModel(ref existingItem);
                            listWaterElectricMeterPriceUpdate.Add(existingItem);
                        }
                    }
                    foreach (var item in allWaterElectricMeterPrice)
                    {
                        var existingInput = input.WaterElectricMeterPrices.Where(o => o.Id == item.ID).FirstOrDefault();
                        if (existingInput == null)
                        {
                            item.IsDeleted = true;
                            listWaterElectricMeterPriceDelete.Add(item);
                        }
                    }
                }
            }
            else
            {
                var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                foreach (var item in allWaterElectricMeterPrice)
                {
                    var existingInput = input.WaterElectricMeterPrices.Where(o => o.Id == item.ID).FirstOrDefault();
                    if (existingInput == null)
                    {
                        item.IsDeleted = true;
                        listWaterElectricMeterPriceDelete.Add(item);
                    }
                }
            }
            DB.Entry(model).State = EntityState.Modified;
            DB.UpdateRange(listWaterElectricMeterPriceUpdate);
            DB.UpdateRange(listWaterElectricMeterPriceDelete);
            await DB.AddRangeAsync(listWaterElectricMeterPriceAdd);
            await DB.SaveChangesAsync();

            var reversions = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
            var reversion = 1;
            foreach (var item in reversions.OrderBy(o => o.Created))
            {
                item.Version = reversion;
                reversion++;
            }
            await DB.SaveChangesAsync();

            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetModelAsync(projectID, model.ID);

            return result;
        }

        public async Task<Model> DeleteModelAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Models.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();


            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> ModelDataStatus(Guid projectID)
        {
            var modelDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
            var modelDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var allModel = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
            var modelID = allModel.Select(o => (Guid?)o.ID).ToList();
            var allWaterElectricMeterPrices = await DB.WaterElectricMeterPrices.Where(o => modelID.Contains(o.ModelID)).ToListAsync();

            var modelDataStatusMasterCenterID = modelDataStatusPrepareMasterCenterID;

            if (allModel.Count() == 0 || allWaterElectricMeterPrices.Count() == 0)
            {
                return modelDataStatusPrepareMasterCenterID;
            }

            if (allModel.TrueForAll(o =>
                     !string.IsNullOrEmpty(o.Code)
                     && !string.IsNullOrEmpty(o.NameTH)
                     && !string.IsNullOrEmpty(o.NameEN)
                     && o.ModelUnitTypeMasterCenterID != null
                     && o.TypeOfRealEstateID != null
                            )
                     && allWaterElectricMeterPrices.TrueForAll(o =>
                                 o.Version != null
                                 && o.WaterMeterPrice != null
                                 && o.WaterMeterSize != null
                                 && o.ElectricMeterPrice != null
                                 && o.ElectricMeterSize != null
                                                        )
                )
            {
                modelDataStatusMasterCenterID = modelDataStatusSaleMasterCenterID;
            }
            else
            {
                modelDataStatusMasterCenterID = modelDataStatusPrepareMasterCenterID;
            }
            return modelDataStatusMasterCenterID;
        }

        private async Task ValidateModel(Guid projectID, ModelDTO input)
        {
            ValidateException ex = new ValidateException();
            //validate unique
            if (!string.IsNullOrEmpty(input.NameTH))
            {
                var checkUniqueNameTH = input.Id != (Guid?)null
               ? await DB.Models.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.NameTH == input.NameTH).CountAsync() > 0
               : await DB.Models.Where(o => o.ProjectID == projectID && o.NameTH == input.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(ModelDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NameEN))
            {
                var checkUniqueNameEN = input.Id != (Guid?)null
               ? await DB.Models.Where(o => o.ProjectID == projectID && o.ID != input.Id && o.NameEN == input.NameEN).CountAsync() > 0
               : await DB.Models.Where(o => o.ProjectID == projectID && o.NameEN == input.NameEN).CountAsync() > 0;
                if (checkUniqueNameEN)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = input.GetType().GetProperty(nameof(ModelDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
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
