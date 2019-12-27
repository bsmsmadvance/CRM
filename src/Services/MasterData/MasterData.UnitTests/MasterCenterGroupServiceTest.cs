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
    public class MasterCenterGroupServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetMasterCenterGroupListAsync()
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

                        var service = new MasterCenterGroupService(db);
                        MasterCenterGroupFilter filter = FixtureFactory.Get().Build<MasterCenterGroupFilter>().Create();
                        PageParam pageParam = new PageParam();
                        MasterCenterGroupSortByParam sortByParam = new MasterCenterGroupSortByParam();

                        var results = await service.GetMasterCenterGroupListAsync(filter, pageParam, sortByParam);

                        filter = new MasterCenterGroupFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterCenterGroupSortBy)).Cast<MasterCenterGroupSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterCenterGroupSortByParam() { SortBy = item };
                            results = await service.GetMasterCenterGroupListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMasterCenterGroupAsync()
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

                        var service = new MasterCenterGroupService(db);
                        var input = new MasterCenterGroupDTO
                        {
                            Key = "UnitTestMasterCenterGroup",
                            Name = "เทสกรุ๊ป"
                        };

                        var result = await service.CreateMasterCenterGroupAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterCenterGroupAsync()
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
                        var service = new MasterCenterGroupService(db);
                        var input = new MasterCenterGroupDTO
                        {
                            Key = "UnitTestMasterCenterGroup",
                            Name = "เทสกรุ๊ป"
                        };

                        var resultCreate = await service.CreateMasterCenterGroupAsync(input);

                        var result = await service.GetMasterCenterGroupAsync(resultCreate.Key);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterCenterGroupAsync()
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
                        var service = new MasterCenterGroupService(db);
                        var input = new MasterCenterGroupDTO
                        {
                            Key = "UnitTestMasterCenterGroup",
                            Name = "เทสกรุ๊ป"
                        };

                        var resultCreate = await service.CreateMasterCenterGroupAsync(input);
                        resultCreate.Name = "สื่อโฆษณา";
                        var result = await service.UpdateMasterCenterGroupAsync(resultCreate.Key, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMasterCenterGroupAsync()
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
                        var service = new MasterCenterGroupService(db);
                        var input = new MasterCenterGroupDTO
                        {
                            Key = "UnitTestMasterCenterGroup",
                            Name = "เทสกรุ๊ป"
                        };

                        var resultCreate = await service.CreateMasterCenterGroupAsync(input);

                        await service.DeleteMasterCenterGroupAsync(resultCreate.Key);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
