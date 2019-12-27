using Base.DTOs;
using Base.DTOs.FIN;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.Models.MST;
using FileStorage;
using Finance.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorHandling;
using Base.DTOs.USR;

namespace Finance.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public PaymentService(IConfiguration configuration, DatabaseContext db)
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

        public async Task<PaymentFormDTO> GetPaymentFormAsync(Guid bookingID, PaymentFormType formType = PaymentFormType.Normal, Guid? refID = null, decimal payAmount = 0)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID)
                                           .Include(o => o.Unit)
                                                .ThenInclude(o => o.UnitStatus)
                                           .Include(o => o.Project)
                                           .Include(o => o.UpdatedBy)
                                           .FirstAsync();
            var result = await PaymentFormDTO.CreateFromBookingAsync(booking, DB, FileHelper, refID, formType, payAmount);
            return result;
        }

        public async Task<Guid?> SubmitPaymentFormAsync(Guid bookingID, PaymentFormDTO input)
        {
            Guid? ReturnID = null;
            foreach (var item in input.PaymentMethods)
            {
                await item.ValidateAsync(DB);
            }

            #region InitialDataFromBooking
            var booking = await DB.Bookings.Where(o => o.ID == bookingID)
                                           .Include(o => o.Project)
                                                .ThenInclude(o => o.Company)
                                                    .ThenInclude(o => o.Province)
                                           .Include(o => o.Project)
                                                .ThenInclude(o => o.Company)
                                                    .ThenInclude(o => o.District)
                                           .Include(o => o.Project)
                                                .ThenInclude(o => o.Company)
                                                    .ThenInclude(o => o.SubDistrict)
                                           .Include(o => o.Unit)
                                           .Include(o => o.BookingStatus)
                                           .FirstOrDefaultAsync();

            // สถานะ จอง สัญญา โอน เท่านั้น
            List<string> bookingStatusCase = new List<string>();
            bookingStatusCase.Add(BookingStatusKeys.Booking);
            bookingStatusCase.Add(BookingStatusKeys.Contract);
            bookingStatusCase.Add(BookingStatusKeys.TransferOwnership);

            // TODO : Kim แก้เงื่อนไขสถานะ ชำระเงิน ตอนสัญญา
            //if (booking.BookingStatus.Key != BookingStatusKeys.Booking || booking.BookingStatus.Key != BookingStatusKeys.Contract)
            if (!bookingStatusCase.Contains(booking.BookingStatus.Key))
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR9999").FirstAsync();
                string desc = "ไม่สามารถชำระเงินได้ เนื่องจากไม่ใช่สถานะจองหรือสัญญา";
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }

            #endregion

            #region ReCalculate UnitPriceItem
            await RecalculateUnitPriceAsync(bookingID, input.ReceiveDate);
            #endregion

            #region CreateNewPayment

            var payment = new Payment();
            payment.Remark = input.Remark;
            payment.ReceiveDate = input.ReceiveDate;
            payment.BookingID = bookingID;
            payment.AttachFile = input.AttachFile?.Name;
            payment.TotalAmount = input.PaymentMethods.Sum(o => o.PayAmount);

            await DB.Payments.AddAsync(payment);
            await DB.SaveChangesAsync();

            #region ReceiptTempHeader

            var receiptTempHeader = await this.CreateReciptTempHeaderAsync(bookingID, payment.ID);

            #endregion

            // Create New Payment

            foreach (var item in input.PaymentMethods)
            {
                var paymentMethod = new PaymentMethod();
                if (input.PaymentFormType == PaymentFormType.Normal)
                {
                    #region CreditCard

                    if (item.PaymentMethodType.Key == PaymentMethodKeys.CreditCard)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodCreditCardMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.CreditCard)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodCreditCardMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);

                        // PaymentCreditCard
                        var paymentCreditCard = new PaymentCreditCard();
                        item.ToCreditCardModel(ref paymentCreditCard);
                        paymentCreditCard.PaymentMethodID = paymentMethod.ID;

                        await DB.PaymentCreditCards.AddAsync(paymentCreditCard);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        #region FET

                        if (paymentCreditCard.IsForeignCreditCard)
                        {
                            FET fet = new FET()
                            {
                                FETRequesterMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETRequester, FETRequesterKeys.Customer),
                                ReferentGUID = paymentMethod.ID,
                                ReferentType = "CreditCard",
                                BookingID = bookingID,
                                Amount = item.PayAmount,
                                FETStatusMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETStatus, FETStatusKeys.Waiting),
                                BankID = paymentCreditCard.BankID,
                                CustomerName = DB.GetFETCustomerName(bookingID)
                            };

                            await DB.AddAsync(fet);
                        }

                        #endregion FET

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }

                    #endregion CreditCard

                    #region Cash

                    if (item.PaymentMethodType.Key == PaymentMethodKeys.Cash)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        item.ToPaymentMethodModel(ref paymentMethod);
                        var paymentMethodCashMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                            && o.Key == PaymentMethodKeys.Cash)
                                                                            .Select(o => o.ID)
                                                                            .FirstAsync();
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodCashMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        // เก็บข้อมูลลง list
                        await DB.PaymentMethods.AddAsync(paymentMethod);
                        await DB.SaveChangesAsync();

                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);
                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion Cash

                    #region Debit
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.DebitCard)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodDebitCardMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.DebitCard)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodDebitCardMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);

                        // PaymentDebitCard
                        var paymentDebitCard = new PaymentDebitCard();
                        item.ToDebitModel(ref paymentDebitCard);
                        paymentDebitCard.PaymentMethodID = paymentMethod.ID;

                        await DB.PaymentDebitCards.AddAsync(paymentDebitCard);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion Debit

                    #region PaymentPersonalCheque
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.PersonalCheque)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodPersonalChequeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.PersonalCheque)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodPersonalChequeMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);

                        // PaymentPersonalCheque
                        var paymentPersonalCheque = new PaymentPersonalCheque();
                        item.ToPaymentPersonalChequeModel(ref paymentPersonalCheque);
                        paymentPersonalCheque.PaymentMethodID = paymentMethod.ID;
                        paymentPersonalCheque.IsFeeConfirm = true;

                        await DB.PaymentPersonalCheques.AddAsync(paymentPersonalCheque);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }

                    #endregion

                    #region PaymentCashierCheque
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.CashierCheque)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodCashierChequeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.CashierCheque)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodCashierChequeMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);
                        await DB.SaveChangesAsync();

                        // PaymentCashierCheque
                        var paymentCashierCheque = new PaymentCashierCheque();
                        item.ToPaymentCashierChequeModel(ref paymentCashierCheque);
                        paymentCashierCheque.PaymentMethodID = paymentMethod.ID;
                        paymentCashierCheque.IsFeeConfirm = true;

                        await DB.PaymentCashierCheques.AddAsync(paymentCashierCheque);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion

                    #region PaymentBankTransfer
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodBankTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.BankTransfer)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodBankTransferMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);


                        // PaymentBankTransfer
                        var paymentBankTransfer = new PaymentBankTransfer();
                        item.ToPaymentBankTransferModel(ref paymentBankTransfer);
                        paymentBankTransfer.PaymentMethodID = paymentMethod.ID;


                        await DB.PaymentBankTransfers.AddAsync(paymentBankTransfer);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion

                    #region PaymentQRCode
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.QRCode)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodQRCodeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.QRCode)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodQRCodeMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);


                        // PaymentBankTransfer
                        var paymentQRCode = new PaymentQRCode();
                        item.ToPaymentQRCodeModel(ref paymentQRCode);
                        paymentQRCode.PaymentMethodID = paymentMethod.ID;


                        await DB.PaymentQRCodes.AddAsync(paymentQRCode);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion

                    #region PaymentForeignBankTransfer
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodForeignBankTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.ForeignBankTransfer)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodForeignBankTransferMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);
                        await DB.SaveChangesAsync();

                        // ForeignBankTransfer
                        var paymentForeignBankTransfer = new PaymentForeignBankTransfer();
                        item.ToPaymentForeignBankTransferModel(ref paymentForeignBankTransfer);
                        paymentForeignBankTransfer.PaymentMethodID = paymentMethod.ID;


                        await DB.PaymentForeignBankTransfers.AddAsync(paymentForeignBankTransfer);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        #region FET
                        if (paymentForeignBankTransfer.IsRequestFET)
                        {
                            FET fet = new FET()
                            {
                                FETRequesterMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETRequester, FETRequesterKeys.Customer),
                                ReferentGUID = paymentMethod.ID,
                                ReferentType = "ForeignBankTransfer",
                                BookingID = bookingID,
                                Amount = item.PayAmount,
                                BankID = item.ForeignBankTransfer.ForeignBank.Id,
                                FETStatusMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.FETStatus, FETStatusKeys.Waiting)
                            };
                            await DB.AddAsync(fet);
                        }
                        #endregion FET

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion PaymentForeignBankTransfer
                }
                else if (input.PaymentFormType == PaymentFormType.UnknownPayment)
                {
                    #region PaymentUnknowPayment
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.UnknowPayment)
                    {
                        var paymentMethodForeignBankTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.ForeignBankTransfer)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();

                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodForeignBankTransferMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);

                        // PaymentUnknowPayment
                        var paymentUnknowPayment = new PaymentUnknownPayment();
                        paymentUnknowPayment.PaymentMethodID = paymentMethod.ID;
                        paymentUnknowPayment.UnknownPaymentID = input.RefID;

                        if ((input.RefID ?? Guid.Empty) != Guid.Empty)
                        {
                            await DB.UpdateUnknownPaymentStatusAsync(input.RefID ?? Guid.Empty);
                        }

                        await DB.PaymentUnknownPayments.AddAsync(paymentUnknowPayment);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);

                        var paymentItems = new List<PaymentItem>();
                        paymentItems.AddRange(paymentdatas.PaymentItems);

                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion
                }
                else if (input.PaymentFormType == PaymentFormType.ChangeUnit)
                {
                    #region PaymentFromChangeUnit
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.ChangeContract)
                    {
                        var paymentItems = new List<PaymentItem>();
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        var paymentMethodChangeContractMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.ChangeContract)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();
                        // PaymentMethod
                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodChangeContractMasterCenterID;
                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);


                        // PaymentChangeUnit
                        var paymentChangeUnits = new List<PaymentChangeUnit>();

                        var oldPaymentIds = await DB.Payments.Where(o => o.BookingID == input.RefID).Select(o => o.ID).ToListAsync();
                        var oldPaymentMethods = await DB.PaymentMethods.Where(o => oldPaymentIds.Contains(o.PaymentID)).ToListAsync();

                        foreach (var oldItem in oldPaymentMethods)
                        {
                            var checkpaymentChangeUnit = await DB.PaymentChangeUnits.Where(o => o.PaymentMethodID == oldItem.ID).FirstOrDefaultAsync();
                            var paymentChangeUnit = new PaymentChangeUnit();
                            paymentChangeUnit.PaymentMethodID = paymentMethod.ID;
                            paymentChangeUnit.FromPaymentMethodID = oldItem.ID;
                            paymentChangeUnit.BasePaymentMethodID = checkpaymentChangeUnit == null ? oldItem.ID : checkpaymentChangeUnit.BasePaymentMethodID;
                            paymentChangeUnits.Add(paymentChangeUnit);
                        }

                        await DB.PaymentChangeUnits.AddRangeAsync(paymentChangeUnits);
                        await DB.SaveChangesAsync();

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);
                    }
                    #endregion
                }
                // TODO : PaymentService DirectCredit DirectDebit
                else if (input.PaymentFormType == PaymentFormType.DirectCredit || input.PaymentFormType == PaymentFormType.DirectDebit)
                {
                    #region DirectDebitCredit
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.UnknowPayment)
                    {
                        var paymentItems = new List<PaymentItem>();

                        // PaymentMethod
                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        if (input.PaymentFormType == PaymentFormType.DirectCredit)
                        {
                            var paymentMethodDirectCreditMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                       && o.Key == PaymentMethodKeys.DirectCredit)
                                                                                                       .Select(o => o.ID)
                                                                                                       .FirstAsync();

                            paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodDirectCreditMasterCenterID;
                        }
                        else
                        {
                            var paymentMethodDirectDebitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                               && o.Key == PaymentMethodKeys.DirectDebit)
                                                                               .Select(o => o.ID)
                                                                               .FirstAsync();

                            paymentMethod.PaymentMethodTypeMasterCenterID = paymentMethodDirectDebitMasterCenterID;
                        }

                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);

                        // paymentDirectCreditDebit
                        var paymentDirectCreditDebit = new PaymentDirectCreditDebit();
                        paymentDirectCreditDebit.PaymentMethodID = paymentMethod.ID;
                        paymentDirectCreditDebit.DirectCreditDebitExportDetailID = input.RefID;

                        await DB.PaymentDirectCreditDebits.AddAsync(paymentDirectCreditDebit);
                        await DB.SaveChangesAsync();

                        ReturnID = paymentDirectCreditDebit.ID;

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);
                        paymentItems.AddRange(paymentdatas.PaymentItems);
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);

                    }
                    #endregion DirectDebitCredit
                }
                // TODO : PaymentService BillPayment
                else if (input.PaymentFormType == PaymentFormType.BillPayment)
                {
                    #region BillPayment
                    if (item.PaymentMethodType.Key == PaymentMethodKeys.BillPayment)
                    {


                        // PaymentMethod

                        var paymentBillPaymentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                                   && o.Key == PaymentMethodKeys.BillPayment)
                                                                                                   .Select(o => o.ID)
                                                                                                   .FirstAsync();

                        paymentMethod.PaymentMethodTypeMasterCenterID = paymentBillPaymentMasterCenterID;


                        paymentMethod.PaymentID = payment.ID;
                        paymentMethod.PayAmount = item.PayAmount;

                        await DB.PaymentMethods.AddAsync(paymentMethod);


                        // PaymentUnknowPayment
                        var paymentBillPayment = new PaymentBillPayment();
                        paymentBillPayment.PaymentMethodID = paymentMethod.ID;
                        paymentBillPayment.BillPaymentDetailID = input.RefID;
                        paymentBillPayment.IsWrongAccount = input.IsWrongAccount ?? false;

                        await DB.PaymentBillPayments.AddAsync(paymentBillPayment);
                        await DB.SaveChangesAsync();

                        ReturnID = input.RefID;

                        // Update UnitPirce And Get payments
                        var paymentdatas = await PayToUnitAsync(bookingID, item.PayAmount, payment.ID, paymentMethod.ID);

                        var paymentItems = new List<PaymentItem>();
                        paymentItems.AddRange(paymentdatas.PaymentItems);

                        var paymentMethodToItems = new List<PaymentMethodToItem>();
                        paymentMethodToItems.AddRange(paymentdatas.PaymentMethodToItems);

                        await DB.PaymentItems.AddRangeAsync(paymentItems);
                        await DB.PaymentMethodToItems.AddRangeAsync(paymentMethodToItems);

                        await DB.SaveChangesAsync();

                        var paymentItemIDs = paymentItems.Select(o => o.ID).ToList();
                        var dbPaymentItems = await DB.PaymentItems.Where(o => paymentItemIDs.Contains(o.ID))
                                                                  .Include(o => o.UnitPriceItem)
                                                                  .Include(o => o.UnitPriceInstallment)
                                                                  .ToListAsync();

                        await CreateReceiptTempDetailAsync(dbPaymentItems, receiptTempHeader.ID);

                    }
                    #endregion BillPayment
                }
            }
            #endregion

            return ReturnID;
        }

        public async Task<List<PaymentHistoryDTO>> GetPaymentHistoryListAsync(Guid bookingID)
        {
            if (bookingID == Guid.Empty)
                return new List<PaymentHistoryDTO>();

            var booking = await DB.Bookings.Where(o => o.ID == bookingID)
                                           .Include(o => o.Unit)
                                                .ThenInclude(o => o.UnitStatus)
                                           .Include(o => o.Project)
                                           .Include(o => o.UpdatedBy)
                                           .FirstAsync();

            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();
            var unitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID)
                                                        .Include(o => o.MasterPriceItem)
                                                        .Include(o => o.PriceUnit)
                                                        .OrderBy(o => o.Order)
                                                        .ToListAsync();

            var payments = await DB.Payments.Where(o => o.BookingID == bookingID)
                                            .OrderBy(o => o.Created)
                                            .ToListAsync();

            var paymentIds = payments.Select(o => o.ID).ToList();

            var paymentMethods = await DB.PaymentMethods.Where(o => paymentIds.Contains(o.PaymentID))
                                                       .OrderBy(o => o.Created)
                                                       .ToListAsync();


            var paymentItems = await DB.PaymentItems.Where(o => paymentIds.Contains(o.PaymentID)).ToListAsync();
            var paymentItemIds = paymentItems.Select(o => o.ID).ToList();
            var paymentMethodToItems = await DB.PaymentMethodToItems.Where(o => paymentItemIds.Contains((Guid)o.PaymentItemID))
                                                                    .Include(o => o.PaymentMethod)
                                                                        .ThenInclude(o => o.PaymentMethodType)
                                                                    .Include(o => o.PaymentMethod)
                                                                        .ThenInclude(o => o.Payment)
                                                                    .Include(o => o.PaymentItem)
                                                                        .ThenInclude(o => o.MasterPriceItem)
                                                                    .Include(o => o.PaymentItem)
                                                                        .ThenInclude(o => o.UnitPriceItem)
                                                                    .Include(o => o.PaymentItem)
                                                                        .ThenInclude(o => o.UnitPriceInstallment)
                                                                    .Include(o => o.PaymentMethod)
                                                                        .ThenInclude(o => o.UpdatedBy)
                                                                    .ToListAsync();

            paymentMethodToItems = paymentMethodToItems.OrderBy(e => e.PaymentItem?.UnitPriceItem?.Order).ThenBy(e => e.PaymentItem?.UnitPriceInstallment?.Period).ToList();

            var results = paymentMethodToItems.GroupBy(o => new { o.PaymentItem.MasterPriceItem, o.PaymentItem.UnitPriceInstallment, o.PaymentItem.UnitPriceItem, o.PaymentMethod, o.PaymentMethod.Payment })
                                     .Select(o => new PaymentHistoryDTO
                                     {

                                         Id = o.Key.PaymentMethod.ID,
                                         //Receipt = o.Key.Payment?.ID == null ? DB.ReceiptTempHeaders.Where(e => e.PaymentID == o.Key.Payment.ID).Select(e => e.ReceiptTempNo).FirstOrDefault() : "",
                                         Receipt = new ReceiptTempListDTO
                                         {
                                             ReceiptTempNo = DB.ReceiptTempHeaders.Where(e => e.PaymentID == o.Key.Payment.ID).Select(e => e.ReceiptTempNo).FirstOrDefault()
                                         },
                                         ReceiveDate = o.Select(p => p.PaymentMethod.Payment.ReceiveDate).FirstOrDefault(),
                                         Name = o.Key.UnitPriceInstallment == null ? o.Select(p => p.PaymentItem.UnitPriceItem.Name).FirstOrDefault() : o.Select(p => p.PaymentItem.UnitPriceItem.Name + " " + p.PaymentItem.UnitPriceInstallment.Period).FirstOrDefault(),
                                         PaymentMethodType = Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(o.Key.PaymentMethod.PaymentMethodType),
                                         Amount = o.Key.UnitPriceInstallment == null ? (decimal?)o.Where(p => p.PaymentItem.UnitPriceItemID == o.Key.UnitPriceItem.ID).Sum(p => p.PaymentItem.PayAmount) : (decimal?)o.Where(p => p.PaymentItem.UnitPriceItemID == o.Key.UnitPriceItem.ID && p.PaymentItem.UnitPriceInstallmentID == o.Key.UnitPriceInstallment.ID).Sum(p => p.PaymentItem.PayAmount),
                                         Updated = o.Key.PaymentMethod.Updated,
                                         UpdatedBy = o.Key.PaymentMethod.UpdatedBy?.DisplayName,
                                         PaymentCreated = UserDTO.CreateFromModel(o.Select(p => p.PaymentMethod.Payment.CreatedBy).FirstOrDefault()),
                                     }).ToList();

            return results;
        }

        public async Task CancelPaymentFormAsync(Guid PaymentMethodID)
        {
            var PaymentMethod = DB.PaymentMethods.Where(x => x.ID == PaymentMethodID).FirstOrDefault();
            if (PaymentMethod != null)
            {
                var DepositDetail = DB.DepositDetails.Include(o => o.DepositHeader).Where(x => x.PaymentMethodID == PaymentMethod.ID).FirstOrDefault();
                //CheckDepositAsync
                if (DepositDetail != null)
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0094").FirstAsync();
                    var msg = errMsg.Message.Replace("[message]", "");
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }


                var Payment = DB.Payments.Where(x => x.ID == PaymentMethod.PaymentID).FirstOrDefault();
                var PaymentItem = DB.PaymentItems.Where(x => x.PaymentID == PaymentMethod.PaymentID).ToList();
                var PaymentMethodToItem = DB.PaymentMethodToItems.Where(x => x.PaymentMethodID == PaymentMethod.ID).ToList();



                PaymentMethod.IsDeleted = true;
                Payment.IsDeleted = true;
                PaymentItem.Any(x => x.IsDeleted = true);
                PaymentMethodToItem.Any(x => x.IsDeleted = true);

                DB.Entry(PaymentMethod).State = EntityState.Modified;
                DB.Entry(Payment).State = EntityState.Modified;
                DB.Entry(PaymentItem).State = EntityState.Modified;
                DB.Entry(PaymentMethodToItem).State = EntityState.Modified;
            }
            else
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0094").FirstAsync();
                var msg = errMsg.Message.Replace("[message]", "เกิดข้อผิดพลาดไม่พบข้อมูลใบเสร็จ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
        }

        public async Task<List<PaymentUnitPriceItemDTO>> GetPaymentUnitPriceItemsAsync(Guid bookingID)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID)
                                           .Include(o => o.Unit)
                                                .ThenInclude(o => o.UnitStatus)
                                           .Include(o => o.Project)
                                           .Include(o => o.UpdatedBy)
                                           .FirstAsync();

            var listMasterPriceItemKeys = new List<Guid>();
            listMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
            listMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
            listMasterPriceItemKeys.Add(MasterPriceItemKeys.DownAmount);

            var results = new List<PaymentUnitPriceItemDTO>();

            var payments = await DB.Payments.Where(o => o.BookingID == booking.ID).ToListAsync();
            var paymentIDs = await DB.Payments.Where(o => o.BookingID == booking.ID).OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();
            var sumPayAmount = await DB.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && listMasterPriceItemKeys.Contains((Guid)o.MasterPriceItemID)).SumAsync(o => o.PayAmount);
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstAsync();
            var unitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID && listMasterPriceItemKeys.Contains((Guid)o.MasterPriceItemID)).Include(o => o.MasterPriceItem).OrderBy(o => o.Order).ToListAsync();


            int i = 0;
            int j = 0;
            int k = 0;
            var order = 1;
            decimal payAmount = 0;
            foreach (var item in unitPriceItems)
            {
                if (sumPayAmount > 0)
                {
                    i++;
                    if (item.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                    {
                        if (sumPayAmount - item.Amount >= 0)
                        {
                            var receipts = new List<ReceiptTempListDTO>();
                            var result = PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, item.Amount, order);
                            if (k > payments.Count() - 1)
                            {
                                var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k - 1], DB);
                                receipts.Add(receipt);
                                result.Receipts = receipts;
                            }
                            else
                            {
                                for (int l = k; l < payments.Count(); l++)
                                {
                                    payAmount += payments[k].TotalAmount;
                                    var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k], DB);
                                    receipts.Add(receipt);
                                    k++;
                                    if (payAmount - item.Amount >= 0)
                                    {
                                        result.Receipts = receipts;
                                        break;
                                    }

                                }
                            }
                            results.Add(result);
                            order++;
                        }
                        else if (sumPayAmount - item.Amount < 0)
                        {
                            var amount = sumPayAmount;
                            var receipts = new List<ReceiptTempListDTO>();
                            var result = PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, sumPayAmount, order);
                            if (k > payments.Count() - 1)
                            {
                                var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k - 1], DB);
                                receipts.Add(receipt);
                            }
                            else
                            {
                                for (int l = k; l < payments.Count(); l++)
                                {
                                    payAmount += payments[k].TotalAmount;
                                    var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k], DB);
                                    receipts.Add(receipt);
                                    k++;
                                }
                            }
                            result.Receipts = receipts;
                            results.Add(result);
                            i++;
                            order++;
                            break;
                        }
                        sumPayAmount -= item.Amount;
                    }
                    if (item.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                    {
                        var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == item.ID)
                                                                                  .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                      .ThenInclude(o => o.MasterPriceItem)
                                                                                  .OrderBy(o => o.Period)
                                                                                  .ToListAsync();
                        foreach (var installment in unitPriceInstallments)
                        {
                            j++;
                            if (sumPayAmount - installment.Amount >= 0)
                            {
                                var result = PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, installment.Amount, order);
                                var receipts = new List<ReceiptTempListDTO>();
                                if (k > payments.Count() - 1)
                                {
                                    var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k - 1], DB);
                                    receipts.Add(receipt);
                                    result.Receipts = receipts;
                                }
                                else
                                {
                                    for (int l = k; l < payments.Count(); l++)
                                    {
                                        payAmount += payments[k].TotalAmount;
                                        var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k], DB);
                                        receipts.Add(receipt);
                                        k++;
                                        if (payAmount - item.Amount >= 0)
                                        {
                                            result.Receipts = receipts;
                                            break;
                                        }
                                    }
                                }
                                results.Add(result);

                                order++;
                            }
                            else if (sumPayAmount - installment.Amount < 0)
                            {
                                var amount = sumPayAmount;
                                var receipts = new List<ReceiptTempListDTO>();
                                var result = PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, sumPayAmount, order);
                                if (k > payments.Count() - 1)
                                {
                                    var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k - 1], DB);
                                    receipts.Add(receipt);
                                }
                                else
                                {
                                    for (int l = k; l < payments.Count(); l++)
                                    {
                                        payAmount += payments[k].TotalAmount;
                                        var receipt = await ReceiptTempListDTO.CreateFromUnitPriceItemModelAsync(payments[k], DB);
                                        receipts.Add(receipt);
                                        k++;
                                    }
                                }
                                result.Receipts = receipts;
                                results.Add(result);
                                j++;
                                order++;
                                break;
                            }
                            sumPayAmount -= installment.Amount;
                        }
                    }
                }
                else
                {
                    i++;
                    if (item.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                    {
                        results.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(item, 0, order));
                        order++;
                    }
                    if (item.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                    {
                        var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == item.ID)
                                                                                     .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                         .ThenInclude(o => o.MasterPriceItem)
                                                                                     .OrderBy(o => o.Period)
                                                                                     .ToListAsync();
                        foreach (var installment in unitPriceInstallments)
                        {
                            results.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(installment, 0, order));
                            order++;
                        }
                    }
                }
            }
            if (i != 0)
            {
                for (int a = i - 1; a < unitPriceItems.Count(); a++)
                {
                    if (unitPriceItems[a].MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                    {
                        results.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceItemModel(unitPriceItems[a], 0, order));
                        order++;
                    }
                    if (unitPriceItems[a].MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                    {
                        var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems[a].ID)
                                                                                     .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                         .ThenInclude(o => o.MasterPriceItem)
                                                                                     .OrderBy(o => o.Period)
                                                                                     .ToListAsync();
                        if (j != 0)
                        {
                            for (int b = j - 1; b < unitPriceInstallments.Count(); b++)
                            {
                                results.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(unitPriceInstallments[b], 0, order));
                                order++;
                            }
                        }
                        else
                        {
                            for (int b = j; b < unitPriceInstallments.Count(); b++)
                            {
                                results.Add(PaymentUnitPriceItemDTO.CreateFromUnitPriceInstallmentModel(unitPriceInstallments[b], 0, order));
                                order++;
                            }
                        }
                    }

                }
            }
            results = results.OrderBy(o => o.Order).ToList();
            return results;
        }
        public async Task RecalculateUnitPriceAsync(Guid bookingID, DateTime? ReceiveDate)
        {
            var paymentIDs = await DB.Payments.Where(o => o.BookingID == (Guid?)bookingID).Select(o => o.ID).ToListAsync();
            var sumPayAmount = DB.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID)).Sum(o => o.PayAmount);
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == (Guid?)bookingID && o.IsActive == true).FirstAsync();
            var listMasterPriceItemKey = new List<Guid?>();

            listMasterPriceItemKey.Add(MasterPriceItemKeys.BookingAmount);
            listMasterPriceItemKey.Add(MasterPriceItemKeys.ContractAmount);
            listMasterPriceItemKey.Add(MasterPriceItemKeys.DownAmount);


            var unitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID && listMasterPriceItemKey.Contains(o.MasterPriceItemID))
                                                        .Include(o => o.MasterPriceItem)
                                                        .OrderBy(o => o.Order)
                                                        .ToListAsync();

            var updateUnitPriceItems = new List<UnitPriceItem>();
            var updateUnitPriceInstallments = new List<UnitPriceInstallment>();
            int i = 0;
            int j = 0;
            if (sumPayAmount > 0)
            {
                foreach (var unitPriceItem in unitPriceItems)
                {
                    i++;
                    if (unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                    {
                        if (sumPayAmount - unitPriceItem.Amount >= 0)
                        {
                            unitPriceItem.IsPaid = true;
                            unitPriceItem.PayDate = unitPriceItem.PayDate == null ? ReceiveDate : unitPriceItem.PayDate;
                            updateUnitPriceItems.Add(unitPriceItem);
                        }
                        else
                        {
                            unitPriceItem.IsPaid = false;
                            unitPriceItem.PayDate = unitPriceItem.PayDate == null ? ReceiveDate : unitPriceItem.PayDate;
                            updateUnitPriceItems.Add(unitPriceItem);
                            i++;
                            break;
                        }
                        sumPayAmount -= unitPriceItem.Amount;
                    }
                    if (unitPriceItem.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                    {
                        var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItem.ID).OrderBy(o => o.Period).ToListAsync();

                        foreach (var unitPriceInstallment in unitPriceInstallments)
                        {
                            j++;
                            if (sumPayAmount - unitPriceInstallment.Amount >= 0)
                            {
                                // Update UnitPriceInstallment
                                unitPriceInstallment.IsPaid = true;
                                unitPriceInstallment.PaidAmount = unitPriceInstallment.Amount;
                                unitPriceInstallment.PayDate = unitPriceInstallment.PayDate == null ? ReceiveDate : unitPriceItem.PayDate;
                                updateUnitPriceInstallments.Add(unitPriceInstallment);
                            }
                            else
                            {
                                // Update UnitPriceInstallment
                                unitPriceInstallment.IsPaid = false;
                                unitPriceInstallment.PaidAmount = unitPriceInstallment.Amount - sumPayAmount;
                                unitPriceInstallment.PayDate = unitPriceInstallment.PayDate == null ? ReceiveDate : unitPriceItem.PayDate;
                                updateUnitPriceInstallments.Add(unitPriceInstallment);
                                j++;
                                break;
                            }
                            sumPayAmount -= unitPriceInstallment.Amount;
                        }
                    }
                }

                if (i != 0)
                {
                    for (int a = i - 1; a < unitPriceItems.Count(); a++)
                    {
                        if (unitPriceItems[a].MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            unitPriceItems[a].IsPaid = null;
                            unitPriceItems[a].PayDate = null;
                            updateUnitPriceItems.Add(unitPriceItems[a]);
                        }
                        if (unitPriceItems[a].MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems[a].ID)
                                                                                         .Include(o => o.InstallmentOfUnitPriceItem)
                                                                                             .ThenInclude(o => o.MasterPriceItem)
                                                                                         .OrderBy(o => o.Period)
                                                                                         .ToListAsync();
                            if (j != 0)
                            {
                                for (int b = j - 1; b < unitPriceInstallments.Count(); b++)
                                {
                                    unitPriceInstallments[b].IsPaid = null;
                                    unitPriceInstallments[b].PaidAmount = 0;
                                    unitPriceInstallments[b].PayDate = null;
                                    updateUnitPriceInstallments.Add(unitPriceInstallments[b]);
                                }
                            }
                            else
                            {
                                for (int b = j; b < unitPriceInstallments.Count(); b++)
                                {
                                    unitPriceInstallments[b].IsPaid = null;
                                    unitPriceInstallments[b].PaidAmount = 0;
                                    unitPriceInstallments[b].PayDate = null;
                                    updateUnitPriceInstallments.Add(unitPriceInstallments[b]);
                                }
                            }
                        }
                    }
                }

                DB.UnitPriceItems.UpdateRange(updateUnitPriceItems);
                DB.UnitPriceInstallments.UpdateRange(updateUnitPriceInstallments);
                await DB.SaveChangesAsync();
            }
        }
        public async Task<ReceiptTempHeader> CreateReciptTempHeaderAsync(Guid bookingID, Guid paymentID)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID)
                                           .Include(o => o.Unit)
                                           .Include(o => o.Project)
                                           .FirstAsync();

            var payment = await DB.Payments.Where(o => o.ID == paymentID).FirstAsync();
            var bookingOwner = await DB.BookingOwners.Where(o => o.BookingID == bookingID && o.IsMainOwner == true)
                                                .Include(o => o.ContactTitleTH)
                                                .Include(o => o.ContactTitleEN)
                                                .FirstAsync();

            var bookingOwnerAddress = await DB.BookingOwnerAddresses.Include(o => o.ContactAddressType)
                                                                    .Include(o => o.Country)
                                                                    .Include(o => o.Province)
                                                                    .Include(o => o.District)
                                                                    .Include(o => o.SubDistrict)
                                                                    .Where(o => o.BookingOwnerID == bookingOwner.ID
                                                                            && o.ContactAddressType.Key == ContactAddressTypeKeys.Contact)
                                                                    .FirstOrDefaultAsync();

            var agreement = await DB.Agreements.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
            var receiptTempHeader = new ReceiptTempHeader();
            receiptTempHeader.Remark = payment.Remark;
            receiptTempHeader.BookingNo = booking?.BookingNo;
            receiptTempHeader.AgreementNo = agreement?.AgreementNo;

            #region ReceiptTempNo
            string year = Convert.ToString(DateTime.Today.Year);
            var key = "TF" + booking?.Project?.ProjectNo + year[2] + year[3];
            var type = "FIN.ReceiptTempHeader";
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

                receiptTempHeader.ReceiptTempNo = key + runningNumberCounter.Count.ToString("0000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                receiptTempHeader.ReceiptTempNo = key + runningno.Count.ToString("0000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }
            #endregion

            #region Company
            receiptTempHeader.PaymentID = payment.ID;
            receiptTempHeader.CompanyNameTH = booking.Project.Company?.NameTH;
            receiptTempHeader.CompanyNameEN = booking.Project.Company?.NameEN;
            receiptTempHeader.CompanyHouseNoTH = booking.Project.Company?.AddressTH;
            receiptTempHeader.CompanyHouseNoEN = booking.Project.Company?.AddressEN;
            receiptTempHeader.CompanyBuildingTH = booking.Project.Company?.BuildingTH;
            receiptTempHeader.CompanyBuildingEN = booking.Project.Company?.BuildingEN;
            receiptTempHeader.CompanySoiTH = booking.Project.Company?.SoiTH;
            receiptTempHeader.CompanySoiEN = booking.Project.Company?.SoiEN;
            receiptTempHeader.CompanyRoadTH = booking.Project.Company?.RoadTH;
            receiptTempHeader.CompanyRoadEN = booking.Project.Company?.RoadEN;
            receiptTempHeader.CompanyProvinceTH = booking.Project.Company?.Province?.NameTH;
            receiptTempHeader.CompanyProvinceEN = booking.Project.Company?.Province?.NameEN;
            receiptTempHeader.CompanyDistrictTH = booking.Project.Company?.District?.NameTH;
            receiptTempHeader.CompanyDistrictEN = booking.Project.Company?.District?.NameEN;
            receiptTempHeader.CompanySubDistrictTH = booking.Project.Company?.SubDistrict?.NameTH;
            receiptTempHeader.CompanySubDistrictEN = booking.Project.Company?.SubDistrict?.NameEN;
            receiptTempHeader.CompanyPostalCode = booking.Project.Company?.PostalCode;
            receiptTempHeader.CompanyTelephone = booking.Project.Company?.Telephone;
            receiptTempHeader.CompanyFax = booking.Project.Company?.Fax;
            #endregion

            #region Contract
            receiptTempHeader.ContactTitle = bookingOwner.ContactTitleTH?.Name;
            receiptTempHeader.ContactTitleExtEN = bookingOwner.ContactTitleEN?.Name;
            receiptTempHeader.ContactFirstNameTH = bookingOwner.FirstNameTH;
            receiptTempHeader.ContactFirstNameEN = bookingOwner.FirstNameEN;
            receiptTempHeader.ContactMiddleNameTH = bookingOwner.MiddleNameTH;
            receiptTempHeader.ContactMiddleNameEN = bookingOwner.MiddleNameEN;
            receiptTempHeader.ContactLastNameTH = bookingOwner.LastNameTH;
            receiptTempHeader.ContactLastNameEN = bookingOwner.LastNameEN;
            receiptTempHeader.CompanyHouseNoTH = bookingOwnerAddress.HouseNoTH;
            receiptTempHeader.CompanyHouseNoEN = bookingOwnerAddress.HouseNoEN;
            receiptTempHeader.ContactMooTH = bookingOwnerAddress.MooTH;
            receiptTempHeader.ContactMooEN = bookingOwnerAddress.MooEN;
            receiptTempHeader.ContactVillageTH = bookingOwnerAddress.VillageTH;
            receiptTempHeader.ContactVillageEN = bookingOwnerAddress.VillageEN;
            receiptTempHeader.ContactSoiTH = bookingOwnerAddress.SoiTH;
            receiptTempHeader.ContactSoiEN = bookingOwnerAddress.SoiEN;
            receiptTempHeader.ContactRoadTH = bookingOwnerAddress.RoadTH;
            receiptTempHeader.ContactRoadEN = bookingOwnerAddress.RoadEN;
            receiptTempHeader.ContactPostalCode = bookingOwnerAddress.PostalCode;
            receiptTempHeader.ContactCountryTH = bookingOwnerAddress.Country?.NameTH;
            receiptTempHeader.ContactCountryEN = bookingOwnerAddress.Country?.NameEN;
            receiptTempHeader.ContactProvinceTH = bookingOwnerAddress.Province?.NameTH;
            receiptTempHeader.ContactProvinceEN = bookingOwnerAddress.Province?.NameEN;
            receiptTempHeader.ContactDistrictTH = bookingOwnerAddress.District?.NameTH;
            receiptTempHeader.ContactDistrictEN = bookingOwnerAddress.District?.NameEN;
            receiptTempHeader.ContactSubDistrictTH = bookingOwnerAddress.SubDistrict?.NameTH;
            receiptTempHeader.ContactSubDistrictEN = bookingOwnerAddress.SubDistrict?.NameEN;
            receiptTempHeader.ReceiveDate = payment.ReceiveDate;
            receiptTempHeader.ProjectNo = booking.Project.ProjectNo;
            receiptTempHeader.ProjectName = booking.Project.ProjectNameTH;
            receiptTempHeader.ProjectNameEN = booking.Project.ProjectNameEN;
            receiptTempHeader.UnitNo = booking.Unit.UnitNo;

            #endregion

            await DB.ReceiptTempHeaders.AddAsync(receiptTempHeader);
            await DB.SaveChangesAsync();

            var result = await DB.ReceiptTempHeaders.Where(o => o.ID == receiptTempHeader.ID).FirstAsync();
            return result;
        }
        public async Task CreateReceiptTempDetailAsync(List<PaymentItem> paymentItems, Guid receiptTempHeaderID)
        {
            var receiptTempHeader = await DB.ReceiptTempHeaders.Where(o => o.ID == receiptTempHeaderID).FirstAsync();
            var receiptTempDetails = new List<ReceiptTempDetail>();
            foreach (var item in paymentItems)
            {
                var receiptTempDetail = new ReceiptTempDetail();
                receiptTempDetail.ReceiptTempHeaderID = receiptTempHeaderID;
                receiptTempDetail.PaymentItemID = item.ID;
                receiptTempDetail.Description = item.UnitPriceInstallmentID == null ? item.UnitPriceItem.Name : item.UnitPriceItem.Name + " งวดที่" + item.UnitPriceInstallment.Period.ToString();
                receiptTempDetail.DescriptionEN = "";
                receiptTempDetail.Amount = item.PayAmount;
                receiptTempDetails.Add(receiptTempDetail);
            }

            await DB.ReceiptTempDetails.AddRangeAsync(receiptTempDetails);
            await DB.SaveChangesAsync();
        }
        public async Task<PaymentLists> PayToUnitAsync(Guid bookingID, decimal payAmount, Guid paymentID, Guid paymentMethodID)
        {
            var payment = await DB.Payments.Where(o => o.ID == paymentID).FirstAsync();
            var paymentMethod = await DB.PaymentMethods.Where(o => o.ID == paymentMethodID).FirstAsync();
            var paymentItems = new List<PaymentItem>();
            var paymentMethodToItems = new List<PaymentMethodToItem>();

            var listMasterPriceItemKey = new List<Guid?>();

            listMasterPriceItemKey.Add(MasterPriceItemKeys.BookingAmount);
            listMasterPriceItemKey.Add(MasterPriceItemKeys.ContractAmount);
            listMasterPriceItemKey.Add(MasterPriceItemKeys.DownAmount);

            var updateUnitPriceItems = new List<UnitPriceItem>();
            var updateUnitPriceInstallments = new List<UnitPriceInstallment>();

            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();

            var unitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID && listMasterPriceItemKey.Contains(o.MasterPriceItemID) && o.IsPaid != true)
                                                        .Include(o => o.MasterPriceItem)
                                                        .OrderBy(o => o.Order)
                                                        .ToListAsync();
            var remainAmount = payAmount;

            foreach (var unitPriceItem in unitPriceItems)
            {
                var oldPaymentItems = await DB.PaymentItems.Where(o => o.PaymentID == payment.ID && o.MasterPriceItemID == unitPriceItem.MasterPriceItemID && o.RemainAmount != 0).ToListAsync();
                var remainOldPayment = oldPaymentItems.OrderBy(o => o.RemainAmount).FirstOrDefault();

                #region alreadyPaymentItem
                if (remainOldPayment != null)
                {
                    if (remainAmount != 0)
                    {
                        if (remainAmount >= remainOldPayment.RemainAmount && unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            // Update UnitPrice
                            unitPriceItem.IsPaid = true;
                            //unitPriceItem.PaidAmount = remainOldPlayment.RemainAmount;
                            unitPriceItem.PayDate = payment.ReceiveDate;
                            updateUnitPriceItems.Add(unitPriceItem);

                            // Create PaymentItem
                            var paymentItem = new PaymentItem();
                            paymentItem.PayAmount = remainOldPayment.RemainAmount;
                            paymentItem.PaymentID = payment.ID;
                            paymentItem.RemainAmount = 0;
                            paymentItem.UnitPriceItemID = unitPriceItem.ID;
                            paymentItem.ItemAmount = remainOldPayment.RemainAmount;
                            paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;

                            paymentItems.Add(paymentItem);

                            // Create PaymentMethodToItem
                            var paymentMethodToItem = new PaymentMethodToItem();
                            paymentMethodToItem.PaymentItemID = paymentItem.ID;
                            paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                            paymentMethodToItem.PayAmount = remainOldPayment.RemainAmount;

                            paymentMethodToItems.Add(paymentMethodToItem);
                        }
                        else if (remainAmount < remainOldPayment.RemainAmount && unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            // Update UnitPrice
                            //unitPriceItem.PaidAmount = remainAmount;
                            unitPriceItem.IsPaid = false;
                            unitPriceItem.PayDate = payment.ReceiveDate;
                            updateUnitPriceItems.Add(unitPriceItem);

                            // Create PaymentItem
                            var paymentItem = new PaymentItem();
                            paymentItem.PayAmount = remainAmount;
                            paymentItem.PaymentID = payment.ID;
                            paymentItem.UnitPriceItemID = unitPriceItem.ID;
                            paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                            paymentItem.ItemAmount = remainOldPayment.RemainAmount;
                            paymentItem.RemainAmount = remainOldPayment.RemainAmount - remainAmount;


                            paymentItems.Add(paymentItem);

                            // Create PaymentMethodToItem
                            var paymentMethodToItem = new PaymentMethodToItem();
                            paymentMethodToItem.PaymentItemID = paymentItem.ID;
                            paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                            paymentMethodToItem.PayAmount = remainAmount;

                            paymentMethodToItems.Add(paymentMethodToItem);
                            break;
                        }
                        else if (remainAmount >= 0 && unitPriceItem.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItem.ID && o.IsPaid != true).OrderBy(o => o.Period).ToListAsync();
                            var remainTotalAmount = remainAmount;

                            foreach (var unitPriceInstallment in unitPriceInstallments)
                            {
                                var remainOldPaymentInstallment = unitPriceInstallment.Amount - unitPriceInstallment.PaidAmount;
                                if (remainOldPaymentInstallment != 0)
                                {
                                    if (remainTotalAmount >= remainOldPaymentInstallment)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceInstallment.IsPaid = true;
                                        unitPriceInstallment.PaidAmount = unitPriceInstallment.Amount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = remainOldPaymentInstallment;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = 0;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = remainOldPaymentInstallment;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                    }
                                    if (remainTotalAmount < remainOldPaymentInstallment)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceItem.IsPaid = false;
                                        unitPriceInstallment.PaidAmount = remainTotalAmount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = remainTotalAmount;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = remainOldPaymentInstallment - remainTotalAmount;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = remainOldPaymentInstallment;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                        remainAmount = remainTotalAmount;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (remainTotalAmount >= unitPriceInstallment.Amount)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceInstallment.IsPaid = true;
                                        unitPriceInstallment.PaidAmount = unitPriceInstallment.Amount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = remainOldPaymentInstallment;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = 0;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = unitPriceInstallment.Amount;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                    }
                                    if (remainTotalAmount < unitPriceInstallment.Amount)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceItem.IsPaid = false;
                                        unitPriceInstallment.PaidAmount = remainTotalAmount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = unitPriceInstallment.PaidAmount;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = unitPriceInstallment.Amount - remainTotalAmount;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = unitPriceInstallment.Amount;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                        remainAmount = remainTotalAmount;
                                        break;
                                    }
                                }
                                if (remainOldPaymentInstallment == 0)
                                    remainTotalAmount -= unitPriceInstallment.Amount;
                                else
                                    remainTotalAmount -= remainOldPaymentInstallment;
                            }
                        }
                    }
                }
                #endregion

                #region newPaymentItem
                else
                {
                    if (remainAmount != 0)
                    {
                        if (remainAmount >= unitPriceItem.Amount && unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            // Update UnitPrice
                            unitPriceItem.IsPaid = true;
                            //unitPriceItem.PaidAmount = unitPriceItem.Amount;
                            unitPriceItem.PayDate = payment.ReceiveDate;
                            updateUnitPriceItems.Add(unitPriceItem);

                            // Create PaymentItem
                            var paymentItem = new PaymentItem();
                            paymentItem.PayAmount = unitPriceItem.Amount;
                            paymentItem.PaymentID = payment.ID;
                            paymentItem.RemainAmount = 0;
                            paymentItem.UnitPriceItemID = unitPriceItem.ID;
                            paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                            paymentItem.ItemAmount = unitPriceItem.Amount;

                            paymentItems.Add(paymentItem);

                            // Create PaymentMethodToItem
                            var paymentMethodToItem = new PaymentMethodToItem();
                            paymentMethodToItem.PaymentItemID = paymentItem.ID;
                            paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                            paymentMethodToItem.PayAmount = unitPriceItem.Amount;

                            paymentMethodToItems.Add(paymentMethodToItem);
                        }
                        else if (remainAmount < unitPriceItem.Amount && unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                        {
                            // Update UnitPrice
                            //unitPriceItem.PaidAmount = remainAmount;
                            unitPriceItem.IsPaid = false;
                            unitPriceItem.PayDate = payment.ReceiveDate;
                            updateUnitPriceItems.Add(unitPriceItem);

                            // Create PaymentItem
                            var paymentItem = new PaymentItem();
                            paymentItem.PayAmount = remainAmount;
                            paymentItem.PaymentID = payment.ID;
                            paymentItem.UnitPriceItemID = unitPriceItem.ID;
                            paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                            paymentItem.ItemAmount = unitPriceItem.Amount;
                            paymentItem.RemainAmount = unitPriceItem.Amount - remainAmount;


                            paymentItems.Add(paymentItem);

                            // Create PaymentMethodToItem
                            var paymentMethodToItem = new PaymentMethodToItem();
                            paymentMethodToItem.PaymentItemID = paymentItem.ID;
                            paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                            paymentMethodToItem.PayAmount = remainAmount;

                            paymentMethodToItems.Add(paymentMethodToItem);
                            break;
                        }
                        else if (remainAmount >= 0 && unitPriceItem.MasterPriceItemID == MasterPriceItemKeys.DownAmount)
                        {
                            var unitPriceInstallments = await DB.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItem.ID && o.IsPaid != true).OrderBy(o => o.Period).ToListAsync();
                            var remainTotalAmount = remainAmount;

                            foreach (var unitPriceInstallment in unitPriceInstallments)
                            {
                                var remainOldPaymentInstallment = unitPriceInstallment.Amount - unitPriceInstallment.PaidAmount;
                                if (remainOldPaymentInstallment != 0)
                                {
                                    if (remainTotalAmount >= remainOldPaymentInstallment)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceInstallment.IsPaid = true;
                                        unitPriceInstallment.PaidAmount = remainOldPaymentInstallment;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = remainOldPaymentInstallment;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = 0;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItem.ItemAmount = remainOldPaymentInstallment;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                    }
                                    if (remainTotalAmount < remainOldPaymentInstallment)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceItem.IsPaid = false;
                                        unitPriceInstallment.PaidAmount = remainTotalAmount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = unitPriceInstallment.PaidAmount;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = remainOldPaymentInstallment - remainTotalAmount;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = remainOldPaymentInstallment;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;

                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                        remainAmount = remainTotalAmount;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (remainTotalAmount >= unitPriceInstallment.Amount)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceInstallment.IsPaid = true;
                                        unitPriceInstallment.PaidAmount = unitPriceInstallment.Amount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = unitPriceInstallment.PaidAmount;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = 0;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = unitPriceInstallment.Amount;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                    }
                                    if (remainTotalAmount < unitPriceInstallment.Amount)
                                    {
                                        // Update UnitPriceInstallment
                                        unitPriceItem.IsPaid = false;
                                        unitPriceInstallment.PaidAmount = remainTotalAmount;
                                        unitPriceInstallment.PayDate = payment.ReceiveDate;
                                        updateUnitPriceInstallments.Add(unitPriceInstallment);

                                        // Create PaymentItem
                                        var paymentItem = new PaymentItem();
                                        paymentItem.PayAmount = unitPriceInstallment.PaidAmount;
                                        paymentItem.PaymentID = payment.ID;
                                        paymentItem.RemainAmount = unitPriceInstallment.Amount - remainTotalAmount;
                                        paymentItem.UnitPriceItemID = unitPriceItem.ID;
                                        paymentItem.ItemAmount = unitPriceInstallment.Amount;
                                        paymentItem.UnitPriceInstallmentID = unitPriceInstallment.ID;
                                        paymentItem.MasterPriceItemID = unitPriceItem.MasterPriceItemID;
                                        paymentItems.Add(paymentItem);

                                        // Create PaymentMethodToItem
                                        var paymentMethodToItem = new PaymentMethodToItem();
                                        paymentMethodToItem.PaymentItemID = paymentItem.ID;
                                        paymentMethodToItem.PaymentMethodID = paymentMethod.ID;
                                        paymentMethodToItem.PayAmount = remainTotalAmount;

                                        paymentMethodToItems.Add(paymentMethodToItem);
                                        remainAmount = remainTotalAmount;
                                        break;
                                    }
                                }
                                if (remainOldPaymentInstallment == 0)
                                    remainTotalAmount -= unitPriceInstallment.Amount;
                                else
                                    remainTotalAmount -= remainOldPaymentInstallment;
                            }
                        }
                    }
                }
                #endregion

                if (unitPriceItem.MasterPriceItemID != MasterPriceItemKeys.DownAmount)
                    remainAmount -= remainOldPayment == null ? unitPriceItem.Amount : remainOldPayment.RemainAmount;
            }

            DB.UnitPriceItems.UpdateRange(updateUnitPriceItems);
            DB.UnitPriceInstallments.UpdateRange(updateUnitPriceInstallments);
            await DB.SaveChangesAsync();


            return new PaymentLists
            {
                PaymentItems = paymentItems,
                PaymentMethodToItems = paymentMethodToItems
            };
        }
    }
}


