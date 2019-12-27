using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Base.DTOs.MST;
using Base.DTOs.SAL;
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
using Database.Models.SAL;
using Database.Models.MasterKeys;
using Database.Models;

namespace Commission.UnitTests
{
    public class CalculatePerMonthLowRiseServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CalculatePerMonthLowRiseServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCalculatePerMonthLowRiseListAsync()
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
                        var service = new CalculatePerMonthLowRiseService(db);
                        CalculatePerMonthLowRiseFilter filter = FixtureFactory.Get().Build<CalculatePerMonthLowRiseFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CalculatePerMonthLowRiseSortByParam sortByParam = new CalculatePerMonthLowRiseSortByParam();
                        var results = await service.GetCalculatePerMonthLowRiseListAsync(filter, pageParam, sortByParam);

                        filter = new CalculatePerMonthLowRiseFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CalculatePerMonthLowRiseSortBy)).Cast<CalculatePerMonthLowRiseSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CalculatePerMonthLowRiseSortByParam() { SortBy = item };
                            results = await service.GetCalculatePerMonthLowRiseListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CalculatePerMonthLowRise()
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
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthLowRiseService(db);
                        service.CalculatePerMonthLowRise(project.ID, DateTime.Now.Date, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveCalculatePerMonthLowRiseAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthLowRises.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthLowRiseService(db);
                        await service.ApproveCalculatePerMonthLowRiseAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelApproveCalculatePerMonthLowRiseAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthLowRises.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthLowRiseService(db);
                        await service.CancelApproveCalculatePerMonthLowRiseAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
