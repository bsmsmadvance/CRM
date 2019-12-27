using AutoFixture;
using Base.DTOs.SAL;
using CustomAutoFixture;
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
using Xunit;

namespace Sale.UnitTests
{
    public class UnitInfoServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public UnitInfoServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetUnitInfoListAsync()
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
                        var service = new UnitInfoService(db);
                        UnitInfoListFilter filter = new UnitInfoListFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnitInfoListSortByParam sortByParam = new UnitInfoListSortByParam();

                        //var results = await service.GetUnitInfoListAsync(filter, pageParam, sortByParam);

                        //filter = FixtureFactory.Get().Build<UnitInfoListFilter>().Create();

                        //filter.ProjectID = Guid.Parse("ef1798cc-a0c5-47c5-8846-ac8379ae2e29");
                        filter.UnitNo = "N05D01";

                        var results = await service.GetUnitInfoListAsync(filter, pageParam, sortByParam);

                        //filter = new UnitInfoListFilter();
                        //pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        //var sortByParams = Enum.GetValues(typeof(UnitInfoListSortBy)).Cast<UnitInfoListSortBy>();
                        //foreach (var item in sortByParams)
                        //{
                        //    sortByParam = new UnitInfoListSortByParam() { SortBy = item };
                        //    results = await service.GetUnitInfoListAsync(filter, pageParam, sortByParam);
                        //}

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoAsync(booking.UnitID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoBookingPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoBookingPromotionAsync(booking.UnitID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoPromotionExpensesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoPromotionExpensesAsync(booking.UnitID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoPreSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetUnitInfoPreSalePromotionAsync(booking.UnitID);

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
                        var service = new UnitInfoService(db);

                        var booking = await db.Bookings.Where(o => !string.IsNullOrEmpty(o.BookingNo)).FirstAsync();
                        var result = await service.GetPriceListAsync(booking.UnitID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoCountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnitInfoService(db);

                        var ProjectID = Guid.NewGuid();

                        var result = await service.GetUnitInfoCountAsync(ProjectID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoPaymentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {

                    var service = new UnitInfoService(db);

                    var UnitID = new Guid("35BC7B0B-1D90-4F87-94A5-B5E92E570CB5");

                    var result = await service.GetUnitInfoPaymentAsync(UnitID);

                    return;
                });
            }
        }
    }
}
