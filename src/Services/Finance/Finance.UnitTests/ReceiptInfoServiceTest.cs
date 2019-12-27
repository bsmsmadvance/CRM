﻿using AutoFixture;
using Base.DTOs.FIN;
using Database.UnitTestExtensions;
using FileStorage;
using Finance.Params.Filters;
using Finance.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using Xunit;
using System.Linq;
using ErrorHandling;
//using Nancy.Json;
using Newtonsoft.Json;

namespace Finance.UnitTests
{
    public class ReceiptInfoServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        private FileHelper FileHelper;

        public ReceiptInfoServiceTest()
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
        public async void GetReceiptInfoListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new ReceiptInfoService(db, Configuration);
                        ReceiptInfoFilter filter = new ReceiptInfoFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        ReceiptInfoSortByParam sortByParam = new ReceiptInfoSortByParam();

                        var results = await service.GetReceiptInfoListAsync(filter, pageParam, sortByParam);

                        //var json = new JavaScriptSerializer().Serialize(results.ReceiptInfos);

                        //Console.WriteLine(json);

                        return;
                    }
                });
            }
        }

        //[Fact]
        //public async void GetReceiptInfoProjectListAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                var service = new ReceiptInfoService(db, Configuration);
        //                var filter = new ReceiptInfoFilterViewProject();
        //                var pageParam = new PageParam { Page = 1, PageSize = 10 };
        //                var sortByParam = new ReceiptInfoSortProjectListByParam();

        //                var results = await service.GetReceiptInfoProjectListAsync(filter, pageParam, sortByParam);
        //            }
        //        });
        //    }
        //}

        //[Fact]
        //public async void GetReceiptInfoUnitListAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                var service = new ReceiptInfoService(db, Configuration);
        //                var filter = new ReceiptInfoFilterViewUnit();
        //                var pageParam = new PageParam { Page = 1, PageSize = 10 };
        //                var sortByParam = new ReceiptInfoSortUnitListByParam();

        //                var results = await service.GetReceiptInfoUnitListAsync(filter, pageParam, sortByParam);

        //                return;
        //            }
        //        });
        //    }
        //}

        //[Fact]
        //public async void GetUnitDropdowListForReceiptInfoAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                var service = new ReceiptInfoService(db, Configuration);

        //                string displayName = null;
        //                Guid? projectID = Guid.Parse("c959460a-d4f2-49b2-8533-664204dda641");
        //                Guid? unitID = null;

        //                var results = await service.GetUnitDropdowListForReceiptInfoAsync(displayName, projectID, unitID);
        //            }
        //        });
        //    }
        //}


        //[Fact]
        //public async void UpdateReceiptInfoAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    var service = new ReceiptInfoService(db, Configuration);
        //                    ReceiptInfoFilter filter = new ReceiptInfoFilter();
        //                    PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
        //                    ReceiptInfoSortByParam sortByParam = new ReceiptInfoSortByParam();

        //                    var ListReceiptInfo = await service.GetReceiptInfoListAsync(filter, pageParam, sortByParam);

        //                    var GUID = Guid.Parse("CB336079-4602-4729-9228-127E1B992881");

        //                    //var ReceiptInfoDTO = ListReceiptInfo.ReceiptInfos.Where(e => e.Id == GUID).FirstOrDefault() ;

        //                    //if (ReceiptInfoDTO != null)
        //                    //{
        //                    //    ReceiptInfoDTO.AttachFile.Url = "http://192.168.2.29:9001/finances/ReceiptInfo/2092/e5fe30e7-35f7-4406-a2d5-0f19c5498181_%E0%B8%A3%E0%B8%B2%E0%B8%A2%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%81%E0%B8%81%E0%B9%89%E0%B9%84%E0%B8%82_UI_CRM%28FI%29.docx?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=XNTYE7HIMF6KK4BVEIXA%2F20191108%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20191108T080640Z&X-Amz-Expires=21600&X-Amz-SignedHeaders=host&&X-Amz-Signature=54ca4febbaea378f525fada057a1ac8a1e6b50d8e6aad3e0ef9dc8f7b6cba6ce";
        //                    //    ReceiptInfoDTO.AttachFile.Name = "Test";
        //                    //    ReceiptInfoDTO.AttachFile.IsTemp = true;

        //                    //    //var results = await service.UpdateReceiptInfoAsync(ReceiptInfoDTO);

        //                    //    
        //                    //}

        //                    string json = "{\"paymentForeignBankTransfer\":{\"fee\":0,\"bankAccount\":null,\"isWrongAccount\":false,\"foreignBank\":null,\"foreignTransferType\":null,\"ir\":null,\"transferorName\":null,\"isRequestReceiptInfo\":false,\"isNotifyReceiptInfo\":false,\"notifyReceiptInfoMemo\":null,\"id\":\"70249dae-21d9-480f-9d21-921a2b2b2793\",\"updatedBy\":null,\"updated\":null},\"paymentCreditCard\":{\"isForeignCreditCard\":false,\"fee\":0,\"cardNo\":null,\"creditCardPaymentType\":null,\"creditCardType\":null,\"bank\":null,\"edc\":null,\"isWrongAccount\":false,\"id\":\"d2ff0ea9-ca05-4277-a9ff-077ceb697b58\",\"updatedBy\":null,\"updated\":null},\"bookingID\":\"21a668e2-e408-4a46-9ce3-536fc3db64ae\",\"customerName\":\"name1 name23\",\"ReceiptInfoAmount\":100,\"remark\":\"ทดสอบแก้ไข2-1\",\"ReceiptInfoStatus\":null,\"attachFile\":{\"url\":\"http://192.168.2.29:9001/temp/0127a67c-3ef9-41dc-86e7-09ac7f540ef1_TEST.docx?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=XNTYE7HIMF6KK4BVEIXA%2F20191120%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20191120T022034Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&&X-Amz-Signature=8b8711da912332ad99fbdaa3a10f6eed3e79f5ca84e37e5551e962d5b20285a6\",\"name\":\"0127a67c-3ef9-41dc-86e7-09ac7f540ef1_TEST.docx\",\"isTemp\":true},\"cancelRemark\":\"ทดสอบลบ\",\"rejectByUserID\":null,\"rejectDate\":null,\"rejectRemark\":\"ทดสอบ reject\",\"unit\":{\"id\":\"0bf8aa5b-327f-4b97-947a-4e9dd5d0e1c2\",\"unitNo\":\"N19B05\",\"houseNo\":\"62/891\",\"tower\":null,\"floor\":null,\"usedArea\":28.3,\"saleArea\":29.5,\"titledeedArea\":null,\"unitStatus\":null},\"project\":{\"projectNo\":\"40017\",\"projectNameTH\":\"Aspire เอราวัณ (ทาวเวอร์ บี)\",\"projectNameEN\":\"Aspire Erawan (Tower B)\",\"projectStatus\":null,\"productType\":null,\"bg\":null,\"id\":\"c959460a-d4f2-49b2-8533-664204dda641\",\"updatedBy\":null,\"updated\":null},\"countReceiptInfo\":null,\"countUnit\":null,\"sumAmountReceiptInfo\":null,\"paymentUserOwner\":{\"employeeNo\":\"AP002838\",\"title\":null,\"firstName\":\"อริยดา\",\"middleName\":null,\"lastName\":\"ตึกดี\",\"displayName\":\"อริยดา ตึกดี\",\"profilePicture\":null,\"email\":\"ariyada_t@apthai.com\",\"lastLoginTime\":null,\"lastActivityTime\":null,\"phoneNo\":null,\"lineId\":null,\"lineQRCode\":null,\"reportToUserID\":null,\"isClient\":false,\"clientID\":null,\"clientSecret\":null,\"userAuthorizeProjects\":null,\"userRoles\":null,\"id\":\"10cdbd3f-276d-4a88-9b19-b3d90ef6a574\",\"created\":\"0001-01-01 00:00:00\",\"updated\":\"0001-01-01 00:00:00\",\"createdByUserID\":null,\"createdBy\":null,\"updatedByUserID\":null,\"updatedBy\":null,\"isDeleted\":false,\"isFromMigration\":false},\"depositNo\":null,\"receiptTempNo\":null,\"receiveDate\":\"0001-01-01 00:00:00\",\"ReceiptInfoRequesterMasterCenter\":{\"id\":\"b18de578-cf18-4d92-9e2b-6bc440b38fe8\",\"name\":\"BC\",\"key\":\"3\"},\"ReceiptInfoStatusMasterCenter\":{\"id\":\"e736d6b9-07b4-4be3-8d2f-1a15068ef2e1\",\"name\":\"ขอ ReceiptInfo แล้ว\",\"key\":\"1\"},\"paymentMethodTypeMasterCenter\":{\"id\":\"519cac41-226d-4dee-8146-1a2b201bc40e\",\"name\":null,\"key\":null},\"bank\":{\"id\":\"5bf05562-213e-492f-810f-0032b7c9da9d\",\"bankNo\":\"026\",\"nameTH\":\"ธนาคาร เมกะ สากลพาณิชย์ จำกัด (มหาชน)\",\"nameEN\":\"Mega International Commercial Bank\",\"alias\":\"MEGA ICBC\"},\"id\":\"f761fbee-0616-4e8a-ab49-3a3679dfc3a3\",\"updatedBy\":null,\"updated\":null}";

        //                    //ReceiptInfoDTO model = new JsonConvert.DeserializeObject<ReceiptInfoDTO>(json);

        //                    ReceiptInfoDTO xx = new JavaScriptSerializer().Deserialize<ReceiptInfoDTO>(json);

        //                    return;
        //                    tran.Rollback();
        //                }
        //                catch (ValidateException ex)
        //                {
        //                    tran.Rollback();
        //                }

        //            }
        //        });
        //    }
        //}
    }
}
