using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Params.Outputs;
using Project.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling;
using System.Reflection;
using System.ComponentModel;

namespace Project.Services
{
    public class TitleDeedService : ITitleDeedService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public TitleDeedService(IConfiguration configuration, DatabaseContext db)
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

        /// <summary>
        /// UI https://projects.invisionapp.com/d/main#/console/17482171/362360642/preview
        /// </summary>
        /// <returns>The title deed list async.</returns>
        /// <param name="projectID">Project identifier.</param>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam)
        {
            IQueryable<TitleDeedQueryResult> query = DB.TitledeedDetails
                                                    .Include(o => o.Unit.UnitStatus)
                                                    .Select(o => new TitleDeedQueryResult
                                                    {
                                                        Titledeed = o,
                                                        Project = o.Project,
                                                        Unit = o.Unit,
                                                        Model = o.Unit.Model,
                                                        LandOffice = o.Unit.LandOffice,
                                                        LandStatus = o.LandStatus,
                                                        PreferStatus = o.PreferStatus,
                                                        UpdatedBy = o.UpdatedBy
                                                    });

            #region Filter
            if (projectID != null)
            {
                query = query.Where(x => x.Project.ID == projectID);
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo));
            }
            if (!string.IsNullOrEmpty(filter.TitledeedNo))
            {
                query = query.Where(x => x.Titledeed.TitledeedNo.Contains(filter.TitledeedNo));
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                query = query.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo));
            }
            if (filter.LandOfficeID != null && filter.LandOfficeID != Guid.Empty)
            {
                query = query.Where(x => x.LandOffice.ID == filter.LandOfficeID);
            }
            if (!string.IsNullOrEmpty(filter.HouseName))
            {
                query = query.Where(x => x.Model.NameTH.Contains(filter.HouseName));
            }

            if (filter.TitledeedAreaFrom != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom);
            }
            if (filter.TitledeedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
            }
            if (filter.TitledeedAreaFrom != null && filter.TitledeedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
                                        && x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo);
            }

            if (filter.UsedAreaFrom != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom);
            }
            if (filter.UsedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
            }
            if (filter.UsedAreaFrom != null && filter.UsedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.Unit.UsedArea >= filter.UsedAreaFrom
                                        && x.Titledeed.Unit.UsedArea <= filter.UsedAreaTo);
            }

            if (!string.IsNullOrEmpty(filter.LandNo))
            {
                query = query.Where(x => x.Titledeed.LandNo.Contains(filter.LandNo));
            }
            if (!string.IsNullOrEmpty(filter.LandSurveyArea))
            {
                query = query.Where(x => x.Titledeed.LandSurveyArea.Contains(filter.LandSurveyArea));
            }
            if (!string.IsNullOrEmpty(filter.LandPortionNo))
            {
                query = query.Where(x => x.Titledeed.LandPortionNo.Contains(filter.LandPortionNo));
            }
            if (!string.IsNullOrEmpty(filter.LandStatusKey))
            {
                var landStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.LandStatusKey
                                                                       && x.MasterCenterGroupKey == "LandStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.LandStatus.ID == landStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.PreferStatusKey))
            {
                var preferStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PreferStatusKey
                                                                       && x.MasterCenterGroupKey == "PreferStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.PreferStatus.ID == preferStatusMasterCenterID);
            }

            if (filter.LandStatusDateFrom != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom);
            }
            if (filter.LandStatusDateTo != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
            }
            if (filter.LandStatusDateFrom != null && filter.LandStatusDateTo != null)
            {
                query = query.Where(x => x.Titledeed.LandStatusDate >= filter.LandStatusDateFrom
                                        && x.Titledeed.LandStatusDate <= filter.LandStatusDateTo);
            }


            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Titledeed.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Titledeed.Updated >= filter.UpdatedFrom && x.Titledeed.Updated <= filter.UpdatedTo);
            }
            #endregion

            TitleDeedListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<TitleDeedQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => TitleDeedListDTO.CreateFromQueryResult(o)).ToList();

            return new TitleDeedPaging()
            {
                PageOutput = pageOutput,
                TitleDeeds = results
            };
        }

        public async Task<TitleDeedDTO> GetTitleDeedAsync(Guid id)
        {
            var model = await DB.TitledeedDetails.Where(o => o.ID == id)
                                                 .Include(o => o.Project)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.LandOffice)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseProvince)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseDistrict)
                                                 .Include(o => o.Unit)
                                                 .ThenInclude(o => o.HouseSubDistrict)
                                                 .Include(o => o.Address)
                                                 .Include(o => o.LandStatus)
                                                 .Include(o => o.PreferStatus)
                                                 .Include(o => o.UpdatedBy)
                                                 .FirstAsync();

            var result = TitleDeedDTO.CreateFromModel(model);
            return result;
        }

        public async Task<TitleDeedDTO> CreateTitleDeedAsync(Guid projectID, TitleDeedDTO input)
        {
            await input.ValidateAsync(projectID, DB);
            var unit = await DB.Units.FirstAsync(o => o.ID == input.Unit.Id);
            TitledeedDetail model = new TitledeedDetail();
            model.Unit = unit;
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.TitledeedDetails.AddAsync(model);
            DB.Update(model.Unit);
            await DB.SaveChangesAsync();
            var result = await this.GetTitleDeedAsync(model.ID);
            return result;
        }

        public async Task<TitleDeedDTO> UpdateTitleDeedAsync(Guid projectID, Guid id, TitleDeedDTO input)
        {
            await input.ValidateAsync(projectID, DB);
            var model = await (DB.TitledeedDetails.Include(o => o.Unit).Where(x => x.ProjectID == projectID && x.ID == id).FirstAsync());

            input.ToModel(ref model);
            model.ProjectID = projectID;

            DB.Entry(model).State = EntityState.Modified;
            DB.Update(model.Unit);
            await DB.SaveChangesAsync();
            var result = await this.GetTitleDeedAsync(model.ID);
            return result;
        }

        public async Task<TitleDeedDTO> UpdateTitleDeedStatusAsync(Guid id, TitleDeedDTO input)
        {
            var model = await DB.TitledeedDetails.Where(o => o.ID == id).FirstAsync();
            model.LandStatusMasterCenterID = input.LandStatus?.Id;
            model.PreferStatusMasterCenterID = input.PreferStatus?.Id;
            model.LandStatusDate = input.LandStatusDate;
            model.LandStatusNote = input.LandStatusNote;
            var newModel = model.CloneToHistoryItem();

            DB.Entry(model).State = EntityState.Modified;
            await DB.AddAsync(newModel);
            await DB.SaveChangesAsync();
            var result = await this.GetTitleDeedAsync(model.ID);
            return result;
        }



        public async Task<TitledeedDetail> DeleteTitleDeedAsync(Guid projectID, Guid id)
        {
            var model = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();

            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<List<TitleDeedDTO>> GetTitleDeedHistoryItemsAsync(Guid id)
        {
            var model = await DB.TitledeedDetails.Where(o => o.ID == id).FirstAsync();
            var historyModels = await DB.TitledeedDetailHistories
                .Where(o => o.TitledeedDetailID == model.ID)
                .OrderBy(o => o.Created)
                .ToListAsync();
            var results = historyModels.Select(o => TitleDeedDTO.CreateFromHistoryModel(o)).ToList();

            return results;
        }

        public async Task<TitledeedExcelDTO> ImportTitleDeedAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Integer
            var err0069 = await DB.ErrorMessages.Where(o => o.Key == "ERR0069").FirstAsync();
            // Integer with Special
            var err0068 = await DB.ErrorMessages.Where(o => o.Key == "ERR0068").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // Unique
            var err0084 = await DB.ErrorMessages.Where(o => o.Key == "ERR0084").FirstAsync();
            var result = new TitledeedExcelDTO { Error = 0, Success = 0, ErrorMessages = new List<string>() };
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 15)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var titledeedDetailModel = new List<TitledeedDetailExcelModel>();
            var row = 2;
            var error = 0;

            var checkNullWbsCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var checkNullModelNames = new List<string>();
            var checkNullHouseNos = new List<string>();
            var checkNullTitleDeedNos = new List<string>();
            var checkFormatTitleDeedNos = new List<string>();
            var checkFormatLandNos = new List<string>();
            var checkFormatLandPortionNos = new List<string>();
            var checkFormatLandSurveyAreas = new List<string>();
            var checkFormatHouseNos = new List<string>();
            var checkFormatSaleAreas = new List<string>();
            var checkFormatEstimatePrices = new List<string>();
            var checkFormatBookNos = new List<string>();
            var checkFormatPageNos = new List<string>();
            var checkUnitNotFound = new List<string>();
            var checkUniqueTitleDeedNo = new List<string>();

            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var titleDeeds = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();

            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = TitledeedDetailExcelModel.CreateFromDataRow(r);
                titledeedDetailModel.Add(excelModel);
                var unit = units.Where(o => o.ProjectID == projectID && o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSNo).FirstOrDefault();
                if (unit == null)
                {
                    checkUnitNotFound.Add((row).ToString());
                    isError = true;
                }

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWbsCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.ModelName))
                {
                    checkNullModelNames.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.HouseNo))
                {
                    checkNullHouseNos.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!excelModel.HouseNo.IsOnlyNumberWithSpecialCharacter(true))
                    {
                        checkFormatHouseNos.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.TitledeedNo))
                {
                    checkNullTitleDeedNos.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!excelModel.TitledeedNo.IsOnlyNumberWithSpecialCharacter(true))
                    {
                        checkFormatTitleDeedNos.Add((row).ToString());
                        isError = true;
                    }
                    var titleDeed = titleDeeds.Where(o => o.TitledeedNo == excelModel.TitledeedNo).FirstOrDefault();
                    if (titleDeed != null)
                    {
                        checkUniqueTitleDeedNo.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.LandNo))
                {
                    if (!excelModel.LandNo.IsOnlyNumberWithSpecialCharacter(true))
                    {
                        checkFormatLandNos.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.LandPortionNo))
                {
                    if (!excelModel.LandNo.IsOnlyNumberWithSpecialCharacter(true))
                    {
                        checkFormatLandPortionNos.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.LandSurveyArea))
                {
                    if (!excelModel.LandNo.IsOnlyNumberWithSpecialCharacter(true))
                    {
                        checkFormatLandSurveyAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.BookNo))
                {
                    if (!excelModel.BookNo.IsOnlyNumber())
                    {
                        checkFormatBookNos.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(excelModel.PageNo))
                {
                    if (!excelModel.PageNo.IsOnlyNumber())
                    {
                        checkFormatPageNos.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[TitledeedDetailExcelModel._titledeedAreaIndex].ToString()))
                {
                    if (!r[TitledeedDetailExcelModel._titledeedAreaIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatSaleAreas.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[TitledeedDetailExcelModel._estimatePriceIndex].ToString()))
                {
                    if (!r[TitledeedDetailExcelModel._estimatePriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatEstimatePrices.Add((row).ToString());
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


            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (titledeedDetailModel.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

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
            if (checkNullHouseNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "บ้านเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkNullHouseNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "บ้านเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkNullHouseNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullTitleDeedNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTitleDeedNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkNullTitleDeedNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatTitleDeedNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatTitleDeedNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatLandNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "เลขที่ดิน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "เลขที่ดิน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatLandSurveyAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "หน้าสำรวจ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandSurveyAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "หน้าสำรวจ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandSurveyAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatLandPortionNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "เลขระวาง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandPortionNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "เลขระวาง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandPortionNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatHouseNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "บ้านเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandPortionNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "บ้านเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLandPortionNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatSaleAreas.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0068.Message.Replace("[column]", "เนื้อที่ (ตร.ว.)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0068.Message.Replace("[column]", "เนื้อที่ (ตร.ว.)");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSaleAreas));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatEstimatePrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาประเมิน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatEstimatePrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatBookNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0069.Message.Replace("[column]", "เล่ม");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBookNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0069.Message.Replace("[column]", "เล่ม");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBookNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatPageNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0069.Message.Replace("[column]", "หน้า");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatPageNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0069.Message.Replace("[column]", "หน้า");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatPageNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkUnitNotFound.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFound));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFound));
                    result.ErrorMessages.Add(msg);
                }
            }

            #region Unique
            if (checkUniqueTitleDeedNo.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0084.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkUniqueTitleDeedNo));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0084.Message.Replace("[column]", "โฉนดเลขที่");
                    msg = msg.Replace("[row]", String.Join(",", checkUniqueTitleDeedNo));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullWbsCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullModelNames);
            rowErrors.AddRange(checkNullHouseNos);
            rowErrors.AddRange(checkNullTitleDeedNos);
            rowErrors.AddRange(checkFormatTitleDeedNos);
            rowErrors.AddRange(checkFormatLandNos);
            rowErrors.AddRange(checkFormatLandPortionNos);
            rowErrors.AddRange(checkFormatLandSurveyAreas);
            rowErrors.AddRange(checkFormatHouseNos);
            rowErrors.AddRange(checkFormatSaleAreas);
            rowErrors.AddRange(checkFormatEstimatePrices);
            rowErrors.AddRange(checkFormatBookNos);
            rowErrors.AddRange(checkFormatPageNos);
            rowErrors.AddRange(checkUnitNotFound);
            rowErrors.AddRange(checkUniqueTitleDeedNo);
            #endregion

            List<TitledeedDetail> titledeedsAdd = new List<TitledeedDetail>();
            List<TitledeedDetail> titledeedsUpdate = new List<TitledeedDetail>();
            List<Unit> unitsUpdate = new List<Unit>();

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;
            foreach (var item in titledeedDetailModel)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var existingTitleDeed = titleDeeds.Where(o => o.TitledeedNo == item.TitledeedNo).FirstOrDefault();
                    var unit = await DB.Units.Where(o => o.ProjectID == projectID && o.UnitNo == item.UnitNo && o.SAPWBSNo == item.WBSNo).FirstOrDefaultAsync();
                    if (existingTitleDeed == null)
                    {
                        if (unit != null)
                        {
                            TitledeedDetail titledeed = new TitledeedDetail();
                            titledeed.ProjectID = projectID;
                            titledeed.UnitID = unit.ID;
                            unit.HouseNo = item.HouseNo;
                            unit.HouseNoReceivedYear = item.HouseNoReceivedYear;
                            item.ToModel(ref titledeed);
                            titledeedsAdd.Add(titledeed);
                            unitsUpdate.Add(unit);
                            result.Success++;
                        }
                    }
                    else
                    {
                        if (unit != null)
                        {
                            existingTitleDeed.UnitID = unit.ID;
                            item.ToModel(ref existingTitleDeed);
                            unit.HouseNo = item.HouseNo;
                            unit.HouseNoReceivedYear = item.HouseNoReceivedYear;
                            titledeedsUpdate.Add(existingTitleDeed);
                            unitsUpdate.Add(unit);
                            result.Success++;
                        }
                    }
                }
                row++;
            }
            await DB.TitledeedDetails.AddRangeAsync(titledeedsAdd);
            DB.UpdateRange(titledeedsUpdate);
            DB.UpdateRange(unitsUpdate);
            await DB.SaveChangesAsync();
            result.Error = error;
            return result;

        }

        public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;

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

        public async Task<FileDTO> ExportExcelTitleDeedAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<TitleDeedQueryResult> query = DB.TitledeedDetails
                                                    .Where(o=>o.ProjectID==projectID)
                                                    .Include(o => o.Unit.UnitStatus)
                                                    .Select(o => new TitleDeedQueryResult
                                                    {
                                                        Titledeed = o,
                                                        Project = o.Project,
                                                        Unit = o.Unit,
                                                        Model = o.Unit.Model,
                                                        LandOffice = o.Unit.LandOffice,
                                                        LandStatus = o.LandStatus,
                                                        PreferStatus = o.PreferStatus,
                                                        UpdatedBy = o.UpdatedBy
                                                    });

            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_TitleDeed.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = TitledeedDetailExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = TitledeedDetailExcelModel._wbsNoIndex + 1;
                int _unitNoIndex = TitledeedDetailExcelModel._unitNoIndex + 1;
                int _modelNameIndex = TitledeedDetailExcelModel._modelNameIndex + 1;
                int _houseNoIndex = TitledeedDetailExcelModel._houseNoIndex + 1;
                int _yearGotHouseNo = TitledeedDetailExcelModel._houseNoReceivedYear + 1;
                int _titledeedNoIndex = TitledeedDetailExcelModel._titledeedNoIndex + 1;
                int _landNoIndex = TitledeedDetailExcelModel._landNoIndex + 1;
                int _landSurveyAreaIndex = TitledeedDetailExcelModel._landSurveyAreaIndex + 1;
                int _landPortionNoIndex = TitledeedDetailExcelModel._landPortionNoIndex + 1;
                int _pageNoIndex = TitledeedDetailExcelModel._pageNoIndex + 1;
                int _bookNoIndex = TitledeedDetailExcelModel._bookNoIndex + 1;
                int _titledeedAreaIndex = TitledeedDetailExcelModel._titledeedAreaIndex + 1;
                int _estimatePriceIndex = TitledeedDetailExcelModel._estimatePriceIndex + 1;
                int _remarkIndex = TitledeedDetailExcelModel._remarkIndex + 1;

                var Project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = Project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _modelNameIndex].Value = data[c - 2].Model?.NameTH;
                    worksheet.Cells[c, _houseNoIndex].Value = data[c - 2].Titledeed.Unit.HouseNo;
                    worksheet.Cells[c, _yearGotHouseNo].Value = data[c - 2].Titledeed.Unit.HouseNoReceivedYear;
                    worksheet.Cells[c, _titledeedNoIndex].Value = data[c - 2].Titledeed.TitledeedNo;
                    worksheet.Cells[c, _landNoIndex].Value = data[c - 2].Titledeed.LandNo;
                    worksheet.Cells[c, _landSurveyAreaIndex].Value = data[c - 2].Titledeed.LandSurveyArea;
                    worksheet.Cells[c, _landPortionNoIndex].Value = data[c - 2].Titledeed.LandPortionNo;
                    worksheet.Cells[c, _pageNoIndex].Value = data[c - 2].Titledeed.PageNo;
                    worksheet.Cells[c, _bookNoIndex].Value = data[c - 2].Titledeed.BookNo;
                    worksheet.Cells[c, _titledeedAreaIndex].Value = data[c - 2].Titledeed.TitledeedArea;
                    worksheet.Cells[c, _estimatePriceIndex].Value = data[c - 2].Titledeed.EstimatePrice;
                    worksheet.Cells[c, _remarkIndex].Value = data[c - 2].Titledeed.Remark;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = Project.ID + "_TitleDeed.xlsx";
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


        private async Task<Guid> TitleDeedDataStatus(Guid projectID)
        {
            var allTitleDeedDetail = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();
            var titleDeedDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var titleDeedDataStatusTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Transfer).Select(o => o.ID).FirstAsync();
            var titleDeedDataStatusMasterCenterID = titleDeedDataStatusPrepareMasterCenterID;
            if (allTitleDeedDetail.TrueForAll(o => o.TitledeedNo != null
                                         && o.UnitID != null
                                         && o.AddressID != null))
            {
                titleDeedDataStatusMasterCenterID = titleDeedDataStatusTransferMasterCenterID;
            }

            return titleDeedDataStatusMasterCenterID;
        }

        public async Task UpdateMultipleHouseNosAsync(Guid projectID, UpdateMultipleHouseNoParam input)
        {
            ValidateException ex = new ValidateException();
            //validate house no
            if (!input.FromHouseNo.IsOnlyNumberWithSpecialCharacter(true))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(UpdateMultipleHouseNoParam.FromHouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            var units = await DB.Units
                .Where(c => c.ProjectID == projectID && (String.Compare(c.UnitNo, input.FromUnit.UnitNo) >= 0) && (String.Compare(c.UnitNo, input.ToUnit.UnitNo) <= 0))
                .OrderBy(o => o.UnitNo)
                .ToListAsync();
            string houseNo = input.FromHouseNo;
            int runningHouseNo = 1;
            if (input.FromHouseNo.Contains('/'))
            {
                var lastSlashIndex = input.FromHouseNo.LastIndexOf('/');
                var beforeSlash = input.FromHouseNo.Substring(0, lastSlashIndex);
                var afterSlash = input.FromHouseNo.Substring(lastSlashIndex + 1, input.FromHouseNo.Length - (lastSlashIndex + 1));
                houseNo = beforeSlash;
                if (!int.TryParse(afterSlash, out runningHouseNo))
                {
                    runningHouseNo = 1;
                }
            }
            List<string> houseNos = new List<string>();
            foreach (var item in units)
            {
                houseNos.Add($"{houseNo}/{runningHouseNo++}");
            }

            //validate unique
            var hasHouseNo = await DB.Units.Where(o => o.ProjectID == projectID && houseNos.Contains(o.HouseNo)).AnyAsync();
            if (hasHouseNo)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(UpdateMultipleHouseNoParam.FromHouseNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", string.Join(',', houseNos));
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            foreach (var item in units)
            {
                item.HouseNo = item.HouseNo;
                item.HouseNoReceivedYear = item.HouseNoReceivedYear;
            }
            DB.UpdateRange(units);
            await DB.SaveChangesAsync();

        }

        public async Task UpdateMultipleLandOfficesAsync(Guid projectID, UpdateMultipleLandOfficeParam input)
        {
            var units = await DB.Units
                .Where(c => c.ProjectID == projectID && (String.Compare(c.UnitNo, input.FromUnit.UnitNo) >= 0) && (String.Compare(c.UnitNo, input.ToUnit.UnitNo) <= 0))
                .ToListAsync();
            foreach (var unit in units)
            {
                unit.LandOfficeID = input.LandOffice.Id;
            }
            DB.UpdateRange(units);
            await DB.SaveChangesAsync();
        }

    }
}
