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
    public class RateSettingFixTransferServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingFixTransferServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingFixTransferListAsync()
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
                        var service = new RateSettingFixTransferService(db);
                        RateSettingFixTransferFilter filter = FixtureFactory.Get().Build<RateSettingFixTransferFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingFixTransferSortByParam sortByParam = new RateSettingFixTransferSortByParam();
                        var results = await service.GetRateSettingFixTransferListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingFixTransferFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingFixTransferSortBy)).Cast<RateSettingFixTransferSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingFixTransferSortByParam() { SortBy = item };
                            results = await service.GetRateSettingFixTransferListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixTransferAsync()
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
                        var input = new RateSettingFixSaleTransferDTO();
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 5;
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new RateSettingFixTransferService(db);
                        await service.CreateRateSettingFixTransferAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixTransferAsync()
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
                        var input = new RateSettingFixSaleTransferDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 5;
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new RateSettingFixTransferService(db);
                        await service.CreateRateSettingFixTransferAsync(input);

                        var resultCreate = await db.RateSettingFixTransfers.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");
                        var result = await service.GetRateSettingFixTransferAsync(resultCreate.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingFixTransferAsync()
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
                        var input = new RateSettingFixSaleTransferDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 5;
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new RateSettingFixTransferService(db);
                        await service.CreateRateSettingFixTransferAsync(input);

                        var resultCreate = await db.RateSettingFixTransfers.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");
                        resultCreate.Amount = 7;

                        var RateSettingFixTransfer2 = RateSettingFixSaleTransferDTO.CreateFromFixTransferModel(resultCreate);

                        var result = await service.UpdateRateSettingFixTransferAsync(resultCreate.ID, RateSettingFixTransfer2);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingFixTransferAsync()
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
                        var input = new RateSettingFixSaleTransferDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 5;
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new RateSettingFixTransferService(db);
                        await service.CreateRateSettingFixTransferAsync(input);

                        var resultCreate = await db.RateSettingFixTransfers.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");

                        await service.DeleteRateSettingFixTransferAsync(resultCreate.ID);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
