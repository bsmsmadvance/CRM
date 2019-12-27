using System;
using System.Net.Http;
using AutoFixture;
using CustomAutoFixture;
using Database.UnitTestExtensions;
using Identity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Identity.UnitTests
{
    public class UserSyncServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        private IConfiguration Configuration;
        public UserSyncServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void SyncUserDataAsync()
        {
            using (var factory = new InMemoryDbContextFactory())
            {
                using (var db = factory.CreateContext())
                {
                    var service = new UserSyncService(Configuration, db);
                    await service.SyncUserDataAsync();
                }
            }
        }

        [Fact]
        public async void SyncRoleOfUserDataAsync()
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
                            var service = new UserSyncService(Configuration, db);
                            await service.SyncRoleOfUserDataAsync();
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
