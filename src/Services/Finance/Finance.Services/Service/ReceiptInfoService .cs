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
using static Base.DTOs.FIN.ReceiptInfoDTO;
using Base.DTOs;
using FileStorage;
using Base.DTOs.PRJ;
using Database.Models.SAL;
using Database.Models.PRJ;
using Microsoft.Extensions.Configuration;
using Database.Models.MasterKeys;
using Database.Models.ACC;

namespace Finance.Services.Service
{
    public class ReceiptInfoService : IReceiptInfoService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public ReceiptInfoService(DatabaseContext db, IConfiguration configuration)
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

        #region GetList

        public async Task<ReceiptInfoPaging> GetReceiptInfoListAsync(ReceiptInfoFilter filter, PageParam pageParam, ReceiptInfoSortByParam sortByParam)
        {
            var query = from o in DB.ReceiptTempDetails.IgnoreQueryFilters()
                                             .Include(o => o.ReceiptTempHeader)
                                                 .ThenInclude(o => o.Company)
                                             .Include(o => o.ReceiptTempHeader)
                                                 .ThenInclude(o => o.Project)
                                             .Include(o => o.ReceiptTempHeader)
                                                 .ThenInclude(o => o.Unit)
                                             .Include(o => o.PaymentItem)
                                            // .Where(x => x.IsCancel = false)
                        join pmti in DB.PaymentMethodToItems on o.PaymentItem.ID equals pmti.PaymentItemID into pmtiData
                        from pmtiModel in pmtiData.DefaultIfEmpty()

                        join pm in DB.PaymentMethods
                                    .Include(o => o.PaymentMethodType)
                            on pmtiModel.PaymentMethodID equals pm.ID into pmData
                        from pmModel in pmData.DefaultIfEmpty()

                        join dd in DB.DepositDetails
                                    .Include(o => o.DepositHeader)
                            on pmModel.ID equals dd.PaymentMethodID into ddData
                        from ddModel in ddData.DefaultIfEmpty()

                        join gl in DB.PostGLHeaders
                            on o.ID equals gl.ReferentID into glData
                        from glModel in glData.DefaultIfEmpty()

                        select new ReceiptInfoQueryResult
                        {
                            ReceiptTempDetail = o,
                            ReceiptTempHeader = o.ReceiptTempHeader,

                            Company = o.ReceiptTempHeader.Company,
                            Project = o.ReceiptTempHeader.Project,
                            Unit = o.ReceiptTempHeader.Unit,

                            PaymentItem = o.PaymentItem,
                            PaymentMethodToItem = pmtiModel ?? new PaymentMethodToItem(),
                            PaymentMethod = pmModel ?? new PaymentMethod(),
                            PaymentMethodType = (pmModel ?? new PaymentMethod()).PaymentMethodType ?? new MasterCenter(),

                            DepositHeader = (ddModel ?? new DepositDetail()).DepositHeader ?? new DepositHeader(),
                            PostGLHeader = glModel ?? new PostGLHeader()
                        };

            var Data = query.GroupBy(o => new { o.ReceiptTempHeader }).Select(o => new ReceiptInfoQueryResultList
            {

                ReceiptTempHeader = o.Key.ReceiptTempHeader,
                Company = o.Select(x => x.Company).FirstOrDefault(),
                Project = o.Select(x => x.Project).FirstOrDefault(),
                Unit = o.Select(x => x.Unit).FirstOrDefault(),

                ReceiptTempDetail = o.Select(x => x.ReceiptTempDetail).ToList(),
                PaymentItem = o.Select(x => x.PaymentItem).ToList(),
                PaymentMethodToItem = o.Select(x => x.PaymentMethodToItem).ToList(),
                PaymentMethod = o.Select(x => x.PaymentMethod).ToList(),
                PaymentMethodType = o.Select(x => x.PaymentMethodType).ToList(),
                DepositHeader = o.Select(x => x.DepositHeader).ToList(),
                PostGLHeader = o.Select(x => x.PostGLHeader).ToList()
            }).ToList();

            if (filter.CompanyID != null)
                Data = Data.Where(x => x.Company?.ID == filter.CompanyID).ToList();

            if (filter.ProjectID != null)
                Data = Data.Where(x => x.Project?.ID == filter.ProjectID).ToList();

            if (filter.UnitID != null)
                Data = Data.Where(x => x.Unit?.ID == filter.UnitID).ToList();

            if (filter.ReceiveDateFrom != null)
                Data = Data.Where(x => x.ReceiptTempHeader.ReceiveDate >= filter.ReceiveDateFrom).ToList();
            if (filter.ReceiveDateTo != null)
                Data = Data.Where(x => x.ReceiptTempHeader.ReceiveDate <= filter.ReceiveDateTo).ToList();

            if (!string.IsNullOrEmpty(filter.ReceiptTempNo))
                Data = Data.Where(x => x.ReceiptTempHeader.ReceiptTempNo.Contains(filter.ReceiptTempNo)).ToList();

            if ((filter.PaymentMethodTypeID ?? Guid.Empty) != Guid.Empty)
                Data = Data.Where(x => x.PaymentMethodType.Any(z => z.ID == filter.PaymentMethodTypeID)).Select(x => x).ToList();

            if (!string.IsNullOrEmpty(filter.ReceiptDescription))
                Data = Data.Where(x => x.ReceiptTempDetail.Any(z => (z.Description ?? "").Contains(filter.ReceiptDescription))).Select(x => x).ToList();

            if (filter.AmountFrom != null)
                Data = Data.Where(x => x.ReceiptTempDetail.Sum(z => z.Amount) >= filter.AmountFrom).Select(x => x).ToList();
            if (filter.AmountTo != null)
                Data = Data.Where(x => x.ReceiptTempDetail.Sum(z => z.Amount) <= filter.AmountTo).Select(x => x).ToList(); 

            if (!string.IsNullOrEmpty(filter.DepositNo))
                Data = Data.Where(x => x.DepositHeader.Any( z => (z.DepositNo ?? "").Contains(filter.DepositNo))).Select(x => x).ToList();

            if (!string.IsNullOrEmpty(filter.RVNumber))
                Data = Data.Where(x => x.PostGLHeader.Any(z => (z.DocumentNo ?? "").Contains(filter.RVNumber))).Select(x => x).ToList();

            if (filter.ReceiptStatus != null)
            {
                if (filter.ReceiptStatus == true)
                    Data = Data.Where(x => x.ReceiptTempHeader.IsDeleted == false).ToList();
                else if (filter.ReceiptStatus == false)
                    Data = Data.Where(x => x.ReceiptTempHeader.IsDeleted == true).ToList();
            }

            var pageOuput = PagingHelper.PagingList<ReceiptInfoQueryResultList>(pageParam, ref Data);

            var results = Data.Select(oi => CreateFromQueryList(oi, DB)).ToList();

            SortBy(sortByParam, ref results);

            if ((filter.BankAccountID ?? Guid.Empty) != Guid.Empty)
                results = results.Where(x => x.BankAccount?.Id == filter.BankAccountID).ToList();

            return new ReceiptInfoPaging()
            {
                ReceiptInfos = results,
                PageOutput = pageOuput
            };

        }

