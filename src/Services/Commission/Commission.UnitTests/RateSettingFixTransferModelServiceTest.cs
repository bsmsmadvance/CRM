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
    public class RateSettingFixTransferModelServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingFixTransferModelServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingFixTransferModelListAsync()
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
                        var service = new RateSettingFixTransferModelService(db);
                        RateSettingFixTransferModelFilter filter = FixtureFactory.Get().Build<RateSettingFixTransferModelFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingFixTransferModelSortByParam sortByParam = new RateSettingFixTransferModelSortByParam();
                        var results = await service.GetRateSettingFixTransferModelListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingFixTransferModelFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingFixTransferModelSortBy)).Cast<RateSettingFixTransferModelSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingFixTransferModelSortByParam() { SortBy = item };
                            results = await service.GetRateSettingFixTransferModelListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixTransferModelProjectListAsync()
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

                        var service = new RateSettingFixTransferModelService(db);
                        var result = await service.GetRateSettingFixTransferModelProjectListAsync(project.ID, DateTime.Now.Date);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixTransferModelListAsync()
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

                        var service = new RateSettingFixTransferModelService(db);
                        await service.CreateRateSettingFixTransferModelListAsync(ListRateSettingFixSaleTransferModelDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixTransferModelAsync()
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

                        var service = new RateSettingFixTransferModelService(db);
                        var result = await service.CreateRateSettingFixTransferModelAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixTransferModelAsync()
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

                        var service = new RateSettingFixTransferModelService(db);
                        var resultCreate = await service.CreateRateSettingFixTransferModelAsync(input);

                        var result = await service.GetRateSettingFixTransferModelAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingFixTransferModelAsync()
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
                        var service = new RateSettingFixTransferModelService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var model = await db.Models.FirstOrDefaultAsync(o => o.Code == "60015001");

                        //Put unit test here
                        var input = new RateSettingFixSaleTransferModelDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var resultCreate = await service.CreateRateSettingFixTransferModelAsync(input);
                        resultCreate.Amount = 999999;

                        var result = await service.UpdateRateSettingFixTransferModelAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingFixTransferModelAsync()
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

                        var service = new RateSettingFixTransferModelService(db);
                        var resultCreate = await service.CreateRateSettingFixTransferModelAsync(input);
                        await service.DeleteRateSettingFixTransferModelAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
