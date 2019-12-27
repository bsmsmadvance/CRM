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
    public class LegalEntityServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetLegalEntityListAsync()
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

                        var service = new LegalEntityService(db);
                        LegalFilter filter = FixtureFactory.Get().Build<LegalFilter>().Create();
                        PageParam pageParam = new PageParam();
                        LegalEntitySortByParam sortByParam = new LegalEntitySortByParam();
                        var results = await service.GetLegalEntityListAsync(filter, pageParam, sortByParam);

                        filter = new LegalFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LegalEntitySortBy)).Cast<LegalEntitySortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LegalEntitySortByParam() { SortBy = item };
                            results = await service.GetLegalEntityListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLegalEntityAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var service = new LegalEntityService(db);
                        var input = new LegalEntityDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType);
                        input.BankAccountNo = "222222";

                        var result = await service.CreateLegalEntityAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLegalEntityAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var service = new LegalEntityService(db);
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var input = new LegalEntityDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountNo = "222222";
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType);

                        var resultCreate = await service.CreateLegalEntityAsync(input);

                        var result = await service.GetLegalEntityAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateLegalEntityAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var service = new LegalEntityService(db);
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var input = new LegalEntityDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountNo = "222222";
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType);

                        var resultCreate = await service.CreateLegalEntityAsync(input);
                        resultCreate.NameTH = "เทส";
                        resultCreate.NameEN = "TestTest";
                        var result = await service.UpdateLegalEntityAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLegalEntityAsync()
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
                        var bank = await db.Banks.FirstAsync();
                        var bankAccType = await db.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "BankAccountType");
                        var service = new LegalEntityService(db);
                        var input = new LegalEntityDTO();
                        input.NameTH = "เทส";
                        input.NameEN = "Test";
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountNo = "222222";
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccType);

                        var resultCreate = await service.CreateLegalEntityAsync(input);

                        await service.DeleteLegalEntityAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
