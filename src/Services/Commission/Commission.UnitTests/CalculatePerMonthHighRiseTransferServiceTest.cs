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
    public class CalculatePerMonthHighRiseTransferServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CalculatePerMonthHighRiseTransferServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCalculatePerMonthHighRiseTransferListAsync()
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
                        var service = new CalculatePerMonthHighRiseTransferService(db);
                        CalculatePerMonthHighRiseTransferFilter filter = FixtureFactory.Get().Build<CalculatePerMonthHighRiseTransferFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CalculatePerMonthHighRiseTransferSortByParam sortByParam = new CalculatePerMonthHighRiseTransferSortByParam();
                        var results = await service.GetCalculatePerMonthHighRiseTransferListAsync(filter, pageParam, sortByParam);

                        filter = new CalculatePerMonthHighRiseTransferFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CalculatePerMonthHighRiseTransferSortBy)).Cast<CalculatePerMonthHighRiseTransferSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CalculatePerMonthHighRiseTransferSortByParam() { SortBy = item };
                            results = await service.GetCalculatePerMonthHighRiseTransferListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CalculatePerMonthHighRiseTransfer()
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

                        var service = new CalculatePerMonthHighRiseTransferService(db);
                        service.CalculatePerMonthHighRiseTransfer(project.ID, DateTime.Now.Date, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveCalculatePerMonthHighRiseTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthHighRiseTransfers.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthHighRiseTransferService(db);
                        await service.ApproveCalculatePerMonthHighRiseTransferAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelApproveCalculatePerMonthHighRiseTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var cal = await db.CalculatePerMonthHighRiseTransfers.FirstOrDefaultAsync(o => o.Project.ProjectNo == "60015" && o.PeriodMonth == 1 && o.PeriodYear == 2019);
                        var user = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var service = new CalculatePerMonthHighRiseTransferService(db);
                        await service.CancelApproveCalculatePerMonthHighRiseTransferAsync(cal.ID, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
