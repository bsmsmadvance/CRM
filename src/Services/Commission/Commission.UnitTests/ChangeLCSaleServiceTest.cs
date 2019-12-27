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
using Database.Models.SAL;
using Database.Models.MasterKeys;
using Database.Models;

namespace Commission.UnitTests
{
    public class ChangeLCSaleServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        IConfiguration Configuration;
        public ChangeLCSaleServiceTest()
        {
            this.Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        [Fact]
        public async void GetChangeLCSaleListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var lc = await (from u in db.Users
                                  join ur in db.UserRoles on u.ID equals ur.UserID
                                  join r in db.Roles on ur.RoleID equals r.ID
                                  where r.Code == "LC" && u.UserAuthorizeProjects.Any()
                                  select u).FirstAsync();
                        var project = await db.UserAuthorizeProjects.Where(o => o.UserID == lc.ID).Select(o => o.Project).FirstAsync();
                        var lc2 = await db.UserAuthorizeProjects.Where(o => o.UserID != lc.ID).Select(o => o.User).FirstAsync();
                        var unit = await db.Units.FirstAsync(o => o.ProjectID == project.ID);
                        var saleType = await db.MasterCenters.Where(o => o.Key == SaleOfficerTypeKeys.AP && o.MasterCenterGroupKey == MasterCenterGroupKeys.SaleOfficerType).FirstAsync();

                        Booking booking = new Booking()
                        {
                            BookingNo = "BK000000",
                            ProjectID = project.ID,
                            UnitID = unit.ID,
                            SaleOfficerTypeMasterCenterID = await db.MasterCenters.GetIDAsync(MasterCenterGroupKeys.SaleOfficerType, SaleOfficerTypeKeys.AP),
                            SaleUserID = lc.ID,
                            ProjectSaleUserID = lc.ID
                        };
                        db.Add(booking);
                        Agreement agreement = new Agreement()
                        {
                            AgreementNo = "AG000000",
                            ProjectID = project.ID,
                            UnitID = unit.ID,
                            BookingID = booking.ID,
                            AgreementStatusMasterCenterID = await db.MasterCenters.GetIDAsync(MasterCenterGroupKeys.AgreementStatus, AgreementStatusKeys.WaitingForTransfer),
                            IsSignContractApproved = true
                        };
                        db.Add(agreement);
                        AgreementOwner agreementOwner = new AgreementOwner()
                        {
                            AgreementID = agreement.ID,
                            FirstNameTH = "Owner1",
                            LastNameTH = "LastOwner1",
                            IsMainOwner = true
                        };
                        db.Add(agreementOwner);
                        agreementOwner = new AgreementOwner()
                        {
                            AgreementID = agreement.ID,
                            FirstNameTH = "Owner2",
                            LastNameTH = "LastOwner2",
                            IsMainOwner = false
                        };
                        db.Add(agreementOwner);
                        ChangeLCSale changeLC = new ChangeLCSale()
                        {
                            AgreementID = agreement.ID,
                            OldSaleUserID = lc.ID,
                            OldProjectSaleUserID = lc.ID,
                            OldSaleOfficerTypeMasterCenterID = saleType.ID,
                            NewSaleUserID = lc.ID,
                            NewProjectSaleUserID = lc.ID,
                            NewSaleOfficerTypeMasterCenterID = saleType.ID,
                            ActiveDate = DateTime.Today.AddDays(-1),
                        };
                        db.Add(changeLC);
                        changeLC = new ChangeLCSale()
                        {
                            AgreementID = agreement.ID,
                            OldSaleUserID = lc.ID,
                            OldProjectSaleUserID = lc.ID,
                            OldSaleOfficerTypeMasterCenterID = saleType.ID,
                            NewSaleUserID = lc2.ID,
                            NewProjectSaleUserID = lc2.ID,
                            NewSaleOfficerTypeMasterCenterID = saleType.ID,
                            ActiveDate = DateTime.Today
                        };
                        db.Add(changeLC);
                        await db.SaveChangesAsync();

                        var service = new ChangeLCSaleService(db);
                        ChangeLCSaleFilter filter = FixtureFactory.Get().Build<ChangeLCSaleFilter>().Create();
                        PageParam pageParam = new PageParam();
                        ChangeLCSaleSortByParam sortByParam = new ChangeLCSaleSortByParam();
                        var results = await service.GetChangeLCSaleListAsync(filter, pageParam, sortByParam);

                        filter = new ChangeLCSaleFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ChangeLCSaleSortBy)).Cast<ChangeLCSaleSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ChangeLCSaleSortByParam() { SortBy = item };
                            results = await service.GetChangeLCSaleListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateChangeLCSaleAsync()
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
                        var agreement = await db.Agreements.FirstOrDefaultAsync(o => o.AgreementNo == "AG400171910000100");

                        var oldType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var oldPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        var newType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");
                        var newPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        //Put unit test here
                        var input = new ChangeLCSaleDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Agreement = AgreementDropdownDTO.CreateFromModel(agreement);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(oldType);
                        input.OldSaleUser = UserListDTO.CreateFromModel(oldSale);
                        input.OldProjectSaleUser = UserListDTO.CreateFromModel(oldPrjSale);

                        input.NewSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(newType);
                        input.NewSaleUser = UserListDTO.CreateFromModel(newSale);
                        input.NewProjectSaleUser = UserListDTO.CreateFromModel(newPrjSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCSaleService(db);
                        var result = await service.CreateChangeLCSaleAsync(input);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetChangeLCSaleAsync()
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
                        var agreement = await db.Agreements.FirstOrDefaultAsync(o => o.AgreementNo == "AG400171910000100");

                        var oldType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var oldPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        var newType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");
                        var newPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        //Put unit test here
                        var input = new ChangeLCSaleDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Agreement = AgreementDropdownDTO.CreateFromModel(agreement);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(oldType);
                        input.OldSaleUser = UserListDTO.CreateFromModel(oldSale);
                        input.OldProjectSaleUser = UserListDTO.CreateFromModel(oldPrjSale);

                        input.NewSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(newType);
                        input.NewSaleUser = UserListDTO.CreateFromModel(newSale);
                        input.NewProjectSaleUser = UserListDTO.CreateFromModel(newPrjSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCSaleService(db);
                        var resultCreate = await service.CreateChangeLCSaleAsync(input);

                        var result = await service.GetChangeLCSaleAsync(resultCreate.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateChangeLCSaleAsync()
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
                        var service = new ChangeLCSaleService(db);

                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        var unit = await db.Units.FirstOrDefaultAsync(o => o.UnitNo == "N19B05" && o.Project == project);
                        var agreement = await db.Agreements.FirstOrDefaultAsync(o => o.AgreementNo == "AG400171910000100");

                        var oldType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var oldPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        var newType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");
                        var newPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        var newType2 = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var newSale2 = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002357");
                        var newPrjSale2 = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002357");

                        //Put unit test here
                        var input = new ChangeLCSaleDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Agreement = AgreementDropdownDTO.CreateFromModel(agreement);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(oldType);
                        input.OldSaleUser = UserListDTO.CreateFromModel(oldSale);
                        input.OldProjectSaleUser = UserListDTO.CreateFromModel(oldPrjSale);

                        input.NewSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(newType);
                        input.NewSaleUser = UserListDTO.CreateFromModel(newSale);
                        input.NewProjectSaleUser = UserListDTO.CreateFromModel(newPrjSale);

                        input.Remark = "∑¥ Õ∫";

                        var resultCreate = await service.CreateChangeLCSaleAsync(input);

                        resultCreate.NewSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(newType2);
                        resultCreate.NewSaleUser = UserListDTO.CreateFromModel(newSale2);
                        resultCreate.NewProjectSaleUser = UserListDTO.CreateFromModel(newPrjSale2);

                        var result = await service.UpdateChangeLCSaleAsync(resultCreate.Id.Value, resultCreate);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteChangeLCSaleAsync()
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
                        var agreement = await db.Agreements.FirstOrDefaultAsync(o => o.AgreementNo == "AG400171910000100");

                        var oldType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var oldSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");
                        var oldPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP004728");

                        var newType = await db.MasterCenters.FirstOrDefaultAsync(o => o.Name == "AP");
                        var newSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");
                        var newPrjSale = await db.Users.FirstOrDefaultAsync(o => o.EmployeeNo == "AP002424");

                        //Put unit test here
                        var input = new ChangeLCSaleDTO();
                        input.Project = ProjectDropdownDTO.CreateFromModel(project);
                        input.Unit = UnitDropdownDTO.CreateFromModel(unit);
                        input.Agreement = AgreementDropdownDTO.CreateFromModel(agreement);

                        input.ActiveDate = DateTime.Now.Date;

                        input.OldSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(oldType);
                        input.OldSaleUser = UserListDTO.CreateFromModel(oldSale);
                        input.OldProjectSaleUser = UserListDTO.CreateFromModel(oldPrjSale);

                        input.NewSaleOfficerType = MasterCenterDropdownDTO.CreateFromModel(newType);
                        input.NewSaleUser = UserListDTO.CreateFromModel(newSale);
                        input.NewProjectSaleUser = UserListDTO.CreateFromModel(newPrjSale);

                        input.Remark = "∑¥ Õ∫";

                        var service = new ChangeLCSaleService(db);
                        var resultCreate = await service.CreateChangeLCSaleAsync(input);
                        await service.DeleteChangeLCSaleAsync(resultCreate.Id.Value);
                        tran.Rollback();
                    }
                });
            }
        }
    }
}
