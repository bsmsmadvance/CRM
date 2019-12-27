using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models.CMS;
using Database.UnitTestExtensions;
using Commission.Params.Filters;
using Commission.Params.Inputs;
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
    public class RateSettingTransferServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingTransferServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingTransferListAsync()
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
                        var service = new RateSettingTransferService(Configuration, db);
                        RateSettingTransferFilter filter = FixtureFactory.Get().Build<RateSettingTransferFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingTransferSortByParam sortByParam = new RateSettingTransferSortByParam();
                        var results = await service.GetRateSettingTransferListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingTransferFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingTransferSortBy)).Cast<RateSettingTransferSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingTransferSortByParam() { SortBy = item };
                            results = await service.GetRateSettingTransferListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingTransferProjectListForNewAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingTransferService(Configuration, db);
                        var result = await service.GetRateSettingTransferProjectListForNewAsync("1");

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingTransferProjectListAsync()
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

                        var service = new RateSettingTransferService(Configuration, db);
                        var result = await service.GetRateSettingTransferProjectListForUpdateAsync(project.ID, DateTime.Now.Date);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingTransferListAsync()
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
                        var lstProject = new List<ProjectInput>();
                        lstProject.Add(ProjectInput.CreateFromModel(project));

                        //Put unit test here
                        var ListRateSettingSaleTransferDTO = new List<RateSettingSaleTransferDTO>();
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;
                        ListRateSettingSaleTransferDTO.Add(input);

                        input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 2000000;
                        input.EndRange = 2999999;
                        input.Amount = 0.1;
                        input.IsActive = true;
                        ListRateSettingSaleTransferDTO.Add(input);

                        var inputModel = new RateSettingTransferInput();
                        inputModel.ListProject = lstProject;
                        inputModel.ListRateSettingTransfer = ListRateSettingSaleTransferDTO;

                        var service = new RateSettingTransferService(Configuration, db);
                        await service.CreateRateSettingTransferListAsync(inputModel);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingTransferListAsync()
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

                        //Put unit test here
                        var ListRateSettingSaleTransferDTO = new List<RateSettingSaleTransferDTO>();
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;
                        input.Id = Guid.NewGuid();
                        ListRateSettingSaleTransferDTO.Add(input);

                        input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 2000000;
                        input.EndRange = 2999999;
                        input.Amount = 0.1;
                        input.IsActive = true;
                        input.Id = Guid.NewGuid();
                        ListRateSettingSaleTransferDTO.Add(input);

                        var service = new RateSettingTransferService(Configuration, db);
                        await service.UpdateRateSettingTransferListAsync(ListRateSettingSaleTransferDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        /*
        [Fact]
        public async void CreateRateSettingTransferAsync()
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

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingTransferService(Configuration, db);
                        var result = await service.CreateRateSettingTransferAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingTransferAsync()
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

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingTransferService(Configuration, db);
                        var resultCreate = await service.CreateRateSettingTransferAsync(input);

                        var result = await service.GetRateSettingTransferAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingTransferAsync()
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
                        var service = new RateSettingTransferService(Configuration, db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var resultCreate = await service.CreateRateSettingTransferAsync(input);
                        resultCreate.Amount = 0.1;

                        var result = await service.UpdateRateSettingTransferAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingTransferAsync()
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

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingTransferService(Configuration, db);
                        var resultCreate = await service.CreateRateSettingTransferAsync(input);
                        await service.DeleteRateSettingTransferAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
        */

        [Fact]
        public async void ImportRateSettingTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingTransferService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/BG_RankingTransfer.xlsx",
                            Name = "BG_RankingTransfer.xlsx"
                        };
                        var bg = await db.BGs.Where(o => o.BGNo == "3").FirstAsync();
                        var result = await service.ImportRateSettingTransferAsync(bg.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportRateSettingTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingTransferService(Configuration, db);
                        var bg = await db.BGs.Where(o => o.BGNo == "3").FirstAsync();
                        var project = await db.Projects.Where(o => o.ProjectNo == "10099").FirstAsync();
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        var filter = new RateSettingTransferFilter
                        {
                            ListProjectId = lstProjectId
                        };

                        RateSettingTransferSortByParam sortByParam = new RateSettingTransferSortByParam();
                        var result = await service.ExportExcelRateSettingTransferAsync(bg.ID, filter, sortByParam);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
