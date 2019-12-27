using AutoFixture;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using CustomAutoFixture;
using Database.Models.MST;
using Database.UnitTestExtensions;
using ErrorHandling;
using Finance.Params.Filters;
using Finance.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Finance.UnitTests
{
    public class DepositServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public DepositServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetDepositListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new DepositService(db);
                        DepositFilter filter = FixtureFactory.Get().Build<DepositFilter>().Create();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        DepositSortByParam sortByParam = new DepositSortByParam();
                        var results = await service.GetDepositListAsync(filter, pageParam, sortByParam);

                        return;
                    }
                });
            }
        }

        [Fact]
        public async void GetDepositListForUpdateAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new DepositService(db);
                        DepositFilter filter = new DepositFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        DepositSortByParam sortByParam = new DepositSortByParam();

                        Guid id = Guid.Parse("4247f748-5fe7-470b-b424-14f72ae37902");

                        List<Guid> listNewID = new List<Guid>();
                        listNewID.Add(Guid.Parse("bd54dea9-9f6d-4028-9624-03a0ffa45684"));

                        var results = await service.GetDepositListForUpdateAsync(id, listNewID, filter, pageParam, sortByParam);
                    }
                });
            }
        }


        [Fact]
        public async void CreateDepositAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var service = new DepositService(db);
                            DepositFilter filter = new DepositFilter();
                            filter.PaymentMethodTypeID = Guid.Parse("06000000-0000-0000-0000-000000000000");

                            PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                            DepositSortByParam sortByParam = new DepositSortByParam();
                            var GetResult = await service.GetDepositListAsync(filter, pageParam, sortByParam);

                            //var input = GetResult.DepositDetails.Where(e => e.PaymentMethod.Id.Equals("91577919-2DE0-4192-ADB7-A595FB9A7330")).ToList() ?? new List<DepositDetailDTO>();
                            var input = GetResult.DepositDetails.ToList();

                            foreach (var item in input)
                            {
                                var BackAcc = db.BankAccounts.Where(o => o.ID == Guid.Parse("c9d19436-3a9b-49f2-8173-7bb9f25953a4")).FirstOrDefault() ?? new BankAccount();
                                var CompAcc = db.Companies.Where(o => o.ID == BackAcc.CompanyID).FirstOrDefault();

                                var DepositHeader = new DepositHeaderDTO();
                                DepositHeader.BankAccount = BankAccountDropdownDTO.CreateFromModel(BackAcc);
                                DepositHeader.Company = CompanyDTO.CreateFromModel(CompAcc);

                                DepositHeader.DepositDate = DateTime.Now;
                                DepositHeader.Remark = "Unit Test";

                                input.Select(c => { c.DepositHeader = DepositHeader; return c; }).ToList();
                            }

                            // Action
                            var results = await service.CreateDepositAsync(input);

                            tran.Rollback();
                        }
                        catch (ValidateException ex)
                        {
                            tran.Rollback();
                        }

                    }
                });
            }
        }

        [Fact]
        public async void DeleteLeadAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var service = new DepositService(db);
                        await service.DeleteDepositAsync(Guid.Parse("77b40a1c-b567-4211-ba16-4c1899431cc0"));

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void UpdateDepositAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var service = new DepositService(db);

                            var OldID = Guid.Parse("2F364801-795E-4683-9FB2-16222C48F401");

                            var newModel = await service.GetDepositAsync(OldID);

                            // Action
                            var results = await service.UpdateDepositAsync(OldID, newModel);

                            tran.Commit();
                        }
                        catch (ValidateException ex)
                        {
                            tran.Rollback();
                        }

                    }
                });
            }
        }
    }
}
