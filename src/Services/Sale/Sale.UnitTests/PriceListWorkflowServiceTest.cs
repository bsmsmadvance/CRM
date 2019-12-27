using AutoFixture;
using Base.DTOs.SAL.Sortings;
using CustomAutoFixture;
using Database.Models;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class PriceListWorkflowServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public PriceListWorkflowServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void CreatePriceListWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var priceListWorkflowService = new PriceListWorkflowService(db);
                        var service = new QuotationService(priceListWorkflowService, Configuration, db);
                        var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

                        #region Quotation


                        Quotation quotation = new Quotation()
                        {
                            QuotationNo = "QT1111111",
                            ProjectID = project.ID,
                            IssueDate = DateTime.Now,
                            QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                            UnitID = unit.ID
                        };

                        await db.Quotations.AddAsync(quotation);

                        QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        {
                            FromPriceListID = priceList.ID,
                            QuotationID = quotation.ID,
                        };

                        List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        {
                            // DownAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = /*priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()*/15,
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // ContractAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // Booking
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // NetSellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // SellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                        };
                        await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                        #endregion

                        #region Promotion
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                           .FirstAsync();

                        MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        {
                            PromotionNo = "PPTest0001",
                            ProjectID = project.ID,
                            PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        };

                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                            {
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000001",
                                ItemNo = "I-00001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                Price = 20000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            },
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000002",
                                ItemNo = "I-00002",
                                MaterialCode = "M-00002",
                                NameTH = "ไทย2",
                                NameEN = "ENG2",
                                Price = 30000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            }
                            };

                        List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                            {
                            new MasterBookingPromotionItem
                            {
                                MasterBookingPromotionID = masterBookingPromotion.ID,
                                PromotionMaterialItemID = promotionMaterials[0].ID,
                                Quantity = 1,
                                AgreementNo = "000001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                UnitTH = "หน่วย",
                                UnitEN = "Unit"
                            }
                            };
                        var submasterBookingItem = new MasterBookingPromotionItem
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            PromotionMaterialItemID = promotionMaterials[1].ID,
                            Quantity = 1,
                            AgreementNo = "000001",
                            MaterialCode = "M-00001",
                            NameTH = "ไทย",
                            NameEN = "ENG",
                            UnitTH = "หน่วย",
                            UnitEN = "Unit",
                            MainPromotionItemID = masterBookingPromotionItems[0].ID
                        };
                        masterBookingPromotionItems.Add(submasterBookingItem);

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);


                        var quotationBookingPromotion = new QuotationBookingPromotion()
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            QuotationID = quotation.ID
                        };

                        var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                            {
                                new QuotationBookingPromotionItem
                                {
                                    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                                    MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                                    Quantity = 1,
                                }
                            };
                        var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        {
                            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                            MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                            Quantity = 1,
                            MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        };
                        quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                        #endregion


                        await db.SaveChangesAsync();

                        var resultQuotation = await db.Quotations.FirstOrDefaultAsync();

                        await service.ConvertToBookingAsync(resultQuotation.ID);

                        var booking = await db.Bookings.FirstOrDefaultAsync();
                        var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        var priceListWorkflow = await db.PriceListWorkflows.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPriceListWorkflowListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PriceListWorkflowService(db);

                        PriceListWorkflowFilter filter = FixtureFactory.Get().Build<PriceListWorkflowFilter>().Create();
                        filter.UnitStatusKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus").Select(o => o.Key).FirstAsync();
                        filter.PriceListWorkflowStageKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PriceListWorkflowStage").Select(o => o.Key).FirstAsync();
                        PageParam pageParam = new PageParam();
                        PriceListWorkflowSortByParam sortByParam = new PriceListWorkflowSortByParam();

                        var results = await service.GetPriceListWorkflowListAsync(filter, pageParam, sortByParam);

                        filter = new PriceListWorkflowFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(PriceListWorkflowSortBy)).Cast<PriceListWorkflowSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new PriceListWorkflowSortByParam() { SortBy = item };
                            results = await service.GetPriceListWorkflowListAsync(filter, pageParam, sortByParam);
                        }
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPriceListWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var priceListWorkflowService = new PriceListWorkflowService(db);
                        var service = new QuotationService(priceListWorkflowService, Configuration, db);
                        var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

                        #region Quotation


                        Quotation quotation = new Quotation()
                        {
                            QuotationNo = "QT1111111",
                            ProjectID = project.ID,
                            IssueDate = DateTime.Now,
                            QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                            UnitID = unit.ID
                        };

                        await db.Quotations.AddAsync(quotation);

                        QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        {
                            FromPriceListID = priceList.ID,
                            QuotationID = quotation.ID,
                        };

                        List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        {
                            // DownAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = /*priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()*/15,
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // ContractAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // Booking
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // NetSellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // SellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                        };
                        await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                        #endregion

                        #region Promotion
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                           .FirstAsync();

                        MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        {
                            PromotionNo = "PPTest0001",
                            ProjectID = project.ID,
                            PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        };

                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                            {
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000001",
                                ItemNo = "I-00001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                Price = 20000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            },
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000002",
                                ItemNo = "I-00002",
                                MaterialCode = "M-00002",
                                NameTH = "ไทย2",
                                NameEN = "ENG2",
                                Price = 30000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            }
                            };

                        List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                            {
                            new MasterBookingPromotionItem
                            {
                                MasterBookingPromotionID = masterBookingPromotion.ID,
                                PromotionMaterialItemID = promotionMaterials[0].ID,
                                Quantity = 1,
                                AgreementNo = "000001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                UnitTH = "หน่วย",
                                UnitEN = "Unit"
                            }
                            };
                        var submasterBookingItem = new MasterBookingPromotionItem
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            PromotionMaterialItemID = promotionMaterials[1].ID,
                            Quantity = 1,
                            AgreementNo = "000001",
                            MaterialCode = "M-00001",
                            NameTH = "ไทย",
                            NameEN = "ENG",
                            UnitTH = "หน่วย",
                            UnitEN = "Unit",
                            MainPromotionItemID = masterBookingPromotionItems[0].ID
                        };
                        masterBookingPromotionItems.Add(submasterBookingItem);

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);


                        var quotationBookingPromotion = new QuotationBookingPromotion()
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            QuotationID = quotation.ID
                        };

                        var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                            {
                                new QuotationBookingPromotionItem
                                {
                                    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                                    MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                                    Quantity = 1,
                                }
                            };
                        var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        {
                            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                            MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                            Quantity = 1,
                            MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        };
                        quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                        #endregion

                        await db.SaveChangesAsync();

                        var resultQuotation = await db.Quotations.FirstOrDefaultAsync();

                        await service.ConvertToBookingAsync(resultQuotation.ID);

                        var booking = await db.Bookings.FirstOrDefaultAsync();
                        var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        var priceListWorkflow = await db.PriceListWorkflows.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();

                        var priceListWorkFlowDTO = await priceListWorkflowService.GetPriceListWorkflowAsync(priceListWorkflow.ID);
                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void ApproveAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var priceListWorkflowService = new PriceListWorkflowService(db);
                        var service = new QuotationService(priceListWorkflowService, Configuration, db);
                        var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();
                        var quatationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First();

                    #region Quotation


                    Quotation quotation = new Quotation()
                        {
                            QuotationNo = "QT1111111",
                            ProjectID = project.ID,
                            IssueDate = DateTime.Now,
                            QuotationStatusMasterCenterID = quatationStatusMasterCenterID,
                            UnitID = unit.ID
                        };

                        await db.Quotations.AddAsync(quotation);

                        QuotationUnitPrice quotationUnitPrice = new QuotationUnitPrice()
                        {
                            FromPriceListID = priceList.ID,
                            QuotationID = quotation.ID,
                        };

                        List<QuotationUnitPriceItem> quotationUnitPriceItems = new List<QuotationUnitPriceItem>()
                        {
                            // DownAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = /*priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()*/15,
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // ContractAmount
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // Booking
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // NetSellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                            // SellPrice
                            new QuotationUnitPriceItem
                            {
                                QuotationUnitPriceID = quotationUnitPrice.ID,
                                SpecialInstallmentAmounts = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                SpecialInstallments = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallments).FirstOrDefault(),
                                Name = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Name).FirstOrDefault(),
                                Amount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault(),
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Installment).FirstOrDefault(),
                                InstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.InstallmentAmount).FirstOrDefault(),
                                MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                                PriceTypeMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceTypeMasterCenterID).FirstOrDefault(),
                                PricePerUnitAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PricePerUnitAmount).FirstOrDefault(),
                                PriceUnitMasterCenterID = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.PriceUnitMasterCenterID).FirstOrDefault(),
                            },
                        };
                        await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);
                    #endregion

                    #region Promotion
                    var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                   .FirstAsync();

                        MasterBookingPromotion masterBookingPromotion = new MasterBookingPromotion()
                        {
                            PromotionNo = "PPTest0001",
                            ProjectID = project.ID,
                            PromotionStatusMasterCenterID = promotionStatusMasterCenterID.ID
                        };

                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                            {
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000001",
                                ItemNo = "I-00001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                Price = 20000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            },
                            new PromotionMaterialItem
                            {
                                AgreementNo = "000002",
                                ItemNo = "I-00002",
                                MaterialCode = "M-00002",
                                NameTH = "ไทย2",
                                NameEN = "ENG2",
                                Price = 30000,
                                UnitTH = "หน่วย",
                                UnitEN = "Unit",
                                IsDeleted = false,
                            }
                            };

                        List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                            {
                            new MasterBookingPromotionItem
                            {
                                MasterBookingPromotionID = masterBookingPromotion.ID,
                                PromotionMaterialItemID = promotionMaterials[0].ID,
                                Quantity = 1,
                                AgreementNo = "000001",
                                MaterialCode = "M-00001",
                                NameTH = "ไทย",
                                NameEN = "ENG",
                                UnitTH = "หน่วย",
                                UnitEN = "Unit"
                            }
                            };
                        var submasterBookingItem = new MasterBookingPromotionItem
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            PromotionMaterialItemID = promotionMaterials[1].ID,
                            Quantity = 1,
                            AgreementNo = "000001",
                            MaterialCode = "M-00001",
                            NameTH = "ไทย",
                            NameEN = "ENG",
                            UnitTH = "หน่วย",
                            UnitEN = "Unit",
                            MainPromotionItemID = masterBookingPromotionItems[0].ID
                        };
                        masterBookingPromotionItems.Add(submasterBookingItem);

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);


                        var quotationBookingPromotion = new QuotationBookingPromotion()
                        {
                            MasterBookingPromotionID = masterBookingPromotion.ID,
                            QuotationID = quotation.ID
                        };

                        var quotationBookingPromotionItems = new List<QuotationBookingPromotionItem>()
                            {
                                new QuotationBookingPromotionItem
                                {
                                    QuotationBookingPromotionID = quotationBookingPromotion.ID,
                                    MasterBookingPromotionItemID = masterBookingPromotionItems[0].ID,
                                    Quantity = 1,
                                }
                            };
                        var subquotationBookingPromotionItem = new QuotationBookingPromotionItem
                        {
                            QuotationBookingPromotionID = quotationBookingPromotion.ID,
                            MasterBookingPromotionItemID = masterBookingPromotionItems[1].ID,
                            Quantity = 1,
                            MainQuotationBookingPromotionID = quotationBookingPromotionItems[0].ID
                        };
                        quotationBookingPromotionItems.Add(subquotationBookingPromotionItem);

                        await db.QuotationBookingPromotions.AddAsync(quotationBookingPromotion);
                        await db.QuotationBookingPromotionItems.AddRangeAsync(quotationBookingPromotionItems);

                    #endregion


                    await db.SaveChangesAsync();

                        var resultQuotation = await db.Quotations.Where(o => o.QuotationNo == "QT1111111").FirstOrDefaultAsync();

                        await service.ConvertToBookingAsync(resultQuotation.ID);

                        var booking = await db.Bookings.FirstOrDefaultAsync();
                        var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();
                        var priceListWorkflow = await db.PriceListWorkflows.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();

                        var userRole = await db.UserRoles.Include(o => o.Role).Where(o => o.Role.Code == "LCM").FirstOrDefaultAsync();

                        await priceListWorkflowService.ApproveAsync(priceListWorkflow.ID, (Guid)userRole.UserID);


                        var priceListWorkflowUpdate = await db.PriceListWorkflows.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var newPriceList = await db.PriceLists.Where(o => o.UnitID == quotation.UnitID && o.ActiveDate <= DateTime.Now).OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();
                        var newPirceListItem = await db.PriceListItems.Where(o => o.PriceListID == newPriceList.ID).ToListAsync();
                        tran.Rollback();
                    }
                });
            }
        }

    }
}
