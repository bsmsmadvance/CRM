using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoFixture;
using Database.UnitTestExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Promotion.Services.Service;
using Base.DTOs.SAL;
using Database.Models.MasterKeys;

namespace Promotion.UnitTests
{
    public class TransferPromotionServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public TransferPromotionServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetTransferPromotionDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionDataAsync(new Guid("3A1B3324-E7AA-4398-90D6-DEF82B5CF568"));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionIDAsync(new Guid("2D4031C9-08B4-482E-BE8D-309B97F22E65"));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionItemListAsync(new Guid("70D94DCA-B8A3-4765-A79D-4D1555D03700"));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionExpenseListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionExpenseListAsync(new Guid());

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionExpensesDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionExpensesDraftAsync(new Guid("8F231EAF-BF29-4273-8FDF-5D5EF723CF2C"));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPromotionDrafDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.GetTransferPromotionDrafDataAsync(new Guid("5AD4BB1A-2E7B-4BEA-A31E-C425BE06A324"));

                        tran.Rollback();
                    }
                });
            }
        }
        
        [Fact]
        public async void CreateTransferPromotionDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);

                        var transferPromotion = await db.TransferPromotions.FirstAsync();
                        Guid transferPromotionID = transferPromotion.ID;

                        transferPromotionID = new Guid("70D94DCA-B8A3-4765-A79D-4D1555D03700");

                        var model = await service.GetTransferPromotionDataAsync(transferPromotionID);
                        var Receiver = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentReceiver).FirstAsync();

                        var datas = new List<TransferPromotionExpenseDTO>()
                        {
                           new TransferPromotionExpenseDTO
                           {
                               Amount = 3930900,
                               BuyerPayAmount = 12345,
                               SellerPayAmount = 29365,
                               Id= new Guid("3956bd1d-7adf-4bca-8d09-384ffe5da9ce"),
                               UpdatedBy = null,
                               Updated = null,
                               MasterPriceItem = new Base.DTOs.MST.MasterPriceItemDTO
                               {
                                    Id =  new Guid("4AE8AF32-E46F-457B-95F1-00E9EF680F73")

                                },
                               ExpenseReponsibleBy = new Base.DTOs.MST.MasterCenterDropdownDTO
                               {
                                 Id = new Guid("3A947173-FF0F-47EA-9337-27264E629624"),

                               },
                              PaymentReceiverMasterCenterID = Receiver.ID,

                           }
                        };


                        var result = await service.CreateTransferPromotionDataAsync(model, datas);

                        //tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateAllowTransferDiscountAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TransferPromotionService(db);

                        var transferPromotion = await db.TransferPromotions.FirstAsync();
                        Guid transferPromotionID = transferPromotion.ID;

                        transferPromotionID = new Guid("70D94DCA-B8A3-4765-A79D-4D1555D03700");

                        var model = await service.GetTransferPromotionDataAsync(transferPromotionID);
                        var Receiver = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentReceiver).FirstAsync();

                        var datas = new List<TransferPromotionExpenseDTO>()
                        {
                           new TransferPromotionExpenseDTO
                           {
                               Amount = 3930900,
                               BuyerPayAmount = 12345,
                               SellerPayAmount = 29365,
                               Id= new Guid("3956bd1d-7adf-4bca-8d09-384ffe5da9ce"),
                               UpdatedBy = null,
                               Updated = null,
                               MasterPriceItem = new Base.DTOs.MST.MasterPriceItemDTO
                               {
                                    Id =  new Guid("4AE8AF32-E46F-457B-95F1-00E9EF680F73")

                                },
                               ExpenseReponsibleBy = new Base.DTOs.MST.MasterCenterDropdownDTO
                               {
                                 Id = new Guid("3A947173-FF0F-47EA-9337-27264E629624"),

                               },
                              PaymentReceiverMasterCenterID = Receiver.ID,

                           }
                        };

                        //create
                        var resultcreate = await service.CreateTransferPromotionDataAsync(model, datas);

                        //update

                        var result = await service.UpdateAllowTransferDiscountAsync(resultcreate.Id.Value, model);

                        tran.Rollback();
                    }
                });
            }
        }

        
        [Fact]
        public async void UpdateAllowTransferDiscountOver3PercentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new TransferPromotionService(db);

                        var transferPromotion = await db.TransferPromotions.FirstAsync();
                        Guid transferPromotionID = transferPromotion.ID;

                        transferPromotionID = new Guid("70D94DCA-B8A3-4765-A79D-4D1555D03700");

                        var model = await service.GetTransferPromotionDataAsync(transferPromotionID);
                        var Receiver = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentReceiver).FirstAsync();

                        var datas = new List<TransferPromotionExpenseDTO>()
                        {
                           new TransferPromotionExpenseDTO
                           {
                               Amount = 3930900,
                               BuyerPayAmount = 12345,
                               SellerPayAmount = 29365,
                               Id= new Guid("3956bd1d-7adf-4bca-8d09-384ffe5da9ce"),
                               UpdatedBy = null,
                               Updated = null,
                               MasterPriceItem = new Base.DTOs.MST.MasterPriceItemDTO
                               {
                                    Id =  new Guid("4AE8AF32-E46F-457B-95F1-00E9EF680F73")

                                },
                               ExpenseReponsibleBy = new Base.DTOs.MST.MasterCenterDropdownDTO
                               {
                                 Id = new Guid("3A947173-FF0F-47EA-9337-27264E629624"),

                               },
                              PaymentReceiverMasterCenterID = Receiver.ID,

                           }
                        };

                        //create
                        var resultcreate = await service.CreateTransferPromotionDataAsync(model, datas);

                        //update

                        var resultDiscount = await service.UpdateAllowTransferDiscountAsync(resultcreate.Id.Value, model);
                        
                        //update

                        var result = await service.UpdateAllowTransferDiscountOver3PercentAsync(resultDiscount.Id.Value, model);

                        tran.Rollback();
                    }
                });
            }
        }

        #region Sprint3

        [Fact]
        public async void IsWaitingMinPriceApproveAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferPromotionService(db);
                        var results = await service.IsWaitingMinPriceApproveAsync(new Guid("CA8A6976-4431-441D-A856-32760A39A792"));

                        tran.Rollback();
                    }
                });
            }
        }

        #endregion

    }
}
