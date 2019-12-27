using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models.CMS;
using Database.UnitTestExtensions;
using Commission.Params.Filters;
using Commission.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Commission.UnitTests
{
    public class RateSettingFixSaleModelServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingFixSaleModelServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingFixSaleModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var service = new RateSettingFixSaleModelService(db);
                        RateSettingFixSaleModelFilter filter = FixtureFactory.Get().Build<RateSettingFixSaleModelFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingFixSaleModelSortByParam sortByParam = new RateSettingFixSaleModelSortByParam();
                        var results = await service.GetRateSettingFixSaleModelListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingFixSaleModelFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingFixSaleModelSortBy)).Cast<RateSettingFixSaleModelSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingFixSaleModelSortByParam() { SortBy = item };
                            results = await service.GetRateSettingFixSaleModelListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixSaleModelProjectListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");

                        var service = new RateSettingFixSaleModelService(db);
                        var result = await service.GetRateSettingFixSaleModelProjectListAsync(project.ID, DateTime.Now.Date);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixSaleModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");
                        var model2 = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015002");

                        //Put unit test here
                        var ListRateSettingFixSaleTransferModelDTO = new List<RateSettingFixSaleTransferModelDTO>();
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingFixSaleTransferModelDTO.Add(input);

                        input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model2);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingFixSaleTransferModelDTO.Add(input);

                        var service = new RateSettingFixSaleModelService(db);
                        await service.CreateRateSettingFixSaleModelListAsync(ListRateSettingFixSaleTransferModelDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixSaleModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");

                        //Put unit test here
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingFixSaleModelService(db);
                        var result = await service.CreateRateSettingFixSaleModelAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixSaleModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");

                        //Put unit test here
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingFixSaleModelService(db);
                        var resultCreate = await service.CreateRateSettingFixSaleModelAsync(input);

                        var result = await service.GetRateSettingFixSaleModelAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingFixSaleModelAsync()
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
                        var service = new RateSettingFixSaleModelService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");

                        //Put unit test here
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var resultCreate = await service.CreateRateSettingFixSaleModelAsync(input);
                        resultCreate.Amount = 999999;

                        var result = await service.UpdateRateSettingFixSaleModelAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingFixSaleModelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");

                        //Put unit test here
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingFixSaleModelService(db);
                        var resultCreate = await service.CreateRateSettingFixSaleModelAsync(input);
                        await service.DeleteRateSettingFixSaleModelAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
