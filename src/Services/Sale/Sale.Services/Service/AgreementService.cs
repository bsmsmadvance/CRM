using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.NTF;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using FileStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using static Base.DTOs.SAL.AgreementListDTO;

namespace Sale.Services.Service
{
    public class AgreementService : IAgreementService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public AgreementService(DatabaseContext db, IConfiguration configuration)
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

        public async Task<AgreementDTO> GetAgreementAsync(Guid agreementId)
        {
            var model = await DB.Agreements
                .Include(o => o.Booking)
                .Include(o => o.Unit)
                .ThenInclude(o => o.Model)
                .ThenInclude(o => o.TypeOfRealEstate)
                .Include(o => o.Project)
                .Include(o => o.SignContractRequestUser)
                .Include(o => o.AgreementStatus)
                .Include(o => o.PrintApprovedBy)
                .Include(o => o.HighRiseConstructionStatus)
                .Where(o => o.ID == agreementId).FirstOrDefaultAsync() ?? new Agreement();


            var result = await AgreementDTO.CreateFromModelAsync(model, FileHelper, DB);
            return result;
        }

        public async Task<AgreementDTO> GetAgreementByUnitAsync(Guid unitID)
        {
            var model = await DB.Agreements
                .Include(o => o.Booking)
                .Include(o => o.Unit)
                .Include(o => o.Project)
                .Include(o => o.SignContractRequestUser)
                .Include(o => o.AgreementStatus)
                .Include(o => o.PrintApprovedBy)
                .Include(o => o.HighRiseConstructionStatus)
                .Where(o => o.Unit.ID == unitID).FirstOrDefaultAsync() ?? new Agreement();

            var result = new AgreementDTO();

            result = await AgreementDTO.CreateFromModelAsync(model, FileHelper, DB);

            if (model.UnitID == null)
            {
                var model2 = await DB.Bookings
                  .Include(o => o.Unit)
                  .Include(o => o.Project)
                  .Where(o => o.Unit.ID == unitID).FirstOrDefaultAsync() ?? new Booking();

                result = await AgreementDTO.CreateFromModelByBookingAsync(model2, DB);
            }

            return result;
        }

