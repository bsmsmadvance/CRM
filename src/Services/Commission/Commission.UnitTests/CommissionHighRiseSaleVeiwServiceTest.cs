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
    public class CommissionHighRiseSaleVeiwServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CommissionHighRiseSaleVeiwServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCommissionHighRiseSaleVeiwListAsync()
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
                        var service = new CommissionHighRiseSaleVeiwService(db);
                        CommissionHighRiseSaleVeiwFilter filter = FixtureFactory.Get().Build<CommissionHighRiseSaleVeiwFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CommissionHighRiseSaleVeiwSortByParam sortByParam = new CommissionHighRiseSaleVeiwSortByParam();
                        var results = await service.GetCommissionHighRiseSaleVeiwListAsync(filter, pageParam, sortByParam);

                        filter = new CommissionHighRiseSaleVeiwFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CommissionHighRiseSaleVeiwSortBy)).Cast<CommissionHighRiseSaleVeiwSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CommissionHighRiseSaleVeiwSortByParam() { SortBy = item };
                            results = await service.GetCommissionHighRiseSaleVeiwListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
