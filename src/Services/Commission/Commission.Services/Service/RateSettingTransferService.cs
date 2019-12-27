using Database.Models;
using Database.Models.CMS;
using Database.Models.PRJ;
using Commission.Params.Filters;
using Commission.Params.Outputs;
using Commission.Params.Inputs;
using Commission.Services.Excels;
using Base.DTOs;
using Base.DTOs.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using FileStorage;
using OfficeOpenXml;
using ExcelExtensions;
using ErrorHandling;


namespace Commission.Services
{
    public class RateSettingTransferService : IRateSettingTransferService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public RateSettingTransferService(IConfiguration configuration, DatabaseContext db)
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

        public async Task<RateSettingTransferPaging> GetRateSettingTransferListAsync(RateSettingTransferFilter filter, PageParam pageParam, RateSettingTransferSortByParam sortByParam)
        {
            IQueryable<RateSettingTransferQueryResult> query = DB.RateSettingTransfers
                                                  .Include(x => x.Project)
                                                  .Select(o => new RateSettingTransferQueryResult()
                                                  {
                                                      RateSettingTransfer = o,
                                                      Project = o.Project
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingTransfer.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingTransfer.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingTransfer.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingTransfer.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingTransfer.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingTransfer.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingTransfer.Created >= filter.CreateDateFrom && x.RateSettingTransfer.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingTransfer.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingSaleTransferDTO.SortByTransfer(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingTransferQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => RateSettingSaleTransferDTO.CreateFromTransferQueryResult(o)).ToList();

            return new RateSettingTransferPaging()
            {
                PageOutput = pageOutput,
                RateSettingTransfers = results
            };
        }

        public async Task<List<RateSettingSaleTransferDTO>> GetRateSettingTransferProjectListForNewAsync(string BGNo)
        {
            IQueryable<RateSettingTransferQueryResult> query = from r in DB.RateTransfers
                                                               //join p in DB.Projects on r.BGNo equals p.BG.BGNo into g1
                                                               //from rp in g1.DefaultIfEmpty()
                                                               //join rm in DB.RateSettingTransfers on rp.ID equals rm.ProjectID into g2
                                                               //from o in g2.DefaultIfEmpty()
                                                               //where o.IsActive
                                                           select new RateSettingTransferQueryResult()
                                                           {
                                                               RateSettingTransfer = new RateSettingTransfer(),
                                                               Project = new Project(),
                                                               RateTransfer = r
                                                           };

            #region Filter
            if (!string.IsNullOrEmpty(BGNo))
            {
                query = query.Where(x => x.RateTransfer.BGNo == BGNo);
            }
            #endregion

            var results = await query.Select(o => RateSettingSaleTransferDTO.CreateFromTransferQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task<List<RateSettingSaleTransferDTO>> GetRateSettingTransferProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate)
        {
            IQueryable<RateSettingTransferQueryResult> query = from r in DB.RateTransfers
                                                           join p in DB.Projects on r.BGNo equals p.BG.BGNo into g1
                                                           from rp in g1.DefaultIfEmpty()
                                                           join rm in DB.RateSettingTransfers on rp.ID equals rm.ProjectID into g2
                                                           from o in g2.DefaultIfEmpty()
                                                           where o.IsActive
                                                           select new RateSettingTransferQueryResult()
                                                            {
                                                                RateSettingTransfer = o ?? new RateSettingTransfer(),
                                                                Project = rp,
                                                                RateTransfer = r
                                                            };

            #region Filter
            if (ProjectID != null)
            {
                query = query.Where(x => x.RateSettingTransfer.ProjectID == ProjectID);
            }
            if (ActiveDate != null)
            {
                query = query.Where(x => x.RateSettingTransfer.ActiveDate == ActiveDate);
            }
            #endregion

            var results = await query.Select(o => RateSettingSaleTransferDTO.CreateFromTransferQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task CreateRateSettingTransferListAsync(RateSettingTransferInput inputModel)
        {
            var lstProject = inputModel.ListProject;
            var ListInput = inputModel.ListRateSettingTransfer;

            if (lstProject.Count > 0 && ListInput.Count() > 0)
            {
                var lstRateSettingTransfer = new List<RateSettingTransfer>();
                var lstUpdateRateSettingTransfer = new List<RateSettingTransfer>();
                foreach (var pr in lstProject)
                {
                    foreach (var input in ListInput)
                    {
                        var model = new RateSettingTransfer();
                        model.ActiveDate = input.ActiveDate;
                        model.ProjectID = pr.ProjectID;
                        model.StartRange = input.StartRange;
                        model.EndRange = input.EndRange;
                        model.Amount = input.Amount;
                        model.IsActive = true;
                        lstRateSettingTransfer.Add(model);


                        var lstUpdate = await DB.RateSettingTransfers.Where(o => o.ProjectID == pr.ProjectID
                                                                                && o.Amount == input.Amount
                                                                                && o.ActiveDate <= input.ActiveDate
                                                                                && o.IsActive == true).ToListAsync();
                        foreach (var update in lstUpdate)
                        {
                            update.IsActive = false;

                            lstUpdateRateSettingTransfer.Add(update);
                        }
                    }
                }

                DB.RateSettingTransfers.UpdateRange(lstUpdateRateSettingTransfer);
                await DB.RateSettingTransfers.AddRangeAsync(lstRateSettingTransfer);
                await DB.SaveChangesAsync();
            }
        }

        public async Task UpdateRateSettingTransferListAsync(List<RateSettingSaleTransferDTO> ListInput)
        {
            if (ListInput.Count() > 0)
            {
                //var lstRateSettingTransfer = new List<RateSettingTransfer>();
                //var lstUpdateRateSettingTransfer = new List<RateSettingTransfer>();

                foreach (var input in ListInput)
                {
                    await input.TransferValidateAsync(DB);

                    var model = await DB.RateSettingTransfers.Where(o => o.ID == input.Id).FirstAsync();
                    input.ToTransferModel(ref model);

                    DB.Entry(model).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
        }

        /*
        public async Task<RateSettingSaleTransferDTO> GetRateSettingTransferAsync(Guid id)
        {
            var model = await DB.RateSettingTransfers.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingSaleTransferDTO> CreateRateSettingTransferAsync(RateSettingSaleTransferDTO input)
        {
            await input.ValidateAsync(DB);

            RateSettingTransfer model = new RateSettingTransfer();
            input.ToModel(ref model);

            await DB.RateSettingTransfers.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingSaleTransferDTO> UpdateRateSettingTransferAsync(Guid id, RateSettingSaleTransferDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.RateSettingTransfers.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingTransfer> DeleteRateSettingTransferAsync(Guid id)
        {
            var model = await DB.RateSettingTransfers.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
        */

        public async Task<RateSettingSaleTransferExcelDTO> ImportRateSettingTransferAsync(Guid BGID, FileDTO input)
        {
            #region Validate Data
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();

            // FormatDate
            var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();

            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();


            var result = new RateSettingSaleTransferExcelDTO { Success = 0, Error = 0, ErrorMessages = new List<string>() };
            var projects = await DB.Projects.Where(o => o.BGID == BGID).ToListAsync();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 8)
            {
                throw new Exception("Invalid File Format");
            }
            var row = 2;
            var error = 0;

            var checkNullProjects = new List<string>();
            var checkNullRates = new List<string>();
            var checkFormateEffectiveMonths = new List<string>();
            var checkProjectNotFounds = new List<string>();

            //Read Excel Model
            var rateSettingTransferExcelModels = new List<RateSettingTransferExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = RateSettingTransferExcelModel.CreateFromDataRow(r);
                rateSettingTransferExcelModels.Add(excelModel);

                #region Validate
                var prj = projects.Where(o => o.ProjectNo == excelModel.ProjectNo && o.BG.BGNo == excelModel.BGNo).FirstOrDefault();
                if (prj == null)
                {
                    checkProjectNotFounds.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.ProjectNo))
                {
                    checkNullProjects.Add((row).ToString());
                    isError = true;
                }
                if (excelModel.Rate == 0)
                {
                    checkNullRates.Add((row).ToString());
                    isError = true;
                }
                if (!string.IsNullOrEmpty(r[RateSettingTransferExcelModel._effectiveMonthIndex].ToString()))
                {
                    if (!r[RateSettingTransferExcelModel._effectiveMonthIndex].ToString().isFormatDate())
                    {
                        checkFormateEffectiveMonths.Add((row).ToString());
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
            #endregion

            #region Validate BG
            var bg = await DB.BGs.Where(o => o.ID == BGID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (rateSettingTransferExcelModels.Any(o => o.BGNo != bg.BGNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "BG");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            #region Add Result ErrorMassage
            if (checkNullProjects.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เลขที่โครงการ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullProjects));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เลขที่โครงการ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullProjects));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkNullRates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "%Rate");
                    msg = msg.Replace("[row]", String.Join(",", checkNullRates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "%Rate");
                    msg = msg.Replace("[row]", String.Join(",", checkNullRates));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormateEffectiveMonths.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "Effective Month");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateEffectiveMonths));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "Effective Month");
                    msg = msg.Replace("[row]", String.Join(",", checkFormateEffectiveMonths));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkProjectNotFounds.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่โครงการ");
                    msg = msg.Replace("[row]", String.Join(",", checkProjectNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "เลขที่โครงการ");
                    msg = msg.Replace("[row]", String.Join(",", checkProjectNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region RowErrors
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullProjects);
            rowErrors.AddRange(checkNullRates);
            rowErrors.AddRange(checkFormateEffectiveMonths);
            rowErrors.AddRange(checkProjectNotFounds);
            #endregion


            var RateSettingTransfers = await DB.RateSettingTransfers.Where(o => o.Project.BGID == BGID).ToListAsync();

            List<RateSettingTransfer> RateSettingTransfersCreate = new List<RateSettingTransfer>();
            List<RateSettingTransfer> RateSettingTransfersUpdate = new List<RateSettingTransfer>();
            List<RateSettingTransfer> InactiveRateSettingTransferUpdate = new List<RateSettingTransfer>();
            //Update Data
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;

            foreach (var item in rateSettingTransferExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var prj = projects.Where(o => o.ProjectNo == item.ProjectNo && o.BG.BGNo == item.BGNo).FirstOrDefault();
                    if (prj != null)
                    {
                        var rate = RateSettingTransfers.Where(x => x.ProjectID == prj.ID && x.Amount == item.Rate).FirstOrDefault();
                        if (rate == null)
                        {
                            rate = new RateSettingTransfer();
                            item.ToModel(ref rate);
                            rate.ProjectID = prj.ID;
                            rate.StartRange = item.StartRange;
                            rate.EndRange = item.EndRange;
                            rate.Amount = item.Rate;
                            rate.IsActive = true;
                            result.Success++;
                            RateSettingTransfersCreate.Add(rate);
                        }
                        else
                        {
                            item.ToModel(ref rate);
                            rate.ProjectID = prj.ID;
                            rate.StartRange = item.StartRange;
                            rate.EndRange = item.EndRange;
                            //rate.Amount = item.Rate;
                            rate.IsActive = true;
                            result.Success++;
                            RateSettingTransfersUpdate.Add(rate);

                            var updates = RateSettingTransfers.Where(n => n.ProjectID == prj.ID && n.Amount == item.Rate && n.ActiveDate < item.EffectiveMonth && n.IsActive == true).ToList();
                            foreach (var upd in updates)
                            {
                                upd.IsActive = false;

                                InactiveRateSettingTransferUpdate.Add(upd);
                            }
                        }
                    }
                }
                row++;
            }

            await DB.RateSettingTransfers.AddRangeAsync(RateSettingTransfersCreate);
            DB.UpdateRange(RateSettingTransfersUpdate);
            DB.UpdateRange(InactiveRateSettingTransferUpdate);
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

        public async Task<FileDTO> ExportExcelRateSettingTransferAsync(Guid BGID, RateSettingTransferFilter filter, RateSettingTransferSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<RateSettingTransferQueryResult> query = from p in DB.Projects
                                                           .Include(p => p.BG)
                                                           join rt in DB.RateTransfers on p.BG.BGNo equals rt.BGNo into g1
                                                           from r in g1.DefaultIfEmpty()
                                                           join s in DB.RateSettingTransfers on p.ID equals s.ProjectID into g2 //new { p.ID, r.Rate } equals new { s.ProjectID, s.Amount } into g2
                                                           from rs in g2.Where(x => x.Amount == r.Rate).DefaultIfEmpty()
                                                           where (rs == null
                                                                    || rs.ActiveDate == (DB.RateSettingTransfers.Where(n => n.ProjectID == p.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                                && rs.IsActive
                                                           select new RateSettingTransferQueryResult()
                                                           {
                                                               RateSettingTransfer = rs,
                                                               Project = p,
                                                               RateTransfer = r
                                                           };
            #region Filter            
            if (BGID != null)
            {
                query = query.Where(x => x.Project.BGID == BGID);
            }
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.Project.ID));
            }
            #endregion

            RateSettingSaleTransferDTO.SortByTransfer(sortByParam, ref query);

            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "BG_RankingTransfer.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _effectiveMonthIndex = RateSettingTransferExcelModel._effectiveMonthIndex + 1;
                int _bGIndex = RateSettingTransferExcelModel._bGIndex + 1;
                int _projectIDIndex = RateSettingTransferExcelModel._projectIDIndex + 1;
                int _projectNameIndex = RateSettingTransferExcelModel._projectNameIndex + 1;
                int _itemIDIndex = RateSettingTransferExcelModel._itemIDIndex + 1;
                int _rateIndex = RateSettingTransferExcelModel._rateIndex + 1;
                int _startRangeIndex = RateSettingTransferExcelModel._startRangeIndex + 1;
                int _endRangeIndex = RateSettingTransferExcelModel._endRangeIndex + 1;


                var bg = await DB.BGs.Where(x => x.ID == BGID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _effectiveMonthIndex].Style.Numberformat.Format = "mm/yyyy";
                    worksheet.Cells[c, _effectiveMonthIndex].Value = DateTime.Now; //data[c - 2].RateSettingTransfer.ActiveDate;

                    worksheet.Cells[c, _bGIndex].Value = bg.BGNo;
                    worksheet.Cells[c, _projectIDIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = data[c - 2].Project?.ProjectNameTH;
                    worksheet.Cells[c, _itemIDIndex].Value = data[c - 2].RateTransfer?.Sequence;
                    worksheet.Cells[c, _rateIndex].Value = data[c - 2].RateTransfer?.Rate;
                    worksheet.Cells[c, _startRangeIndex].Value = data[c - 2].RateSettingTransfer?.StartRange;
                    worksheet.Cells[c, _endRangeIndex].Value = data[c - 2].RateSettingTransfer?.EndRange;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "BG_" + bg.BGNo + "_RankingTransfer.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{BGID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }
    }
}
