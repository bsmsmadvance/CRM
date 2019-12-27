using Database.Models;
using Database.Models.CMS;
using Commission.Params.Filters;
using Commission.Params.Outputs;
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
    public class CommissionLowRiseVeiwService : ICommissionLowRiseVeiwService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public CommissionLowRiseVeiwService(IConfiguration configuration, DatabaseContext db)
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

        public async Task<CommissionLowRiseVeiwPaging> GetCommissionLowRiseVeiwListAsync(CommissionLowRiseVeiwFilter filter, PageParam pageParam, CommissionLowRiseVeiwSortByParam sortByParam)
        {
            #region Header
            IQueryable<CalculatePerMonthLowRiseQueryResult> queryHeader = DB.CalculatePerMonthLowRises
                                              .Select(o => new CalculatePerMonthLowRiseQueryResult()
                                              {
                                                  CalculatePerMonthLowRise = o,
                                                  Project = o.Project,
                                                  CalculateUserName = o.CreatedBy
                                              });

            #region Filter
            if (filter.ProjectID != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthLowRise.ProjectID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                queryHeader = queryHeader.Where(x => x.CalculatePerMonthLowRise.PeriodYear == filter.PeriodYear && x.CalculatePerMonthLowRise.PeriodMonth == filter.PeriodMonth);
            }
            #endregion

            var resultHeader = await queryHeader.Select(o => CalculatePerMonthLowRiseDTO.CreateFromQueryResult(o)).FirstOrDefaultAsync();
            #endregion

            //ทำสัญญากับโอนในเดือนเดียวกัน
            IQueryable<CommissionLowRiseVeiwQueryResult> query1 = from cls in DB.CalculateLowRiseSales
                                                                  join clt in DB.CalculateLowRiseTransfers on cls.AgreementID equals clt.Transfer.AgreementID into g1
                                                                  from c in g1.Where(x => x.TransferID != null && !x.IsDeleted).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on cls.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on cls.AgreementID equals t.Transfer.AgreementID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = cls,
                                                                      CalculateLowRiseTransfer = c,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = cls.Agreement.Project,
                                                                      Unit = cls.Agreement.Unit,
                                                                      SaleUserName = cls.SaleUser,
                                                                      ProjectSaleUserName = cls.ProjectSaleUser
                                                                  };

            //ทำสัญญาแต่ยังไม่โอน
            IQueryable<CommissionLowRiseVeiwQueryResult> query2 = from cls in DB.CalculateLowRiseSales
                                                                  join clt in DB.CalculateLowRiseTransfers on cls.AgreementID equals clt.Transfer.AgreementID into g1
                                                                  from c in g1.Where(x => x.TransferID == null).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on cls.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on cls.AgreementID equals t.Transfer.AgreementID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = cls,
                                                                      CalculateLowRiseTransfer = null,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = cls.Agreement.Project,
                                                                      Unit = cls.Agreement.Unit,
                                                                      SaleUserName = cls.SaleUser,
                                                                      ProjectSaleUserName = cls.ProjectSaleUser
                                                                  };


            //ทำสัญญามาแล้วแต่มาโอนในเดือนนี้
            IQueryable<CommissionLowRiseVeiwQueryResult> query3 = from clt in DB.CalculateLowRiseTransfers
                                                                  join cls in DB.CalculateLowRiseSales on clt.Transfer.AgreementID equals cls.AgreementID into g1
                                                                  from c in g1.Where(x => x.AgreementID == null).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on clt.Transfer.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on clt.TransferID equals t.TransferID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = null,
                                                                      CalculateLowRiseTransfer = clt,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = clt.Transfer.Project,
                                                                      Unit = clt.Transfer.Unit,
                                                                      SaleUserName = clt.SaleUser,
                                                                      ProjectSaleUserName = clt.ProjectSaleUser
                                                                  };

            var query = query1.Union(query2).Union(query3);

            #region Filter
            if (filter.ProjectID != null)
            {
                query = query.Where(x => x.Project.ID == filter.ProjectID);
            }
            if (filter.PeriodYear != null && filter.PeriodMonth != null)
            {
                query = query.Where(x => x.CalculateLowRiseSale.PeriodYear == filter.PeriodYear && x.CalculateLowRiseSale.PeriodMonth == filter.PeriodMonth);
            }
            if (filter.UnitID != null)
            {
                query = query.Where(x => x.Unit.ID == filter.UnitID);
            }
            if (filter.SaleUserID != null)
            {
                query = query.Where(x => x.CalculateLowRiseSale.SaleUserID == filter.SaleUserID);
            }
            if (filter.ProjectSaleUserID != null)
            {
                query = query.Where(x => x.CalculateLowRiseSale.ProjectSaleUserID == filter.ProjectSaleUserID);
            }
            if (filter.CommissionPercentRate != null)
            {
                query = query.Where(x => x.CalculateLowRiseSale.CommissionPercentRate == filter.CommissionPercentRate);
            }
            if (filter.CommissionPercentType != null)
            {
                query = query.Where(x => x.CalculateLowRiseSale.CommissionPercentType == filter.CommissionPercentType);
            }
            if (filter.TotalContractNetAmountForm.HasValue)
            {
                query = query.Where(x => (x.Contract.SellingPrice - x.Contract.TransferDiscount ?? 0 - x.Contract.FreeDownAmount ?? 0) >= filter.TotalContractNetAmountForm);
            }
            if (filter.TotalContractNetAmountTo.HasValue)
            {
                query = query.Where(x => (x.Contract.SellingPrice - x.Contract.TransferDiscount ?? 0 - x.Contract.FreeDownAmount ?? 0) <= filter.TotalContractNetAmountTo);
            }
            if (filter.ActualTransferDateForm.HasValue)
            {
                query = query.Where(x => x.Transfer.TransferDate >= filter.ActualTransferDateForm);
            }
            if (filter.ActualTransferDateTo.HasValue)
            {
                query = query.Where(x => x.Transfer.TransferDate <= filter.ActualTransferDateTo);
            }
            if (filter.SaleUserSalePaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.SaleUserSalePaid >= filter.SaleUserSalePaidForm);
            }
            if (filter.SaleUserSalePaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.SaleUserSalePaid <= filter.SaleUserSalePaidTo);
            }
            if (filter.ProjectSaleSalePaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.ProjectSaleSalePaid >= filter.ProjectSaleSalePaidForm);
            }
            if (filter.ProjectSaleSalePaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.ProjectSaleSalePaid <= filter.ProjectSaleSalePaidTo);
            }
            if (filter.TotalSalePaidForm.HasValue)
            {
                query = query.Where(x => (x.CalculateLowRiseSale.SaleUserSalePaid + x.CalculateLowRiseSale.ProjectSaleSalePaid) >= filter.TotalSalePaidForm);
            }
            if (filter.TotalSalePaidTo.HasValue)
            {
                query = query.Where(x => (x.CalculateLowRiseSale.SaleUserSalePaid + x.CalculateLowRiseSale.ProjectSaleSalePaid) <= filter.TotalSalePaidTo);
            }
            if (filter.SaleUserNewLaunchPaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.SaleUserNewLaunchPaid >= filter.SaleUserNewLaunchPaidForm);
            }
            if (filter.SaleUserNewLaunchPaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.SaleUserNewLaunchPaid <= filter.SaleUserNewLaunchPaidTo);
            }
            if (filter.ProjectSaleNewLaunchPaidForm.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.ProjectSaleNewLaunchPaid >= filter.ProjectSaleNewLaunchPaidForm);
            }
            if (filter.ProjectSaleNewLaunchPaidTo.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.ProjectSaleNewLaunchPaid <= filter.ProjectSaleNewLaunchPaidTo);
            }
            if (filter.TotalNewLaunchPaidForm.HasValue)
            {
                query = query.Where(x => (x.CalculateLowRiseSale.SaleUserNewLaunchPaid + x.CalculateLowRiseSale.ProjectSaleNewLaunchPaid) >= filter.TotalNewLaunchPaidForm);
            }
            if (filter.TotalNewLaunchPaidTo.HasValue)
            {
                query = query.Where(x => (x.CalculateLowRiseSale.SaleUserNewLaunchPaid + x.CalculateLowRiseSale.ProjectSaleNewLaunchPaid) <= filter.TotalNewLaunchPaidTo);
            }
            if (filter.CommissionForThisMonthForm.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.TotalCommissionPaid >= filter.CommissionForThisMonthForm);
            }
            if (filter.CommissionForThisMonthTo.HasValue)
            {
                query = query.Where(x => x.CalculateLowRiseSale.TotalCommissionPaid <= filter.CommissionForThisMonthTo);
            }
            #endregion

            CommissionLowRiseVeiwDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<CommissionLowRiseVeiwQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => CommissionLowRiseVeiwDTO.CreateFromQueryResult(o)).ToList();

            resultHeader.CommissionLowRiseVeiws = results;

            return new CommissionLowRiseVeiwPaging()
            {
                PageOutput = pageOutput,
                CalculatePerMonthLowRise = resultHeader
            };
        }

        public async Task<FileDTO> ExportExcelCommissionLowRiseAsync(Guid? ProjectID, DateTime? CommissionMonth)
        {
            ExportExcel result = new ExportExcel();

            //ทำสัญญากับโอนในเดือนเดียวกัน
            IQueryable<CommissionLowRiseVeiwQueryResult> query1 = from cls in DB.CalculateLowRiseSales
                                                                  join clt in DB.CalculateLowRiseTransfers on cls.AgreementID equals clt.Transfer.AgreementID into g1
                                                                  from c in g1.Where(x => x.TransferID != null && !x.IsDeleted).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on cls.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on cls.AgreementID equals t.Transfer.AgreementID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = cls,
                                                                      CalculateLowRiseTransfer = c,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = cls.Agreement.Project,
                                                                      Unit = cls.Agreement.Unit,
                                                                      SaleUserName = cls.SaleUser,
                                                                      ProjectSaleUserName = cls.ProjectSaleUser
                                                                  };

            #region Filter1            
            if (ProjectID != null)
            {
                query1 = query1.Where(x => x.CalculateLowRiseSale.Agreement.ProjectID == ProjectID);
            }
            if (CommissionMonth != null)
            {
                query1 = query1.Where(x => x.CalculateLowRiseSale.PeriodYear == CommissionMonth.Value.Year && x.CalculateLowRiseSale.PeriodMonth == CommissionMonth.Value.Month);
            }
            #endregion

            //ทำสัญญาแต่ยังไม่โอน
            IQueryable<CommissionLowRiseVeiwQueryResult> query2 = from cls in DB.CalculateLowRiseSales
                                                                  join clt in DB.CalculateLowRiseTransfers on cls.AgreementID equals clt.Transfer.AgreementID into g1
                                                                  from c in g1.Where(x => x.TransferID == null).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on cls.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on cls.AgreementID equals t.Transfer.AgreementID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = cls,
                                                                      CalculateLowRiseTransfer = null,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = cls.Agreement.Project,
                                                                      Unit = cls.Agreement.Unit,
                                                                      SaleUserName = cls.SaleUser,
                                                                      ProjectSaleUserName = cls.ProjectSaleUser
                                                                  };

            #region Filter2            
            if (ProjectID != null)
            {
                query2 = query2.Where(x => x.CalculateLowRiseSale.Agreement.ProjectID == ProjectID);
            }
            if (CommissionMonth != null)
            {
                query2 = query2.Where(x => x.CalculateLowRiseSale.PeriodYear == CommissionMonth.Value.Year && x.CalculateLowRiseSale.PeriodMonth == CommissionMonth.Value.Month);
            }
            #endregion

            //ทำสัญญามาแล้วแต่มาโอนในเดือนนี้
            IQueryable<CommissionLowRiseVeiwQueryResult> query3 = from clt in DB.CalculateLowRiseTransfers
                                                                  join cls in DB.CalculateLowRiseSales on clt.Transfer.AgreementID equals cls.AgreementID into g1
                                                                  from c in g1.Where(x => x.AgreementID == null).DefaultIfEmpty()
                                                                  join a in DB.CommissionContracts on clt.Transfer.AgreementID equals a.AgreementID into g2
                                                                  from ag in g2.DefaultIfEmpty()
                                                                  join t in DB.CommissionTransfers on clt.TransferID equals t.TransferID into g3
                                                                  from tf in g3.DefaultIfEmpty()
                                                                  select new CommissionLowRiseVeiwQueryResult()
                                                                  {
                                                                      CalculateLowRiseSale = null,
                                                                      CalculateLowRiseTransfer = clt,
                                                                      Contract = ag,
                                                                      Transfer = tf,
                                                                      Project = clt.Transfer.Project,
                                                                      Unit = clt.Transfer.Unit,
                                                                      SaleUserName = clt.SaleUser,
                                                                      ProjectSaleUserName = clt.ProjectSaleUser
                                                                  };

            #region Filter3            
            if (ProjectID != null)
            {
                query3 = query3.Where(x => x.CalculateLowRiseTransfer.Transfer.ProjectID == ProjectID);
            }
            if (CommissionMonth != null)
            {
                query3 = query3.Where(x => x.CalculateLowRiseTransfer.PeriodYear == CommissionMonth.Value.Year && x.CalculateLowRiseTransfer.PeriodMonth == CommissionMonth.Value.Month);
            }
            #endregion
            
            //CommissionLowRiseVeiwDTO.SortBy(sortByParam, ref query);

            var query = query1.Union(query2).Union(query3);
            var data = await query.ToListAsync();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "CommissionLowLise_MM_YYYY.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _bGNoIndex = CommissionLowRiseVeiwExcelModel._bGNoIndex + 1;
                int _projectNoIndex = CommissionLowRiseVeiwExcelModel._projectNoIndex + 1;
                int _projectNameIndex = CommissionLowRiseVeiwExcelModel._projectNameIndex + 1;
                int _unitNoIndex = CommissionLowRiseVeiwExcelModel._unitNoIndex + 1;
                //int _leaderNameIndex = CommissionLowRiseVeiwExcelModel._leaderNameIndex + 1;
                int _saleUserEmpNoIndex = CommissionLowRiseVeiwExcelModel._saleUserEmpNoIndex + 1;
                int _saleUserNameIndex = CommissionLowRiseVeiwExcelModel._saleUserNameIndex + 1;
                int _projectSaleEmpNoIndex = CommissionLowRiseVeiwExcelModel._projectSaleEmpNoIndex + 1;
                int _projectSaleNameIndex = CommissionLowRiseVeiwExcelModel._projectSaleNameIndex + 1;
                //int _customerNameIndex = CommissionLowRiseVeiwExcelModel._customerNameIndex + 1;
                int _bookingDateIndex = CommissionLowRiseVeiwExcelModel._bookingDateIndex + 1;
                int _contractDateIndex = CommissionLowRiseVeiwExcelModel._contractDateIndex + 1;
                int _approveDateIndex = CommissionLowRiseVeiwExcelModel._approveDateIndex + 1;
                int _signContractApproveDateIndex = CommissionLowRiseVeiwExcelModel._signContractApproveDateIndex + 1;
                int _transferDateIndex = CommissionLowRiseVeiwExcelModel._transferDateIndex + 1;
                int _sellingPriceIndex = CommissionLowRiseVeiwExcelModel._sellingPriceIndex + 1;
                int _rateIndex = CommissionLowRiseVeiwExcelModel._rateIndex + 1;
                int _saleUserTransferPaidIndex = CommissionLowRiseVeiwExcelModel._saleUserTransferPaidIndex + 1;
                int _projectSaleTransferPaidIndex = CommissionLowRiseVeiwExcelModel._projectSaleTransferPaidIndex + 1;
                int _totalTransferPaidIndex = CommissionLowRiseVeiwExcelModel._totalTransferPaidIndex + 1;
                int _saleUserNewLaunchPaidIndex = CommissionLowRiseVeiwExcelModel._saleUserNewLaunchPaidIndex + 1;
                int _projectSaleNewLaunchPaidIndex = CommissionLowRiseVeiwExcelModel._projectSaleNewLaunchPaidIndex + 1;
                int _totalNewLaunchPaidIndex = CommissionLowRiseVeiwExcelModel._totalNewLaunchPaidIndex + 1;
                int _commissionForThisMonthIndex = CommissionLowRiseVeiwExcelModel._commissionForThisMonthIndex + 1;
                //int _flagDataIndex = CommissionLowRiseVeiwExcelModel._flagDataIndex + 1;



                var prj = await DB.Projects.Where(x => x.ID == ProjectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    //worksheet.Cells[c, _effectiveMonthIndex].Style.Numberformat.Format = "mm/yyyy";
                    //worksheet.Cells[c, _effectiveMonthIndex].Value = DateTime.Now; //data[c - 2].CommissionLowRiseVeiw.ActiveDate;

                    worksheet.Cells[c, _bGNoIndex].Value = data[c - 2].Project?.BG.BGNo;
                    worksheet.Cells[c, _projectNoIndex].Value = data[c - 2].Project?.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = data[c - 2].Project?.ProjectNameTH;
                    worksheet.Cells[c, _unitNoIndex].Value = data[c - 2].Contract?.UnitNo;
                    //worksheet.Cells[c, _leaderNameIndex].Value = data[c - 2].Project?.LeaderName;
                    worksheet.Cells[c, _saleUserEmpNoIndex].Value = data[c - 2].CalculateLowRiseSale.SaleUser?.EmployeeNo;
                    worksheet.Cells[c, _saleUserNameIndex].Value = data[c - 2].CalculateLowRiseSale.SaleUser?.DisplayName;
                    worksheet.Cells[c, _projectSaleEmpNoIndex].Value = data[c - 2].CalculateLowRiseSale.ProjectSaleUser?.EmployeeNo;
                    worksheet.Cells[c, _projectSaleNameIndex].Value = data[c - 2].CalculateLowRiseSale.ProjectSaleUser?.DisplayName;
                    //worksheet.Cells[c, _customerNameIndex].Value = data[c - 2].Project?.CustomerName;
                    worksheet.Cells[c, _bookingDateIndex].Value = data[c - 2].Contract?.BookingDate;
                    worksheet.Cells[c, _contractDateIndex].Value = data[c - 2].Contract?.ContractDate;
                    worksheet.Cells[c, _approveDateIndex].Value = data[c - 2].Contract?.ApproveDate;
                    worksheet.Cells[c, _signContractApproveDateIndex].Value = data[c - 2].Contract?.SignContractApproveDate;
                    worksheet.Cells[c, _transferDateIndex].Value = data[c - 2].Transfer?.TransferDate;
                    worksheet.Cells[c, _sellingPriceIndex].Value = data[c - 2].Contract.SellingPrice - data[c - 2].Contract.TransferDiscount ?? 0 - data[c - 2].Contract.FreeDownAmount ?? 0;
                    worksheet.Cells[c, _rateIndex].Value = data[c - 2].CalculateLowRiseSale?.CommissionPercentRate;
                    worksheet.Cells[c, _saleUserTransferPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.SaleUserTransferPaid;
                    worksheet.Cells[c, _projectSaleTransferPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.ProjectSaleTransferPaid;
                    worksheet.Cells[c, _totalTransferPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.SaleUserTransferPaid + data[c - 2].CalculateLowRiseSale?.ProjectSaleTransferPaid;
                    worksheet.Cells[c, _saleUserNewLaunchPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.SaleUserNewLaunchPaid;
                    worksheet.Cells[c, _projectSaleNewLaunchPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.ProjectSaleNewLaunchPaid;
                    worksheet.Cells[c, _totalNewLaunchPaidIndex].Value = data[c - 2].CalculateLowRiseSale?.SaleUserNewLaunchPaid + data[c - 2].CalculateLowRiseSale?.ProjectSaleNewLaunchPaid;
                    worksheet.Cells[c, _commissionForThisMonthIndex].Value = data[c - 2].CalculateLowRiseSale?.TotalCommissionPaid;
                    //worksheet.Cells[c, _flagDataIndex].Value = data[c - 2].CalculateLowRiseSale?.FlagData;

                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = string.Format("{0}_CommissionLowLise_{1:00}_{2:0000}.xlsx", prj.ProjectNo, CommissionMonth.Value.Month, CommissionMonth.Value.Year);
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{ProjectID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }
    }
}
