using System;
using System.Linq;
using System.Net.Http;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.USR;
using Database.UnitTestExtensions;
using Identity.Params.Filters;
using Identity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Xunit;

namespace Identity.UnitTests
{
    public class UserServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        private IConfiguration Configuration;
        public UserServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetUserListAsync()
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
                            var service = new UserService(db);
                            UserFilter filter = FixtureFactory.Get().Build<UserFilter>().Create();
                            PageParam pageParam = new PageParam();
                            UserListSortByParam sortByParam = new UserListSortByParam();
                            var results = await service.GetUserListAsync(filter, pageParam, sortByParam);

                            var sortByParams = Enum.GetValues(typeof(UserListSortBy)).Cast<UserListSortBy>();
                            foreach (var item in sortByParams)
                            {
                                sortByParam = new UserListSortByParam() { SortBy = item };
                                results = await service.GetUserListAsync(filter, pageParam, sortByParam);
                            }

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
        public async void GetUserDropdownListAsync()
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
                            var service = new UserService(db);
                            var results = await service.GetUserDropdownListAsync("ก");

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
    }
}
