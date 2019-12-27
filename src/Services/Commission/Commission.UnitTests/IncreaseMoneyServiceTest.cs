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
    public class IncreaseMoneyServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public IncreaseMoneyServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetIncreaseMoneyListAsync()
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

                        var service = new IncreaseMoneyService(db);
                        IncreaseMoneyFilter filter = FixtureFactory.Get().Build<IncreaseMoneyFilter>().Create();
                        PageParam pageParam = new PageParam();
                        IncreaseMoneySortByParam sortByParam = new IncreaseMoneySortByParam();
                        var results = await service.GetIncreaseMoneyListAsync(filter, pageParam, sortByParam);

                        filter = new IncreaseMoneyFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(IncreaseMoneySortBy)).Cast<IncreaseMoneySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new IncreaseMoneySortByParam() { SortBy = item };
                            results = await service.GetIncreaseMoneyListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateIncreaseMoneyAsync()
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

                        var service = new IncreaseMoneyService(db);
                        var result = await service.CreateIncreaseMoneyAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetIncreaseMoneyAsync()
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

                        var service = new IncreaseMoneyService(db);
                        var resultCreate = await service.CreateIncreaseMoneyAsync(input);

                        var result = await service.GetIncreaseMoneyAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateIncreaseMoneyAsync()
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
                        var service = new IncreaseMoneyService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");

                        //Put unit test here
                        var input = new IncreaseDeductMoneyDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.SaleUser = UserListDTO.CreateFromModel(sale);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.Remark = "∑¥ Õ∫";

                        var resultCreate = await service.CreateIncreaseMoneyAsync(input);
                        resultCreate.Amount = 999999;

                        var result = await service.UpdateIncreaseMoneyAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteIncreaseMoneyAsync()
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

                        var service = new IncreaseMoneyService(db);
                        var resultCreate = await service.CreateIncreaseMoneyAsync(input);
                        await service.DeleteIncreaseMoneyAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
