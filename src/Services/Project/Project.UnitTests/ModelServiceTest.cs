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

namespace Project.UnitTests
{
    public class ModelServiceTest
    {
        IConfiguration Configuration;
        public ModelServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetModelListAsync()
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

                        var service = new ModelService(db);
                        ModelsFilter filter = FixtureFactory.Get().Build<ModelsFilter>().Create();
                        filter.ModelShortNameKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ModelShortName")
                                                                      .Select(x => x.Key).FirstAsync();
                        filter.ModelTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ModelType")
                                                                      .Select(x => x.Key).FirstAsync();
                        filter.ModelUnitTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ModelUnitType")
                                                                     .Select(x => x.Key).FirstAsync();
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();
                        PageParam pageParam = new PageParam();
                        ModelListSortByParam sortByParam = new ModelListSortByParam();
                        var results = await service.GetModelListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new ModelsFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ModelListSortBy)).Cast<ModelListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ModelListSortByParam() { SortBy = item };
                            results = await service.GetModelListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetModelAsync()
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

                        var service = new ModelService(db);
                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();

                        var result = await service.GetModelAsync(model.ProjectID, model.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateModelAsync()
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

                        var service = new ModelService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "60022").FirstAsync();
                        var input = FixtureFactory.Get().Build<ModelDTO>().Create();
                        input.NameTH = "CC3";
                        input.NameEN = "C3";
                        input.ModelUnitType = null;
                        input.ModelType = null;
                        input.ModelShortName = null;
                        input.TypeOfRealEstate = null;
                        input.WaterElectricMeterPrices = new List<WaterElectricMeterPriceDTO>
                        {
                            new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };


                        var result = await service.CreateModelAsync(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateModelAsync()
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

                        var service = new ModelService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "60022").FirstAsync();
                        var input = FixtureFactory.Get().Build<ModelDTO>().Create();
                        input.NameTH = "CC3";
                        input.NameEN = "C3";
                        input.ModelUnitType = null;
                        input.ModelType = null;
                        input.ModelShortName = null;
                        input.TypeOfRealEstate = null;
                        input.WaterElectricMeterPrices = new List<WaterElectricMeterPriceDTO>
                        {
                            new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };


                        var resultCreate = await service.CreateModelAsync(project.ID, input);
                        resultCreate.WaterElectricMeterPrices.Add(new WaterElectricMeterPriceDTO
                        {
                            WaterMeterPrice = 10,
                            WaterMeterSize = "10",
                            ElectricMeterPrice = 10,
                            ElectricMeterSize = "10"
                        });
                        var result = await service.UpdateModelAsync(project.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteModelAsync()
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

                        var service = new ModelService(db);
                        var project = await db.Projects.Where(o => !o.IsDeleted).FirstAsync();
                        var input = FixtureFactory.Get().Build<ModelDTO>().Create();

                        input.ModelUnitType = null;
                        input.ModelType = null;
                        input.ModelShortName = null;
                        input.TypeOfRealEstate = null;
                        input.WaterElectricMeterPrices = new List<WaterElectricMeterPriceDTO>
                        {
                            new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };


                        var resultCreate = await service.CreateModelAsync(project.ID, input);

                        var result = await service.DeleteModelAsync(project.ID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestCreateModelDataStatusPrepare()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var modelUnitType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ModelUnitType").FirstAsync();
                        var typeOfRealEstate = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
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
                            },
                              new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            },
                                new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };


                        var resultModel = await serviceModel.CreateModelAsync(resultCreateProject.Id.Value, inputModel);

                        var result = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestUpdateModelDataStatusPrepare()
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
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var modelUnitType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ModelUnitType").FirstAsync();
                        var typeOfRealEstate = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
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


                        var resultModel = await serviceModel.CreateModelAsync(resultCreateProject.Id.Value, inputModel);

                        resultModel.Code = "";
                        await serviceModel.UpdateModelAsync(resultCreateProject.Id.Value, resultModel.Id.Value, resultModel);
                        var resultWhenUpdate = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
