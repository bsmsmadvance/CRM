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
    public class CalculatePerMonthHighRiseSaleServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CalculatePerMonthHighRiseSaleServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCalculatePerMonthHighRiseSaleListAsync()
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
                        var service = new CalculatePerMonthHighRiseSaleService(db);
                        CalculatePerMonthHighRiseSaleFilter filter = FixtureFactory.Get().Build<CalculatePerMonthHighRiseSaleFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CalculatePerMonthHighRiseSaleSortByParam sortByParam = new CalculatePerMonthHighRiseSaleSortByParam();
                        var results = await service.GetCalculatePerMonthHighRiseSaleListAsync(filter, pageParam, sortByParam);

                        filter = new CalculatePerMonthHighRiseSaleFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CalculatePerMonthHighRiseSaleSortBy)).Cast<CalculatePerMonthHighRiseSaleSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CalculatePerMonthHighRiseSaleSortByParam() { SortBy = item };
                            results = await service.GetCalculatePerMonthHighRiseSaleListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CalculatePerMonthHighRiseSale()
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

                        var service = new CalculatePerMonthHighRiseSaleService(db);
                        service.CalculatePerMonthHighRiseSale(project.ID, DateTime.Now.Date, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveCalculatePerMonthHighRiseSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthHighRiseSales.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthHighRiseSaleService(db);
                        await service.ApproveCalculatePerMonthHighRiseSaleAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelApproveCalculatePerMonthHighRiseSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthHighRiseSales.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthHighRiseSaleService(db);
                        await service.CancelApproveCalculatePerMonthHighRiseSaleAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
