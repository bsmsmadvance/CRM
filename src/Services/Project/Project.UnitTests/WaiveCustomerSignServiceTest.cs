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
    public class WaiveCustomerSignServiceTest
    {
        IConfiguration Configuration;
        public WaiveCustomerSignServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetWaiveCustomerSignListAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);

                        WaiveCustomerSignFilter filter = FixtureFactory.Get().Build<WaiveCustomerSignFilter>().Create();
                        filter.UnitStatusKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus").Select(o => o.Key).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        WaiveCustomerSignSortByParam sortByParam = new WaiveCustomerSignSortByParam();
                        var results = await service.GetWaiveCustomerSignListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new WaiveCustomerSignFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(WaiveCustomerSignSortBy)).Cast<WaiveCustomerSignSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new WaiveCustomerSignSortByParam() { SortBy = item };
                            results = await service.GetWaiveCustomerSignListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);

                        var model = await db.WaiveQCs.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetWaiveCustomerSignAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveCustomerSignDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveSignDate = DateTime.Now;

                        var result = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);

                        tran.Rollback();
                    }
                });
            }
        }
        [Fact]
        public async void ImportWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_WaiveCustomerSign.xlsx",
                            Name = "ProjectID_WaiveCustomerSign.xlsx"
                        };
                        var results = await service.ImportWaiveCustomerSignAsync(project.ID, fileInput);

                        tran.Rollback();
                    }
                });
            }
        }



        [Fact]
        public async void UpdateWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveCustomerSignDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveSignDate = DateTime.Now;

                        var resultCreate = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);

                        resultCreate.WaiveSignDate = new DateTime(2019, 5, 22);

                        var result = await service.UpdateWaiveCustomerSignAsync(unit.ProjectID.Value, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteWaiveCustomerSignAsync()
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

                        var service = new WaiveCustomerSignService(Configuration, db);

                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var input = new WaiveCustomerSignDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.WaiveSignDate = DateTime.Now;

                        var resultCreate = await service.CreateWaiveCustomerSignAsync(unit.ProjectID.Value, input);

                        await service.DeleteWaiveCustomerSignAsync(unit.ProjectID.Value, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
