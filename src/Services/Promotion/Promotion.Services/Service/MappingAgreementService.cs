using System;
using Base.DTOs;
using Base.DTOs.PRM;
using OfficeOpenXml;
using Database.Models;
using ExcelExtensions;
using FileStorage;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Promotion.Services.Excel;
using Microsoft.EntityFrameworkCore;
using Database.Models.PRM;

namespace Promotion.Services
{
    public class MappingAgreementService : IMappingAgreementService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        public MappingAgreementService(IConfiguration configuration, DatabaseContext db)
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
        /// สร้าง List ของ Mapping Agreement โดยการ Upload Excel File
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The mapping agreements data from excel async.</returns>
        /// <param name="input">Input.</param>
        public async Task<List<MappingAgreementDTO>> GetMappingAgreementsDataFromExcelAsync(FileDTO input)
        {
            //ดูตัวอย่างการสร้าง unit test File Upload ที่ Project > PriceListUnitTest
            //Url ตัวอย่างไฟล์ http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 7)
            {
                throw new Exception("Invalid File Format");
            }
            //Read Excel Model
            var mappingAgreementExcels = new List<MappingAgreementExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var excelModel = MappingAgreementExcelModel.CreateFromDataRow(r);
                mappingAgreementExcels.Add(excelModel);
            }
            List<MappingAgreementDTO> mappingAgreements = new List<MappingAgreementDTO>();
            var RunningNumber = 1;
            foreach (var item in mappingAgreementExcels)
            {
                MappingAgreementDTO mappingAgreement = new MappingAgreementDTO();
                mappingAgreement.No = RunningNumber;
                mappingAgreement.NewAgreement = item.NewAgreementNo;
                mappingAgreement.NewItem = item.NewItemNo;
                mappingAgreement.NewMaterialCode = item.NewMaterialCode;
                mappingAgreement.OldAgreement = item.OldAgreementNo;
                mappingAgreement.OldItem = item.OldItemNo;
                mappingAgreement.OldMaterialCode = item.OldMaterialCode;
                mappingAgreement.Remark = item.Remartk;

                if (string.IsNullOrEmpty(mappingAgreement.NewAgreement)
                   || string.IsNullOrEmpty(mappingAgreement.NewItem)
                   || string.IsNullOrEmpty(mappingAgreement.NewMaterialCode)
                   || string.IsNullOrEmpty(mappingAgreement.OldAgreement)
                   || string.IsNullOrEmpty(mappingAgreement.OldItem)
                   || string.IsNullOrEmpty(mappingAgreement.OldMaterialCode))
                {
                    mappingAgreement.IsValidData = false;
                }
                else
                {
                    mappingAgreement.IsValidData = true;
                }
                mappingAgreements.Add(mappingAgreement);
                RunningNumber++;
            }
            return mappingAgreements;

        }

        /// <summary>
        /// ล้างข้อมูล Mapping Agreement เก่าทิ้งทั้งหมด แล้ว Insert เข้าไปใหม่
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The import mapping agreements async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MappingAgreementDTO>> ConfirmImportMappingAgreementsAsync(List<MappingAgreementDTO> inputs)
        {
            //Remove Datas
            var allData = await DB.MappingAgreements.ToListAsync();
            foreach (var item in allData)
            {
                item.IsDeleted = true;
                DB.Entry(item).State = EntityState.Modified;
            }
            DB.UpdateRange(allData);

            // Add Datas
            var mappingAgreements = new List<MappingAgreement>();

            foreach (var item in inputs)
            {
                MappingAgreement model = new MappingAgreement();
                item.ToModel(ref model);

                mappingAgreements.Add(model);
            }

            await DB.AddRangeAsync(mappingAgreements);
            await DB.SaveChangesAsync();

            var queryResults = await DB.MappingAgreements.Include(o => o.UpdatedBy).ToListAsync();
            var results = queryResults.Select(o => MappingAgreementDTO.CreateFromModel(o)).ToList();
            return results;
        }

        /// <summary>
        /// Export Excel โดยใช้ไฟล์ Template 
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/366447264/preview
        /// </summary>
        /// <returns>The mapping agreements async.</returns>
        public async Task<FileDTO> ExportMappingAgreementsAsync()
        {
            ExportExcel result = new ExportExcel();
            var data = await DB.MappingAgreements.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "Export_MappingAgreement.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _oldAgreementNoIndex = MappingAgreementExcelModel._oldAgreementNoIndex + 1;
                int _oldItemNoIndex = MappingAgreementExcelModel._oldItemNoIndex + 1;
                int _oldMaterialCodeIndex = MappingAgreementExcelModel._oldMaterialCodeIndex + 1;
                int _newAgreementNoIndex = MappingAgreementExcelModel._newAgreementNoIndex + 1;
                int _newItemNoIndex = MappingAgreementExcelModel._newItemNoIndex + 1;
                int _newMaterialCodeIndex = MappingAgreementExcelModel._newMaterialCodeIndex + 1;
                int _remarkIndex = MappingAgreementExcelModel._remarkIndex + 1;

                for (int c = 3; c < data.Count + 3; c++)
                {
                    worksheet.Cells[c, _oldAgreementNoIndex].Value = data[c - 3].OldAgreement;
                    worksheet.Cells[c, _oldItemNoIndex].Value = data[c - 3].OldItem;
                    worksheet.Cells[c, _oldMaterialCodeIndex].Value = data[c - 3].OldMaterialCode;
                    worksheet.Cells[c, _newAgreementNoIndex].Value = data[c - 3].NewAgreement;
                    worksheet.Cells[c, _newItemNoIndex].Value = data[c - 3].OldItem;
                    worksheet.Cells[c, _newMaterialCodeIndex].Value = data[c - 3].OldMaterialCode;
                    worksheet.Cells[c, _remarkIndex].Value = data[c - 3].Remark;
                }

                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = "Export_MappingAgreement.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"mapping-agreement/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
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
                    var startRow = hasHeader ? 3 : 2;
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
    }
}
