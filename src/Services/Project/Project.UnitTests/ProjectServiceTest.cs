using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Diagnostics;
using Database.Models.MasterKeys;

namespace Project.UnitTests
{
    public class ProjectServiceTest
    {
        IConfiguration Configuration;
        public ProjectServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetProjectListAsync()
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
                        ProjectsFilter filter = FixtureFactory.Get().Build<ProjectsFilter>().Create();
                        filter.ProductTypeKey = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProductType")
                                                                      .Select(x => x.Key).FirstAsync();
                        var statusKeys = await db.MasterCenters.Where(x => x.MasterCenterGroupKey == "ProjectStatus")
                                                                      .Select(x => x.Key).ToListAsync();
                        filter.ProjectStatusKeys = string.Join(',', statusKeys);

                        PageParam pageParam = new PageParam();
                        ProjectSortByParam sortByParam = new ProjectSortByParam();
                        var results = await service.GetProjectListAsync(filter, pageParam, sortByParam);

                        filter = new ProjectsFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ProjectSortBy)).Cast<ProjectSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ProjectSortByParam() { SortBy = item };
                            results = await service.GetProjectListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new ProjectService(Configuration, db);
                        var projectID = (await db.Projects.FirstAsync(o => o.CompanyID != null)).ID;
                        var result = await service.GetProjectAsync(projectID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateProjectAsync()
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
                        var input = new ProjectDTO();
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "2").FirstAsync();
                        input.ProjectNo = "AAA-0002/";
                        input.SapCode = "1/03333";
                        input.ProjectNameTH = "กกก";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        var result = await service.CreateProjectAsync(input);

                        var getProjectStatus = await service.GetProjectDataStatusAsync(result.Id.Value);

                        var agreement = await db.AgreementConfigs.Where(o => o.ProjectID == result.Id.Value).FirstOrDefaultAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteProjectAsync()
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
                        var input = new ProjectDTO();
                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        input.ProjectNo = "AAA-0002/";
                        input.SapCode = "1/03333";
                        input.ProjectNameTH = "กกก";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);

                        var resultCreate = await service.CreateProjectAsync(input);


                        var result = await service.DeleteProjectAsync(resultCreate.Id.Value, "ลบ");

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateProjectStatus()
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
                        var projectStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus && o.Key == ProjectStatusKeys.Active).Select(o => o.ID).FirstAsync();
                        var projectStatusInActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectStatus && o.Key == ProjectStatusKeys.InActive).FirstAsync();
                        var project = await db.Projects.Where(o => o.ProjectStatusMasterCenterID == projectStatusActive).FirstAsync();
                        var input = MasterCenterDropdownDTO.CreateFromModel(projectStatusInActive);
                        await service.UpdateProjectStatus(project.ID, input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectCountAsync()
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

                        var result = await service.GetProjectCountAsync();

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectDataStatusAsync()
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

                        var projectID = await db.Projects.Where(o => o.ProjectNo == "60013").Select(o => o.ID).FirstAsync();

                        var result = await service.GetProjectDataStatusAsync(projectID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestProjectDataStatusSale()
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
                        var serviceInfo = new ProjectInfoService(db);
                        var serviceAddress = new ProjectAddressService(db);

                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var brand = await db.Brands.Where(o => !o.IsDeleted).FirstAsync();
                        var company = await db.Companies.Where(o => !o.IsDeleted).FirstAsync();
                        var bg = await db.BGs.Where(o => o.Name == "1").FirstAsync();
                        var subbg = await db.SubBGs.Where(o => o.BGID == bg.ID).FirstAsync();

                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => o.DistrictID == district.ID).FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจคTest";
                        input.ProjectNo = "22546";

                        var resultCreate = await service.CreateProjectAsync(input);

                        var projectAddressType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectAddressType").FirstAsync();
                        var inputProjectAddress = new ProjectAddressDTO();
                        inputProjectAddress.AddressNameTH = "ไทย";
                        inputProjectAddress.AddressNameEN = "Eng";
                        inputProjectAddress.LandNo = "2222,33333";
                        inputProjectAddress.InspectionNo = "2222,33333";
                        inputProjectAddress.TitleDeedNo = "2222,33333";
                        inputProjectAddress.PostalCode = "5555";
                        inputProjectAddress.Province = ProvinceListDTO.CreateFromModel(province);
                        inputProjectAddress.District = DistrictListDTO.CreateFromModel(district);
                        inputProjectAddress.SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict);
                        inputProjectAddress.ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(projectAddressType);
                        var resultCreateAddress = await serviceAddress.CreateProjectAddressAsync(resultCreate.Id.Value, inputProjectAddress);


                        var resultInfo = await serviceInfo.GetProjectInfoAsync(resultCreate.Id.Value);
                        resultInfo.Brand = BrandDropdownDTO.CreateFromModel(brand);
                        resultInfo.Company = CompanyDropdownDTO.CreateFromModel(company);
                        resultInfo.BG = BGDropdownDTO.CreateFromModel(bg);
                        resultInfo.SubBG = SubBGDropdownDTO.CreateFromModel(subbg);
                        resultInfo.CostCenterCode = "1111";
                        resultInfo.ProfitCenterCode = "22222";
                        resultInfo.ProjectNameEN = "TestProject";

                        var testUpdateProjectInfo = await serviceInfo.UpdateProjectInfoAsync(resultInfo.Id.Value, resultInfo);
                        var result = await service.GetProjectDataStatusAsync(testUpdateProjectInfo.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void TestProjectDataStatusTransfer()
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
                        var serviceInfo = new ProjectInfoService(db);
                        var serviceAddress = new ProjectAddressService(db);

                        var productType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProductType" && o.Key == "1").FirstAsync();
                        var brand = await db.Brands.Where(o => !o.IsDeleted).FirstAsync();
                        var company = await db.Companies.Where(o => !o.IsDeleted).FirstAsync();
                        var bg = await db.BGs.Where(o => o.Name == "1").FirstAsync();
                        var subbg = await db.SubBGs.Where(o => o.BGID == bg.ID).FirstAsync();

                        var province = await db.Provinces.Where(o => !o.IsDeleted).FirstAsync();
                        var district = await db.Districts.Where(o => o.ProvinceID == province.ID).FirstAsync();
                        var subdistrict = await db.SubDistricts.Where(o => o.DistrictID == district.ID).FirstAsync();

                        var input = new ProjectDTO();
                        input.SapCode = "1234/55";
                        input.ProductType = MasterCenterDropdownDTO.CreateFromModel(productType);
                        input.ProjectNameTH = "ทดสอบโปรเจค";
                        input.ProjectNo = "22546";

                        var resultCreate = await service.CreateProjectAsync(input);

                        var projectAddressType = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ProjectAddressType" && o.Key == "3").FirstAsync();
                        var inputProjectAddress = new ProjectAddressDTO();
                        inputProjectAddress.AddressNameTH = "ไทย";
                        inputProjectAddress.AddressNameEN = "Eng";
                        inputProjectAddress.LandNo = "2222,33333";
                        inputProjectAddress.InspectionNo = "2222,33333";
                        inputProjectAddress.TitleDeedNo = "2222,33333";
                        inputProjectAddress.PostalCode = "5555";
                        inputProjectAddress.Province = ProvinceListDTO.CreateFromModel(province);
                        inputProjectAddress.District = DistrictListDTO.CreateFromModel(district);
                        inputProjectAddress.SubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict);
                        inputProjectAddress.ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(projectAddressType);
                        inputProjectAddress.HouseSubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict);
                        inputProjectAddress.TitledeedSubDistrict = SubDistrictListDTO.CreateFromModel(subdistrict);
                        var resultCreateAddress = await serviceAddress.CreateProjectAddressAsync(resultCreate.Id.Value, inputProjectAddress);


                        var resultInfo = await serviceInfo.GetProjectInfoAsync(resultCreate.Id.Value);
                        resultInfo.Brand = BrandDropdownDTO.CreateFromModel(brand);
                        resultInfo.Company = CompanyDropdownDTO.CreateFromModel(company);
                        resultInfo.BG = BGDropdownDTO.CreateFromModel(bg);
                        resultInfo.SubBG = SubBGDropdownDTO.CreateFromModel(subbg);
                        resultInfo.CostCenterCode = "1111";
                        resultInfo.ProfitCenterCode = "22222";
                        resultInfo.ProjectNameEN = "TestProject";

                        var testUpdateProjectInfo = await serviceInfo.UpdateProjectInfoAsync(resultInfo.Id.Value, resultInfo);
                        var result = await service.GetProjectDataStatusAsync(testUpdateProjectInfo.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetExportBookingTemplateUrlAsync()
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
                        var project = await db.Projects.FirstAsync();
                        var result = await service.GetExportBookingTemplateUrlAsync(project.ID);
                        Trace.WriteLine(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetProjectDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new ProjectService(Configuration, db);
                        var project = (await db.Projects.FirstAsync(o => o.CompanyID != null));
                        var result = await service.GetProjectDropdownListAsync("", project.CompanyID, true, null);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetExportAgreementTemplateUrlAsync()
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
                        var project = await db.Projects.FirstAsync();
                        var result = await service.GetExportAgreementTemplateUrlAsync(project.ID);
                        Trace.WriteLine(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetExportProjectListUrlAsync()
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
                        ProjectsFilter filter = new ProjectsFilter();
                        var result = await service.GetExportProjectListUrlAsync(filter, Report.Integration.ShowAs.PDF);
                        Trace.WriteLine(result);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
