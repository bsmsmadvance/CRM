using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using models = Database.Models;
using Database.UnitTestExtensions;
using Microsoft.Extensions.Configuration;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Project.Params.Filters;
using PagingExtensions;
using Base.DTOs.PRJ;
using System.Linq;
using System.IO;
using FileStorage;
using Database.Models.PRJ;

namespace Project.UnitTests
{
    public class BudgetPromotionServiceTest
    {
        IConfiguration Configuration;
        private FileHelper FileHelper;
        private FTPHelper FTPHelper;
        public BudgetPromotionServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            var ftpHostIpAddress = Configuration["Ftp:HostIpAddress"];
            var ftpusername = Configuration["Ftp:Username"];
            var ftppasswrod = Configuration["Ftp:Password"];
            var ftpport = Convert.ToInt32(Configuration["Ftp:Port"]);

            this.FTPHelper = new FTPHelper(ftpHostIpAddress, ftpusername, ftppasswrod, ftpport);
            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetBudgetPromotionListAsync()
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
                        //var project = FixtureFactory.Get().Build<models.PRJ.Project>().With(o => o.IsDeleted, false).Create();
                        var project = await db.Projects.Where(o => o.ProjectNo == "10191").FirstOrDefaultAsync();
                        //var unit = db.Units.Where(o => o.ID == new Guid("d2d9f92e-3be6-4bfa-bb5c-1a8c6a88b75c")).FirstOrDefault();
                        //await db.Projects.AddAsync(project);
                        //await db.SaveChangesAsync();
                        var budgetPromotionSyncStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionSyncStatus").FirstAsync();
                        var service = new BudgetPromotionService(Configuration, db);
                        BudgetPromotionFilter filter = FixtureFactory.Get().Build<BudgetPromotionFilter>().Create();
                        filter.SyncJob_StatusKey = budgetPromotionSyncStatus.Key;
                        PageParam pageParam = new PageParam();
                        BudgetPromotionSortByParam sortByParam = new BudgetPromotionSortByParam();
                        var results = await service.GetBudgetPromotionListAsync(project.ID, filter, pageParam, sortByParam);

                        filter = new BudgetPromotionFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(BudgetPromotionSortBy)).Cast<BudgetPromotionSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new BudgetPromotionSortByParam() { SortBy = item };
                            results = await service.GetBudgetPromotionListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = new models.PRJ.Unit { ProjectID = project.ID, SAPWBSObject_P = "2223456" };

                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();

                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var result = await service.CreateBudgetPromotionAsync(project.ID, input);
                        //await service.RunWaitingSyncJobAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = FixtureFactory.Get().Build<models.PRJ.Unit>()
                                          .With(o => o.ProjectID, project.ID)
                                          .With(o => o.IsDeleted, false)
                                          .Create();
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();


                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var resultCreate = await service.CreateBudgetPromotionAsync(project.ID, input);

                        var result = await service.GetBudgetPromotionAsync(project.ID, resultCreate.Unit.Id.Value);

                        await service.RunWaitingSyncJobAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = FixtureFactory.Get().Build<models.PRJ.Unit>()
                                          .With(o => o.ProjectID, project.ID)
                                          .With(o => o.IsDeleted, false)
                                          .Create();
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();


                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var resultCreate = await service.CreateBudgetPromotionAsync(project.ID, input);

