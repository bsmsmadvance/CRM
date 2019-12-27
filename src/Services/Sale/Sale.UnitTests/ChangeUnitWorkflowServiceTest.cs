using AutoFixture;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using CustomAutoFixture;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Finance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sale.Params.Filters;
using Sale.Services;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class ChangeUnitWorkflowServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public ChangeUnitWorkflowServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetBookingChangeUnitWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var paymentService = new PaymentService(this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var service = new ChangeUnitWorkflowService(this.Configuration, db, quotationService, bookingService, paymentService);

                        //var changeUnitWorkflow = FixtureFactory.Get().Build<ChangeUnitWorkflow>()
                        //                         .Create();

                        //await db.ChangeUnitWorkflows.AddAsync(changeUnitWorkflow);
                        //await db.SaveChangesAsync();

                        //var result = await service.GetBookingChangeUnitWorkflowAsync((Guid)changeUnitWorkflow.FromBookingID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBookingChangeUnitWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var paymentService = new PaymentService(this.Configuration, db);
                        var minPriceBudgetWf = new MinPriceBudgetWorkflowService(db, paymentService,bookingService);
                        var service = new ChangeUnitWorkflowService(this.Configuration, db, quotationService, bookingService, paymentService);

                        //var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();
                        //var unit = await db.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == "S21B17").FirstAsync();
                        //var booking = await db.Bookings.Where(o => o.ID == new Guid("C959460A-D4F2-49B2-8533-664204DDA641")).FirstAsync();
                        //var pricelist = await quotationService.GetPriceListDraftAsync(unit.ID);
                        //var bookingPromotions = await quotationService.GetBookingPromotionDraftAsync(unit.ID);
                        //var transferPromotions = await quotationService.GetTransferPromotionDraftAsync(unit.ID);
                        //var expensePromotions = await quotationService.GetPromotionExpensesDraftAsync(unit.ID);


                        //var result = await service.CreateBookingChangeUnitWorkflowAsync(booking.ID, unit.ID, pricelist, bookingPromotions, transferPromotions, expensePromotions);
                        //var newBooking = await db.Bookings.Where(o => o.QuotationID == result.ToQuotation.Id)
                        //                                  .Include(o => o.Unit)
                        //                                    .ThenInclude(o => o.UnitStatus)
                        //                                  .Include(o => o.BookingStatus)
                        //                                  .FirstAsync();

                        //var oldBooking = await db.Bookings.Where(o => o.ID == result.FromBooking.Id)
                        //                                  .Include(o => o.Unit)
                        //                                    .ThenInclude(o => o.UnitStatus)
                        //                                  .Include(o => o.BookingStatus)
                        //                                  .FirstAsync();
                        //var checkPlwf = await db.PriceListWorkflows.Where(o => o.BookingID == newBooking.ID).FirstOrDefaultAsync();
                        //var checkMnwf = await db.MinPriceBudgetWorkflows.Where(o => o.BookingID == newBooking.ID).FirstOrDefaultAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBookingChangeUnitWorkflowAsyncWithPayments()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var paymentService = new PaymentService(this.Configuration, db);
                        var service = new ChangeUnitWorkflowService(this.Configuration, db, quotationService, bookingService, paymentService);

                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == "N14C24").FirstAsync();
                        var booking = await db.Bookings.Where(o => o.BookingNo == "BK400171910001500").FirstAsync();

                        var paymentMethodCashMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                    && o.Key == PaymentMethodKeys.Cash)
                                                                    .FirstAsync();

                        var resultPayment = await paymentService.GetPaymentFormAsync(booking.ID);
                        resultPayment.ReceiveDate = DateTime.Now;
                        resultPayment.PaymentMethods = new List<PaymentMethodDTO>()
                        {
                            new PaymentMethodDTO
                            {
                                PayAmount= 50000,
                                PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodCashMasterCenter),
                            },
                        };
                        await paymentService.SubmitPaymentFormAsync(booking.ID, resultPayment);

                        var pricelist = await quotationService.GetPriceListDraftAsync(unit.ID);
                        var bookingPromotions = await quotationService.GetBookingPromotionDraftAsync(unit.ID);
                        var transferPromotions = await quotationService.GetTransferPromotionDraftAsync(unit.ID);
                        var expensePromotions = await quotationService.GetPromotionExpensesDraftAsync(unit.ID);


                        var result = await service.CreateBookingChangeUnitWorkflowAsync(booking.ID, unit.ID, pricelist, bookingPromotions, transferPromotions, expensePromotions);
                        var newBooking = await db.Bookings.Where(o => o.QuotationID == result.ToQuotation.Id).FirstAsync();
                        var payments = await db.Payments.Where(o => o.BookingID == newBooking.ID).ToListAsync();
                        var resultNewPayment = await paymentService.GetPaymentFormAsync(newBooking.ID);

                        var paymentChangeUnit = await db.PaymentChangeUnits.ToListAsync();
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
