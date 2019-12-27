using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Project.UnitTests
{
    public class FloorServiceTest
    {
        IConfiguration Configuration;
        public FloorServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetFloorListAsync()
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

                        var service = new FloorService(db);
                        FloorsFilter filter = FixtureFactory.Get().Build<FloorsFilter>().Create();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.TowerCode == "01").FirstAsync();
                        PageParam pageParam = new PageParam();
                        FloorSortByParam sortByParam = new FloorSortByParam();
                        var results = await service.GetFloorListAsync(project.ID, tower.ID, filter, pageParam, sortByParam);

                        filter = new FloorsFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(FloorSortBy)).Cast<FloorSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new FloorSortByParam() { SortBy = item };
                            results = await service.GetFloorListAsync(project.ID, tower.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetFloorAsync()
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

                        var service = new FloorService(db);
                        var model = await db.Floors.Where(o => !o.IsDeleted && o.ProjectID != null && o.TowerID != null).FirstAsync();

                        var result = await service.GetFloorAsync(model.ProjectID, model.TowerID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateFloorAsync()
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

                        var service = new FloorService(db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";

                        var result = await service.CreateFloorAsync(project.ID, tower.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMultipleFloorAsync()
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

                        var service = new FloorService(db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<CreateMultipleFloorInput>().Create();
                        input.From = 5;
                        input.To = 10;

                        var result = await service.CreateMultipleFloorAsync(project.ID, tower.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateFloorAsync()
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

                        var service = new FloorService(db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";
                        var resultCreate = await service.CreateFloorAsync(project.ID, tower.ID, input);
                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "Test";
                        var result = await service.UpdateFloorAsync(project.ID, tower.ID, resultCreate.Id.Value, resultCreate);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteFloorAsync()
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

                        var service = new FloorService(db);
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10016").FirstAsync();
                        var tower = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var input = FixtureFactory.Get().Build<FloorDTO>().Create();
                        input.NameTH = "70";
                        input.NameEN = "70";

                        var resultCreate = await service.CreateFloorAsync(project.ID, tower.ID, input);

                        var result = await service.DeleteFloorAsync(project.ID, tower.ID, resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
