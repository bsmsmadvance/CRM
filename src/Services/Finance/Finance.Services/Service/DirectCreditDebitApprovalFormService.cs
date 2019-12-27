using Base.DTOs.FIN;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Services.IService;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using PagingExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Base.DTOs;
using System.IO;
using FileStorage;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Database.Models.FIN;
using Database.Models.SAL;
using Base.DTOs.MST;
using Database.Models.MST;
using Base.DTOs.SAL;
using Database.Models.MasterKeys;

namespace Finance.Services.Service
{
    public class DirectCreditDebitApprovalFormService : IDirectCreditDebitApprovalFormService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public DirectCreditDebitApprovalFormService(DatabaseContext db, IConfiguration configuration)
        {
            this.DB = db;

            this.Configuration = configuration;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:TempBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        private IQueryable<GetDirectCreditDebitApprovalFormListResult> getData(DirectCreditDebitApprovalFormFilter filter)
        {
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = from o in DB.DirectCreditDebitApprovalForms
                                                                           .Include(x => x.BankAccount)
                                                                                    .ThenInclude(x => x.Company)
                                                                           .Include(x => x.BankAccount)
                                                                                    .ThenInclude(x => x.BankBranch)
                                                                           .Include(x => x.Booking)
                                                                                    .ThenInclude(x => x.Project)
                                                                           .Include(x => x.Province)


                                                                           join amt in DB.Agreements on o.BookingID equals amt.BookingID into amtg
                                                                           from amto in amtg.DefaultIfEmpty()

                                                                           join ao in DB.AgreementOwners.Where(x => x.IsMainOwner == true) on o.ID equals ao.AgreementID into aoData
                                                                           from aoModel in aoData.DefaultIfEmpty()
                                                                           select new GetDirectCreditDebitApprovalFormListResult
                                                                           {
                                                                               DirectCreditDebitApprovalForm = o,
                                                                               Booking = o.Booking,
                                                                               DirectApprovalFormType = o.DirectApprovalFormType,
                                                                               DirectApprovalFormStatus = o.DirectApprovalFormStatus,

                                                                               BankAccount = o.BankAccount,
                                                                               Company = o.BankAccount.Company,
                                                                               Bank = o.BankAccount.Bank,
                                                                               BankBranch = o.BankAccount.BankBranch,

                                                                               Province = o.Province,
                                                                               Project = o.Booking.Project,
                                                                               Unit = o.Booking.Unit,

                                                                               Agreement = amto ?? new Agreement(),
                                                                               AgreementOwner = aoModel ?? new AgreementOwner()
                                                                           };

            #region filter
            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(filter.Company.ToString(), out newGuid))
            {
                query = query.Where(x => x.Company.ID == filter.Company);
            }
            if (Guid.TryParse(filter.Project.ToString(), out newGuid))
            {
                query = query.Where(x => x.Project.ID == filter.Project);
            }
            if (!string.IsNullOrEmpty(filter.Unit))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.Unit));
            }
            if (!string.IsNullOrEmpty(filter.ContractNumber))
            {
                query = query.Where(x => x.Agreement.AgreementNo.Contains(filter.ContractNumber));
            }
            if (Guid.TryParse(filter.Bank.ToString(), out newGuid))
            {
                query = query.Where(x => x.Bank.ID == filter.Bank);
            }
            if (!string.IsNullOrEmpty(filter.AccountNO))
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.AccountNO.Contains(filter.AccountNO));
            }
            //if (!string.IsNullOrEmpty(filter.CustomerName))
            //{
            //    //query = query.Where(x => x.DirectCreditDebitApprovalForm.OwnerName.Contains(filter.CustomerName));
            //    query = query.Where(x => x.AgreementOwner.FirstNameTH
            //}
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                var xxx = filter.CustomerName.Split(' ');

                if (xxx.Length >= 0)
                {
                    if (xxx[0] != null)
                    {
                        query = query.Where(x => x.AgreementOwner.FirstNameTH.Contains(filter.CustomerName));
                    }
                }
                if (xxx.Length >= 2)
                {
                    if (xxx[1] != null)
                    {
                        query = query.Where(x => x.AgreementOwner.LastNameTH.Contains(filter.CustomerName));
                    }
                }
            }
            if (Guid.TryParse(filter.DirectApprovalFormType.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.DirectApprovalFormType.ID == filter.DirectApprovalFormType);
            }
            if (Guid.TryParse(filter.DirectApprovalFormStatus.ToString(), out newGuid))
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.DirectApprovalFormStatus.ID == filter.DirectApprovalFormStatus);
            }
            if ((filter.DirectPeriod ?? 0) > 0)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.DirectPeriod == filter.DirectPeriod);
            }


            if (filter.ExpireDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.CreditCardExpireYear >= filter.ExpireDateFrom.Value.Year
                                            && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth >= filter.ExpireDateFrom.Value.Month);
            }
            if (filter.ExpireDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.CreditCardExpireYear <= filter.ExpireDateFrom.Value.Year
                                         && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth <= filter.ExpireDateFrom.Value.Month);
            }

            if (filter.StartDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.StartDate >= filter.StartDateFrom);
            }
            if (filter.StartDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.StartDate <= filter.StartDateTo);
            }
            if (filter.CreateDateFrom != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.Created >= filter.CreateDateFrom);
            }
            if (filter.CreateDateTo != null)
            {
                query = query.Where(x => x.DirectCreditDebitApprovalForm.Created <= filter.CreateDateTo);
            }

            #endregion
            return query;
        }


        public async Task<GetUnitDirectCreditDebitApprovalPaging> GetUnitListAsync(Guid? id, DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam)
        {

            IQueryable<CreateDataResult> query = from o in DB.Agreements
                                                 join tf in DB.Transfers on o.ID equals tf.AgreementID into tfData
                                                 from tfModel in tfData.DefaultIfEmpty()
                                                 join ao in DB.AgreementOwners.Where(x => x.IsMainOwner == true) on o.ID equals ao.AgreementID into aoData
                                                 from aoModel in aoData.DefaultIfEmpty()


                                                 select new CreateDataResult
                                                 {
                                                     Agreement = o,
                                                     Unit = o.Unit,
                                                     Project = o.Project,
                                                     BookingID = o.BookingID,
                                                     CompanyID = o.Project.CompanyID,
                                                     Transfer = tfModel ?? new Transfer(),
                                                     AgreementOwner = aoModel ?? new AgreementOwner()
                                                 };


            #region filter
            var chkBooking = DB.DirectCreditDebitApprovalForms.Include(x => x.DirectApprovalFormStatus).ToList();
            List<string> chkID = new List<string>();
            chkID.Add(DirectApprovalFormStatusKey.New);
            chkID.Add(DirectApprovalFormStatusKey.Approved);
            var chkBooking2 = chkBooking.Where(x => chkID.Any(x2 => x2 == x.DirectApprovalFormStatus.Key)).Select(x => x.BookingID).ToList();
            var newGuid = Guid.NewGuid();

            if (chkBooking2.Count > 0)
            {
                query = query.Where(x => !chkBooking2.Any(x2 => x.BookingID == x2));
            }

            if (Guid.TryParse(id.ToString(), out newGuid))
            {
                query = query.Where(x => x.Project.ID == id);
            }

            if (!string.IsNullOrEmpty(filter.Unit))
            {
                query = query.Where(x => x.Unit.UnitNo.Contains(filter.Unit));
            }
            if (!string.IsNullOrEmpty(filter.CustomerName))
            {
                var xxx = filter.CustomerName.Split(' ');

                if (xxx.Length >= 0)
                {
                    if (xxx[0] != null)
                    {
                        query = query.Where(x => x.AgreementOwner.FirstNameTH.Contains(filter.CustomerName));
                    }
                }
                if (xxx.Length >= 2)
                {
                    if (xxx[1] != null)
                    {
                        query = query.Where(x => x.AgreementOwner.LastNameTH.Contains(filter.CustomerName));
                    }
                }


            }




            query = query.Where(x => x.Transfer.IsReadyToTransfer == false);
            #endregion

            AgreementNoTransferDTO.SortBy(sortByParam, ref query);
            PageOutput pageOuput = PagingHelper.Paging<CreateDataResult>(pageParam, ref query);
            var Data = query.ToList();
            var results = Data.Select(o => AgreementNoTransferDTO.CreateFromModel(o, DB)).ToList();
            return new GetUnitDirectCreditDebitApprovalPaging()
            {
                AgreementNoTransfer = results,
                PageOutput = pageOuput
            };
        }

        public async Task<DirectCreditDebitApprovalFormDTO> GetDirectCreditDebitApprovalFormAsync(Guid? id)
        {
            DirectCreditDebitApprovalFormFilter filter = new DirectCreditDebitApprovalFormFilter();
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = getData(filter);
            var results = new DirectCreditDebitApprovalFormDTO();
            if (id != null)
            {
                var queryResults = query.Where(x => x.DirectCreditDebitApprovalForm.ID == id).FirstOrDefault();
                results = DirectCreditDebitApprovalFormDTO.CreateFromQueryUnitPriceInstallmentResult(queryResults, DB);
            }
            return results;
        }

        public async Task<DirectCreditDebitApprovalFormPaging> GetDirectCreditDebitApprovalFormExpire3MonthsListAsync(DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam)
        {

            //  DirectCreditDebitApprovalFormFilter filterNull = new DirectCreditDebitApprovalFormFilter();
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = getData(filter);

            //วันหมดอายุ

            DateTime sDate = DateTime.Now;
            //DateTime.ParseExact(filter.ExpireDateFrom.ToString(), "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));

            DateTime lDate = sDate.AddMonths(+3);
            query = query.Where(x => x.DirectCreditDebitApprovalForm.CreditCardExpireYear >= sDate.Year
                                        && x.DirectCreditDebitApprovalForm.CreditCardExpireYear <= lDate.Year
                                        && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth >= sDate.Month
                                        && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth <= lDate.Month);


            PageOutput pageOuput = null;
            List<GetDirectCreditDebitApprovalFormListResult> queryResults = new List<GetDirectCreditDebitApprovalFormListResult>();
            DirectCreditDebitApprovalFormDTO.SortBy(sortByParam, ref query);

            pageOuput = PagingHelper.Paging<GetDirectCreditDebitApprovalFormListResult>(pageParam, ref query);

            queryResults = query.ToList();
            var results = queryResults.Select(o => DirectCreditDebitApprovalFormDTO.CreateFromQueryResult(o, DB)).ToList();

            return new DirectCreditDebitApprovalFormPaging()
            {
                DirectCreditDebitApprovalForms = results,
                PageOutput = pageOuput
            };
        }

        public async Task<DirectCreditDebitApprovalFormPaging> GetDirectCreditDebitApprovalFormListAsync(DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam)
        {
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = getData(filter);
            PageOutput pageOuput = null;
            List<GetDirectCreditDebitApprovalFormListResult> queryResults = new List<GetDirectCreditDebitApprovalFormListResult>();

            DirectCreditDebitApprovalFormDTO.SortBy(sortByParam, ref query);

            pageOuput = PagingHelper.Paging<GetDirectCreditDebitApprovalFormListResult>(pageParam, ref query);

            queryResults = query.ToList();
            var results = queryResults.Select(o => DirectCreditDebitApprovalFormDTO.CreateFromQueryResult(o, DB)).ToList();

            return new DirectCreditDebitApprovalFormPaging()
            {
                DirectCreditDebitApprovalForms = results,
                PageOutput = pageOuput
            };

        }


        public async Task<DirectCreditDebitApprovalFormDTO> getDataCreateAsync(Guid? ID)
        {

            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = from o in DB.Bookings
                .Include(x => x.Unit)
                .Include(x => x.Project)
                    .ThenInclude(x => x.Company)

                                                                           join ao in DB.Agreements on o.ID equals ao.BookingID into aoData
                                                                           from aoModel in aoData.DefaultIfEmpty()
                                                                           select new GetDirectCreditDebitApprovalFormListResult
                                                                           {
                                                                               Booking = o,
                                                                               Unit = o.Unit,
                                                                               Project = o.Project,
                                                                               Company = o.Project.Company,
                                                                               Agreement = aoModel ?? new Agreement(),

                                                                               AgreementOwner = null,
                                                                               Bank = null,
                                                                               BankAccount = null,
                                                                               DirectApprovalFormStatus = null,
                                                                               DirectApprovalFormType = null,
                                                                               DirectCreditDebitApprovalForm = null,
                                                                               PaymentUnitPriceItem = null,
                                                                               Province = null
                                                                           };
            var results = new DirectCreditDebitApprovalFormDTO();
            if (ID != null)
            {
                var queryResults = query.Where(x => x.Booking.ID == ID).FirstOrDefault();
                results = DirectCreditDebitApprovalFormDTO.CreateFromQueryUnitPriceInstallmentResult(queryResults, DB);
            }
            return results;
        }


        public async Task<FileDTO> ExportRequestAsync(bool Is3Month, DirectCreditDebitApprovalFormFilter filter)
        {
            ExportExcel result = new ExportExcel();

            IQueryable<GetDirectCreditDebitApprovalFormListResult> DataExport = getData(filter);


            //วันหมดอายุ
            if (Is3Month)
            {
                DateTime sDate = DateTime.Now;
                DateTime lDate = sDate.AddMonths(+3);
                DataExport = DataExport.Where(x => x.DirectCreditDebitApprovalForm.CreditCardExpireYear >= sDate.Year
                                            && x.DirectCreditDebitApprovalForm.CreditCardExpireYear <= lDate.Year
                                            && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth >= sDate.Month
                                            && x.DirectCreditDebitApprovalForm.CreditCardExpireMonth <= lDate.Month);
            }
            DataExport = DataExport.Where(x => x.DirectApprovalFormStatus.Key == "New");
            var Data = DataExport.ToList();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplatesExport.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int nRow = 3;
                foreach (var row in Data)
                {

                    var UnitPriceInstallment = DB.UnitPriceInstallments
                                               .Include(o => o.InstallmentOfUnitPriceItem)
                                               .ThenInclude(o => o.UnitPrice)
                                               .Where(o => o.InstallmentOfUnitPriceItem.UnitPrice.BookingID == row.Booking.ID).ToList();
                    var AgreementOwners = DB.AgreementOwners
                                                .Include(o => o.FromContact)
                                                .Where(o => o.AgreementID == row.Agreement.ID && o.IsMainOwner == true).FirstOrDefault();

                    string tAgreementOwners = AgreementOwners.FirstNameTH ?? null + " " + AgreementOwners.LastNameTH ?? null;
                    worksheet.Cells[nRow, 1].Value = nRow - 2;
                    worksheet.Cells[nRow, 2].Value = DateTime.Now.ToString("MM/dd/yyyy");
                    worksheet.Cells[nRow, 3].Value = row.DirectApprovalFormStatus.Name ?? null;
                    worksheet.Cells[nRow, 4].Value = row.DirectCreditDebitApprovalForm.AccountNO ?? null;
                    worksheet.Cells[nRow, 5].Value = row.DirectCreditDebitApprovalForm.OwnerName ?? null;
                    worksheet.Cells[nRow, 6].Value = row.DirectCreditDebitApprovalForm.CitizenIdentityNo ?? null;
                    worksheet.Cells[nRow, 7].Value = AgreementOwners.ContactNo ?? null;
                    worksheet.Cells[nRow, 8].Value = row.Unit.UnitNo ?? null;
                    worksheet.Cells[nRow, 9].Value = row.Project.ProjectNameTH ?? null;
                    worksheet.Cells[nRow, 10].Value = row.Project.ProjectNo ?? null;
                    worksheet.Cells[nRow, 11].Value = tAgreementOwners;
                    worksheet.Cells[nRow, 12].Value = UnitPriceInstallment?.Min(x => x.Amount);
                    worksheet.Cells[nRow, 13].Value = row.DirectCreditDebitApprovalForm.Remark ?? null;
                    nRow++;
                }
                worksheet.Cells[1, 1].Value = "Enrollment Application lot " + DateTime.Today.ToString("dd-MM-YYYY");


                result.FileContent = package.GetAsByteArray();
                result.FileName = "Export_DirectCreditDebitApprovalForm" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"DirectCreditDebitApprovalForm/Export/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        public async Task<FileDTO> PrintDirectCreditDebitApprovalFormAsync(List<DirectCreditDebitApprovalFormDTO> ListDirectCreditDebitApprovalForm)
        {
            ExportExcel result = new ExportExcel();
            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "TemplatesReport.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int nRow = 3;
                foreach (var row in ListDirectCreditDebitApprovalForm)
                {
                    var AgreementOwners = DB.AgreementOwners
                                               .Include(o => o.FromContact)
                                               .Where(o => o.AgreementID == row.Agreement.Id && o.IsMainOwner == true).FirstOrDefault();
                    string Company = row.Company.SAPCompanyID ?? null + "-" + row.Company.NameTH;
                    string Project = row.Project.ProjectNo ?? null + "-" + row.Project.ProjectNameTH ?? null;
                    string tDate = (row.CreditCardExpireMonth + "/" + row.CreditCardExpireYear) ?? null;
                    string tAgreementOwners = AgreementOwners.FirstNameTH ?? null + " " + AgreementOwners.LastNameTH ?? null;
                    worksheet.Cells[nRow, 1].Value = nRow - 2;
                    worksheet.Cells[nRow, 2].Value = Company ?? null;
                    worksheet.Cells[nRow, 3].Value = Project;
                    worksheet.Cells[nRow, 4].Value = row.Unit.UnitNo ?? null;
                    worksheet.Cells[nRow, 5].Value = row.Agreement.AgreementNo ?? null;
                    worksheet.Cells[nRow, 6].Value = row.Bank.NameTH ?? null;
                    worksheet.Cells[nRow, 7].Value = row.AccountNO;
                    if (tDate != null)
                    {
                        worksheet.Cells[nRow, 8].Value = tDate;
                    }

                    worksheet.Cells[nRow, 9].Value = tAgreementOwners;
                    worksheet.Cells[nRow, 10].Value = row.StartDate ?? null;
                    worksheet.Cells[nRow, 11].Value = row.DirectApprovalFormType.Name ?? null;
                    worksheet.Cells[nRow, 12].Value = row.DirectPeriod;
                    worksheet.Cells[nRow, 13].Value = row.Updated ?? null;
                    worksheet.Cells[nRow, 14].Value = row.DirectApprovalFormStatus.Name ?? null;
                    nRow++;
                }
                worksheet.Cells[1, 1].Value = "Lot " + DateTime.Today.ToString("dd-MM-YYYY");
                result.FileContent = package.GetAsByteArray();
                result.FileName = "Report_DirectCreditDebitApprovalForm" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = result.FileName;
            string contentType = result.FileType;
            string filePath = $"DirectCreditDebitApprovalForm/Report/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }



        public async Task<DirectCreditDebitApprovalFormDTO> UpdateDirectCreditDebitApprovalFormAsync(DirectCreditDebitApprovalFormDTO input)
        {
            await input.ValidateAsync(DB);
            DirectCreditDebitApprovalFormFilter filter = new DirectCreditDebitApprovalFormFilter();
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = getData(filter);
            var results = new DirectCreditDebitApprovalFormDTO();

            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(input.Id.ToString(), out newGuid))
            {
                query.Where(x => x.DirectCreditDebitApprovalForm.ID == input.Id);
                var model = query.Where(x => x.DirectCreditDebitApprovalForm.ID == input.Id).Select(x => x.DirectCreditDebitApprovalForm).FirstOrDefault();



                string tDate = DateTime.Today.ToString("dd/MM/yyyy");
                switch (input.DirectApprovalFormStatus.Key)
                {
                    case "Approved":
                        if (model.ApproveDate == null)
                            model.ApproveDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                        break;
                    case "NotApproved":
                        if (model.RejectDate == null)
                            model.RejectDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                        break;
                    case "Cancel":
                        if (model.CancelDate == null)
                        {
                            model.CancelDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                            //model.IsDeleted = true;
                        }
                        break;
                    case "CancelTransferred":
                        if (model.CancelDate == null)
                        {
                            model.CancelDate = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"));
                            //model.IsDeleted = true;
                        }
                        break;
                }

                //DirectCredit
                var isDirectType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormType).ToList();
                var isDirectCredit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectCredit).Select(x => x.ID).FirstOrDefault();
                var isDirectDebit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectDebit).Select(x => x.ID).FirstOrDefault();


                if (input.DirectApprovalFormType.Id == isDirectCredit)
                {
                    model.CreditCardExpireMonth = input.CreditCardExpireMonth;
                    model.CreditCardExpireYear = input.CreditCardExpireYear;
                    model.ProvinceID = null;
                    model.BankBranchName = null;
                    model.BankBranchID = null;
                }
                else if(input.DirectApprovalFormType.Id == isDirectDebit) 
                {
                    model.CreditCardExpireMonth = null;
                    model.CreditCardExpireYear = null;
                    model.ProvinceID = input.Province.Id;
                    model.BankBranchName = input.BankBranch.Name;
                    model.BankBranchID = input.BankBranch.Id;

                }
                var DirectApprovalFormStatusApproved = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus && x.Key == DirectApprovalFormStatusKey.Approved).FirstOrDefault();
                if (input.DirectApprovalFormStatus.Id == DirectApprovalFormStatusApproved.ID)
                {
                    model.StartDate = input.StartDate;
                }

                model.BookingID = input.Booking.Id ?? Guid.NewGuid();
                model.OwnerName = input.OwnerName;
                model.DirectPeriod = input.DirectPeriod;
                model.AccountNO = input.AccountNO;
                model.BankID = input.Bank.Id;
                model.CitizenIdentityNo = input.CitizenIdentityNo;
                model.DirectApprovalFormTypeMasterCenterID = input.DirectApprovalFormType.Id;
                model.DirectApprovalFormStatusMasterCenterID = input.DirectApprovalFormStatus.Id;
                model.Remark = input.Remark ?? null;
                model.BankAccountID = input.Bank.BankAccountID;

                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                results = await GetDirectCreditDebitApprovalFormAsync(input.Id);
            }
            return results;
        }
        public async Task<DirectCreditDebitApprovalFormDTO> CreateDirectCreditDebitApprovalFormAsync(DirectCreditDebitApprovalFormDTO input)
        {
            await input.ValidateAsync(DB);
            var results = new DirectCreditDebitApprovalFormDTO();
            var model = new DirectCreditDebitApprovalForm();

            model.BookingID = input.Booking.Id ?? Guid.NewGuid();
            model.OwnerName = input.OwnerName;
            model.DirectPeriod = input.DirectPeriod;
            model.AccountNO = input.AccountNO;

            model.BankID = input.Bank.Id;
            model.CitizenIdentityNo = input.CitizenIdentityNo;
            model.DirectApprovalFormTypeMasterCenterID = input.DirectApprovalFormType.Id;
            model.DirectApprovalFormStatusMasterCenterID = input.DirectApprovalFormStatus.Id;

            //  DirectCredit
            //DirectCredit
            var isDirectType = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormType).ToList();
            var isDirectCredit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectCredit).Select(x => x.ID).FirstOrDefault();
            var isDirectDebit = isDirectType.Where(x => x.Key == DirectApprovalFormTypeKeys.DirectDebit).Select(x => x.ID).FirstOrDefault();

            if (input.DirectApprovalFormType.Id == isDirectCredit)
            {
                model.CreditCardExpireMonth = input.CreditCardExpireMonth;
                model.CreditCardExpireYear = input.CreditCardExpireYear;
                model.ProvinceID = null;
                model.BankBranchName = null;
                model.BankBranchID = null;
            }
            else if (input.DirectApprovalFormType.Id == isDirectDebit)
            {
                model.CreditCardExpireMonth = null;
                model.CreditCardExpireYear = null;
                model.ProvinceID = input.Province.Id;
                model.BankBranchName = input.BankBranch.Name;
                model.BankBranchID = input.BankBranch.Id;

            }
            var DirectApprovalFormStatusApproved = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus && x.Key == DirectApprovalFormStatusKey.Approved).FirstOrDefault();
            if (input.DirectApprovalFormStatus.Id == DirectApprovalFormStatusApproved.ID)
            {
                model.StartDate = input.StartDate;
            }

            model.Remark = input.Remark ?? null;
            model.BankAccountID = input.Bank.BankAccountID;


            await DB.DirectCreditDebitApprovalForms.AddAsync(model);
            await DB.SaveChangesAsync();
            results = await GetDirectCreditDebitApprovalFormAsync(model.ID);
            return results;
        }

        public async Task<List<BankAccNameDTO>> GetBankDirectCreditDropdowListAsync(Guid? ComID)
        {
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = from o in DB.BankAccounts
                .Include(o => o.Bank)
                                                                           select new GetDirectCreditDebitApprovalFormListResult
                                                                           {
                                                                               BankAccount = o,
                                                                               Bank = o.Bank
                                                                           };
            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(ComID.ToString(), out newGuid))
            {
                query = query.Where(x => x.BankAccount.CompanyID == ComID);
            }

            query = query.Where(o => o.BankAccount.IsDirectCredit == true && o.Bank.IsDeleted == false);
            var results = query.Select(o => BankAccNameDTO.CreateFromModel(o.BankAccount)).ToList();

            return results;
        }

        public async Task<List<BankAccNameDTO>> GetBankDirectDebitDropdowListAsync(Guid? ComID)
        {
            IQueryable<GetDirectCreditDebitApprovalFormListResult> query = from o in DB.BankAccounts
                .Include(o => o.Bank)
                                                                           select new GetDirectCreditDebitApprovalFormListResult
                                                                           {
                                                                               BankAccount = o,
                                                                               Bank = o.Bank
                                                                           };
            var newGuid = Guid.NewGuid();
            if (Guid.TryParse(ComID.ToString(), out newGuid))
            {
                query = query.Where(x => x.BankAccount.CompanyID == ComID);
            }
            query = query.Where(o => o.BankAccount.IsDirectDebit == true && o.Bank.IsDeleted == false);
            var results = query.Select(o => BankAccNameDTO.CreateFromModel(o.BankAccount)).ToList();

            return results;
        }




        public async Task<List<MasterCenterDropdownDTO>> GetStatusDropdowListAsync()
        {
            var query = DB.MasterCenters.Where(x => x.MasterCenterGroupKey == MasterCenterGroupKeys.DirectApprovalFormStatus).OrderBy(x => x.Order).ToList();

            var results = query.Select(o => MasterCenterDropdownDTO.CreateFromModel(o)).ToList();

            return results;
        }
    }
}


