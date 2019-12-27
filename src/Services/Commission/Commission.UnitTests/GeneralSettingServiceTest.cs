using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models.CMS;
using Database.Models.PRJ;
using Database.UnitTestExtensions;
using Commission.Params.Filters;
using Commission.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace Commission.UnitTests
{
    public class GeneralSettingServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public GeneralSettingServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetGeneralSettingListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var service = new GeneralSettingService(db);
                        GeneralSettingFilter filter = FixtureFactory.Get().Build<GeneralSettingFilter>().Create();
                        filter.ListProjectId = lstProjectId;

                        PageParam pageParam = new PageParam();
                        GeneralSettingSortByParam sortByParam = new GeneralSettingSortByParam();
                        var results = await service.GetGeneralSettingListAsync(filter, pageParam, sortByParam);

                        filter = new GeneralSettingFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(GeneralSettingSortBy)).Cast<GeneralSettingSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new GeneralSettingSortByParam() { SortBy = item };
                            results = await service.GetGeneralSettingListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateGeneralSettingAsync()
        {            
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var input = new GeneralSettingDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.AfterLaunchAmount = 1000;
                        input.LaunchStartDate = DateTime.Now.Date.AddDays(7);
                        input.LaunchEndDate = DateTime.Now.Date.AddDays(7).AddMonths(3);
                        input.CreatedByUser = UserListDTO.CreateFromModel(sale);
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new GeneralSettingService(db);
                        await service.CreateGeneralSettingAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetGeneralSettingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var input = new GeneralSettingDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.AfterLaunchAmount = 1000;
                        input.LaunchStartDate = DateTime.Now.Date.AddDays(7);
                        input.LaunchEndDate = DateTime.Now.Date.AddDays(7).AddMonths(3);
                        input.CreatedByUser = UserListDTO.CreateFromModel(sale);
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new GeneralSettingService(db);
                        await service.CreateGeneralSettingAsync(input);

                        var resultCreate = await db.GeneralSettings.FirstOrDefaultAsync();
                        var result = await service.GetGeneralSettingAsync(resultCreate.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateGeneralSettingAsync()
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
                        var service = new GeneralSettingService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var input = new GeneralSettingDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.AfterLaunchAmount = 1000;
                        input.LaunchStartDate = DateTime.Now.Date.AddDays(7);
                        input.LaunchEndDate = DateTime.Now.Date.AddDays(7).AddMonths(3);
                        input.CreatedByUser = UserListDTO.CreateFromModel(sale);
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        await service.CreateGeneralSettingAsync(input);

                        var resultCreate = await db.GeneralSettings.FirstOrDefaultAsync();
                        resultCreate.AfterLaunchAmount = 1500;
                        resultCreate.LaunchStartDate = DateTime.Now.Date.AddDays(14);
                        resultCreate.LaunchEndDate = DateTime.Now.Date.AddDays(14).AddMonths(3);

                        var resultCreateDTO = GeneralSettingDTO.CreateFromModel(resultCreate);

                        var result = await service.UpdateGeneralSettingAsync(resultCreate.ID, resultCreateDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteGeneralSettingAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var sale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002406");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var input = new GeneralSettingDTO();
                        //input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.AfterLaunchAmount = 1000;
                        input.LaunchStartDate = DateTime.Now.Date.AddDays(7);
                        input.LaunchEndDate = DateTime.Now.Date.AddDays(7).AddMonths(3);
                        input.CreatedByUser = UserListDTO.CreateFromModel(sale);
                        input.IsActive = true;
                        input.ListProjectId = lstProjectId;

                        var service = new GeneralSettingService(db);
                        await service.CreateGeneralSettingAsync(input);

                        var resultCreate = await db.GeneralSettings.FirstOrDefaultAsync();
                        await service.DeleteGeneralSettingAsync(resultCreate.ID);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
