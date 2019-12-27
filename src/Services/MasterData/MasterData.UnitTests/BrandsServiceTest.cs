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
    public class BrandsServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async void GetBrandListAsync()
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
                        var service = new BrandService(db);
                        BrandFilter filter = FixtureFactory.Get().Build<BrandFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BrandSortByParam sortByParam = new BrandSortByParam();
                        filter.UnitNumberFormatKey = "1";
                        var results = await service.GetBrandListAsync(filter, pageParam, sortByParam);

                        filter = new BrandFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BrandSortBy)).Cast<BrandSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BrandSortByParam() { SortBy = item };
                            results = await service.GetBrandListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBrandAsync()
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
                        var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                        var input = new BrandDTO();
                        input.BrandNo = "0001";
                        input.Name = "HP";
                        //input.UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber);

                        var service = new BrandService(db);
                        var result = await service.CreateBrandAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBrandAsync()
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
                        var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                        var input = new BrandDTO();
                        input.BrandNo = "0001";
                        input.Name = "HP";
                        input.UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber);

                        var service = new BrandService(db);
                        var resultCreate = await service.CreateBrandAsync(input);

                        var result = await service.GetBrandAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBrandAsync()
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
                        var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                        var input = new BrandDTO();
                        input.BrandNo = "0001";
                        input.Name = "HP";
                        input.UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber);

                        var service = new BrandService(db);
                        var resultCreate = await service.CreateBrandAsync(input);
                        resultCreate.Name = "HP01";
                        var result = await service.UpdateBrandAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBrandAsync()
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
                        var unitForMatNumber = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitNumberFormat").FirstAsync();

                        var input = new BrandDTO();
                        input.BrandNo = "0001";
                        input.Name = "HP";
                        input.UnitNumberFormat = MasterCenterDropdownDTO.CreateFromModel(unitForMatNumber);

                        var service = new BrandService(db);
                        var resultCreate = await service.CreateBrandAsync(input);

                        await service.DeleteBrandAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
