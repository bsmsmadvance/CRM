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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using models = Database.Models;

namespace Project.UnitTests
{
    public class WaiveQCServiceTest
    {
        IConfiguration Configuration;
        public WaiveQCServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void ImportWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_WaiveQC.xlsx",
                            Name = "ProjectID_WaiveQC.xlsx"
                        };
                        var results = await service.ImportWaiveQCAsync(project.ID, fileInput);

                        tran.Rollback();
                    }
                });
            }
        }
        [Fact]
        public async void GetWaiveQCListAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        WaiveQCFilter filter = FixtureFactory.Get().Build<WaiveQCFilter>().Create();
                        filter.UnitStatusKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus").Select(o => o.Key).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        WaiveQCSortByParam sortByParam = new WaiveQCSortByParam();
                        var results = await service.GetWaiveQCListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new WaiveQCFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(WaiveQCSortBy)).Cast<WaiveQCSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new WaiveQCSortByParam() { SortBy = item };
                            results = await service.GetWaiveQCListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var model = await db.WaiveQCs.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetWaiveQCAsync(model.ProjectID, model.ID);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateWaiveQCAsyncs()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveQCDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveQCDate = DateTime.Now;

                        var result = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveQCDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveQCDate = DateTime.Now;

                        var resultCreate = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);
                        resultCreate.WaiveQCDate = new DateTime(2019, 5, 20);

                        var result = await service.UpdateWaiveQCAsync(unit.ProjectID.Value, resultCreate.Id.Value, resultCreate);
                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void DeleteWaiveQCAsync()
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

                        var service = new WaiveQCService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveQCDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveQCDate = DateTime.Now;

                        var resultCreate = await service.CreateWaiveQCAsync(unit.ProjectID.Value, input);

                        await service.DeleteWaiveQCAsync(unit.ProjectID.Value, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
