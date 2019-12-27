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
    public class BudgetPromotionService : IBudgetPromotionService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        private FileHelper FileHelperSap;

        public BudgetPromotionService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioWithSSL = Configuration["Minio:WithSSL"];

            var minioSapEndpoint = Configuration["MinioSAP:Endpoint"];
            var minioSapAccessKey = Configuration["MinioSAP:AccessKey"];
            var minioSapSecretKey = Configuration["MinioSAP:SecretKey"];
            var minioSapWithSSL = Configuration["MinioSAP:WithSSL"];

            this.FileHelperSap = new FileHelper(minioSapEndpoint, minioSapAccessKey, minioSapSecretKey, "bgt", "", minioSapWithSSL == "true");

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, minioWithSSL == "true");
        }

        public async Task<BudgetPromotionPaging> GetBudgetPromotionListAsync(Guid projectID, BudgetPromotionFilter filter, PageParam pageParam, BudgetPromotionSortByParam sortByParam)
        {
            var query = await DB.BudgetPromotions.Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID)
                                                 .Select(o => new
                                                 {
                                                     BudgetPromotion = o,
                                                     Unit = o.Unit
                                                 }).ToListAsync();

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).ToList();

            var results = data.Select(async o => await BudgetPromotionDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            #region Filter

            #region SaleArea
            if (filter.SaleAreaFrom != null)
            {
                results = results.Where(o => o.Unit.SaleArea >= filter.SaleAreaFrom).ToList();
            }
            if (filter.SaleAreaTo != null)
            {
                results = results.Where(o => o.Unit.SaleArea <= filter.SaleAreaTo).ToList();
            }
            #endregion

            #region TotalPrice
            if (filter.TotalPriceFrom != null)
            {
                results = results.Where(o => o.TotalPrice >= filter.TotalPriceFrom).ToList();
            }
            if (filter.TotalPriceTo != null)
            {
                results = results.Where(o => o.TotalPrice <= filter.TotalPriceTo).ToList();
            }
            if (filter.TotalPriceFrom != null && filter.TotalPriceTo != null)
            {
                results = results.Where(o => o.TotalPrice >= filter.TotalPriceFrom &&
                                             o.TotalPrice <= filter.TotalPriceTo).ToList();
            }
            #endregion

            #region PromotionPriceFrom
            if (filter.PromotionPriceFrom != null)
            {
                results = results.Where(o => o.PromotionPrice >= filter.PromotionPriceFrom).ToList();
            }
            if (filter.PromotionPriceTo != null)
            {
                results = results.Where(o => o.PromotionPrice <= filter.PromotionPriceTo).ToList();
            }
            if (filter.PromotionPriceFrom != null && filter.PromotionPriceTo != null)
            {
                results = results.Where(o => o.PromotionPrice >= filter.PromotionPriceFrom &&
                                             o.PromotionPrice <= filter.PromotionPriceTo).ToList();
            }
            #endregion

            #region PromotionTransferPrice
            if (filter.PromotionTransferPriceFrom != null)
            {
                results = results.Where(o => o.PromotionTransferPrice >= filter.PromotionTransferPriceFrom).ToList();
            }
            if (filter.PromotionTransferPriceTo != null)
            {
                results = results.Where(o => o.PromotionTransferPrice <= filter.PromotionTransferPriceTo).ToList();
            }
            if (filter.PromotionTransferPriceFrom != null && filter.PromotionTransferPriceTo != null)
            {
                results = results.Where(o => o.PromotionTransferPrice >= filter.PromotionTransferPriceFrom &&
                                             o.PromotionTransferPrice <= filter.PromotionTransferPriceTo).ToList();
            }
            #endregion

            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                results = results.Where(x => x.Unit.UnitNo.Contains(filter.UnitNo)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.HouseNo))
            {
                results = results.Where(x => x.Unit.HouseNo.Contains(filter.HouseNo)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.WBSCRM_P))
            {
                results = results.Where(x => x.Unit.SapwbsObject_P.Contains(filter.WBSCRM_P)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.WBSSAP_P))
            {
                results = results.Where(x => x.Unit.SapwbsNo_P.Contains(filter.WBSSAP_P)).ToList();
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
            if (!string.IsNullOrEmpty(filter.SyncJob_StatusKey))
            {
                var budgetPromotionSyncStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.SyncJob_StatusKey
                                                                       && x.MasterCenterGroupKey == "BudgetPromotionSyncStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                results = results.Where(o => o.SyncJob?.Status?.Id == budgetPromotionSyncStatusMasterCenterID).ToList();
            }
            #endregion

            BudgetPromotionDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<BudgetPromotionDTO>(pageParam, ref results);

            return new BudgetPromotionPaging()
            {
                PageOutput = pageOutput,
                BudgetPromotions = results
            };
        }

        public async Task<BudgetPromotionDTO> GetBudgetPromotionAsync(Guid projectID, Guid unitID)
        {
            var query = await DB.BudgetPromotions.Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID && o.UnitID == unitID)
                                                 .Select(o => new
                                                 {
                                                     BudgetPromotion = o,
                                                     Unit = o.Unit
                                                 }).ToListAsync();

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).FirstOrDefault();

            var result = await BudgetPromotionDTO.CreateFromQueryResultAsync(data, DB);
            return result;
        }

        public async Task<BudgetPromotionDTO> CreateBudgetPromotionAsync(Guid projectID, BudgetPromotionDTO input)
        {
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            BudgetPromotion modelSale = new BudgetPromotion();
            input.ToModelSale(ref modelSale);
            modelSale.ProjectID = projectID;
            modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;

            BudgetPromotion modelTransfer = new BudgetPromotion();
            input.ToModelTransfer(ref modelTransfer);
            modelTransfer.ProjectID = projectID;
            modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;

            await DB.BudgetPromotions.AddAsync(modelSale);
            await DB.BudgetPromotions.AddAsync(modelTransfer);
            await DB.SaveChangesAsync();

            var listBudgetPromotion = new List<BudgetPromotion>();

            listBudgetPromotion.Add(modelTransfer);
            listBudgetPromotion.Add(modelSale);
            await this.CreateNewSyncJobAsync(listBudgetPromotion);


            var result = await this.GetBudgetPromotionAsync(projectID, input.Unit.Id.Value);

            return result;
        }

        public async Task<BudgetPromotionDTO> UpdateBudgetPromotionAsync(Guid projectID, Guid unitID, BudgetPromotionDTO input)
        {
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

            BudgetPromotion modelSale = new BudgetPromotion();
            input.ToModelSale(ref modelSale);
            modelSale.ProjectID = projectID;
            modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;

            BudgetPromotion modelTransfer = new BudgetPromotion();
            input.ToModelTransfer(ref modelTransfer);
            modelTransfer.ProjectID = projectID;
            modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;

            await DB.BudgetPromotions.AddAsync(modelSale);
            await DB.BudgetPromotions.AddAsync(modelTransfer);
            await DB.SaveChangesAsync();

            var listBudgetPromotion = new List<BudgetPromotion>();
            listBudgetPromotion.Add(modelTransfer);
            listBudgetPromotion.Add(modelSale);
            await this.CreateNewSyncJobAsync(listBudgetPromotion);

            var result = await this.GetBudgetPromotionAsync(projectID, unitID);
            return result;
        }

        public async Task DeleteBudgetPromotionAsync(Guid projectID, Guid unitID)
        {
            var models = await DB.BudgetPromotions.Where(o => o.ProjectID == projectID && o.ID == unitID).ToListAsync();
            foreach (var item in models)
            {
                item.IsDeleted = true;
                DB.Entry(item).State = EntityState.Modified;
            }
            await DB.SaveChangesAsync();
        }

        public async Task<BudgetPromotionExcelDTO> ImportBudgetPromotionAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();

            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();

            BudgetPromotionExcelDTO result = new BudgetPromotionExcelDTO { Error = 0, Success = 0, ErrorMessages = new List<string>() };
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 8)
            {
                throw new Exception("Invalid File Format");
            }

            var checkNullUnitNos = new List<string>();
            var checkNullWbsNumber = new List<string>();
            var unitNotFounds = new List<string>();
            var checkFormatBudgetPromotionTransfers = new List<string>();
            var checkFormatBudgetPromotionSales = new List<string>();

            var row = 2;
            var error = 0;
            //Read Excel Model
            var budgetPromotionExcelModel = new List<BudgetPromotionExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = BudgetPromotionExcelModel.CreateFromDataRow(r);
                budgetPromotionExcelModel.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWbsNumber.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[BudgetPromotionExcelModel._promotionTransferPriceIndex].ToString()))
                {
                    if (!r[BudgetPromotionExcelModel._promotionTransferPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatBudgetPromotionTransfers.Add((row).ToString());
                        isError = true;
                    }
                }
                if (!string.IsNullOrEmpty(r[BudgetPromotionExcelModel._promotionPriceIndex].ToString()))
                {
                    if (!r[BudgetPromotionExcelModel._promotionPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatBudgetPromotionSales.Add((row).ToString());
                        isError = true;
                    }
                }
                var unit = units.Where(o => o.ProjectID == projectID && o.SAPWBSNo_P == excelModel.WBSNo && o.UnitNo == excelModel.UnitNo).FirstOrDefault();
                if (unit == null)
                {
                    unitNotFounds.Add((row).ToString());
                    isError = true;
                }
                #endregion


                if (isError)
                {
                    error++;
                }
                row++;
            }
            ValidateException ex = new ValidateException();
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            if (budgetPromotionExcelModel.Any(o => o.ProjectNo != project.ProjectNo))
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
            if (checkNullWbsNumber.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS Number");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsNumber));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS Number");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsNumber));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatBudgetPromotionTransfers.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "โปรโอน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBudgetPromotionTransfers));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "โปรโอน");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBudgetPromotionTransfers));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatBudgetPromotionSales.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "โปรขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBudgetPromotionSales));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "โปรขาย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBudgetPromotionSales));
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
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", unitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }

            #endregion

            #region RowError
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullWbsNumber);
            rowErrors.AddRange(unitNotFounds);
            rowErrors.AddRange(checkFormatBudgetPromotionTransfers);
            rowErrors.AddRange(checkFormatBudgetPromotionSales);
            #endregion

            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            List<BudgetPromotion> budgetPromotion = new List<BudgetPromotion>();
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

            row = 2;
            var listBudgetPromotion = new List<BudgetPromotion>();
            foreach (var item in budgetPromotionExcelModel)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Where(o => o.ProjectID == projectID && o.SAPWBSNo_P == item.WBSNo && o.UnitNo == item.UnitNo).FirstOrDefault();
                    if (unit != null)
                    {
                        BudgetPromotion modelSale = new BudgetPromotion();
                        item.ToModelSale(ref modelSale);
                        modelSale.ProjectID = projectID;
                        modelSale.UnitID = unit.ID;
                        modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;

                        BudgetPromotion modelTransfer = new BudgetPromotion();
                        item.ToModelTransfer(ref modelTransfer);
                        modelTransfer.ProjectID = projectID;
                        modelTransfer.UnitID = unit.ID;
                        modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;

                        budgetPromotion.Add(modelSale);
                        budgetPromotion.Add(modelTransfer);

                        listBudgetPromotion.Add(modelTransfer);
                        listBudgetPromotion.Add(modelSale);
                        result.Success++;
                    }
                }
                row++;
            }
            await DB.BudgetPromotions.AddRangeAsync(budgetPromotion);
            await DB.SaveChangesAsync();
            if (listBudgetPromotion.Count() > 0)
            {
                await this.CreateNewSyncJobAsync(listBudgetPromotion);
            }
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

        public async Task<FileDTO> ExportExcelBudgetPromotionAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            var query = await DB.BudgetPromotions.Where(o => o.ProjectID == projectID)
                                                 .Select(o => new
                                                 {
                                                     BudgetPromotion = o,
                                                     Unit = o.Unit
                                                 }).ToListAsync();

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
            }).ToList();


            var results = data.Select(async o => await BudgetPromotionDTO.CreateFromQueryResultAsync(o, DB)).Select(o => o.Result).ToList();

            results = results.OrderBy(o => o.Unit.UnitNo).ToList();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_Budget.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = BudgetPromotionExcelModel._projectNoIndex + 1;
                int _unitNoIndex = BudgetPromotionExcelModel._unitNoIndex + 1;
                int _houseNoIndex = BudgetPromotionExcelModel._houseNoIndex + 1;
                int _houseTypeIndex = BudgetPromotionExcelModel._houseTypeIndex + 1;
                int _wbsNoIndex = BudgetPromotionExcelModel._wbsNoIndex + 1;
                int _promotionPriceIndex = BudgetPromotionExcelModel._promotionPriceIndex + 1;
                int _promotionTransferPriceIndex = BudgetPromotionExcelModel._promotionTransferPriceIndex + 1;
                int _totalPriceIndex = BudgetPromotionExcelModel._totalPriceIndex + 1;


                var Project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < results.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = Project.ProjectNo;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].Unit.UnitNo;
                    worksheet.Cells[c, _houseNoIndex].Value = results[c - 2].Unit.HouseNo;
                    worksheet.Cells[c, _houseTypeIndex].Value = "";
                    worksheet.Cells[c, _wbsNoIndex].Value = results[c - 2].Unit.SapwbsNo_P;

                    worksheet.Cells[c, _promotionPriceIndex].Value = results[c - 2].PromotionPrice;
                    worksheet.Cells[c, _promotionTransferPriceIndex].Value = results[c - 2].PromotionTransferPrice;
                    worksheet.Cells[c, _totalPriceIndex].Value = results[c - 2].PromotionPrice + results[c - 2].PromotionTransferPrice;

                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = Project.ID + "_Budget.xlsx";
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
        /// สร้าง Job ใหม่ หลังจากมีการ Update ข้อมูล Budget
        /// </summary>
        /// <returns></returns>
        public async Task CreateNewSyncJobAsync(List<BudgetPromotion> input)
        {
            var model = new BudgetPromotionSyncJob();

            model.Status = BackgroundJobStatus.Waiting;

            var listSynItem = new List<BudgetPromotionSyncItem>();
            var temp = input.GroupBy(o => o.UnitID).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = DB.Units.Where(p => p.ID == o.Key).FirstOrDefault(),
                BudgetPromotions = o.Select(p => p).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var data = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).ToList();


            var budgetPromotionSyncStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            foreach (var item in data)
            {
                var unit = await DB.Units.Where(o => o.ID == item.Unit.ID).FirstOrDefaultAsync();
                var synItem = new BudgetPromotionSyncItem();
                synItem.BudgetPromotionSyncJobID = model.ID;
                synItem.SAPWBSObject_P = unit.SAPWBSObject_P;
                synItem.SaleBudgetPromotionID = item.BudgetPromotionSale.ID;
                synItem.TransferBudgetPromotionID = item.BudgetPromotionTransfer.ID;
                synItem.Amount = Convert.ToDecimal(item.BudgetPromotionSale.Budget + item.BudgetPromotionTransfer.Budget);
                synItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSyncingMasterCenterID;
                synItem.Retry = 0;
                synItem.Currency = "THB";
                listSynItem.Add(synItem);
            }

            await DB.BudgetPromotionSyncJobs.AddAsync(model);

            await DB.BudgetPromotionSyncItems.AddRangeAsync(listSynItem);

            await DB.SaveChangesAsync();

        }

        /// <summary>
        /// สั่ง Run Job ที่อยู่ในสถานะ Waiting เพื่อส่ง Text File ให้ SAP
        /// </summary>
        /// <returns></returns>
        public async Task RunWaitingSyncJobAsync()
        {
            try
            {
                var waitingSyncJobs = await DB.BudgetPromotionSyncJobs.Where(o => o.Status == BackgroundJobStatus.Waiting).ToListAsync();
                waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.InProgress);
                DB.BudgetPromotionSyncJobs.UpdateRange(waitingSyncJobs);
                await DB.SaveChangesAsync();

                var listBudgetPromotionSyncJob = new List<BudgetPromotionSyncJob>();

                foreach (var item in waitingSyncJobs)
                {
                    try
                    {
                        var syncItem = await DB.BudgetPromotionSyncItems.Where(o => o.BudgetPromotionSyncJobID == item.ID).ToListAsync();
                        item.FileName = "CRMBG_" + item.Created.Value.ToString("yyyyMMddHHmmssfff") + ".txt";
                        item.Status = BackgroundJobStatus.WaitingForResult;

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item1 in syncItem)
                            {
                                output.WriteLine(item.FileName + ";" + item1.ID + ";" + item.CreatedBy + ";" + item1.SAPWBSObject_P + ";" + item1.Created?.ToString("yyyyMMdd") + ";" + item1.Amount + ";" + item1.Currency);
                            }
                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = item.FileName;
                            string filePath = $"data/";
                            string contentType = "text/*";
                            await this.FileHelperSap.UploadFileFromStreamWithOutGuid(fileStream, "bgt", filePath, fileName, contentType);

                            DB.BudgetPromotionSyncJobs.Update(item);
                            await DB.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        item.Status = BackgroundJobStatus.Failed;
                        item.ErrorMessage = "Error occurs when write text file to SAP: " + ex.ToString();
                        DB.BudgetPromotionSyncJobs.Update(item);
                        await DB.SaveChangesAsync();
                    }
                }

                waitingSyncJobs.ForEach(o => o.Status = BackgroundJobStatus.Completed);
                DB.BudgetPromotionSyncJobs.UpdateRange(waitingSyncJobs);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// อ่าน Result จาก SAP เพื่ออัพเดตสถานะของ Job
        /// </summary>
        /// <returns></returns>
        public async Task ReadSyncResultFromSAPAsync()
        {
            var getFileNameFromResults = await this.FileHelperSap.GetListFile("bgt", "result/");
            var budgetPromotionSyncStatusSuccessMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Success).Select(o => o.ID).FirstAsync();
            var budgetPromotionSyncStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Retrying).Select(o => o.ID).FirstAsync();

            foreach (var item in getFileNameFromResults)
            {
                var fileName = item.Split("/").Last();
                if (fileName != "empty.txt")
                {
                    var temp = await FileHelperSap.DownLoadToTempFileAsync("bgt", "result/", fileName);
                    var listSyncResults = new List<BudgetPromotionSyncItemResult>();

                    using (StreamReader streamReader = new StreamReader(temp, Encoding.UTF8))
                    {
                        string line;
                        var content = new List<string>();
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            content.Add(line);
                        }
                        foreach (var data in content)
                        {
                            var detail = data.Split(';').ToList();
                            var syncResult = new BudgetPromotionSyncItemResult();
                            syncResult.BudgetPromotionSyncItemID = new Guid(detail[1]);
                            syncResult.IsError = detail[2].ToLower() == "x" ? true : false;
                            syncResult.ErrorCode = detail[3];
                            syncResult.ErrorDescription = detail[4];
                            syncResult.IsFMUpdateBudget = detail[5].ToLower() == "x" ? true : false;
                            syncResult.SAPWBSObject_P = detail[6];
                            syncResult.LastUpdateBudgetFromSAP = detail[7];
                            syncResult.UserSAP = detail[8];
                            syncResult.SAPCreateDateTime = DateTime.ParseExact(detail[9] + " " + detail[10], "yyyyMMdd HH:mm:ss", null);
                            listSyncResults.Add(syncResult);
                        }
                    }
                    foreach (var itemResults in listSyncResults.OrderBy(o => o.BudgetPromotionSyncItemID))
                    {
                        var syncItem = await DB.BudgetPromotionSyncItems.Where(o => o.ID == itemResults.BudgetPromotionSyncItemID).Include(o => o.SaleBudgetPromotion).FirstOrDefaultAsync();
                        if (syncItem != null)
                        {
                            var syncJob = await DB.BudgetPromotionSyncJobs.Where(o => o.ID == syncItem.BudgetPromotionSyncJobID).FirstOrDefaultAsync();
                            if (syncJob != null)
                            {
                                try
                                {
                                    if (Convert.ToDecimal(itemResults.LastUpdateBudgetFromSAP) == syncItem.Amount)
                                    {
                                        syncItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSuccessMasterCenterID;
                                    }
                                    else
                                    {
                                        syncItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusRetryingMasterCenterID;
                                    }
                                    var unit = await DB.Units.Where(o => o.ID == syncItem.SaleBudgetPromotion.UnitID).FirstOrDefaultAsync();
                                    unit.SAPBudgetProAmount = Convert.ToDecimal(itemResults.LastUpdateBudgetFromSAP);
                                    unit.SAPBudgetProUpdated = itemResults.SAPCreateDateTime;
                                    syncJob.Status = BackgroundJobStatus.Completed;
                                    syncJob.SAPResultFileName = fileName;
                                    DB.BudgetPromotionSyncJobs.Update(syncJob);
                                    DB.BudgetPromotionSyncItems.Update(syncItem);
                                    DB.Units.Update(unit);
                                }
                                catch (Exception ex)
                                {
                                    if (syncJob != null)
                                    {
                                        syncJob.Status = BackgroundJobStatus.Failed;
                                        syncJob.ErrorMessage = "Error occurs when read text file from SAP: " + ex.ToString();
                                        DB.BudgetPromotionSyncJobs.Update(syncJob);
                                        await DB.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                    var checkDatas = await DB.BudgetPromotionSyncItems.Where(o => listSyncResults.Select(p => p.BudgetPromotionSyncItemID).Contains(o.ID)).CountAsync();
                    if (checkDatas == listSyncResults.Count())
                    {
                        await DB.BudgetPromotionSyncItemResults.AddRangeAsync(listSyncResults);
                        await DB.SaveChangesAsync();
                        await this.FileHelperSap.MoveAndRemoveFileAsync("bgt", "result/" + fileName, "bgt", "result_backup/" + fileName);
                    }
                }
            }
        }

        /// <summary>
        /// สร้าง Job ใหม่สำหรับ Item ที่อยู่ในสถานะ Retry
        /// </summary>
        /// <returns></returns>
        public async Task CreateRetrySyncJobAsync()
        {
            var budgetPromotionSyncStatusRetryingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Retrying).Select(o => o.ID).FirstAsync();
            var budgetPromotionSyncStatusSyncingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus" && o.Key == BudgetPromotionSyncStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            var syncItems = await DB.BudgetPromotionSyncItems.Where(o => o.BudgetPromotionSyncStatusMasterCenterID == budgetPromotionSyncStatusRetryingMasterCenterID).ToListAsync();

            if (syncItems.Count() > 0)
            {
                var syncJob = new BudgetPromotionSyncJob();
                var listSyncitems = new List<BudgetPromotionSyncItem>();

                syncJob.Status = BackgroundJobStatus.Waiting;

                foreach (var item in syncItems)
                {
                    var synItem = new BudgetPromotionSyncItem();
                    synItem.BudgetPromotionSyncJobID = syncJob.ID;
                    synItem.SAPWBSObject_P = item.SAPWBSObject_P;
                    synItem.SaleBudgetPromotionID = item.SaleBudgetPromotionID;
                    synItem.TransferBudgetPromotionID = item.TransferBudgetPromotionID;
                    synItem.Amount = item.Amount;
                    synItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSyncingMasterCenterID;
                    synItem.Retry = ++item.Retry;
                    synItem.Currency = "THB";
                    listSyncitems.Add(synItem);
                }

                await DB.BudgetPromotionSyncJobs.AddAsync(syncJob);
                await DB.BudgetPromotionSyncItems.AddRangeAsync(listSyncitems);
                await DB.SaveChangesAsync();
            }
        }
    }
}
