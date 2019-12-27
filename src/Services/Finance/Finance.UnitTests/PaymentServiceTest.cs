using AutoFixture;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Finance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sale.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Finance.UnitTests
{
    public class PaymentServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public PaymentServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        [Fact]
        public async void GetPaymentFormAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var plwService = new PriceListWorkflowService(db);
                        var serviceQuotation = new QuotationService(plwService, Configuration, db);
                        var service = new PaymentService(Configuration, db);

                        //var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        //var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        //var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        //var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

                        //#region Quotation


                        //Quotation quotation = new Quotation()
                        //{
                        //    QuotationNo = "QT1111111",
                        //    ProjectID = project.ID,
                        //    IssueDate = DateTime.Now,
                        //    QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                        //    UnitID = unit.ID
                        //};

                        //await db.Quotations.AddAsync(quotation);

                        //QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        //{
                        //    FromPriceListID = priceList.ID,
                        //    QuotationID = quotation.ID,
                        //};

                        //List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        //{
                        //    // DownAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // ContractAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // Booking
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // NetSellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // SellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //};
                        //await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        //await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                        //#endregion

                        //#region Promotion
                        //var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                        //                   .FirstAsync();

                        //MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        //{
                        //    PromotionNo = "PPTest0001",
                        //    ProjectID = project.ID,
                        //    PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        //};

                        //List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        //    {
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000001",
                        //        ItemNo = "I-00001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        Price = 20000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    },
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000002",
                        //        ItemNo = "I-00002",
                        //        MaterialCode = "M-00002",
                        //        NameTH = "ไทย2",
                        //        NameEN = "ENG2",
                        //        Price = 30000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    }
                        //    };

                        //List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                        //    {
                        //    new MasterBookingPromotionItem
                        //    {
                        //        MasterBookingPromotionID = masterBookingPromotion.ID,
                        //        PromotionMaterialItemID = promotionMaterials[0].ID,
                        //        Quantity = 1,
                        //        AgreementNo = "000001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit"
                        //    }
                        //    };
                        //var submasterBookingItem = new MasterBookingPromotionItem
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    PromotionMaterialItemID = promotionMaterials[1].ID,
                        //    Quantity = 1,
                        //    AgreementNo = "000001",
                        //    MaterialCode = "M-00001",
                        //    NameTH = "ไทย",
                        //    NameEN = "ENG",
                        //    UnitTH = "หน่วย",
                        //    UnitEN = "Unit",
                        //    MainPromotionItemID = masterBookingPromotionItems[0].ID
                        //};
                        //masterBookingPromotionItems.Add(submasterBookingItem);

                        //await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        //await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        //await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);


                        //var quotationBookingPromotion = new QuotationBookingPromotion()
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    QuotationID = quotation.ID
                        //};

                        //var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                        //    {
                        //        new QuotationBookingPromotionItem
                        //        {
                        //            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //            MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                        //            Quantity = 1,
                        //        }
                        //    };
                        //var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        //{
                        //    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //    MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                        //    Quantity = 1,
                        //    MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        //};
                        //quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        //await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        //await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                        //#endregion


                        //await db.SaveChangesAsync();

                        //var resultQuotation = await db.Quotations.Where(o => o.QuotationNo == "QT1111111").FirstOrDefaultAsync();

                        //await serviceQuotation.ConvertToBookingAsync(resultQuotation.ID);

                        //var booking = await db.Bookings.Where(o => o.QuotationID == quotation.ID).FirstOrDefaultAsync();
                        //var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        //var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        //var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        //var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        //var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        //var sumAmountInstallment = unitPriceInstallments.Sum(o => o.Amount);
                        var resultPayment = await service.GetPaymentFormAsync(new Guid("6134434F-6C06-48E3-AE5F-4456E190F923"), PaymentFormType.UnknownPayment, Guid.NewGuid(), 222);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void SubmitPaymentFormAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var plwService = new PriceListWorkflowService(db);
                        var serviceQuotation = new QuotationService(plwService, Configuration, db);
                        var service = new PaymentService(Configuration, db);

                        //var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        //var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        //var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        //var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

                        //#region Quotation


                        //Quotation quotation = new Quotation()
                        //{
                        //    QuotationNo = "QT1111111",
                        //    ProjectID = project.ID,
                        //    IssueDate = DateTime.Now,
                        //    QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                        //    UnitID = unit.ID
                        //};

                        //await db.Quotations.AddAsync(quotation);

                        //QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        //{
                        //    FromPriceListID = priceList.ID,
                        //    QuotationID = quotation.ID,
                        //};

                        //List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        //{
                        //    // DownAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // ContractAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // Booking
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // NetSellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // SellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //};
                        //await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        //await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                        //#endregion

                        //#region Promotion
                        //var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                        //                   .FirstAsync();

                        //MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        //{
                        //    PromotionNo = "PPTest0001",
                        //    ProjectID = project.ID,
                        //    PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        //};

                        //List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        //    {
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000001",
                        //        ItemNo = "I-00001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        Price = 20000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    },
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000002",
                        //        ItemNo = "I-00002",
                        //        MaterialCode = "M-00002",
                        //        NameTH = "ไทย2",
                        //        NameEN = "ENG2",
                        //        Price = 30000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    }
                        //    };

                        //List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                        //    {
                        //    new MasterBookingPromotionItem
                        //    {
                        //        MasterBookingPromotionID = masterBookingPromotion.ID,
                        //        PromotionMaterialItemID = promotionMaterials[0].ID,
                        //        Quantity = 1,
                        //        AgreementNo = "000001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit"
                        //    }
                        //    };
                        //var submasterBookingItem = new MasterBookingPromotionItem
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    PromotionMaterialItemID = promotionMaterials[1].ID,
                        //    Quantity = 1,
                        //    AgreementNo = "000001",
                        //    MaterialCode = "M-00001",
                        //    NameTH = "ไทย",
                        //    NameEN = "ENG",
                        //    UnitTH = "หน่วย",
                        //    UnitEN = "Unit",
                        //    MainPromotionItemID = masterBookingPromotionItems[0].ID
                        //};
                        //masterBookingPromotionItems.Add(submasterBookingItem);

                        //MasterTransferPromotion masterTransferPromotion = new MasterTransferPromotion()
                        //{
                        //    PromotionNo = "PPTest0001",
                        //    ProjectID = project.ID,
                        //    PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        //};

                        //await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        //await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        //await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);
                        //await db.MasterTransferPromotions.AddAsync(masterTransferPromotion);

                        //var quotationBookingPromotion = new QuotationBookingPromotion()
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    QuotationID = quotation.ID
                        //};

                        //var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                        //    {
                        //        new QuotationBookingPromotionItem
                        //        {
                        //            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //            MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                        //            Quantity = 1,
                        //        }
                        //    };
                        //var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        //{
                        //    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //    MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                        //    Quantity = 1,
                        //    MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        //};
                        //quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        //var quotationTransferPromotion = new QuotationTransferPromotion()
                        //{
                        //    MasterTransferPromotionID = masterTransferPromotion.ID,
                        //    QuotationID = quotation.ID
                        //};

                        //await db.QuotationTransferPromotions.AddAsync(quotationTransferPromotion);
                        //await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        //await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                        //#endregion


                        //await db.SaveChangesAsync();

                        //var resultQuotation = await db.Quotations.Where(o => o.QuotationNo == "QT1111111").FirstOrDefaultAsync();

                        //await serviceQuotation.ConvertToBookingAsync(resultQuotation.ID);

                        //var booking = await db.Bookings.Where(o => o.QuotationID == quotation.ID).FirstOrDefaultAsync();
                        //var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        //var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        //var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstOrDefaultAsync();
                        //var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        //var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        //var sumAmountInstallment = unitPriceInstallments.Sum(o => o.Amount);
                        var resultPayment = await service.GetPaymentFormAsync(new Guid("21A668E2-E408-4A46-9CE3-536FC3DB64AE"));


                        var paymentMethodCashMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                     && o.Key == PaymentMethodKeys.Cash)
                                                                     .FirstAsync();

                        #region CreditCard
                        var paymentMethodCreditCardMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                     && o.Key == PaymentMethodKeys.CreditCard)
                                                                     .FirstAsync();
                        var bank = await db.Banks.FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardPaymentType)
                                                                          .FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardType)
                                                  .FirstAsync();
                        var edc = await db.EDCs.FirstAsync();
                        #endregion

                        #region DebitCard
                        var paymentMethodDebitCardMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                    && o.Key == PaymentMethodKeys.DebitCard)
                                                                    .FirstAsync();
                        #endregion

                        #region PersonalCheque
                        var paymentMethodPersonalChequeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                  && o.Key == PaymentMethodKeys.PersonalCheque)
                                                                  .FirstAsync();
                        var bankBranch = await db.BankBranches.Include(o => o.Bank).FirstAsync();
                        var company = await db.Companies.FirstAsync();
                        #endregion

                        #region CashierCheque
                        var paymentMethodCashierChequeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                && o.Key == PaymentMethodKeys.CashierCheque)
                                                                .FirstAsync();
                        #endregion

                        #region PaymentBankTransferDTO
                        var paymentMethodBankTransferMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                             && o.Key == PaymentMethodKeys.BankTransfer)
                                                             .FirstAsync();
                        var bankAccount = await db.BankAccounts.Where(o => o.BankAccountTypeMasterCenterID == new Guid("387A93AC-8D7E-4E51-9B26-3D8FC2F73A18")).FirstAsync();
                        #endregion

                        #region QRCode
                        var paymentMethodQRCodeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                            && o.Key == PaymentMethodKeys.QRCode)
                                                            .FirstAsync();
                        #endregion

                        #region PaymentForeignBankTransfer
                        var paymentMethodPaymentForeignBankTransferMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                            && o.Key == PaymentMethodKeys.ForeignBankTransfer)
                                                            .FirstAsync();
                        var foreignTransferTypeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ForeignTransferType"
                                                            && o.Key == "1")
                                                            .FirstAsync();
                        #endregion


                        resultPayment.ReceiveDate = DateTime.Now;
                        resultPayment.PaymentMethods = new List<PaymentMethodDTO>()
                            {
                            //new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCashMasterCenter),
                            //},
                             new PaymentMethodDTO
                            {
                                PayAmount= 100,
                                PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCreditCardMasterCenter),
                                CreditCard = new PaymentCreditCardDTO
                                {
                                    IsForeignCreditCard = true,
                                    CardNo= "1234567890123456",
                                    Fee = 5,
                                    Bank = BankDropdownDTO.CreateFromModel(bank),
                                    CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType),
                                    CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType),
                                    EDC = EDCDropdownDTO.CreateFromModel(edc)
                                }
                            },
                            // new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodDebitCardMasterCenter),
                            //    DebitCard = new PaymentDebitCardDTO
                            //    {
                            //        CardNo= "1234567890123456",
                            //        Fee = 5,
                            //        Bank = BankDropdownDTO.CreateFromModel(bank),
                            //        EDC = EDCDropdownDTO.CreateFromModel(edc)
                            //    }
                            //},
                            //new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodPersonalChequeMasterCenter),
                            //    PersonalCheque = new PaymentPersonalChequeDTO
                            //    {
                            //        Bank = BankDropdownDTO.CreateFromModel(bankBranch.Bank),
                            //        ChequeDate = DateTime.Now,
                            //        ChequeNo = "3234234",
                            //        BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranch),
                            //        PayToCompany = CompanyDropdownDTO.CreateFromModel(company)
                            //    }
                            //},
                            //new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCashierChequeMasterCenter),
                            //    CashierCheque = new PaymentCashierChequeDTO
                            //    {
                            //        Bank = BankDropdownDTO.CreateFromModel(bankBranch.Bank),
                            //        ChequeDate = DateTime.Now,
                            //        ChequeNo = "3234234",
                            //        BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranch),
                            //        PayToCompany = CompanyDropdownDTO.CreateFromModel(company)
                            //    }
                            //},
                            // new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodBankTransferMasterCenter),
                            //    BankTransfer=new PaymentBankTransferDTO
                            //    {
                            //        BankAccount = BankAccountDropdownDTO.CreateFromModel(bankAccount),
                            //    }
                            //},
                            //new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodQRCodeMasterCenter),
                            //    QRCode=new PaymentQRCodeDTO
                            //    {
                            //        BankAccount = BankAccountDropdownDTO.CreateFromModel(bankAccount),
                            //    }
                            //},
                            //new PaymentMethodDTO
                            //{
                            //    PayAmount= 100,
                            //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodPaymentForeignBankTransferMasterCenter),
                            //    ForeignBankTransfer=new PaymentForeignBankTransferDTO
                            //    {
                            //        IsRequestFET =true,
                            //        Fee = 5,
                            //        BankAccount = BankAccountDropdownDTO.CreateFromModel(bankAccount),
                            //        ForeignTransferType = MasterCenterDropdownDTO.CreateFromModel(foreignTransferTypeMasterCenter),
                            //        ForeignBank = BankDropdownDTO.CreateFromModel(bank),
                            //        TransferorName = "Test"
                            //    }
                            //},
                        };

                        //for (int i = 0; i < 5; i++)
                        //{
                        //    await service.SubmitPaymentFormAsync(new Guid("026C6EFC-94A6-40B4-BED4-7CF55D4A555E"), resultPayment);
                        //}

                        //var t = await db.ReceiptTempHeaders.ToListAsync();
                        //var b = await db.ReceiptTempDetails.ToListAsync();

                        //var resultPaymentSubmit = await service.GetPaymentFormAsync(booking.ID);

                        //var payments = await db.Payments.Where(o => o.BookingID == new Guid("21A668E2-E408-4A46-9CE3-536FC3DB64AE")).ToListAsync();
                        //var paymentIDs = payments.Select(o => o.ID).ToList();
                        //var paymentItems = await db.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID)).ToListAsync();
                        //var paymentMethods = await db.PaymentMethods.Where(o => paymentIDs.Contains(o.PaymentID)).ToListAsync();
                        //var paymentMethodToItems = await db.PaymentMethodToItems.Where(o => o.PaymentMethodID == paymentMethods[0].ID).ToListAsync();
                        //var paymentCreditCards = await db.PaymentCreditCards.Where(o => o.PaymentMethodID == paymentMethods[0].ID).ToListAsync();

                        //unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstAsync();
                        //unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).Include(o => o.MasterPriceItem).ToListAsync();
                        //unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).OrderBy(o => o.Period).ToListAsync();

                        //await service.SubmitPaymentFormAsync(booking.ID, resultPayment);
                        //var resultPaymentSubmit = await service.GetPaymentFormAsync(new Guid("21A668E2-E408-4A46-9CE3-536FC3DB64AE"));
                        //unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstAsync();
                        //unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).Include(o => o.MasterPriceItem).ToListAsync();
                        //unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).OrderBy(o => o.Period).ToListAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPaymentHistoryListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var plwService = new PriceListWorkflowService(db);
                        var serviceQuotation = new QuotationService(plwService, Configuration, db);
                        var service = new PaymentService(Configuration, db);

                        //var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        //var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        //var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        //var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

                        //#region Quotation


                        //Quotation quotation = new Quotation()
                        //{
                        //    QuotationNo = "QT1111111",
                        //    ProjectID = project.ID,
                        //    IssueDate = DateTime.Now,
                        //    QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                        //    UnitID = unit.ID
                        //};

                        //await db.Quotations.AddAsync(quotation);

                        //QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        //{
                        //    FromPriceListID = priceList.ID,
                        //    QuotationID = quotation.ID,
                        //};

                        //List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        //{
                        //    // DownAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // ContractAmount
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // Booking
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // NetSellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //    // SellPrice
                        //    new QuotationUnitPriceItem
                        //    {
                        //        QuotationUnitPriceID = quotationUnitPrice.ID,
                        //        SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                        //        SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                        //        Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                        //        Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                        //        Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                        //        InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                        //        MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                        //        Order = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Order).FirstOrDefault(),
                        //        PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                        //        PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                        //        PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                        //    },
                        //};
                        //await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        //await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                        //#endregion

                        //#region Promotion
                        //var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                        //                   .FirstAsync();

                        //MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        //{
                        //    PromotionNo = "PPTest0001",
                        //    ProjectID = project.ID,
                        //    PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        //};

                        //List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        //    {
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000001",
                        //        ItemNo = "I-00001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        Price = 20000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    },
                        //    new PromotionMaterialItem
                        //    {
                        //        AgreementNo = "000002",
                        //        ItemNo = "I-00002",
                        //        MaterialCode = "M-00002",
                        //        NameTH = "ไทย2",
                        //        NameEN = "ENG2",
                        //        Price = 30000,
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit",
                        //        IsDeleted = false,
                        //    }
                        //    };

                        //List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                        //    {
                        //    new MasterBookingPromotionItem
                        //    {
                        //        MasterBookingPromotionID = masterBookingPromotion.ID,
                        //        PromotionMaterialItemID = promotionMaterials[0].ID,
                        //        Quantity = 1,
                        //        AgreementNo = "000001",
                        //        MaterialCode = "M-00001",
                        //        NameTH = "ไทย",
                        //        NameEN = "ENG",
                        //        UnitTH = "หน่วย",
                        //        UnitEN = "Unit"
                        //    }
                        //    };
                        //var submasterBookingItem = new MasterBookingPromotionItem
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    PromotionMaterialItemID = promotionMaterials[1].ID,
                        //    Quantity = 1,
                        //    AgreementNo = "000001",
                        //    MaterialCode = "M-00001",
                        //    NameTH = "ไทย",
                        //    NameEN = "ENG",
                        //    UnitTH = "หน่วย",
                        //    UnitEN = "Unit",
                        //    MainPromotionItemID = masterBookingPromotionItems[0].ID
                        //};
                        //masterBookingPromotionItems.Add(submasterBookingItem);

                        //await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        //await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        //await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);


                        //var quotationBookingPromotion = new QuotationBookingPromotion()
                        //{
                        //    MasterBookingPromotionID = masterBookingPromotion.ID,
                        //    QuotationID = quotation.ID
                        //};

                        //var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                        //    {
                        //        new QuotationBookingPromotionItem
                        //        {
                        //            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //            MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                        //            Quantity = 1,
                        //        }
                        //    };
                        //var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        //{
                        //    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                        //    MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                        //    Quantity = 1,
                        //    MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        //};
                        //quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        //await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        //await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                        //#endregion


                        //await db.SaveChangesAsync();

                        //var resultQuotation = await db.Quotations.Where(o => o.QuotationNo == "QT1111111").FirstOrDefaultAsync();

                        //await serviceQuotation.ConvertToBookingAsync(resultQuotation.ID);

                        //var booking = await db.Bookings.Where(o => o.QuotationID == quotation.ID).FirstOrDefaultAsync();
                        //var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        //var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        //var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID && o.IsActive == true).FirstOrDefaultAsync();
                        //var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        //var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        //var sumAmountInstallment = unitPriceInstallments.Sum(o => o.Amount);
                        //var resultPayment = await service.GetPaymentFormAsync(booking.ID);


                        //var paymentMethodCashMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                        //                                             && o.Key == PaymentMethodKeys.Cash)
                        //                                             .FirstAsync();

                        //#region CreditCard
                        //var paymentMethodCreditCardMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                        //                                             && o.Key == PaymentMethodKeys.CreditCard)
                        //                                             .FirstAsync();
                        //var bank = await db.Banks.FirstAsync();
                        //var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardPaymentType)
                        //                                                  .FirstAsync();
                        //var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CreditCardType)
                        //                          .FirstAsync();
                        //var edc = await db.EDCs.FirstAsync();
                        //#endregion

                        //#region DebitCard
                        //var paymentMethodDebitCardMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                        //                                            && o.Key == PaymentMethodKeys.DebitCard)
                        //                                            .FirstAsync();
                        //#endregion

                        //#region PersonalCheque
                        //var paymentMethodPersonalChequeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                        //                                          && o.Key == PaymentMethodKeys.PersonalCheque)
                        //                                          .FirstAsync();
                        //var bankBranch = await db.BankBranches.Include(o => o.Bank).FirstAsync();
                        //var company = await db.Companies.FirstAsync();
                        //#endregion

                        //#region CashierCheque
                        //var paymentMethodCashierChequeMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                        //                                        && o.Key == PaymentMethodKeys.CashierCheque)
                        //                                        .FirstAsync();
                        //#endregion

                        //resultPayment.ReceiveDate = DateTime.Now;
                        //resultPayment.PaymentMethods = new List<PaymentMethodDTO>()
                        //    {
                        //        new PaymentMethodDTO
                        //        {
                        //            PayAmount= 200000,
                        //            PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCashMasterCenter),
                        //        },
                        //        // new PaymentMethodDTO
                        //        //{
                        //        //    PayAmount= 20000,
                        //        //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCreditCardMasterCenter),
                        //        //    CreditCard = new PaymentCreditCardDTO
                        //        //    {
                        //        //        CardNo= "1234567890123456",
                        //        //        Fee = 5,
                        //        //        Bank = BankDropdownDTO.CreateFromModel(bank),
                        //        //        CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType),
                        //        //        CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType),
                        //        //        EDC = EDCDropdownDTO.CreateFromModel(edc)
                        //        //    }
                        //        //},
                        //        // new PaymentMethodDTO
                        //        //{
                        //        //    PayAmount= 5000,
                        //        //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodDebitCardMasterCenter),
                        //        //    DebitCard = new PaymentDebitCardDTO
                        //        //    {
                        //        //        CardNo= "1234567890123456",
                        //        //        Fee = 5,
                        //        //        Bank = BankDropdownDTO.CreateFromModel(bank),
                        //        //        EDC = EDCDropdownDTO.CreateFromModel(edc)
                        //        //    }
                        //        //},
                        //        //new PaymentMethodDTO
                        //        //{
                        //        //    PayAmount= 50000,
                        //        //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodPersonalChequeMasterCenter),
                        //        //    PersonalCheque = new PaymentPersonalChequeDTO
                        //        //    {
                        //        //        Bank = BankDropdownDTO.CreateFromModel(bankBranch.Bank),
                        //        //        ChequeDate = DateTime.Now,
                        //        //        ChequeNo = "3234234",
                        //        //        BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranch),
                        //        //        PayToCompany = CompanyDropdownDTO.CreateFromModel(company)
                        //        //    }
                        //        //},
                        //        //new PaymentMethodDTO
                        //        //{
                        //        //    PayAmount= 50000,
                        //        //    PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCashierChequeMasterCenter),
                        //        //    CashierCheque = new PaymentCashierChequeDTO
                        //        //    {
                        //        //        Bank = BankDropdownDTO.CreateFromModel(bankBranch.Bank),
                        //        //        ChequeDate = DateTime.Now,
                        //        //        ChequeNo = "3234234",
                        //        //        BankBranch = BankBranchDropdownDTO.CreateFromModel(bankBranch),
                        //        //        PayToCompany = CompanyDropdownDTO.CreateFromModel(company)
                        //        //    }
                        //        //},
                        //    };

                        //await service.SubmitPaymentFormAsync(booking.ID, resultPayment);

                        //var resultPaymentSubmit = await service.GetPaymentFormAsync(booking.ID);

                        //var payments = await db.Payments.Where(o => o.BookingID == booking.ID).ToListAsync();
                        //var paymentIDs = payments.Select(o => o.ID).ToList();
                        //var paymentItems = await db.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID)).ToListAsync();
                        //var paymentMethods = await db.PaymentMethods.Where(o => paymentIDs.Contains(o.PaymentID)).ToListAsync();
                        //var paymentMethodToItems = await db.PaymentMethodToItems.Where(o => o.PaymentMethodID == paymentMethods[0].ID).ToListAsync();
                        //var paymentCreditCards = await db.PaymentCreditCards.Where(o => o.PaymentMethodID == paymentMethods[0].ID).ToListAsync();
                        var test = await service.GetPaymentHistoryListAsync(new Guid("026C6EFC-94A6-40B4-BED4-7CF55D4A555E"));
                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void GetPaymentUnitPriceItems()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PaymentService(Configuration, db);
                        var booking = await db.Bookings.Where(o => o.ID == new Guid("6134434F-6C06-48E3-AE5F-4456E190F923")).FirstAsync();
                        var results = await service.GetPaymentUnitPriceItemsAsync(booking.ID);
                        tran.Rollback();
                    }
                });
            }
        }

    }
}
