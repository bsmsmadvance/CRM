using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.SAL;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sale.Params.Outputs;
using static Base.DTOs.SAL.TransferDTO;

namespace Sale.Services.Service
{
    public class TransferService : ITransferService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public TransferService(DatabaseContext db, IConfiguration configuration)
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

        public async Task<TransferDTO> GetTransferAsync(Guid transferId)
        {
            var modelQuery = from o in DB.Transfers.Where(e => e.ID == transferId)
                             .Include(o => o.Project)
                             .Include(o => o.Unit)
                             .Include(o => o.Agreement)
                             .Include(o => o.TransferSale)
                             .Include(o => o.MeterCheque)

                             select (o);

            var modelTransfer = await modelQuery.FirstOrDefaultAsync() ?? new Transfer();

            var unitID = modelTransfer.UnitID ?? new Guid();

            var modelQueryUnit = from o in DB.Units.Where(o => o.ID == unitID)
                                .Include(o => o.Model)
                                .Include(o => o.Floor)
                                .Include(o => o.UnitDirection)
                                .Include(o => o.Tower)
                                .Include(o => o.UnitStatus)
                                .Include(o => o.UnitType)
                                .Include(o => o.UpdatedBy)
                                .Include(o => o.TitledeedDetails)

                                 select (o);

            var modelUnit = await modelQueryUnit.FirstOrDefaultAsync() ?? new Database.Models.PRJ.Unit();

            modelTransfer.Unit = modelUnit;

            var result = TransferDTO.CreateFromModel(modelTransfer);

            return result;
        }

        public async Task<TransferDTO> GetTransferDrafAsync(Guid agreementId)
        {
            var model = DB.Agreements.Where(x => x.ID == agreementId)
                             .Include(o => o.Booking)
                                .ThenInclude(o => o.SaleUser)
                             .Include(o => o.Project)
                             .Include(o => o.Unit)
                             .FirstOrDefault();

            if (model != null)
            {

                var unitPriceModel = await DB.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == model.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();
                                
                decimal NetSellingPrice = 0;

                if (unitPriceModel != null)
                {
                    var unitPriceItemModel = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                    NetSellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                }

                string str = "exec [SAL].[sp_TSF_CALC_TRANSFERFEE] @AgreementID, @TransferDate, @SalePrice;";

                DB.Database.ExecuteSqlCommand(str,
                    new {
                        AgreementID = model.ID,
                        TransferDate = model.TransferOwnershipDate,
                        SalePrice = NetSellingPrice
                    });

                DB.SaveChanges();
            }

            var result = await TransferDTO.CreateFromAgreementModelAsync(model, DB);

            return result;
        }

