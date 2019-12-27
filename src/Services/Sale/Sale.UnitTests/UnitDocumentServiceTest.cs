using AutoFixture;
using Base.DTOs.SAL;
using CustomAutoFixture;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sale.UnitTests
{
    public class UnitDocumentServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        IConfiguration Configuration;
        public UnitDocumentServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetUnitDocumentListAsync()
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
                        var service = new UnitDocumentService(db);
                        UnitDocumentFilter filter = new UnitDocumentFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        UnitDocumentSortByParam sortByParam = new UnitDocumentSortByParam();

                        var results = await service.GetUnitDocumentDropdownListAsync(filter, pageParam, sortByParam);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
