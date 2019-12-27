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

namespace MasterData.UnitTests
{
    public class BankAccountServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetBankAccountDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);
                        var results = await service.GetBankAccountDropdownListAsync("1","",Guid.Empty);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBankAccountListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here

                        BankAccountFilter filter = FixtureFactory.Get().Build<BankAccountFilter>().Create();
                        PageParam pageParam = new PageParam();
                        BankAccountSortByParam sortByParam = new BankAccountSortByParam();
                        filter.BankAccountTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "BankAccountType")
                                                                          .Select(x => x.Key).FirstAsync();
                        filter.GLAccountTypeKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "GLAccountType").Select(o => o.Key).FirstAsync();
                        var results = await service.GetBankAccountListAsync(filter, pageParam, sortByParam);

                        filter = new BankAccountFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BankAccountSortBy)).Cast<BankAccountSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BankAccountSortByParam() { SortBy = item };
                            results = await service.GetBankAccountListAsync(filter, pageParam, sortByParam);
                        }


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBankAccountDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var data = FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create();
                        await db.BankAccounts.AddAsync(data);
                        await db.SaveChangesAsync();

                        var result = await service.GetBankAccountDetailAsync(data.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        var company = await db.Companies.FirstAsync();
                        var bankbranch = await db.BankBranches.FirstAsync();
                        var bank = await db.Banks.Where(o => o.ID == bankbranch.BankID).FirstAsync();
                        var province = await db.Provinces.FirstAsync();
                        var bankAccountType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BankAccountType").FirstAsync();

                        BankAccountDTO input = new BankAccountDTO();
                        input.GLAccountNo = "222222";
                        input.BankAccountNo = "111111";
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.Company = CompanyDropdownDTO.CreateFromModel(company);
                        input.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankbranch);
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccountType);
                        input.MerchantID = "11111";
                        input.ServiceCode = "22222";
                        input.IsPCard = true;

                        var result = await service.CreateBankAccountAsync(input);

                        //Validate P.Card Test
                        input = new BankAccountDTO();
                        input.GLAccountNo = "aaaaaa";
                        input.BankAccountNo = "bbbbbb";
                        input.Province = ProvinceListDTO.CreateFromModel(province);
                        input.Company = CompanyDropdownDTO.CreateFromModel(company);
                        input.BankBranch = BankBranchDropdownDTO.CreateFromModel(bankbranch);
                        input.Bank = BankDropdownDTO.CreateFromModel(bank);
                        input.BankAccountType = MasterCenterDropdownDTO.CreateFromModel(bankAccountType);
                        input.MerchantID = "aaaaa";
                        input.ServiceCode = "bbbbb";
                        input.IsPCard = true;

                        try
                        {
                            result = await service.CreateBankAccountAsync(input);
                        }
                        catch (ValidateException ex)
                        {
                            Assert.NotEmpty(ex.ErrorResponse.PopupErrors.Where(o => o.Code == "ERR0056").ToList());
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateChartOfAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);
                        var glAccountType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "glAccountType" && o.Key == "3").FirstAsync();

                        BankAccountDTO input = new BankAccountDTO();
                        input.GLAccountNo = "222222";
                        input.BankAccountNo = "111111";
                        input.GLAccountType = MasterCenterDropdownDTO.CreateFromModel(glAccountType);
                        input.Name = "Testtt";

                        var result = await service.CreateChartOfAccountAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var glAccountType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "glAccountType" && o.Key == "3").FirstAsync();

                        BankAccountDTO input = new BankAccountDTO();
                        input.GLAccountNo = "222222";
                        input.BankAccountNo = "111111";
                        input.GLAccountType = MasterCenterDropdownDTO.CreateFromModel(glAccountType);
                        input.Name = "Testtt";

                        var resultCreate = await service.CreateBankAccountAsync(input);
                        resultCreate.BankAccountNo = "5555555";
                        var resultupdate = await service.UpdateBankAccountAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBankAccountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        var data = FixtureFactory.Get().Build<BankAccount>()
                                  .With(o => o.BankAccountNo, "B-0001")
                                  .With(o => o.IsDeleted, false)
                                  .Create();
                        await db.BankAccounts.AddAsync(data);
                        await db.SaveChangesAsync();
                        await service.DeleteBankAccountAsync(data.ID);

                        BankAccountFilter bankAccountFilter = new BankAccountFilter();
                        PageParam pagparam = new PageParam();
                        BankAccountSortByParam sortByParam = new BankAccountSortByParam();

                        var results = await service.GetBankAccountListAsync(bankAccountFilter, pagparam, sortByParam);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBankAccountListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BankAccountService(db);

                        //Put unit test here
                        List<BankAccount> bankAccounts = new List<BankAccount>()
                        {
                            FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<BankAccount>()
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.BankAccounts.AddRangeAsync(bankAccounts);
                        await db.SaveChangesAsync();

                        BankAccountFilter bankAccountFilter = new BankAccountFilter();
                        PageParam pagparam = new PageParam();
                        BankAccountSortByParam sortByParam = new BankAccountSortByParam();

                        var results = await service.GetBankAccountListAsync(bankAccountFilter, pagparam, sortByParam);
                        await service.DeleteBankAccountListAsync(results.BankAccounts);
                        var resultsDelete = await service.GetBankAccountListAsync(bankAccountFilter, pagparam, sortByParam);


                        tran.Rollback();
                    }
                });
            }
        }
    }
}
