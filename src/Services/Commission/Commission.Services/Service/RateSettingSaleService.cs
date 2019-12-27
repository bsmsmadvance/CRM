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
    public class RateSettingSaleService : IRateSettingSaleService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public RateSettingSaleService(IConfiguration configuration, DatabaseContext db)
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

        public async Task<RateSettingSalePaging> GetRateSettingSaleListAsync(RateSettingSaleFilter filter, PageParam pageParam, RateSettingSaleSortByParam sortByParam)
        {
            IQueryable<RateSettingSaleQueryResult> query = DB.RateSettingSales
                                                  .Include(x => x.Project)
                                                  .Select(o => new RateSettingSaleQueryResult()
                                                  {
                                                      RateSettingSale = new RateSettingSale()
                                                      {
                                                          ID = Guid.Empty,
                                                          ActiveDate = o.ActiveDate,
                                                          ProjectID = o.ProjectID,
                                                          StartRange = 0,
                                                          EndRange = 0,
                                                          Amount = 0,
                                                          CreatedBy = o.CreatedBy,
                                                          Created = o.Created.Value.AddSeconds(o.Created.Value.Second * -1).AddMilliseconds(o.Created.Value.Millisecond * -1),
                                                          IsActive = o.IsActive
                                                      },
                                                      Project = o.Project
                                                  });

            #region Filter
            if (filter.ListProjectId != null && filter.ListProjectId.Count > 0)
            {
                var lstId = filter.ListProjectId.Select(o => o).ToList();

                query = query.Where(x => lstId.Contains(x.RateSettingSale.ProjectID ?? Guid.Empty));
            }
            if (filter.ActiveDate.HasValue)
            {
                query = query.Where(x => x.RateSettingSale.ActiveDate == filter.ActiveDate);
            }
            if (filter.ProjectID.HasValue)
            {
                query = query.Where(x => x.RateSettingSale.ProjectID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.CreateUserName))
            {
                query = query.Where(x => x.RateSettingSale.CreatedBy.DisplayName.Contains(filter.CreateUserName));
            }
            if (filter.CreateDateFrom.HasValue)
            {
                query = query.Where(x => x.RateSettingSale.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingSale.Created <= filter.CreateDateTo);
            }
            if (filter.CreateDateFrom.HasValue && filter.CreateDateTo.HasValue)
            {
                query = query.Where(x => x.RateSettingSale.Created >= filter.CreateDateFrom && x.RateSettingSale.Created <= filter.CreateDateTo);
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.RateSettingSale.IsActive == filter.IsActive);
            }
            #endregion

            RateSettingSaleTransferDTO.SortBySale(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<RateSettingSaleQueryResult>(pageParam, ref query);

            var queryResults = await query.Distinct().ToListAsync();

            var results = queryResults.Select(o => RateSettingSaleTransferDTO.CreateFromDistinctSaleQueryResult(o)).ToList();

            return new RateSettingSalePaging()
            {
                PageOutput = pageOutput,
                RateSettingSales = results
            };
        }

        public async Task<List<RateSettingSaleTransferDTO>> GetRateSettingSaleProjectListForNewAsync(string BGNo)
        {
            IQueryable<RateSettingSaleQueryResult> query = from r in DB.RateSales
                                                               //join p in DB.Projects on r.BGNo equals p.BG.BGNo into g1
                                                               //from rp in g1.DefaultIfEmpty()
                                                               //join rm in DB.RateSettingSales on rp.ID equals rm.ProjectID into g2
                                                               //from o in g2.DefaultIfEmpty()
                                                               //where o.IsActive
                                                           select new RateSettingSaleQueryResult()
                                                           {
                                                               RateSettingSale = new RateSettingSale(),
                                                               Project = new Project(),
                                                               RateSale = r
                                                           };

            #region Filter
            if (!string.IsNullOrEmpty(BGNo))
            {
                query = query.Where(x => x.RateSale.BGNo == BGNo);
            }
            #endregion

            var results = await query.Select(o => RateSettingSaleTransferDTO.CreateFromSaleQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task<List<RateSettingSaleTransferDTO>> GetRateSettingSaleProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate)
        {
            IQueryable<RateSettingSaleQueryResult> query = from r in DB.RateSales
                                                           join p in DB.Projects on r.BGNo equals p.BG.BGNo into g1
                                                           from rp in g1.DefaultIfEmpty()
                                                           join rm in DB.RateSettingSales on rp.ID equals rm.ProjectID into g2
                                                           from o in g2.DefaultIfEmpty()
                                                           where o.IsActive
                                                           select new RateSettingSaleQueryResult()
                                                           {
                                                               RateSettingSale = o ?? new RateSettingSale(),
                                                               Project = rp,
                                                               RateSale = r
                                                           };

            #region Filter
            if (ProjectID != null)
            {
                query = query.Where(x => x.RateSettingSale.ProjectID == ProjectID);
            }
            if (ActiveDate != null)
            {
                query = query.Where(x => x.RateSettingSale.ActiveDate == ActiveDate);
            }
            #endregion

            var results = await query.Select(o => RateSettingSaleTransferDTO.CreateFromSaleQueryResult(o)).ToListAsync();
            return results;
        }

        public async Task CreateRateSettingSaleListAsync(RateSettingSaleInput inputModel)
        {
            var lstProject = inputModel.ListProject;
            var ListInput = inputModel.ListRateSettingSale;

            if (lstProject.Count > 0 && ListInput.Count() > 0)
            {
                var lstRateSettingSale = new List<RateSettingSale>();
                var lstUpdateRateSettingSale = new List<RateSettingSale>();
                foreach (var pr in lstProject)
                {
                    foreach (var input in ListInput)
                    {
                        await input.SaleValidateAsync(DB);

                        var model = new RateSettingSale();
                        model.ActiveDate = input.ActiveDate;
                        model.ProjectID = pr.ProjectID;
                        model.StartRange = input.StartRange;
                        model.EndRange = input.EndRange;
                        model.Amount = input.Amount;
                        model.IsActive = true;
                        lstRateSettingSale.Add(model);


                        var lstUpdate = await DB.RateSettingSales.Where(o => o.ProjectID == pr.ProjectID
                                                                                && o.Amount == input.Amount
                                                                                && o.ActiveDate <= input.ActiveDate
                                                                                && o.IsActive == true).ToListAsync();
                        foreach (var update in lstUpdate)
                        {
                            update.IsActive = false;

                            lstUpdateRateSettingSale.Add(update);
                        }
                    }
                }

                DB.RateSettingSales.UpdateRange(lstUpdateRateSettingSale);
                await DB.RateSettingSales.AddRangeAsync(lstRateSettingSale);
                await DB.SaveChangesAsync();
            }
        }

        public async Task UpdateRateSettingSaleListAsync(List<RateSettingSaleTransferDTO> ListInput)
        {
            if (ListInput.Count() > 0)
            {
                //lstRateSettingSale = new List<RateSettingSale>();
                //var lstUpdateRateSettingSale = new List<RateSettingSale>();

                foreach (var input in ListInput)
                {
                    await input.SaleValidateAsync(DB);

                    var model = await DB.RateSettingSales.Where(o => o.ID == input.Id).FirstAsync();
                    input.ToSaleModel(ref model);

                    DB.Entry(model).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
        }

        /*
        public async Task<RateSettingSaleTransferDTO> GetRateSettingSaleAsync(Guid id)
        {
            var model = await DB.RateSettingSales.Where(o => o.ID == id).FirstAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingSaleTransferDTO> CreateRateSettingSaleAsync(RateSettingSaleTransferDTO input)
        {
            await input.ValidateAsync(DB);

            RateSettingSale model = new RateSettingSale();
            input.ToModel(ref model);

            await DB.RateSettingSales.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingSaleTransferDTO> UpdateRateSettingSaleAsync(Guid id, RateSettingSaleTransferDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.RateSettingSales.Where(o => o.ID == id).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = RateSettingSaleTransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<RateSettingSale> DeleteRateSettingSaleAsync(Guid id)
        {
            var model = await DB.RateSettingSales.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }
        */

        public async Task<RateSettingSaleTransferExcelDTO> ImportRateSettingSaleAsync(Guid BGID, FileDTO input)
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
            var rateSettingSaleExcelModels = new List<RateSettingSaleExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = RateSettingSaleExcelModel.CreateFromDataRow(r);
                rateSettingSaleExcelModels.Add(excelModel);

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
                if (!string.IsNullOrEmpty(r[RateSettingSaleExcelModel._effectiveMonthIndex].ToString()))
                {
                    if (!r[RateSettingSaleExcelModel._effectiveMonthIndex].ToString().isFormatDate())
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
            if (rateSettingSaleExcelModels.Any(o => o.BGNo != bg.BGNo))
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


            var RateSettingSales = await DB.RateSettingSales.Where(o => o.Project.BGID == BGID).ToListAsync();

            List<RateSettingSale> RateSettingSalesCreate = new List<RateSettingSale>();
            List<RateSettingSale> RateSettingSalesUpdate = new List<RateSettingSale>();
            List<RateSettingSale> InactiveRateSettingSaleUpdate = new List<RateSettingSale>();
            //Update Data
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;

            foreach (var item in rateSettingSaleExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var prj = projects.Where(o => o.ProjectNo == item.ProjectNo && o.BG.BGNo == item.BGNo).FirstOrDefault();
                    if (prj != null)
                    {
                        var rate = RateSettingSales.Where(x => x.ProjectID == prj.ID && x.Amount == item.Rate).FirstOrDefault();
                        if (rate == null)
                        {
                            rate = new RateSettingSale();
                            item.ToModel(ref rate);
                            rate.ProjectID = prj.ID;
                            rate.StartRange = item.StartRange;
                            rate.EndRange = item.EndRange;
                            rate.Amount = item.Rate;
                            rate.IsActive = true;
                            result.Success++;
                            RateSettingSalesCreate.Add(rate);
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
                            RateSettingSalesUpdate.Add(rate);

                            var updates = RateSettingSales.Where(n => n.ProjectID == prj.ID && n.Amount == item.Rate && n.ActiveDate < item.EffectiveMonth && n.IsActive == true).ToList();
                            foreach (var upd in updates)
                            {
                                upd.IsActive = false;

                                InactiveRateSettingSaleUpdate.Add(upd);
                            }
                        }
                    }
                }
                row++;
            }

            await DB.RateSettingSales.AddRangeAsync(RateSettingSalesCreate);
            DB.UpdateRange(RateSettingSalesUpdate);
            DB.UpdateRange(InactiveRateSettingSaleUpdate);
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

        public async Task<FileDTO> ExportExcelRateSettingSaleAsync(Guid BGID, RateSettingSaleFilter filter, RateSettingSaleSortByParam sortByParam)
        {
            ExportExcel result = new ExportExcel();
            IQueryable<RateSettingSaleQueryResult> query = from p in DB.Projects
                                                           .Include(p => p.BG)
                                                           join rr in DB.RateSales on p.BG.BGNo equals rr.BGNo into g1
                                                           from r in g1.DefaultIfEmpty()
                                                           join s in DB.RateSettingSales on p.ID equals s.ProjectID into g2 //new { p.ID, r.Rate } equals new { s.ProjectID, s.Amount } into g2
                                                           from rs in g2.Where(x => x.Amount == r.Rate).DefaultIfEmpty()
                                                           where (rs == null
                                                                    || rs.ActiveDate == (DB.RateSettingSales.Where(n => n.ProjectID == p.ID).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate)))
                                                                && rs.IsActive
                                                           select new RateSettingSaleQueryResult()
                                                           {
                                                               RateSettingSale = rs,
                                                               Project = p,
                                                               RateSale = r
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

            RateSettingSaleTransferDTO.SortBySale(sortByParam, ref query);

            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "BG_RankingSale.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _effectiveMonthIndex = RateSettingSaleExcelModel._effectiveMonthIndex + 1;
                int _bGIndex = RateSettingSaleExcelModel._bGIndex + 1;
                int _projectIDIndex = RateSettingSaleExcelModel._projectIDIndex + 1;
                int _projectNameIndex = RateSettingSaleExcelModel._projectNameIndex + 1;
                int _itemIDIndex = RateSettingSaleExcelModel._itemIDIndex + 1;
                int _rateIndex = RateSettingSaleExcelModel._rateIndex + 1;
                int _startRangeIndex = RateSettingSaleExcelModel._startRangeIndex + 1;
                int _endRangeIndex = RateSettingSaleExcelModel._endRangeIndex + 1;


                var bg = await DB.BGs.Where(x => x.ID == BGID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    worksheet.Cells[c, _effectiveMonthIndex].Style.Numberformat.Format = "mm/yyyy";
                    worksheet.Cells[c, _effectiveMonthIndex].Value = DateTime.Now; //data[c - 2].RateSettingSale.ActiveDate;

                    worksheet.Cells[c, _bGIndex].Value = bg.BGNo;
                    worksheet.Cells[c, _projectIDIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = data[c - 2].Project?.ProjectNameTH;
                    worksheet.Cells[c, _itemIDIndex].Value = data[c - 2].RateSale?.Sequence;
                    worksheet.Cells[c, _rateIndex].Value = data[c - 2].RateSale?.Rate;
                    worksheet.Cells[c, _startRangeIndex].Value = data[c - 2].RateSettingSale?.StartRange;
                    worksheet.Cells[c, _endRangeIndex].Value = data[c - 2].RateSettingSale?.EndRange;
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "BG_" + bg.BGNo + "_RankingSale.xlsx";
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