        public async Task<AgreementDTO> ConvertToAgreementAsync(Guid bookingID)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            booking.BookingStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.Contract).Select(o => o.ID).FirstAsync();
            DB.Entry(booking).State = EntityState.Modified;

            var bookingUnitPrice = await DB.UnitPrices
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking).FirstAsync();
            bookingUnitPrice.IsActive = false;
            DB.Entry(bookingUnitPrice).State = EntityState.Modified;

            var bookingUnitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == bookingUnitPrice.ID).OrderBy(o => o.Order).ToListAsync();

            var agreement = new Agreement()
            {
                ProjectID = booking.ProjectID,
                UnitID = booking.UnitID,
                BookingID = booking.ID,
                AgreementStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.AgreementStatus && o.Key == AgreementStatusKeys.WaitingForContract).Select(o => o.ID).FirstAsync(),
                ContractDate = booking.ContractDate,
                TransferOwnershipDate = booking.TransferOwnershipDate
            };

            var agreementUniPrice = new UnitPrice()
            {
                BookingID = booking.ID,
                UnitPriceStageMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitPriceStage && o.Key == UnitPriceStageKeys.Agreement).Select(o => o.ID).FirstAsync(),
                IsActive = true
            };

            var agreementUntPriceItems = new List<UnitPriceItem>();
            var agreementUnitPriceInstallments = new List<UnitPriceInstallment>();
            foreach (var item in bookingUnitPriceItems)
            {
                var agreementUnitPriceItem = new UnitPriceItem()
                {
                    UnitPriceID = agreementUniPrice.ID,
                    Order = item.Order,
                    MasterPriceItemID = item.MasterPriceItemID,
                    Name = item.Name,
                    PriceUnitAmount = item.PriceUnitAmount,
                    PriceUnitMasterCenterID = item.PriceUnitMasterCenterID,
                    PricePerUnitAmount = item.PricePerUnitAmount,
                    Amount = item.Amount,
                    IsToBePay = item.IsToBePay,
                    Installment = item.Installment,
                    PriceTypeMasterCenterID = item.PriceTypeMasterCenterID,
                    PayDate = item.PayDate,
                    IsPaid = item.IsPaid,
                    DueDate = item.DueDate,
                    FromMasterPriceListItemID = item.FromMasterPriceListItemID
                };
                agreementUntPriceItems.Add(agreementUnitPriceItem);

                var bookingUnitPriceInstallment = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == item.ID).ToListAsync();
                foreach (var installment in bookingUnitPriceInstallment)
                {
                    var agreementUnitPriceInstallment = new UnitPriceInstallment()
                    {
                        Period = installment.Period,
                        InstallmentOfUnitPriceItemID = agreementUnitPriceItem.ID,
                        IsSpecialInstallment = installment.IsSpecialInstallment,
                        PayDate = installment.PayDate,
                        IsPaid = installment.IsPaid,
                        DueDate = installment.DueDate,
                        Amount = installment.Amount,
                        PaidAmount = installment.PaidAmount,
                        IsSellerPay = installment.IsSellerPay
                    };
                    agreementUnitPriceInstallments.Add(agreementUnitPriceInstallment);
                }
            }

            await DB.Agreements.AddAsync(agreement);
            await DB.UnitPrices.AddAsync(agreementUniPrice);
            await DB.UnitPriceItems.AddRangeAsync(agreementUntPriceItems);
            await DB.UnitPriceInstallments.AddRangeAsync(agreementUnitPriceInstallments);
            await DB.SaveChangesAsync();

            var result = await this.GetAgreementAsync(agreement.ID);
            return result;
        }

        public async Task<AgreementDTO> CreateAgreementAsync(Guid agreementID)
        {
            var model = await DB.Agreements
                .Include(o => o.Project)
                .Where(o => o.ID == agreementID).FirstOrDefaultAsync() ?? new Agreement();
            model.AgreementStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.AgreementStatus && o.Key == AgreementStatusKeys.WaitingForSignContract).Select(o => o.ID).FirstAsync();

            #region AgreementNo
            if (model.AgreementNo == null)
            {
                string year = Convert.ToString(DateTime.Today.Year);
                string month = DateTime.Today.ToString("MM");
                var key = "AG" + model.Project.ProjectNo + year[2] + year[3] + month;
                var type = "SAL.Agreement";
                var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
                if (runningno == null)
                {
                    var runningNumberCounter = new RunningNumberCounter
                    {
                        Key = key,
                        Type = type,
                        Count = 1
                    };

                    model.AgreementNo = key + runningNumberCounter.Count.ToString("0000") + "00";
                    runningNumberCounter.Count++;
                    await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                }
                else
                {
                    model.AgreementNo = key + runningno.Count.ToString("0000") + "00";
                    runningno.Count++;
                    DB.Entry(runningno).State = EntityState.Modified;
                }
            }
            #endregion

            var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstAsync();
            unit.UnitStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForTransfer).Select(o => o.ID).FirstAsync();

            var bookingPromotion = await DB.BookingPromotions.Where(e => e.BookingID == model.BookingID).FirstOrDefaultAsync();


            DB.Entry(model).State = EntityState.Modified;
            DB.Entry(unit).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await this.GetAgreementAsync(agreementID);
            return result;
        }

        private string GetAgreementFileName(string ProjectNo)
        {
            #region Running

            /*
                Format : <FAG><ProjectID><yy><MM><NNNN>
                Table  : MST.RunningNumberCounters
                Column : Key = "SAL.AgreementFile"
            */

            string year = Convert.ToString(DateTime.Today.Year);
            string month = DateTime.Today.ToString("MM");
            var runningKey = "FAG" + ProjectNo + year[2] + year[3] + month;
            var AgreementFileName = string.Empty;

            var runningNumber = DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "SAL.AgreementFile").FirstOrDefault();
            if (runningNumber == null)
            {
                var runningModel = new RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = "SAL.AgreementFile",
                    Count = 1
                };

                DB.RunningNumberCounters.AddAsync(runningModel);
                AgreementFileName = runningKey + runningModel.Count.ToString("0000");
            }
            else
            {
                runningNumber.Count = runningNumber.Count + 1;
                AgreementFileName = runningKey + runningNumber.Count.ToString("0000");
                DB.Entry(runningNumber).State = EntityState.Modified;
            }

            #endregion
            return AgreementFileName;
        }

        public async Task<List<AgreementFileDTO>> CreateAgreementFileAsync(Guid agreementID, List<FileDTO> input, Guid? userID)
        {
            var list = new List<AgreementFileDTO>();
            

            var query = await DB.Agreements.Where(e => e.ID == agreementID).FirstOrDefaultAsync() ?? new Agreement();
            var project = await DB.Projects.Where(e => e.ID == query.ProjectID).FirstOrDefaultAsync() ?? new Project();

           

            DateTime UTCNow = DateTime.UtcNow;
            int year = UTCNow.Year;
            int month = UTCNow.Month;
            int day = UTCNow.Day;
            int hour = UTCNow.Hour;
            int min = UTCNow.Minute;
            int sec = UTCNow.Second;

            var agreementFile = new AgreementFile();

            var path = year + month + day + hour + min + sec;

            if (input.Any())
            {
                foreach (var i in input)
                {
                    if (i != null)
                    {
                        var filename = GetAgreementFileName(project.ProjectNo);
                        filename += $"{Path.GetExtension(i.Name)}";

                        var result = new AgreementFileDTO();
                        result.Agreement = AgreementDropdownDTO.CreateFromModel(query);
                        result.AgreementNo = query.AgreementNo;

                        if (i.IsTemp)
                        {
                            string pathName = $"fag/{path}/{filename}";
                            await FileHelper.MoveTempFileAsync(filename, pathName);

                            string url = await FileHelper.GetFileUrlAsync(pathName);

                            var newFile = new FileDTO()
                            {
                                Name = filename,
                                Url = url
                            };

                            result.FileName = filename;
                            result.Files = newFile;
                        }
                        else
                        {
                            result.Files = i;

                        }

                        agreementFile.AgreementID = agreementID;
                        agreementFile.FileName = filename;
                        agreementFile.CreatedByUserID = userID;

                        DB.AgreementFiles.Add(agreementFile);
                        list.Add(result);
                    }
                }
            }

            DB.SaveChanges();

            return list;
        }

        public async Task<List<AgreementFileDTO>> GetAgreementFileListAsync(Guid agreementID)
        {
            var fileList = await DB.AgreementFiles.Where(o => o.AgreementID == agreementID && o.IsDeleted != true).ToListAsync();
            var result = new List<AgreementFileDTO>();

            if (fileList.Any())
            {
                foreach (var f in fileList)
                {
                    f.Agreement = await DB.Agreements.Where(o => o.ID == agreementID).FirstAsync();
                    var item = await AgreementFileDTO.CreateFromModel(f);
                    result.Add(item);
                }
            }

            return result;
        }

        public void CreateNotificationAgreementFileAsync(Guid agreementID, string Message, Guid? userID)
        {


            var ntf = new WebNotification();
            ntf.Created = DateTime.Now;
            ntf.Message = Message;
            ntf.CreatedByUserID = userID;
            ntf.Status = SendStatus.Create;


            var ag = DB.Agreements.Where(o => o.ID == agreementID).FirstOrDefault() ?? new Agreement();
            var up = DB.UserDefaultProjects.Where(o => o.ProjectID == ag.ProjectID).ToList() ?? new List<Database.Models.USR.UserDefaultProject>();

            if (up.Any())
            {
                foreach (var p in up)
                {
                    ntf.UserID = p.UserID;
                    DB.WebNotifications.Add(ntf);
                }
            }
            else
            {
                var agRoleID = DB.Roles.Where(o => o.Code == "AG").Select(o => o.ID).FirstOrDefault();
                var agUsers = from r in DB.Users
                              .Where(o => o.UserRoles.Where(n => n.RoleID == agRoleID).Any())
                              select r;

                if (agUsers.Any())
                {
                    foreach (var p in agUsers)
                    {
                        ntf.UserID = p.ID;
                        DB.WebNotifications.Add(ntf);
                    }
                }
            }
            //ntf.UserID = ag.


            DB.SaveChanges();

        }

        public void CreateAgreementPrintingHistoryDataAsync(Guid agreementID, Guid? userID)
        {
            try
            {
                var agreement = DB.Agreements.Where(e => e.ID == agreementID).FirstOrDefault() ?? new Agreement();
                var item = new AgreementPrintingHistory();
                item.AgreementID = agreementID;
                item.Agreement = agreement;
                item.AgreementPrintingByUserID = userID;
                item.AgreementPrintingDate = DateTime.Now;
                item.CreatedByUserID = userID;
                DB.AgreementPrintingHistorys.Add(item);
                DB.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AgreementInstallmentDTO>> GetAgreementInstallmentDataAsync(Guid agreementID)
        {
            var list = new List<AgreementInstallmentDTO>();

            var agree = await DB.Agreements
              .Where(o => o.ID == agreementID).FirstOrDefaultAsync() ?? new Agreement();

            var unitPriceModel = await DB.UnitPrices
                .Include(o => o.Booking)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync() ?? new UnitPrice();

            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync() ?? new List<UnitPriceItem>();
                //var result = new AgreementInstallmentDTO();

                var book = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).FirstOrDefault() ?? new UnitPriceItem();
                if (book != null)
                {
                    var paymentIDs = await DB.Payments.Where(o => o.BookingID == agree.BookingID).Select(o => o.ID).ToListAsync();
                    var sumPayAmount = await DB.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).SumAsync(o => o.PayAmount);

                    list.Add(new AgreementInstallmentDTO()
                    {
                        InstallmentOfUnitPriceItem = "จอง",
                        Period = 0,
                        IsSpecialInstallment = false,
                        DueDate = book.DueDate,
                        PayDate = book.PayDate,
                        Amount = book.Amount,
                        PaidAmount = sumPayAmount,
                        RemainAmount = book.Amount - sumPayAmount
                    });
                }

                var contract = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).FirstOrDefault();
                if (contract != null)
                {
                    var paymentIDs = await DB.Payments.Where(o => o.BookingID == agree.BookingID).Select(o => o.ID).ToListAsync();
                    var sumPayAmount = await DB.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).SumAsync(o => o.PayAmount);

                    list.Add(new AgreementInstallmentDTO()
                    {
                        InstallmentOfUnitPriceItem = "สัญญา",
                        Period = 0,
                        IsSpecialInstallment = false,
                        DueDate = contract.DueDate,
                        PayDate = contract.PayDate,
                        Amount = contract.Amount,
                        PaidAmount = sumPayAmount,
                        RemainAmount = contract.Amount - sumPayAmount
                    });
                }

                var downAmountItemId = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.ID).FirstOrDefault();
                var installments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == downAmountItemId).ToListAsync();

                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList() ?? new List<UnitPriceInstallment>();
                    var specialInstallment = installments.Where(o => o.IsSpecialInstallment == true).ToList() ?? new List<UnitPriceInstallment>();

                    if (normalInstallment.Any())
                    {

                        foreach (var item in normalInstallment.OrderBy(e => e.Period))
                        {
                            list.Add(new AgreementInstallmentDTO()
                            {
                                InstallmentOfUnitPriceItem = "งวดดาวน์",
                                Period = item.Period,
                                IsSpecialInstallment = false,
                                DueDate = item.DueDate,
                                PayDate = item.PayDate,
                                Amount = item.Amount,
                                PaidAmount = item.PaidAmount,
                                RemainAmount = item.Amount - item.PaidAmount
                            });
                        }
                    }

                    if (specialInstallment.Any())
                    {

                        foreach (var item in specialInstallment.OrderBy(e => e.Period))
                        {
                            list.Add(new AgreementInstallmentDTO()
                            {
                                InstallmentOfUnitPriceItem = "งวดดาวน์พิเศษ",
                                Period = item.Period,
                                IsSpecialInstallment = true,
                                DueDate = item.DueDate,
                                PayDate = item.PayDate,
                                Amount = item.Amount,
                                PaidAmount = item.PaidAmount,
                                RemainAmount = item.Amount - item.PaidAmount
                            });
                        }
                    }
                }

                var transfer = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferAmount).FirstOrDefault();
                if (transfer != null)
                {
                    var paymentIDs = await DB.Payments.Where(o => o.BookingID == agree.BookingID).Select(o => o.ID).ToListAsync();
                    var sumPayAmount = await DB.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && o.MasterPriceItemID == MasterPriceItemKeys.TransferAmount).SumAsync(o => o.PayAmount);

                    list.Add(new AgreementInstallmentDTO()
                    {
                        InstallmentOfUnitPriceItem = "โอนกรรมสิทธิ์",
                        Period = 0,
                        IsSpecialInstallment = false,
                        DueDate = transfer.DueDate,
                        PayDate = transfer.PayDate,
                        Amount = transfer.Amount,
                        PaidAmount = sumPayAmount,
                        RemainAmount = transfer.Amount - sumPayAmount
                    });
                }
            }

            return list;
        }

        public async Task<AgreementPriceListDTO> CalculateAgreementInstallmentDataAsync(AgreementPriceListDTO agreementPriceListDTO)
        {
            var model = agreementPriceListDTO;
            model.DownAmount = ((model.NetSellingPrice * (decimal)(model.PercentDown ?? 0)) / 100) - (model.BookingAmount + model.ContractAmount);
            int remainPeriod = (model.Installment - model.SpecialInstallment) - ((model.IsSellerPay == true) ? 1 : 0);
            decimal otherAmount = model.SpecialInstallmentPeriods.Any() ? model.SpecialInstallmentPeriods.Sum(e => e.Amount) + ((model.IsSellerPay == true) ? model.InstallmentAmount : 0) : ((model.IsSellerPay == true) ? model.InstallmentAmount : 0);
            model.PercentDown = (double)(((model.DownAmount + (model.BookingAmount + model.ContractAmount)) * 100) / model.NetSellingPrice);
            var NormalDownAmount = model.DownAmount - otherAmount;
            model.InstallmentAmount = NormalDownAmount / remainPeriod;
            model.InstallmentAmount = Math.Round((model.InstallmentAmount > 0) ? model.InstallmentAmount : 0);
            model.DownAmount = (model.InstallmentAmount * remainPeriod) + otherAmount;
            model.TransferAmount = model.NetSellingPrice - (model.DownAmount + model.BookingAmount + model.ContractAmount);
            //model.tot = TransAmount + DownAmount + BookAmount + ContAmount;

            return model;
        }

        public async Task<AgreementPriceListDTO> GetPriceListDataAsync(Guid agreementID)
        {
            return await AgreementPriceListDTO.CreateFromModelPriceListAsync(agreementID, DB);
        }

        public async Task<AgreementPriceListDTO> GetAgreementPriceListAsync(Guid agreementID)
        {
            return await AgreementPriceListDTO.CreateFromModelAsync(agreementID, DB);
        }

        public async Task<AgreementListPaging> GetAgreementListAsync(AgreementListFilter filter, PageParam pageParam, AgreementListSortByParam sortByParam)
        {
            var query = from o in DB.Agreements
                            .Include(o => o.Booking)
                            .Include(o => o.Project)
                            .Include(o => o.Unit)
                        join ow in DB.AgreementOwners on o.ID equals ow.AgreementID into g
                        from owner in g.Where(p => p.IsMainOwner == true).DefaultIfEmpty()
                        join c in DB.ChangeAgreementOwnerWorkflows.Include(o => o.ChangeAgreementOwnerStatus) on o.ID equals c.AgreementID into gg
                        from wf in gg.DefaultIfEmpty()
                        where (wf == null
                                    || (wf.Created == (DB.ChangeAgreementOwnerWorkflows.Where(n => n.AgreementID == o.ID).OrderByDescending(n => n.Created).Max(n => n.Created)))
                                          && wf.IsApproved == false && wf.IsDeleted == false)
                        select new AgreementListQueryResult
                        {
                            Agreement = o,
                            AgreementOwner = owner,
                            ChangeAgreementOwnerWorkflow = wf
                        };

            #region Filter
            if (filter.ProjectID != null)
            {
                query = query.Where(o => o.Agreement.ProjectID == filter.ProjectID);
            }

            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(o => o.Agreement.Unit.UnitNo.Contains(filter.UnitNo));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(o => (o.AgreementOwner.FirstNameTH + o.AgreementOwner.LastNameTH).Trim().Contains(filter.FullName.Trim()));
            }

            if (!string.IsNullOrEmpty(filter.BookingNo))
            {
                query = query.Where(o => o.Agreement.Booking.BookingNo.Contains(filter.BookingNo));
            }

            if (!string.IsNullOrEmpty(filter.AgreementNo))
            {
                query = query.Where(o => o.Agreement.AgreementNo.Contains(filter.AgreementNo));
            }

            if (!string.IsNullOrEmpty(filter.AgreementStatusKey))
            {
                var AgreementStatusMasterCenterID = await DB.MasterCenters
                   .Where(x => x.Key == filter.AgreementStatusKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.AgreementStatus)
                   .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.Agreement.AgreementStatusMasterCenterID == AgreementStatusMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.AgreementStatusKeys))
            {
                var keys = filter.AgreementStatusKey.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.AgreementStatus)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.Agreement.AgreementStatusMasterCenterID ?? Guid.Empty));
            }

            if (!string.IsNullOrEmpty(filter.ChangeAgreementOwnerTypeKey))
            {
                var ChangeAgreementOwnerTypeMasterCenterID = await DB.MasterCenters
                   .Where(x => x.Key == filter.ChangeAgreementOwnerTypeKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.ChangeAgreementOwnerType)
                   .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerTypeMasterCenterID == ChangeAgreementOwnerTypeMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.ChangeAgreementOwnerTypeKeys))
            {
                var keys = filter.ChangeAgreementOwnerTypeKeys.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.ChangeAgreementOwnerType)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.ChangeAgreementOwnerWorkflow.ChangeAgreementOwnerTypeMasterCenterID ?? Guid.Empty));
            }
            #endregion

            AgreementListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<AgreementListQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var results = queryResults.Select(o => AgreementListDTO.CreateFromQueryResult(o)).ToList();

            return new AgreementListPaging()
            {
                PageOutput = pageOutput,
                Agreements = results
            };
        }

        public async Task<AgreementDTO> UpdateAgreementAsync(Guid agreementID, AgreementDTO agreement, BookingPriceListDTO priceList, BookingPromotionDTO agreementPromotion, List<BookingPromotionExpenseDTO> agreementExpenses, bool isMinPriceWorkflow = true)
        {
            var model = await DB.Agreements
                .Include(o => o.Booking)
                .Include(o => o.Project)
                .ThenInclude(o => o.ProductType)
                .Where(o => o.ID == agreementID).FirstAsync();

            var bookingId = model.Booking.ID;

            //model.ContractDueDate = booking.ContractDueDate;
            //model.BookingDate = booking.BookingDate;
            //model.ReferContactID = priceList.ReferContact?.Id;

            var agreementStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == AgreementStatusKeys.WaitingForSignContract).Select(o => o.ID).FirstAsync();
            var minPriceStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.WaitingForApproveMinPrice).Select(o => o.ID).FirstAsync();
          
            if (model.Booking.BookingStatusMasterCenterID != minPriceStatusId)
            {
                if (model.AgreementStatusMasterCenterID != agreementStatusId)
                {
                    model.AgreementStatusMasterCenterID = agreementStatusId;

                    var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstAsync();
                    unit.UnitStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForTransfer).Select(o => o.ID).FirstAsync();
                    DB.Entry(unit).State = EntityState.Modified;
                }
            }

            #region Agency
            //var agencies = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SaleOfficerType).ToListAsync();
            //model.SaleOfficerTypeMasterCenterID = booking.SaleOfficerType.Id;
            //model.ProjectSaleUserID = booking.ProjectSaleUser?.Id;

            //if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.AP).Select(o => o.ID).First())
            //{
            //    model.SaleUserID = booking.SaleUser?.Id;
            //}
            //else if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.Agency).Select(o => o.ID).First())
            //{
            //    model.AgentID = booking.Agent?.Id;
            //    model.AgentEmployeeID = booking.AgentEmployee?.Id;

            //}
            //else if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.Referal).Select(o => o.ID).First())
            //{
            //    model.SaleUserID = booking.SaleUser?.Id;
            //    model.AgentID = booking.Agent?.Id;
            //}
            #endregion

            #region Unit Price
            //var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == model.Booking.ID && o.IsActive == true).FirstAsync();
            //var UnitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
            //var lastOrder = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).OrderByDescending(o => o.Order).Select(o => o.Order).FirstAsync();
            //var fgfDiscount = await DB.UnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).FirstOrDefaultAsync();

            //if (fgfDiscount == null)
            //{
            //    var fgfModel = new UnitPriceItem()
            //    {
            //        UnitPriceID = unitPrice.ID,
            //        Order = lastOrder + 1,
            //        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Detail).FirstAsync(),
            //        Amount = priceList.FGFDiscount,
            //        IsToBePay = false,
            //        PricePerUnitAmount = priceList.FGFDiscount,
            //        PriceUnitAmount = 1,
            //        MasterPriceItemID = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.ID).FirstAsync(),
            //        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == PriceTypeKeys.Discount).Select(o => o.ID).FirstAsync(),
            //        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
            //    };

            //    await DB.UnitPriceItems.AddAsync(fgfModel);
            //}
            //else
            //{
            //    fgfDiscount.Amount = priceList.FGFDiscount;
            //    fgfDiscount.IsToBePay = false;
            //    fgfDiscount.PricePerUnitAmount = priceList.FGFDiscount;
            //    DB.Entry(fgfDiscount).State = EntityState.Modified;
            //}

            #endregion

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Booking Promotion
            var bookingPromotionModel = await DB.BookingPromotions.Where(o => o.BookingID == bookingId && o.IsActive == true).FirstAsync();
            bookingPromotionModel.TransferDateBefore = agreementPromotion.TransferDateBefore;
            DB.Entry(bookingPromotionModel).State = EntityState.Modified;

            #region Item
            var itemPromotion = agreementPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item && o.IsSelected == true).ToList();
            var itemPromotionModel = await DB.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in itemPromotion)
            {
                if (!itemPromotionModel.Any(o => o.ID == item.Id))
                {
                    var promotionItemModel = new BookingPromotionItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        PricePerUnit = item.PricePerUnit,
                        TotalPrice = item.Quantity * item.PricePerUnit,
                        MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingPromotionItemID = item.FromQuotationBookingPromotionItemID
                    };

                    await DB.BookingPromotionItems.AddAsync(promotionItemModel);

                    List<BookingPromotionItem> subItems = new List<BookingPromotionItem>();
                    foreach (var sub in item.SubItems)
                    {
                        var promotionSubItemModel = new BookingPromotionItem()
                        {
                            BookingPromotionID = bookingPromotionModel.ID,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.Quantity * item.PricePerUnit,
                            MainBookingPromotionItemID = promotionItemModel.ID,
                            MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID,
                            QuotationBookingPromotionItemID = item.FromQuotationBookingPromotionItemID
                        };

                        subItems.Add(promotionSubItemModel);
                    }

                    if (subItems.Count >= 0)
                    {
                        await DB.BookingPromotionItems.AddRangeAsync(subItems);
                    }
                }
            }

            foreach (var item in itemPromotionModel)
            {
                var existed = itemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Free
            var freeItemPromotion = agreementPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected == true).ToList();
            var freeItemPromotionModel = await DB.BookingPromotionFreeItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in freeItemPromotion)
            {
                if (!freeItemPromotionModel.Any(o => o.ID == item.Id))
                {
                    var freePromotionItemModel = new BookingPromotionFreeItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterBookingPromotionFreeItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingPromotionFreeItemID = item.FromQuotationBookingPromotionItemID
                    };

                    await DB.BookingPromotionFreeItems.AddAsync(freePromotionItemModel);
                }
            }

            foreach (var item in freeItemPromotionModel)
            {
                var existed = freeItemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Credit
            var creditItemPromotion = agreementPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected == true).ToList();
            var creditItemPromotionModel = await DB.BookingCreditCardItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in creditItemPromotion)
            {
                if (!creditItemPromotionModel.Any(o => o.ID == item.Id))
                {
                    var creditPromotionItemModel = new BookingCreditCardItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        MasterBookingCreditCardItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingCreditCardItemID = item.FromQuotationBookingPromotionItemID
                    };

                    await DB.BookingCreditCardItems.AddAsync(creditPromotionItemModel);
                }
            }

            foreach (var item in creditItemPromotionModel)
            {
                var existed = creditItemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #endregion

            #region Expense
            foreach (var expense in agreementExpenses)
            {
                var expenseModel = await DB.BookingPromotionExpenses.Where(o => o.BookingPromotionID == bookingPromotionModel.ID && o.MasterPriceItemID == expense.MasterPriceItem.Id).FirstAsync();
                expenseModel.SellerAmount = expense.SellerPayAmount;
                expenseModel.BuyerAmount = expense.BuyerPayAmount;
                expenseModel.ExpenseReponsibleByMasterCenterID = expense.ExpenseReponsibleBy.Id;
                DB.Entry(expenseModel).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
            #endregion

            if (isMinPriceWorkflow)
            {
                //await this.CreateMinPriceBudgetWorkflowAsync(bookingID, booking.MinPriceRequestReason?.Id, booking.OtherMinPriceRequestReason);
            }

            var result = await this.GetAgreementAsync(agreementID);
            return result;
        }

        public async void DeleteAgreementFileAsync(Guid agreementFileID, Guid? userID)
        {
            var item = await DB.AgreementFiles.Where(e => e.ID == agreementFileID).FirstOrDefaultAsync() ?? new AgreementFile();
            item.IsDeleted = true;
            item.Updated = DateTime.Now;
            item.UpdatedByUserID = userID;
            DB.AgreementFiles.Update(item);
            await DB.SaveChangesAsync();

        }
    }
}
