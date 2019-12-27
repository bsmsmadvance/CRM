using AutoFixture;
using CustomAutoFixture;
using Customer.Services.LeadSyncService;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Customer.UnitTests
{
    public class LeadSyncServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        private IConfiguration Configuration;

        public LeadSyncServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void SyncLeadsFromCRMAfterSale()
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
                            DateTime startTime = DateTime.Parse("2019-01-28");
                            DateTime endTime = DateTime.Parse("2019-03-30");

                            var service = new LeadSyncService(Configuration, db);
                            var result = await service.SyncLeadsFromCRMAfterSale(startTime, endTime);
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
        public async void SyncLeadsFromAPThaiWeb()
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
                            DateTime startTime = DateTime.Parse("2019-07-31");
                            DateTime endTime = DateTime.Parse("2019-07-31 23:59:59");

                            var service = new LeadSyncService(Configuration, db);
                            await service.SyncLeadsFromAPThaiWeb(startTime, endTime);
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
    }
}
