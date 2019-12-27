using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Database.Models.CTM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Xunit;
using Customer.Services.VisitorService;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Customer.UnitTests
{
    public class VisitorServiceTest
    {
        IConfiguration Configuration;
        private static readonly Fixture Fixture = new Fixture();

        public VisitorServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetVisitorAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var visitor = await db.Visitors.FirstAsync(o => o.ContactID != null);
                        var service = new VisitorService(db, this.Configuration);

                        var result = await service.GetVisitorAsync(visitor.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetVisitorHistoryListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new VisitorService(db, this.Configuration);
                        var visitor = await db.Visitors.FirstAsync(o => o.ContactID != null);

                        var sortByParams = Enum.GetValues(typeof(VisitorHistoryListSortBy)).Cast<VisitorHistoryListSortBy>();
                        List<VisitorHistoryDTO> results = new List<VisitorHistoryDTO>();
                        foreach (var item in sortByParams)
                        {
                            var sortByParam = new VisitorHistoryListSortByParam() { SortBy = item };
                            results = await service.GetVisitorHistoryListAsync(visitor.ID, sortByParam);
                        }

                        Trace.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateVisitorAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        VisitorCreateDTO visitor = new VisitorCreateDTO()
                        {
                            VisitorRunning = "6071",
                            VisitDateIn = DateTime.Parse("2018-08-06"),
                            National = "ไทย",
                            VisitKioskTransactionID = "0001",
                            VisitKioskDeviceID = "0001",
                            CitizenIdentityNo = "3829900044034",
                        };

                        visitor.ContactStatusKey = "o";
                        visitor.VehicleKey = "4";
                        visitor.VisitByKey = "D";
                        visitor.PersonalVisitCardTypeKey = "D";
                        visitor.ProjectNo = "10208";

                        // Act
                        var service = new VisitorService(db, this.Configuration);
                        var results = await service.CreateVisitorAsync(visitor);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void SubmitOrUnSubmitVisitorWelcomeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        VisitorCreateDTO visitor = new VisitorCreateDTO()
                        {
                            VisitorRunning = "6071",
                            VisitDateIn = DateTime.Parse("2018-08-06"),
                            National = "ไทย",
                            VisitKioskTransactionID = "0001",
                            VisitKioskDeviceID = "0001",
                            CitizenIdentityNo = "3829900044034",
                        };

                        visitor.ContactStatusKey = "o";
                        visitor.VehicleKey = "4";
                        visitor.VisitByKey = "D";
                        visitor.PersonalVisitCardTypeKey = "D";
                        visitor.ProjectNo = "40037";

                        var user = await db.Users.Where(o => o.EmployeeNo == "CR000749").FirstOrDefaultAsync();

                        // Act
                        var service = new VisitorService(db, this.Configuration);
                        var results = await service.CreateVisitorAsync(visitor);

                        var submitResult = await service.SubmitOrUnSubmitVisitorWelcomeAsync(results.Id.Value, true, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
