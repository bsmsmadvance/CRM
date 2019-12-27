using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
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
    public class WaiveQCService : IWaiveQCService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public WaiveQCService(IConfiguration configuration, DatabaseContext db)
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

        public async Task<WaiveQCPaging> GetWaiveQCListAsync(Guid projectID, WaiveQCFilter filter, PageParam pageParam, WaiveQCSortByParam sortByParam)
        {
            IQueryable<WaiveQCQueryResult> query = DB.WaiveQCs.Where(o => o.ProjectID == projectID)
                                                              .Include(o => o.Unit.UnitStatus)
                                                              .Select(o => new WaiveQCQueryResult
                                                              {
                                                                  Unit = o.Unit,
                                                                  WaiveQC = o,
                                                                  UpdatedBy = o.UpdatedBy
                                                              });
            #region Filter

            #region ActualTransferDate
            if (filter.ActualTransferDateFrom != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom);
            }
            if (filter.ActualTransferDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
            }
            if (filter.ActualTransferDateFrom != null && filter.ActualTransferDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom
                                    && o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
            }
            #endregion

            #region WaiveQCeDate
            if (filter.WaiveQCDateFrom != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate >= filter.WaiveQCDateFrom);
            }
            if (filter.WaiveQCDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate <= filter.WaiveQCDateTo);
            }
            if (filter.WaiveQCDateFrom != null && filter.WaiveQCDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate >= filter.WaiveQCDateFrom
                                    && o.WaiveQC.WaiveQCDate <= filter.WaiveQCDateTo);
            }
            #endregion

            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.WaiveQC.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom && x.WaiveQC.Updated <= filter.UpdatedTo);
            }
            #endregion

            WaiveQCDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<WaiveQCQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => WaiveQCDTO.CreateFromQueryResult(o)).ToList();

            return new WaiveQCPaging()
            {
                PageOutput = pageOutput,
                WaiveQC = results
            };
        }

        public async Task<WaiveQCDTO> GetWaiveQCAsync(Guid projectID, Guid id)
        {
            var model = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id)
                                         .Include(o => o.Unit)
                                         .Include(o => o.Unit.UnitStatus)
                                         .Include(o => o.UpdatedBy)
                                         .FirstAsync();
            var result = WaiveQCDTO.CreateFromModel(model);
            return result;
        }

        public async Task<WaiveQCDTO> CreateWaiveQCAsync(Guid projectID, WaiveQCDTO input)
        {
            WaiveQC model = new WaiveQC();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            await DB.WaiveQCs.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await this.GetWaiveQCAsync(projectID, model.ID);
            return result;
        }

        public async Task<WaiveQCDTO> UpdateWaiveQCAsync(Guid projectID, Guid id, WaiveQCDTO input)
        {
            var model = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            input.ToModel(ref model);
            model.ProjectID = projectID;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetWaiveQCAsync(projectID, id);
            return result;
        }

        public async Task<WaiveQC> DeleteWaiveQCAsync(Guid projectID, Guid id)
        {
            var model = await DB.WaiveQCs.Where(o => o.ProjectID == projectID && o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<WaiveQCExcelDTO> ImportWaiveQCAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // FormatDate
            var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();

            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();


            var result = new WaiveQCExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var units = await DB.Units.Where(o => o.ProjectID == projectID).ToListAsync();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 4)
            {
                throw new Exception("Invalid File Format");
            }
            var row = 2;
            var error = 0;

            var checkNullUnitNos = new List<string>();
            var checkNullWbsNos = new List<string>();
            var checkFormateWaiveDates = new List<string>();
            var checkUnitNotFounds = new List<string>();
            //Read Excel Model
            var waiveQCExcelModel = new List<WaiveQCExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = WaiveQCExcelModel.CreateFromDataRow(r);
                waiveQCExcelModel.Add(excelModel);

                #region Validate
                var unit = units.Where(o => o.ProjectID == projectID && o.UnitNo == excelModel.UnitNo && o.SAPWBSNo == excelModel.WBSNo).FirstOrDefault();
                if (unit == null)
                {
                    checkUnitNotFounds.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.WBSNo))
                {
                    checkNullWbsNos.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[WaiveQCExcelModel._waiveQCDateIndex].ToString()))
                {
                    if (!r[WaiveQCExcelModel._waiveQCDateIndex].ToString().isFormatDate())
                    {
                        checkFormateWaiveDates.Add((row).ToString());
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

            #region Validate Project
            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (waiveQCExcelModel.Any(o => o.ProjectNo != project.ProjectNo))
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

            #region Add Result ErrorMassage
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

            if (checkNullWbsNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBS Code");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWbsNos));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormateWaiveDates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่ Waive QC");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateWaiveDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่ Waive QC");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateWaiveDates));
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
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(checkNullWbsNos);
            rowErrors.AddRange(checkFormateWaiveDates);
            rowErrors.AddRange(checkUnitNotFounds);
            #endregion


            var waiveQCs = await DB.WaiveQCs.Where(o => o.ProjectID == projectID).ToListAsync();

            List<WaiveQC> waiveQCsCreate = new List<WaiveQC>();
            List<WaiveQC> waiveQCsUpdate = new List<WaiveQC>();
            //Update Data
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;

            foreach (var item in waiveQCExcelModel)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Where(o => o.ProjectID == projectID && o.UnitNo == item.UnitNo && o.SAPWBSNo == item.WBSNo).FirstOrDefault();
                    if (unit != null)
                    {
                        var existedwaive = waiveQCs.Where(x => x.ProjectID == projectID && x.UnitID == unit.ID).FirstOrDefault();
                        if (existedwaive == null)
                        {
                            WaiveQC waive = new WaiveQC();
                            item.ToModel(ref waive);
                            waive.ProjectID = projectID;
                            waive.UnitID = unit.ID;
                            result.Success++;
                            waiveQCsCreate.Add(waive);
                        }
                        else
                        {
                            item.ToModel(ref existedwaive);
                            existedwaive.ProjectID = projectID;
                            existedwaive.UnitID = unit.ID;
                            result.Success++;
                            waiveQCsUpdate.Add(existedwaive);
                        }
                    }
                }
                row++;
            }
            await DB.WaiveQCs.AddRangeAsync(waiveQCsCreate);
            DB.UpdateRange(waiveQCsUpdate);
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

        public async Task<FileDTO> ExportExcelWaiveQCAsync(Guid projectID, WaiveQCFilter filter, WaiveQCSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<WaiveQCQueryResult> query = DB.WaiveQCs.Where(o => o.ProjectID == projectID)
                                                             .Include(o => o.Unit.UnitStatus)
                                                             .Select(o => new WaiveQCQueryResult
                                                             {
                                                                 Unit = o.Unit,
                                                                 WaiveQC = o
                                                             });
            #region Filter

            #region ActualTransferDate
            if (filter.ActualTransferDateFrom != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom);
            }
            if (filter.ActualTransferDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
            }
            if (filter.ActualTransferDateFrom != null && filter.ActualTransferDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.ActualTransferDate >= filter.ActualTransferDateFrom
                                    && o.WaiveQC.ActualTransferDate <= filter.ActualTransferDateTo);
            }
            #endregion

            #region WaiveQCeDate
            if (filter.WaiveQCDateFrom != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate >= filter.WaiveQCDateFrom);
            }
            if (filter.WaiveQCDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate <= filter.WaiveQCDateTo);
            }
            if (filter.WaiveQCDateFrom != null && filter.WaiveQCDateTo != null)
            {
                query = query.Where(o => o.WaiveQC.WaiveQCDate >= filter.WaiveQCDateFrom
                                    && o.WaiveQC.WaiveQCDate <= filter.WaiveQCDateTo);
            }
            #endregion

            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                       && x.MasterCenterGroupKey == "UnitStatus")
                                                                      .Select(x => x.ID).FirstAsync();
                query = query.Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusMasterCenterID);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.WaiveQC.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.WaiveQC.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.WaiveQC.Updated >= filter.UpdatedFrom && x.WaiveQC.Updated <= filter.UpdatedTo);
            }
            #endregion

            WaiveQCDTO.SortBy(sortByParam, ref query);

            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_WaiveQC.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = WaiveQCExcelModel._projectNoIndex + 1;
                int _wbsNoIndex = WaiveQCExcelModel._wbsNoIndex + 1;
                int _unitNoIndex = WaiveQCExcelModel._unitNoIndex + 1;
                int _waiveQCeDateIndex = WaiveQCExcelModel._waiveQCDateIndex + 1;

                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _wbsNoIndex].Value = data[c - 2].Unit?.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Unit?.UnitNo;

                    worksheet.Cells[c, _waiveQCeDateIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[c, _waiveQCeDateIndex].Value = data[c - 2].WaiveQC.WaiveQCDate;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_WaiveQC.xlsx";
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
    }
}
