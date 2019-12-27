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
    public class TypeOfRealEstateServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetTypeOfRealEstateListAsync()
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

                        var service = new TypeOfRealEstateService(db);
                        TypeOfRealEstateFilter filter = FixtureFactory.Get().Build<TypeOfRealEstateFilter>().Create();
                        PageParam pageParam = new PageParam();
                        TypeOfRealEstateSortByParam sortByParam = new TypeOfRealEstateSortByParam();
                        filter.RealEstateCategoryKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "RealEstateCategory")
                                                                             .Select(x => x.Key)
                                                                             .FirstAsync();
                        var results = await service.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam);

                        filter = new TypeOfRealEstateFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(TypeOfRealEstateSortBy)).Cast<TypeOfRealEstateSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new TypeOfRealEstateSortByParam() { SortBy = item };
                            results = await service.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateTypeOfRealEstateAsync()
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
                        var realEstateCategory = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RealEstateCategory").FirstAsync();
                        var input = new TypeOfRealEstateDTO();
                        input.Name = "Test";
                        input.Code = "55";
                        input.RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory);
                        var service = new TypeOfRealEstateService(db);

                        var result = await service.CreateTypeOfRealEstateAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void GetTypeOfRealEstateAsync()
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
                        var realEstateCategory = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RealEstateCategory").FirstAsync();
                        var input = new TypeOfRealEstateDTO();
                        input.Name = "Test";
                        input.Code = "55";
                        input.RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory);
                        var service = new TypeOfRealEstateService(db);

                        var resultCreate = await service.CreateTypeOfRealEstateAsync(input);

                        var result = await service.GetTypeOfRealEstateAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTypeOfRealEstateAsync()
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
                        var realEstateCategory = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RealEstateCategory").FirstAsync();
                        var input = new TypeOfRealEstateDTO();
                        input.Name = "Test";
                        input.Code = "55";
                        input.RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory);
                        var service = new TypeOfRealEstateService(db);

                        var resultCreate = await service.CreateTypeOfRealEstateAsync(input);

                        resultCreate.Name = "Test1234";

                        var result = await service.UpdateTypeOfRealEstateAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteTypeOfRealEstateAsync()
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
                        var realEstateCategory = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RealEstateCategory").FirstAsync();
                        var input = new TypeOfRealEstateDTO();
                        input.Name = "Test";
                        input.Code = "55";
                        input.RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(realEstateCategory);
                        var service = new TypeOfRealEstateService(db);

                        var resultCreate = await service.CreateTypeOfRealEstateAsync(input);

                        await service.DeleteTypeOfRealEstateAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
