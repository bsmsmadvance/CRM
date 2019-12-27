using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Services.ActivityService;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Customer.UnitTests
{
    public class ActivityServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetActivityList()
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
                        var service = new ActivityService(db);
                        var projects = await db.Projects.Take(2).ToListAsync();
                        ActivityFilter filter = new ActivityFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        ActivityListSortByParam sortByParam = new ActivityListSortByParam();
                        var results = await service.GetActivityListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<ActivityFilter>().Create();
                        filter.ProjectIDs = string.Join(',', projects.Select(o => o.ID.ToString()).ToList());
                        results = await service.GetActivityListAsync(filter, pageParam, sortByParam);

                        filter = new ActivityFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        var sortByParams = Enum.GetValues(typeof(ActivityListSortBy)).Cast<ActivityListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ActivityListSortByParam() { SortBy = item };
                            results = await service.GetActivityListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
