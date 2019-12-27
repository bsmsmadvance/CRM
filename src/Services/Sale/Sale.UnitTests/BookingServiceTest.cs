using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using CustomAutoFixture;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
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
    public class BookingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public BookingServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetBookingListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        var booking = new Booking()
                        {
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync(),
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                        };
                        var bookingOwner = BookingOwner.CreateFromContact(contact);
                        bookingOwner.BookingID = booking.ID;

                        await db.Bookings.AddAsync(booking);
                        await db.BookingOwners.AddAsync(bookingOwner);
                        await db.SaveChangesAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        BookingListFilter filter = new BookingListFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        BookingListSortByParam sortByParam = new BookingListSortByParam();
                        var results = await service.GetBookingListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<BookingListFilter>().Create();
                        filter.CreateBookingFromKeys = CreateBookingFromKeys.CRM;
                        results = await service.GetBookingListAsync(filter, pageParam, sortByParam);

                        filter = new BookingListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BookingListSortBy)).Cast<BookingListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BookingListSortByParam() { SortBy = item };
                            results = await service.GetBookingListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBookingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var results = await service.GetBookingAsync(bookingId);

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
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var results = await service.GetPriceListAsync(bookingId);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBookingPresalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var results = await service.GetBookingPreSalePromotionAsync(bookingId);

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
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var results = await service.GetBookingPromotionAsync(bookingId);

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
                        var bookingId = await db.Bookings.Select(o => o.ID).FirstAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var results = await service.GetPromotionExpensesAsync(bookingId);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBookingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.OrderBy(o => o.Updated).FirstAsync();
                        var user = await db.Users.FirstAsync();

                        var bookingId = await db.Bookings.Where(o => o.ID == new Guid("5F11F60F-9D1D-472B-8F25-C8B5DEE7152B")).Select(o => o.ID).FirstAsync();
                        var booking = new BookingDTO();
                        booking.BookingDate = DateTime.Today;
                        booking.ContractDueDate = DateTime.Today.AddMonths(1);
                        booking.SaleOfficerType = new MasterCenterDropdownDTO();
                        booking.SaleOfficerType.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SaleOfficerType && o.Key == SaleOfficerTypeKeys.AP).Select(o => o.ID).FirstAsync();
                        booking.ProjectSaleUser = new Base.DTOs.USR.UserListDTO();
                        booking.ProjectSaleUser.Id = user.ID;
                        booking.SaleUser = new Base.DTOs.USR.UserListDTO();
                        booking.SaleUser.Id = user.ID;

                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var promotions = await service.GetBookingPromotionAsync(bookingId);
                        promotions.TransferDateBefore = DateTime.Today.AddDays(7);

                        var pricelists = await service.GetPriceListAsync(bookingId);
                        pricelists.ReferContact = new Base.DTOs.CTM.ContactListDTO();
                        pricelists.ReferContact.Id = contact.ID;
                        pricelists.FGFDiscount = 1000M;

                        var expenes = await service.GetPromotionExpensesAsync(bookingId);

                        var results = await service.UpdateBookingAsync(bookingId, booking, pricelists, promotions, expenes);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void IsMinPriceChangedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.OrderBy(o => o.Updated).FirstAsync();
                        var user = await db.Users.FirstAsync();

                        var bookingId = await db.Bookings.Where(o => o.ID == new Guid("143F3769-269A-470F-A4D2-A8808ECAD9E1")).Select(o => o.ID).FirstAsync();
                        var booking = new BookingDTO();
                        booking.BookingDate = DateTime.Now;
                        booking.ContractDate = DateTime.Now;
                        booking.SaleOfficerType = new MasterCenterDropdownDTO();
                        booking.SaleOfficerType.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SaleOfficerType && o.Key == SaleOfficerTypeKeys.AP).Select(o => o.ID).FirstAsync();
                        booking.ProjectSaleUser = new Base.DTOs.USR.UserListDTO();
                        booking.ProjectSaleUser.Id = user.ID;
                        booking.SaleUser = new Base.DTOs.USR.UserListDTO();
                        booking.SaleUser.Id = user.ID;

                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var promotions = await service.GetBookingPromotionAsync(bookingId);
                        promotions.TransferDateBefore = DateTime.Today.AddDays(7);

                        var pricelists = await service.GetPriceListAsync(bookingId);
                        pricelists.ReferContact = new Base.DTOs.CTM.ContactListDTO();
                        pricelists.ReferContact.Id = contact.ID;
                        pricelists.FGFDiscount = 1000000M;

                        var expenes = await service.GetPromotionExpensesAsync(bookingId);

                        var results = await service.IsMinPriceChangedAsync(booking, pricelists, promotions, expenes);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCancelMemoFormAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        var booking = new Booking()
                        {
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync(),
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                        };
                        var bookingOwner = BookingOwner.CreateFromContact(contact);
                        bookingOwner.BookingID = booking.ID;

                        await db.Bookings.AddAsync(booking);
                        await db.BookingOwners.AddAsync(bookingOwner);
                        await db.SaveChangesAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var result = await service.GetCancelMemoFormAsync(booking.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelBookingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var user = await db.Users.FirstAsync();
                        var contact = await db.Contacts.FirstAsync();
                        var booking = new Booking()
                        {
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync(),
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                        };
                        var payment = new Payment()
                        {
                            BookingID = booking.ID
                        };
                        var paymentMethod = new PaymentMethod()
                        {
                            PaymentID = payment.ID,
                            PayAmount = 100,
                            PaymentMethodTypeMasterCenterID = await db.MasterCenters.GetIDAsync(MasterCenterGroupKeys.PaymentMethod, PaymentMethodKeys.Cash)
                        };
                        var depositHeader = new DepositHeader();
                        var depositDetail = new DepositDetail()
                        {
                            DepositHeaderID = depositHeader.ID,
                            PaymentMethodID = paymentMethod.ID,
                            PayAmount = 100
                        };

                        await db.AddAsync(booking);
                        await db.AddAsync(payment);
                        await db.AddAsync(paymentMethod);
                        await db.AddAsync(depositHeader);
                        await db.AddAsync(depositDetail);
                        await db.SaveChangesAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        var input = await service.GetCancelMemoFormAsync(booking.ID);
                        input.CancelReason = CancelReasonDropdownDTO.CreateFromModel(await db.CancelReasons.FirstAsync());
                        await service.CancelBookingAsync(booking.ID, input, user.ID);

                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void DeleteBookingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var user = await db.Users.FirstAsync();
                        var booking = new Booking()
                        {
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync(),
                            ProjectID = db.Projects.Select(o => o.ID).First(),
                        };

                        await db.AddAsync(booking);
                        await db.SaveChangesAsync();

                        // Act
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var service = new BookingService(this.Configuration, quotationService, db);
                        await service.DeleteBookingAsync(booking.ID);

                        tran.Rollback();

                    }
                });
            }
        }

    }
}
