
using AutoFixture;
using Database.Models;
using Database.UnitTestExtensions;
using FileStorage;
using Finance.Services;
using Finance.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Base.DTOs.ACC.CalendarLockDTO;

namespace Accounting.UnitTests
{
    public class BillPaymentServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        private readonly IPaymentService PaymentService;
        private FileHelper FileHelper;
        public BillPaymentServiceTest()
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
        public async void GetCalendarLockListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new BillPaymentService(db, Configuration, PaymentService);
                        //CalendarLockReq filter = new CalendarLockReq();
                        //filter.Guid = "78E2BBF2-0764-4149-8EA1-48F715A09A1E";
                        //filter.CalendarLock = "22/10/2019";

                        var ssssssssss = Guid.Parse("AB2A2D82-EE7E-49C7-92A7-6F3B7104B059");
                        //var results = await service.GetBillPaymentSplitAsync(ssssssssss);



                        //DateTime CalendarLock = new DateTime(2019, 8, 23);
                        //string input = "78e2bbf2-0764-4149-8ea1-48f715a09a1e";

                        //var listGuid = new List<Guid>();
                        //var xxx = input.Split(',');
                        //if (!string.IsNullOrEmpty(input))
                        //{
                        //    foreach (var item in xxx)
                        //    {
                        //        var newGuid = Guid.NewGuid();
                        //        var chkGuid = Guid.TryParse(item, out newGuid);

                        //        if (!chkGuid)
                        //        {
                        //            // Return ""l
                        //            break;
                        //        }

                        //        listGuid.Add(newGuid);
                        //    }
                        //    bool Check;
                        //  DatabaseContext xDB = new DatabaseContext();

                        //foreach (var chk in listGuid)
                        //{
                        //    Check = await Database.Models.Extensions.CheckCalendarAsync(chk, CalendarLock, db);
                        //}


                        //CalendarLockFilter filter = new CalendarLockFilter();

                        //filter.Companies = "7bd15e0f-9571-47a3-bd3f-0e94a7fe905f,00000000-0000-0000-0000-000000000002";
                        //filter.Year = 2019;
                        //filter.Month = 03;
                        //var results = await service.GetCalendarLockListAsync(filter);


                        //CalendarLockResult xxxx = new CalendarLockResult();

                        //xxxx.Guid = "0f191e85-de0b-4902-bbf2-103e241bdedd,4047b765-9ac1-4e11-a259-1d9dffb3e5ca";
                        //xxxx.CalendarLock = new DateTime(2019, 3, 22);

                        //var results2 = await service.GetCalendarLockHistoryAsync(xxxx);




                        //System.Collections.Generic.List<CalendarLockResult> test = new System.Collections.Generic.List<CalendarLockResult>();

                        //CalendarLockResult testx = new CalendarLockResult();
                        //testx.Guid = "0f191e85-de0b-4902-bbf2-103e241bdedd,4047b765-9ac1-4e11-a259-1d9dffb3e5ca";
                        //testx.CalendarLock = new DateTime(2019, 3, 22);
                        //testx.staus = 0;
                        //test.Add(testx);

                        //testx = new CalendarLockResult();
                        //testx.Guid = "0f191e85-de0b-4902-bbf2-103e241bdedd,4047b765-9ac1-4e11-a259-1d9dffb3e5ca";
                        //testx.CalendarLock = new DateTime(2019, 3, 25);
                        //testx.staus = 1;
                        //test.Add(testx);

                        //var results3 = await service.CreateCalendarLockAsync(test);


                    }
                });
            }
        }

    }
}
