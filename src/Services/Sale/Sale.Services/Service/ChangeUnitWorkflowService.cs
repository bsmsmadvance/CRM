using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using FileStorage;
using Finance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sale.Params.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public class ChangeUnitWorkflowService : IChangeUnitWorkflowService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;
        private IQuotationService QuotationService;
        private IBookingService BookingService;
        private IPaymentService PaymentService;

        public ChangeUnitWorkflowService(IConfiguration configuration, DatabaseContext db, IQuotationService quotationService, IBookingService bookingService, IPaymentService paymentService)
        {
            this.DB = db;
            this.Configuration = configuration;
            this.QuotationService = quotationService;
            this.BookingService = bookingService;
            this.PaymentService = paymentService;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<BookingChangeUnitWorkflowDTO> CreateBookingChangeUnitWorkflowAsync(Guid fromBookingID,
            Guid toUnitID, QuotationPriceListDTO priceList,
            QuotationBookingPromotionDTO bookingPromotion,
            QuotationTransferPromotionDTO transferPromotion,
            List<QuotationPromotionExpenseDTO> expenses,
            MinPriceBudgetReasonInput minpriceReason = null
            , Guid? userID = null)
        {
            var oldBooking = await DB.Bookings.Where(o => o.ID == fromBookingID).FirstAsync();

            #region UpdateStatusNewUnit
            var unitStatusWatingConfirmBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus
                                                                                                && o.Key == UnitStatusKeys.WaitingForConfirmBooking)
                                                                                     .Select(o => o.ID)
                                                                                     .FirstAsync();
            var unitStatusWatingBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus
                                                                                        && o.Key == UnitStatusKeys.Available)
                                                                              .Select(o => o.ID)
                                                                              .FirstAsync();
            var toUnit = await DB.Units.Where(o => o.ID == toUnitID).FirstAsync();
            if (toUnit.UnitStatusMasterCenterID == unitStatusWatingBookingMasterCenterID || toUnit.UnitStatusMasterCenterID == null)
            {
                toUnit.UnitStatusMasterCenterID = unitStatusWatingConfirmBookingMasterCenterID;
                DB.Units.Update(toUnit);
            }
            #endregion

            var bookingStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus).ToListAsync();

            var newQuotation = await QuotationService.CreateQuotationAsync(toUnit.ID, priceList, bookingPromotion, transferPromotion, expenses);
            await QuotationService.ConvertToBookingAsync((Guid)newQuotation.Id);

            var newBooking = await DB.Bookings.Where(o => o.QuotationID == newQuotation.Id).FirstAsync();

            #region UpdateDataNewBooking
            oldBooking.BookingStatusMasterCenterID = bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.WaitingForApproveUnit).Select(o => o.ID).First();
            oldBooking.ChangeToBookingID = newBooking.ID;

            newBooking.SaleOfficerTypeMasterCenterID = oldBooking.SaleOfficerTypeMasterCenterID;
            newBooking.ProjectSaleUserID = oldBooking.ProjectSaleUserID;
            newBooking.SaleUserID = oldBooking.SaleUserID;
            newBooking.AgentID = oldBooking.AgentID;
            newBooking.AgentEmployeeID = oldBooking.AgentEmployeeID;
            newBooking.ChangeFromBookingID = oldBooking.ID;
            newBooking.IsFromChangeUnit = true;
            newBooking.ReferContactID = priceList.ReferContact?.Id;
            newBooking.TransferOwnershipDate = priceList.TransferOwnershipDate;

            DB.Bookings.Update(oldBooking);
            DB.Bookings.Update(newBooking);
            await DB.SaveChangesAsync();

            #region FGF
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == newBooking.ID && o.IsActive == true).FirstAsync();
            var lastOrder = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).OrderByDescending(o => o.Order).Select(o => o.Order).FirstAsync();
            var discount = await DB.UnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).FirstOrDefaultAsync();

            if (discount == null)
            {
                var fgfModel = new UnitPriceItem()
                {
                    UnitPriceID = unitPrice.ID,
                    Order = lastOrder + 1,
                    Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Detail).FirstAsync(),
                    Amount = priceList.FGFDiscount ?? 0,
                    IsToBePay = false,
                    PricePerUnitAmount = priceList.FGFDiscount,
                    PriceUnitAmount = 1,
                    MasterPriceItemID = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.ID).FirstAsync(),
                    PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == PriceTypeKeys.Discount).Select(o => o.ID).FirstAsync(),
                    PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                };

                await DB.UnitPriceItems.AddAsync(fgfModel);
            }
            else
            {
                discount.Amount = priceList.FGFDiscount ?? 0;
                discount.IsToBePay = false;
                discount.PricePerUnitAmount = priceList.FGFDiscount;
                DB.Entry(discount).State = EntityState.Modified;
            }
            #endregion


            await DB.SaveChangesAsync();

            var bookingOwners = await DB.BookingOwners.Where(o => o.BookingID == oldBooking.ID)
                                                         .Include(o => o.ContactTitleTH)
                                                         .Include(o => o.ContactTitleEN)
                                                         .ToListAsync();
            var bookingOwnerIds = bookingOwners.Select(o => o.ID).ToList();


            var newBookingOwners = new List<BookingOwner>();
            var newBookingOwnerAddreses = new List<BookingOwnerAddress>();
            var newBookingOwnerPhones = new List<BookingOwnerPhone>();
            var newBookingOwnerEmails = new List<BookingOwnerEmail>();
            foreach (var item in bookingOwners)
            {
                var bookingOwnerAddreses = await DB.BookingOwnerAddresses.Include(o => o.ContactAddressType)
                                                                .Include(o => o.Country)
                                                                .Include(o => o.Province)
                                                                .Include(o => o.District)
                                                                .Include(o => o.SubDistrict)
                                                                .Where(o => o.BookingOwnerID == item.ID)
                                                                .ToListAsync();
                var bookingOwnerPhones = await DB.BookingOwnerPhones.Where(o => o.BookingOwnerID == item.ID).ToListAsync();
                var bookingOwnerEmails = await DB.BookingOwnerEmails.Where(o => o.BookingOwnerID == item.ID).ToListAsync();

                var newBookingOwner = item;
                newBookingOwner.ID = Guid.NewGuid();
                newBookingOwner.BookingID = newBooking.ID;
                newBookingOwners.Add(newBookingOwner);

                foreach (var item1 in bookingOwnerAddreses)
                {
                    var newBookingOwnerAddress = item1;
                    newBookingOwnerAddress.ID = Guid.NewGuid();
                    newBookingOwnerAddress.BookingOwnerID = newBookingOwner.ID;
                    newBookingOwnerAddreses.Add(newBookingOwnerAddress);
                }
                foreach (var item1 in bookingOwnerPhones)
                {
                    var newBookingOwnerPhone = item1;
                    newBookingOwnerPhone.ID = Guid.NewGuid();
                    newBookingOwnerPhone.BookingOwnerID = newBookingOwner.ID;
                    newBookingOwnerPhones.Add(newBookingOwnerPhone);
                }
                foreach (var item1 in bookingOwnerEmails)
                {
                    var newBookingOwnerEmail = item1;
                    newBookingOwnerEmail.ID = Guid.NewGuid();
                    newBookingOwnerEmail.BookingOwnerID = newBookingOwner.ID;
                    newBookingOwnerEmails.Add(newBookingOwnerEmail);
                }
            }

            await DB.BookingOwners.AddRangeAsync(newBookingOwners);
            await DB.BookingOwnerAddresses.AddRangeAsync(newBookingOwnerAddreses);
            await DB.BookingOwnerPhones.AddRangeAsync(newBookingOwnerPhones);
            await DB.BookingOwnerEmails.AddRangeAsync(newBookingOwnerEmails);
            await DB.SaveChangesAsync();
            #endregion


            var newBookingDTO = await BookingService.GetBookingAsync(newBooking.ID);
            var newBookingPriceListDTO = await BookingService.GetPriceListAsync(newBooking.ID);
            var newBookingPromotionDTO = await BookingService.GetBookingPromotionAsync(newBooking.ID);
            var newBookingPromotionExpenseDTOs = await BookingService.GetPromotionExpensesAsync(newBooking.ID);

            await BookingService.UpdateBookingAsync(newBooking.ID, newBookingDTO, newBookingPriceListDTO, newBookingPromotionDTO, newBookingPromotionExpenseDTOs, false, false);

            var roleLCM = await DB.Roles.Where(o => o.Code == "LCM").FirstAsync();
            var model = new ChangeUnitWorkflow();
            model.FromBookingID = fromBookingID;
            model.ToBookingID = newBooking.ID;
            model.RequestApproverRoleID = roleLCM.ID;

            if (minpriceReason != null)
            {
                model.MinPriceRequestReasonMasterCenterID = minpriceReason.MinPriceRequestReason?.Id;
                model.OtherMinPriceRequestReason = minpriceReason.OtherMinPriceRequestReason;
            }

            await DB.ChangeUnitWorkflows.AddAsync(model);
            await DB.SaveChangesAsync();

            #region UpdateNewBookingStatus
            var checkPriceListWf = await DB.PriceListWorkflows.Where(o => o.BookingID == newBooking.ID).FirstOrDefaultAsync();
            if (checkPriceListWf != null)
            {
                newBooking.BookingStatusMasterCenterID = bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.WaitingForApprovePriceList)
                                                                                   .Select(o => o.ID)
                                                                                   .First();
            }
            else
            {
                newBooking.BookingStatusMasterCenterID = bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.WaitingForApproveUnit)
                                                                                   .Select(o => o.ID)
                                                                                   .First();
            }

            DB.Bookings.Update(newBooking);
            await DB.SaveChangesAsync();
            #endregion



            var result = await this.GetBookingChangeUnitWorkflowAsync(oldBooking.ID, userID);
            return result;
        }

        public async Task<BookingChangeUnitWorkflowDTO> GetBookingChangeUnitWorkflowAsync(Guid bookingID, Guid? userID = null)
        {
            var booking = await DB.ChangeUnitWorkflows
                                    .Where(o => o.FromBookingID == bookingID)
                                    .Include(o => o.FromBooking)
                                        .ThenInclude(o => o.Quotation)
                                    .Include(o => o.FromBooking)
                                        .ThenInclude(o => o.Project)
                                            .ThenInclude(o => o.ProductType)
                                    .Include(o => o.ToBooking)
                                        .ThenInclude(o => o.Unit)
                                            .ThenInclude(o => o.UnitStatus)
                                    .Include(o => o.ToBooking)
                                        .ThenInclude(o => o.Quotation)
                                            .ThenInclude(o => o.Unit)
                                                .ThenInclude(o => o.UnitStatus)
                                    .FirstOrDefaultAsync();

            var result = await BookingChangeUnitWorkflowDTO.CreateFromModelAsync(booking, DB, userID);
            return result;
        }

        public async Task BookingRequestApproveAsync(Guid changeUnitWorkflowID, Guid? userID = null)
        {
            var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ID == changeUnitWorkflowID).FirstAsync();

            #region newBooking && newUnit
            var newBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.ToBookingID)
                                  .FirstAsync();

            var newUnit = await DB.Units.Where(o => o.ID == newBooking.UnitID)
                                     .FirstAsync();
            #endregion

            #region OldBooking && oldUnit
            var oldBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.FromBookingID).FirstAsync();
            var oldUnit = await DB.Units.Where(o => o.ID == oldBooking.UnitID).FirstAsync();
            #endregion

            #region UnitStatus && bookingStauts
            var bookingStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus).ToListAsync();
            var unitStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus).ToListAsync();
            #endregion

            var newBookingDTO = await BookingService.GetBookingAsync(newBooking.ID);
            if (changeUnitWorkflow.MinPriceRequestReasonMasterCenterID != null)
            {
                newBookingDTO.MinPriceRequestReason = new MasterCenterDropdownDTO();
                newBookingDTO.MinPriceRequestReason.Id = (Guid)changeUnitWorkflow.MinPriceRequestReasonMasterCenterID;
                newBookingDTO.OtherMinPriceRequestReason = changeUnitWorkflow.OtherMinPriceRequestReason;
            }

            var newBookingPriceListDTO = await BookingService.GetPriceListAsync(newBooking.ID);
            var newBookingPromotionDTO = await BookingService.GetBookingPromotionAsync(newBooking.ID);
            var newBookingPromotionExpenseDTOs = await BookingService.GetPromotionExpensesAsync(newBooking.ID);

            await BookingService.UpdateBookingAsync(newBooking.ID, newBookingDTO, newBookingPriceListDTO, newBookingPromotionDTO, newBookingPromotionExpenseDTOs, true, true);

            var minPricewf = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == newBooking.ID && o.IsApproved == null).FirstOrDefaultAsync();
            if (minPricewf == null)
            {
                var oldPayments = await DB.Payments.Where(o => o.BookingID == oldBooking.ID)
                                   .ToListAsync();
                var bookingStatusBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                                    && o.Key == BookingStatusKeys.Booking)
                                                                           .Select(o => o.ID).FirstAsync();
                if (oldPayments.Count() > 0)
                {
                    #region Create Payments

                    newBooking.BookingStatusMasterCenterID = bookingStatusBookingMasterCenterID;
                    DB.Bookings.Update(newBooking);
                    await DB.SaveChangesAsync();

                    var oldPaymentIds = oldPayments.Select(o => o.ID).ToList();
                    var oldPaymentsMethod = await DB.PaymentMethods.Where(o => oldPaymentIds.Contains(o.PaymentID)).ToListAsync();

                    var sumOldPayments = oldPayments.Sum(o => o.TotalAmount);

                    var paymentMethodTypeChangeUnit = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                    && o.Key == PaymentMethodKeys.ChangeContract)
                                                                            .FirstAsync();

                    var paymentNewBooking = await PaymentService.GetPaymentFormAsync(newBooking.ID);

                    paymentNewBooking.ReceiveDate = DateTime.Now;
                    paymentNewBooking.PaymentFormType = PaymentFormType.ChangeUnit;
                    paymentNewBooking.RefID = oldBooking.ID;
                    paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                {
                    new PaymentMethodDTO
                            {
                                PayAmount= sumOldPayments,
                                PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodTypeChangeUnit),
                            },
                };
                    await PaymentService.SubmitPaymentFormAsync(newBooking.ID, paymentNewBooking);

                    #endregion

                    //Cancel OldBooking
                    var cancelMemo = await BookingService.GetCancelMemoFormAsync(oldBooking.ID);
                    await BookingService.CancelBookingAsync(oldBooking.ID, cancelMemo, userID.Value);
                }
                changeUnitWorkflow.IsApproved = true;
            }
            changeUnitWorkflow.IsRequestApproved = true;
            changeUnitWorkflow.RequestApprovedDate = DateTime.Now;
            changeUnitWorkflow.RequestApproverUserID = userID;
            DB.ChangeUnitWorkflows.Update(changeUnitWorkflow);

            await DB.SaveChangesAsync();

        }
        public async Task BookingRequestRejectAsync(Guid changeUnitWorkflowID, RejectParam input, Guid? userID = null)
        {
            var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ID == changeUnitWorkflowID).FirstAsync();
            var bookingStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus).ToListAsync();
            var unitStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus).ToListAsync();

            #region Update newBooking && oldBooking
            var newBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.ToBookingID)
                                              .FirstAsync();
            var oldBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.FromBookingID)
                                              .FirstAsync();
            var oldUnit = await DB.Units.Where(o => o.ID == oldBooking.UnitID).FirstAsync();

            oldUnit.UnitStatusMasterCenterID = unitStatusMasterCenters.Where(o => o.Key == UnitStatusKeys.WaitingForAgreement).Select(o => o.ID).First();

            oldBooking.ChangeToBookingID = null;
            oldBooking.ChangeUnitByUserID = null;
            oldBooking.ChangeUnitDate = null;

            oldBooking.BookingStatusMasterCenterID = bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.Booking).Select(o => o.ID).First();

            // Delete NewBooking
            await BookingService.DeleteBookingAsync(newBooking.ID);
            #endregion

            changeUnitWorkflow.IsRequestApproved = false;
            changeUnitWorkflow.RequestRejectComment = input.Comment;
            changeUnitWorkflow.RequestApprovedDate = DateTime.Now;
            changeUnitWorkflow.RequestApproverUserID = userID;

            DB.Bookings.Update(oldBooking);
            DB.ChangeUnitWorkflows.Update(changeUnitWorkflow);
            await DB.SaveChangesAsync();
        }

        public async Task CancelChangeUnitAsync(Guid changeUnitWorkflowID)
        {
            var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.FromBookingID == changeUnitWorkflowID).FirstAsync();
            var oldBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.FromBookingID)
                                              .FirstAsync();
            var newBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.ToBookingID)
                                              .Include(o => o.BookingStatus)
                                              .FirstAsync();
            var oldUnit = await DB.Units.Where(o => o.ID == oldBooking.UnitID).FirstAsync();
            var newUnit = await DB.Units.Where(o => o.ID == newBooking.UnitID).FirstAsync();
            var bookingStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus).ToListAsync();


            if (newBooking.BookingStatus.Key == bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.WaitingForApproveMinPrice).Select(o => o.Key).First())
            {
                var minPriceBudget = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == newBooking.ID && o.IsRecalled == false && o.IsApproved == null).FirstAsync();
                minPriceBudget.IsRecalled = true;
                minPriceBudget.IsApproved = false;
                minPriceBudget.RecalledByUserID = newBooking.UpdatedByUserID;
                minPriceBudget.RecalledTime = newBooking.Updated;
                DB.Entry(minPriceBudget).State = EntityState.Modified;
            }

            oldBooking.ChangeToBookingID = null;
            oldBooking.ChangeUnitByUserID = null;
            oldBooking.ChangeUnitDate = null;
            oldBooking.BookingStatusMasterCenterID = bookingStatusMasterCenters.Where(o => o.Key == BookingStatusKeys.Booking).Select(o => o.ID).First();
            

            changeUnitWorkflow.IsApproved = false;

            //Delete NewBooking
            await BookingService.DeleteBookingAsync(newBooking.ID);

            DB.Bookings.Update(oldBooking);
            DB.ChangeUnitWorkflows.Update(changeUnitWorkflow);

            await DB.SaveChangesAsync();
        }

        public async Task<BookingChangeUnitWorkflowDTO> AgreementApproveAsync(Guid changeUnitWorkflowID)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingChangeUnitWorkflowDTO> AgreementRejectAsync(Guid changeUnitWorkflowID, RejectParam input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChangeUnitFileDTO>> GetChangeUnitFilesAsync(Guid changeUnitWorkflowID)
        {
            throw new NotImplementedException();
        }

        public async Task<ChangeUnitFileDTO> AddChangeUnitFileAsync(ChangeUnitFileDTO input)
        {
            throw new NotImplementedException();
        }
        public async Task<ChangeUnitFileDTO> UpdateChangeUnitFileAsync(Guid changeUnitWorkdlowID, ChangeUnitFileDTO input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteChangeUnitFileAsync(Guid changeUnitFileID)
        {
            throw new NotImplementedException();
        }
    }
}
