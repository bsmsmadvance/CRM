using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.FIN;
using Database.Models.MST;
using Database.UnitTestExtensions;
using MasterData.Params.Filters;
using MasterData.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace MasterData.UnitTests
{
    public class EDCsServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public EDCsServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetEDCDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        var projectID = await db.Projects.Select(o => o.ID).FirstAsync();
                        var results = await service.GetEDCDropdownListUrlAsync(projectID, "ก");

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void GetEDCListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCFilter filter = FixtureFactory.Get().Build<EDCFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCSortByParam sortByParam = new EDCSortByParam();
                        filter.ProjectStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProjectStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineType")
                                                                         .Select(x => x.Key).FirstAsync();
                        var results = await service.GetEDCListAsync(filter, pageParam, sortByParam);
                        filter = new EDCFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCSortBy)).Cast<EDCSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCSortByParam() { SortBy = item };
                            results = await service.GetEDCListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetEDCDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var resultCreate = await service.CreateEDCAsync(data);

                        var result = await service.GetEDCDetailAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var result = await service.CreateEDCAsync(data);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var result = await service.CreateEDCAsync(data);
                        result.Remark = "TestUpdate";
                        var resultupdate = await service.UpdateEDCAsync(result.Id.Value, result);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteEDCAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var bank = await db.Banks.FirstAsync();
                        var cardMachineType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineType").FirstAsync();
                        var cardMachineStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CardMachineStatus").FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var service = new EDCService(db, Configuration);

                        //Put unit test here
                        var data = new EDCDTO()
                        {
                            Code = "00002",
                            Bank = BankDropdownDTO.CreateFromModel(bank),
                            CardMachineType = MasterCenterDropdownDTO.CreateFromModel(cardMachineType),
                            CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(cardMachineStatus),
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            TelNo = "2222",
                            Remark = "Test",
                            ReceiveBy = "Test",
                            ReceiveDate = new DateTime(),
                        };

                        var resultCreate = await service.CreateEDCAsync(data);
                        await service.DeleteEDCAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetEDCBankListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCBankFilter filter = FixtureFactory.Get().Build<EDCBankFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCBankSortByParam sortByParam = new EDCBankSortByParam();

                        var results = await service.GetEDCBankListAsync(filter, pageParam, sortByParam);

                        filter = new EDCBankFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCBankSortBy)).Cast<EDCBankSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCBankSortByParam() { SortBy = item };
                            results = await service.GetEDCBankListAsync(filter, pageParam, sortByParam);
                        }
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetEDCFeeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        //Put unit test here

                        EDCFeeFilter filter = FixtureFactory.Get().Build<EDCFeeFilter>().Create();
                        PageParam pageParam = new PageParam();
                        EDCFeeSortByParam sortByParam = new EDCFeeSortByParam();
                        filter.CreditCardPaymentTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CreditCardPaymentType")
                                                                       .Select(x => x.Key).FirstAsync();
                        filter.CreditCardTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CreditCardType")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.PaymentCardTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PaymentCardType")
                                                                         .Select(x => x.Key).FirstAsync();
                        var results = await service.GetEDCFeeListAsync(filter, pageParam, sortByParam);

                        filter = new EDCFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(EDCFeeSortBy)).Cast<EDCFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new EDCFeeSortByParam() { SortBy = item };
                            results = await service.GetEDCFeeListAsync(filter, pageParam, sortByParam);
                        }
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO();
                        input.Fee = 50;
                        input.PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType);
                        input.CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType);
                        input.CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType);

                        var result = await service.CreateEDCFeeAsync(input);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO();
                        input.Fee = 50;
                        input.PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType);
                        input.CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType);
                        input.CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType);

                        var resultCreate = await service.CreateEDCFeeAsync(input);

                        resultCreate.Fee = 50;
                        var result = await service.UpdateEDCFeeAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteEDCFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").FirstAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").FirstAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").FirstAsync();

                        //Put unit test here
                        var input = new EDCFeeDTO();
                        input.Fee = 50;
                        input.PaymentCardType = MasterCenterDropdownDTO.CreateFromModel(paymentCardType);
                        input.CreditCardType = MasterCenterDropdownDTO.CreateFromModel(creditCardType);
                        input.CreditCardPaymentType = MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType);

                        var resultCreate = await service.CreateEDCFeeAsync(input);
                        //Put unit test here
                        await service.DeleteEDCFeeAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportEDCListUrlAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new EDCService(db, Configuration);

                        EDCFilter filter = FixtureFactory.Get().Build<EDCFilter>().Create();
                        filter.ProjectStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProjectStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineStatus")
                                                                         .Select(x => x.Key).FirstAsync();
                        filter.CardMachineTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "CardMachineType")
                                                                         .Select(x => x.Key).FirstAsync();
                        var result = await service.ExportEDCListUrlAsync(filter, Report.Integration.ShowAs.PDF);
                        filter = new EDCFilter();
                        result = await service.ExportEDCListUrlAsync(filter, Report.Integration.ShowAs.Excel);

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void GetFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var edc = await db.EDCs.FirstAsync();
                        var bank = await db.Banks.Where(o => o.Alias == "KTC").FirstAsync();
                        var creditCardTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType" && o.Key == "1").Select(o => o.ID).FirstAsync();
                        var creditCardPaymentTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType" && o.Key == "2").Select(o => o.ID).FirstAsync();
                        var paymentCardTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType" && o.Key == "1").Select(o => o.ID).FirstAsync();
                        decimal paidAmount = 500000;

                        var service = new EDCService(db, Configuration);

                        //Put unit test here


                        var result = await service.GetFeeAsync(edc.ID, bank.ID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID, paidAmount);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetFeePercentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var edc = await db.EDCs.FirstAsync();
                        var bank = await db.Banks.Where(o => o.Alias == "KTC").FirstAsync();
                        var creditCardTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType" && o.Key == "1").Select(o => o.ID).FirstAsync();
                        var creditCardPaymentTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType" && o.Key == "2").Select(o => o.ID).FirstAsync();
                        var paymentCardTypeMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType" && o.Key == "1").Select(o => o.ID).FirstAsync();

                        var service = new EDCService(db, Configuration);

                        //Put unit test here


                        var result = await service.GetFeePercentAsync(edc.ID, bank.ID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID);

                        tran.Rollback();
                    }
                });
            }
        }


    }
}
