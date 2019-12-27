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
    public class CommissionLowRiseVeiwServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CommissionLowRiseVeiwServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCommissionLowRiseVeiwListAsync()
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
                        var service = new CommissionLowRiseVeiwService(Configuration, db);
                        CommissionLowRiseVeiwFilter filter = FixtureFactory.Get().Build<CommissionLowRiseVeiwFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CommissionLowRiseVeiwSortByParam sortByParam = new CommissionLowRiseVeiwSortByParam();
                        var results = await service.GetCommissionLowRiseVeiwListAsync(filter, pageParam, sortByParam);

                        filter = new CommissionLowRiseVeiwFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CommissionLowRiseVeiwSortBy)).Cast<CommissionLowRiseVeiwSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CommissionLowRiseVeiwSortByParam() { SortBy = item };
                            results = await service.GetCommissionLowRiseVeiwListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void ExportCommissionLowRiseAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CommissionLowRiseVeiwService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10060").FirstAsync();


                        var result = await service.ExportExcelCommissionLowRiseAsync(project.ID, DateTime.Now);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
