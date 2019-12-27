using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Base.DTOs;

namespace Project.UnitTests
{
    public class MinPriceServiceTest
    {
        IConfiguration Configuration;
        public MinPriceServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetMinPriceListAsync()
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

                        var service = new MinPriceService(Configuration, db);
                        MinPriceFilter filter = FixtureFactory.Get().Build<MinPriceFilter>().Create();
                        filter.DocTypeKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "DocType").Select(o => o.Key).FirstAsync();
                        filter.MinPriceTypeKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceType").Select(o => o.Key).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                      
                        PageParam pageParam = new PageParam();
                        MinPriceSortByParam sortByParam = new MinPriceSortByParam();
                        var results = await service.GetMinPriceListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new MinPriceFilter();

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MinPriceSortBy)).Cast<MinPriceSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MinPriceSortByParam() { SortBy = item };
                            results = await service.GetMinPriceListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMinPriceAsync()
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

                        var service = new MinPriceService(Configuration, db);
                        var model = await db.MinPrices.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetMinPriceAsync(model.ProjectID.Value, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMinPriceAsync()
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

                        var service = new MinPriceService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        var minPriceType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceType").FirstAsync();
                        var docType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "DocType").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && !o.IsDeleted).FirstAsync();
                        var input = FixtureFactory.Get().Build<MinPriceDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MinPriceType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(minPriceType))
                                           .With(o => o.DocType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(docType))
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.ApprovedMinPrice, 55)
                                           .Create();


                        var result = await service.CreateMinPriceAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMinPriceAsync()
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

                        var service = new MinPriceService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        var minPriceType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceType").FirstAsync();
                        var docType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "DocType").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && !o.IsDeleted).FirstAsync();
                        var input = FixtureFactory.Get().Build<MinPriceDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MinPriceType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(minPriceType))
                                           .With(o => o.DocType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(docType))
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.ApprovedMinPrice, 55)
                                           .Create();


                        var resultCreate = await service.CreateMinPriceAsync(project.ID, input);

                        resultCreate.Cost = 50;

                        var result = await service.UpdateMinPriceAsync(project.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMinPriceAsync()
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

                        var service = new MinPriceService(Configuration, db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        var minPriceType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceType").FirstAsync();
                        var docType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "DocType").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && !o.IsDeleted).FirstAsync();
                        var input = FixtureFactory.Get().Build<MinPriceDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MinPriceType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(minPriceType))
                                           .With(o => o.DocType, Base.DTOs.MST.MasterCenterDropdownDTO.CreateFromModel(docType))
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.ApprovedMinPrice, 55)
                                           .Create();


                        var resultCreate = await service.CreateMinPriceAsync(project.ID, input);

                        await service.DeleteMinPriceAsync(project.ID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportMinPriceAsync()
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
                        var service = new MinPriceService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_MinPrice.xlsx",
                            Name = "ProjectID_MinPrice.xlsx"
                        };
                        var results = await service.ImportMinPriceAsync(project.ID, fileInput);


                        tran.Rollback();

                    }
                });
            }
        }
    }
}
