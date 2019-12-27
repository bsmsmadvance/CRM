using System;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MasterData.Params.Filters;
using MasterData.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using Xunit;
using Xunit.Abstractions;

namespace MasterData.UnitTests
{
    public class CountryServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetCountryDropdownListAsync()
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

                        var service = new CountryService(db);
                        CountryFilter filter = FixtureFactory.Get().Build<CountryFilter>().Create();
                        var results = await service.GetCountryDropdownListAsync(filter);

                        filter = new CountryFilter();
                        results = await service.GetCountryDropdownListAsync(filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCountryListAsync()
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
                        var service = new CountryService(db);
                        CountryFilter filter = FixtureFactory.Get().Build<CountryFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CountrySortByParam sortByParam = new CountrySortByParam();

                        var results = await service.GetCountryListAsync(filter, pageParam, sortByParam);

                        filter = new CountryFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CountrySortBy)).Cast<CountrySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CountrySortByParam() { SortBy = item };
                            results = await service.GetCountryListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateCountryAsync()
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

                        var input = new CountryDTO();
                        input.Code = "AB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var result = await service.CreateCountryAsync(input);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        var result = await service.GetCountryAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void FindCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        var result = await service.FindCountryAsync(resultCreate.Code);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        resultCreate.NameTH = "เทสแก้ไขประเทศ";
                        var result = await service.UpdateCountryAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteCountryAsync()
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
                        var input = new CountryDTO();
                        input.Code = "EB";
                        input.NameTH = "เทสประเทศ";
                        input.NameEN = "TestCountry";

                        var service = new CountryService(db);
                        var resultCreate = await service.CreateCountryAsync(input);

                        await service.DeleteCountryAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
