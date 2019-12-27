using AutoFixture;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models.MST;
using Database.UnitTestExtensions;
using ErrorHandling;
using FileStorage;
using Finance.Params.Filters;
using Finance.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Finance.UnitTests
{
    public class DirectCreditDebitApprovalFormTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        private FileHelper FileHelper;

        public DirectCreditDebitApprovalFormTest()
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

        //[Fact]
        //public async void ExportExcelUnitInitialAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                var service = new DirectCreditDebitApprovalFormService(Configuration, db);
        //                var project = await db.Projects.FirstOrDefaultAsync(o => o.SapCode == "A/G01");
        //                var result = await service.ExportExcelUnitInitialAsync(project.ID);
        //                //Trace.WriteLine(result.Url);
        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        [Fact]
        public async void GetDirectCreditDebitApprovalFormListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                     
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var service = new DirectCreditDebitApprovalFormService(db, Configuration);

                            //var service = new DirectCreditDebitApprovalFormService(Configuration, db);
                            DirectCreditDebitApprovalFormFilter filter = new DirectCreditDebitApprovalFormFilter();
                            PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                            //filter.CreateDateTo = DateTime.Today;
                            DirectCreditDebitApprovalFormSortByParam sortByParam = new DirectCreditDebitApprovalFormSortByParam();
                            //var results1 = await service.ExportRequestAsync(filter, pageParam, sortByParam);
                            //var results1 = await service.GetUnitListAsync(null);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            var results2 = ex;
                        }


                    }
                });
            }
        }

        //[Fact]
        //public async void GetDirectCreditDebitApprovalFormListAsync()
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
        //                    var service = new DirectCreditDebitApprovalFormService(Configuration, db);
        //                    DirectCreditDebitApprovalFormFilter filter = new DirectCreditDebitApprovalFormFilter();
        //                    PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };

        //                    //Guid id = Guid.Parse("4247f748-5fe7-470b-b424-14f72ae37902");

        //                    //List<Guid> listNewID = new List<Guid>();
        //                    //filter.Company = Guid.Parse("bd54dea9-9f6d-4028-9624-03a0ffa45684");
        //                    DirectCreditDebitApprovalFormSortByParam sortByParam = new DirectCreditDebitApprovalFormSortByParam();
        //                    var results1 = await service.GetDirectCreditDebitApprovalFormListAsync(filter, pageParam, sortByParam);
        //                    //var results2 = await service.ExportRequestAsync(filter, pageParam, sortByParam);

        //                    DirectCreditDebitApprovalFormDTO up = new DirectCreditDebitApprovalFormDTO();
        //                    up.DirectApprovalFormStatus = new MasterCenterDropdownDTO();
        //                    up.DirectApprovalFormStatus.Id = Guid.Parse("BDE49883-6F1B-4379-9354-414062095FC5");
        //                    up.DirectApprovalFormStatus.Key = "2";
        //                    //up = results1.DirectCreditDebitApprovalForms.FirstOrDefault();
        //                    //var results3 = await service.UpdateDirectCreditDebitApprovalFormAsync(Guid.Parse("56E8F71D-084C-4006-A2C3-78E54B011111"), results1.DirectCreditDebitApprovalForms.FirstOrDefault());
        //                    //var results4 = await service.CreateDirectCreditDebitApprovalFormAsync(results1.DirectCreditDebitApprovalForms.FirstOrDefault());
        //                    // var results5 = await service.GetDirectCreditDebitApprovalFormAsync(Guid.Parse("BDE49883-6F1B-4379-9354-414062095FC5"));
        //                    var results56 = await service.GetStatusDropdowListAsync();
        //                    tran.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    var results2 = ex;
        //                }


        //            }
        //        });
        //    }
        //}
    }
}
