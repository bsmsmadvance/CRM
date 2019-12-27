using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Project.UnitTests
{
    public class RoundFeeServiceTest
    {
        IConfiguration Configuration;
        public RoundFeeServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetRoundFeeListAsync()
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

                        var service = new RoundFeeService(db);

                        RoundFeeFilter filter = FixtureFactory.Get().Build<RoundFeeFilter>().Create();
                        filter.TransferFeeRoundFormulaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RoundFormulaType").Select(o => o.Key).FirstAsync();
                        filter.BusinessTaxRoundFormulaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RoundFormulaType").Select(o => o.Key).FirstAsync();
                        filter.LocalTaxRoundFormulaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RoundFormulaType").Select(o => o.Key).FirstAsync();
                        filter.IncomeTaxRoundFormulaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RoundFormulaType").Select(o => o.Key).FirstAsync();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        RoundFeeSortByParam sortByParam = new RoundFeeSortByParam();
                        var results = await service.GetRoundFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new RoundFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RoundFeeSortBy)).Cast<RoundFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RoundFeeSortByParam() { SortBy = item };
                            results = await service.GetRoundFeeListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRoundFeeAsync()
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

                        var service = new RoundFeeService(db);
                        var model = await db.RoundFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetRoundFeeAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRoundFeeAsync()
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

                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new RoundFeeService(db);
                        var input = new RoundFeeDTO();
                        input.OtherFee = 1000;
                        input.LandOffice = Base.DTOs.MST.LandOfficeListDTO.CreateFromModel(landOffice);

                        var result = await service.CreateRoundFeeAsync(project.ID, input);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRoundFeeAsync()
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

                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new RoundFeeService(db);
                        var input = new RoundFeeDTO();
                        input.OtherFee = 1000;
                        input.LandOffice = Base.DTOs.MST.LandOfficeListDTO.CreateFromModel(landOffice);

                        var resultCreate = await service.CreateRoundFeeAsync(project.ID, input);
                        resultCreate.OtherFee = 5555;
                        var result = await service.UpdateRoundFeeAsync(project.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRoundFeeAsync()
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

                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();

                        var service = new RoundFeeService(db);
                        var input = new RoundFeeDTO();
                        input.OtherFee = 1000;
                        input.LandOffice = Base.DTOs.MST.LandOfficeListDTO.CreateFromModel(landOffice);

                        var resultCreate = await service.CreateRoundFeeAsync(project.ID, input);

                        await service.DeleteRoundFeeAsync(project.ID, resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
