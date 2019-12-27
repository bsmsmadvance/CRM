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
    public class CompaniesServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetCompanyListAsync()
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

                        var service = new CompanyService(db);
                        CompanyFilter filter = FixtureFactory.Get().Build<CompanyFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CompanySortByParam sortByParam = new CompanySortByParam();
                        var results = await service.GetCompanyListAsync(filter, pageParam, sortByParam);

                        filter = new CompanyFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CompanySortBy)).Cast<CompanySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CompanySortByParam() { SortBy = item };
                            results = await service.GetCompanyListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateCompanyAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new CompanyService(db);
                        var input = FixtureFactory.Get().Build<CompanyDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                           .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                           .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                           .Create();

                        var result = await service.CreateCompanyAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCompanyAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new CompanyService(db);
                        var input = FixtureFactory.Get().Build<CompanyDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                           .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                           .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                           .Create();

                        var resultCreate = await service.CreateCompanyAsync(input);

                        var result = await service.GetCompanyAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateCompanyAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new CompanyService(db);
                        var input = FixtureFactory.Get().Build<CompanyDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                           .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                           .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                           .Create();

                        var resultCreate = await service.CreateCompanyAsync(input);
                        resultCreate.NameTH = "Test";
                        var resultUpdate = await service.UpdateCompanyAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBankBranchAsync()
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
                        var district = await db.Districts.Where(o => !o.IsDeleted && o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => !o.IsDeleted && o.DistrictID == district.ID).FirstAsync();

                        var service = new CompanyService(db);
                        var input = FixtureFactory.Get().Build<CompanyDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.Province, ProvinceListDTO.CreateFromModel(province))
                                           .With(o => o.District, DistrictListDTO.CreateFromModel(district))
                                           .With(o => o.SubDistrict, SubDistrictListDTO.CreateFromModel(subdistrict))
                                           .Create();

                        var resultCreate = await service.CreateCompanyAsync(input);

                        await service.DeleteCompanyAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
