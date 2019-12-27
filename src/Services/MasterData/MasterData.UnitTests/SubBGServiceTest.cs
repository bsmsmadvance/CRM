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
    public class SubBGServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();


        [Fact]
        public async void GetSubBGListAsync()
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

                        var service = new SubBGService(db);
                        SubBGFilter filter = FixtureFactory.Get().Build<SubBGFilter>().Create();
                        PageParam pageParam = new PageParam();
                        SubBGSortByParam sortByParam = new SubBGSortByParam();

                        var results = await service.GetSubBGListAsync(filter, pageParam, sortByParam);

                        filter = new SubBGFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(SubBGSortBy)).Cast<SubBGSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new SubBGSortByParam() { SortBy = item };
                            results = await service.GetSubBGListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void CreateSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO();
                        input.Name = "SubBGTEST";
                        input.SubBGNo = "555";
                        input.BG = BGListDTO.CreateFromModel(bg);

                        var service = new SubBGService(db);

                        var result = await service.CreateSubBGAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO();
                        input.Name = "SubBGTEST";
                        input.SubBGNo = "555";
                        input.BG = BGListDTO.CreateFromModel(bg);

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);

                        var result = await service.GetSubBGAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO();
                        input.Name = "SubBGTEST";
                        input.SubBGNo = "555";
                        input.BG = BGListDTO.CreateFromModel(bg);

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);
                        resultCreate.Name = "SubBGTEST2";
                        var result = await service.UpdateSubBGAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteSubBGAsync()
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
                        var bg = await db.BGs.FirstAsync();
                        var input = new SubBGDTO();
                        input.Name = "SubBGTEST";
                        input.SubBGNo = "555";
                        input.BG = BGListDTO.CreateFromModel(bg);

                        var service = new SubBGService(db);

                        var resultCreate = await service.CreateSubBGAsync(input);

                        await service.DeleteSubBGAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
