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

namespace Project.UnitTests
{
    public class LowRiseBuildingPriceFeeServiceTest
    {
        IConfiguration Configuration;
        public LowRiseBuildingPriceFeeServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetLowRiseBuildingPriceFeeListAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);

                        LowRiseBuildingPriceFeeFilter filter = FixtureFactory.Get().Build<LowRiseBuildingPriceFeeFilter>().Create();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        LowRiseBuildingPriceFeeSortByParam sortByParam = new LowRiseBuildingPriceFeeSortByParam();
                        var results = await service.GetLowRiseBuildingPriceFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new LowRiseBuildingPriceFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LowRiseBuildingPriceFeeSortBy)).Cast<LowRiseBuildingPriceFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LowRiseBuildingPriceFeeSortByParam() { SortBy = item };
                            results = await service.GetLowRiseBuildingPriceFeeListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);

                        var model = await db.LowRiseBuildingPriceFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetLowRiseBuildingPriceFeeAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var model = await db.Models.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new LowRiseBuildingPriceFeeDTO();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.Price = 600;

                        var result = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateLowRiseBuildingPriceFeesync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var model = await db.Models.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new LowRiseBuildingPriceFeeDTO();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.Price = 600;

                        var resultCreate = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);
                        resultCreate.Price = 1000;
                        var result = await service.UpdateLowRiseBuildingPriceFeesync(model.ProjectID, resultCreate.Id.Value, resultCreate);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLowRiseBuildingPriceFeeAsync()
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

                        var service = new LowRiseBuildingPriceFeeService(db);
                        var model = await db.Models.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new LowRiseBuildingPriceFeeDTO();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.Price = 600;

                        var resultCreate = await service.CreateLowRiseBuildingPriceFeeAsync(model.ProjectID, input);

                        await service.DeleteLowRiseBuildingPriceFeeAsync(model.ProjectID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
