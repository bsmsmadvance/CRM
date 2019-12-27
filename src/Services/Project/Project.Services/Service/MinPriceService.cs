using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Project.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class MinPriceService : IMinPriceService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        public MinPriceService(IConfiguration configuration, DatabaseContext db)
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
        public async Task<MinPricePaging> GetMinPriceListAsync(Guid projectID, MinPriceFilter filter, PageParam pageParam, MinPriceSortByParam sortByParam)
        {
            var query = await DB.MinPrices.GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                                    (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })
                                                    .Where(x => x.Minprice.ProjectID == projectID)
                                                    .Select(o => new MinPriceQueryResult
                                                    {
                                                        MinPrice = o.Minprice,
                                                        MinPriceType = o.Minprice.MinPriceType,
                                                        DocType = o.Minprice.DocType,
                                                        Unit = o.Minprice.Unit,
                                                        UpdatedBy = o.Minprice.UpdatedBy,
                                                        Titledeed = o.TitleDeed.FirstOrDefault()
                                                    }).ToListAsync();

            query = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                Titledeed = o.Select(p => p.Titledeed).FirstOrDefault(),
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault(),
                UpdatedBy = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.UpdatedBy).FirstOrDefault()
            }).ToList();



            var results = query.Select(o => MinPriceDTO.CreateFromQueryResult(o)).ToList();

            #region Filter
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                results = results.Where(x => (x.Unit.UnitNo == null ? string.Empty : x.Unit.UnitNo.ToLower()).Contains(filter.UnitNo.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                results = results.Where(x => (x.Unit.HouseNo == null ? string.Empty : x.Unit.HouseNo.ToLower()).Contains(filter.HouseNo.ToLower())).ToList();
            }

            if (filter.SaleAreaFrom != null)
            {
                results = results.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom).ToList();
            }
            if (filter.SaleAreaTo != null)
            {
                results = results.Where(x => x.Unit.SaleArea <= filter.SaleAreaTo).ToList();
            }
            if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
            {
                results = results.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom
                                    && x.Unit.SaleArea <= filter.SaleAreaTo).ToList();
            }

            if (filter.SalePriceFrom != null)
            {
                results = results.Where(x => x.SalePrice >= filter.SalePriceFrom).ToList();
            }
            if (filter.SalePriceTo != null)
            {
                results = results.Where(x => x.SalePrice <= filter.SalePriceTo).ToList();
            }
            if (filter.SalePriceFrom != null && filter.SalePriceTo != null)
            {
                results = results.Where(x => x.SalePrice >= filter.SalePriceFrom
                                    && x.SalePrice <= filter.SalePriceTo).ToList();
            }

            if (filter.CostFrom != null)
            {
                results = results.Where(x => x.Cost >= filter.CostFrom).ToList();
            }
            if (filter.CostTo != null)
            {
                results = results.Where(x => x.Cost <= filter.CostTo).ToList();
            }
            if (filter.CostFrom != null && filter.SalePriceTo != null)
            {
                results = results.Where(x => x.Cost >= filter.CostFrom
                                    && x.Cost <= filter.CostTo).ToList();
            }

            if (filter.ROIMinpriceFrom != null)
            {
                results = results.Where(x => x.ROIMinprice >= filter.ROIMinpriceFrom).ToList();
            }
            if (filter.ROIMinpriceTo != null)
            {
                results = results.Where(x => x.ROIMinprice <= filter.ROIMinpriceTo).ToList();
            }
            if (filter.ROIMinpriceFrom != null && filter.ROIMinpriceTo != null)
            {
                results = results.Where(x => x.ROIMinprice >= filter.ROIMinpriceFrom
                                    && x.ROIMinprice <= filter.ROIMinpriceTo).ToList();
            }

            if (filter.ApprovedMinPriceFrom != null)
            {
                results = results.Where(x => x.ApprovedMinPrice >= filter.ApprovedMinPriceFrom).ToList();
            }
            if (filter.ApprovedMinPriceTo != null)
            {
                results = results.Where(x => x.ApprovedMinPrice <= filter.ApprovedMinPriceTo).ToList();
            }
            if (filter.ApprovedMinPriceFrom != null && filter.ApprovedMinPriceTo != null)
            {
                results = results.Where(x => x.ApprovedMinPrice >= filter.ApprovedMinPriceFrom
                                    && x.ApprovedMinPrice <= filter.ApprovedMinPriceTo).ToList();
            }

            if (filter.TitledeedAreaFrom != null)
            {
                results = results.Where(x => x.TitleDeed?.TitledeedArea >= filter.TitledeedAreaFrom).ToList();
            }
            if (filter.TitledeedAreaTo != null)
            {
                results = results.Where(x => x.TitleDeed?.TitledeedArea <= filter.TitledeedAreaTo).ToList();
            }
            if (filter.TitledeedAreaFrom != null && filter.SalePriceTo != null)
            {
                results = results.Where(x => x.TitleDeed?.TitledeedArea >= filter.TitledeedAreaFrom
                                    && x.TitleDeed?.TitledeedArea <= filter.TitledeedAreaTo).ToList();
            }



            if (!string.IsNullOrEmpty(filter.MinPriceTypeKey))
            {
                var minPriceTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.MinPriceTypeKey
                                                                       && x.MasterCenterGroupKey == "MinPriceType")
                                                                      .Select(x => x.ID).FirstAsync();
                results = results.Where(x => x.MinPriceType?.Id == minPriceTypeMasterCenterID).ToList();
            }

            if (!string.IsNullOrEmpty(filter.DocTypeKey))
            {
                var docTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.DocTypeKey
                                                                       && x.MasterCenterGroupKey == "DocType")
                                                                      .Select(x => x.ID).FirstAsync();
                results = results.Where(x => x.DocType?.Id == docTypeMasterCenterID).ToList();
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                results = results.Where(x => x.UpdatedBy.Contains(filter.UpdatedBy)).ToList();
            }
            if (filter.UpdatedFrom != null)
            {
                results = results.Where(x => x.Updated >= filter.UpdatedFrom).ToList();
            }
            if (filter.UpdatedTo != null)
            {
                results = results.Where(x => x.Updated <= filter.UpdatedTo).ToList();
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                results = results.Where(x => x.Updated >= filter.UpdatedFrom && x.Updated <= filter.UpdatedTo).ToList();
            }
            #endregion

            MinPriceDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<MinPriceDTO>(pageParam, ref results);


            return new MinPricePaging()
            {
                PageOutput = pageOutput,
                MinPrices = results
            };
        }

        public async Task<MinPriceDTO> GetMinPriceAsync(Guid projectID, Guid id)
        {
            var model = await DB.MinPrices.GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                                (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })
                                                .Where(x => x.Minprice.ProjectID == projectID && x.Minprice.ID == id)
                                                .Select(x => new MinPriceQueryResult
                                                {
                                                    MinPrice = x.Minprice,
                                                    MinPriceType = x.Minprice.MinPriceType,
                                                    DocType = x.Minprice.DocType,
                                                    Unit = x.Minprice.Unit,
                                                    UpdatedBy = x.Minprice.UpdatedBy,
                                                    Titledeed = x.TitleDeed.FirstOrDefault()
                                                }).FirstAsync();

            var result = MinPriceDTO.CreateFromQueryResult(model);
            return result;
        }

        public async Task<MinPriceDTO> CreateMinPriceAsync(Guid projectID, MinPriceDTO input)
        {
            MinPrice model = new MinPrice();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.ActiveDate = DateTime.Now;

            await DB.MinPrices.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetMinPriceAsync(projectID, model.ID);
            return result;
        }

        public async Task<MinPriceDTO> UpdateMinPriceAsync(Guid projectID, Guid id, MinPriceDTO input)
        {
            MinPrice model = new MinPrice();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            model.ActiveDate = DateTime.Now;
            model.ApprovedMinPrice = await GetApproveMinPrice(model.UnitID);

            await DB.MinPrices.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetMinPriceAsync(projectID, model.ID);
            return result;
        }

        public async Task<MinPrice> DeleteMinPriceAsync(Guid projectID, Guid id)
        {
            var model = await DB.MinPrices.Where(x => x.ProjectID == projectID && x.ID == id).FirstAsync();

            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<MinPriceExcelDTO> ImportMinPriceAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // 1,2,3,4
            var err0070 = await DB.ErrorMessages.Where(o => o.Key == "ERR0070").FirstAsync();

            var result = new MinPriceExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 5)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 2;
            var error = 0;

            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var minPriceTypes = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceType).ToListAsync();
            var formatPriceTypes = new List<string> { "1", "2", "3", "4" };
            var checkNullWbsCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var checkNullMinimumPriceTypes = new List<string>();
            var checkNullMinimumPrices = new List<string>();
            var checkUnitNotFounds = new List<string>();
            var checkFormatMinimumPrices = new List<string>();
            var checkFormatMinimumPriceTypes = new List<string>();
            //Read Excel Model
            var minPriceExcelModels = new List<MinPriceExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = MinPriceExcelModel.CreateFromDataRow(r);
                minPriceExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSCode))
                {
                    checkNullWbsCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var unit = units.Where(o => o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSCode).FirstOrDefault();
                    if (unit == null)
                    {
                        checkUnitNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(r[MinPriceExcelModel._minimumPrice].ToString()))
                {
                    checkNullMinimumPrices.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[MinPriceExcelModel._minimumPrice].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatMinimumPrices.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(excelModel.MinimumPriceType))
                {
                    checkNullMinimumPriceTypes.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!formatPriceTypes.Contains(excelModel.MinimumPriceType))
                    {
                        checkFormatMinimumPriceTypes.Add((row).ToString());
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

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (minPriceExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            List<MinPrice> minPrices = new List<MinPrice>();
            //Update Data
            #region Add Result Validate
            if (checkNullWbsCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
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

            if (checkNullMinimumPrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullMinimumPriceTypes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkNullMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkUnitNotFounds.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatMinimumPrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "Minimum Price");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatMinimumPriceTypes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0070.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0070.Message.Replace("[column]", "Minimum Price Type");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatMinimumPriceTypes));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullWbsCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullMinimumPriceTypes);
            rowErrors.AddRange(checkNullMinimumPrices);
            rowErrors.AddRange(checkUnitNotFounds);
            rowErrors.AddRange(checkFormatMinimumPrices);
            rowErrors.AddRange(checkFormatMinimumPriceTypes);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();

            row = 2;
            foreach (var item in minPriceExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Where(o => o.ProjectID == projectID && o.UnitNo == item.UnitNo && o.SAPWBSNo == item.WBSCode).FirstOrDefault();
                    if (unit != null)
                    {
                        MinPrice min = new MinPrice();
                        Guid? minpricetypemastercenterID = minPriceTypes.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceType && o.Key == item.MinimumPriceType).Select(o => o.ID).FirstOrDefault();
                        min.MinPriceTypeMasterCenterID = minpricetypemastercenterID;
                        min.Cost = Convert.ToDecimal(item.MinimumPrice);
                        if (item.MinimumPriceType == "2")
                        {
                            min.ROIMinprice = Convert.ToDecimal(item.MinimumPrice);
                        }
                        else
                        {
                            min.ROIMinprice = await GetROIMinprice(unit.ID);
                        }
                        min.SalePrice = await GetSalePrice(unit.ID);
                        min.ApprovedMinPrice = await GetApproveMinPrice(unit.ID);
                        min.ActiveDate = DateTime.Now;
                        min.ProjectID = projectID;
                        min.UnitID = unit.ID;
                        minPrices.Add(min);
                    }
                }
                row++;
            }
            await DB.MinPrices.AddRangeAsync(minPrices);
            await DB.SaveChangesAsync();
            result.Success = minPrices.Count();
            result.Error = error;
            return result;
        }

        public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
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

        public async Task<FileDTO> ExportExcelMinPriceAsync(Guid projectID, MinPriceFilter filter, MinPriceSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            var query = await DB.MinPrices.GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                                        (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })
                                                        .Where(x => x.Minprice.ProjectID == projectID)
                                                        .Select(o => new MinPriceQueryResult
                                                        {
                                                            MinPrice = o.Minprice,
                                                            MinPriceType = o.Minprice.MinPriceType,
                                                            DocType = o.Minprice.DocType,
                                                            Unit = o.Minprice.Unit,
                                                            Titledeed = o.TitleDeed.FirstOrDefault()
                                                        }).ToListAsync();

            query = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                Titledeed = o.Select(p => p.Titledeed).FirstOrDefault(),
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
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

            if (filter.SaleAreaFrom != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom).ToList();
            }
            if (filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea <= filter.SaleAreaTo).ToList();
            }
            if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
            {
                query = query.Where(x => x.Unit.SaleArea >= filter.SaleAreaFrom
                                    && x.Unit.SaleArea <= filter.SaleAreaTo).ToList();
            }

            if (filter.SalePriceFrom != null)
            {
                query = query.Where(x => x.MinPrice.SalePrice >= filter.SalePriceFrom).ToList();
            }
            if (filter.SalePriceTo != null)
            {
                query = query.Where(x => x.MinPrice.SalePrice <= filter.SalePriceTo).ToList();
            }
            if (filter.SalePriceFrom != null && filter.SalePriceTo != null)
            {
                query = query.Where(x => x.MinPrice.SalePrice >= filter.SalePriceFrom
                                    && x.MinPrice.SalePrice <= filter.SalePriceTo).ToList();
            }

            if (filter.CostFrom != null)
            {
                query = query.Where(x => x.MinPrice.Cost >= filter.CostFrom).ToList();
            }
            if (filter.CostTo != null)
            {
                query = query.Where(x => x.MinPrice.Cost <= filter.CostTo).ToList();
            }
            if (filter.CostFrom != null && filter.SalePriceTo != null)
            {
                query = query.Where(x => x.MinPrice.Cost >= filter.CostFrom
                                    && x.MinPrice.Cost <= filter.CostTo).ToList();
            }

            if (filter.ROIMinpriceFrom != null)
            {
                query = query.Where(x => x.MinPrice.ROIMinprice >= filter.ROIMinpriceFrom).ToList();
            }
            if (filter.ROIMinpriceTo != null)
            {
                query = query.Where(x => x.MinPrice.ROIMinprice <= filter.ROIMinpriceTo).ToList();
            }
            if (filter.ROIMinpriceFrom != null && filter.ROIMinpriceTo != null)
            {
                query = query.Where(x => x.MinPrice.ROIMinprice >= filter.ROIMinpriceFrom
                                    && x.MinPrice.ROIMinprice <= filter.ROIMinpriceTo).ToList();
            }

            if (filter.ApprovedMinPriceFrom != null)
            {
                query = query.Where(x => x.MinPrice.ApprovedMinPrice >= filter.ApprovedMinPriceFrom).ToList();
            }
            if (filter.ApprovedMinPriceTo != null)
            {
                query = query.Where(x => x.MinPrice.ApprovedMinPrice <= filter.ApprovedMinPriceTo).ToList();
            }
            if (filter.ApprovedMinPriceFrom != null && filter.ApprovedMinPriceTo != null)
            {
                query = query.Where(x => x.MinPrice.ApprovedMinPrice >= filter.ApprovedMinPriceFrom
                                    && x.MinPrice.ApprovedMinPrice <= filter.ApprovedMinPriceTo).ToList();
            }

            if (filter.TitledeedAreaFrom != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom).ToList();
            }
            if (filter.TitledeedAreaTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo).ToList();
            }
            if (filter.TitledeedAreaFrom != null && filter.SalePriceTo != null)
            {
                query = query.Where(x => x.Titledeed.TitledeedArea >= filter.TitledeedAreaFrom
                                    && x.Titledeed.TitledeedArea <= filter.TitledeedAreaTo).ToList();
            }



            if (!string.IsNullOrEmpty(filter.MinPriceTypeKey))
            {
                var minPriceTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.MinPriceTypeKey
                                                                       && x.MasterCenterGroupKey == "MinPriceType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.MinPrice.MinPriceTypeMasterCenterID == minPriceTypeMasterCenterID).ToList();
            }

            if (!string.IsNullOrEmpty(filter.DocTypeKey))
            {
                var docTypeMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.DocTypeKey
                                                                       && x.MasterCenterGroupKey == "DocType")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.MinPrice.DocTypeMasterCenterID == docTypeMasterCenterID).ToList();
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.MinPrice.UpdatedBy.DisplayName.Contains(filter.UpdatedBy)).ToList();
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.MinPrice.Updated >= filter.UpdatedFrom).ToList();
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MinPrice.Updated <= filter.UpdatedTo).ToList();
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MinPrice.Updated >= filter.UpdatedFrom && x.MinPrice.Updated <= filter.UpdatedTo).ToList();
            }
            #endregion

            var results = query.Select(o => MinPriceDTO.CreateFromQueryResult(o)).ToList();

            MinPriceDTO.SortBy(sortByParam, ref results);

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_MinPrice.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = MinPriceExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = MinPriceExcelModel._wbsCodeIndex + 1;
                int _unitNoIndex = MinPriceExcelModel._unitNoIndex + 1;
                int _minimumPrice = MinPriceExcelModel._minimumPrice + 1;
                int _minimumPriceType = MinPriceExcelModel._minimumPriceType + 1;

                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = results[c - 2].Unit?.SapwbsNo;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].Unit?.UnitNo;
                    worksheet.Cells[c, _minimumPrice].Value = results[c - 2].Cost;
                    worksheet.Cells[c, _minimumPriceType].Value = results[c - 2].MinPriceType?.Key;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_MinPrice.xlsx";
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

        private async Task<Guid> MinPriceDataStatus(Guid projectID)
        {
            var query = await DB.MinPrices.GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                              (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })
                                              .Where(x => x.Minprice.ProjectID == projectID)
                                              .Select(o => new MinPriceQueryResult
                                              {
                                                  MinPrice = o.Minprice,
                                                  MinPriceType = o.Minprice.MinPriceType,
                                                  DocType = o.Minprice.DocType,
                                                  Unit = o.Minprice.Unit,
                                                  Titledeed = o.TitleDeed.FirstOrDefault()
                                              }).ToListAsync();

            query = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                Titledeed = o.Select(p => p.Titledeed).FirstOrDefault(),
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).ToList();

            var minPriceDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var minPriceDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
            var minPriceDataStatusMasterCenterID = minPriceDataStatusPrepareMasterCenterID;


            var results = query.Select(o => MinPriceDTO.CreateFromQueryResult(o)).ToList();

            if (results.TrueForAll(o => o.ApprovedMinPrice != null
                                  && o.MinPriceType != null))
            {
                minPriceDataStatusMasterCenterID = minPriceDataStatusSaleMasterCenterID;
            }

            return minPriceDataStatusMasterCenterID;

        }

        private async Task<decimal?> GetApproveMinPrice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      MinPriceType = o.MinPriceType,
                                      DocType = o.DocType,
                                      Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice?.ApprovedMinPrice;
        }
        private async Task<decimal?> GetROIMinprice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      MinPriceType = o.MinPriceType,
                                      DocType = o.DocType,
                                      Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice?.ROIMinprice;
        }

        private async Task<decimal?> GetSalePrice(Guid? unitID)
        {
            var query = await DB.MinPrices
                                  .Where(o => o.UnitID == unitID)
                                  .Select(o => new MinPriceQueryResult
                                  {
                                      MinPrice = o,
                                      MinPriceType = o.MinPriceType,
                                      DocType = o.DocType,
                                      Unit = o.Unit,
                                  }).ToListAsync();

            var queryResult = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
            {
                Unit = o.Key,
                MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault()
            }).FirstOrDefault();


            return queryResult?.MinPrice?.SalePrice;
        }
    }
}
