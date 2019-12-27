using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using CustomAutoFixture;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Services;
using Promotion.Params.Filters;
using Promotion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Database.Models.MasterKeys;

namespace Promotion.UnitTests
{
    public class PreSalePromotionServiceTest
    {
        IConfiguration Configuration;
        private FTPHelper FTPHelper;

        public PreSalePromotionServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var ftpHostIpAddress = Configuration["Ftp:HostIpAddress"];
            var ftpusername = Configuration["Ftp:Username"];
            var ftppasswrod = Configuration["Ftp:Password"];
            var ftpport = Convert.ToInt32(Configuration["Ftp:Port"]);

            this.FTPHelper = new FTPHelper(ftpHostIpAddress, ftpusername, ftppasswrod, ftpport);
        }

        [Fact]
        public async void GetPreSalePromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var unit = await db.Units.FirstAsync();
                        var result = await service.GetPreSalePromotionAsync(unit.ID);
                    }
                });
            }
        }

        [Fact]
        public async void GetPreSalePromotionRequestListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        PageParam pageParam = new PageParam();

                        PreSalePromotionRequestListFilter filter = FixtureFactory.Get().Build<PreSalePromotionRequestListFilter>().Create();
                        filter.PromotionRequestPRStatusKey = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus").Select(o => o.Key).FirstAsync();
                        PreSalePromotionRequestListSortByParam sortByParam = new PreSalePromotionRequestListSortByParam();
                        var results = await service.GetPreSalePromotionRequestListAsync(filter, pageParam, sortByParam);

                        filter = new PreSalePromotionRequestListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(PreSalePromotionRequestListSortBy)).Cast<PreSalePromotionRequestListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new PreSalePromotionRequestListSortByParam() { SortBy = item };
                            results = await service.GetPreSalePromotionRequestListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPreSalePromotionRequestAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration,db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1").FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        var preSalePromotionRequest = new PreSalePromotionRequest
                        {
                            ProjectID = project.ID,
                            IsDeleted = false
                        };
                        await db.PreSalePromotionRequests.AddAsync(preSalePromotionRequest);
                        await db.SaveChangesAsync();

                        var result = await service.GetPreSalePromotionRequestAsync(preSalePromotionRequest.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetPreSalePromotionRequestUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration,db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();



                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        //Put unit test here
                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                                new PromotionMaterialItem
                                    {
                                        AgreementNo="000001",
                                        ItemNo = "I-00001",
                                        MaterialCode = "M-00001",
                                        NameTH = "ไทย",
                                        NameEN = "ENG",
                                        Price = 20000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    },
                                  new PromotionMaterialItem
                                    {
                                        AgreementNo="000002",
                                        ItemNo = "I-00002",
                                        MaterialCode = "M-00002",
                                        NameTH = "ไทย2",
                                        NameEN = "ENG2",
                                        Price = 30000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    }
                        };
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultPromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreateMasterPreSaleItems = await masterService.CreateMasterPreSalePromotionItemFromMaterialAsync(resultCreateMasterPreSale.Id.Value, resultPromotions.PromotionMaterialDTOs);

                        var resultItems = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var resultCreate = await service.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits, resultItems);

                        var result = await service.GetPreSalePromotionRequestUnitAsync(resultCreate.RequestUnits[0].Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetAllActivePreSalePromotionItemsFormMasterAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);
                        var result = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void SaveRequestAndCreatePRAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();



                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        //Put unit test here
                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                                new PromotionMaterialItem
                                    {
                                        AgreementNo="000001",
                                        ItemNo = "I-00001",
                                        MaterialCode = "M-00001",
                                        NameTH = "ไทย",
                                        NameEN = "ENG",
                                        Price = 20000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    },
                                  new PromotionMaterialItem
                                    {
                                        AgreementNo="000002",
                                        ItemNo = "I-00002",
                                        MaterialCode = "M-00002",
                                        NameTH = "ไทย2",
                                        NameEN = "ENG2",
                                        Price = 30000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    }
                        };
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultPromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreateMasterPreSaleItems = await masterService.CreateMasterPreSalePromotionItemFromMaterialAsync(resultCreateMasterPreSale.Id.Value, resultPromotions.PromotionMaterialDTOs);

                        var resultItems = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var result = await service.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);

                        PreSalePromotionRequestListFilter filterpr = new PreSalePromotionRequestListFilter();
                        PreSalePromotionRequestListSortByParam sortByParampr = new PreSalePromotionRequestListSortByParam();
                        var results = await service.GetPreSalePromotionRequestListAsync(filterpr, pageParam, sortByParampr);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void RetryCreatePRAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();



                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        //Put unit test here
                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                                new PromotionMaterialItem
                                    {
                                        AgreementNo="000001",
                                        ItemNo = "I-00001",
                                        MaterialCode = "M-00001",
                                        NameTH = "ไทย",
                                        NameEN = "ENG",
                                        Price = 20000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    },
                                  new PromotionMaterialItem
                                    {
                                        AgreementNo="000002",
                                        ItemNo = "I-00002",
                                        MaterialCode = "M-00002",
                                        NameTH = "ไทย2",
                                        NameEN = "ENG2",
                                        Price = 30000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    }
                        };
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultPromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreateMasterPreSaleItems = await masterService.CreateMasterPreSalePromotionItemFromMaterialAsync(resultCreateMasterPreSale.Id.Value, resultPromotions.PromotionMaterialDTOs);

                        var resultItems = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var resultCreate = await service.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);

                        var requestUnit = await db.PreSalePromotionRequestUnits.FirstAsync(o => o.ID == resultCreate.RequestUnits[1].Id.Value);
                        var jobTypeID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionRequestPRJobType && o.Key == "1").Select(o => o.ID).FirstAsync();
                        var statusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SAPPRStatus && o.Key == "2").Select(o => o.ID).FirstAsync();
                        requestUnit.PromotionRequestPRJobTypeMasterCenterID = jobTypeID;
                        requestUnit.SAPPRStatusMasterCenterID = statusID;
                        db.Update(requestUnit);
                        await db.SaveChangesAsync();

                        var resultCanCel = await service.RetryCreatePRAsync(requestUnit.ID);

                        var result = await service.GetPreSalePromotionRequestAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelPRAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();



                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        //Put unit test here
                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                                new PromotionMaterialItem
                                    {
                                        AgreementNo="000001",
                                        ItemNo = "I-00001",
                                        MaterialCode = "M-00001",
                                        NameTH = "ไทย",
                                        NameEN = "ENG",
                                        Price = 20000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    },
                                  new PromotionMaterialItem
                                    {
                                        AgreementNo="000002",
                                        ItemNo = "I-00002",
                                        MaterialCode = "M-00002",
                                        NameTH = "ไทย2",
                                        NameEN = "ENG2",
                                        Price = 30000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    }
                        };
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultPromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreateMasterPreSaleItems = await masterService.CreateMasterPreSalePromotionItemFromMaterialAsync(resultCreateMasterPreSale.Id.Value, resultPromotions.PromotionMaterialDTOs);

                        var resultItems = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var resultCreate = await service.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);

                        var resultCanCel = await service.CancelPRAsync(resultCreate.RequestUnits[1].Id.Value);

                        var result = await service.GetPreSalePromotionRequestAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CancelMultiplePRAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = await db.Projects.Where(o => o.ProjectNo == "10025").FirstAsync();



                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .FirstAsync();

                        var masterPreSalePromotion = new MasterPreSalePromotionDTO();
                        masterPreSalePromotion.Name = "Test";
                        masterPreSalePromotion.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(promotionStatusMasterCenterID);
                        masterPreSalePromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreateMasterPreSale = await masterService.CreateMasterPreSalePromotionAsync(masterPreSalePromotion);

                        //Put unit test here
                        List<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                                new PromotionMaterialItem
                                    {
                                        AgreementNo="000001",
                                        ItemNo = "I-00001",
                                        MaterialCode = "M-00001",
                                        NameTH = "ไทย",
                                        NameEN = "ENG",
                                        Price = 20000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    },
                                  new PromotionMaterialItem
                                    {
                                        AgreementNo="000002",
                                        ItemNo = "I-00002",
                                        MaterialCode = "M-00002",
                                        NameTH = "ไทย2",
                                        NameEN = "ENG2",
                                        Price = 30000,
                                        UnitTH="หน่วย",
                                        UnitEN = "Unit",
                                        ExpireDate=DateTime.Now.AddYears(1)
                                    }
                        };
                        await db.PromotionMaterialItems.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();

                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultPromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);

                        var resultCreateMasterPreSaleItems = await masterService.CreateMasterPreSalePromotionItemFromMaterialAsync(resultCreateMasterPreSale.Id.Value, resultPromotions.PromotionMaterialDTOs);

                        var resultItems = await service.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var resultCreate = await service.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);

                        var resultCanCel = await service.CancelMultiplePRAsync(resultCreate.RequestUnits);

                        var result = await service.GetPreSalePromotionRequestAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
