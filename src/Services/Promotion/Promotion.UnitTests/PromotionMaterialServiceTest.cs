using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.PRM;
using Castle.Core.Configuration;
using Database.Models.PRM;
using Database.UnitTestExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Services;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Promotion.UnitTests
{
    public class PromotionMaterialServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        private FileHelper FileHelper;

        public PromotionMaterialServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }
        [Fact]
        public async void GetPromotionMaterialListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PromotionMaterialService(db);
                        PageParam pageParam = new PageParam();
                        PromotionMaterialFilter filter = FixtureFactory.Get().Build<PromotionMaterialFilter>().Create();
                        PromotionMaterialSortByParam sortByParam = new PromotionMaterialSortByParam();
                        var results = await service.GetPromotionMaterialListAsync(filter, pageParam, sortByParam);

                        filter = new PromotionMaterialFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(PromotionMaterialSortBy)).Cast<PromotionMaterialSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new PromotionMaterialSortByParam() { SortBy = item };
                            results = await service.GetPromotionMaterialListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ReadMaterialMasterFromSAPAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PromotionMaterialService(db);
                        var group = new PromotionMaterialGroup
                        {
                            Key = "10101",
                            Name = "เสาเข็มคอนกรีต",
                        };
                        await db.PromotionMaterialGroups.AddAsync(group);
                        await db.SaveChangesAsync();

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ZRFCMM01.txt",
                            Name = "ZRFCMM01.txt"
                        };
                        var stream = await FileHelper.GetStreamFromUrlAsync(fileInput.Url);
                        using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string line;
                            var content = new List<string>();
                            while ((line = streamReader.ReadLine()) != null)
                            {
                                content.Add(line);
                            }
                            await service.ReadMaterialMasterFromSAPAsync(content.ToArray());
                        }
                        var testPromotion = await db.PromotionMaterials.ToListAsync();
                        var testSyncItem = await db.SAP_ZRFCMM01s.ToListAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ReadMaterialAgreementFromSAPAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new PromotionMaterialService(db);
                        var group = new PromotionMaterialGroup
                        {
                            Key = "10101",
                            Name = "เสาเข็มคอนกรีต",
                        };

                        var material = new PromotionMaterial
                        {
                            Code = "10110-010001",
                            Name = "Test",
                            MaterialGroupKey = "10101",
                            MaterialGroupName = "เสาเข็มคอนกรีต",
                            PromotionMaterialGroupID = group.ID
                        };

                        var vats = new List<PromotionVatRate>
                        {
                            new PromotionVatRate{Code="U7",VatRate=7}
                        };

                        await db.PromotionMaterialGroups.AddAsync(group);
                        await db.PromotionMaterials.AddAsync(material);
                        await db.PromotionVatRates.AddRangeAsync(vats);
                        await db.SaveChangesAsync();

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ZRFCMM02.txt",
                            Name = "ZRFCMM02.txt"
                        };
                        var stream = await FileHelper.GetStreamFromUrlAsync(fileInput.Url);
                        using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string line;
                            var content = new List<string>();
                            while ((line = streamReader.ReadLine()) != null)
                            {
                                content.Add(line);
                            }
                            await service.ReadMaterialAgreementFromSAPAsync(content.ToArray());
                        }

                        var test = await db.PromotionMaterialItems.Where(o=>o.PromotionMaterialID== material.ID).ToListAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetMaterialSyncJobListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new PromotionMaterialService(db);
                    PageParam pageParam = new PageParam();
                    MaterialSyncJobFilter filter = FixtureFactory.Get().Build<MaterialSyncJobFilter>().Create();
                    MaterialSyncJobSortByParam sortByParam = new MaterialSyncJobSortByParam();
                    var results = await service.GetMaterialSyncJobListAsync(filter, pageParam, sortByParam);

                    filter = new MaterialSyncJobFilter();
                    pageParam = new PageParam() { Page = 1, PageSize = 10 };

                    var sortByParams = Enum.GetValues(typeof(MaterialSyncJobSortBy)).Cast<MaterialSyncJobSortBy>();
                    foreach (var item in sortByParams)
                    {
                        sortByParam = new MaterialSyncJobSortByParam() { SortBy = item };
                        results = await service.GetMaterialSyncJobListAsync(filter, pageParam, sortByParam);
                    }
                });
            }
        }

    }
}
