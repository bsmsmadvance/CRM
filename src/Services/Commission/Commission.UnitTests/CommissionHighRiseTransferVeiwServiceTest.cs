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
    public class CommissionHighRiseTransferVeiwServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public CommissionHighRiseTransferVeiwServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetCommissionHighRiseTransferVeiwListAsync()
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
                        var service = new CommissionHighRiseTransferVeiwService(db);
                        CommissionHighRiseTransferVeiwFilter filter = FixtureFactory.Get().Build<CommissionHighRiseTransferVeiwFilter>().Create();

                        PageParam pageParam = new PageParam();
                        CommissionHighRiseTransferVeiwSortByParam sortByParam = new CommissionHighRiseTransferVeiwSortByParam();
                        var results = await service.GetCommissionHighRiseTransferVeiwListAsync(filter, pageParam, sortByParam);

                        filter = new CommissionHighRiseTransferVeiwFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CommissionHighRiseTransferVeiwSortBy)).Cast<CommissionHighRiseTransferVeiwSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CommissionHighRiseTransferVeiwSortByParam() { SortBy = item };
                            results = await service.GetCommissionHighRiseTransferVeiwListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