        #endregion GetList


        #region Update


        public async Task<ReceiptTempHeader> UpdateReceiptInfoAsync(ReceiptInfoDTO input)
        {
            //await input.ValidateAsync(DB);

            //var model = await DB.ReceiptInfos.Where(o => o.ID == input.Id).FirstAsync();

            //model.ReceiptInfoRequesterMasterCenterID = input.ReceiptInfoRequesterMasterCenter.Id;

            //if (input.ReceiptInfoRequesterMasterCenter.Key != "1" || input.ReceiptInfoRequesterMasterCenter.Key != "5")
            //{
            //    model.BookingID = input.BookingID;
            //    model.ProjectID = input.Project.Id;
            //    model.Amount = input.ReceiptInfoAmount;
            //}

            //model.CustomerName = input.CustomerName;

            //input.AttachFile = input.AttachFile ?? new FileDTO();

            //if (input.AttachFile.Name != null && input.AttachFile.Url != model.AttachFileUrl)
            //{
            //    var fileMove = UpdateAttachFileAsync(input.AttachFile);
            //    model.AttachFileName = fileMove.Result.Name;
            //    model.AttachFileUrl = fileMove.Result.Url;
            //}

            //model.Remark = input.Remark;

            //DB.Entry(model).State = EntityState.Modified;
            //await DB.SaveChangesAsync();

            //return model;

            return null;
        }


        public async Task<ReceiptTempHeader> DeleteReceiptInfoAsync(ReceiptInfoDTO input)
        {
            //await input.ValidateAsync(DB);
            //var model = await DB.ReceiptInfos.Where(o => o.ID == input.Id).FirstAsync();

            //model.IsDeleted = true;
            //model.CancelRemark = input.CancelRemark;

            //DB.Entry(model).State = EntityState.Modified;
            //await DB.SaveChangesAsync();
            //return model;
            return null;
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
                string pathName = $"ReceiptInfo/{path}/{input.Name}";
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

        #endregion Update



    }
}
