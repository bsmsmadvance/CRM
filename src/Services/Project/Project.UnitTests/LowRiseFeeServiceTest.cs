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
    public class LowRiseFeeServiceTest
    {
        IConfiguration Configuration;
        public LowRiseFeeServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetLowRiseFeeListAsync()
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

                        var service = new LowRiseFeeService(db);

                        LowRiseFeeFilter filter = FixtureFactory.Get().Build<LowRiseFeeFilter>().Create();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        LowRiseFeeSortByParam sortByParam = new LowRiseFeeSortByParam();
                        var results = await service.GetLowRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new LowRiseFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LowRiseFeeSortBy)).Cast<LowRiseFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LowRiseFeeSortByParam() { SortBy = item };
                            results = await service.GetLowRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLowRiseFeeAsync()
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

                        var service = new LowRiseFeeService(db);
                        var model = await db.LowRiseFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetLowRiseFeeAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLowRiseFeeAsync()
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

                        var service = new LowRiseFeeService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10098").FirstAsync();

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID && o.UnitNo == "C22-1").FirstAsync();

                        var input = new LowRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.EstimatePriceArea = 700;

                        var result = await service.CreateLowRiseFeeAsync(unit.ProjectID.Value, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateLowRiseFeeAsync()
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

                        var service = new LowRiseFeeService(db);
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new LowRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.EstimatePriceArea = 700;

                        var resultCreate = await service.CreateLowRiseFeeAsync(unit.ProjectID.Value, input);
                        resultCreate.EstimatePriceArea = 1000;
                        var result = await service.UpdateLowRiseFeeAsync(unit.ProjectID.Value, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLowRiseFeeAsync()
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

                        var service = new LowRiseFeeService(db);
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new LowRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.EstimatePriceArea = 700;

                        var resultCreate = await service.CreateLowRiseFeeAsync(unit.ProjectID.Value, input);

                        await service.DeleteLowRiseFeeAsync(unit.ProjectID.Value, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
