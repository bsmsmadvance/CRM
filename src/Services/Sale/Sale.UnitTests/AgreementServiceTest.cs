using AutoFixture;
using Base.DTOs;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sale.UnitTests
{
    public class AgreementServiceTest
    {
        IConfiguration Configuration;
        private static readonly Fixture Fixture = new Fixture();

        public AgreementServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
        }

        [Fact]
        public async void ConvertToAgreementAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var dbQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var booking = await db.Bookings.Where(o => o.ID == new Guid("026C6EFC-94A6-40B4-BED4-7CF55D4A555E")).FirstAsync();

                        // Act
                        var service = new AgreementService(db, this.Configuration);
                        var results = await service.ConvertToAgreementAsync(booking.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateAgreementAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var agreement = await db.Agreements.Where(o => o.ID == new Guid("832ACE49-E008-434B-977A-A30E9B2FF879")).FirstAsync();

                        // Act
                        var service = new AgreementService(db, this.Configuration);
                        var results = await service.CreateAgreementAsync(agreement.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetAgreementFileListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);
                var results = await service.GetAgreementFileListAsync(new Guid("C302F0B7-E3C8-476C-9D7E-CE5B0FE1A375"));

                var x = 0;
            }
        }

        [Fact]
        public async void CreateAgreementFileAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);
                var fl = new List<FileDTO>();
                var f = new FileDTO();
                f.IsTemp = false;
                f.Name = "TestFile.pdf";
                f.Url = "https://miro.medium.com/fit/c/80/80/1*J8tG1NbQh0gVNcofbTGT2g.jpeg";
                fl.Add(f);
                var results = await service.CreateAgreementFileAsync(new Guid("C302F0B7-E3C8-476C-9D7E-CE5B0FE1A375"), fl, new Guid("63B403B6-AF5B-4AE2-9F72-011B6C4CFD4D"));

                var x = 0;
            }
        }

        [Fact]
        public async void CreateNotificationAgreementFileAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                service.CreateNotificationAgreementFileAsync(new Guid("C302F0B7-E3C8-476C-9D7E-CE5B0FE1A375"), "Error60000", new Guid("63B403B6-AF5B-4AE2-9F72-011B6C4CFD4D"));

                var x = 0;
            }
        }

        [Fact]
        public async void GetAgreementAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var item = service.GetAgreementAsync(new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318"));

                var result = item.Result;

                return;
            }
        }


        [Fact]
        public async void GetAgreementByUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var item = service.GetAgreementByUnitAsync(new Guid("0296dd5f-3ffa-4338-a1f3-d9dd4454b5ba"));

                var result = item.Result;

                return;
            }
        }

        [Fact]
        public async void CreateAgreementPrintingHistoryDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                service.CreateAgreementPrintingHistoryDataAsync(new Guid("A104EBC6-8E89-4E20-AE2C-AEB602BE95CF"), new Guid("63B403B6-AF5B-4AE2-9F72-011B6C4CFD4D"));

                var x = 0;
            }
        }

        [Fact]
        public async void GetPriceListDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var item = service.GetPriceListDataAsync(new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318"));

                var i = item.Result;

                var x = 0;
            }
        }

        [Fact]
        public async void GetAgreementPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var item = service.GetAgreementPriceListAsync(new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318"));

                var i = item.Result;

                var x = 0;
            }
        }

        [Fact]
        public async void GetAgreementInstallmentDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var item = service.GetAgreementInstallmentDataAsync(new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318"));

                var i = item.Result;

                var x = 0;
            }
        }

        [Fact]
        public async void CalculateAgreementInstallmentDataAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var x = service.GetAgreementPriceListAsync(new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318"));

                var model = x.Result;

                var item = service.CalculateAgreementInstallmentDataAsync(model);

                var i = item.Result;

                var z = 0;
            }
        }

        [Fact]
        public async void GetAgreementListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var service = new AgreementService(db, this.Configuration);

                var filter = new AgreementListFilter();

                var pageParam = new PageParam();

                var sortByParam = new AgreementListSortByParam();

                var item = service.GetAgreementListAsync(filter, pageParam, sortByParam);

                var i = item.Result;

                var z = 0;
            }
        }
    }
}
