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
using ErrorHandling;
using System.Diagnostics;

namespace MasterData.UnitTests
{
    public class BGsServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async void GetBGListAsync()
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

                        var service = new BGService(db);
                        BGFilter filter = FixtureFactory.Get().Build<BGFilter>().Create();
                        filter.ProductTypeKey = "1";
                        PageParam pageParam = new PageParam();
                        BGSortByParam sortByParam = new BGSortByParam();

                        var results = await service.GetBGListAsync(filter, pageParam, sortByParam);

                        filter = new BGFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BGSortBy)).Cast<BGSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BGSortByParam() { SortBy = item };
                            results = await service.GetBGListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO();
                        input.BGNo = "BG0001";
                        input.Name = "BG0001";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var service = new BGService(db);
                        var result = await service.CreateBGAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO();
                        input.BGNo = "BG0001";
                        input.Name = "BG0001";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var service = new BGService(db);
                        var resultCreate = await service.CreateBGAsync(input);
                        var result = await service.GetBGAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO();
                        input.BGNo = "BG0001";
                        input.Name = "BG0001";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var service = new BGService(db);
                        var resultCreate = await service.CreateBGAsync(input);
                        resultCreate.Name = "BG0002";
                        var result = await service.UpdateBGAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBGAsync()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType").FirstAsync();
                        var input = new BGDTO();
                        input.BGNo = "BG0001";
                        input.Name = "BG0001";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var service = new BGService(db);
                        var resultCreate = await service.CreateBGAsync(input);
                        resultCreate.Name = "BG0002";

                        var subBGService = new SubBGService(db);
                        var subBGInput = new SubBGDTO()
                        {
                            Name = "SUBBG0001",
                            SubBGNo = "S0001",
                            BG = new BGListDTO()
                            {
                                Id = resultCreate.Id.Value
                            }
                        };
                        await subBGService.CreateSubBGAsync(subBGInput);
                        subBGInput = new SubBGDTO()
                        {
                            Name = "SUBBG0002",
                            SubBGNo = "S0002",
                            BG = new BGListDTO()
                            {
                                Id = resultCreate.Id.Value
                            }
                        };
                        await subBGService.CreateSubBGAsync(subBGInput);

                        var project = await db.Projects.FirstAsync();
                        project.BGID = resultCreate.Id;
                        db.Update(project);
                        await db.SaveChangesAsync();
                        try
                        {
                            await service.DeleteBGAsync(resultCreate.Id.Value);
                        }
                        catch (ValidateException ex)
                        {
                            Trace.WriteLine("Validate Test: " + ex.ErrorResponse.PopupErrors[0].Message);
                        }
                        project.BGID = null;
                        db.Update(project);
                        await db.SaveChangesAsync();

                        var subBGs = await db.SubBGs.Where(o => o.BGID == resultCreate.Id.Value).ToListAsync();
                        Assert.NotEmpty(subBGs);
                        var result = await service.DeleteBGAsync(resultCreate.Id.Value);
                        subBGs = await db.SubBGs.Where(o => o.BGID == resultCreate.Id.Value).ToListAsync();
                        Assert.Empty(subBGs);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
