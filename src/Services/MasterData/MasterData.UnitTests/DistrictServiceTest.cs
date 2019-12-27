using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
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
    public class DistrictServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void FindDistrictAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new DistrictService(db);
                        var province = await db.Provinces.FirstAsync();
                        var district = await db.Districts.FirstAsync(o => o.ProvinceID == province.ID);
                        var result = await service.FindDistrictAsync(province.ID, district.NameTH);
                        Assert.NotNull(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetDistrictListAsync()
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

                        var service = new DistrictService(db);
                        DistrictFilter filter = FixtureFactory.Get().Build<DistrictFilter>().Create();
                        PageParam pageParam = new PageParam();
                        DistrictSortByParam sortByParam = new DistrictSortByParam();
                        var results = await service.GetDistrictListAsync(filter, pageParam, sortByParam);

                        filter = new DistrictFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(DistrictSortBy)).Cast<DistrictSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new DistrictSortByParam() { SortBy = item };
                            results = await service.GetDistrictListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateDistrictAsync()
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
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new DistrictService(db);
                        var input = new DistrictDTO
                        {
                            NameTH = "เมือง",
                            Province = ProvinceListDTO.CreateFromModel(province)
                        };
                        var result = await service.CreateDistrictAsync(input);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetDistrictAsync()
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
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new DistrictService(db);
                        var input = new DistrictDTO
                        {
                            NameTH = "เมือง",
                            Province = ProvinceListDTO.CreateFromModel(province)
                        };

                        var resultCreate = await service.CreateDistrictAsync(input);
                        var result = await service.GetDistrictAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateDistrictAsync()
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
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new DistrictService(db);
                        var input = new DistrictDTO
                        {
                            NameTH = "เมือง",
                            Province = ProvinceListDTO.CreateFromModel(province)
                        };

                        var resultCreate = await service.CreateDistrictAsync(input);
                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "Test";
                        var result = await service.UpdateDistrictAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteDistrictAsync()
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
                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new DistrictService(db);
                        var input = new DistrictDTO
                        {
                            NameTH = "เมือง",
                            Province = ProvinceListDTO.CreateFromModel(province)
                        };

                        var resultCreate = await service.CreateDistrictAsync(input);

                        await service.DeleteDistrictAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
