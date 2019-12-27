using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models.MST;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Services;
using Xunit;

namespace Promotion.UnitTests
{
    public class MasterBookingPromotionServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetMasterBookingPromotionListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);

                        MasterBookingPromotionListFilter filter = FixtureFactory.Get().Build<MasterBookingPromotionListFilter>().Create();
                        filter.PromotionStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "PromotionStatus")
                                                                          .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam();
                        MasterBookingPromotionSortByParam sortByParam = new MasterBookingPromotionSortByParam();
                        var results = await service.GetMasterBookingPromotionListAsync(filter, pageParam, sortByParam);

                        filter = new MasterBookingPromotionListFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterBookingPromotionSortBy)).Cast<MasterBookingPromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterBookingPromotionSortByParam() { SortBy = item };
                            results = await service.GetMasterBookingPromotionListAsync(filter, pageParam, sortByParam);
                        }
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingPromotionDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var data = FixtureFactory.Get().Build<MasterBookingPromotion>().With(o => o.IsDeleted, false).Create();
                        var service = new MasterBookingPromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterBookingPromotion = new MasterBookingPromotionDTO();
                        masterBookingPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterBookingPromotionAsync(masterBookingPromotion);
                        var result = await service.GetMasterBookingPromotionDetailAsync(resultCreate.Id.Value);

                        System.Diagnostics.Trace.WriteLine(JsonConvert.SerializeObject(result));
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingPromotionAsync()
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
                        var data = FixtureFactory.Get().Build<MasterBookingPromotion>().With(o => o.IsDeleted, false).Create();
                        var service = new MasterBookingPromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterBookingPromotion = new MasterBookingPromotionDTO();
                        masterBookingPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var resultCreate = await service.CreateMasterBookingPromotionAsync(masterBookingPromotion);

                        resultCreate.Name = "กกก   กกกก11111";
                        resultCreate.StartDate = DateTime.Now;
                        resultCreate.EndDate = DateTime.Now;
                        resultCreate.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);

                        var result = await service.UpdateMasterBookingPromotionAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }
        [Fact]
        public async void CreateMasterBookingPromotionAsync()
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
                        var service = new MasterBookingPromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterBookingPromotion = new MasterBookingPromotionDTO();
                        masterBookingPromotion.Project = ProjectDTO.CreateFromModel(project);

                        var result = await service.CreateMasterBookingPromotionAsync(masterBookingPromotion);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMasterBookingPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var masterBookingPromotion = new MasterBookingPromotionDTO();
                        masterBookingPromotion.Project = ProjectDTO.CreateFromModel(project);

                        //Put unit test here
                        var resultCreate = await service.CreateMasterBookingPromotionAsync(masterBookingPromotion);
                        await service.DeleteMasterBookingPromotionAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingPromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                         .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterBookingPromotionItemSortByParam sortByParam = new MasterBookingPromotionItemSortByParam();
                        var results = await service.GetMasterBookingPromotionItemListAsync(masterBookingPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterBookingPromotionItemSortBy)).Cast<MasterBookingPromotionItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterBookingPromotionItemSortByParam() { SortBy = item };
                            results = await service.GetMasterBookingPromotionItemListAsync(masterBookingPromotion.ID, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingPromotionItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);

                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);

                        resultsCreate[0].NameEN = "Test";
                        resultsCreate[0].NameTH = "เทส";

                        var result = await service.UpdateMasterBookingPromotionItemAsync(masterBookingPromotion.ID, resultsCreate[0].Id.Value, resultsCreate[0]);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingPromotionItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;

                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);


                        foreach (var item in resultsCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "test";
                            item.UnitEN = "test";
                        }
                        var resultUpdates = await service.UpdateMasterBookingPromotionItemListAsync(masterBookingPromotion.ID, resultsCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMasterBookingPromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var results = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateSubMasterBookingPromotionItemFromMaterialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                              .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        filter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var results = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);


                        var masterBookingPromotionItem = results.First();

                        var resultsSub = await service.CreateSubMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, masterBookingPromotionItem.Id.Value, dataPromotionMaterials);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingPromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter filter = new PromotionMaterialFilter();
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam sortParam = new PromotionMaterialSortByParam();
                        filter.AgreementNo = "00000";
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(filter, pageParam, sortParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);
                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        IList<MasterBookingHouseModelItem> masterBookingHouseModelItems = new List<MasterBookingHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,resultsCreate[0].Id)
                                   .With(o=>o.ModelID,model.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,resultsCreate[0].Id)
                                   .With(o=>o.ModelID,model.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        await db.AddRangeAsync(masterBookingHouseModelItems);
                        await db.SaveChangesAsync();
                        var resultsModelItem = await service.GetMasterBookingPromotionItemModelListAsync(resultsCreate[0].Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AddMasterBookingPromotionItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                       .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();

                        //Put unit test here
                        IList<PromotionMaterialItem> promotionMaterials = new List<PromotionMaterialItem>()
                        {
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000001")
                                   .With(o=>o.ItemNo,"I-00001")
                                   .With(o=>o.MaterialCode,"M-00001")
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .With(o=>o.Price,20000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create(),
                            FixtureFactory.Get().Build<PromotionMaterialItem>()
                                   .With(o=>o.AgreementNo,"000002")
                                   .With(o=>o.ItemNo,"I-00002")
                                   .With(o=>o.MaterialCode,"M-00002")
                                   .With(o=>o.NameTH,"ไทย2")
                                   .With(o=>o.NameEN,"ENG2")
                                   .With(o=>o.Price,30000)
                                   .With(o=>o.UnitTH,"หน่วย")
                                   .With(o=>o.UnitEN,"Unit")
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.ExpireDate,DateTime.Now.AddYears(1))
                                   .Create()
                        };
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(promotionMaterials);
                        await db.SaveChangesAsync();
                        PromotionMaterialFilter profilter = new PromotionMaterialFilter();
                        profilter.AgreementNo = "00000";
                        PageParam pageParam = new PageParam();
                        PromotionMaterialSortByParam proSortByParam = new PromotionMaterialSortByParam();

                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var resultpromotions = await servicePro.GetPromotionMaterialListAsync(profilter, pageParam, proSortByParam);
                        var dataPromotionMaterials = resultpromotions.PromotionMaterialDTOs;
                        var resultsCreate = await service.CreateMasterBookingPromotionItemFromMaterialAsync(masterBookingPromotion.ID, dataPromotionMaterials);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterBookingPromotionItemModelListAsync(resultsCreate[0].Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterBookingPromotionItemModelListAsync(resultsCreate[0].Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingPromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterBookingPromotionFreeItemSortByParam sortByParam = new MasterBookingPromotionFreeItemSortByParam();
                        var results = await service.GetMasterBookingPromotionFreeItemListAsync(masterBookingPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterBookingPromotionFreeItemSortBy)).Cast<MasterBookingPromotionFreeItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterBookingPromotionFreeItemSortByParam() { SortBy = item };
                            results = await service.GetMasterBookingPromotionFreeItemListAsync(masterBookingPromotion.ID, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMasterBookingPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                          .With(o => o.NameTH, "เทส")
                                          .With(o => o.NameEN, "Test")
                                          .With(o => o.UnitTH, "เทส")
                                          .With(o => o.UnitEN, "Test")
                                          .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                          .Create();
                        var results = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingPromotionFreeItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                      .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        resultCreate.Quantity = 5;
                        resultCreate.UnitTH = "หน่วย";
                        resultCreate.UnitEN = "Unit";

                        var listData = new List<MasterBookingPromotionFreeItemDTO>();
                        listData.Add(resultCreate);

                        var results = await service.UpdateMasterBookingPromotionFreeItemListAsync(masterBookingPromotion.ID, listData);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                     .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                          .With(o => o.NameTH, "เทส")
                                          .With(o => o.NameEN, "Test")
                                          .With(o => o.UnitTH, "เทส")
                                          .With(o => o.UnitEN, "Test")
                                          .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                          .Create();
                        var resultCreate = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        resultCreate.Quantity = 5;
                        resultCreate.UnitTH = "หน่วย";
                        resultCreate.UnitEN = "Unit";

                        var result = await service.UpdateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMasterBookingPromotionFreeItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        await service.DeleteMasterBookingPromotionFreeItemAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingPromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterBookingPromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterBookingPromotionFreeItemModelListAsync(resultCreate.Id.Value);


                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AddMasterBookingPromotionFreeItemModelListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        //Put unit test here
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        var whenPromotionReceiveStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();

                        var data = FixtureFactory.Get().Build<MasterBookingPromotionFreeItemDTO>()
                                           .With(o => o.NameTH, "เทส")
                                           .With(o => o.NameEN, "Test")
                                           .With(o => o.UnitTH, "เทส")
                                           .With(o => o.UnitEN, "Test")
                                           .With(o => o.WhenPromotionReceive, MasterCenterDropdownDTO.CreateFromModel(whenPromotionReceiveStatus))
                                           .Create();
                        var resultCreate = await service.CreateMasterBookingPromotionFreeItemAsync(masterBookingPromotion.ID, data);

                        var model = await db.Models.Where(o => !o.IsDeleted).FirstAsync();
                        List<ModelListDTO> modelListDTOs = new List<ModelListDTO>()
                        {
                            FixtureFactory.Get().Build<ModelListDTO>()
                                   .With(o=>o.Id,model.ID)
                                   .With(o=>o.NameTH,"ไทย")
                                   .With(o=>o.NameEN,"ENG")
                                   .Create(),
                        };
                        var resultAddModel = await service.AddMasterBookingPromotionFreeItemModelListAsync(resultCreate.Id.Value, modelListDTOs);
                        var resultsModelItem = await service.GetMasterBookingPromotionFreeItemModelListAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMasterBookingCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                            .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();
                        MasterBookingCreditCardItemSortByParam sortByParam = new MasterBookingCreditCardItemSortByParam();
                        var results = await service.GetMasterBookingCreditCardItemAsync(masterBookingPromotion.ID, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(MasterBookingCreditCardItemSortBy)).Cast<MasterBookingCreditCardItemSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new MasterBookingCreditCardItemSortByParam() { SortBy = item };
                            results = await service.GetMasterBookingCreditCardItemAsync(masterBookingPromotion.ID, pageParam, sortByParam);
                        }
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingCreditCardItemListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                              .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterBookingCreditCardItemsAsync(masterBookingPromotion.ID, eDCFeeDTOs);
                        foreach (var item in resultCreate)
                        {
                            item.NameTH = "เทส";
                            item.NameEN = "Test";
                        }
                        var resultupdates = await service.UpdateMasterBookingCreditCardItemListAsync(masterBookingPromotion.ID, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateMasterBookingCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterBookingCreditCardItemsAsync(masterBookingPromotion.ID, eDCFeeDTOs);

                        resultCreate[0].NameTH = "เทส";
                        resultCreate[0].NameEN = "Test";

                        var resultupdate = await service.UpdateMasterBookingCreditCardItemAsync(masterBookingPromotion.ID, resultCreate[0].Id.Value, resultCreate[0]);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteMasterBookingCreditCardItemAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var resultCreate = await service.CreateMasterBookingCreditCardItemsAsync(masterBookingPromotion.ID, eDCFeeDTOs);

                        await service.DeleteMasterBookingCreditCardItemAsync(resultCreate[0].Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CloneMasterBookingPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();
                        var whenPromotionReceiveStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
                        var promotionItemStatusMasterCenter = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
                        var masterCenterPromotionStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                        .FirstAsync();
                        var project = await db.Projects.FirstAsync();
                        var MasterBooking = new MasterBookingPromotionDTO();
                        MasterBooking.Name = "Test";
                        MasterBooking.PromotionStatus = MasterCenterDropdownDTO.CreateFromModel(masterCenterPromotionStatusID);
                        MasterBooking.Project = ProjectDTO.CreateFromModel(project);
                        //Put unit test here
                        var masterBookingPromotion = await service.CreateMasterBookingPromotionAsync(MasterBooking);

                        //Put unit test here
                        List<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2019,03,15))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create()
                        };
                        List<MasterBookingHouseModelItem> masterBookingPromotionHouseItems = new List<MasterBookingHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        List<MasterBookingPromotionFreeItem> masterBookingPromotionFreeItems = new List<MasterBookingPromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingPromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionFreeItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o => o.IsDeleted, false)
                                   .With(o=>o.WhenPromotionReceiveMasterCenterID,whenPromotionReceiveStatusMasterCenter.ID)
                                   .Create(),
                        };
                        List<MasterBookingHouseModelFreeItem> masterBookingHouseModelFreeItems = new List<MasterBookingHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingHouseModelFreeItem>()
                                   .With(o=>o.MasterBookingPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingHouseModelFreeItem>()
                                   .With(o=>o.MasterBookingPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        List<MasterBookingCreditCardItem> masterBookingCreditCardItems = new List<MasterBookingCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingCreditCardItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingCreditCardItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.Id.Value)
                                   .With(o=>o.PromotionItemStatusMasterCenterID,promotionItemStatusMasterCenter.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotionItems.AddRangeAsync(masterBookingPromotionItems);
                        await db.MasterBookingHouseModelItems.AddRangeAsync(masterBookingPromotionHouseItems);
                        await db.MasterBookingPromotionFreeItems.AddRangeAsync(masterBookingPromotionFreeItems);
                        await db.MasterBookingHouseModelFreeItems.AddRangeAsync(masterBookingHouseModelFreeItems);
                        await db.MasterBookingCreditCardItems.AddRangeAsync(masterBookingCreditCardItems);
                        await db.SaveChangesAsync();

                        PageParam pageParam = new PageParam();

                        MasterBookingPromotionSortByParam sortParam = new MasterBookingPromotionSortByParam();
                        MasterBookingPromotionItemSortByParam sortParamItem = new MasterBookingPromotionItemSortByParam();
                        MasterBookingPromotionFreeItemSortByParam sortParamFreeItem = new MasterBookingPromotionFreeItemSortByParam();
                        MasterBookingCreditCardItemSortByParam sortParamCardItem = new MasterBookingCreditCardItemSortByParam();

                        var resultsItem = await service.GetMasterBookingPromotionItemListAsync(masterBookingPromotion.Id.Value, pageParam, sortParamItem);
                        var resultModel = await service.GetMasterBookingPromotionItemModelListAsync(new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"));
                        var resultsFreeItems = await service.GetMasterBookingPromotionFreeItemListAsync(masterBookingPromotion.Id.Value, pageParam, sortParamFreeItem);
                        var resultsModelFreeItems = await service.GetMasterBookingPromotionFreeItemModelListAsync(new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"));
                        var resultsCreditItems = await service.GetMasterBookingCreditCardItemAsync(masterBookingPromotion.Id.Value, pageParam, sortParamCardItem);
                        var dataClone = await service.CloneMasterBookingPromotionAsync(masterBookingPromotion.Id.Value);

                        var resultsItemClone = await service.GetMasterBookingPromotionItemListAsync(dataClone.Id.Value, pageParam, sortParamItem);
                        var resultModelClone = await service.GetMasterBookingPromotionItemModelListAsync(resultsItemClone.MasterBookingPromotionItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsFreeItemsClone = await service.GetMasterBookingPromotionFreeItemListAsync(dataClone.Id.Value, pageParam, sortParamFreeItem);
                        var resultsModelFreeItemsClone = await service.GetMasterBookingPromotionFreeItemModelListAsync(resultsFreeItemsClone.MasterBookingPromotionFreeItemDTOs.Where(o => o.NameEN == "Test").First().Id.Value);
                        var resultsCreditItemsClone = await service.GetMasterBookingCreditCardItemAsync(dataClone.Id.Value, pageParam, sortParamCardItem);


                        MasterBookingPromotionListFilter filter = new MasterBookingPromotionListFilter();
                        var testResults = await service.GetMasterBookingPromotionListAsync(filter, pageParam, sortParam);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateMasterBookingCreditCardItemsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);

                        var promotionStatusMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                                    .Select(o => o.ID).FirstAsync();

                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                        .With(o => o.IsDeleted, false)
                                                        .With(o => o.ProjectID, (Guid?)null)
                                                        .With(o => o.PromotionStatusMasterCenterID, promotionStatusMasterCenterID)
                                                        .Create();
                        var banks = await db.Banks.Where(o => !o.IsDeleted).ToListAsync();
                        var paymentCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PaymentCardType").ToListAsync();
                        var creditCardType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardType").ToListAsync();
                        var creditCardPaymentType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "CreditCardPaymentType").ToListAsync();


                        //Put unit test here
                        List<EDCFee> eDCFees = new List<EDCFee>()
                        {
                            FixtureFactory.Get().Build<EDCFee>()
                                   .With(o=>o.ID,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.BankID,banks[0].ID)
                                   .With(o=>o.PaymentCardTypeMasterCenterID,paymentCardType[0].ID)
                                   .With(o=>o.CreditCardTypeMasterCenterID,creditCardType[0].ID)
                                   .With(o=>o.CreditCardPaymentTypeMasterCenterID,creditCardPaymentType[0].ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(eDCFees);
                        await db.SaveChangesAsync();

                        List<EDCFeeDTO> eDCFeeDTOs = new List<EDCFeeDTO>()
                        {
                            FixtureFactory.Get().Build<EDCFeeDTO>()
                                   .With(o=>o.Id,new Guid("7f65a6d0-759c-4d41-a6ec-e53687f7c3ce"))
                                   .With(o=>o.Fee,55)
                                   .With(o=>o.Bank,BankDropdownDTO.CreateFromModel(banks[0]))
                                   .With(o=>o.PaymentCardType,MasterCenterDropdownDTO.CreateFromModel(paymentCardType[0]))
                                   .With(o=>o.CreditCardType,MasterCenterDropdownDTO.CreateFromModel(creditCardType[0]))
                                   .With(o=>o.CreditCardPaymentType,MasterCenterDropdownDTO.CreateFromModel(creditCardPaymentType[0]))
                                   .Create(),
                        };
                        var results = await service.CreateMasterBookingCreditCardItemsAsync(masterBookingPromotion.ID, eDCFeeDTOs);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetCloneMasterBookingPromotionConfirmAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new MasterBookingPromotionService(db);
                        var servicePro = new PromotionMaterialService(db);
                        var masterBookingPromotion = FixtureFactory.Get().Build<MasterBookingPromotion>()
                                                            //.With(o => o.Status, PromotionStatus.Active)
                                                            .With(o => o.IsDeleted, false)
                                                            .Create();

                        //Put unit test here
                        IList<MasterBookingPromotionItem> masterBookingPromotionItems = new List<MasterBookingPromotionItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.ID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,(Guid?)null)
                                   .With(o=>o.ExpireDate,new DateTime(2019,03,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o=>o.MainPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o=>o.ExpireDate,new DateTime(2022,06,15))
                                   .With(o => o.IsDeleted, false)
                                   .Create()
                        };
                        IList<MasterBookingHouseModelItem> masterBookingPromotionHouseItems = new List<MasterBookingHouseModelItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingHouseModelItem>()
                                   .With(o=>o.MasterBookingPromotionItemID,new Guid("bb577348-c9fd-435e-b897-abefcf4d8aa1"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterBookingPromotionFreeItem> masterBookingPromotionFreeItems = new List<MasterBookingPromotionFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingPromotionFreeItem>()
                                   .With(o=>o.ID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o=>o.NameEN,"Test")
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingPromotionFreeItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterBookingHouseModelFreeItem> masterBookingHouseModelFreeItems = new List<MasterBookingHouseModelFreeItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingHouseModelFreeItem>()
                                   .With(o=>o.MasterBookingPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingHouseModelFreeItem>()
                                   .With(o=>o.MasterBookingPromotionFreeItemID,new Guid("08c2bd4a-9bf6-4425-b3ae-a3455869676e"))
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };
                        IList<MasterBookingCreditCardItem> masterBookingCreditCardItems = new List<MasterBookingCreditCardItem>()
                        {
                            FixtureFactory.Get().Build<MasterBookingCreditCardItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                            FixtureFactory.Get().Build<MasterBookingCreditCardItem>()
                                   .With(o=>o.MasterBookingPromotionID,masterBookingPromotion.ID)
                                   .With(o => o.IsDeleted, false)
                                   .Create(),
                        };

                        await db.MasterBookingPromotions.AddAsync(masterBookingPromotion);
                        await db.AddRangeAsync(masterBookingPromotionItems);
                        await db.AddRangeAsync(masterBookingPromotionHouseItems);
                        await db.AddRangeAsync(masterBookingPromotionFreeItems);
                        await db.AddRangeAsync(masterBookingHouseModelFreeItems);
                        await db.AddRangeAsync(masterBookingCreditCardItems);
                        await db.SaveChangesAsync();

                        var dataCloneConfirm = await service.GetCloneMasterBookingPromotionConfirmAsync(masterBookingPromotion.ID);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
