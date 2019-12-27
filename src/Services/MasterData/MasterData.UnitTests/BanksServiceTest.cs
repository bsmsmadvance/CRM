using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MasterData.Params.Filters;
using MasterData.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace MasterData.UnitTests
{
    public class BanksServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public BanksServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetBankListAsync()
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

                        var service = new BankService(db);
                        BankFilter filter = FixtureFactory.Get().Build<BankFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BankSortByParam sortByParam = new BankSortByParam();
                        var results = await service.GetBankListAsync(filter, pageParam, sortByParam);

                        filter = new BankFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BankSortBy)).Cast<BankSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BankSortByParam() { SortBy = item };
                            results = await service.GetBankListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBankAsync()
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
                        var input = new BankDTO();
                        input.BankNo = "792";
                        input.NameTH = "ธนาคารไทยทดสอบ";
                        input.NameEN = "TestBank";
                        input.Alias = "test";

                        var service = new BankService(db);
                        var result = await service.CreateBankAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBankAsync()
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
                        var input = new BankDTO();
                        input.BankNo = "792";
                        input.NameTH = "ธนาคารไทยทดสอบ";
                        input.NameEN = "TestBank";
                        input.Alias = "test";

                        var service = new BankService(db);
                        var resultCreate = await service.CreateBankAsync(input);

                        var result = await service.GetBankAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBankAsync()
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
                        var service = new BankService(db);

                        var input = new BankDTO();
                        input.BankNo = "792";
                        input.NameTH = "ธนาคารไทยทดสอบ";
                        input.NameEN = "TestBank";
                        input.Alias = "test";
                        var resultCreate = await service.CreateBankAsync(input);
                        resultCreate.NameTH = "เทส";

                        var result = await service.UpdateBankAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBankAsync()
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
                        var input = new BankDTO();
                        input.BankNo = "792";
                        input.NameTH = "ธนาคารไทยทดสอบ";
                        input.NameEN = "TestBank";
                        input.Alias = "test";

                        var service = new BankService(db);
                        var resultCreate = await service.CreateBankAsync(input);
                        await service.DeleteBankAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
