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
using ErrorHandling;

namespace MasterData.UnitTests
{
    public class CancelReasonServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetCancelReasonListAsync()
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

                        var service = new CancelReasonService(db);
                        CancelReasonFilter filter = FixtureFactory.Get().Build<CancelReasonFilter>().Create();
                        PageParam pageParam = new PageParam();
                        CancelReasonSortByParam sortByParam = new CancelReasonSortByParam();
                        filter.GroupOfCancelReasonKey = "1";
                        filter.CancelApproveFlowKey = "1";
                        var results = await service.GetCancelReasonListAsync(filter, pageParam, sortByParam);

                        filter = new CancelReasonFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(CancelReasonSortBy)).Cast<CancelReasonSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new CancelReasonSortByParam() { SortBy = item };
                            results = await service.GetCancelReasonListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateCancelReasonAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO();
                        input.Key = "792";
                        input.Description = "ทดสอบabc123#, -, _ ";
                        input.GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason);
                        input.CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow);
                        var result = await service.CreateCancelReasonAsync(input);

                        //Test Unique "Description"
                        input = new CancelReasonDTO();
                        input.Key = "876";
                        input.Description = "ทดสอบabc123#, -, _ ";
                        input.GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason);
                        input.CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow);
                        try
                        {
                            result = await service.CreateCancelReasonAsync(input);
                        }
                        catch (ValidateException ex)
                        {
                            Assert.NotEmpty(ex.ErrorResponse.FieldErrors.Where(o => o.Code == "ERR0042").ToList());
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO();
                        input.Key = "792";
                        input.Description = "ทดสอบ";
                        input.GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason);
                        input.CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow);
                        var resultCreate = await service.CreateCancelReasonAsync(input);

                        var result = await service.GetCancelReasonAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO();
                        input.Key = "792";
                        input.Description = "ทดสอบ";
                        input.GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason);
                        input.CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow);
                        var resultCreate = await service.CreateCancelReasonAsync(input);
                        resultCreate.Key = "5555";

                        var result = await service.UpdateCancelReasonAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteCancelReasonAsync()
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
                        var service = new CancelReasonService(db);

                        var groupOfCancelReason = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GroupOfCancelReason").FirstAsync();
                        var cancelApproveFlow = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CancelApproveFlow").FirstAsync();
                        //Put unit test here
                        var input = new CancelReasonDTO();
                        input.Key = "792";
                        input.Description = "ทดสอบ";
                        input.GroupOfCancelReason = MasterCenterDropdownDTO.CreateFromModel(groupOfCancelReason);
                        input.CancelApproveFlow = MasterCenterDropdownDTO.CreateFromModel(cancelApproveFlow);
                        var resultCreate = await service.CreateCancelReasonAsync(input);

                        await service.DeleteCancelReasonAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
