using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using FileStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Project.Services.Excels;
using Project.Services.Sap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class UnitService : IUnitService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        public UnitService(IConfiguration configuration, DatabaseContext db)
        {
            this.DB = db;
            this.Configuration = configuration;
            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioWithSSL = Configuration["Minio:WithSSL"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, minioWithSSL == "true");
        }

        public async Task<List<UnitDropdownDTO>> GetUnitDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null)
        {
            IQueryable<Unit> query = DB.Units.Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.UnitNo.Contains(name));
            }
            if (towerID != null)
            {
                query = query.Where(o => o.TowerID == towerID);
            }
            if (floorID != null)
            {
                query = query.Where(o => o.FloorID == floorID);
            }
            if (!string.IsNullOrEmpty(unitStatusKey))
            {
                query = query.Where(o => o.UnitStatus.Key == unitStatusKey);
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).Take(100).OrderBy(x => x.UnitNo).ToListAsync();
            var results = queryResults.Select(o => UnitDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }

        public async Task<List<UnitDropdownSellPriceDTO>> GetUnitDropdownWithSellPriceListAsync(Guid projectID, string name, string unitStatusKey = null)
        {
            IQueryable<Unit> query = DB.Units.Where(x => x.ProjectID == projectID);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.UnitNo.Contains(name));
            }
            if (!string.IsNullOrEmpty(unitStatusKey))
            {
                query = query.Where(o => o.UnitStatus.Key == unitStatusKey);
            }

            var queryResults = await query.OrderBy(o => o.UnitNo).Take(50).OrderBy(x => x.UnitNo).ToListAsync();
            var results = queryResults.Select(async o => await UnitDropdownSellPriceDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

            return results;
        }

        public async Task<UnitPaging> GetUnitListAsync(Guid projectID, UnitFilter filter, PageParam pageParam, UnitListSortByParam sortByParam)
        {
            var query = from o in DB.Units
                        .Include(o => o.Model)
                        .ThenInclude(o => o.TypeOfRealEstate)
                        where o.ProjectID == projectID
                        join t in DB.TitledeedDetails on o.ID equals t.UnitID into t2
                        from td in t2.DefaultIfEmpty()
                        select new UnitQueryResult
                        {
                            Model = o.Model,
                            Floor = o.Floor,
                            UnitDirection = o.UnitDirection,
                            Tower = o.Tower,
                            Unit = o,
                            TitledeedDetail = td,
                            UnitStatus = o.UnitStatus,
                            UnitType = o.UnitType,
                            UpdatedBy = o.UpdatedBy
                        };
            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (!string.IsNullOrEmpty(filter.ModelCode))
            {
                query = query.Where(x => x.Model.Code.Contains(filter.ModelCode));
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(o => o.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }
            if (!string.IsNullOrEmpty(filter.ModelName))
            {
                query = query.Where(o => o.Model.NameTH.Contains(filter.ModelName));
            }
            if (filter.TowerID != null && filter.TowerID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.TowerID == filter.TowerID);
            }
            if (filter.FloorID != null && filter.FloorID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.FloorID == filter.FloorID);
            }
            if (!string.IsNullOrEmpty(filter.UnitDirectionKey))
            {
                var unitDirectionMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitDirectionKey
                                                                       && x.MasterCenterGroupKey == "UnitDirection")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitDirectionMasterCenterID == unitDirectionMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UnitTypeKey))
            {
                var unitTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitTypeKey
                                                                       && x.MasterCenterGroupKey == "UnitType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitTypeMasterCenterID == unitTypeMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }

            if (filter.SaleAreaFrom != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom);
            }
            if (filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea <= filter.SaleAreaTo);
            }
            if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom
                                    && x.Unit.SaleArea <= filter.SaleAreaTo);
            }
            if (filter.TitleDeedAreaFrom != null)
            {
                query = query.Where(o => (o.Unit.TitledeedDetails.FirstOrDefault() != null ? o.Unit.TitledeedDetails.FirstOrDefault().TitledeedArea : (double?)0) >= filter.TitleDeedAreaFrom);
            }
            if (filter.TitleDeedAreaTo != null)
            {
                query = query.Where(o => (o.Unit.TitledeedDetails.FirstOrDefault() != null ? o.Unit.TitledeedDetails.FirstOrDefault().TitledeedArea : (double?)0) <= filter.TitleDeedAreaTo);
            }


            #endregion

            UnitDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<UnitQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => UnitDTO.CreateFromQueryResult(o)).ToList();

            return new UnitPaging()
            {
                PageOutput = pageOutput,
                Units = results
            };

        }

        public async Task<UnitDTO> GetUnitAsync(Guid projectID, Guid id)
        {
            var model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id)
                .Include(o => o.Model).ThenInclude(o => o.TypeOfRealEstate)
                                      .Include(o => o.Model)
                                      .Include(o => o.Floor)
                                      .Include(o => o.UnitDirection)
                                      .Include(o => o.Tower)
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.UnitType)
                                      .Include(o => o.UpdatedBy)
                                      .Include(o => o.TitledeedDetails)
                                      .FirstAsync();


            #region ImageName
            var floorPlan = await DB.FloorPlanImages.Where(o => o.Name == model.FloorPlanFileName)
                                                .Select(o => FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                                                .Select(o => o.Result)
                                                .FirstOrDefaultAsync();
            var roomPlan = await DB.RoomPlanImages.Where(o => o.Name == model.FloorPlanFileName)
                                                .Select(o => RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
                                                .Select(o => o.Result)
                                                .FirstOrDefaultAsync();
            #endregion
            if (model.TitledeedDetails?.FirstOrDefault() == null)
            {
                TitledeedDetail titledeedDetail = new TitledeedDetail();
                titledeedDetail.ProjectID = model.ProjectID;
                titledeedDetail.UnitID = model.ID;

                await DB.TitledeedDetails.AddAsync(titledeedDetail);
                await DB.SaveChangesAsync();

                model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id)
                                      .Include(o => o.Model)
                                      .Include(o => o.Floor)
                                      .Include(o => o.UnitDirection)
                                      .Include(o => o.Tower)
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.UnitType)
                                      .Include(o => o.UpdatedBy)
                                      .Include(o => o.TitledeedDetails)
                                      .FirstAsync();
            }

            var result = UnitDTO.CreateFromModel(model, floorPlan, roomPlan);
            return result;
        }

        public async Task<UnitDTO> CreateUnitAsync(Guid projectID, UnitDTO input)
        {
            await input.ValidateAsync(DB);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            Unit model = new Unit();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.Units.AddAsync(model);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await this.UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetUnitAsync(projectID, model.ID);
            return result;
        }

        public async Task<UnitDTO> UpdateUnitAsync(Guid projectID, Guid id, UnitDTO input)
        {
            await input.ValidateAsync(DB);
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var titleDeed = await DB.TitledeedDetails.Where(o => o.UnitID == model.ID).FirstOrDefaultAsync();
            if (input.TitleDeed?.TitledeedArea != null && titleDeed.TitledeedArea != input.TitleDeed?.TitledeedArea)
            {
                titleDeed.TitledeedArea = input.TitleDeed.TitledeedArea;
                DB.Entry(titleDeed).State = EntityState.Modified;
            }

            var unitDataStatusMasterCenterID = await this.UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetUnitAsync(projectID, id);
            return result;
        }

        public async Task<Unit> DeleteUnitAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Units.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await this.UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<UnitInfoDTO> GetUnitInfoAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            var model = await DB.Units.Include(o => o.Model)
                                      .Include(o => o.Floor)
                                      .Include(o => o.UnitDirection)
                                      .Include(o => o.Tower)
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.UnitType)
                                      .Include(o => o.TitledeedDetails)
                                      .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            var result = UnitInfoDTO.CreateFromModel(model);
            return result;
        }

        public async Task<UnitInitialExcelDTO> ImportUnitInitialAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // String Format (Eng And Number)
            var err0064 = await DB.ErrorMessages.Where(o => o.Key == "ERR0064").FirstAsync();
            // Decimal Format 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();

            var rowErrors = new List<string>();

            var checkNullWbsCodes = new List<string>();
            var checkNullObjectCodes = new List<string>();
            var checkFormatObjectCodes = new List<string>();
            var checkFormatSaleArea = new List<string>();

            var result = new UnitInitialExcelDTO();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 9)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var checkFormatUnitArea = new List<string>();
            var unitExcels = new List<UnitInitialExcelModel>();
            var row = 1;
            var error = 0;
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;

                var excelModel = UnitInitialExcelModel.CreateFromDataRow(r);

                #region Validate
                if (!string.IsNullOrEmpty(r[UnitInitialExcelModel._saleAreaIndex].ToString()))
                {
                    if (!r[UnitInitialExcelModel._saleAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatSaleArea.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.SAPWBSNo))
                {
                    checkNullWbsCodes.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.SAPWBSObject))
                {
                    checkNullObjectCodes.Add((row + 1).ToString());
                    isError = true;
                }
                else
                {
                    if (!excelModel.SAPWBSObject.CheckLang(false, true, false, false))
                    {
                        checkFormatObjectCodes.Add((row + 1).ToString());
                        isError = true;
                    }
                }

                if (isError)
                {
                    error++;
                }
                #endregion

                unitExcels.Add(excelModel);

                row++;
            }

            List<Unit> addingUnits = new List<Unit>();
            List<Unit> updatingUnits = new List<Unit>();
            List<Unit> deletingUnits = new List<Unit>();

            List<Floor> addingFloors = new List<Floor>();
            List<Tower> addingTowers = new List<Tower>();

            var units = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var project = await (from p in DB.Projects.Include(o => o.ProductType)
                                 where p.ID == projectID
                                 select p).FirstAsync();
            #region Validate ProjectCode
            ValidateException ex = new ValidateException();
            if (unitExcels.Any(o => o.ProjectSapCode != project.SapCode))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "PROJECTCODE");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            #region Add Result Validate
            if (checkNullWbsCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullObjectCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatObjectCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0064.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0064.Message.Replace("[column]", "OBJECTCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatObjectCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatSaleArea.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "AREA UNIT");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleArea));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "AREA UNIT");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleArea));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            rowErrors.AddRange(checkNullWbsCodes);
            rowErrors.AddRange(checkNullObjectCodes);
            rowErrors.AddRange(checkFormatObjectCodes);
            rowErrors.AddRange(checkFormatSaleArea);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            var towers = await (from t in DB.Towers where t.ProjectID == projectID select t).ToListAsync();
            var floors = await (from f in DB.Floors where f.ProjectID == projectID select f).ToListAsync();
            var unitStatuses = await (from us in DB.MasterCenters where us.MasterCenterGroupKey == "UnitStatus" select us).ToListAsync();
            row = 2;
            foreach (var item in unitExcels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    //invalid project
                    if (item.ProjectSapCode != project.SapCode)
                    {
                        continue;
                    }

                    bool isAddNew = false;
                    if (!string.IsNullOrEmpty(item.SAPWBSNo))
                    {
                        var unit = units.FirstOrDefault(o => o.ProjectID == projectID && o.SAPWBSNo == item.SAPWBSNo);
                        if (unit == null)
                        {
                            unit = new Unit()
                            {
                                ProjectID = projectID
                            };
                            unit.UnitStatusMasterCenterID = unitStatuses.Where(o => o.Key == UnitStatusKeys.Available).Select(o => o.ID).First();
                            isAddNew = true;
                        }
                        else
                        {
                            //if not not available then do nothing
                            if (UnitStatusKeys.IsSold(unit.UnitStatus?.Key))
                            {
                                continue;
                            }
                        }
                        item.ToModel(ref unit);

                        if (project.ProductType.Key == ProductTypeKeys.HighRise && unit.SAPWBSNo.Length > 15)
                        {
                            var floorNames = new List<string>();
                            var towerCode = item.SAPWBSNo.Substring(12, 1);
                            var tower = towers.FirstOrDefault(o => o.TowerCode == towerCode && o.ProjectID == projectID);
                            if (tower == null)
                            {
                                tower = addingTowers.FirstOrDefault(o => o.TowerCode == towerCode && o.ProjectID == projectID);
                                if (tower == null)
                                {
                                    tower = new Tower() { TowerCode = towerCode, ProjectID = projectID };
                                    addingTowers.Add(tower);
                                }
                            }
                            unit.TowerID = tower.ID;
                            var floorName = item.SAPWBSNo.Substring(13, 2);
                            int floorInt;
                            if (int.TryParse(floorName, out floorInt))
                            {
                                //floorName = floorInt.ToString();
                                for (int i = 1; i <= floorInt; i++)
                                {
                                    floorNames.Add(i.ToString("00"));
                                }
                            }
                            foreach (var name in floorNames)
                            {
                                var floor = floors.FirstOrDefault(o => o.NameEN == name && o.TowerID == tower.ID);
                                if (floor == null)
                                {
                                    floor = addingFloors.FirstOrDefault(o => o.NameEN == name && o.TowerID == tower.ID);
                                    if (floor == null)
                                    {
                                        floor = new Floor() { NameEN = name, NameTH = name, TowerID = tower.ID, ProjectID = projectID };
                                        addingFloors.Add(floor);
                                    }
                                }
                            }
                            var floorID = addingFloors.Where(o => o.NameEN == floorName && o.TowerID == tower.ID).Select(o => o.ID).FirstOrDefault() == Guid.Empty ? floors.Where(o => o.NameEN == floorName && o.TowerID == tower.ID).Select(o => o.ID).FirstOrDefault() : addingFloors.Where(o => o.NameEN == floorName && o.TowerID == tower.ID).Select(o => o.ID).FirstOrDefault();
                            unit.FloorID = floorID;
                        }

                        if (isAddNew)
                        {
                            addingUnits.Add(unit);
                        }
                        else
                        {
                            updatingUnits.Add(unit);
                        }
                    }
                }
                row++;
            }

            foreach (var dbItem in units)
            {
                var existingInput = unitExcels.Where(o => o.SAPWBSNo == dbItem.SAPWBSNo).FirstOrDefault();
                if (existingInput == null)
                {
                    deletingUnits.Add(dbItem);
                    dbItem.IsDeleted = true;
                }
            }

            DB.AddRange(addingTowers);
            DB.AddRange(addingFloors);
            DB.AddRange(addingUnits);
            DB.UpdateRange(updatingUnits);
            DB.UpdateRange(deletingUnits);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await this.UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            result.Success = addingUnits.Count() + updatingUnits.Count();
            result.Error = error;
            result.Delete = deletingUnits.Count();
            //result.CreateTowerCount = addingTowers.Count;
            //result.CreateFloorCount = addingFloors.Count;
            //result.CreateUnitCount = addingUnits.Count;
            //result.UpdateUnitCount = updatingUnits.Count;
            //result.DeleteUnitCount = deletingUnits.Count;
            //result.CreateUnitSapWbsNos = addingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.UpdateUnitSapWbsNos = updatingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.DeleteUnitSapWbsNos = deletingUnits.Select(o => o.SAPWBSNo).ToList();
            //result.CreateTowerCodes = addingTowers.Select(o => o.TowerCode).ToList();
            //result.CreateFloorNames = addingFloors.Select(o => o.NameEN).ToList();
            return result;
        }

        public async Task<DataTable> ConvertExcelInitialToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            ////Stream ddd = new MemoryStream(test);

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
        }

        public async Task<UnitGeneralExcelDTO> ImportUnitGeneralAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // 1 2 3 4 5
            var err0067 = await DB.ErrorMessages.Where(o => o.Key == "ERR0067").FirstAsync();
            // Direction 
            var err0066 = await DB.ErrorMessages.Where(o => o.Key == "ERR0066").FirstAsync();

            var dt = await this.ConvertExcelGeneralToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 18)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var unitExcelModels = new List<UnitExcelModel>();

            var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
            var assetTypes = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "AssetType").ToListAsync();
            var unitDirections = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitDirection").ToListAsync();
            var towers = await DB.Towers.Where(o => o.ProjectID == projectID).ToListAsync();
            var floors = await DB.Floors.Where(o => o.ProjectID == projectID).ToListAsync();
            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType)
                                                                         .Include(o => o.Brand)
                                                                         .ThenInclude(o => o.UnitNumberFormat)
                                                                         .FirstAsync();
            #region Valdiate
            var row = 4;
            var error = 0;
            var formatUnitdirections = new List<string> { "n", "e", "w", "s", "ne", "nw", "se", "sw" };
            var formatAssetTypes = new List<string> { "1", "2", "3", "4", "5" };
            var checkNullWBSCodes = new List<string>();
            var checkNullWBSObjects = new List<string>();
            var checkNullModelNames = new List<string>();
            var checkNullAssetTypes = new List<string>();
            var checkNullSaleAreas = new List<string>();

            var modelNotFounds = new List<string>();

            //var checkFormatHighLocations = new List<string>();
            var checkFormatUnitDirections = new List<string>();
            var checkFormatAssertTypes = new List<string>();
            var checkFormatTitleDeedAreas = new List<string>();
            var checkFormatNumberOfPrivileges = new List<string>();
            var checkFormatNumberOfParkingFixs = new List<string>();
            var checkFormatNumberOfParkingUnFixs = new List<string>();


            var rowErrors = new List<string>();
            #endregion

            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitExcelModel.CreateFromDataRow(r);
                unitExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWBSCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSObjectCode))
                {
                    checkNullWBSObjects.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.ModelName))
                {
                    checkNullModelNames.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var model = models.Where(o => o.NameEN == excelModel.ModelName).FirstOrDefault();
                    if (model == null)
                    {
                        modelNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.AssetType))
                {
                    checkNullAssetTypes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(r[UnitExcelModel._saleAreaIndex].ToString()))
                {
                    checkNullSaleAreas.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.UnitDirection))
                {
                    if (!formatUnitdirections.Contains(excelModel.UnitDirection.ToLower()))
                    {
                        checkFormatUnitDirections.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.AssetType))
                {
                    if (!formatAssetTypes.Contains(excelModel.AssetType))
                    {
                        checkFormatAssertTypes.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._titledeedArea].ToString()))
                {
                    if (!r[UnitExcelModel._titledeedArea].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatTitleDeedAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (project.ProductType.Key == ProductTypeKeys.LowRise)
                {
                    if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfPrivilegeIndex].ToString()))
                    {
                        if (!r[UnitExcelModel._numberOfPrivilegeIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                        {
                            checkFormatNumberOfPrivileges.Add((row).ToString());
                            isError = true;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfParkingFixIndex].ToString()))
                {
                    if (!r[UnitExcelModel._numberOfParkingFixIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatNumberOfParkingFixs.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitExcelModel._numberOfParkingUnFixIndex].ToString()))
                {
                    if (!r[UnitExcelModel._numberOfParkingUnFixIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatNumberOfParkingUnFixs.Add((row).ToString());
                        isError = true;
                    }
                }
                if (isError)
                {
                    error++;
                }
                #endregion

                row++;
            }
            UnitGeneralExcelDTO result = new UnitGeneralExcelDTO() { Error = 0, Success = 0, ErrorMessages = new List<string>() };

            ValidateException ex = new ValidateException();
            if (project.Brand?.UnitNumberFormatMasterCenterID == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0054").FirstAsync();
                string value = project.Brand?.Name;
                var msg = errMsg.Message.Replace("[value]", value);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            ex = new ValidateException();
            if (unitExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ CRM");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            ex = new ValidateException();
            if (unitExcelModels.Any(o => o.ProjectCode != project.SapCode))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "ProjectCode");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }


            //ex = new ValidateException();
            //foreach (var item in unitExcelModels)
            //{
            //    if (!string.IsNullOrEmpty(item.ModelName))
            //    {
            //        var checkModel = models.Where(o => o.NameEN == item.ModelName).FirstOrDefault();
            //        if (checkModel == null)
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
            //            string value = "ไม่พบข้อมูลแบบบ้าน " + item.ModelName + "ในโครงการ ";
            //            var msg = errMsg.Message.Replace("[message]", value);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //    }
            //    else
            //    {
            //        item.ModelName = null;
            //    }
            //}

            //if (ex.HasError)
            //{
            //    throw ex;
            //}

            #region Add Result Validation

            #region Required

            if (checkNullWBSCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullWBSObjects.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ObjectCode");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSObjects));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ObjectCode");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSObjects));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullModelNames.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", checkNullModelNames));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", checkNullModelNames));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullAssetTypes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAssetTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAssetTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullSaleAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่ขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่ขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Direction
            if (checkFormatUnitDirections.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0066.Message.Replace("[column]", "ทิศ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatUnitDirections));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0066.Message.Replace("[column]", "ทิศ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatUnitDirections));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Format 1,2,3,4,5
            if (checkFormatAssertTypes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0067.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAssertTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0067.Message.Replace("[column]", "AssertType (1,2,3,4,5)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAssertTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Decimal 2 Digit
            if (checkFormatTitleDeedAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่โฉนด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่โฉนด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfPrivileges.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาบุริมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfPrivileges));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาบุริมสิทธิ์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfPrivileges));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfParkingFixs.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingFixs));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingFixs));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatNumberOfParkingUnFixs.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถไม่ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingUnFixs));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "จำนวนที่จอดรถไม่ Fix");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatNumberOfParkingUnFixs));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region NotFound
            if (modelNotFounds.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", modelNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "ชื่อแบบบ้าน");
                    msg = msg.Replace("[row]", String.Join(",", modelNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            rowErrors.AddRange(checkNullWBSCodes);
            rowErrors.AddRange(checkNullWBSObjects);
            rowErrors.AddRange(checkNullModelNames);
            rowErrors.AddRange(checkNullAssetTypes);
            rowErrors.AddRange(checkNullSaleAreas);
            rowErrors.AddRange(modelNotFounds);
            rowErrors.AddRange(checkFormatUnitDirections);
            rowErrors.AddRange(checkFormatAssertTypes);
            rowErrors.AddRange(checkFormatTitleDeedAreas);
            rowErrors.AddRange(checkFormatNumberOfPrivileges);
            rowErrors.AddRange(checkFormatNumberOfParkingFixs);
            rowErrors.AddRange(checkFormatNumberOfParkingUnFixs);

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            #endregion

            #endregion
            row = 4;
            List<Unit> unitsUpdate = new List<Unit>();
            foreach (var item in unitExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var existingUnit = units.Where(o => o.SAPWBSNo == item.WBSNo).FirstOrDefault();
                    if (existingUnit != null)
                    {
                        item.ToModel(ref existingUnit, project.ProductType.Key == ProductTypeKeys.LowRise ? true : false);
                        existingUnit.ProjectID = projectID;
                        //calculate UnitNo
                        var productTypeKey = project.ProductType?.Key;
                        var unitNumberFormat = project.Brand?.UnitNumberFormat?.Key;
                        var splitWbsNo = item.WBSNo.Split("-").ToList();
                        if (productTypeKey == "1" && unitNumberFormat == "1")
                        {
                            existingUnit.UnitNo = splitWbsNo[3];
                        }
                        if (productTypeKey == "1" && unitNumberFormat == "2")
                        {
                            existingUnit.UnitNo = splitWbsNo[3] + "-" + Convert.ToInt32(splitWbsNo[5]);
                        }
                        if (productTypeKey == "2" && item.ModelName.ToLower() != "shop")
                        {
                            existingUnit.UnitNo = item.TowerCode + item.FloorName + item.ModelName + item.HighRiseLocation;
                        }
                        if (productTypeKey == "2" && item.ModelName.ToLower() == "shop")
                        {
                            existingUnit.UnitNo = item.ModelName + item.HighRiseLocation + item.TowerCode;
                        }

                        var assetTypeMasterCenter = assetTypes.Where(o => o.MasterCenterGroupKey == "AssetType" && o.Key == item.AssetType).FirstOrDefault();
                        existingUnit.AssetTypeMasterCenterID = assetTypeMasterCenter?.ID;

                        var unitDirectionMasterCenter = unitDirections.Where(o => o.MasterCenterGroupKey == "UnitDirection" && o.Key == item.UnitDirection).FirstOrDefault();
                        existingUnit.UnitDirectionMasterCenterID = unitDirectionMasterCenter?.ID;

                        existingUnit.ModelID = models.Where(o => o.NameEN == item.ModelName).FirstOrDefault() == null ? (Guid?)null : models.Where(o => o.NameEN == item.ModelName).Select(o => o.ID).FirstOrDefault();
                        if (!string.IsNullOrEmpty(item.TowerCode))
                        {
                            var existingTower = towers.Where(o => o.ProjectID == projectID && o.TowerCode == item.TowerCode).FirstOrDefault();
                            if (existingTower != null)
                            {
                                existingUnit.TowerID = existingTower.ID;
                                var existingFloor = floors.Where(o => o.TowerID == existingTower.ID && o.NameTH == item.FloorName).FirstOrDefault();
                                if (existingFloor != null)
                                {
                                    existingUnit.FloorID = existingFloor.ID;
                                }
                            }
                        }
                        unitsUpdate.Add(existingUnit);
                    }
                }
                row++;
            }
            DB.UpdateRange(unitsUpdate);
            await DB.SaveChangesAsync();

            var unitDataStatusMasterCenterID = await this.UnitDataStatus(projectID);
            project.UnitDataStatusMasterCenterID = unitDataStatusMasterCenterID;
            await DB.SaveChangesAsync();
            result.Success = unitsUpdate.Count();
            result.Error = error;
            return result;
        }

        public async Task<DataTable> ConvertExcelGeneralToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[3, 1, 3, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 4 : 3;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
        }

        public async Task<UnitFenceAreaExcelDTO> ImportUnitFenceAreaAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal Format 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Not Found 
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

            ValidateException ex = new ValidateException();

            #region Validate ProductType
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstOrDefaultAsync();
            if (project.ProductType.Key == ProductTypeKeys.HighRise)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0050").FirstAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            var result = new UnitFenceAreaExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var dt = await this.ConvertExcelUnitFenceAreaToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var checkNullWBSCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var checkNullFenceAreas = new List<string>();
            var checkNullFenceIronAreas = new List<string>();
            var checkFormatFenceAreas = new List<string>();
            var checkFormatFenceIronAreas = new List<string>();
            var unitNotFounds = new List<string>();
            var row = 2;
            var error = 0;

            var units = await DB.Units.Where(o => o.ProjectID == project.ID).ToListAsync();
            //Read Excel Model
            var unitFenceAreaExcelModels = new List<UnitFenceAreaExcelModel>();
            foreach (DataRow r in dt.Rows)
            {

                var excelModel = UnitFenceAreaExcelModel.CreateFromDataRow(r);
                unitFenceAreaExcelModels.Add(excelModel);

                var isError = false;

                #region Validate
                var unit = units.Where(o => o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSNo).FirstOrDefault();
                if (unit == null)
                {
                    unitNotFounds.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWBSCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[UnitFenceAreaExcelModel._fenceAreaIndex].ToString()))
                {
                    if (!r[UnitFenceAreaExcelModel._fenceAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatFenceAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                else
                {
                    checkNullFenceAreas.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[UnitFenceAreaExcelModel._fenceIronAreaIndex].ToString()))
                {
                    if (!r[UnitFenceAreaExcelModel._fenceIronAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatFenceIronAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                else
                {
                    checkNullFenceIronAreas.Add((row).ToString());
                    isError = true;
                }
                if (isError)
                {
                    error++;
                }
                row++;
                #endregion

            }



            #region Validate ProjectNo In File
            ex = new ValidateException();
            if (unitFenceAreaExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion


            #region ResultValidate
            if (checkNullWBSCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS CODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS CODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullUnitNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullFenceAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullFenceIronAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkNullFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatFenceAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วคอนกรีต");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatFenceIronAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatFenceIronAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (unitNotFounds.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", unitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "พื้นที่รั้วเหล็กดัด");
                    msg = msg.Replace("[row]", String.Join(",", unitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowError
            var rowErrors = new List<string>();

            rowErrors.AddRange(checkNullWBSCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullFenceAreas);
            rowErrors.AddRange(checkNullFenceIronAreas);
            rowErrors.AddRange(checkFormatFenceAreas);
            rowErrors.AddRange(checkFormatFenceIronAreas);
            rowErrors.AddRange(unitNotFounds);

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            #endregion
            row = 2;
            List<Unit> updateList = new List<Unit>();
            //Update Data
            foreach (var item in unitFenceAreaExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Where(o => o.SAPWBSNo == item.WBSNo && o.UnitNo == item.UnitNo).FirstOrDefault();
                    if (unit != null)
                    {
                        item.ToModel(ref unit);
                        updateList.Add(unit);
                    }
                }
                row++;
            }
            DB.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        private async Task<DataTable> ConvertExcelUnitFenceAreaToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            ////Stream ddd = new MemoryStream(test);

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
        }

        public async Task<Unit> DeleteUnitMeterAsync(Guid id)
        {
            try
            {
                var model = await DB.Units.Where(x => x.ID == id).FirstAsync();
                model.ElectricMeter = null;
                model.WaterMeter = null;
                model.ElectricMeterPrice = null;
                model.WaterMeterPrice = null;
                model.IsTransferElectricMeter = null;
                model.IsTransferWaterMeter = null;
                model.ElectricMeterTransferDate = null;
                model.WaterMeterTransferDate = null;
                model.ElectricMeterTopic = null;
                model.WaterMeterTopic = null;
                model.ElectricMeterRemark = null;
                model.WaterMeterRemark = null;

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UnitMeterDTO> GetUnitMeterAsync(Guid unitID)
        {
            try
            {
                var model = await DB.Units.Where(o => o.ID == unitID)
                                          .Include(o => o.UnitStatus)
                                          .Include(o => o.Model)
                                          .Include(o => o.ElectrictMeterStatus)
                                          .Include(o => o.WaterMeterStatus)
                                          .Include(o => o.ElectricMeterTopic)
                                          .Include(o => o.WaterMeterTopic)
                                          .Include(o => o.ElectricMeterPrice)
                                          .Include(o => o.WaterMeterPrice)
                                          .Include(o => o.UpdatedBy)
                                          .FirstAsync();

                var result = UnitMeterDTO.CreateFromModel(model);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UnitMeterDTO> UpdateUnitMeterAsync(Guid unitID, UnitMeterDTO input)
        {
            var model = await DB.Units.Where(x => x.ID == unitID).FirstAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();
            input.ToModel(ref model);

            if (model.IsTransferElectricMeter == true)
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "2").Select(o => o.ID).FirstOrDefault();
            }
            else if (model.IsTransferElectricMeter != true && !string.IsNullOrEmpty(model.ElectricMeter))
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
            }
            else if (string.IsNullOrEmpty(model.ElectricMeter))
            {
                model.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
            }

            if (model.IsTransferWaterMeter == true)
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "2").Select(o => o.ID).FirstOrDefault();
            }
            else if (model.IsTransferWaterMeter != true && !string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
            }
            else if (string.IsNullOrEmpty(model.WaterMeter))
            {
                model.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetUnitMeterAsync(unitID);
            return result;
        }

        public async Task<UnitMeterPaging> GetUnitMeterListAsync(UnitMeterFilter filter, PageParam pageParam, UnitMeterListSortByParam sortByParam)
        {
            IQueryable<UnitMeterListQueryResult> query = (from project in DB.Projects
                                                          join unit in DB.Units.DefaultIfEmpty()
                                                          .Include(o => o.UnitStatus)
                                                          .Include(o => o.Model)
                                                          .Include(o => o.ElectrictMeterStatus)
                                                          .Include(o => o.WaterMeterStatus)
                                                          .Include(o => o.WaterMeterTopic)
                                                          .Include(o => o.ElectricMeterTopic)
                                                          on project.ID equals unit.ProjectID
                                                          select new UnitMeterListQueryResult
                                                          {
                                                              Project = project,
                                                              Unit = unit,
                                                              UpdatedBy = unit.UpdatedBy
                                                          });
            #region Filter
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(x => projectIds.Contains(x.Project.ID));
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom);
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo);
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo);
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter));
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter));
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom);
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo);
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo);
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Unit.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Unit.Updated <= filter.UpdatedTo);
            }

            #endregion


            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<UnitMeterListQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => new ProjectUnitMeterListDTO
            {
                Project = ProjectDTO.CreateFromModel(o.Project),
                UnitMeter = UnitMeterListDTO.CreateFromModel(o.Unit)
            }).ToList();

            return new UnitMeterPaging()
            {
                PageOutput = pageOutput,
                ProjectUnitMeterLists = results
            };

        }

        public async Task<FileDTO> ExportExcelUnitInitialAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            var project = await (from p in DB.Projects.Include(o => o.Company)
                                 where p.ID == projectID
                                 select p).FirstAsync();
            var units = await (from u in DB.Units.Include(o => o.Model.TypeOfRealEstate)
                               where u.ProjectID == projectID
                               select u).ToListAsync();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_InitialFromSAP.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int projectSapCodeIndex = UnitInitialExcelModel._projectSapCodeIndex + 1;
                int wbsNoIndex = UnitInitialExcelModel._wbsNoIndex + 1;
                int wbsObjectCodeIndex = UnitInitialExcelModel._wbsObjectCodeIndex + 1;
                int companyIndex = UnitInitialExcelModel._companyIndex + 1;
                int boqStyleIndex = UnitInitialExcelModel._boqStyleIndex + 1;
                int typeOfRealEstateIndex = UnitInitialExcelModel._typeOfRealEstateIndex + 1;
                int wbsStatusIndex = UnitInitialExcelModel._wbsStatusIndex + 1;
                int saleAreaIndex = UnitInitialExcelModel._saleAreaIndex + 1;

                for (int c = 2; c < units.Count + 2; c++)
                {
                    var unit = units[c - 2];
                    worksheet.Cells[c, projectSapCodeIndex].Value = project.SapCode;
                    worksheet.Cells[c, wbsNoIndex].Value = unit.SAPWBSNo;
                    worksheet.Cells[c, wbsObjectCodeIndex].Value = unit.SAPWBSObject;
                    worksheet.Cells[c, companyIndex].Value = project.Company?.SAPCompanyID;
                    worksheet.Cells[c, boqStyleIndex].Value = string.Empty;
                    worksheet.Cells[c, typeOfRealEstateIndex].Value = unit.Model?.TypeOfRealEstate?.Name;
                    worksheet.Cells[c, wbsStatusIndex].Value = unit.SAPWBSStatus;
                    worksheet.Cells[c, saleAreaIndex].Value = unit.SaleArea;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_Initial_Units.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{projectID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        public async Task<FileDTO> ExportExcelUnitGeneralAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<UnitQueryResult> query = DB.Units.Where(x => x.ProjectID == projectID)
                                                                  .Select(x => new UnitQueryResult
                                                                  {
                                                                      Model = x.Model,
                                                                      Floor = x.Floor,
                                                                      UnitDirection = x.UnitDirection,
                                                                      Tower = x.Tower,
                                                                      Unit = x,
                                                                      UnitStatus = x.UnitStatus,
                                                                      UnitType = x.UnitType,
                                                                      AssetType = x.AssetType,
                                                                      TitledeedDetail = DB.TitledeedDetails.Where(c => c.UnitID == x.ID).FirstOrDefault()
                                                                  });
            var data = query.OrderBy(x => x.Unit.UnitNo).ToList();
            var project = await DB.Projects.Where(x => x.ID == projectID).FirstAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Units.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitExcelModel._projectNoIndex + 1;
                int _projectCodeIndex = UnitExcelModel._projectCodeIndex + 1;
                int _wbsCodeIndex = UnitExcelModel._wbsCodeIndex + 1;
                int _wbsobjectCodeIndex = UnitExcelModel._wbsobjectCodeIndex + 1;
                int _unitNoIndex = UnitExcelModel._unitNoIndex + 1;
                int _modelNameIndex = UnitExcelModel._modelNameIndex + 1;
                int _unitHighRiseLocationIndex = UnitExcelModel._unitHighRiseLocationIndex + 1;
                int _towerIndex = UnitExcelModel._towerIndex + 1;
                int _floorIndex = UnitExcelModel._floorIndex + 1;
                int _unitDirectionIndex = UnitExcelModel._unitDirectionIndex + 1;
                int _floorplanNameIndex = UnitExcelModel._floorplanNameIndex + 1;
                int _roomplanNameIndex = UnitExcelModel._roomplanNameIndex + 1;
                int _assetTypeIndex = UnitExcelModel._assetTypeIndex + 1;
                int _saleAreaIndex = UnitExcelModel._saleAreaIndex + 1;
                int _titledeedArea = UnitExcelModel._titledeedArea + 1;
                int _numberOfPrivilegeIndex = UnitExcelModel._numberOfPrivilegeIndex + 1;
                int _numberOfParkingFixIndex = UnitExcelModel._numberOfParkingFixIndex + 1;
                int _numberOfParkingUnFixIndex = UnitExcelModel._numberOfParkingUnFixIndex + 1;


                for (int c = 4; c < data.Count + 4; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _projectCodeIndex].Value = project.SapCode;
                    worksheet.Cells[c, _wbsCodeIndex].Value = data[c - 4].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _wbsobjectCodeIndex].Value = data[c - 4].Unit?.SAPWBSObject;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 4].Unit.UnitNo;
                    worksheet.Cells[c, _modelNameIndex].Value = data[c - 4].Model?.NameTH;
                    worksheet.Cells[c, _unitHighRiseLocationIndex].Value = data[c - 4].Unit.Position;
                    worksheet.Cells[c, _towerIndex].Value = data[c - 4].Tower?.TowerNoTH;
                    worksheet.Cells[c, _floorIndex].Value = data[c - 4].Floor?.NameTH;
                    worksheet.Cells[c, _unitDirectionIndex].Value = data[c - 4].UnitDirection?.Key;
                    worksheet.Cells[c, _floorplanNameIndex].Value = data[c - 4].Unit.FloorPlanFileName;
                    worksheet.Cells[c, _roomplanNameIndex].Value = data[c - 4].Unit.RoomPlanFileName;
                    worksheet.Cells[c, _assetTypeIndex].Value = data[c - 4].AssetType?.Key;
                    worksheet.Cells[c, _saleAreaIndex].Value = data[c - 4].Unit.SaleArea;
                    worksheet.Cells[c, _titledeedArea].Value = data[c - 4].TitledeedDetail?.TitledeedArea;
                    worksheet.Cells[c, _numberOfPrivilegeIndex].Value = data[c - 4].Unit.NumberOfPrivilege;
                    worksheet.Cells[c, _numberOfParkingFixIndex].Value = data[c - 4].Unit.NumberOfParkingFix;
                    worksheet.Cells[c, _numberOfParkingUnFixIndex].Value = data[c - 4].Unit.NumberOfParkingUnFix;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_Units.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{projectID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        public async Task<FileDTO> ExportExcelUnitFenceAreaAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<UnitQueryResult> query = from unit in DB.Units
                                                join titledeed in DB.TitledeedDetails
                                                on unit.ID equals titledeed.UnitID into d
                                                where unit.ProjectID == projectID
                                                from e in d.DefaultIfEmpty()
                                                select new UnitQueryResult
                                                {
                                                    TitledeedDetail = e,
                                                    Unit = unit
                                                };
            var data = query.OrderBy(o => o.Unit.UnitNo).ToList();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Address.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitFenceAreaExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = UnitFenceAreaExcelModel._wbsNoIndex + 1;
                int _unitNoIndex = UnitFenceAreaExcelModel._unitNoIndex + 1;
                int _fenceAreaIndex = UnitFenceAreaExcelModel._fenceAreaIndex + 1;
                int _fenceIronAreaIndex = UnitFenceAreaExcelModel._fenceIronAreaIndex + 1;


                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count() + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _fenceAreaIndex].Value = data[c - 2].Unit?.FenceArea;
                    worksheet.Cells[c, _fenceIronAreaIndex].Value = data[c - 2].Unit?.FenceIronArea;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_Address.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{projectID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeter.xlsx
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<UnitMeterExcelDTO> ImportUnitMeterExcelAsync(FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // String with 10 character
            var err0074 = await DB.ErrorMessages.Where(o => o.Key == "ERR0074").FirstAsync();

            UnitMeterExcelDTO result = new UnitMeterExcelDTO();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 1;
            var error = 0;

            var checkNullUnitNos = new List<string>();
            var checkNullAddressNumbers = new List<string>();
            var checkFormatElectricMeterNumbers = new List<string>();
            var checkFormatWaterMeterNumbers = new List<string>();

            //Read Excel Model
            var unitMeterExcelModels = new List<UnitMeterExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitMeterExcelModel.CreateFromDataRow(r);
                unitMeterExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.HouseNo))
                {
                    checkNullAddressNumbers.Add((row + 1).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.WaterMeter))
                {
                    if (!excelModel.WaterMeter.IsOnlyNumberWithMaxLength(10))
                    {
                        checkFormatElectricMeterNumbers.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.ElectricMeter))
                {
                    if (!excelModel.ElectricMeter.IsOnlyNumberWithMaxLength(10))
                    {
                        checkFormatWaterMeterNumbers.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                #endregion

                if (isError)
                {
                    error++;
                }
                row++;
            }

            #region Add Result Validate


            if (checkNullUnitNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullAddressNumbers.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatElectricMeterNumbers.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0074.Message.Replace("[column]", "ElectricMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatElectricMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0074.Message.Replace("[column]", "ElectricMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatElectricMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatWaterMeterNumbers.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WaterMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatWaterMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WaterMeterNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatWaterMeterNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion


            var projectNos = unitMeterExcelModels.Select(o => o.ProjectNo).Distinct().ToList();
            var projects = await (from p in DB.Projects
                                  where projectNos.Contains(p.ProjectNo)
                                  select p).ToListAsync();
            var projectIDs = projects.Select(o => o.ID).Distinct().ToList();
            var units = await (from u in DB.Units
                               where projectIDs.Contains(u.ProjectID ?? Guid.Empty)
                               select u).ToListAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();

            List<Unit> updateList = new List<Unit>();
            //Update Data
            foreach (var item in unitMeterExcelModels)
            {
                var project = projects.FirstOrDefault(o => o.ProjectNo == item.ProjectNo);
                if (project != null)
                {
                    var unit = units.FirstOrDefault(x => x.ProjectID == project.ID && x.UnitNo == item.UnitNo);
                    if (unit != null)
                    {
                        item.ToModel(ref unit);
                        if (string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
                        }
                        else
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
                        }
                        if (string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
                        }
                        else
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
                        }
                        updateList.Add(unit);
                    }
                }
            }
            DB.Units.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        private async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? $"Column {firstRowCell.Start.Column} {firstRowCell.Text}" : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeter.xls
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<FileDTO> ExportUnitMeterExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync();
            }
            else
            {
                projects = await DB.Projects.ToListAsync();
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync();

            var query = (from project in projects
                         join unit in units
                         on project.ID equals unit.ProjectID
                         into temp
                         from tempdata in temp.DefaultIfEmpty()
                         select new UnitMeterListQueryResult
                         {
                             Project = project,
                             Unit = tempdata,
                         }).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo)).ToList();
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom).ToList();
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter)).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom).ToList();
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID).ToList();
            }

            #endregion

            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var data = query;

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "UnitMeter.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitMeterExcelModel._projectNoIndex + 1;
                int _unitNoIndex = UnitMeterExcelModel._unitNoIndex + 1;
                int _addressNoIndex = UnitMeterExcelModel._addressNoIndex + 1;
                int _electricMeterNoIndex = UnitMeterExcelModel._electricMeterNoIndex + 1;
                int _waterMeterNoIndex = UnitMeterExcelModel._waterMeterNoIndex + 1;


                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _addressNoIndex].Value = data[c - 2].Unit?.HouseNo;
                    worksheet.Cells[c, _electricMeterNoIndex].Value = data[c - 2].Unit?.ElectricMeter;
                    worksheet.Cells[c, _waterMeterNoIndex].Value = data[c - 2].Unit?.WaterMeter;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "UnitMeter.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"unit-meter/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeterStatus.xls
        /// </summary>
        /// <returns>The unit meter status excel async.</returns>
        public async Task<UnitMeterExcelDTO> ImportUnitMeterStatusExcelAsync(FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

            // 1 2 null
            var err0075 = await DB.ErrorMessages.Where(o => o.Key == "ERR0075").FirstAsync();

            //Format Date
            var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();

            // 1,2,3,4
            var err0070 = await DB.ErrorMessages.Where(o => o.Key == "ERR0070").FirstAsync();


            var result = new UnitMeterExcelDTO();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 11)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 1;
            var error = 0;

            var topics = new List<string> { "1", "2", "3", "4" };

            var checkNullUnitNos = new List<string>();
            var checkNullAddressNumbers = new List<string>();
            var checkIsTransferElectricMeters = new List<string>();
            var checkIsTransferWaterMeters = new List<string>();
            var checkElectricMeterTopics = new List<string>();
            var checkElectricMeterDates = new List<string>();
            var checkWaterMeterTopics = new List<string>();
            var checkWaterMeterDates = new List<string>();

            //Read Excel Model
            var unitMeterStatusExcelModels = new List<UnitMeterStatusExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = UnitMeterStatusExcelModel.CreateFromDataRow(r);
                unitMeterStatusExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row + 1).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.HouseNo))
                {
                    checkNullAddressNumbers.Add((row + 1).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(excelModel.IsTransferElectricMeter))
                {
                    if (excelModel.IsTransferElectricMeter != "0" || excelModel.IsTransferElectricMeter != "1")
                    {
                        checkIsTransferElectricMeters.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.IsTransferWaterMeter))
                {
                    if (excelModel.IsTransferWaterMeter != "0" || excelModel.IsTransferWaterMeter != "1")
                    {
                        checkIsTransferWaterMeters.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.ElectricMeterTopic))
                {
                    if (!topics.Contains(excelModel.ElectricMeterTopic))
                    {
                        checkElectricMeterTopics.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.WaterMeterTopic))
                {
                    if (!topics.Contains(excelModel.WaterMeterTopic))
                    {
                        checkWaterMeterTopics.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitMeterStatusExcelModel._electricMeterTransferDateIndex].ToString()))
                {
                    if (!r[UnitMeterStatusExcelModel._electricMeterTransferDateIndex].ToString().isFormatDate())
                    {
                        checkElectricMeterDates.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[UnitMeterStatusExcelModel._waterMeterTransferDate].ToString()))
                {
                    if (!r[UnitMeterStatusExcelModel._waterMeterTransferDate].ToString().isFormatDate())
                    {
                        checkWaterMeterDates.Add((row + 1).ToString());
                        isError = true;
                    }
                }
                #endregion

                if (isError)
                {
                    error++;
                }
                row++;
            }

            if (checkNullUnitNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "UnitNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullAddressNumbers.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "AddressNumber");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAddressNumbers));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkIsTransferElectricMeters.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์ไฟฟ้า");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferElectricMeters));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์ไฟฟ้า");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferElectricMeters));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkIsTransferWaterMeters.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์น้ำประปา");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferWaterMeters));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0075.Message.Replace("[column]", "สถานะโอนมิเตอร์น้ำประปา");
                    msg = msg.Replace("[row]", String.Join(",", checkIsTransferWaterMeters));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkElectricMeterTopics.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkWaterMeterTopics.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "หัวข้อ 1,2,3,4");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterTopics));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkElectricMeterDates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์ไฟฟ้า Format=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์ไฟฟ้า Format=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkElectricMeterDates));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkWaterMeterDates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์น้ำประปาFormat=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่โอนมิเตอร์น้ำประปาFormat=DD/MM/YYYY");
                    msg = msg.Replace("[row]", String.Join(",", checkWaterMeterDates));
                    result.ErrorMessages.Add(msg);
                }
            }

            var projectNos = unitMeterStatusExcelModels.Select(o => o.ProjectNo).Distinct().ToList();
            var projects = await (from p in DB.Projects
                                  where projectNos.Contains(p.ProjectNo)
                                  select p).ToListAsync();
            var projectIDs = projects.Select(o => o.ID).Distinct().ToList();
            var units = await (from u in DB.Units
                               where projectIDs.Contains(u.ProjectID ?? Guid.Empty)
                               select u).ToListAsync();
            var meterTopics = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterTopic).ToListAsync();
            var meterStatuses = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MeterStatus).ToListAsync();
            List<Unit> updateList = new List<Unit>();

            //Update Data
            foreach (var item in unitMeterStatusExcelModels)
            {
                var project = projects.Where(o => o.ProjectNo == item.ProjectNo).FirstOrDefault();
                if (project != null)
                {
                    var unit = units.Where(x => x.ProjectID == project.ID && x.UnitNo == item.UnitNo).FirstOrDefault();
                    if (unit != null)
                    {
                        // ไฟฟ้า
                        unit.IsTransferElectricMeter = item.IsTransferElectricMeter == "0" ? false : item.IsTransferElectricMeter == "1" ? true : (bool?)null;
                        DateTime electricMeterTransferDate;
                        if (DateTime.TryParseExact(item.ElectricMeterTransferDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out electricMeterTransferDate))
                        {
                            unit.ElectricMeterTransferDate = electricMeterTransferDate;
                        }
                        var electricMeterTopicMasterCenter = meterTopics.Where(x => x.Key == item.ElectricMeterTopic).FirstOrDefault();
                        unit.ElectricMeterTopicMasterCenterID = electricMeterTopicMasterCenter?.ID;
                        unit.ElectricMeterRemark = item.ElectricMeterRemark;
                        if (unit.IsTransferElectricMeter == true)
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "2").Select(o => o.ID).FirstOrDefault();
                        }
                        else if (unit.IsTransferElectricMeter != true && !string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
                        }
                        else if (string.IsNullOrEmpty(unit.ElectricMeter))
                        {
                            unit.ElectricMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
                        }
                        //น้ำ
                        unit.IsTransferWaterMeter = item.IsTransferWaterMeter == "0" ? false : item.IsTransferWaterMeter == "1" ? true : (bool?)null;
                        DateTime waterMeterTransferDate;
                        if (DateTime.TryParseExact(item.ElectricMeterTransferDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out waterMeterTransferDate))
                        {
                            unit.WaterMeterTransferDate = waterMeterTransferDate;
                        }
                        var waterMeterTopicMasterCenter = meterTopics.Where(x => x.Key == item.WaterMeterTopic).FirstOrDefault();
                        unit.WaterMeterTopicMasterCenterID = waterMeterTopicMasterCenter?.ID;
                        unit.WaterMeterRemark = item.WaterMeterRemark;
                        if (unit.IsTransferWaterMeter == true)
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "2").Select(o => o.ID).FirstOrDefault();
                        }
                        else if (unit.IsTransferWaterMeter != true && !string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "1").Select(o => o.ID).FirstOrDefault();
                        }
                        else if (string.IsNullOrEmpty(unit.WaterMeter))
                        {
                            unit.WaterMeterStatusMasterCenterID = meterStatuses.Where(o => o.Key == "0").Select(o => o.ID).FirstOrDefault();
                        }
                        updateList.Add(unit);
                    }
                }
            }
            DB.Units.UpdateRange(updateList);
            await DB.SaveChangesAsync();
            result.Success = updateList.Count();
            result.Error = error;
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17484404/364137937/preview
        /// Sample File: http://192.168.2.29:9001/xunit-tests/UnitMeterStatus.xls
        /// </summary>
        /// <returns>The unit meter excel async.</returns>
        public async Task<FileDTO> ExportUnitMeterStatusExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            List<Database.Models.PRJ.Project> projects = new List<Database.Models.PRJ.Project>();
            if (!string.IsNullOrEmpty(filter.ProjectIDs))
            {
                var projectIds = filter.ProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                projects = await DB.Projects.Where(o => projectIds.Contains(o.ID)).ToListAsync();
            }
            else
            {
                projects = await DB.Projects.ToListAsync();
            }
            var projectsId = projects.Select(o => o.ID).ToList();
            var units = await DB.Units.Where(o => projectsId.Contains(o.ProjectID.Value))
                                      .Include(o => o.UnitStatus)
                                      .Include(o => o.Model)
                                      .Include(o => o.ElectrictMeterStatus)
                                      .Include(o => o.WaterMeterStatus)
                                      .Include(o => o.WaterMeterTopic)
                                      .Include(o => o.ElectricMeterTopic)
                                      .ToListAsync();

            var query = (from project in projects
                         join unit in units
                         on project.ID equals unit.ProjectID
                         into temp
                         from tempdata in temp.DefaultIfEmpty()
                         select new UnitMeterListQueryResult
                         {
                             Project = project,
                             Unit = tempdata,
                         }).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo)).ToList();
            }
            if (filter.ModelID != null && filter.ModelID != Guid.Empty)
            {
                query = query.Where(x => x.Unit.ModelID == filter.ModelID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                      && x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom).ToList();
            }
            if (filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (filter.TransferOwnerShipDateFrom != null && filter.TransferOwnerShipDateTo != null)
            {
                query = query.Where(x => x.Unit.TransferOwnerShipDate >= filter.TransferOwnerShipDateFrom
                                    && x.Unit.TransferOwnerShipDate <= filter.TransferOwnerShipDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeter))
            {
                query = query.Where(x => x.Unit.ElectricMeter.Contains(filter.ElectricMeter)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeter))
            {
                query = query.Where(x => x.Unit.WaterMeter.Contains(filter.WaterMeter)).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom).ToList();
            }
            if (filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (filter.CompletedDocumentDateFrom != null && filter.CompletedDocumentDateTo != null)
            {
                query = query.Where(x => x.Unit.CompletedDocumentDate >= filter.CompletedDocumentDateFrom
                                    && x.Unit.CompletedDocumentDate <= filter.CompletedDocumentDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.ElectricMeterStatusKey))
            {
                var electricMeterStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.ElectricMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == electricMeterStatusMasterCenterID).ToList();
            }
            if (!string.IsNullOrEmpty(filter.WaterMeterStatusKey))
            {
                var waterMeterStatusKeyMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.WaterMeterStatusKey
                                                                    && x.MasterCenterGroupKey == "MeterStatus")
                                                                   .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.ElectricMeterStatusMasterCenterID == waterMeterStatusKeyMasterCenterID).ToList();
            }

            #endregion

            ProjectUnitMeterListDTO.SortBy(sortByParam, ref query);

            var data = query;

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "UnitMeterStatus.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = UnitMeterStatusExcelModel._projectNoIndex + 1;
                int _unitNoIndex = UnitMeterStatusExcelModel._unitNoIndex + 1;
                int _addressNoIndex = UnitMeterStatusExcelModel._addressNoIndex + 1;
                int _isTransferElectricMeter = UnitMeterStatusExcelModel._isTransferElectricMeter + 1;
                int _electricMeterTransferDateIndex = UnitMeterStatusExcelModel._electricMeterTransferDateIndex + 1;
                int _electricMeterTopic = UnitMeterStatusExcelModel._electricMeterTopic + 1;
                int _electricMeterRemark = UnitMeterStatusExcelModel._electricMeterRemark + 1;
                int _isTransferWaterMeter = UnitMeterStatusExcelModel._isTransferWaterMeter + 1;
                int _waterMeterTransferDate = UnitMeterStatusExcelModel._waterMeterTransferDate + 1;
                int _waterMeterTopic = UnitMeterStatusExcelModel._waterMeterTopic + 1;
                int _waterMeterRemark = UnitMeterStatusExcelModel._waterMeterRemark + 1;



                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _addressNoIndex].Value = data[c - 2].Unit?.HouseNo;

                    worksheet.Cells[c, _isTransferElectricMeter].Value = data[c - 2].Unit?.IsTransferElectricMeter == true ? 1.ToString() : data[c - 2].Unit?.IsTransferElectricMeter == false ? 0.ToString() : null;

                    worksheet.Cells[c, _electricMeterTransferDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[c, _electricMeterTransferDateIndex].Value = data[c - 2].Unit?.ElectricMeterTransferDate;

                    worksheet.Cells[c, _electricMeterTopic].Value = data[c - 2].Unit?.ElectricMeterTopic?.Key;
                    worksheet.Cells[c, _electricMeterRemark].Value = data[c - 2].Unit?.ElectricMeterRemark;


                    worksheet.Cells[c, _isTransferWaterMeter].Value = data[c - 2].Unit?.IsTransferWaterMeter == true ? 1.ToString() : data[c - 2].Unit?.IsTransferWaterMeter == false ? 0.ToString() : null; ;
                    worksheet.Cells[c, _waterMeterTransferDate].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[c, _waterMeterTransferDate].Value = data[c - 2].Unit?.WaterMeterTransferDate;
                    worksheet.Cells[c, _waterMeterTopic].Value = data[c - 2].Unit?.WaterMeterTopic?.Key;
                    worksheet.Cells[c, _waterMeterRemark].Value = data[c - 2].Unit?.WaterMeterRemark;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "UnitMeterStatus.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"unit-meter/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }


        private async Task<Guid> UnitDataStatus(Guid projectID)
        {
            var allUnit = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var project = await DB.Projects.Where(o => o.ID == projectID).Include(o => o.ProductType).FirstAsync();
            var unitDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
            var unitDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();

            var unitDataStatusMasterCenterID = unitDataStatusPrepareMasterCenterID;
            if (allUnit.Count() == 0)
            {
                return unitDataStatusPrepareMasterCenterID;
            }
            if (project.ProductType.Key == ProductTypeKeys.HighRise)
            {
                if (allUnit.Count() > 0 && allUnit.TrueForAll(o =>
                          !string.IsNullOrEmpty(o.SAPWBSNo)
                          && !string.IsNullOrEmpty(o.SAPWBSObject)
                          && o.ModelID != null
                          && o.SaleArea != null
                          && o.UnitStatusMasterCenterID != null
                          && !string.IsNullOrEmpty(o.FloorPlanFileName)
                          && !string.IsNullOrEmpty(o.RoomPlanFileName)
                ))
                {
                    unitDataStatusMasterCenterID = unitDataStatusSaleMasterCenterID;
                }
            }
            else
            {
                if (allUnit.Count() > 0 && allUnit.TrueForAll(o =>
                        !string.IsNullOrEmpty(o.SAPWBSNo)
                        && !string.IsNullOrEmpty(o.SAPWBSObject)
                        && o.ModelID != null
                        && o.SaleArea != null
                        && o.UnitStatusMasterCenterID != null
               ))
                {
                    unitDataStatusMasterCenterID = unitDataStatusSaleMasterCenterID;
                }
            }
            return unitDataStatusMasterCenterID;
        }

        /// <summary>
        /// อ่าน Text File เพื่อ Update WBS กิ่ง P
        /// </summary>
        /// <returns></returns>
        public async Task<UnitSyncResponse> ReadSAPWBSPromotionTextFileAsync(string[] text)
        {
            //TODO: [Big] ReadSAPWBSPromotionTextFile
            var response = new UnitSyncResponse { SAPWBSNoNotFound = new List<string>(), Update = 0 };
            var listModelSap = new List<UnitSap>();
            foreach (var item in text)
            {
                var data = item.Split(';').ToList();
                var model = new UnitSap
                {
                    PSPNR = data[0],
                    POSID = data[1],
                    PSPHI = data[2],
                    ERDAT = data[3],
                    AEDAT = data[4],
                    VERNR = data[5],
                    VERNA = data[6],
                    ASTNR = data[7],
                    ASTNA = data[8],
                    PBUKR = data[9],
                    PRCTR = data[10],
                    PRART = data[11],
                    STUFE = data[12],
                    POST1 = data[13],
                    OBJNR = data[14],
                    DOWN = data[15],
                    WERKS = data[16],
                    HMTYP = data[17],
                    CRMID = data[18],
                    WBS = data[19],
                    WBSR = data[20],
                    ROBJ = data[21],
                };
                listModelSap.Add(model);
            }

            var updateUnit = new List<Unit>();
            foreach (var item in listModelSap)
            {
                var unit = await DB.Units.Where(o => o.SAPWBSNo == item.WBSR).FirstOrDefaultAsync();
                if (unit != null)
                {
                    unit.SAPWBSNo_P = item.WBS;
                    unit.SAPWBSObject_P = item.OBJNR;
                    updateUnit.Add(unit);
                }
                else
                {
                    response.SAPWBSNoNotFound.Add(item.WBSR);
                }
            }

            DB.Units.UpdateRange(updateUnit);
            await DB.SaveChangesAsync();
            response.Update = updateUnit.Count();
            return response;
        }

    }

}
