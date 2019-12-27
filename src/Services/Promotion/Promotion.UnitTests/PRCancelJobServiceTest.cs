using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using CustomAutoFixture;
using Database.Models.PRJ;
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
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using models = Database.Models;

namespace Promotion.UnitTests
{
    public class PRCancelJobServiceTest
    {
        IConfiguration Configuration;
        private FTPHelper FTPHelper;

        public PRCancelJobServiceTest()
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
        public async void CreateNewPreSalePRCancelJobAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PRRequestJobService(Configuration, db);
                        var servicePreSale = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                              .With(o => o.IsDeleted, false)
                                              .Create();

                        var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        var unit1 = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222235", UnitNo = "T2" };
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.Units.AddAsync(unit1);
                        await db.SaveChangesAsync();



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

                        var resultItems = await servicePreSale.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var result = await servicePreSale.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);
                        var getRequestItem = await db.PreSalePromotionRequestItems.Where(o => result.RequestUnits.Select(p => p.Id).Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
                        await service.RunWaitingSyncJobAsync();

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRRequestJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRRequestJobs.Where(o => o.ID == prjobItem.PRRequestJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + ";" + ";" + ";" + "x;" + "PR02;" + prjobItem.ItemNo + ";" + prjobItem.MaterialNo + ";" + "Test;" + "20190620;" + "13:53");
                            }

                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPR_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/createpr/result/", fileName);
                        }
                        await service.ReadSyncResultFromSAPAsync();
                        var preSaleRequest = await db.PreSalePromotionRequests.Where(o => o.ID == result.Id.Value).Include(o => o.PromotionRequestPRStatus).FirstOrDefaultAsync();
                        var preSaleRequestUnit = await db.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == preSaleRequest.ID).Include(o => o.SAPPRStatus).ToListAsync();

                        await servicePreSale.CancelPRAsync(preSaleRequestUnit[1].ID);
                        var prCancelUnit = await db.PreSalePromotionRequestUnits.Where(o => o.ID == preSaleRequestUnit[1].ID).FirstOrDefaultAsync();
                        var pRCancelJobs = await db.PRCancelJobs.ToListAsync();
                        var pRCancelJobItems = await db.PRCancelJobItems.ToListAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void RunWaitingSyncJobAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PRRequestJobService(Configuration, db);
                        var serviceCancel = new PRCancelJobService(Configuration, db);
                        var servicePreSale = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                              .With(o => o.IsDeleted, false)
                                              .Create();

                        var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        var unit1 = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222235", UnitNo = "T2" };
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.Units.AddAsync(unit1);
                        await db.SaveChangesAsync();



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

                        var resultItems = await servicePreSale.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var result = await servicePreSale.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);
                        var getRequestItem = await db.PreSalePromotionRequestItems.Where(o => result.RequestUnits.Select(p => p.Id).Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
                        await service.RunWaitingSyncJobAsync();

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRRequestJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRRequestJobs.Where(o => o.ID == prjobItem.PRRequestJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + ";" + ";" + ";" + "x;" + "PR02;" + prjobItem.ItemNo + ";" + prjobItem.MaterialNo + ";" + "Test;" + "20190620;" + "13:53");
                            }

                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPR_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/createpr/result/", fileName);
                        }
                        await service.ReadSyncResultFromSAPAsync();

                        var preSaleRequest = await db.PreSalePromotionRequests.Where(o => o.ID == result.Id.Value).Include(o => o.PromotionRequestPRStatus).FirstOrDefaultAsync();
                        var preSaleRequestUnit = await db.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == preSaleRequest.ID).Include(o => o.SAPPRStatus).ToListAsync();

                        await servicePreSale.CancelPRAsync(preSaleRequestUnit[1].ID);

                        await serviceCancel.RunWaitingSyncJobAsync();

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void ReadSyncResultFromSAPAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PRRequestJobService(Configuration, db);
                        var serviceCancel = new PRCancelJobService(Configuration, db);
                        var servicePreSale = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                              .With(o => o.IsDeleted, false)
                                              .Create();

                        var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        var unit1 = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222235", UnitNo = "T2" };
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.Units.AddAsync(unit1);
                        await db.SaveChangesAsync();



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

                        var resultItems = await servicePreSale.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var result = await servicePreSale.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);
                        var getRequestItem = await db.PreSalePromotionRequestItems.Where(o => result.RequestUnits.Select(p => p.Id).Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
                        await service.RunWaitingSyncJobAsync();

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRRequestJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRRequestJobs.Where(o => o.ID == prjobItem.PRRequestJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + ";" + ";" + ";" + "x;" + "PR02;" + prjobItem.ItemNo + ";" + prjobItem.MaterialNo + ";" + "Test;" + "20190620;" + "13:53");
                            }

                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPR_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/createpr/result/", fileName);
                        }
                        await service.ReadSyncResultFromSAPAsync();

                        var preSaleRequest = await db.PreSalePromotionRequests.Where(o => o.ID == result.Id.Value).Include(o => o.PromotionRequestPRStatus).FirstOrDefaultAsync();
                        var preSaleRequestUnit = await db.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == preSaleRequest.ID).Include(o => o.SAPPRStatus).ToListAsync();

                        await servicePreSale.CancelPRAsync(preSaleRequestUnit[1].ID);

                        await serviceCancel.RunWaitingSyncJobAsync();

                        getRequestItem = await db.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == preSaleRequestUnit[1].ID).ToListAsync();
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRCancelJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRCancelJobs.Where(o => o.ID == prjobItem.PRCancelJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + ";" + ";" + ";" + "x;" + prjobItem.PRNo + ";" + prjobItem.ItemNo + ";" + "x;" + "Test;" + "20190620;" + "13:53");
                            }
                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPRDEL_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/deletepr/result/", fileName);
                        }

                        await serviceCancel.ReadSyncResultFromSAPAsync();

                        tran.Rollback();
                    }
                });
            }
        }


        [Fact]
        public async void CreateRetrySyncJobAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PRRequestJobService(Configuration, db);
                        var serviceCancel = new PRCancelJobService(Configuration, db);
                        var servicePreSale = new PreSalePromotionService(Configuration, db);
                        var masterService = new MasterPreSalePromotionService(db);
                        var unitservice = new UnitService(Configuration, db);
                        var servicePro = new PromotionMaterialService(db);


                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                              .With(o => o.IsDeleted, false)
                                              .Create();

                        var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        var unit1 = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222235", UnitNo = "T2" };
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.Units.AddAsync(unit1);
                        await db.SaveChangesAsync();



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

                        var resultItems = await servicePreSale.GetPreSalePromotionItemsFormMasterAsync(resultCreateMasterPreSale.Id.Value);
                        var resultUnits = await unitservice.GetUnitDropdownWithSellPriceListAsync(project.ID, string.Empty);

                        var result = await servicePreSale.SaveRequestAndCreatePRAsync(resultCreateMasterPreSale.Id.Value, resultUnits.Take(2).ToList(), resultItems);
                        var getRequestItem = await db.PreSalePromotionRequestItems.Where(o => result.RequestUnits.Select(p => p.Id).Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
                        await service.RunWaitingSyncJobAsync();

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRRequestJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRRequestJobs.Where(o => o.ID == prjobItem.PRRequestJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + ";" + ";" + ";" + "x;" + "PR02;" + prjobItem.ItemNo + ";" + prjobItem.MaterialNo + ";" + "Test;" + "20190620;" + "13:53");
                            }

                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPR_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/createpr/result/", fileName);
                        }
                        await service.ReadSyncResultFromSAPAsync();

                        var preSaleRequest = await db.PreSalePromotionRequests.Where(o => o.ID == result.Id.Value).Include(o => o.PromotionRequestPRStatus).FirstOrDefaultAsync();
                        var preSaleRequestUnit = await db.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == preSaleRequest.ID).Include(o => o.SAPPRStatus).ToListAsync();

                        await servicePreSale.CancelPRAsync(preSaleRequestUnit[1].ID);

                        await serviceCancel.RunWaitingSyncJobAsync();

                        getRequestItem = await db.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == preSaleRequestUnit[1].ID).ToListAsync();
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            foreach (var item in getRequestItem)
                            {
                                var prjobItem = await db.PRCancelJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                                var prjob = await db.PRCancelJobs.Where(o => o.ID == prjobItem.PRCancelJobID).FirstOrDefaultAsync();
                                output.WriteLine(prjob.FileName + ";" + prjobItem.ID + ";" + "x;" + "ER5004;" + "Error sum;" + "x;" + prjobItem.PRNo + ";" + prjobItem.ItemNo + ";" + "x;" + "Test;" + "20190620;" + "13:53");
                            }
                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "CRMPRDEL_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, "sap/deletepr/result/", fileName);
                        }

                        await serviceCancel.ReadSyncResultFromSAPAsync();

                        await serviceCancel.CreateRetrySyncJobAsync(preSaleRequestUnit[1].ID);

                        var prjobs = await db.PRCancelJobs.Where(o => o.Status == models.BackgroundJobStatus.Waiting).FirstOrDefaultAsync();
                        var prjobitems = await db.PRCancelJobItems.Where(o => o.PRCancelJobID == prjobs.ID).Include(o=>o.PRCancelJobStatus).ToListAsync();
                        tran.Rollback();
                    }
                });
            }
        }


    }
}
