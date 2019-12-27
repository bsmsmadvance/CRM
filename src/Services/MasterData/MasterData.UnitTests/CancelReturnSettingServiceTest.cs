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
    public class CancelReturnSettingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetCancelReturnSettingAsync()
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

                        var service = new CancelReturnSettingService(db);
                        var result = await service.GetCancelReturnSettingAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateCancelReturnSettingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new CancelReturnSettingService(db);
                        var result = await service.GetCancelReturnSettingAsync();
                        result.HandlingFee = 66;
                        var resultUpdate = await service.UpdateCancelReturnSettingAsync(result.Id.Value, result);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
