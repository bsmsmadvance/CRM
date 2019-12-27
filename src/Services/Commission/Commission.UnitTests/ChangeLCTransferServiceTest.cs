using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Database.Models.CMS;
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
    public class ChangeLCTransferServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public ChangeLCTransferServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetChangeLCTransferListAsync()
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

                        var service = new ChangeLCTransferService(db);
                        ChangeLCTransferFilter filter = FixtureFactory.Get().Build<ChangeLCTransferFilter>().Create();
                        PageParam pageParam = new PageParam();
                        ChangeLCTransferSortByParam sortByParam = new ChangeLCTransferSortByParam();
                        var results = await service.GetChangeLCTransferListAsync(filter, pageParam, sortByParam);

                        filter = new ChangeLCTransferFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ChangeLCTransferSortBy)).Cast<ChangeLCTransferSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ChangeLCTransferSortByParam() { SortBy = item };
                            results = await service.GetChangeLCTransferListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateChangeLCTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var unit = await db.Units.FirstOrDefaultAsync(o => o.UnitNo == "N19B05" && o.Project == project);
                        var transfer = await db.Transfers.FirstOrDefaultAsync(o => o.TransferNo == "TF400171910000100");

                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        //Put unit test here
                        var input = new ChangeLCTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Transfer = TransferDropdownDTO.CreateFromModel(transfer);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldLCTransfer = UserListDTO.CreateFromModel(oldSale);
                        input.NewLCTransfer = UserListDTO.CreateFromModel(newSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCTransferService(db);
                        var result = await service.CreateChangeLCTransferAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetChangeLCTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var unit = await db.Units.FirstOrDefaultAsync(o => o.UnitNo == "N19B05" && o.Project == project);
                        var transfer = await db.Transfers.FirstOrDefaultAsync(o => o.TransferNo == "TF400171910000100");

                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        //Put unit test here
                        var input = new ChangeLCTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Transfer = TransferDropdownDTO.CreateFromModel(transfer);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldLCTransfer = UserListDTO.CreateFromModel(oldSale);
                        input.NewLCTransfer = UserListDTO.CreateFromModel(newSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCTransferService(db);
                        var resultCreate = await service.CreateChangeLCTransferAsync(input);

                        var result = await service.GetChangeLCTransferAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateChangeLCTransferAsync()
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
                        var service = new ChangeLCTransferService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var unit = await db.Units.FirstOrDefaultAsync(o => o.UnitNo == "N19B05" && o.Project == project);
                        var transfer = await db.Transfers.FirstOrDefaultAsync(o => o.TransferNo == "TF400171910000100");

                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var newSale2 = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        //Put unit test here
                        var input = new ChangeLCTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Transfer = TransferDropdownDTO.CreateFromModel(transfer);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldLCTransfer = UserListDTO.CreateFromModel(oldSale);
                        input.NewLCTransfer = UserListDTO.CreateFromModel(newSale);

                        input.Remark = "∑¥ Õ∫";

                        var resultCreate = await service.CreateChangeLCTransferAsync(input);

                        resultCreate.NewLCTransfer = UserListDTO.CreateFromModel(newSale2);

                        var result = await service.UpdateChangeLCTransferAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteChangeLCTransferAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var unit = await db.Units.FirstOrDefaultAsync(o => o.UnitNo == "N19B05" && o.Project == project);
                        var transfer = await db.Transfers.FirstOrDefaultAsync(o => o.TransferNo == "TF400171910000100");

                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        //Put unit test here
                        var input = new ChangeLCTransferDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Transfer = TransferDropdownDTO.CreateFromModel(transfer);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldLCTransfer = UserListDTO.CreateFromModel(oldSale);
                        input.NewLCTransfer = UserListDTO.CreateFromModel(newSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCTransferService(db);
                        var resultCreate = await service.CreateChangeLCTransferAsync(input);
                        await service.DeleteChangeLCTransferAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
