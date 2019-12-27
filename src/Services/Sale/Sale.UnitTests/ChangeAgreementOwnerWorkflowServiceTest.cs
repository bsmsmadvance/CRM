using AutoFixture;
using Base.DTOs.SAL;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sale.UnitTests
{
    public class ChangeAgreementOwnerWorkflowServiceTest
    {
       
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;

        public ChangeAgreementOwnerWorkflowServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void CreateChangeAgreementOwnerWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var service = new ChangeAgreementOwnerWorkflowService(db);
                        var changeAgreementOwnerType = db.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey== "ChangeAgreementOwnerType").FirstOrDefault();


                        var input = new ChangeAgreementOwnerWorkflowDTO();
                        input.AppointmentDate = DateTime.Now;
                        input.NewTransferOwnershipDate = DateTime.Now;
                        input.Fee = 900;
                        input.ChangeAgreementOwnerType = MasterCenterDropdownDTO.CreateFromModel(changeAgreementOwnerType);
                        var contact = db.Contacts.Where(o => o.ID == new Guid("E030BEB9-B4C6-41CD-8A54-846F2EDEF3FA")).ToList() ?? new List<Database.Models.CTM.Contact>();
                        var ListAgreementOwner = new List<AgreementOwnerDTO>();

                        foreach(var c in contact)
                        {
                            var i = new AgreementOwnerDTO();
                            i.Agreement = new AgreementDropdownDTO();
                            i.FromContactID = c.ID;
                            i.Agreement.Id = new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318");
                            i.ChangeAgreementOwnerInType = false;
                            ListAgreementOwner.Add(i);
                        }

                             var results = await service.CreateChangeAgreementOwnerWorkflowAsync(input, ListAgreementOwner);

                        tran.Rollback();
                    }
                });
            }
        }

        //[Fact]
        //public async void CreateAgreementOwnerAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                //var agreement = await db.Agreements.Where(o => o.ID == new Guid("8F231EAF-BF29-4273-8FDF-5D5EF723CF2C")).FirstAsync();
        //                //var contact = await db.Contacts.Where(o => o.ID == new Guid("AF3BB879-7E85-4B25-A81A-000006C8B310")).FirstOrDefaultAsync();
        //                var contact = await db.Contacts.FirstAsync(o => o.ID == new Guid("60634210-9F09-4698-97C2-00020C8A5E16"));
        //                var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("832ACE49-E008-434B-977A-A30E9B2FF879")).FirstAsync();
        //                // Act
        //                var service = new ChangeAgreementOwnerWorkflowService(db);
        //                var results = await service.CreateAgreementOwnerAsync(agreement.ID, contact.ID);

        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        //[Fact]
        //public async void DeleteChangeAgreementOwnerWorkflowAsync()
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
        //                    var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("832ACE49-E008-434B-977A-A30E9B2FF879")).FirstAsync();

        //                    // Act
        //                    var service = new ChangeAgreementOwnerWorkflowService(db);
        //                    await service.DeleteChangeAgreementOwnerWorkflowAsync(agreement.ID);
        //                    tran.Rollback();
        //                }
        //                catch (Exception ex)
        //                {
        //                    tran.Rollback();
        //                }
        //            }
        //        });
        //    }
        //}

        [Fact]
        public async void ValidateAgreementOwnerWorkflowAsync()
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
                            var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("832ACE49-E008-434B-977A-A30E9B2FF879")).FirstAsync();

                            // Act
                            var service = new ChangeAgreementOwnerWorkflowService(db);
                            await service.ValidateAgreementOwnerWorkflowAsync(agreement.ID);
                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                });
            }
        }

        [Fact]
        public async void GetAgreementOwnersChangeAgreementOwnerWorkflowAsync()
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
                            var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318")).FirstAsync();
                            var changeAgreementOwnerWorkflows =  new Guid("7235117D-725E-466B-8B11-DB9C65F97FCB");

                            // Act
                            var service = new ChangeAgreementOwnerWorkflowService(db);
                            await service.GetAgreementOwnersChangeAgreementOwnerWorkflowAsync(agreement.ID, changeAgreementOwnerWorkflows);
                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                });
            }
        }
        [Fact]
        public async void GetChangeAgreementOwnerWorkflowAsync()
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
                            var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("d0f0abb7-efec-4fee-82a8-d2e05bb9c318")).FirstAsync();
                         
                            // Act
                            var service = new ChangeAgreementOwnerWorkflowService(db);
                            await service.GetChangeAgreementOwnerWorkflowAsync(agreement.ID);
                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                });
            }
        }

        [Fact]
        public async void CancelApproveChangeAgreementOwnerWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var service = new ChangeAgreementOwnerWorkflowService(db);
                        //var changeAgreementOwnerType = db.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "ChangeAgreementOwnerType").FirstOrDefault();


                        var input = new ChangeAgreementOwnerWorkflowDTO();
                        input.Id = new Guid("29BEAF90-35D3-4B4B-AD5F-FAA3A45A4026");
                        var UserID  = new Guid("");
                        //input.ChangeAgreementOwnerType = MasterCenterDropdownDTO.CreateFromModel(changeAgreementOwnerType);



                        var results = await service.CancelApproveChangeAgreementOwnerWorkflowAsync(input, UserID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveChangeAgreementOwnerWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var service = new ChangeAgreementOwnerWorkflowService(db);
                        //var changeAgreementOwnerType = db.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "ChangeAgreementOwnerType").FirstOrDefault();


                        var input = new ChangeAgreementOwnerWorkflowDTO();
                        input.Id =  new Guid("29BEAF90-35D3-4B4B-AD5F-FAA3A45A4026");
                        var UserID = new Guid("");

                        //input.ChangeAgreementOwnerType = MasterCenterDropdownDTO.CreateFromModel(changeAgreementOwnerType);



                        var results = await service.ApproveChangeAgreementOwnerWorkflowAsync(input, UserID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveRequestChangeAgreementOwnerWorkflowAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Act
                        var service = new ChangeAgreementOwnerWorkflowService(db);
                        //var changeAgreementOwnerType = db.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == "ChangeAgreementOwnerType").FirstOrDefault();


                        var input = new ChangeAgreementOwnerWorkflowDTO();
                        input.Id = new Guid("29BEAF90-35D3-4B4B-AD5F-FAA3A45A4026");
                        var UserID = new Guid("");
                        //input.ChangeAgreementOwnerType = MasterCenterDropdownDTO.CreateFromModel(changeAgreementOwnerType);



                        var results = await service.ApproveRequestChangeAgreementOwnerWorkflowAsync(input, UserID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
