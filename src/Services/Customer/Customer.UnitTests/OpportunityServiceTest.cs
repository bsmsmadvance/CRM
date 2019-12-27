using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Database.Models.CTM;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Xunit;
using Customer.Services.OpportunityService;
using ErrorHandling;
using Database.Models.MasterKeys;

namespace Customer.UnitTests
{
    public class OpportunityServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetOpportunityListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync(),
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First()
                        };
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        var service = new OpportunityService(db);
                        OpportunityFilter filter = new OpportunityFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        OpportunityListSortByParam sortByParam = new OpportunityListSortByParam();
                        var results = await service.GetOpportunityListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<OpportunityFilter>().Create();
                        filter.ExcludeIDs = string.Join(',', results.Opportunities.Select(o => o.Id).ToList());
                        results = await service.GetOpportunityListAsync(filter, pageParam, sortByParam);

                        var sortByParams = Enum.GetValues(typeof(OpportunityListSortBy)).Cast<OpportunityListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new OpportunityListSortByParam() { SortBy = item };
                            results = await service.GetOpportunityListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetOpportunityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, db.Contacts.Select(o => o.ID).First())
                            .With(o => o.ProjectID, db.Projects.Select(o => o.ID).First())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        //var opportunityModel = await db.Opportunities.Where(o => o.ID == new Guid("BF08C366-69D4-4D02-A6D9-000012037F37")).FirstAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetOpportunityAsync(opportunity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateOpportunityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        OpportunityDTO opportunityDTO = new OpportunityDTO()
                        {
                            InterestedProduct1 = "Test1",
                            InterestedProduct2 = "Test2",
                            InterestedProduct3 = "Test3",
                            ArriveDate = DateTime.Now
                        };
                        opportunityDTO.Contact = new ContactListDTO();
                        opportunityDTO.Contact.Id = contact.ID;
                        opportunityDTO.Project = new Base.DTOs.PRJ.ProjectDTO();
                        opportunityDTO.Project.Id = await db.Projects.Where(o => o.ProjectNo == "40037").Select(o => o.ID).FirstAsync();
                        opportunityDTO.SalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityDTO.SalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).FirstAsync();
                        opportunityDTO.EstimateSalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityDTO.EstimateSalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).FirstAsync();

                        var user = await db.Users.Where(o => o.EmployeeNo == "CR000749").FirstOrDefaultAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.CreateOpportunityAsync(opportunityDTO, null);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateOpportunityAsync()
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
                            var contact = await db.Contacts.FirstAsync();
                            OpportunityDTO opportunityDTO = new OpportunityDTO()
                            {
                                InterestedProduct1 = "Test1",
                                InterestedProduct2 = "Test2",
                                InterestedProduct3 = "Test3",
                                ArriveDate = DateTime.Now
                            };
                            opportunityDTO.Contact = new ContactListDTO();
                            opportunityDTO.Contact.Id = contact.ID;
                            opportunityDTO.Project = new Base.DTOs.PRJ.ProjectDTO();
                            opportunityDTO.Project.Id = await db.Projects.Select(o => o.ID).FirstAsync();
                            opportunityDTO.SalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            opportunityDTO.SalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).FirstAsync();
                            opportunityDTO.EstimateSalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            opportunityDTO.EstimateSalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).FirstAsync();

                            // Act
                            var service = new OpportunityService(db);
                            var results = await service.CreateOpportunityAsync(opportunityDTO, null);

                            opportunityDTO.ArriveDate = DateTime.Now;
                            opportunityDTO.InterestedProduct3 = null;
                            var updateResults = await service.UpdateOpportunityAsync(results.Id.Value, opportunityDTO);

                            tran.Rollback();
                        }
                        catch (ValidateException ex)
                        {
                            tran.Rollback();
                        }
                    }
                });
            }
        }

        [Fact]
        public async void DeleteOpportunityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        OpportunityDTO opportunityDTO = new OpportunityDTO()
                        {
                            InterestedProduct1 = "Test1",
                            InterestedProduct2 = "Test2",
                            InterestedProduct3 = "Test3",
                            ArriveDate = DateTime.Now
                        };
                        opportunityDTO.Contact = new ContactListDTO();
                        opportunityDTO.Contact.Id = contact.ID;
                        opportunityDTO.Project = new Base.DTOs.PRJ.ProjectDTO();
                        opportunityDTO.Project.Id = await db.Projects.Select(o => o.ID).FirstAsync();
                        opportunityDTO.SalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityDTO.SalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).FirstAsync();
                        opportunityDTO.EstimateSalesOpportunity = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityDTO.EstimateSalesOpportunity.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).FirstAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.CreateOpportunityAsync(opportunityDTO, null);

                        await service.DeleteOpportunityAsync(results.Id.Value);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetOpportunityActivityListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "OpportunityActivityType").Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity.OpportunityActivityType = null;
                        opportunityActivity.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivityResult opportunityActivityResultFirst = new OpportunityActivityResult();
                        OpportunityActivityResult opportunityActivityResultSecond = new OpportunityActivityResult();

                        opportunityActivityResultFirst.OpportunityAcitivityID = opportunityActivity.ID;
                        opportunityActivityResultFirst.StatusID = await db.OpportunityActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        opportunityActivityResultSecond.OpportunityAcitivityID = opportunityActivity.ID;
                        opportunityActivityResultSecond.StatusID = await db.OpportunityActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();

                        await db.OpportunityActivityResults.AddAsync(opportunityActivityResultFirst);
                        await db.OpportunityActivityResults.AddAsync(opportunityActivityResultSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetOpportunityActivityListAsync(opportunity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetOpportunityActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "1").Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity.OpportunityActivityType = null;
                        opportunityActivity.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity2 = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "3").Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity2.OpportunityActivityType = null;
                        opportunityActivity2.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity2);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity3 = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "4").Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity3.OpportunityActivityType = null;
                        opportunityActivity3.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity3);
                        await db.SaveChangesAsync();

                        OpportunityActivityResult opportunityActivityResultFirst = new OpportunityActivityResult();
                        OpportunityActivityResult opportunityActivityResultSecond = new OpportunityActivityResult();

                        opportunityActivityResultFirst.OpportunityAcitivityID = opportunityActivity.ID;
                        opportunityActivityResultFirst.StatusID = await db.OpportunityActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        opportunityActivityResultSecond.OpportunityAcitivityID = opportunityActivity.ID;
                        opportunityActivityResultSecond.StatusID = await db.OpportunityActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();

                        await db.OpportunityActivityResults.AddAsync(opportunityActivityResultFirst);
                        await db.OpportunityActivityResults.AddAsync(opportunityActivityResultSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetOpportunityActivityAsync(opportunityActivity2.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateOpportunityActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = new OpportunityActivity()
                        {
                            OpportunityID = opportunity.ID,
                            OpportunityActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "3").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First(),
                            IsCompleted = false,
                            DueDate = DateTime.Now,
                            IsDeleted = false
                        };

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivityDTO opportunityActivityDTO = new OpportunityActivityDTO()
                        {
                            OpportunityID = opportunity.ID,
                            DueDate = new DateTime(2019, 08, 16),
                            ActualDate = DateTime.Today,
                            AppointmentDate = DateTime.Today
                        };
                        opportunityActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "1").Select(o => o.ID).First();
                        opportunityActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime && o.Key == "1").Select(o => o.ID).First();

                        OpportunityActivityStatusDTO opportunityActivityStatusDTO = new OpportunityActivityStatusDTO();
                        opportunityActivityStatusDTO.Id = await db.OpportunityActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        opportunityActivityStatusDTO.IsSelected = false;

                        opportunityActivityDTO.ActivityStatuses = new List<OpportunityActivityStatusDTO>();
                        opportunityActivityDTO.ActivityStatuses.Add(opportunityActivityStatusDTO);

                        var service = new OpportunityService(db);
                        var results = await service.CreateOpportunityActivityAsync(opportunity.ID, opportunityActivityDTO);

                        var activityTaskResult = await db.ActivityTasks.Where(o => o.OpportunityActivityID == results.Id).FirstOrDefaultAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateOpportunityActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = new OpportunityActivity()
                        {
                            OpportunityID = opportunity.ID,
                            OpportunityActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "OpportunityActivityType" && o.Key == "1").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            IsCompleted = false,
                            IsDeleted = false,
                            DueDate = DateTime.Parse("2019-09-01"),
                            AppointmentDate = DateTime.Now
                        };

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivityDTO opportunityActivityDTO = new OpportunityActivityDTO()
                        {
                            OpportunityID = opportunity.ID,
                            DueDate = DateTime.Now,
                            ActualDate = DateTime.Now,
                            AppointmentDate = DateTime.Now
                        };
                        opportunityActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "OpportunityActivityType" && o.Key == "2").Select(o => o.ID).First();
                        opportunityActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime" && o.Key == "2").Select(o => o.ID).First();

                        OpportunityActivityStatusDTO opportunityActivityStatusDTO = new OpportunityActivityStatusDTO();
                        opportunityActivityStatusDTO.Id = await db.OpportunityActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        opportunityActivityStatusDTO.IsSelected = true;

                        opportunityActivityDTO.ActivityStatuses = new List<OpportunityActivityStatusDTO>();
                        opportunityActivityDTO.ActivityStatuses.Add(opportunityActivityStatusDTO);

                        var service = new OpportunityService(db);
                        var results = await service.UpdateOpportunityActivityAsync(opportunityActivity.ID, opportunityActivityDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteOpportunityActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = new OpportunityActivity()
                        {
                            OpportunityID = opportunity.ID,
                            OpportunityActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "OpportunityActivityType" && o.Key == "1").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            DueDate = DateTime.Now,
                            IsCompleted = true,
                            IsDeleted = false
                        };

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivityDTO opportunityActivityDTO = new OpportunityActivityDTO()
                        {
                            OpportunityID = opportunity.ID,
                            DueDate = DateTime.Now,
                            ActualDate = DateTime.Now,
                            AppointmentDate = DateTime.Now
                        };
                        opportunityActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "OpportunityActivityType" && o.Key == "2").Select(o => o.ID).First();
                        opportunityActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        opportunityActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime" && o.Key == "2").Select(o => o.ID).First();

                        OpportunityActivityStatusDTO opportunityActivityStatusDTO = new OpportunityActivityStatusDTO();
                        opportunityActivityStatusDTO.Id = await db.OpportunityActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        opportunityActivityStatusDTO.IsSelected = true;

                        opportunityActivityDTO.ActivityStatuses = new List<OpportunityActivityStatusDTO>();
                        opportunityActivityDTO.ActivityStatuses.Add(opportunityActivityStatusDTO);

                        // Act
                        var service = new OpportunityService(db);
                        await service.DeleteOpportunityActivityAsync(opportunityActivity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRevisitActivityListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity = FixtureFactory.Get().Build<RevisitActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.RevisitActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.RevisitActivityType && o.Order == 1).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        revisitActivity.RevisitActivityType = null;
                        revisitActivity.ConvenientTime = null;

                        await db.RevisitActivities.AddAsync(revisitActivity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity2 = FixtureFactory.Get().Build<RevisitActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.RevisitActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.RevisitActivityType && o.Order == 2).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        revisitActivity2.RevisitActivityType = null;
                        revisitActivity2.ConvenientTime = null;

                        await db.RevisitActivities.AddAsync(revisitActivity2);
                        await db.SaveChangesAsync();

                        RevisitActivityResult revisitActivityResultFirst = new RevisitActivityResult();
                        RevisitActivityResult revisitActivityResultSecond = new RevisitActivityResult();

                        revisitActivityResultFirst.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultFirst.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        revisitActivityResultSecond.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultSecond.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();

                        await db.RevisitActivityResults.AddAsync(revisitActivityResultFirst);
                        await db.RevisitActivityResults.AddAsync(revisitActivityResultSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetRevisitActivityListAsync(opportunity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRevisitActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity = FixtureFactory.Get().Build<RevisitActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.RevisitActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType").Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        revisitActivity.RevisitActivityType = null;
                        revisitActivity.ConvenientTime = null;

                        await db.RevisitActivities.AddAsync(revisitActivity);
                        await db.SaveChangesAsync();

                        RevisitActivityResult revisitActivityResultFirst = new RevisitActivityResult();
                        RevisitActivityResult revisitActivityResultSecond = new RevisitActivityResult();

                        revisitActivityResultFirst.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultFirst.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        revisitActivityResultSecond.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultSecond.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();

                        await db.RevisitActivityResults.AddAsync(revisitActivityResultFirst);
                        await db.RevisitActivityResults.AddAsync(revisitActivityResultSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetRevisitActivityAsync(revisitActivity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateRevisitActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivityDTO revisitActivityDTO = new RevisitActivityDTO()
                        {
                            OpportunityID = opportunity.ID,
                            ActualDate = DateTime.Now,
                            AppointmentDate = DateTime.Now
                        };
                        revisitActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        revisitActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.RevisitActivityType && o.Key == "1").Select(o => o.ID).First();
                        revisitActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        revisitActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime && o.Key == "1").Select(o => o.ID).First();

                        RevisitActivityStatusDTO revisitActivityStatusDTO = new RevisitActivityStatusDTO();
                        revisitActivityStatusDTO.Id = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        revisitActivityStatusDTO.IsSelected = true;

                        revisitActivityDTO.ActivityStatuses = new List<RevisitActivityStatusDTO>();
                        revisitActivityDTO.ActivityStatuses.Add(revisitActivityStatusDTO);

                        var service = new OpportunityService(db);
                        var results = await service.CreateRevisitActivityAsync(opportunity.ID, revisitActivityDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateRevisitActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity = new RevisitActivity()
                        {
                            OpportunityID = opportunity.ID,
                            RevisitActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            IsCompleted = true,
                            IsDeleted = false,
                            ActualDate = DateTime.Parse("2019-09-01")
                        };

                        await db.RevisitActivities.AddAsync(revisitActivity);
                        await db.SaveChangesAsync();

                        RevisitActivityResult revisitActivityResult = new RevisitActivityResult()
                        {
                            RevisitAcitivityID = revisitActivity.ID,
                            StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync(),
                            OtherReasons = "dsdsd"
                        };

                        RevisitActivityResult revisitActivityResult2 = new RevisitActivityResult()
                        {
                            RevisitAcitivityID = revisitActivity.ID,
                            StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 3).Select(o => o.ID).FirstAsync(),
                            OtherReasons = "dsdsd"
                        };

                        await db.RevisitActivityResults.AddAsync(revisitActivityResult);
                        await db.RevisitActivityResults.AddAsync(revisitActivityResult2);
                        await db.SaveChangesAsync();

                        RevisitActivityDTO revisitActivityDTO = new RevisitActivityDTO()
                        {
                            OpportunityID = opportunity.ID,
                            ActualDate = DateTime.Now,
                            AppointmentDate = DateTime.Now
                        };
                        revisitActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        revisitActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType" && o.Key == "1").Select(o => o.ID).First();
                        revisitActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        revisitActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime" && o.Key == "1").Select(o => o.ID).First();

                        RevisitActivityStatusDTO revisitActivityStatusDTO = new RevisitActivityStatusDTO();
                        revisitActivityStatusDTO.Id = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        revisitActivityStatusDTO.IsSelected = true;

                        RevisitActivityStatusDTO revisitActivityStatusDTO2 = new RevisitActivityStatusDTO();
                        revisitActivityStatusDTO2.Id = await db.RevisitActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();
                        revisitActivityStatusDTO2.IsSelected = true;

                        revisitActivityDTO.ActivityStatuses = new List<RevisitActivityStatusDTO>();
                        revisitActivityDTO.ActivityStatuses.Add(revisitActivityStatusDTO);
                        revisitActivityDTO.ActivityStatuses.Add(revisitActivityStatusDTO2);

                        var service = new OpportunityService(db);
                        var results = await service.UpdateRevisitActivityAsync(revisitActivity.ID, revisitActivityDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteRevisitActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync();
                        Opportunity opportunity = new Opportunity()
                        {
                            ContactID = contact.ID,
                            ProjectID = await db.Projects.Select(o => o.ID).FirstOrDefaultAsync(),
                            EstimateSalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First(),
                            StatusQuestionaireMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First(),
                            SalesOpportunityMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First(),
                            IsDeleted = false
                        };

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity = new RevisitActivity()
                        {
                            OpportunityID = opportunity.ID,
                            RevisitActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            IsCompleted = true,
                            IsDeleted = false,
                            ActualDate = DateTime.Now
                        };

                        await db.RevisitActivities.AddAsync(revisitActivity);
                        await db.SaveChangesAsync();

                        RevisitActivityResult revisitActivityResultFirst = new RevisitActivityResult();
                        RevisitActivityResult revisitActivityResultSecond = new RevisitActivityResult();

                        revisitActivityResultFirst.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultFirst.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        revisitActivityResultSecond.RevisitAcitivityID = revisitActivity.ID;
                        revisitActivityResultSecond.StatusID = await db.RevisitActivityStatuses.Where(o => o.Order == 2).Select(o => o.ID).FirstAsync();

                        await db.RevisitActivityResults.AddAsync(revisitActivityResultFirst);
                        await db.RevisitActivityResults.AddAsync(revisitActivityResultSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        await service.DeleteRevisitActivityAsync(revisitActivity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetOpportunityActivityDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.EstimateSalesOpportunity).Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire).Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SalesOpportunity).Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Order == 1).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity.OpportunityActivityType = null;
                        opportunityActivity.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity2 = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Order == 2).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity2.ActualDate = null;
                        opportunityActivity2.OpportunityActivityType = null;
                        opportunityActivity2.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity2);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity3 = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Order == 3).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity3.ActualDate = null;
                        opportunityActivity3.OpportunityActivityType = null;
                        opportunityActivity3.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity3);
                        await db.SaveChangesAsync();

                        OpportunityActivity opportunityActivity4 = FixtureFactory.Get().Build<OpportunityActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.OpportunityActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Order == 7).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        opportunityActivity4.ActualDate = null;
                        opportunityActivity4.OpportunityActivityType = null;
                        opportunityActivity4.ConvenientTime = null;

                        await db.OpportunityActivities.AddAsync(opportunityActivity4);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetOpportunityActivityDraftAsync(opportunity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetRevisitActivityDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, await db.Contacts.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.ProjectID, await db.Projects.Select(o => o.ID).FirstOrDefaultAsync())
                            .With(o => o.EstimateSalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "EstimateSalesOpportunity").Select(o => o.ID).First())
                            .With(o => o.StatusQuestionaireMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "StatusQuestionaire").Select(o => o.ID).First())
                            .With(o => o.SalesOpportunityMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SalesOpportunity").Select(o => o.ID).First())
                            .Without(o => o.OwnerID)
                            .With(o => o.IsDeleted, false).Create();
                        opportunity.OwnerID = null;
                        opportunity.Contact = null;
                        opportunity.Project = null;

                        await db.Opportunities.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity = FixtureFactory.Get().Build<RevisitActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.RevisitActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType" && o.Order == 1).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        revisitActivity.RevisitActivityType = null;
                        revisitActivity.ConvenientTime = null;

                        await db.RevisitActivities.AddAsync(revisitActivity);
                        await db.SaveChangesAsync();

                        RevisitActivity revisitActivity2 = FixtureFactory.Get().Build<RevisitActivity>()
                            .With(o => o.OpportunityID, opportunity.ID)
                            .With(o => o.RevisitActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "RevisitActivityType" && o.Order == 2).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        revisitActivity2.RevisitActivityType = null;
                        revisitActivity2.ConvenientTime = null;

                        await db.RevisitActivities.AddAsync(revisitActivity2);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.GetRevisitActivityDraftAsync(opportunity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateOpportunityWithVisitorAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var visitor = await db.Visitors.Where(o => o.ContactID != null).FirstOrDefaultAsync();
                        var user = await db.Users.Where(o => o.EmployeeNo == "CR000749").FirstOrDefaultAsync();

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.CreateOpportunityAsync(null, user.ID, visitor.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AssignOpportunityListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        OpportunityAssignDTO opportunityAssignDTO = new OpportunityAssignDTO();

                        var oppFirst = await db.Opportunities.OrderBy(o => o.Created).Select(o => o.ID).FirstAsync();
                        var oppLast = await db.Opportunities.OrderByDescending(o => o.Updated).Select(o => o.ID).FirstAsync();

                        OpportunityListDTO oppListFirst = new OpportunityListDTO()
                        {
                            Id = oppFirst
                        };
                        OpportunityListDTO oppListSecond = new OpportunityListDTO()
                        {
                            Id = oppLast
                        };

                        opportunityAssignDTO.Opportunities = new List<OpportunityListDTO>();
                        opportunityAssignDTO.Opportunities.Add(oppListFirst);
                        opportunityAssignDTO.Opportunities.Add(oppListSecond);

                        var user = await db.Users.Select(o => o.ID).FirstAsync();
                        opportunityAssignDTO.User = new Base.DTOs.USR.UserListDTO();
                        opportunityAssignDTO.User.Id = user;

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.AssignOpportunityListAsync(opportunityAssignDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AssignOpportunityListRandomAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        OpportunityAssignDTO opportunityAssignDTO = new OpportunityAssignDTO();

                        var project = await db.Projects.Where(o => o.ProjectNo == "60010").FirstAsync();
                        var oppFirst = await db.Opportunities.Where(o => o.ProjectID == project.ID).OrderBy(o => o.Created).Select(o => o.ID).FirstAsync();
                        var oppLast = await db.Opportunities.Where(o => o.ProjectID == project.ID).OrderByDescending(o => o.Updated).Select(o => o.ID).FirstAsync();

                        OpportunityListDTO oppListFirst = new OpportunityListDTO()
                        {
                            Id = oppFirst
                        };
                        OpportunityListDTO oppListSecond = new OpportunityListDTO()
                        {
                            Id = oppLast
                        };

                        var opportunities = new List<OpportunityListDTO>();
                        opportunities.Add(oppListFirst);
                        opportunities.Add(oppListSecond);

                        // Act
                        var service = new OpportunityService(db);
                        var results = await service.AssignOpportunityListRandomAsync(project.ID, opportunities);

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
