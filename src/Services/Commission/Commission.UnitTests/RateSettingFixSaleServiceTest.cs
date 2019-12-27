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
    public class RateSettingFixSaleServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingFixSaleServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingFixSaleListAsync()
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
                        var service = new RateSettingFixSaleService(db);
                        RateSettingFixSaleFilter filter = FixtureFactory.Get().Build<RateSettingFixSaleFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingFixSaleSortByParam sortByParam = new RateSettingFixSaleSortByParam();
                        var results = await service.GetRateSettingFixSaleListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingFixSaleFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingFixSaleSortBy)).Cast<RateSettingFixSaleSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingFixSaleSortByParam() { SortBy = item };
                            results = await service.GetRateSettingFixSaleListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingFixSaleAsync()
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

                        var service = new RateSettingFixSaleService(db);
                        await service.CreateRateSettingFixSaleAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingFixSaleAsync()
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

                        var service = new RateSettingFixSaleService(db);
                        await service.CreateRateSettingFixSaleAsync(input);

                        var resultCreate = await db.RateSettingFixSales.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");
                        var result = await service.GetRateSettingFixSaleAsync(resultCreate.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingFixSaleAsync()
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

                        var service = new RateSettingFixSaleService(db);
                        await service.CreateRateSettingFixSaleAsync(input);

                        var resultCreate = await db.RateSettingFixSales.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");
                        resultCreate.Amount = 7;

                        var RateSettingFixSale2 = RateSettingFixSaleTransferDTO.CreateFromFixSaleModel(resultCreate);

                        var result = await service.UpdateRateSettingFixSaleAsync(resultCreate.ID, RateSettingFixSale2);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingFixSaleAsync()
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

                        var service = new RateSettingFixSaleService(db);
                        await service.CreateRateSettingFixSaleAsync(input);

                        var resultCreate = await db.RateSettingFixSales.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015");

                        await service.DeleteRateSettingFixSaleAsync(resultCreate.ID);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
