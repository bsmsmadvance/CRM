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

namespace MasterData.UnitTests
{
    public class ProvinceServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void FindProvinceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new ProvinceService(db);

                        var model = await db.Provinces.FirstAsync();
                        var result = await service.FindProvinceAsync(model.NameTH);
                        Assert.NotNull(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProvinceListAsync()
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

                        var service = new ProvinceService(db);
                        ProvinceFilter filter = FixtureFactory.Get().Build<ProvinceFilter>().Create();
                        PageParam pageParam = new PageParam();
                        ProvinceSortByParam sortByParam = new ProvinceSortByParam();

                        var results = await service.GetProvinceListAsync(filter, pageParam, sortByParam);

                        filter = new ProvinceFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ProvinceSortBy)).Cast<ProvinceSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ProvinceSortByParam() { SortBy = item };
                            results = await service.GetProvinceListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateProvinceAsync()
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
                        var input = new ProvinceDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";

                        var service = new ProvinceService(db);
                        var result = await service.CreateProvinceAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProvinceAsync()
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
                        var input = new ProvinceDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";

                        var service = new ProvinceService(db);
                        var resultCreate = await service.CreateProvinceAsync(input);

                        var result = await service.GetProvinceAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProvincePostalCodeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new ProvinceService(db);

                        var result = await service.GetProvincePostalCodeAsync("39140");

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateProvinceAsync()
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
                        var input = new ProvinceDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";

                        var service = new ProvinceService(db);
                        var resultCreate = await service.CreateProvinceAsync(input);
                        resultCreate.NameTH = "เทสหนึ่ง";
                        var result = await service.UpdateProvinceAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteProvinceAsync()
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
                        var input = new ProvinceDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";

                        var service = new ProvinceService(db);
                        var resultCreate = await service.CreateProvinceAsync(input);

                        await service.DeleteProvinceAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
