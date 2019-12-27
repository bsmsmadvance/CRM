using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
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
    public class TowerServiceTest
    {
        IConfiguration Configuration;
        public TowerServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetTowerListAsync()
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

                        var service = new TowerService(db);
                        TowerFilter filter = FixtureFactory.Get().Build<TowerFilter>().Create();
                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        TowerSortByParam sortByParam = new TowerSortByParam();
                        var results = await service.GetTowerListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new TowerFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TowerSortBy)).Cast<TowerSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TowerSortByParam() { SortBy = item };
                            results = await service.GetTowerListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTowerAsync()
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

                        var service = new TowerService(db);
                        var model = await db.Towers.Where(o => !o.IsDeleted && o.ProjectID != null).FirstAsync();

                        var result = await service.GetTowerAsync(model.ProjectID.Value, model.ID);

                            tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateTowerAsync()
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

                        var service = new TowerService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10038").FirstAsync();
                        var input = new TowerDTO();
                        input.Code = "S";
                        input.NoTH = "อาคาร 10";

                        var result = await service.CreateTowerAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTowerAsync()
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

                        var service = new TowerService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10038").FirstAsync();
                        var input = new TowerDTO();
                        input.Code = "S";
                        input.NoTH = "อาคาร 10";

                        var resultCreate = await service.CreateTowerAsync(project.ID, input);

                        resultCreate.Code = "09";

                        var result = await service.UpdateTowerAsync(project.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteTowerAsync()
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

                        var service = new TowerService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10038").FirstAsync();
                        var input = new TowerDTO();
                        input.Code = "S";
                        input.NoTH = "อาคาร 10";

                        var resultCreate = await service.CreateTowerAsync(project.ID, input);

                        var result = await service.DeleteTowerAsync(project.ID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestTowerDataStatus()
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
                        var service = new ProjectService(Configuration, db);
                        var serviceTower = new TowerService(db);
                        var serviceFloor = new FloorService(db);
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreateProject = await service.CreateProjectAsync(input);

                        var inputTower = new TowerDTO();

                        inputTower.Code = "2222";
                        inputTower.NoTH = "111";
                        inputTower.NoEN = "oneoneone";
                        inputTower.CondominiumName = "tttt";
                        inputTower.CondominiumNo = "22223";

                        var resultCreateTower = await serviceTower.CreateTowerAsync(resultCreateProject.Id.Value, inputTower);
                        var inputFloor = new FloorDTO();
                        inputFloor.NameTH = "1";
                        inputFloor.NameEN = "one";
                        var resultCreateFloor = await serviceFloor.CreateFloorAsync(resultCreateProject.Id.Value, resultCreateTower.Id.Value, inputFloor);
                        var inputFloor2 = new FloorDTO();
                        inputFloor2.NameTH = "2";
                        inputFloor2.NameEN = "two";
                        var resultCreateFloor2 = await serviceFloor.CreateFloorAsync(resultCreateProject.Id.Value, resultCreateTower.Id.Value, inputFloor2);

                        var resultDataStatusWhenCreate = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);

                        resultCreateFloor2.NameTH = "";
                        await serviceFloor.UpdateFloorAsync(resultCreateProject.Id.Value, resultCreateTower.Id.Value, resultCreateFloor2.Id.Value, resultCreateFloor2);
                        var resultDataStatusWhenUpdate = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);
                        await serviceFloor.DeleteFloorAsync(resultCreateProject.Id.Value, resultCreateTower.Id.Value, resultCreateFloor2.Id.Value);
                        var resultDataStatusWhenDelete = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
