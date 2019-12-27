using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Params.Filters;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Project.UnitTests
{
    public class BudgetMinPriceServiceTest
    {
        IConfiguration Configuration;
        public BudgetMinPriceServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

       // [Fact]
        //public async void GetBudgetMinPriceAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                //Put unit test here                     
        //                var project = await db.Projects.Where(o => o.ProjectNo == "10179").FirstAsync();
        //                var service = new BudgetMinPriceService(Configuration, db);
        //                var filter = new BudgetMinPriceFilter
        //                {
        //                    ProjectID = project.ID,
        //                    Quarter = 4,
        //                    Year = 2018
        //                };
        //                var result = await service.GetBudgetMinPriceAsync(filter);
        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        //[Fact]
        //public async void SaveBudgetMinPriceAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                //Put unit test here                     
        //                var project = await db.Projects.Where(o => o.ProjectNo == "10179").FirstAsync();
        //                var service = new BudgetMinPriceService(Configuration, db);
        //                var input = new BudgetMinPriceDTO
        //                {
        //                    Project = ProjectDropdownDTO.CreateFromModel(project),
        //                    Quarter = 2,
        //                    Year = 2018,
        //                    QuarterlyTotalAmount = 6,
        //                    TransferTotalAmount = 7,
        //                    TransferTotalUnit = 7
        //                };
        //                var filter = new BudgetMinPriceFilter
        //                {
        //                    ProjectID = project.ID,
        //                    Quarter = 2,
        //                    Year = 2018
        //                };
        //                var result = await service.SaveBudgetMinPriceAsync(filter, input);
        //                var resultUnit = await service.GetBudgetMinPriceUnitListAsync(filter);
        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        //[Fact]
        //public async void GetBudgetMinPriceUnitListAsync()
        //{
        //    using (var factory = new UnitTestDbContextFactory())
        //    {
        //        var db = factory.CreateContext();
        //        var strategy = db.Database.CreateExecutionStrategy();
        //        await strategy.ExecuteAsync(async () =>
        //        {
        //            using (var tran = db.Database.BeginTransaction())
        //            {
        //                //Put unit test here                     
        //                var project = await db.Projects.Where(o => o.ProjectNo == "10179").FirstAsync();
        //                //var project = await db.Projects.Where(o => o.ID == Guid.Parse("f47dc4e6-2dd8-4737-9800-a0b24a5cbd1f")).FirstAsync();
        //                var service = new BudgetMinPriceService(Configuration, db);
        //                var filter = new BudgetMinPriceFilter
        //                {
        //                    ProjectID = project.ID,
        //                    Quarter = 2,
        //                    Year = 2018
        //                };
        //                var result = await service.GetBudgetMinPriceUnitListAsync(filter);
        //                tran.Rollback();
        //            }
        //        });
        //    }
        //}

        [Fact]
        public async void SaveBudgetMinPriceUnitListAsync()
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
                        var project = await db.Projects.Where(o => o.ProjectNo == "10179").FirstAsync();
                        var unit = await db.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == "C02-3").FirstAsync();
                        var service = new BudgetMinPriceService(Configuration, db);
                      
                        var inputs = new BudgetMinPriceListDTO
                        {
                            BudgetMinPriceDTO = new BudgetMinPriceDTO(),
                            BudgetMinPriceUnitDTO = new List<BudgetMinPriceUnitDTO>()
                        };
                        var result = service.SaveBudgetMinPriceUnitListAsync(inputs);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportQuarterlyBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/QuarterlySaleBudget_10059_2019_2.xlsx",
                            Name = "QuarterlySaleBudget_10059_2019_2.xlsx"
                        };
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2019
                        };
                        var result = await service.ImportQuarterlyBudgetAsync(fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ConfirmImportQuarterlyBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/QuarterlySaleBudget_10059_2019_2.xlsx",
                            Name = "QuarterlySaleBudget_10059_2019_2.xlsx"
                        };
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2019
                        };
                        var result = await service.ImportQuarterlyBudgetAsync(fileInput);

                       // var resultConfirm = await service.ConfirmImportQuarterlyBudgetAsync(filter, result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportTransferBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/TransferSaleBudget_0_2018_2.xlsx",
                            Name = "TransferSaleBudget_0_2018_2.xlsx"
                        };
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2019
                        };
                        var result = await service.ImportTransferBudgetAsync(fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ConfirmImportTransferBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/TransferSaleBudget_0_2018_2.xlsx",
                            Name = "TransferSaleBudget_0_2018_2.xlsx"
                        };
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2019
                        };
                        var result = await service.ImportTransferBudgetAsync(fileInput);
                        await service.ConfirmImportTransferBudgetAsync(result);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportTransferBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2018
                        };
                        var result = await service.ExportTransferBudgetAsync(filter);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportQuarterlyBudgetAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new BudgetMinPriceService(Configuration, db);
                        var project = await db.Projects.Where(o => o.ProjectNo == "10059").FirstAsync();
                        var filter = new BudgetMinPriceFilter
                        {
                            ProjectID = project.ID,
                            Quarter = 2,
                            Year = 2018
                        };
                        var result = await service.ExportQuarterlyBudgetAsync(filter);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
