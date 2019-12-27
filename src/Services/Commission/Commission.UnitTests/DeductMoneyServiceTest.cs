using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models.CMS;
using Database.UnitTestExtensions;
using Commission.Params.Filters;
using Commission.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Commission.UnitTests
{
    public class DeductMoneyServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public DeductMoneyServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetDeductMoneyListAsync()
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

                        var service = new DeductMoneyService(db);
                        DeductMoneyFilter filter = FixtureFactory.Get().Build<DeductMoneyFilter>().Create();
                        PageParam pageParam = new PageParam();
                        DeductMoneySortByParam sortByParam = new DeductMoneySortByParam();
                        var results = await service.GetDeductMoneyListAsync(filter, pageParam, sortByParam);

                        filter = new DeductMoneyFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(DeductMoneySortBy)).Cast<DeductMoneySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new DeductMoneySortByParam() { SortBy = item };
                            results = await service.GetDeductMoneyListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateDeductMoneyAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");

                        //Put unit test here
                        var input = new IncreaseDeductMoneyDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.SaleUser = UserListDTO.CreateFromModel(sale);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.Remark = "∑¥ Õ∫";

                        var service = new DeductMoneyService(db);
                        var result = await service.CreateDeductMoneyAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetDeductMoneyAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");

                        //Put unit test here
                        var input = new IncreaseDeductMoneyDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.SaleUser = UserListDTO.CreateFromModel(sale);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.Remark = "∑¥ Õ∫";

                        var service = new DeductMoneyService(db);
                        var resultCreate = await service.CreateDeductMoneyAsync(input);

                        var result = await service.GetDeductMoneyAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateDeductMoneyAsync()
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
                        var service = new DeductMoneyService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");

                        //Put unit test here
                        var input = new IncreaseDeductMoneyDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.SaleUser = UserListDTO.CreateFromModel(sale);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.Remark = "∑¥ Õ∫";

                        var resultCreate = await service.CreateDeductMoneyAsync(input);
                        resultCreate.Amount = 999999;

                        var result = await service.UpdateDeductMoneyAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteDeductMoneyAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");

                        //Put unit test here
                        var input = new IncreaseDeductMoneyDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.SaleUser = UserListDTO.CreateFromModel(sale);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.Remark = "∑¥ Õ∫";

                        var service = new DeductMoneyService(db);
                        var resultCreate = await service.CreateDeductMoneyAsync(input);
                        await service.DeleteDeductMoneyAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
