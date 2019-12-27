using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Base.DTOs;

namespace Project.UnitTests
{
    public class HighRiseFeeServiceTest
    {
        IConfiguration Configuration;
        public HighRiseFeeServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        //[Fact]
        //public async void ImportHighRiseFeeAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                var service = new HighRiseFeeService(Configuration, db);
        //                var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "19901");
        //                FileDTO fileInput = new FileDTO()
        //                {
        //                    Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_LandAppraisalPrice_Test1.xlsx",
        //                    Name = "ProjectID_LandAppraisalPrice_Test1.xlsx"
        //                };
        //                var result = await service.ImportHighRiseFeeAsync(project.ID, fileInput);
        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        [Fact]
        public async void GetHighRiseFeeListAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);

                        HighRiseFeeFilter filter = FixtureFactory.Get().Build<HighRiseFeeFilter>().Create();
                        filter.CalculateParkAreaKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CalculateParkArea").Select(o => o.Key).FirstAsync();

                        var project = await db.Projects.Where(o => !o.IsDeleted && o.ProjectNo == "10033").FirstAsync();
                        PageParam pageParam = new PageParam();
                        HighRiseFeeSortByParam sortByParam = new HighRiseFeeSortByParam();
                        var results = await service.GetHighRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new HighRiseFeeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(HighRiseFeeSortBy)).Cast<HighRiseFeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new HighRiseFeeSortByParam() { SortBy = item };
                            results = await service.GetHighRiseFeeListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);
                        var model = await db.HighRiseFees.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetHighRiseFeeAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);

                        var project = await db.Projects.Where(o => o.ProjectNo == "10103").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID).FirstAsync();
                        var tower = await db.Towers.Where(o => o.ProjectID == project.ID).FirstAsync();
                        var floor = await db.Floors.Where(o => o.TowerID == tower.ID).FirstAsync();
                        var input = new HighRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Tower = TowerDropdownDTO.CreateFromModel(tower);
                        input.Floor = FloorDropdownDTO.CreateFromModel(floor);
                        input.EstimatePriceAirArea = 50;

                        var result = await service.CreateHighRiseFeeAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateHighRiseFeeAsync()
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

                        var service = new HighRiseFeeService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10057").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID && o.UnitNo == "A04-B01").FirstAsync();

                        var input = new HighRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.EstimatePriceAirArea = 50;

                        var resultCreate = await service.CreateHighRiseFeeAsync(project.ID, input);

                        resultCreate.EstimatePriceAirArea = 60;

                        var result = await service.UpdateHighRiseFeeAsync(project.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteHighRiseFeeAsync()
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
                        var service = new HighRiseFeeService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10057").FirstAsync();
                        var unit = await db.Units.Where(o => !o.IsDeleted && o.ProjectID == project.ID && o.UnitNo == "A04-B01").FirstAsync();

                        var input = new HighRiseFeeDTO();
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.EstimatePriceAirArea = 50;

                        var resultCreate = await service.CreateHighRiseFeeAsync(project.ID, input);

                        await service.DeleteHighRiseFeeAsync(project.ID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TransferFeeDataStatus()
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
                        var serviceModel = new ModelService(db);
                        var serviceUnit = new UnitService(Configuration, db);
                        var serviceHighRise = new HighRiseFeeService(Configuration, db);
                        var serviceLowRise = new LowRiseFeeService(db);
                        var serviceLowRiseBuildingPrice = new LowRiseBuildingPriceFeeService(db);
                        var serviceRound = new RoundFeeService(db);
                        var serviceLowRiseFence = new LowRiseFenceFeeService(db);

                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var modelUnitType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ModelUnitType").FirstAsync();
                        var typeOfRealEstate = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
                        var unitStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus" && o.Key == "0").FirstAsync();
                        var landOffice = await db.LandOffices.Where(o => !o.IsDeleted).FirstAsync();
                        var roundFormula = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RoundFormulaType").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreateProject = await service.CreateProjectAsync(input);

                        var inputModel = new ModelDTO();

                        inputModel.Code = "2222";
                        inputModel.NameTH = "เทส";
                        inputModel.NameEN = "Test";
                        inputModel.ModelUnitType = MasterCenterDropdownDTO.CreateFromModel(modelUnitType);
                        inputModel.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(typeOfRealEstate);
                        inputModel.WaterElectricMeterPrices = new List<WaterElectricMeterPriceDTO>
                        {
                            new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };
                        var resultCreateModel = await serviceModel.CreateModelAsync(resultCreateProject.Id.Value, inputModel);
                        var model = await db.Models.Where(o => o.ID == resultCreateModel.Id.Value).FirstAsync();
                        var inputUnit = new UnitDTO();
                        inputUnit.SapwbsObject = "22222";
                        inputUnit.SapwbsNo = "44444";
                        inputUnit.Model = ModelDropdownDTO.CreateFromModel(model);
                        inputUnit.SaleArea = 5555;
                        inputUnit.UnitStatus = MasterCenterDropdownDTO.CreateFromModel(unitStatus);
                        inputUnit.RoomPlan = new RoomPlanImageDTO { Name = "Test" };
                        inputUnit.FloorPlan = new FloorPlanImageDTO { Name = "Test" };

                        var resultsCreateUnit = await serviceUnit.CreateUnitAsync(resultCreateProject.Id.Value, inputUnit);

                        var unit = await db.Units.Where(o => o.ID == resultsCreateUnit.Id.Value).FirstAsync();
                        var inputLowRiseFee = new LowRiseFeeDTO();
                        inputLowRiseFee.EstimatePriceArea = 555;
                        inputLowRiseFee.Unit = UnitDropdownDTO.CreateFromModel(unit);

                        var resultCreateLowRiseFee = await serviceLowRise.CreateLowRiseFeeAsync(resultCreateProject.Id.Value, inputLowRiseFee);

                        var inputLowRiseFenceFee = new LowRiseFenceFeeDTO();
                        inputLowRiseFenceFee.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        inputLowRiseFenceFee.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(typeOfRealEstate);

                        var resultCreateLowRiseFenceFee = await serviceLowRiseFence.CreateLowRiseFenceFeeAsync(resultCreateProject.Id.Value, inputLowRiseFenceFee);

                        var inputLowRiseBuildingPrice = new LowRiseBuildingPriceFeeDTO();
                        inputLowRiseBuildingPrice.Model = ModelDropdownDTO.CreateFromModel(model);
                        inputLowRiseBuildingPrice.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        inputLowRiseBuildingPrice.Price = 555;

                        var resultCreateLowRiseBuildingPriceFee = await serviceLowRiseBuildingPrice.CreateLowRiseBuildingPriceFeeAsync(resultCreateProject.Id.Value, inputLowRiseBuildingPrice);

                        var inputRoundFee = new RoundFeeDTO();
                        inputRoundFee.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        inputRoundFee.BusinessTaxRoundFormula = MasterCenterDropdownDTO.CreateFromModel(roundFormula);
                        inputRoundFee.IncomeTaxRoundFormula = MasterCenterDropdownDTO.CreateFromModel(roundFormula);
                        inputRoundFee.LocalTaxRoundFormula = MasterCenterDropdownDTO.CreateFromModel(roundFormula);
                        inputRoundFee.TransferFeeRoundFormula = MasterCenterDropdownDTO.CreateFromModel(roundFormula);
                        inputRoundFee.OtherFee = 655;

                        var resultCreateRoundFee = await serviceRound.CreateRoundFeeAsync(resultCreateProject.Id.Value, inputRoundFee);

                        var getDatastattus = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);


                        tran.Rollback();
                    }
                });
            }
        }

    }
}