                        resultCreate.PromotionPrice = 25;
                        resultCreate.PromotionTransferPrice = 50;
                        var result = await service.UpdateBudgetPromotionAsync(project.ID, resultCreate.Unit.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBudgetPromotionAsync()
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
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = FixtureFactory.Get().Build<models.PRJ.Unit>()
                                          .With(o => o.ProjectID, project.ID)
                                          .With(o => o.IsDeleted, false)
                                          .Create();
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();


                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var resultCreate = await service.CreateBudgetPromotionAsync(project.ID, input);

                        await service.DeleteBudgetPromotionAsync(project.ID, resultCreate.Unit.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportBudgetPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        //var unitservice = new UnitService(Configuration, db);
                        var service = new BudgetPromotionService(Configuration, db);

                        //FileDTO fileInputUnit = new FileDTO()
                        //{
                        //    Url = "http://192.168.2.29:9001/xunit-tests/ZRFCPS01.txt",
                        //    Name = "ZRFCPS01.txt"
                        //};
                        //var stream = await FileHelper.GetStreamFromUrlAsync(fileInputUnit.Url);
                        //using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                        //{
                        //    string line;
                        //    var content = new List<string>();
                        //    while ((line = streamReader.ReadLine()) != null)
                        //    {
                        //        content.Add(line);
                        //    }
                        //    await unitservice.ReadSAPWBSPromotionTextFileAsync(content.ToArray());
                        //}

                        var projectID = await db.Projects.Where(o => o.ProjectNo == "40017").Select(o => o.ID).FirstAsync();
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_Budget.xlsx",
                            Name = "ProjectID_Budget.xlsx"
                        };
                        var result = await service.ImportBudgetPromotionAsync(projectID, fileInput);
                        //await service.RunWaitingSyncJobAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelBudgetPromotionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "10191");
                        var result = await service.ExportExcelBudgetPromotionAsync(project.ID);
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
                        var service = new BudgetPromotionService(Configuration, db);
                        //var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                        //                     .With(o => o.IsDeleted, false)
                        //                     .Create();
                        //var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        //var unit1 = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222235", UnitNo = "T2" };
                        //await db.Projects.AddAsync(project);
                        //await db.Units.AddAsync(unit);
                        //await db.Units.AddAsync(unit1);
                        //await db.SaveChangesAsync();

                        //var unit = await db.Units.Where(o => o.ID == new Guid("d2d9f92e-3be6-4bfa-bb5c-1a8c6a88b75c")).FirstOrDefaultAsync();

                        //var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //                   .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //                   .With(o => o.PromotionPrice, 450000)
                        //                   .With(o => o.PromotionTransferPrice, 450000)
                        //                   .Create();
                        //var input1 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 400000)
                        //               .With(o => o.PromotionTransferPrice, 400000)
                        //               .Create();
                        //var input2 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 350000)
                        //               .With(o => o.PromotionTransferPrice, 350000)
                        //               .Create();
                        //var input3 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 300000)
                        //               .With(o => o.PromotionTransferPrice, 300000)
                        //               .Create();
                        //var input4 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 250000)
                        //               .With(o => o.PromotionTransferPrice, 250000)
                        //               .Create();
                        //var input5 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 200000)
                        //               .With(o => o.PromotionTransferPrice, 200000)
                        //               .Create();
                        //var input6 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 150000)
                        //               .With(o => o.PromotionTransferPrice, 150000)
                        //               .Create();
                        //var input7 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 100000)
                        //               .With(o => o.PromotionTransferPrice, 100000)
                        //               .Create();
                        //var input8 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 100000)
                        //               .With(o => o.PromotionTransferPrice, 50000)
                        //               .Create();
                        //var input9 = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                        //               .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                        //               .With(o => o.PromotionPrice, 50000)
                        //               .With(o => o.PromotionTransferPrice, 50000)
                        //               .Create();
                        //var result = await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input1);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input2);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input3);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input4);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input5);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input6);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input7);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input8);
                        //await service.UpdateBudgetPromotionAsync(unit.ProjectID.Value, unit.ID, input9);

                        //await service.RunWaitingSyncJobAsync();



                        //var item1 = await db.BudgetPromotionSyncItems.Where(o => o.SAPWBSObject_P == unit.SAPWBSObject_P).FirstAsync();
                        //var model = await db.BudgetPromotionSyncJobs.Where(o => o.ID == item1.BudgetPromotionSyncJobID).FirstAsync();

                        //var item2 = await db.BudgetPromotionSyncItems.Where(o => o.SAPWBSObject_P == unit1.SAPWBSObject_P).FirstAsync();
                        //var model2 = await db.BudgetPromotionSyncJobs.Where(o => o.ID == item2.BudgetPromotionSyncJobID).FirstAsync();

                        //using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        //using (StreamWriter output = new StreamWriter(stream))
                        //{
                        //    output.WriteLine(model.FileName + ";" + item1.ID + ";" + "x;" + "ER5004;" + "Error sum;" + "x;" + item1.SAPWBSObject_P + ";" + "20200;" + "Test;" + "20190620;" + "13:53");
                        //    output.WriteLine(model2.FileName + ";" + item2.ID + ";" + "x;" + "ER5004;" + "Error sum;" + "x;" + item2.SAPWBSObject_P + ";" + "202002;" + "Test;" + "20190620;" + "13:53");
                        //    output.Flush();
                        //    Stream fileStream = new MemoryStream(stream.ToArray());
                        //    string fileName = "CRMBG_OUT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                        //    string filePath = $"projects/budget-promotion-sync/{model.ID}/";
                        //    await this.FTPHelper.UploadFileFromStreamAsync(fileStream, filePath, fileName);
                        //    await this.FTPHelper.MoveFileAsync(filePath, fileName, "sap/result/", fileName);
                        //}
                        await service.ReadSyncResultFromSAPAsync();
                        //var testunit = await db.Units.Where(o => o.ID == unit.ID).FirstOrDefaultAsync();
                        tran.Rollback();
                        //tran.Commit();
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
                        var service = new BudgetPromotionService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = new Unit { ProjectID = project.ID, SAPWBSObject_P = "T2222234", UnitNo = "T1" };
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();


                        var input = FixtureFactory.Get().Build<BudgetPromotionDTO>()
                                           .With(o => o.Unit, UnitDTO.CreateFromModel(unit))
                                           .With(o => o.PromotionPrice, 50)
                                           .With(o => o.PromotionTransferPrice, 100)
                                           .Create();

                        var result = await service.CreateBudgetPromotionAsync(project.ID, input);


                        await service.RunWaitingSyncJobAsync();



                        var item1 = await db.BudgetPromotionSyncItems.Where(o => o.SAPWBSObject_P == unit.SAPWBSObject_P).FirstAsync();
                        var model = await db.BudgetPromotionSyncJobs.Where(o => o.ID == item1.BudgetPromotionSyncJobID).FirstAsync();

                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        using (StreamWriter output = new StreamWriter(stream))
                        {
                            output.WriteLine(model.FileName + ";" + item1.ID + ";" + "x;" + "ER5004;" + "Error sum;" + "x;" + "PR00246774;" + "222222;" + "Test;" + "20190620;" + "13:53");
                            output.Flush();
                            Stream fileStream = new MemoryStream(stream.ToArray());
                            string fileName = "TestData000001.txt";
                            string filePath = $"projects/budget-promotion-sync/{model.ID}/";
                            await this.FTPHelper.UploadFileFromStreamAsync(fileStream, filePath, fileName);
                            await this.FTPHelper.MoveFileAsync(filePath, fileName, "sap/result/", fileName);
                            //string fileName = model.SAPResultFileName;
                            //string contentType = "text/*";
                            //string filePath = $"budget-promotion-sync/{model.ID}/";
                            //var uploadResult = await this.FileHelper.UploadFileFromStreamWithOutGuid(fileStream, filePath, "TestData000001", contentType);
                            //await this.FileHelper.MoveAndRemoveFileAsync("projects", $"budget-promotion-sync/{model.ID}/" + "TestData000001", "sap", "result/" + "TestData000001");
                        }
                        await service.ReadSyncResultFromSAPAsync();
                        await service.CreateRetrySyncJobAsync();

                        BudgetPromotionFilter filter = new BudgetPromotionFilter();
                        //filter.SyncJob_StatusKey = "3";
                        PageParam pageParam = new PageParam();
                        BudgetPromotionSortByParam sortByParam = new BudgetPromotionSortByParam();
                        sortByParam.SortBy = BudgetPromotionSortBy.SyncJob_Status;
                        var results = await service.GetBudgetPromotionListAsync(project.ID, filter, pageParam, sortByParam);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
