using Base.DTOs.FIN;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Services.IService;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using PagingExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.FIN.FETDTO;
using Base.DTOs;
using FileStorage;
using Base.DTOs.PRJ;
using Database.Models.SAL;
using Database.Models.PRJ;
using Microsoft.Extensions.Configuration;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore.Internal;

namespace Finance.Services.Service
{
    public class FETService : IFETService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public FETService(DatabaseContext db, IConfiguration configuration)
        {
            DB = db;
            this.Configuration = configuration;
            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        #region Dropdown

        public async Task<List<ProjectDropdownDTO>> GetProjectDropdownListForFETAsync(string displayName, Guid? projectID)
        {
            IQueryable<Project> query = DB.Projects;

            if ((projectID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(o => o.ID == projectID);

            if ((displayName ?? "") != "")
                query = query.Where(o => o.ProjectNameTH.Contains(displayName) || o.ProjectNo.Contains(displayName));

            var results = await query.Select(o => ProjectDropdownDTO.CreateFromModel(o)).ToListAsync();

            return results;
        }

        public async Task<List<FETUnitDropdownDTO>> GetUnitDropdowListForFETAsync(string displayName, Guid? projectID, Guid? unitID)
        {
            var query = from o in DB.Bookings
                .Include(o => o.Unit)
                .Include(o => o.Project)

                        join ag in DB.Agreements on o.ID equals ag.BookingID into agData
                        from agModel in agData.DefaultIfEmpty()

                        select new FETUnitQueryResult
                        {
                            Unit = o.Unit,
                            Booking = o,
                            Project = o.Project,
                            Agreement = agModel ?? new Agreement()
                        };

            query = query.Where(o => (o.Booking.IsPaid ?? false) == true);

            if ((projectID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(o => o.Booking.ProjectID == projectID);

            if ((unitID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(o => o.Booking.UnitID == unitID);

            if ((displayName ?? "") != "")
                query = query.Where(o => o.Unit.UnitNo.Contains(displayName));

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => FETUnitDropdownDTO.CreateFromQueryResult(o, DB)).ToList();

            return results;
        }

        #endregion Dropdown


        #region GetList

        public async Task<FETPaging> GetFETListAsync(FETFilter filter, PageParam pageParam, FETSortByParam sortByParam)
        {
            var query = from o in DB.FETs.Where(o => (o.IsReject ?? false) == false)
                                    .Include(o => o.Booking)
                                        .ThenInclude(o => o.Unit)
                                            .ThenInclude(o => o.Project)
                                    .Include(o => o.Bank)
                                    .Include(o => o.UpdatedBy)
                                    .Include(o => o.CreatedBy)

                        join mcR in DB.MasterCenters on o.FETRequesterMasterCenterID equals mcR.ID into mcRData
                        from mcRModel in mcRData.DefaultIfEmpty()

                        join mcS in DB.MasterCenters on o.FETStatusMasterCenterID equals mcS.ID into mcSData
                        from mcSModel in mcSData.DefaultIfEmpty()

                        join pm in DB.PaymentMethods on o.ReferentGUID equals pm.ID into pmData
                        from pmModel in pmData.DefaultIfEmpty()

                        join pmt in DB.MasterCenters on pmModel.PaymentMethodTypeMasterCenterID equals pmt.ID into pmtData
                        from pmtModel in pmtData.DefaultIfEmpty()

                        join p in DB.Payments.Include(e => e.CreatedBy) on pmModel.PaymentID equals p.ID into pData
                        from pModel in pData.DefaultIfEmpty()

                        join pcc in DB.PaymentCreditCards on pmModel.ID equals pcc.PaymentMethodID into pccData
                        from pccModel in pccData.DefaultIfEmpty()

                        join pfbt in DB.PaymentForeignBankTransfers on pmModel.ID equals pfbt.PaymentMethodID into pfbtData
                        from pfbtModel in pfbtData.DefaultIfEmpty()

                        join dd in DB.DepositDetails on pmModel.ID equals dd.PaymentMethodID into ddData
                        from ddModel in ddData.DefaultIfEmpty()

                        join dh in DB.DepositHeaders on ddModel.ID equals dh.ID into dhData
                        from dhModel in dhData.DefaultIfEmpty()

                        select new FETQueryResult
                        {
                            FET = o,
                            Booking = o.Booking,
                            Unit = o.Booking.Unit,
                            Project = o.Booking.Unit.Project,
                            Bank = o.Bank,

                            FETRequesterMasterCenter = mcRModel ?? new MasterCenter(),
                            FETStatusMasterCenter = mcSModel ?? new MasterCenter(),

                            Payment = pModel ?? new Payment(),
                            PaymentMethod = pmModel ?? new PaymentMethod(),
                            DepositHeader = dhModel ?? new DepositHeader(),
                            PaymentCreditCard = pccModel ?? new PaymentCreditCard(),
                            PaymentForeignBankTransfer = pfbtModel ?? new PaymentForeignBankTransfer(),
                            PaymentMethodTypeMasterCenter = pmtModel ?? new MasterCenter(),
                        };

            if ((filter.ProjectID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(x => x.Project.ID == filter.ProjectID);

            if ((filter.CompanyID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(x => x.Company.ID == filter.CompanyID);

            if ((filter.FETStatusMasterCenterID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(x => x.FET.FETStatusMasterCenterID == filter.FETStatusMasterCenterID);

            if ((filter.FETRequesterID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(x => x.FETRequesterMasterCenter.ID == filter.FETRequesterID);

            if ((filter.BankID ?? Guid.Empty) != Guid.Empty)
                query = query.Where(x => x.Bank.ID == filter.BankID);

            if (filter.DepositNO != null)
                query = query.Where(x => x.DepositHeader.DepositNo == filter.DepositNO);

            if (filter.PayStatus != null)
                query = query.Where(x => x.PaymentMethodTypeMasterCenter.ID == filter.PayStatus);

            if (filter.UnitID != null)
                query = query.Where(x => x.Booking.Unit.ID == filter.UnitID);

            if (filter.FETAmountFrom != null)
                query = query.Where(x => x.FET.Amount >= filter.FETAmountFrom);
            if (filter.FETAmountTo != null)
                query = query.Where(x => x.FET.Amount <= filter.FETAmountTo);

            if (filter.ReceiveDateFrom != null)
                query = query.Where(x => x.Payment.ReceiveDate >= filter.ReceiveDateFrom);
            if (filter.ReceiveDateTo != null)
                query = query.Where(x => x.Payment.ReceiveDate <= filter.ReceiveDateTo);

            if (filter.UpdatedFrom != null)
                query = query.Where(x => x.FET.Updated >= filter.UpdatedFrom);
            if (filter.UpdatedTo != null)
                query = query.Where(x => x.FET.Updated <= filter.UpdatedTo);

            List<string> CKPaymentMethodTypeMasterCenter = new List<string>();
            CKPaymentMethodTypeMasterCenter.Add("12");
            CKPaymentMethodTypeMasterCenter.Add("3");

            query = query.Where(x => CKPaymentMethodTypeMasterCenter.Any(x2 => x2 == x.PaymentMethodTypeMasterCenter.Key) || x.FET.ReferentGUID == null);

            SortBy(sortByParam, ref query);

            var pageOuput = PagingHelper.Paging(pageParam, ref query);

            var Data = await query.ToListAsync();

            var results = Data.Select(oi => CreateFromQuery(oi, DB)).ToList();

            return new FETPaging()
            {
                FETs = results,
                PageOutput = pageOuput
            };

        }

        public async Task<FETPaging> GetFETProjectListAsync(FETFilterViewProject filter, PageParam pageParam, FETSortProjectListByParam sortByParam)
        {
            var query = await DB.Projects.ToListAsync();

            var results = query.Select(o => CreateProjectListModel(o, DB)).ToList();

            if (filter.ProjectID != null)
                results = results.Where(o => o.Project.Id == filter.ProjectID).ToList();
            if (filter.countFET != null)
                results = results.Where(o => o.countFET == filter.countFET).ToList();
            if (filter.countUnit != null)
                results = results.Where(o => o.countUnit == filter.countUnit).ToList();
            if (filter.countAmountFrom != null)
                results = results.Where(o => o.SumAmountFET >= filter.countAmountFrom).ToList();
            if (filter.countAmountTo != null)
                results = results.Where(o => o.SumAmountFET <= filter.countAmountTo).ToList();

            var pageOuput = PagingHelper.PagingList(pageParam, ref results);
            SortProjectListBy(sortByParam, ref results);

            return new FETPaging()
            {
                FETs = results,
                PageOutput = pageOuput
            };
        }

        public async Task<FETPaging> GetFETUnitListAsync(FETFilterViewUnit filter, PageParam pageParam, FETSortUnitListByParam sortByParam)
        {
            IQueryable<FETQueryResultViewUnit> query = from o in DB.Bookings
                                                  .Include(o => o.Unit)
                                                      .ThenInclude(o => o.Project)

                                                       select new FETQueryResultViewUnit
                                                       {
                                                           Booking = o,
                                                           Unit = o.Unit,
                                                           Project = o.Unit.Project
                                                       };

            query = query.Where(o => o.Booking.IsDeleted == false);
            query = query.Where(o => o.Booking.IsPaid == true);

            //// TODO : kim ทำ group
            
            if (filter.ProjectID != null)
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            if (filter.UnitID != null)
                query = query.Where(o => o.Unit.ID == filter.UnitID);

            var model = await query.ToListAsync();

            var results = model.Select(o => CreateUnitListModel(o, DB)).ToList();

            if (filter.countUnit != null)
                results = results.Where(o => o.countUnit == filter.countUnit).ToList();

            if (filter.countAmountFrom != null)
                results = results.Where(o => o.SumAmountFET >= filter.countAmountFrom).ToList();
            if (filter.countAmountTo != null)
                results = results.Where(o => o.SumAmountFET <= filter.countAmountTo).ToList();

            if (filter.ContactName != null)
                results = results.Where(o => o.CustomerName.Contains(filter.ContactName)).ToList();

            results = results.GroupBy(o => new { o.Unit, o.CustomerName, o.countUnit, o.SumAmountFET }).Select(x => x.FirstOrDefault()).ToList();

            var pageOuput = PagingHelper.PagingList(pageParam, ref results);

            SortUnitListBy(sortByParam, ref results);

            return new FETPaging()
            {
                FETs = results,
                PageOutput = pageOuput
            };
        }

        #endregion GetList


        #region Update

        public async Task<FET> CreateFETAsync(FETDTO input)
        {
            await input.ValidateAsync(DB);

            FET model = new FET();

            model.FETRequesterMasterCenterID = input.FETRequesterMasterCenter.Id;

            //model.ReferentGUID =
            //model.ReferentType =
            //model.BankID =

            model.CustomerName = input.CustomerName;
            model.Amount = input.FETAmount;
            model.Remark = input.Remark;

            model.FETStatusMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETStatus, FETStatusKeys.Waiting);

            input.AttachFile = input.AttachFile ?? new FileDTO();

            if (input.AttachFile.Name != null)
            {
                var fileMove = UpdateAttachFileAsync(input.AttachFile);
                model.AttachFileName = fileMove.Result.Name;
                model.AttachFileUrl = fileMove.Result.Url;
            }

            model.BookingID = input.BookingID;

            model.ProjectID = input.Project.Id;

            await DB.FETs.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = await DB.FETs.Where(o => o.ID == model.ID).FirstAsync();
            return result;
        }

        public async Task<FET> UpdateFETAsync(FETDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.FETs.Where(o => o.ID == input.Id).FirstAsync();

            model.FETRequesterMasterCenterID = input.FETRequesterMasterCenter.Id;

            if (input.FETRequesterMasterCenter.Key != "1" || input.FETRequesterMasterCenter.Key != "5")
            {
                model.BookingID = input.BookingID;
                model.ProjectID = input.Project.Id;
                model.Amount = input.FETAmount;
            }

            model.CustomerName = input.CustomerName;

            input.AttachFile = input.AttachFile ?? new FileDTO();

            if (input.AttachFile.Name != null && input.AttachFile.Url != model.AttachFileUrl)
            {
                var fileMove = UpdateAttachFileAsync(input.AttachFile);
                model.AttachFileName = fileMove.Result.Name;
                model.AttachFileUrl = fileMove.Result.Url;
            }

            model.Remark = input.Remark;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            return model;
        }

        public async Task<FET> UpdateFETStatusAsync(Guid Id)
        {
            var model = await DB.FETs.Where(o => o.ID == Id).FirstAsync();

            var FETStatusWaitingID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETStatus, FETStatusKeys.Waiting);
            var FETStatusRequestedID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETStatus, FETStatusKeys.Requested);

            if (model.FETStatusMasterCenterID == FETStatusWaitingID)
                model.FETStatusMasterCenterID = FETStatusRequestedID;
            else
                model.FETStatusMasterCenterID = FETStatusWaitingID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<FET> RejectFETAsync(FETDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.FETs.Where(o => o.ID == input.Id).FirstAsync();

            model.IsReject = true;
            model.RejectRemark = input.RejectRemark;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<FET> DeleteFETAsync(FETDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.FETs.Where(o => o.ID == input.Id).FirstAsync();

            model.IsDeleted = true;
            model.CancelRemark = input.CancelRemark;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            return model;
        }


        #endregion Update


        public Task<string> DownloadCreditAdviceAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DownloadFETFormAsync(Guid id, Guid bankID)
        {
            throw new NotImplementedException();
        }

        public Task<string> ExportFETAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FileDTO> UpdateAttachFileAsync(FileDTO input)
        {
            DateTime UTCNow = DateTime.UtcNow;
            int year = UTCNow.Year;
            int month = UTCNow.Month;
            int day = UTCNow.Day;
            int hour = UTCNow.Hour;
            int min = UTCNow.Minute;
            int sec = UTCNow.Second;

            var path = year + month + day + hour + min + sec;

            if (input.IsTemp)
            {
                string pathName = $"fet/{path}/{input.Name}";
                await FileHelper.MoveTempFileAsync(input.Name, pathName);

                string url = await FileHelper.GetFileUrlAsync(pathName);

                var result = new FileDTO()
                {
                    Name = input.Name,
                    Url = url
                };

                return result;
            }
            else
            {
                return input;
            }
        }

    }
}
