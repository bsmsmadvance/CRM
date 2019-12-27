using System;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Services;
using Xunit;

namespace Project.UnitTests
{
    public class AgreementServiceTest
    {
        IConfiguration Configuration;
        public AgreementServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetAgreement()
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
                        var serviceAgreement = new AgreementService(db);
                        var service = new ProjectService(Configuration, db);
                        var input = new ProjectDTO();
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();

                        input.ProjectNo = "AAA-0002/";
                        input.SapCode = "1/03333";
                        input.ProjectNameTH = "กกก";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var result = await service.CreateProjectAsync(input);

                        var getAgreement = await serviceAgreement.GetAgreementAsync(result.Id.Value);

                        getAgreement.AttorneyNameTH1 = "เทส";
                        getAgreement.AttorneyNameEN1 = "Test";
                        getAgreement.PreferApproveName = "Test";

                        var resultUpdateAgreement = await serviceAgreement.UpdateAgreementAsync(result.Id.Value, getAgreement.Id.Value, getAgreement);

                        var getProjectStatus = await service.GetProjectDataStatusAsync(result.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestAgreementDataStatusPrepare()
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
                        var service = new ProjectService(Configuration, db);
                        var serviceAgreement = new AgreementService(db);
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreate = await service.CreateProjectAsync(input);

                        var getAgreement = await serviceAgreement.GetAgreementAsync(resultCreate.Id.Value);

                        var result = await service.GetProjectDataStatusAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestAgreementDataStatusReady()
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
                        var service = new ProjectService(Configuration, db);
                        var serviceAgreement = new AgreementService(db);
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreate = await service.CreateProjectAsync(input);

                        var getAgreement = await serviceAgreement.GetAgreementAsync(resultCreate.Id.Value);
                        getAgreement.AttorneyNameTH1 = "เทส";
                        getAgreement.AttorneyNameEN1 = "Test";
                        getAgreement.PreferApproveName = "Test";

                        var resultUpdateAgreement = await serviceAgreement.UpdateAgreementAsync(resultCreate.Id.Value, getAgreement.Id.Value, getAgreement);

                        var result = await service.GetProjectDataStatusAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestAgreementDataStatusTransfer()
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
                        var service = new ProjectService(Configuration, db);
                        var serviceAgreement = new AgreementService(db);
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreate = await service.CreateProjectAsync(input);

                        var getAgreement = await serviceAgreement.GetAgreementAsync(resultCreate.Id.Value);
                        getAgreement.AttorneyNameTH1 = "เทส";
                        getAgreement.AttorneyNameEN1 = "Test";
                        getAgreement.PreferApproveName = "Test";
                        getAgreement.AttorneyNameTransfer = "Test";
                        var resultUpdateAgreement = await serviceAgreement.UpdateAgreementAsync(resultCreate.Id.Value, getAgreement.Id.Value, getAgreement);
                        var result = await service.GetProjectDataStatusAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
