using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Base.DTOs.MST;
using Database.Models.CMS;
using Database.Models.MST;
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
using Commission.Params.Inputs;

namespace Commission.UnitTests
{
    public class RateSettingAgentServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public RateSettingAgentServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetRateSettingAgentListAsync()
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
                        var service = new RateSettingAgentService(db);
                        RateSettingAgentFilter filter = FixtureFactory.Get().Build<RateSettingAgentFilter>().Create();
                        filter.ListProjectId = lstProjectId;
                        PageParam pageParam = new PageParam();
                        RateSettingAgentSortByParam sortByParam = new RateSettingAgentSortByParam();
                        var results = await service.GetRateSettingAgentListAsync(filter, pageParam, sortByParam);

                        filter = new RateSettingAgentFilter();
                        filter.ListProjectId = lstProjectId;
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(RateSettingAgentSortBy)).Cast<RateSettingAgentSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new RateSettingAgentSortByParam() { SortBy = item };
                            results = await service.GetRateSettingAgentListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingAgentProjectListForNewAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {

                        var service = new RateSettingAgentService(db);
                        var result = await service.GetRateSettingAgentProjectListForNewAsync();

                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingAgentProjectListForUpdateAsync()
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

                        var service = new RateSettingAgentService(db);
                        var result = await service.GetRateSettingAgentProjectListForUpdateAsync(project.ID, DateTime.Now.Date);
                        
                        Assert.NotEmpty(result);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRateSettingAgentListAsync()
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

                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");
                        var agt2 = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 2");

                        //Put unit test here
                        var ListRateSettingAgentDTO = new List<RateSettingAgentDTO>();
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingAgentDTO.Add(input);

                        input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt2);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingAgentDTO.Add(input);

                        var inputModel = new RateSettingAgentInput();
                        inputModel.ListProject = lstProject;
                        inputModel.ListRateSettingAgent = ListRateSettingAgentDTO;

                        var service = new RateSettingAgentService(db);
                        await service.CreateRateSettingAgentListAsync(inputModel);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingAgentListAsync()
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
                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");
                        var agt2 = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 2");

                        //Put unit test here
                        var ListRateSettingAgentDTO = new List<RateSettingAgentDTO>();
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingAgentDTO.Add(input);

                        input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt2);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;
                        ListRateSettingAgentDTO.Add(input);

                        var service = new RateSettingAgentService(db);
                        await service.UpdateRateSettingAgentListAsync(ListRateSettingAgentDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        /*
        [Fact]
        public async void CreateRateSettingAgentAsync()
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
                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");

                        //Put unit test here
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingAgentService(db);
                        var result = await service.CreateRateSettingAgentAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRateSettingAgentAsync()
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
                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");

                        //Put unit test here
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingAgentService(db);
                        var resultCreate = await service.CreateRateSettingAgentAsync(input);

                        var result = await service.GetRateSettingAgentAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRateSettingAgentAsync()
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
                        var service = new RateSettingAgentService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "60015");
                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");

                        //Put unit test here
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var resultCreate = await service.CreateRateSettingAgentAsync(input);
                        resultCreate.Amount = 999999;

                        var result = await service.UpdateRateSettingAgentAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRateSettingAgentAsync()
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
                        var agt = await db.Agents.FirstOrDefaultAsync(o => o.NameTH == "Agent 1");

                        //Put unit test here
                        var input = new RateSettingAgentDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Agent = AgentDTO.CreateFromModel(agt);
                        input.ActiveDate = DateTime.Now.Date;
                        input.Amount = 100000;
                        input.IsActive = true;

                        var service = new RateSettingAgentService(db);
                        var resultCreate = await service.CreateRateSettingAgentAsync(input);
                        await service.DeleteRateSettingAgentAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
        */
    }
}
