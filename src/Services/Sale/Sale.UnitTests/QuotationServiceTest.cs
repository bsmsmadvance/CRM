using AutoFixture;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using CustomAutoFixture;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class QuotationServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public QuotationServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetQuotationListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Quotation quotation = new Quotation()
                        {
                            QuotationNo = "QT12345678",
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                            IssueDate = DateTime.Now,
                            QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                            UnitID = db.Units.Select(o => o.ID).First()
                        };

                        await db.Quotations.AddAsync(quotation);
                        await db.SaveChangesAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        QuotationListFilter filter = new QuotationListFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        QuotationListSortByParam sortByParam = new QuotationListSortByParam();
                        var results = await service.GetQuotationListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<QuotationListFilter>().Create();
                        results = await service.GetQuotationListAsync(filter, pageParam, sortByParam);

                        filter = new QuotationListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(QuotationListSortBy)).Cast<QuotationListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new QuotationListSortByParam() { SortBy = item };
                            results = await service.GetQuotationListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetQuotationAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Quotation quotation = new Quotation()
                        {
                            QuotationNo = "QT12345678",
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                            IssueDate = DateTime.Now,
                            QuotationStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "QuotationStatus").Select(o => o.ID).First(),
                            UnitID = db.Units.Select(o => o.ID).First()
                        };

                        await db.Quotations.AddAsync(quotation);
                        await db.SaveChangesAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetQuotationAsync(quotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBookingPromotionDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("3CDBD0BA-5B9B-4ADC-AA3B-0059CDB91D59")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetBookingPromotionDraftAsync(unit.ID);

                        var filter = FixtureFactory.Get().Build<QuotationBookingPromotionFilter>().Create();
                        results = await service.GetBookingPromotionDraftAsync(unit.ID, filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("AFD9B122-4383-4CE1-AF07-0364E4DB3D42")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetTransferPromotionDraftAsync(unit.ID);

                        var filter = FixtureFactory.Get().Build<QuotationTransferPromotionFilter>().Create();
                        results = await service.GetTransferPromotionDraftAsync(unit.ID, filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPromotionExpensesDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("AFD9B122-4383-4CE1-AF07-0364E4DB3D42")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetPromotionExpensesDraftAsync(unit.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPriceListDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("2f5ae39f-dd8a-4f30-91a9-210cb7032f57")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetPriceListDraftAsync(unit.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateQuotationAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("2f5ae39f-dd8a-4f30-91a9-210cb7032f57")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var booking = await service.GetBookingPromotionDraftAsync(unit.ID);
                        var tranfer = await service.GetTransferPromotionDraftAsync(unit.ID);
                        var expense = await service.GetPromotionExpensesDraftAsync(unit.ID);

                        var expenseDTO = new List<QuotationPromotionExpenseDTO>();
                        var reponsibleBy = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == "2").FirstAsync();
                        foreach (var item in expense)
                        {
                            item.ExpenseReponsibleBy = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            item.ExpenseReponsibleBy.Id = reponsibleBy.ID;
                            item.ExpenseReponsibleBy.Key = reponsibleBy.Key;
                            item.ExpenseReponsibleBy.Name = reponsibleBy.Name;
                            expenseDTO.Add(item);
                        }

                        var pricelist = await service.GetPriceListDraftAsync(unit.ID);
                        pricelist.CashDiscount = 90000.00M;
                        pricelist.TransferDiscount = 80000.00M;
                        pricelist.DownAmount = 10000.00M;
                        pricelist.Installment = 4;
                        pricelist.InstallmentAmount = 2000;
                        pricelist.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>()
                        {
                            new SpecialInstallmentDTO()
                            {
                                Period = 2,
                                Amount = 3000
                            },
                            new SpecialInstallmentDTO()
                            {
                                Period = 3,
                                Amount = 3000
                            }
                        };

                        var result = await service.CreateQuotationAsync(unit.ID, pricelist, booking, tranfer, expenseDTO);
                        var priceListResult = await service.GetPriceListAsync(result.Id.Value);
                        var bookingResult = await service.GetBookingPromotionAsync(result.Id.Value);
                        var transferResult = await service.GetTransferPromotionAsync(result.Id.Value);
                        var expenseResult = await service.GetPromotionExpensesAsync(result.Id.Value);

                        Trace.WriteLine(JsonConvert.SerializeObject(result));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void SaveQuotationAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var quotation = await db.Quotations.FirstOrDefaultAsync();

                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var newPriceList = await service.GetPriceListAsync(quotation.ID);
                        var newBooking = await service.GetBookingPromotionAsync(quotation.ID);
                        var newTransfer = await service.GetTransferPromotionAsync(quotation.ID);
                        var newExpense = await service.GetPromotionExpensesAsync(quotation.ID);

                        var result = await service.SaveQuotationAsync(quotation.ID, newPriceList, newBooking, newTransfer, newExpense);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteQuotationAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var unit = await db.Units.Where(o => o.ID == new Guid("AFD9B122-4383-4CE1-AF07-0364E4DB3D42")).FirstAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var booking = await service.GetBookingPromotionDraftAsync(unit.ID);
                        var tranfer = await service.GetTransferPromotionDraftAsync(unit.ID);
                        var expense = await service.GetPromotionExpensesDraftAsync(unit.ID);

                        var expenseDTO = new List<QuotationPromotionExpenseDTO>();
                        var reponsibleBy = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ExpenseReponsibleBy" && o.Key == "2").FirstAsync();
                        foreach (var item in expense)
                        {
                            item.ExpenseReponsibleBy = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            item.ExpenseReponsibleBy.Id = reponsibleBy.ID;
                            item.ExpenseReponsibleBy.Key = reponsibleBy.Key;
                            item.ExpenseReponsibleBy.Name = reponsibleBy.Name;
                            expenseDTO.Add(item);
                        }
                        var pricelist = await service.GetPriceListDraftAsync(unit.ID);
                        pricelist.CashDiscount = 2000000.00M;
                        var quotation = await service.CreateQuotationAsync(unit.ID, pricelist, booking, tranfer, expenseDTO);

                        await service.DeleteQuotationAsync(quotation.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var quotation = await db.Quotations.FirstOrDefaultAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetPriceListAsync(quotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBookingPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var quotation = await db.Quotations.FirstAsync(o => o.ID == new Guid("988253A0-73B9-4881-A8E0-98AE311AC8F0"));

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetBookingPromotionAsync(quotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var quotation = await db.Quotations.FirstAsync(o => o.ID == new Guid("97731852-3e39-434f-abda-dc157411d252"));

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetTransferPromotionAsync(quotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPromotionExpensesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var quotation = await db.Quotations.FirstOrDefaultAsync();

                        // Act
                        var plwService = new PriceListWorkflowService(db);
                        var service = new QuotationService(plwService, Configuration, db);
                        var results = await service.GetPromotionExpensesAsync(quotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void IsPriceListChangedAsync()
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
                        var service = new QuotationService(plwService, Configuration, db);

                        var project = db.Projects.Where(o => o.ProjectNo == "60016").First();
                        var unit = db.Units.Where(o => o.ProjectID == (Guid?)project.ID && o.UnitNo == "A12AC137").First();
                        var priceList = db.PriceLists.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unit.ID).OrderByDescending(o => o.ActiveDate).FirstOrDefault();
                        var priceListItems = db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToList();

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
                                    SpecialInstallmentAmounts =  priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                    SpecialInstallments = priceListItems.Where(o=>o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o=>o.SpecialInstallments).FirstOrDefault(),
                                    Name = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.Name).FirstOrDefault(),
                                    Amount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.Amount).FirstOrDefault(),
                                    Installment = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.Installment).FirstOrDefault(),
                                    InstallmentAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.InstallmentAmount).FirstOrDefault(),
                                    MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                                    PriceTypeMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.PriceTypeMasterCenterID).FirstOrDefault(),
                                    PricePerUnitAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.PricePerUnitAmount).FirstOrDefault(),
                                    PriceUnitMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.DownAmount).Select(o=>o.PriceUnitMasterCenterID).FirstOrDefault(),
                                },
                                // ContractAmount
                                new QuotationUnitPriceItem
                                {
                                    QuotationUnitPriceID = quotationUnitPrice.ID,
                                    SpecialInstallmentAmounts =  priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                    SpecialInstallments = priceListItems.Where(o=>o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o=>o.SpecialInstallments).FirstOrDefault(),
                                    Name = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.Name).FirstOrDefault(),
                                    Amount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.Amount).FirstOrDefault(),
                                    Installment = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.Installment).FirstOrDefault(),
                                    InstallmentAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.InstallmentAmount).FirstOrDefault(),
                                    MasterPriceItemID = MasterPriceItemKeys.ContractAmount,
                                    PriceTypeMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.PriceTypeMasterCenterID).FirstOrDefault(),
                                    PricePerUnitAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.PricePerUnitAmount).FirstOrDefault(),
                                    PriceUnitMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.ContractAmount).Select(o=>o.PriceUnitMasterCenterID).FirstOrDefault(),
                                },
                                // Booking
                                new QuotationUnitPriceItem
                                {
                                    QuotationUnitPriceID = quotationUnitPrice.ID,
                                    SpecialInstallmentAmounts =  priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                    SpecialInstallments = priceListItems.Where(o=>o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o=>o.SpecialInstallments).FirstOrDefault(),
                                    Name = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.Name).FirstOrDefault(),
                                    Amount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.Amount).FirstOrDefault(),
                                    Installment = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.Installment).FirstOrDefault(),
                                    InstallmentAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.InstallmentAmount).FirstOrDefault(),
                                    MasterPriceItemID = MasterPriceItemKeys.BookingAmount,
                                    PriceTypeMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.PriceTypeMasterCenterID).FirstOrDefault(),
                                    PricePerUnitAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.PricePerUnitAmount).FirstOrDefault(),
                                    PriceUnitMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.BookingAmount).Select(o=>o.PriceUnitMasterCenterID).FirstOrDefault(),
                                },
                                // NetSellPrice
                                new QuotationUnitPriceItem
                                {
                                    QuotationUnitPriceID = quotationUnitPrice.ID,
                                    SpecialInstallmentAmounts =  priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                    SpecialInstallments = priceListItems.Where(o=>o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o=>o.SpecialInstallments).FirstOrDefault(),
                                    Name = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.Name).FirstOrDefault(),
                                    Amount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.Amount).FirstOrDefault(),
                                    Installment = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.Installment).FirstOrDefault(),
                                    InstallmentAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.InstallmentAmount).FirstOrDefault(),
                                    MasterPriceItemID = MasterPriceItemKeys.NetSellPrice,
                                    PriceTypeMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.PriceTypeMasterCenterID).FirstOrDefault(),
                                    PricePerUnitAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.PricePerUnitAmount).FirstOrDefault(),
                                    PriceUnitMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.NetSellPrice).Select(o=>o.PriceUnitMasterCenterID).FirstOrDefault(),
                                },
                                 // SellPrice
                                new QuotationUnitPriceItem
                                {
                                    QuotationUnitPriceID = quotationUnitPrice.ID,
                                    SpecialInstallmentAmounts =  priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault(),
                                    SpecialInstallments = priceListItems.Where(o=>o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o=>o.SpecialInstallments).FirstOrDefault(),
                                    Name = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.Name).FirstOrDefault(),
                                    Amount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.Amount).FirstOrDefault(),
                                    Installment = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.Installment).FirstOrDefault(),
                                    InstallmentAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.InstallmentAmount).FirstOrDefault(),
                                    MasterPriceItemID = MasterPriceItemKeys.SellPrice,
                                    PriceTypeMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.PriceTypeMasterCenterID).FirstOrDefault(),
                                    PricePerUnitAmount = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.PricePerUnitAmount).FirstOrDefault(),
                                    PriceUnitMasterCenterID = priceListItems.Where(o=>o.MasterPriceItemID==MasterPriceItemKeys.SellPrice).Select(o=>o.PriceUnitMasterCenterID).FirstOrDefault(),
                                },
                            };
                        await db.QuotationUnitPrices.AddAsync(quotationUnitPrice);
                        await db.QuotationUnitPriceItems.AddRangeAsync(quotationUnitPriceItems);

                        await db.SaveChangesAsync();

                        var resultQuotation = await db.Quotations.FirstOrDefaultAsync();

                        //var result = await service.IsPriceListChangedAsync(resultQuotation.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ConvertToBookingAsync()
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
                        var service = new QuotationService(plwService, Configuration, db);

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
                                Installment = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault(),
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

                        var booking = await db.Bookings.Where(o => o.QuotationID == quotation.ID).FirstOrDefaultAsync();
                        var bookingPromotion = await db.BookingPromotions.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var bookingPromotionItems = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotion.ID).ToListAsync();
                        var unitPrice = await db.UnitPrices.Where(o => o.BookingID == booking.ID).FirstOrDefaultAsync();
                        var unitPriceItems = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
                        var unitPriceInstallments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == unitPriceItems.Where(p => p.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(p => p.ID).FirstOrDefault()).ToListAsync();

                        //await service.ConvertToBookingAsync(Guid.Parse("7c5c62bc-7d1a-4cb5-83ab-dc9337966790"));

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
