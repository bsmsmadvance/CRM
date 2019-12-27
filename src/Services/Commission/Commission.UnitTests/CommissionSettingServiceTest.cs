using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models.CMS;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Commission.Params.Filters;
using Commission.Params.Inputs;
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
    public class CommissionSettingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CommissionSettingServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCommissionSettingListAsync()
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
                        var bg = await db.BGs.FirstOrDefaultAsync(o => o.BGNo == "1");

                        var service = new CommissionSettingService(db);
                        CommissionSettingFilter filter = FixtureFactory.Get().Build<CommissionSettingFilter>().Create();
                        filter.BGID = bg.ID;

                        PageParam pageParam = new PageParam();
                        CommissionSettingSortByParam sortByParam = new CommissionSettingSortByParam();
                        var results = await service.GetCommissionSettingListAsync(filter, pageParam, sortByParam);

                        filter = new CommissionSettingFilter();
                        filter.BGID = bg.ID;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CommissionSettingSortBy)).Cast<CommissionSettingSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CommissionSettingSortByParam() { SortBy = item };
                            results = await service.GetCommissionSettingListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetSaleUserProjectAsync()
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

                        var service = new CommissionSettingService(db);

                        var result = await service.GetSaleUserProjectAsync(project.ID, "");

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetSaleUserAllAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CommissionSettingService(db);

                        var result = await service.GetSaleUserAllAsync();

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectDropdownListByBGAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bg = await db.BGs.FirstOrDefaultAsync(o => o.BGNo == "1");

                        var service = new CommissionSettingService(db);

                        var result = await service.GetProjectDropdownListByBGAsync(bg.ID);

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void GetProjectDropdownListByProjectAsync()
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
                        var project2 = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60012");

                        var lst = new List<ProjectInput>();
                        lst.Add(ProjectInput.CreateFromModel(project));
                        lst.Add(ProjectInput.CreateFromModel(project2));

                        var listPro = new ListProjectInput();
                        listPro.Projects = lst;

                        var service = new CommissionSettingService(db);

                        var result = await service.GetProjectDropdownListByProjectAsync(listPro);

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectDropdownListByProductTypeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        string productType = "1";

                        // 1=แนวราบ/2=แนวสูง        
                        var service = new CommissionSettingService(db);

                        var result = await service.GetProjectDropdownListByProductTypeAsync(productType);

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