        public async Task<TransferPriceListDTO> GetTransferPriceAsync(Guid transferId)
        {
            var modelQuery = from o in DB.Transfers.Where(e => e.ID == transferId)
                            .Include(o => o.Agreement)
                                .ThenInclude(o => o.Booking)

                             select (o.Agreement.Booking);

            var model = modelQuery.FirstOrDefault() ?? new Booking();
            var BookingID = model.ID;

            var result = await await modelQuery.Select(o => TransferPriceListDTO.CreateFromModelAsync(BookingID, DB)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<TransferPriceListDTO> GetTransferPriceDrafAsync(Guid agreementId)
        {
            var modelQuery = from o in DB.Agreements.Where(e => e.ID == agreementId)
                             .Include(o => o.Booking)
                             select (o.Booking);

            var model = modelQuery.FirstOrDefault() ?? new Booking();
            var BookingID = model.ID;

            var result = await await modelQuery.Select(o => TransferPriceListDTO.CreateFromModelAsync(BookingID, DB)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<TransferExpenseListDTO>> GetTransferFeeAsync(Guid transferId)
        {
            var result = await TransferExpenseListDTO.CreateFromModelAsync(transferId, DB);

            return result;
        }

        public async Task<List<TransferExpenseListDTO>> GetTransferFeeDrafAsync(Guid agreementId)
        {
            var result = await TransferExpenseListDTO.CreateFromDrafModelAsync(agreementId, DB);

            return result;
        }

        public async Task<TransferDTO> GetTransferMoneyAsync(Guid transferId)
        {
            var modelQuery = from o in DB.Transfers.Where(e => e.ID == transferId)

                             select (o);

            var result = await modelQuery.Select(o => TransferDTO.CreateFromModel(o)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<TransferValidate> ValidateCreateTransferAsync(Guid agreementId)
        {

            #region Get Data
            var model = await DB.Agreements
                .Where(o => o.ID == agreementId).FirstOrDefaultAsync();
            model = model ?? new Agreement();
            #endregion

            #region Validate

            //select*
            //from SAL.Booking a
            //Inner join sal.Agreement b on a.ID = b.bookingid
            //Inner join prj.TitledeedDetail c on c.UnitID = a.UnitID and Isnull(c.TitledeedNo,'')<> ''
            //Where a.CreditBankingTypeMasterCenterID is not null-- ตรวจสอบว่าบันทึกขอสินเชื่อหรือยัง
            //and b.ID not in(select AgreementID from sal.Transfer t where Isnull(t.IsDeleted,0)= 0)--ตรวจสอบว่ายังไม่ตั้งเรื่องโอน

            //--ถ้า check api => qc and defect ไม่มี ให้มาเช็คที่ WaiveQC ที่
            //SELECT* from prj.WaiveQC

            var modelQueryTitledeedNo = from o in DB.Agreements.Where(e => e.ID == agreementId)
                                .Include(o => o.Booking)
                                        join up in DB.TitledeedDetails
                                        on o.Booking.UnitID equals up.UnitID into upData
                                        from upModel in upData.DefaultIfEmpty()
                                        select (upModel);

            var modelQueryCreditBanking = from o in DB.Agreements.Where(e => e.ID == agreementId)
                                .Include(o => o.Booking)
                                          select (o.Booking);

            var modelQueryTransfer = await DB.Transfers.Where(e =>
                                            e.AgreementID == agreementId && e.IsDeleted == false
                                        ).FirstOrDefaultAsync();

            var modelQueryWaiveQC = await DB.WaiveQCs.Where(e =>
                                            e.UnitID == model.UnitID
                                        ).FirstOrDefaultAsync();

            var modelTitledeedNo = modelQueryTitledeedNo.FirstOrDefault() ?? new Database.Models.PRJ.TitledeedDetail();
            var modelCreditBanking = modelQueryCreditBanking.FirstOrDefault() ?? new Booking();
            var modelTransfer = modelQueryTransfer ?? new Transfer();
            var modelWaiveQC = modelQueryWaiveQC ?? new Database.Models.PRJ.WaiveQC();

            var IsTitledeedNo = false;
            var IsCreditBanking = false;
            var IsNotTransfer = false;
            var IsWaiveQC = false;
            var IsWaiveSign = false;

            if (!string.IsNullOrEmpty(modelTitledeedNo.TitledeedNo))
            {
                IsTitledeedNo = true;
            }
            if (modelCreditBanking.CreditBankingTypeMasterCenterID.HasValue)
            {
                IsCreditBanking = true;
            }
            if (string.IsNullOrEmpty(modelTransfer.TransferNo))
            {
                IsNotTransfer = true;
            }

            /* ----------------------------------------------------------- */

            #region Get Data For API
            var modelPrj = await DB.Projects
                .Where(o => o.ID == model.ProjectID).FirstOrDefaultAsync();
            modelPrj = modelPrj ?? new Database.Models.PRJ.Project();

            var modelUnit = await DB.Units
                .Where(o => o.ID == model.UnitID).FirstOrDefaultAsync();
            modelUnit = modelUnit ?? new Database.Models.PRJ.Unit();
            #endregion

            #region API GetUnitEndProductDate
            var IsEndProductDate = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://58.137.32.228");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiParam = new api_GetUnitEndProductDate_Param();

                apiParam.ProjectId = modelPrj.ProjectNo;
                apiParam.UnitNumber = modelUnit.UnitNo;

                /*--- Fix Test ---*/
                //apiParam.ProjectId = "10087";
                //apiParam.UnitNumber = "16NB10";

                var contentJson = JsonConvert.SerializeObject(apiParam);
                var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
                var response = client.PostAsync("CRM_QISAPI/api/Services/GetUnitEndProductDate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    var modelObject = JsonConvert.DeserializeObject<api_GetUnitEndProductDate_Response>(responseString);

                    modelObject = modelObject ?? new api_GetUnitEndProductDate_Response();

                    var itemResultList = modelObject.data ?? new List<EndProductUnit>();
                    var itemResultDt = itemResultList.FirstOrDefault() ?? new EndProductUnit();

                    IsEndProductDate = itemResultDt.NProductDate.HasValue ? true : false;
                }
            }

            #endregion

            #region API GetUnitReceiveDate
            var IsDocReceiveUnitDate = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://58.137.32.228");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiParam = new api_GetUnitReceiveDate_Param();

                apiParam.ProjectId = modelPrj.ProjectNo;
                apiParam.UnitNumber = modelUnit.UnitNo;

                /*--- Fix Test ---*/
                //apiParam.ProjectId = "10059";
                //apiParam.UnitNumber = "05B20";

                var contentJson = JsonConvert.SerializeObject(apiParam);
                var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
                var response = client.PostAsync("restdblink_defect/api/Services/GetDocReceiveUnitDate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    var modelObject = JsonConvert.DeserializeObject<api_GetUnitReceiveDate_Response>(responseString);

                    modelObject = modelObject ?? new api_GetUnitReceiveDate_Response();

                    var itemResultList = modelObject.data ?? new List<DefectReceiveUnit>();
                    var itemResultDt = itemResultList.FirstOrDefault() ?? new DefectReceiveUnit();

                    IsDocReceiveUnitDate = itemResultDt.DocReceiveUnitDate.HasValue ? true : false;
                }
            }

            #endregion

            if (IsEndProductDate)
            {
                IsWaiveQC = true;
            }
            if (IsDocReceiveUnitDate)
            {
                IsWaiveSign = true;
            }

            /* ----------------------------------------------------------- */

            if (modelWaiveQC.WaiveQCDate.HasValue && IsWaiveQC == false)
            {
                IsWaiveQC = true;
            }
            if (modelWaiveQC.WaiveSignDate.HasValue && IsWaiveSign == false)
            {
                IsWaiveSign = true;
            }

            var result = new TransferValidate();

            result.IsTitledeedNo = IsTitledeedNo;
            result.IsCreditBanking = IsCreditBanking;
            result.IsWaiveQC = IsWaiveQC;
            result.IsWaiveSign = IsWaiveSign;
            result.IsNotTransfer = IsNotTransfer;

            return result;

            #endregion

        }

        public async Task<AgreementOwnerDTO> GetTransferOwnerDrafAsync(Guid agreementId)
        {
            var modelQuery = from o in DB.AgreementOwners.Where(e => e.AgreementID == agreementId)
                             .Include(o => o.Agreement)
                             .Include(o => o.ContactType)
                             .Include(o => o.ContactTitleTH)
                             .Include(o => o.ContactTitleEN)
                             .Include(o => o.National)
                             .Include(o => o.Gender)

                             select o;

            var model = await await modelQuery.Select(o => AgreementOwnerDTO.CreateFromModelDraftAsync(o, DB)).FirstOrDefaultAsync();

            var result = model ?? new AgreementOwnerDTO();

            return result;
        }

        public async Task<TransferOwnerDTO> GetTransferOwnerAsync(Guid transferOwnerId)
        {
            var modelQuery = from o in DB.TransferOwners.Where(e => e.ID == transferOwnerId)
                             .Include(o => o.Transfer)
                             .Include(o => o.ContactType)
                             .Include(o => o.ContactTitleTH)
                             .Include(o => o.National)
                             .Include(o => o.MarriageNational)
                             .Include(o => o.Country)
                             .Include(o => o.Province)
                             .Include(o => o.District)
                             .Include(o => o.SubDistrict)

                             select o;

            var model = await await modelQuery.Select(o => TransferOwnerDTO.CreateFromModelAsync(o, DB)).FirstOrDefaultAsync();

            var result = model ?? new TransferOwnerDTO();

            return result;
        }

        public async Task<TransferOwnerDTO> UpdateTransferOwnerAsync(Guid transferOwnerId, TransferOwnerDTO transferOwner)
        {
            var transfer = new TransferDTO();

            #region Get Data
            var model = await DB.TransferOwners
                .Where(o => o.ID == transferOwnerId).FirstOrDefaultAsync();
            model = model ?? new TransferOwner();
            #endregion

            #region Set Model
            transferOwner.ToModel(ref model);
            #endregion

            #region Save
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            #endregion

            #region Return
            var result = await this.GetTransferOwnerAsync(transferOwnerId) ?? new TransferOwnerDTO();
            return result;
            #endregion

        }

        public async Task<List<TransferOwnerDTO>> GetTransferOwnerListAsync(Guid transferId)
        {
            var modelQuery = from o in DB.TransferOwners.Where(e => e.TransferID == transferId)
                             .Include(o => o.Transfer)
                             .Include(o => o.ContactType)
                             .Include(o => o.ContactTitleTH)
                             .Include(o => o.National)
                             .Include(o => o.MarriageNational)
                             .Include(o => o.Country)
                             .Include(o => o.Province)
                             .Include(o => o.District)
                             .Include(o => o.SubDistrict)

                             select o;

            var model = await modelQuery.ToListAsync() ?? new List<TransferOwner>(); ;

            var modelList = new List<TransferOwnerDTO>();

            foreach (var a in model)
            {
                var item = await model.Select(o => TransferOwnerDTO.CreateFromModelAsync(o, DB)).FirstOrDefault();

                modelList.Add(item);
            }

            var result = modelList ?? new List<TransferOwnerDTO>();

            return result;
        }

        public async Task<List<TransferOwnerDTO>> GetTransferOwnerDrafListAsync(Guid agreementId)
        {
            var modelQuery = from o in DB.AgreementOwners.Where(e => e.AgreementID == agreementId)
                             .Include(o => o.Agreement)
                             .Include(o => o.ContactType)
                             .Include(o => o.ContactTitleTH)
                             .Include(o => o.National)
                             .Include(o => o.MarriageNational)
                                 //.Include(o => o.Country)
                                 //.Include(o => o.Province)
                                 //.Include(o => o.District)
                                 //.Include(o => o.SubDistrict)

                             select o;

            var model = await modelQuery.ToListAsync() ?? new List<AgreementOwner>(); ;

            var modelList = new List<TransferOwnerDTO>();

            foreach (var a in model)
            {
                var item = await model.Select(o => TransferOwnerDTO.CreateFromAgreementOwnerModelAsync(o, DB)).FirstOrDefault();

                modelList.Add(item);
            }

            var result = modelList ?? new List<TransferOwnerDTO>();

            return result;
        }

        public async Task<ContactAddressDTO> CopyContactAddressAsync(Guid contactId)
        {
            var model = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Where(e => e.ContactID == contactId && e.ContactAddressType.Key == "1").FirstOrDefaultAsync();

            return await ContactAddressDTO.CreateFromModelAsync(model, DB);
        }

        public async Task<ProjectAddressDTO> CopyProjectAddressAsync(Guid projectId)
        {
            var model = await DB.Addresses
                    .Include(o => o.ProjectAddressType)
                    .Where(e => e.ProjectID == projectId && e.ProjectAddressType.Key == "1").FirstOrDefaultAsync();

            return ProjectAddressDTO.CreateFromModel(model);
        }

        public async Task<TransferDTO> CreateTransferDataAsync(TransferDTO input)
        {
            await input.ValidateAsync(DB);

            Transfer model = new Transfer();
            input.ToModel(ref model);

            #region TransferNo
            if (model.TransferNo == null)
            {

                var modelProject = await DB.Projects
                        .Where(e => e.ID == model.ProjectID).FirstOrDefaultAsync();
                modelProject = modelProject ?? new Database.Models.PRJ.Project();

                string year = Convert.ToString(DateTime.Today.Year);
                string month = DateTime.Today.ToString("MM");
                var key = "TF" + modelProject.ProjectNo + year[2] + year[3] + month;
                var type = "SAL.Transfer";
                var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
                if (runningno == null)
                {
                    var runningNumberCounter = new RunningNumberCounter
                    {
                        Key = key,
                        Type = type,
                        Count = 1
                    };
                    await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                    await DB.SaveChangesAsync();

                    model.TransferNo = key + runningNumberCounter.Count.ToString("0000") + "00";
                    runningNumberCounter.Count++;
                    DB.Entry(runningNumberCounter).State = EntityState.Modified;
                }
                else
                {
                    model.TransferNo = key + runningno.Count.ToString("0000") + "00";
                    runningno.Count++;
                    DB.Entry(runningno).State = EntityState.Modified;
                }

            }
            #endregion

            await DB.Transfers.AddAsync(model);
            await DB.SaveChangesAsync();

            var result = TransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<TransferDTO> UpdateTransferDataAsync(Guid transferId, TransferDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.Transfers.Where(o => o.ID == transferId).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = TransferDTO.CreateFromModel(model);
            return result;
        }

        public async Task<Transfer> DeleteTransferAsync(Guid transferId)
        {
            var model = await DB.Transfers.Where(o => o.ID == transferId).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<List<Base.DTOs.FIN.TransferPaymentDTO>> GetPaymentDetailAsync(Guid transferId)
        {

            var modelQuery = from o in DB.Transfers.Where(e => e.ID == transferId)
                            .Include(o => o.Agreement)
                                .ThenInclude(o => o.Booking)

                             select (o.Agreement.Booking);

            var model = modelQuery.FirstOrDefault() ?? new Booking();
            var bookingID = model.ID;

            var result = new List<Base.DTOs.FIN.TransferPaymentDTO>();

            IQueryable<Base.DTOs.FIN.TransferPaymentDTO> queryCashier = from o in DB.PaymentItems
                                                                             .Include(x => x.Payment)
                                                                             .ThenInclude(x => x.Booking)
                                                                             .Include(x => x.UnitPriceInstallment)
                                                                             .Include(x => x.UnitPriceItem)

                                                                        join Receipt in DB.ReceiptTempHeaders on o.Payment.ID equals Receipt.PaymentID into ReceiptGroup
                                                                        from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                                        select new Base.DTOs.FIN.TransferPaymentDTO
                                                                        {
                                                                            BookingID = o.Payment.BookingID,
                                                                            PaymentBy = o.UnitPriceItem.Name + (o.UnitPriceInstallment != null ? "ที่ " + o.UnitPriceInstallment.Period.ToString() : ""),
                                                                            ReceiptTempNo = ReceiptModel.ReceiptTempNo,
                                                                            Amount = o.PayAmount,
                                                                            PayAmount = o.UnitPriceItem.MasterPriceItemID == new Guid("CA4416B8-3D0D-4250-9323-C1FED9401462") ? o.UnitPriceInstallment.Amount : o.UnitPriceItem.Amount
                                                                        };

            #region filter
            var query = queryCashier.Where(x => x.BookingID == bookingID);
            #endregion

            result = await queryCashier.ToListAsync();

            return result;
        }

        public async Task<List<Base.DTOs.FIN.TransferPaymentDTO>> GetReceiptDetailAsync(Guid transferId)
        {

            var modelQuery = from o in DB.Transfers.Where(e => e.ID == transferId)
                            .Include(o => o.Agreement)
                                .ThenInclude(o => o.Booking)

                             select (o.Agreement.Booking);

            var model = modelQuery.FirstOrDefault() ?? new Booking();
            var bookingID = model.ID;

            var result = new List<Base.DTOs.FIN.TransferPaymentDTO>();

            IQueryable<Base.DTOs.FIN.TransferPaymentDTO> queryCashier = from o in DB.PaymentCashierCheques
                                                                           .Include(x => x.PaymentMethod)
                                                                           //.ThenInclude(x=>x.PaymentMethodType)
                                                                           .ThenInclude(x => x.Payment)
                                                                           .ThenInclude(x => x.Booking)
                                                                           .Include(x => x.Bank)

                                                                        join Receipt in DB.ReceiptTempHeaders on o.PaymentMethod.PaymentID equals Receipt.PaymentID into ReceiptGroup
                                                                        from ReceiptModel in ReceiptGroup.DefaultIfEmpty()

                                                                        join Deposit in DB.DepositDetails.Include(x => x.DepositHeader) on o.PaymentMethodID equals Deposit.PaymentMethodID into DepositGroup
                                                                        from DepositModel in DepositGroup.DefaultIfEmpty()

                                                                        select new Base.DTOs.FIN.TransferPaymentDTO
                                                                        {
                                                                            BookingID = o.PaymentMethod.Payment.BookingID,
                                                                            ReceiptTempNo = ReceiptModel.ReceiptTempNo,
                                                                            PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(o.PaymentMethod.PaymentMethodType),
                                                                            ReceiveDate = o.PaymentMethod.Payment.ReceiveDate,
                                                                            ChequeNo = o.ChequeNo,
                                                                            ChequeDate = o.ChequeDate,
                                                                            Bank = BankDTO.CreateFromModel(o.Bank),
                                                                            Amount = o.PaymentMethod.Payment.TotalAmount,
                                                                            DepositStatus = DepositModel.DepositHeader.DepositNo != "" ? "นำฝากแล้ว" : "ยังไมได้นำฝาก",
                                                                            DepositNo = DepositModel.DepositHeader.DepositNo
                                                                        };

            #region filter
            var query = queryCashier.Where(x => x.BookingID == bookingID);
            #endregion

            result = await queryCashier.ToListAsync();

            return result;
        }
    }
}
