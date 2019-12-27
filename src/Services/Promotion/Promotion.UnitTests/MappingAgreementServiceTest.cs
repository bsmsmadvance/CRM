using System;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Promotion.Services;
using AutoFixture;
using CustomAutoFixture;
using Xunit;
using Base.DTOs;
using Database.Models.PRM;

namespace Promotion.UnitTests
{
    public class MappingAgreementServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public MappingAgreementServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        [Fact]
        public async void GetMappingAgreementsDataFromExcelAsync()
        {
            using (var factory = new InMemoryDbContextFactory())
            {
                using (var db = factory.CreateContext())
                {
                    MappingAgreementService service = new MappingAgreementService(Configuration, db);
                    FileDTO fileInput = new FileDTO()
                    {
                        Url = "http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx"
                    };
                    var results = await service.GetMappingAgreementsDataFromExcelAsync(fileInput);
                }
            }
        }

        [Fact]
        public async void ExportMappingAgreementsAsync()
        {
            using (var factory = new InMemoryDbContextFactory())
            {
                using (var db = factory.CreateContext())
                {
                    MappingAgreementService service = new MappingAgreementService(Configuration, db);
                    var results = await service.ExportMappingAgreementsAsync();
                }
            }
        }


        [Fact]
        public async void ConfirmImportMappingAgreementsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var data = FixtureFactory.Get().Build<MappingAgreement>()
                                          .With(o => o.IsDeleted, false)
                                          .Create();
                        await db.MappingAgreements.AddAsync(data);
                        await db.SaveChangesAsync();
                        var service = new MappingAgreementService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/Export_MappingAgreement.xlsx"
                        };
                        var results = await service.GetMappingAgreementsDataFromExcelAsync(fileInput);
                        var resultsComfirm = await service.ConfirmImportMappingAgreementsAsync(results);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
