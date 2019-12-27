using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using CustomAutoFixture;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class TransferServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public TransferServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferDrafAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Agreements.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferDrafAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferPriceAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferPriceAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferFeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferFeeAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferMoneyAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferMoneyAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ValidateCreateTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Agreements.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.ValidateCreateTransferAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferOwnerDrafAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Agreements.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferOwnerDrafAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferOwnerAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.TransferOwners.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferOwnerAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTransferOwnerAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferService(db, Configuration);

                        var id = await db.TransferOwners.Select(o => o.ID).FirstAsync();

                        var transferOwner = await service.GetTransferOwnerAsync(id);

                        var results = await service.UpdateTransferOwnerAsync(id, transferOwner);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetTransferOwnerListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetTransferOwnerListAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CopyContactAddressAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var Id = await db.Contacts.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.CopyContactAddressAsync(Id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CopyProjectAddressAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var Id = await db.Projects.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.CopyProjectAddressAsync(Id);

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void CreateTransferDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferService(db, Configuration);

                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var transferDb = await service.GetTransferAsync(id);

                        var transfer = new TransferDTO();

                        transfer.ScheduleTransferDate = transferDb.ScheduleTransferDate;
                        transfer.TransferSale = transferDb.TransferSale;

                        transfer.Project = transferDb.Project;
                        transfer.Unit = transferDb.Unit;
                        transfer.Agreement = transferDb.Agreement;
                        transfer.TransferSale = transferDb.TransferSale;
                        transfer.MeterCheque = transferDb.MeterCheque;

                        var results = await service.CreateTransferDataAsync(transfer);

                        // tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateTransferDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new TransferService(db, Configuration);

                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var transfer = await service.GetTransferAsync(id);

                        var results = await service.UpdateTransferDataAsync(id, transfer);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.DeleteTransferAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPaymentDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetPaymentDetailAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetReceiptDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var id = await db.Transfers.Select(o => o.ID).FirstAsync();

                        var service = new TransferService(db, Configuration);
                        var results = await service.GetReceiptDetailAsync(id);

                        tran.Rollback();
                    }
                });
            }
        }


    }
}
