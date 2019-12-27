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
    public class RateSettingSaleServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingSaleServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingSaleListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        //Put unit test here
                        var service = new RateSettingSaleService(Configuration, db);
                        RateSettingSaleFilter filter = FixtureFactory.Get().Build<RateSettingSaleFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingSaleSortByParam sortByParam = new RateSettingSaleSortByParam();
                        var results = await service.GetRateSettingSaleListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingSaleFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingSaleSortBy)).Cast<RateSettingSaleSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingSaleSortByParam() { SortBy = item };
                            results = await service.GetRateSettingSaleListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingSaleProjectListForNewAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingSaleService(Configuration, db);
                        var result = await service.GetRateSettingSaleProjectListForNewAsync("1");

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingSaleProjectListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

                        var service = new RateSettingSaleService(Configuration, db);
                        var result = await service.GetRateSettingSaleProjectListForUpdateAsync(project.ID, DateTime.Now.Date);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingSaleListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");
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

                        var inputModel = new RateSettingSaleInput();
                        inputModel.ListProject = lstProject;
                        inputModel.ListRateSettingSale = ListRateSettingSaleTransferDTO;

                        var service = new RateSettingSaleService(Configuration, db);
                        await service.CreateRateSettingSaleListAsync(inputModel);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingSaleListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

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

                        var service = new RateSettingSaleService(Configuration, db);
                        await service.UpdateRateSettingSaleListAsync(ListRateSettingSaleTransferDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        /*
        [Fact]
        public async void CreateRateSettingSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingSaleService(Configuration, db);
                        var result = await service.CreateRateSettingSaleAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingSaleService(Configuration, db);
                        var resultCreate = await service.CreateRateSettingSaleAsync(input);

                        var result = await service.GetRateSettingSaleAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingSaleAsync()
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
                        var service = new RateSettingSaleService(Configuration, db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var resultCreate = await service.CreateRateSettingSaleAsync(input);
                        resultCreate.Amount = 0.1;

                        var result = await service.UpdateRateSettingSaleAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40045");

                        //Put unit test here
                        var input = new RateSettingSaleTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.ActiveDate = DateTime.Now.Date;
                        input.StartRange = 1;
                        input.EndRange = 1999999;
                        input.Amount = 0.05;
                        input.IsActive = true;

                        var service = new RateSettingSaleService(Configuration, db);
                        var resultCreate = await service.CreateRateSettingSaleAsync(input);
                        await service.DeleteRateSettingSaleAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
        */

        [Fact]
        public async void ImportRateSettingSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingSaleService(Configuration, db);
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/BG_RankingSale.xlsx",
                            Name = "BG_RankingSale.xlsx"
                        };
                        var bg = await db.BGs.Where(o => o.BGNo == "1").FirstAsync();
                        var result = await service.ImportRateSettingSaleAsync(bg.ID, fileInput);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void ExportRateSettingSaleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var service = new RateSettingSaleService(Configuration, db);
                        var bg = await db.BGs.Where(o => o.BGNo == "1").FirstAsync();
                        var project = await db.Projects.Where(o => o.ProjectNo == "10060").FirstAsync();
                        var lstProjectId = new List<Guid>();
                        lstProjectId.Add(project.ID);

                        var filter = new RateSettingSaleFilter
                        {
                            ListProjectId = lstProjectId
                        };

                        RateSettingSaleSortByParam sortByParam = new RateSettingSaleSortByParam();
                        var result = await service.ExportExcelRateSettingSaleAsync(bg.ID, filter, sortByParam);

                        tran.Rollback();
                    }
                });
            }
        }
    }
}
