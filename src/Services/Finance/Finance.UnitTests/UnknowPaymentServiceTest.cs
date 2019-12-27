using AutoFixture;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using Finance.Params.Filters;
using Finance.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using Xunit;
using ErrorHandling;

namespace Finance.UnitTests
{
    public class UnknowPaymentServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public UnknowPaymentServiceTest()
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetUnknowPaymentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = Guid.Parse("7CEFD37E-959D-435F-8BF8-BABFE7267798");

                        var service = new UnknownPaymentService(db);

                        var result = await service.GetUnknownPaymentAsync(id);
                    }
                });
            }
        }

        [Fact]
        public async void GetUnknowPaymentListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnknownPaymentService(db);
                        UnknownPaymentFilter filter = new UnknownPaymentFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnknownPaymentSortByParam sortByParam = new UnknownPaymentSortByParam();

                        //filter.ReceiveDateFrom = new DateTime(DateTime.Now.Year, 10, 14);
                        //filter.ReceiveDateTo = new DateTime(DateTime.Now.Year, 10, 16);

                        filter.IsPostPI = false;

                        var results = await service.GetUnknownPaymentListAsync(filter, pageParam, sortByParam);

                        //var id = Guid.Parse("");
                        //var result = await service.GetUnknownPaymentAsync(id);
                    }
                });
            }
        }

        [Fact]
        public async void TestCreateUnknowPayment()
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
                            var service = new UnknownPaymentService(db);

                            var Project = service.GetProjectDropdownListAsync("", Guid.Parse("5D462700-741C-4C83-98D2-BEF3B7425ADA"));
                            var Unit = service.GetSoldUnitDropdowListAsync("", Guid.Parse("C959460A-D4F2-49B2-8533-664204DDA641"));

                            var input = new UnknownPaymentDTO
                            {
                                UnknownPaymentCode = "",
                                ReceiveDate = DateTime.Now,
                                Company = new CompanyDropdownDTO { Id = Guid.Parse("5D462700-741C-4C83-98D2-BEF3B7425ADA") },
                                BankAccount = new BankAccountDropdownDTO { Id = Guid.Parse("4B159AB3-A7BA-46E6-A114-4D8A18A94519") },
                                Amount = 599,
                                Remark = "Unit Test",
                            };

                            var results = await service.CreateUnknownPaymentAsync(input);

                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                });
            }
        }

        [Fact]
        public async void GetUnknowPaymentForReverseAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new UnknownPaymentService(db);

                        var UnknowPaymentID = Guid.Parse("F1670536-69F6-4136-8AF0-AADD73306280");
                        var results = await service.GetUnknownPaymentForReverseAsync(UnknowPaymentID);

                        return;
                    }
                });
            }
        }

        [Fact]
        public async void UpdateUnknowPaymentAsync()
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
                            var service = new UnknownPaymentService(db);

                            var GUID = Guid.Parse("3E97076B-219F-4245-98FD-EDDE31CC36AB");

                            var unknownPaymentModel = await service.GetUnknownPaymentAsync(GUID);

                            if ((unknownPaymentModel.UnknownPaymentCode ?? "") != "")
                            {
                                unknownPaymentModel.Remark = "run UnitTest Update";

                                var results = await service.UpdateUnknownPaymentAsync(unknownPaymentModel);
                            }

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
        public async void ValidateBeforeUpdateAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = Guid.Parse("d1ee79f9-5230-463c-a96a-3865a5777f56");

                        var service = new UnknownPaymentService(db);

                        var result = await service.ValidateBeforeUpdateAsync(id, 2);
                    }
                });
            }
        }
    }
}
