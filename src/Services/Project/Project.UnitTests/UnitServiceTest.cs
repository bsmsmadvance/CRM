using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using Xunit;
using models = Database.Models;
using Database.Models;

namespace Project.UnitTests
{
    public class UnitServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        private FileHelper FileHelper;

        public UnitServiceTest()
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
        public async void GetUnitDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var tower = await db.Towers.FirstAsync(o => o.ProjectID == project.ID);
                        var floor = await db.Floors.FirstAsync(o => o.TowerID == tower.ID);
                        var results = await service.GetUnitDropdownListAsync(project.ID, null, null, "A3");
                        Trace.WriteLine(JsonConvert.SerializeObject(results.First(), Formatting.Indented));
                        results = await service.GetUnitDropdownListAsync(project.ID, null, null, null, UnitStatusKeys.Available);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitDropdownWithSellPriceListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var results = await service.GetUnitDropdownWithSellPriceListAsync(project.ID, "A3");
                        Trace.WriteLine(JsonConvert.SerializeObject(results.First(), Formatting.Indented));
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "10062");

                        //filter test
                        UnitFilter filter = FixtureFactory.Get().Build<UnitFilter>().Create();
                        filter.UnitDirectionKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "UnitDirection")
                                                                     .Select(x => x.Key).FirstAsync();
                        filter.UnitTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "UnitType")
                                                                     .Select(x => x.Key).FirstAsync();
                        filter.UnitStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.Key).FirstAsync();
                        PageParam pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        UnitListSortByParam sortByParam = new UnitListSortByParam();
                        var results = await service.GetUnitListAsync(project.ID, filter, pageParam, sortByParam);

                        //sort by test
                        filter = new UnitFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UnitListSortBy)).Cast<UnitListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UnitListSortByParam() { SortBy = item };
                            results = await service.GetUnitListAsync(project.ID, filter, pageParam, sortByParam);
                        }

                        Trace.WriteLine(JsonConvert.SerializeObject(results.Units.First(), Formatting.Indented));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var unit = await db.Units.FirstAsync(o => o.ProjectID == project.ID);
                        var result = await service.GetUnitAsync(project.ID, unit.ID);
                       
                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        UnitDTO input = new UnitDTO();
                        input.UnitNo = "ATEST00001";
                        input.HouseNo = "111/1111";
                        input.HouseNoReceivedYear = 2019;
                        var model = await db.Models.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        var floorPlan = await db.FloorPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.FloorPlan = await FloorPlanImageDTO.CreateFromModelAsync(floorPlan, this.FileHelper);
                        var roomPlan = await db.RoomPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.RoomPlan = await RoomPlanImageDTO.CreateFromModelAsync(roomPlan, this.FileHelper);
                        var unitDirection = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitDirection");
                        input.UnitDirection = MasterCenterDropdownDTO.CreateFromModel(unitDirection);
                        var unitType = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitType");
                        input.UnitType = MasterCenterDropdownDTO.CreateFromModel(unitType);
                        var unitStatus = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitStatus");
                        input.UnitStatus = MasterCenterDropdownDTO.CreateFromModel(unitStatus);
                        input.SaleArea = 30;
                        var tower = await db.Towers.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Tower = TowerDropdownDTO.CreateFromModel(tower);
                        var floor = await db.Floors.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Floor = FloorDropdownDTO.CreateFromModel(floor);
                        input.NumberOfPrivilege = 1;
                        input.NumberOfParkingFix = 1;
                        input.NumberOfParkingUnFix = 1;
                        input.IsForeignUnit = false;
                        input.Position = "Under";

                        var result = await service.CreateUnitAsync(project.ID, input);

                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var unit = await db.Units.FirstAsync(o => o.ProjectID == project.ID);
                        UnitDTO input = await service.GetUnitAsync(project.ID, unit.ID);
                        input.UnitNo = "ATEST00001";
                        input.HouseNo = "111/1111";
                        input.HouseNoReceivedYear = 2019;
                        var model = await db.Models.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        var floorPlan = await db.FloorPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.FloorPlan = await FloorPlanImageDTO.CreateFromModelAsync(floorPlan, this.FileHelper);
                        var roomPlan = await db.RoomPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.RoomPlan = await RoomPlanImageDTO.CreateFromModelAsync(roomPlan, this.FileHelper);
                        var unitDirection = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitDirection");
                        input.UnitDirection = MasterCenterDropdownDTO.CreateFromModel(unitDirection);
                        var unitType = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitType");
                        input.UnitType = MasterCenterDropdownDTO.CreateFromModel(unitType);
                        var unitStatus = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitStatus");
                        input.UnitStatus = MasterCenterDropdownDTO.CreateFromModel(unitStatus);
                        input.SaleArea = 30;
                        var tower = await db.Towers.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Tower = TowerDropdownDTO.CreateFromModel(tower);
                        var floor = await db.Floors.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Floor = FloorDropdownDTO.CreateFromModel(floor);
                        input.NumberOfPrivilege = 1;
                        input.NumberOfParkingFix = 1;
                        input.NumberOfParkingUnFix = 1;
                        input.IsForeignUnit = false;
                        input.Position = "Under";

                        var result = await service.UpdateUnitAsync(project.ID, unit.ID, input);

                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteUnitAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        UnitDTO input = new UnitDTO();
                        input.UnitNo = "ATEST00001";
                        input.HouseNo = "111/1111";
                        input.HouseNoReceivedYear = 2019;
                        var model = await db.Models.Where(o => o.ProjectID == project.ID).FirstOrDefaultAsync();
                        input.Model = ModelDropdownDTO.CreateFromModel(model);
                        var floorPlan = await db.FloorPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.FloorPlan = await FloorPlanImageDTO.CreateFromModelAsync(floorPlan, this.FileHelper);
                        var roomPlan = await db.RoomPlanImages.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.RoomPlan = await RoomPlanImageDTO.CreateFromModelAsync(roomPlan, this.FileHelper);
                        var unitDirection = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitDirection");
                        input.UnitDirection = MasterCenterDropdownDTO.CreateFromModel(unitDirection);
                        var unitType = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitType");
                        input.UnitType = MasterCenterDropdownDTO.CreateFromModel(unitType);
                        var unitStatus = await db.MasterCenters.FirstAsync(x => x.MasterCenterGroupKey == "UnitStatus");
                        input.UnitStatus = MasterCenterDropdownDTO.CreateFromModel(unitStatus);
                        input.SaleArea = 30;
                        var tower = await db.Towers.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Tower = TowerDropdownDTO.CreateFromModel(tower);
                        var floor = await db.Floors.FirstOrDefaultAsync(o => o.ProjectID == project.ID);
                        input.Floor = FloorDropdownDTO.CreateFromModel(floor);
                        input.NumberOfPrivilege = 1;
                        input.NumberOfParkingFix = 1;
                        input.NumberOfParkingUnFix = 1;
                        input.IsForeignUnit = false;
                        input.Position = "Under";

                        var resultCreate = await service.CreateUnitAsync(project.ID, input);

                        await service.DeleteUnitAsync(project.ID, resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitInfoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    UnitService service = new UnitService(Configuration, db);
                    var titleDeed = await db.TitledeedDetails.Include(o => o.Unit).FirstAsync();
                    var result = await service.GetUnitInfoAsync(titleDeed.Unit.ProjectID.Value, titleDeed.UnitID.Value);
                    //http://crmrevo-project-api-crmrevo-dev.devops-app.apthai.com/api/Projects/45a5dcb5-fbb6-4687-8dba-59835177202f/Units/baa1f2cd-e2af-4af5-b63b-bcd3b74a22c7/Info
                    //result = await service.GetUnitInfoAsync(Guid.Parse("45a5dcb5-fbb6-4687-8dba-59835177202f"), Guid.Parse("baa1f2cd-e2af-4af5-b63b-bcd3b74a22c7"));

                    Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                });
            }
        }

        [Fact]
        public async void ImportUnitInitialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.SapCode == "2/018");
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_InitialFromSAP.XLS",
                            Name = "ProjectID_InitialFromSAP.XLS"
                        };
                        var result = await service.ImportUnitInitialAsync(project.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportUnitGeneralAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.Include(o => o.Brand).ThenInclude(o => o.UnitNumberFormat).FirstOrDefaultAsync(o => o.SapCode == "A/G01");

                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_Units.xlsx",
                            Name = "ProjectID_Units.xlsx"
                        };
                        var result = await service.ImportUnitGeneralAsync(project.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportUnitFenceAreaAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_Address.xlsx",
                            Name = "ProjectID_Address.xlsx"
                        };
                        var result = await service.ImportUnitFenceAreaAsync(project.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetUnitMeterListAsync()
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
                        var projectIDs = await db.Projects.Take(3).Select(o => o.ID).ToListAsync();
                        var service = new UnitService(Configuration, db);

                        UnitMeterFilter filter = FixtureFactory.Get().Build<UnitMeterFilter>()
                        .With(o => o.ProjectIDs, string.Join(',', projectIDs.Select(o => o.ToString()).ToList()))
                        .Create();

                        PageParam pageParam = new PageParam();
                        UnitMeterListSortByParam sortByParam = new UnitMeterListSortByParam();

                        filter.ElectricMeterStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "MeterStatus")
                                                                      .Select(x => x.Key).FirstAsync();
                        filter.WaterMeterStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "MeterStatus")
                                                                      .Select(x => x.Key).FirstAsync();
                        filter.UnitStatusKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "UnitStatus")
                                                                     .Select(x => x.Key).FirstAsync();

                        var results = await service.GetUnitMeterListAsync(filter, pageParam, sortByParam);
                        filter = new UnitMeterFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UnitMeterListSortBy)).Cast<UnitMeterListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UnitMeterListSortByParam() { SortBy = item };
                            results = await service.GetUnitMeterListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }
        [Fact]
        public async void GetUnitMeterAsync()
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
                        var service = new UnitService(Configuration, db);
                        var project = FixtureFactory.Get().Build<models.PRJ.Project>()
                                             .With(o => o.IsDeleted, false)
                                             .Create();
                        var unit = FixtureFactory.Get().Build<models.PRJ.Unit>()
                                          .With(o => o.ProjectID, project.ID)
                                          .With(o => o.IsDeleted, false)
                                          .With(o => o.TitledeedDetails, new List<models.PRJ.TitledeedDetail>())
                                          .Create();
                        await db.Projects.AddAsync(project);
                        await db.Units.AddAsync(unit);
                        await db.SaveChangesAsync();


                        var result = await service.GetUnitMeterAsync(unit.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateUnitMeterAsync()
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
                        var service = new UnitService(Configuration, db);
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


                        var input = new UnitMeterDTO();
                        input.Id = unit.ID;
                        input.WaterMeter = "50";
                        input.ElectricMeter = "";
                        input.IsTransferElectricMeter = true;


                        var result = await service.UpdateUnitMeterAsync(unit.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteUnitMeterAsync()
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
                        var service = new UnitService(Configuration, db);
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


                        var input = new UnitMeterDTO();
                        input.Id = unit.ID;
                        input.WaterMeter = "50";
                        input.ElectricMeter = "100";


                        var result = await service.UpdateUnitMeterAsync(unit.ID, input);

                        var resultDelete = await service.DeleteUnitMeterAsync(unit.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelUnitInitialAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.SapCode == "A/G01");
                        var result = await service.ExportExcelUnitInitialAsync(project.ID);
                        Trace.WriteLine(result.Url);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelUnitGeneralAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.SapCode == "A/G01");
                        var result = await service.ExportExcelUnitGeneralAsync(project.ID);
                        Trace.WriteLine(result.Url);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportExcelUnitFenceAreaAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var result = await service.ExportExcelUnitFenceAreaAsync(project.ID);
                        Trace.WriteLine(result.Url);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportUnitMeterExcelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/UnitMeter.xlsx",
                            Name = "UnitMeter.xlsx"
                        };
                        var result = await service.ImportUnitMeterExcelAsync(fileInput);
                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportUnitMeterExcelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        UnitMeterFilter filter = new UnitMeterFilter()
                        {
                            ProjectIDs = project.ID.ToString()
                        };
                        UnitMeterListSortByParam sortBy = new UnitMeterListSortByParam();
                        var result = await service.ExportUnitMeterExcelAsync(filter, sortBy);
                        Trace.WriteLine(result.Url);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ImportUnitMeterStatusExcelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/UnitMeterStatus.xlsx",
                            Name = "UnitMeterStatus.xlsx"
                        };
                        var result = await service.ImportUnitMeterStatusExcelAsync(fileInput);
                        var testresult = await db.Units.Where(o => o.ElectricMeterTopicMasterCenterID != null).ToListAsync();
                        Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportUnitMeterStatusExcelAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        UnitMeterFilter filter = new UnitMeterFilter()
                        {
                            ProjectIDs = project.ID.ToString()
                        };
                        UnitMeterListSortByParam sortBy = new UnitMeterListSortByParam();
                        var result = await service.ExportUnitMeterStatusExcelAsync(filter, sortBy);
                        Trace.WriteLine(result.Url);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestUnitDataStatus()
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
                        var service = new ProjectService(Configuration, db);
                        var serviceModel = new ModelService(db);
                        var serviceUnit = new UnitService(Configuration, db);

                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var modelUnitType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ModelUnitType").FirstAsync();
                        var typeOfRealEstate = await db.TypeOfRealEstates.Where(o => !o.IsDeleted).FirstAsync();
                        var unitStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "UnitStatus" && o.Key == "0").FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreateProject = await service.CreateProjectAsync(input);

                        var inputModel = new ModelDTO();

                        inputModel.Code = "2222";
                        inputModel.NameTH = "เทส";
                        inputModel.NameEN = "Test";
                        inputModel.ModelUnitType = MasterCenterDropdownDTO.CreateFromModel(modelUnitType);
                        inputModel.TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(typeOfRealEstate);
                        inputModel.WaterElectricMeterPrices = new List<WaterElectricMeterPriceDTO>
                        {
                            new WaterElectricMeterPriceDTO
                            {
                                WaterMeterPrice=5,
                                WaterMeterSize="5",
                                ElectricMeterPrice=5,
                                ElectricMeterSize="5"
                            }
                        };
                        var resultCreateModel = await serviceModel.CreateModelAsync(resultCreateProject.Id.Value, inputModel);
                        var model = await db.Models.Where(o => o.ID == resultCreateModel.Id.Value).FirstAsync();
                        var inputUnit = new UnitDTO();
                        inputUnit.SapwbsObject = "22222";
                        inputUnit.SapwbsNo = "44444";
                        inputUnit.Model = ModelDropdownDTO.CreateFromModel(model);
                        inputUnit.SaleArea = 5555;
                        inputUnit.UnitStatus = MasterCenterDropdownDTO.CreateFromModel(unitStatus);
                        inputUnit.RoomPlan = new RoomPlanImageDTO { Name = "Test" };
                        inputUnit.FloorPlan = new FloorPlanImageDTO { Name = "Test" };

                        var resultsCreateUnit = await serviceUnit.CreateUnitAsync(resultCreateProject.Id.Value, inputUnit);
                        var resultDataStatusWhenCreate = await service.GetProjectDataStatusAsync(resultCreateProject.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ReadSAPWBSPromotionTextFileAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        UnitService service = new UnitService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ZRFCPS01.txt",
                            Name = "ZRFCPS01.txt"
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
                            await service.ReadSAPWBSPromotionTextFileAsync(content.ToArray());
                        }
                        tran.Rollback();
                    }
                });
            }
        }

    }
}
