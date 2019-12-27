using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using CustomAutoFixture;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Finance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sale.Params.Filters;
using Sale.Services;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class BudgetMinPriceWorkflowServiceTest
    {
        IConfiguration Configuration;
        private static readonly Fixture Fixture = new Fixture();

        public BudgetMinPriceWorkflowServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        

        [Fact]
        public async void GetAdhocAsync()
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
                        var paymentService = new PaymentService(this.Configuration, db);
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var service = new MinPriceBudgetWorkflowService(db, paymentService,bookingService);
                        var project = await db.Projects.FirstAsync();
                        var minPriceBudgetWorkflow = FixtureFactory.Get().Build<MinPriceBudgetWorkflow>()
                                                                         .With(o => o.ProjectID, project.ID)
                                                                         .Create();
                        var minPriceApproval = FixtureFactory.Get().Build<MinPriceBudgetApproval>()
                                                 .With(o => o.MinPriceBudgetWorkflowID, minPriceBudgetWorkflow.ID)
                                                 .Create();

                        await db.MinPriceBudgetWorkflows.AddAsync(minPriceBudgetWorkflow);
                        await db.MinPriceBudgetApprovals.AddAsync(minPriceApproval);
                        await db.SaveChangesAsync();

                        var filter = new BudgetMinPriceWorkflowFilter();
                        filter.ProjectID = (Guid?)project.ID;
                        await service.GetAdhocAsync(filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBudgetQuarterlyAsync()
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
                        var paymentService = new PaymentService(this.Configuration, db);
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var service = new MinPriceBudgetWorkflowService(db, paymentService, bookingService);
                        var project = await db.Projects.FirstAsync();
                        var minPriceBudgetWorkflow = FixtureFactory.Get().Build<MinPriceBudgetWorkflow>()
                                                                         .With(o => o.ProjectID, project.ID)
                                                                         .Create();
                        var minPriceApproval = FixtureFactory.Get().Build<MinPriceBudgetApproval>()
                                                 .With(o => o.MinPriceBudgetWorkflowID, minPriceBudgetWorkflow.ID)
                                                 .Create();

                        await db.MinPriceBudgetWorkflows.AddAsync(minPriceBudgetWorkflow);
                        await db.MinPriceBudgetApprovals.AddAsync(minPriceApproval);
                        await db.SaveChangesAsync();

                        var filter = new BudgetMinPriceWorkflowFilter();
                        await service.GetBudgetQuarterlyAsync(filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetBudgetMinPriceWorkFlowsAsync()
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
                        var paymentService = new PaymentService(this.Configuration, db);
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var service = new MinPriceBudgetWorkflowService(db, paymentService, bookingService);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();
                        //var minPriceBudgetWorkflow = FixtureFactory.Get().Build<MinPriceBudgetWorkflow>()
                        //                                                 .With(o=>o.ProjectID,project.ID)
                        //                                                 .Create();
                        //var minPriceApproval = FixtureFactory.Get().Build<MinPriceBudgetApproval>()
                        //                         .With(o => o.MinPriceBudgetWorkflowID, minPriceBudgetWorkflow.ID)
                        //                         .Create();

                        //await db.MinPriceBudgetWorkflows.AddAsync(minPriceBudgetWorkflow);
                        //await db.MinPriceBudgetApprovals.AddAsync(minPriceApproval);
                        //await db.SaveChangesAsync();
                        var user = await db.Users.Where(o => o.ID == new Guid("64AC5F60-58E5-4F1A-89C7-82F342EC652A")).FirstAsync();
                        var filter = new BudgetMinPriceWorkflowFilter();
                        filter.ProjectID = (Guid?)project.ID;
                        filter.Quarter = 4;
                        filter.Year = 2019;
                        var results = await service.GetMinPriceBudgetWorkFlowsAsync(filter, user.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ApproveMinPriceBudgetWorkFlowAsync()
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
                        var paymentService = new PaymentService(this.Configuration, db);
                        var priceListWorkflow = new PriceListWorkflowService(db);
                        var quotationService = new QuotationService(priceListWorkflow, this.Configuration, db);
                        var bookingService = new BookingService(this.Configuration, quotationService, db);
                        var service = new MinPriceBudgetWorkflowService(db, paymentService, bookingService);
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == "N05D01").FirstAsync();
                        var minPriceWfStage = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceBudgetWorkflowStage" && o.Key == "1").FirstAsync();
                        var minPriceWfType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "MinPriceWorkflowType" && o.Key == "1").FirstAsync();
                        var budgetPromotionType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "BudgetPromotionType" && o.Key == "1").FirstAsync();
                        var datas = new List<MinPriceBudgetWorkflowDTO>()
                        {
                           new MinPriceBudgetWorkflowDTO
                           {
                               Project = ProjectDropdownDTO.CreateFromModel(project),
                               Unit = UnitDropdownDTO.CreateFromModel(unit),
                               MinPriceBudgetWorkflowStage = MasterCenterDropdownDTO.CreateFromModel(minPriceWfStage),
                               SellingPrice = 3930900,
                               MasterMinPrice = 3525446.2289M,
                               RequestMinPrice = -285453.7711M,
                               LowerMinPrice = -3810900,
                               MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minPriceWfType),
                               MasterBudgetPromotion = 120000,
                               RequestBudgetPromotion = 0,
                               BudgetPromotionType = MasterCenterDropdownDTO.CreateFromModel(budgetPromotionType),
                               Wait = "Living Consultant Manager",
                               IsApproved = null,
                               RejectComment = null,
                               Id= new Guid("3956bd1d-7adf-4bca-8d09-384ffe5da9ce"),
                               UpdatedBy = null,
                               Updated = null,
                           }
                        };

                        await service.ApproveMinPriceBudgetWorkFlowAsync(datas);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
