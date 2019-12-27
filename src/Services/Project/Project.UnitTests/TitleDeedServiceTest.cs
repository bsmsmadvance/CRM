using System;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Services;
using Xunit;
using models = Database.Models;

namespace Project.UnitTests
{
    public class TitleDeedServiceTest
    {
        IConfiguration Configuration;
        private static readonly Fixture Fixture = new Fixture();

        public TitleDeedServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetTitleDeedListAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var model = await db.TitledeedDetails.FirstAsync();
                        var input = await service.GetTitleDeedAsync(model.ID);
                        var projectID = await db.Projects.Where(o => o.ProjectNo == "60015").Select(o => o.ID).FirstOrDefaultAsync();

                        //filter test
                        TitleDeedFilter filter = FixtureFactory.Get().Build<TitleDeedFilter>().Create();
                        filter.LandStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "LandStatus")
                                                                     .Select(x => x.Key).FirstAsync();
                        filter.PreferStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PreferStatus")
                                                                       .Select(x => x.Key).FirstAsync();
                        filter.UnitStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam();
                        var results = await service.GetTitleDeedListAsync(projectID, filter, pageParam, sortByParam);

                        //sort by test
                        var titleDeeds = await db.TitledeedDetails.Where(o => !o.IsDeleted).FirstOrDefaultAsync();

                        filter = new TitleDeedFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TitleDeedListSortBy)).Cast<TitleDeedListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TitleDeedListSortByParam() { SortBy = item };
                            results = await service.GetTitleDeedListAsync(titleDeeds.ProjectID, filter, pageParam, sortByParam);
                        }


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTitleDeedAsync()
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

                        var service = new TitleDeedService(Configuration, db);

                        var titleDeed = await db.TitledeedDetails.FirstOrDefaultAsync();


                        var results = await service.GetTitleDeedAsync(titleDeed.ID);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateTitleDeedAsync()
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

                        var service = new TitleDeedService(Configuration, db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "60016").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        var titledeed = new TitleDeedDTO();

                        titledeed.TitledeedNo = "123456";
                        titledeed.Project = ProjectDropdownDTO.CreateFromModel(project);
                        titledeed.Unit = UnitDropdownDTO.CreateFromModel(unit);

                        var result = await service.CreateTitleDeedAsync(project.ID, titledeed);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTitleDeedAsync()
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

                        var service = new TitleDeedService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "60016").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        var titledeed = new TitleDeedDTO();

                        titledeed.TitledeedNo = "123456";
                        titledeed.Project = ProjectDropdownDTO.CreateFromModel(project);
                        titledeed.Unit = UnitDropdownDTO.CreateFromModel(unit);

                        var result = await service.CreateTitleDeedAsync(project.ID, titledeed);

                        result.TitledeedNo = "123456";
                        var resultUpdate = await service.UpdateTitleDeedAsync(project.ID, result.Id.Value, result);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteTitleDeedAsync()
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

                        var service = new TitleDeedService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "60016").FirstOrDefaultAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        var titledeed = new TitleDeedDTO();

                        titledeed.TitledeedNo = "123456";
                        titledeed.Project = ProjectDropdownDTO.CreateFromModel(project);
                        titledeed.Unit = UnitDropdownDTO.CreateFromModel(unit);

                        var result = await service.CreateTitleDeedAsync(project.ID, titledeed);

                        await service.DeleteTitleDeedAsync(project.ID, result.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTitleDeedStatusAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var model = await db.TitledeedDetails.FirstAsync();
                        var input = await service.GetTitleDeedAsync(model.ID);

                        await service.UpdateTitleDeedStatusAsync(model.ID, input);
                        var histories = await db.TitledeedDetailHistories.Where(o => o.TitledeedDetailID == model.ID).ToListAsync();
                        Assert.NotEmpty(histories);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTitledeedHistoryItemsAsync()
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

                        var service = new TitleDeedService(Configuration, db);
                        var model = await db.TitledeedDetails.FirstAsync();
                        var input = await service.GetTitleDeedAsync(model.ID);

                        await service.UpdateTitleDeedStatusAsync(model.ID, input);
                        await service.UpdateTitleDeedStatusAsync(model.ID, input);
                        var results = await service.GetTitleDeedHistoryItemsAsync(model.ID);
                        Assert.NotEmpty(results);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMultipleHouseNosAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TitleDeedService(Configuration, db);
                        var project = await db.Projects.FirstAsync(o => o.ProjectNo == "40017");
                        var fromUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B02");
                        var toUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B10");
                        var input = new UpdateMultipleHouseNoParam();
                        input.FromUnit = UnitDropdownDTO.CreateFromModel(fromUnit);
                        input.ToUnit = UnitDropdownDTO.CreateFromModel(toUnit);
                        input.FromHouseNo = "16/2";
                        await service.UpdateMultipleHouseNosAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMultipleLandOfficesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TitleDeedService(Configuration, db);
                        var input = new UpdateMultipleLandOfficeParam();
                        var project = await db.Projects.FirstAsync(o => o.ProjectNo == "40017");
                        var fromUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B02");
                        var toUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B10");
                        var landOffice = await db.LandOffices.FirstAsync();
                        input.FromUnit = UnitDropdownDTO.CreateFromModel(fromUnit);
                        input.ToUnit = UnitDropdownDTO.CreateFromModel(toUnit);
                        input.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        await service.UpdateMultipleLandOfficesAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TitleDeedService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_TitleDeed.xlsx",
                            Name = "ProjectID_TitleDeed.xlsx"
                        };
                        var result = await service.ImportTitleDeedAsync(project.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TitleDeedService(Configuration, db);
                        TitleDeedFilter filter = new TitleDeedFilter();
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam();
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstOrDefaultAsync();
                        var result = await service.ExportExcelTitleDeedAsync(project.ID);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
