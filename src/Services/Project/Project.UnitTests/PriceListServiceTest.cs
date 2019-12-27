using System;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using Xunit;
using models = Database.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Project.UnitTests
{
    public class PriceListServiceTest
    {
        IConfiguration Configuration;
        public PriceListServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void ImportProjectPriceList()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here
                        var project = await db.Projects.Where(o => o.ProjectNo == "40047").FirstOrDefaultAsync();
                        PriceListService service = new PriceListService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_PriceList.xlsx",
                            Name = "ProjectID_PriceList.xlsx"
                        };
                        var results = await service.ImportProjectPriceListAsync(project.ID, fileInput);


                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void GetPriceListsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here

                        var service = new PriceListService(Configuration, db);

                        PriceListFilter filter = FixtureFactory.Get().Build<PriceListFilter>().Create();
                        filter.UnitStatusKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus").Select(o => o.Key).FirstAsync();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "40039").FirstAsync();
                        PageParam pageParam = new PageParam();
                        PriceListSortByParam sortByParam = new PriceListSortByParam();
                        var results = await service.GetPriceListsAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new PriceListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(PriceListSortBy)).Cast<PriceListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new PriceListSortByParam() { SortBy = item };
                            results = await service.GetPriceListsAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void CreatePriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here

                        var service = new PriceListService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();

                        var input = new PriceListDTO()
                        {
                            BookingAmount = 60000,
                            ContractAmount = 60000,
                            TotalSalePrice = 1200000,
                            UnitNo = unit.UnitNo,
                            DownAmount = 200000,
                            DownPaymentPeriod = 20,
                            DownPaymentPerPeriod = 10000,
                            SpecialDown = "2,10",
                            SpecialDownPrice = "20000,20000",
                            PercentDownPayment = 20,                
                        };

                        var result = await service.CreatePriceListAsync(project.ID, input);


                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void UpdatePriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here

                        var service = new PriceListService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();

                        var input = new PriceListDTO()
                        {
                            BookingAmount = 60000,
                            ContractAmount = 60000,
                            TotalSalePrice = 1200000,
                            UnitNo = unit.UnitNo,
                            DownAmount = 200000,
                            DownPaymentPeriod = 20,
                            DownPaymentPerPeriod = 10000,
                            SpecialDown = "2,10",
                            SpecialDownPrice = "20000,20000",
                            PercentDownPayment = 20,
                        };

                        var resultCreate = await service.CreatePriceListAsync(project.ID, input);

                        resultCreate.SpecialDown = "";
                        resultCreate.SpecialDownPrice = "";

                        var resultupdate = await service.UpdatePriceListAsync(project.ID, resultCreate.Id.Value, resultCreate);


                        tran.Rollback();

                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        PriceListService service = new PriceListService(Configuration, db);
                        //PriceListFilter filter = new PriceListFilter();
                        //PriceListSortByParam sortByParam = new PriceListSortByParam();
                        var project = await db.Projects.Where(o => o.ProjectNo == "40039").FirstOrDefaultAsync();
                        var result = await service.ExportExcelPriceListAsync(project.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportProjectPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        PriceListService service = new PriceListService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40047").FirstAsync();
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_PriceList.xlsx",
                            Name = "ProjectID_PriceList.xlsx"
                        };
                        var result = await service.ImportProjectPriceListAsync(project.ID,fileInput);
                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                        tran.Rollback();
                    }
                });
            }
        }

    }
}
