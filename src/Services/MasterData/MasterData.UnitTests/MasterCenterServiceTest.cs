using AutoFixture;
using CustomAutoFixture;
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
    public class MasterCenterServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetMasterCenterListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here

                        var service = new MasterCenterService(db, dbQuery);
                        MasterCenterFilter filter = FixtureFactory.Get().Build<MasterCenterFilter>().Create();
                        PageParam pageParam = new PageParam();
                        MasterCenterSortByParam sortByParam = new MasterCenterSortByParam();

                        var results = await service.GetMasterCenterListAsync(filter, pageParam, sortByParam);

                        filter = new MasterCenterFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterCenterSortBy)).Cast<MasterCenterSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterCenterSortByParam() { SortBy = item };
                            results = await service.GetMasterCenterListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetFindMasterCenterDropdownItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here

                        var service = new MasterCenterService(db, dbQuery);
                        var result = await service.GetFindMasterCenterDropdownItemAsync("UnitDirection", "N");
                        Assert.NotNull(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMasterCenterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here
                        var masterCenterGroupKey = new MasterCenterGroup
                        {
                            Key = "Test",
                            Name = "Test"
                        };
                        await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                        await db.SaveChangesAsync();

                        var service = new MasterCenterService(db, dbQuery);
                        var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                           .With(o => o.Key, "UnitTestMasterCenter")
                                           .With(o => o.Name, "เทส")
                                           .Create();

                        var result = await service.CreateMasterCenterAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterCenterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here
                        var masterCenterGroupKey = new MasterCenterGroup
                        {
                            Key = "Test",
                            Name = "Test"
                        };
                        await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                        await db.SaveChangesAsync();

                        var service = new MasterCenterService(db, dbQuery);
                        var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                           .With(o => o.Key, "UnitTestMasterCenter")
                                           .With(o => o.Name, "เทส")
                                           .Create();

                        var resultCreate = await service.CreateMasterCenterAsync(input);

                        var result = await service.GetMasterCenterAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterCenterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here
                        var masterCenterGroupKey = new MasterCenterGroup
                        {
                            Key = "Test",
                            Name = "Test"
                        };
                        await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                        await db.SaveChangesAsync();

                        var service = new MasterCenterService(db, dbQuery);
                        var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                           .With(o => o.Key, "UnitTestMasterCenter")
                                           .With(o => o.Name, "เทส")
                                           .Create();

                        var resultCreate = await service.CreateMasterCenterAsync(input);
                        resultCreate.Name = "เทส1";
                        var result = await service.UpdateMasterCenterAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMasterCenterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //Put unit test here
                        var masterCenterGroupKey = new MasterCenterGroup
                        {
                            Key = "Test",
                            Name = "Test"
                        };
                        await db.MasterCenterGroups.AddAsync(masterCenterGroupKey);
                        await db.SaveChangesAsync();

                        var service = new MasterCenterService(db, dbQuery);
                        var input = FixtureFactory.Get().Build<MasterCenterDTO>()
                                           .With(o => o.Id, (Guid?)null)
                                           .With(o => o.MasterCenterGroup, MasterCenterGroupListDTO.CreateFromModel(masterCenterGroupKey))
                                           .With(o => o.Key, "UnitTestMasterCenter")
                                           .With(o => o.Name, "เทส")
                                           .Create();

                        var resultCreate = await service.CreateMasterCenterAsync(input);

                        await service.DeleteMasterCenterAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterCenterUsingDbQueryAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterCenterService(db, dbQuery);

                        var results = await service.GetMasterCenterUsingDbQueryAsync("ProjectStatus");

                        tran.Rollback();
                    }
                });
            }
        }


    }
}
