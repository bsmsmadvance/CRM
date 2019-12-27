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
    public class LandOfficeServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetLandOfficeDropdownListAsync()
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

                        var service = new LandOfficeService(db);
                        var province = await db.Provinces.FirstAsync(o => o.NameTH.Contains("กรุงเทพ"));
                        var results = await service.GetLandOfficeDropdownListAsync("ก", province.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLandOfficeListAsync()
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

                        var service = new LandOfficeService(db);
                        LandOfficeFilter filter = FixtureFactory.Get().Build<LandOfficeFilter>().Create();
                        PageParam pageParam = new PageParam();
                        LandOfficeSortByParam sortByParam = new LandOfficeSortByParam();
                        var results = await service.GetLandOfficeListAsync(filter, pageParam, sortByParam);

                        filter = new LandOfficeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LandOfficeSortBy)).Cast<LandOfficeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LandOfficeSortByParam() { SortBy = item };
                            results = await service.GetLandOfficeListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLandOfficeAsync()
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
                        var service = new LandOfficeService(db);
                        var subDistrict = await db.SubDistricts.Where(o => o.NameTH == "สวด").FirstOrDefaultAsync();
                        var district = await db.Districts.Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                        var province = await db.Provinces.Where(o => o.ID == district.ProvinceID).FirstOrDefaultAsync();
                        var input = new LandOfficeDTO();

                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.District = DistrictListDTO.CreateFromModel(district);
                        var result = await service.CreateLandOfficeAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLandOfficeAsync()
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
                        var service = new LandOfficeService(db);
                        var subDistrict = await db.SubDistricts.Where(o => o.NameTH == "สวด").FirstOrDefaultAsync();
                        var district = await db.Districts.Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                        var province = await db.Provinces.Where(o => o.ID == district.ProvinceID).FirstOrDefaultAsync();
                        var input = new LandOfficeDTO();

                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.District = DistrictListDTO.CreateFromModel(district);
                        var resultCreate = await service.CreateLandOfficeAsync(input);
                        var result = await service.GetLandOfficeAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateLandOfficeAsync()
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
                        var service = new LandOfficeService(db);
                        var subDistrict = await db.SubDistricts.Where(o => o.NameTH == "สวด").FirstOrDefaultAsync();
                        var district = await db.Districts.Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                        var province = await db.Provinces.Where(o => o.ID == district.ProvinceID).FirstOrDefaultAsync();
                        var input = new LandOfficeDTO();

                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.District = DistrictListDTO.CreateFromModel(district);
                        var resultCreate = await service.CreateLandOfficeAsync(input);

                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "TTTT";


                        var result = await service.UpdateLandOfficeAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLandOfficeAsync()
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
                        var service = new LandOfficeService(db);
                        var subDistrict = await db.SubDistricts.Where(o => o.NameTH == "สวด").FirstOrDefaultAsync();
                        var district = await db.Districts.Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                        var province = await db.Provinces.Where(o => o.ID == district.ProvinceID).FirstOrDefaultAsync();
                        var input = new LandOfficeDTO();

                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.District = DistrictListDTO.CreateFromModel(district);

                        var resultCreate = await service.CreateLandOfficeAsync(input);

                        await service.DeleteLandOfficeAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
