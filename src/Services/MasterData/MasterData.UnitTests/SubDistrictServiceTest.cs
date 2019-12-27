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
    public class SubDistrictServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void FindSubDistrictAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new SubDistrictService(db);
                        var province = await db.Provinces.FirstAsync();
                        var district = await db.Districts.FirstAsync(o => o.ProvinceID == province.ID);
                        var subDistrict = await db.SubDistricts.FirstAsync(o => o.DistrictID == district.ID);

                        var result = await service.FindSubDistrictAsync(district.ID, district.NameTH);
                        Assert.NotNull(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetSubDistrictListAsync()
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

                        var service = new SubDistrictService(db);
                        SubDistrictFilter filter = FixtureFactory.Get().Build<SubDistrictFilter>().Create();
                        PageParam pageParam = new PageParam();
                        SubDistrictSortByParam sortByParam = new SubDistrictSortByParam();

                        var results = await service.GetSubDistrictListAsync(filter, pageParam, sortByParam);

                        filter = new SubDistrictFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(SubDistrictSortBy)).Cast<SubDistrictSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new SubDistrictSortByParam() { SortBy = item };
                            results = await service.GetSubDistrictListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateSubDistrictAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var district = await db.Districts.Where(o => !o.IsDeleted).FirstAsync();
                        //Put unit test here
                        var input = new SubDistrictDTO
                        {
                            District = DistrictListDTO.CreateFromModel(district),
                            NameTH = "เทส",
                            PostalCode = "55555",
                            LandOffice = new LandOfficeListDTO
                            {
                                NameTH = "เทสสสสสสสสสสส"
                            }      
                        };
                        var service = new SubDistrictService(db);

                        var result = await service.CreateSubDistrictAsync(input);
                        var testCreateLandOffice = await db.LandOffices.Where(o => o.NameTH == "เทสสสสสสสสสสส").FirstOrDefaultAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetSubDistrictAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted).FirstAsync();

                        var input = new SubDistrictDTO
                        {
                            District = DistrictListDTO.CreateFromModel(district),
                            NameTH = "เทส",
                            PostalCode = "55555"
                        };

                        var service = new SubDistrictService(db);

                        var resultCreate = await service.CreateSubDistrictAsync(input);

                        var result = await service.GetSubDistrictAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateSubDistrictAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted).FirstAsync();

                        var input = new SubDistrictDTO
                        {
                            District = DistrictListDTO.CreateFromModel(district),
                            NameTH = "เทส",
                            PostalCode = "81150"
                        };
                        var service = new SubDistrictService(db);

                        var resultCreate = await service.CreateSubDistrictAsync(input);

                        resultCreate.LandOffice = new LandOfficeListDTO
                        {
                            NameTH = "เทสสสสสสสสสสส"
                        };

                        var result = await service.UpdateSubDistrictAsync(resultCreate.Id.Value, resultCreate);
                        var testCreateLandOffice = await db.LandOffices.Where(o => o.NameTH == "เทสสสสสสสสสสส").FirstOrDefaultAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteSubDistrictAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted).FirstAsync();

                        var input = new SubDistrictDTO
                        {
                            District = DistrictListDTO.CreateFromModel(district),
                            NameTH = "เทส",
                            PostalCode = "55555"
                        };
                        var service = new SubDistrictService(db);

                        var resultCreate = await service.CreateSubDistrictAsync(input);

                        await service.DeleteSubDistrictAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
