using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.CTM;
using Base.DTOs.USR;
using Customer.Params.Filters;
using Customer.Services.LeadService;
using Database.Models.CTM;
using Database.UnitTestExtensions;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagingExtensions;
using Xunit;
using System.Diagnostics;
using Database.Models.MasterKeys;

namespace Customer.UnitTests
{
    public class LeadServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetLeadListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            Telephone = "022222222",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First()
                        };
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        LeadFilter filter = new LeadFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        LeadListSortByParam sortByParam = new LeadListSortByParam();
                        var results = await service.GetLeadListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<LeadFilter>().Create();
                        filter.ExcludeIDs = string.Join(',', results.Leads.Select(o => o.Id).ToList());
                        results = await service.GetLeadListAsync(filter, pageParam, sortByParam);

                        filter = new LeadFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LeadListSortBy)).Cast<LeadListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LeadListSortByParam() { SortBy = item };
                            results = await service.GetLeadListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLeadAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = FixtureFactory.Get().Build<Lead>()
                            .With(o => o.FirstName, "Test")
                            .With(o => o.LastName, "TestLast")
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.Telephone, "022222222")
                            .With(o => o.LeadTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType" && o.Key == "C").Select(o => o.ID).First())
                            .With(o => o.AdvertisementMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First())
                            .With(o => o.LeadStatusMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First())
                            .With(o => o.ProjectID, db.Projects.Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        lead.ContactID = null;
                        lead.Contact = null;
                        lead.LeadType = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.GetLeadAsync(lead.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLeadAsync()
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
                            LeadDTO lead = new LeadDTO()
                            {
                                FirstName = "พริกไทย",
                                LastName = "จ้า",
                                PhoneNumber = "0101019293",
                                //Telephone = "023428976",
                                CitizenIdentityNo = "1234567891237",
                                Email = "test@test.com"
                            };
                            lead.Telephone = null;
                            lead.LeadType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            lead.LeadType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First();
                            lead.Advertisement = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            lead.Advertisement.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First();
                            lead.LeadStatus = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            lead.LeadStatus.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First();
                            var project = await db.Projects.Where(o => o.ProjectNo == "40011").FirstOrDefaultAsync();
                            lead.Project = new Base.DTOs.PRJ.ProjectDTO();
                            lead.Project.Id = project.ID;

                            var user = await db.Users.Where(o => o.EmployeeNo == "CR000749").FirstOrDefaultAsync();

                            // Act
                            var service = new LeadService(db);
                            var results = await service.CreateLeadAsync(lead, null);

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
        public async void UpdateLeadAsync()
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
                            var project = await db.Projects.FirstOrDefaultAsync();

                            Lead lead = new Lead()
                            {
                                FirstName = "Test",
                                LastName = "TestLast",
                                PhoneNumber = "0891231667",
                                Telephone = "022222222",
                                LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                                AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                                LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First(),
                                ProjectID = project.ID
                            };
                            lead.ContactID = null;
                            lead.Contact = null;
                            lead.LeadScore = 1;

                            await db.AddAsync(lead);

                            #region Lead Scoring
                            var leadScoringTypeList = await db.LeadScoringTypes.ToListAsync();
                            var callType = leadScoringTypeList.Where(o => o.Key == "1").First();
                            var emailType = leadScoringTypeList.Where(o => o.Key == "2").First();
                            var contactType = leadScoringTypeList.Where(o => o.Key == "3").First();
                            var bookType = leadScoringTypeList.Where(o => o.Key == "4").First();
                            var projectType = leadScoringTypeList.Where(o => o.Key == "5").First();

                            List<LeadScoring> leadSoreList = new List<LeadScoring>();
                            var leadCallModel = new LeadScoring()
                            {
                                LeadID = lead.ID,
                                LeadScoringTypeID = callType.ID,
                                Score = callType.Score,
                                IsGetScore = true,
                                IsLatestScore = true
                            };

                            var leadEmailModel = new LeadScoring()
                            {
                                LeadID = lead.ID,
                                LeadScoringTypeID = emailType.ID,
                                Score = emailType.Score,
                                IsGetScore = false,
                                IsLatestScore = true
                            };

                            var leadContactModel = new LeadScoring()
                            {
                                LeadID = lead.ID,
                                LeadScoringTypeID = contactType.ID,
                                Score = contactType.Score,
                                IsGetScore = false,
                                IsLatestScore = true
                            };

                            var leadBookingModel = new LeadScoring()
                            {
                                LeadID = lead.ID,
                                LeadScoringTypeID = bookType.ID,
                                Score = bookType.Score,
                                IsGetScore = false,
                                IsLatestScore = true
                            };

                            var leadProjectModel = new LeadScoring()
                            {
                                LeadID = lead.ID,
                                LeadScoringTypeID = projectType.ID,
                                Score = projectType.Score,
                                IsGetScore = false,
                                IsLatestScore = true
                            };

                            leadSoreList.Add(leadCallModel);
                            leadSoreList.Add(leadEmailModel);
                            leadSoreList.Add(leadContactModel);
                            leadSoreList.Add(leadBookingModel);
                            leadSoreList.Add(leadProjectModel);
                            #endregion

                            await db.LeadScorings.AddRangeAsync(leadSoreList);
                            await db.SaveChangesAsync();

                            LeadDTO leadDTO = new LeadDTO()
                            {
                                Id = lead.ID,
                                FirstName = "พริกไทย",
                                LastName = "จ้า",
                                PhoneNumber = "0891231667",
                                Telephone = "023428976",
                                CitizenIdentityNo = "1234567891237"
                            };
                            leadDTO.LeadType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            leadDTO.LeadType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First();
                            leadDTO.Advertisement = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            leadDTO.Advertisement.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First();
                            leadDTO.LeadStatus = new Base.DTOs.MST.MasterCenterDropdownDTO();
                            leadDTO.LeadStatus.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First();
                            leadDTO.Project = new Base.DTOs.PRJ.ProjectDTO();
                            leadDTO.Project.Id = project.ID;
                            leadDTO.LeadScoreList = new List<LeadScoreListDTO>();

                            // Act
                            var service = new LeadService(db);
                            var results = await service.UpdateLeadAsync(lead.ID, leadDTO);

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
        public async void AssignLeadAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var ownerUser = await db.Users
                        .Include(o => o.UserAuthorizeProjects)
                        .ThenInclude(m => m.Project)
                        .Include(o => o.UserRoles)
                        .Where(o => o.UserAuthorizeProjects.Any())
                        .Where(o => o.UserRoles.Where(x => x.Role.Code == "LC").Any())
                        .FirstAsync();
                        var lead = await db.Leads.FirstAsync(o => o.ProjectID == ownerUser.UserAuthorizeProjects.First().ProjectID && o.OwnerID != ownerUser.ID);
                        var input = UserListDTO.CreateFromModel(ownerUser);
                        var service = new LeadService(db);
                        var result = await service.AssignLeadAsync(lead.ID, input);
                        var hasLeadAssign = await db.LeadAssigns.Where(o => o.LeadID == lead.ID && o.OwnerID == input.Id).AnyAsync();
                        Assert.True(hasLeadAssign);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLeadAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = FixtureFactory.Get().Build<Lead>()
                            .With(o => o.FirstName, "Test")
                            .With(o => o.LastName, "TestLast")
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.Telephone, "022222222")
                            .With(o => o.LeadTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First())
                            .With(o => o.AdvertisementMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First())
                            .With(o => o.LeadStatusMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First())
                            .With(o => o.ProjectID, db.Projects.Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        await service.DeleteLeadAsync(lead.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLeadQualify()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Contact contact = FixtureFactory.Get().Build<Contact>()
                            .With(o => o.ContactNo, "ABC001")
                            .With(o => o.FirstNameTH, "Test")
                            .With(o => o.LastNameTH, "TestLast")
                            .With(o => o.ContactTitleTHMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First())
                            .With(o => o.ContactTitleENMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN).Select(o => o.ID).First())
                            .With(o => o.NationalMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National).Select(o => o.ID).First())
                            .With(o => o.GenderMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        ContactPhone contactPhone = FixtureFactory.Get().Build<ContactPhone>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.PhoneTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(o => o.ID).First())
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.IsMain, true)
                            .With(o => o.IsDeleted, false).Create();

                        Lead lead = FixtureFactory.Get().Build<Lead>()
                            .With(o => o.FirstName, "Test")
                            .With(o => o.LastName, "TestLast")
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.LeadTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType).Select(o => o.ID).First())
                            .With(o => o.AdvertisementMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Advertisement).Select(o => o.ID).First())
                            .With(o => o.LeadStatusMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus && o.Key == "0").Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.AddAsync(contact);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.GetLeadQualify(lead.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void SubmitLeadQualify()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Contact contact = new Contact()
                        {
                            ContactNo = "ABC001",
                            FirstNameTH = "Test",
                            LastNameTH = "TestLast",
                            ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First(),
                            ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN").Select(o => o.ID).First(),
                            NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National").Select(o => o.ID).First(),
                            GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First()
                        };

                        ContactPhone contactPhone = new ContactPhone()
                        {
                            ContactID = contact.ID,
                            PhoneTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType").Select(o => o.ID).First(),
                            PhoneNumber = "0979811668",
                            IsMain = true
                        };

                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus" && o.Key == "0").Select(o => o.ID).First(),
                        };
                        lead.ContactID = null;

                        await db.AddAsync(contact);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var leadQualifyList = await service.GetLeadQualify(lead.ID);
                        var leadQualifies = leadQualifyList.FirstOrDefault();
                        var results = await service.SubmitLeadQualify(lead.ID, null);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLeadActivityListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = FixtureFactory.Get().Build<Lead>()
                            .With(o => o.FirstName, "Test")
                            .With(o => o.LastName, "TestLast")
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.LeadTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First())
                            .With(o => o.AdvertisementMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First())
                            .With(o => o.LeadStatusMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity = FixtureFactory.Get().Build<LeadActivity>()
                            .With(o => o.LeadID, lead.ID)
                            .With(o => o.LeadActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType" && o.Order == 1).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        leadActivity.ActualDate = null;
                        leadActivity.LeadActivityType = null;
                        leadActivity.ConvenientTime = null;
                        leadActivity.StatusID = await db.LeadActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        leadActivity.LeadActivityStatus = null;

                        await db.LeadActivities.AddAsync(leadActivity);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity2 = FixtureFactory.Get().Build<LeadActivity>()
                            .With(o => o.LeadID, lead.ID)
                            .With(o => o.LeadActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType" && o.Order == 2).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, true)
                            .With(o => o.IsDeleted, false).Create();
                        leadActivity2.ActualDate = null;
                        leadActivity2.LeadActivityType = null;
                        leadActivity2.ConvenientTime = null;
                        leadActivity2.StatusID = await db.LeadActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();
                        leadActivity2.LeadActivityStatus = null;

                        await db.LeadActivities.AddAsync(leadActivity2);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.GetLeadActivityListAsync(lead.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLeadActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First()
                        };
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity = new LeadActivity()
                        {
                            LeadID = lead.ID,
                            LeadActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType" && o.Order == 1).Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            DueDate = DateTime.Now,
                            IsCompleted = true,
                            AppointmentDate = DateTime.Now
                        };

                        leadActivity.StatusID = await db.LeadActivityStatuses.Where(o => o.Order == 1).Select(o => o.ID).FirstAsync();

                        await db.LeadActivities.AddAsync(leadActivity);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.GetLeadActivityAsync(leadActivity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateLeadActivity()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType).Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Advertisement).Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus && o.Key == "0").Select(o => o.ID).First(),
                            IsDeleted = false
                        };
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivityDTO leadActivityDTO = new LeadActivityDTO()
                        {
                            LeadID = lead.ID,
                            DueDate = new DateTime(2019, 09, 17),
                            ActualDate = DateTime.Today,
                            FollowUpDueDate = DateTime.Today
                        };

                        var activityStatusResults = await db.LeadActivityStatuses
                            .Include(o => o.LeadActivityStatusType)
                            .Include(o => o.LeadActivityFollowUpType)
                            .Where(o => o.Order == 6)
                            .FirstOrDefaultAsync();

                        leadActivityDTO.SelectedActivityStatusID = activityStatusResults.ID;

                        leadActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        leadActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityType && o.Order == 4).Select(o => o.ID).First();
                        leadActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        leadActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ConvenientTime).Select(o => o.ID).First();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.CreateLeadActivityAsync(lead.ID, leadActivityDTO);

                        var activityTaskResult = await db.ActivityTasks.Where(o => o.LeadActivityID == results.Id).FirstOrDefaultAsync();
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateLeadActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First()
                        };
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity = new LeadActivity()
                        {
                            LeadID = lead.ID,
                            LeadActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            IsCompleted = false,
                            DueDate = DateTime.Now
                        };

                        await db.LeadActivities.AddAsync(leadActivity);
                        await db.SaveChangesAsync();

                        LeadActivityDTO leadActivityDTO = new LeadActivityDTO()
                        {
                            LeadID = lead.ID,
                            DueDate = DateTime.Now,
                            ActualDate = DateTime.Now,
                            AppointmentDate = DateTime.Now
                        };

                        var activityStatusResults = await db.LeadActivityStatuses
                            .Include(o => o.LeadActivityStatusType)
                            .Include(o => o.LeadActivityFollowUpType)
                            .Where(o => o.Order == 12)
                            .FirstOrDefaultAsync();

                        leadActivityDTO.SelectedActivityStatusID = activityStatusResults.ID;

                        var activityStatus = LeadActivityStatusDTO.CreateFromModel(activityStatusResults);
                        leadActivityDTO.ActivityStatuses = new List<LeadActivityStatusDTO>();
                        leadActivityDTO.ActivityStatuses.Add(activityStatus);

                        leadActivityDTO.ActivityType = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        leadActivityDTO.ActivityType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType").Select(o => o.ID).First();
                        leadActivityDTO.ConvenientTime = new Base.DTOs.MST.MasterCenterDropdownDTO();
                        leadActivityDTO.ConvenientTime.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.UpdateLeadActivityAsync(leadActivity.ID, leadActivityDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteLeadActivityAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = new Lead()
                        {
                            FirstName = "Test",
                            LastName = "TestLast",
                            PhoneNumber = "0979811668",
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First()
                        };
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity = new LeadActivity()
                        {
                            LeadID = lead.ID,
                            LeadActivityTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType").Select(o => o.ID).First(),
                            ConvenientTimeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First(),
                            IsCompleted = false,
                            DueDate = DateTime.Now
                        };

                        await db.LeadActivities.AddAsync(leadActivity);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        await service.DeleteLeadActivityAsync(leadActivity.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetLeadActivityDraftAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        Lead lead = FixtureFactory.Get().Build<Lead>()
                            .With(o => o.FirstName, "Test")
                            .With(o => o.LastName, "TestLast")
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.LeadTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadType").Select(o => o.ID).First())
                            .With(o => o.AdvertisementMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Advertisement").Select(o => o.ID).First())
                            .With(o => o.LeadStatusMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadStatus").Select(o => o.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        lead.ContactID = null;
                        lead.Contact = null;

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        LeadActivity leadActivity = FixtureFactory.Get().Build<LeadActivity>()
                            .With(o => o.LeadID, lead.ID)
                            .With(o => o.LeadActivityTypeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "LeadActivityType" && o.Order == 1).Select(o => o.ID).First())
                            .With(o => o.ConvenientTimeMasterCenterID, db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ConvenientTime").Select(o => o.ID).First())
                            .With(o => o.IsCompleted, false)
                            .With(o => o.IsDeleted, false).Create();
                        leadActivity.ActualDate = null;
                        leadActivity.LeadActivityType = null;
                        leadActivity.ConvenientTime = null;
                        leadActivity.StatusID = null;
                        leadActivity.LeadActivityStatus = null;

                        await db.LeadActivities.AddAsync(leadActivity);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new LeadService(db);
                        var results = await service.GetLeadActivityDraftAsync(lead.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AssignLeadListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        LeadAssignDTO leadAssignDTO = new LeadAssignDTO();

                        var leadFirst = await db.Leads.OrderBy(o => o.Created).Select(o => o.ID).FirstAsync();
                        var leadLast = await db.Leads.OrderBy(o => o.Updated).Select(o => o.ID).FirstAsync();

                        LeadListDTO leadListFirst = new LeadListDTO()
                        {
                            Id = leadFirst
                        };
                        LeadListDTO leadListSecond = new LeadListDTO()
                        {
                            Id = leadLast
                        };

                        leadAssignDTO.Leads = new List<LeadListDTO>();
                        leadAssignDTO.Leads.Add(leadListFirst);
                        leadAssignDTO.Leads.Add(leadListSecond);

                        var user = await db.Users.Select(o => o.ID).FirstAsync();
                        leadAssignDTO.User = new Base.DTOs.USR.UserListDTO();
                        leadAssignDTO.User.Id = user;
                        // Act
                        var service = new LeadService(db);
                        var results = await service.AssignLeadListAsync(leadAssignDTO);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void AssignLeadListRandomAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        LeadAssignDTO leadAssignDTO = new LeadAssignDTO();
                        var project = await db.Projects.Where(o => o.ProjectNo == "60010").FirstAsync();
                        var leadFirst = await db.Leads.Where(o => o.ProjectID == project.ID).OrderBy(o => o.Created).Select(o => o.ID).FirstAsync();
                        var leadLast = await db.Leads.Where(o => o.ProjectID == project.ID).OrderBy(o => o.Updated).Select(o => o.ID).FirstAsync();

                        LeadListDTO leadListFirst = new LeadListDTO()
                        {
                            Id = leadFirst
                        };
                        LeadListDTO leadListSecond = new LeadListDTO()
                        {
                            Id = leadLast
                        };

                        var leads = new List<LeadListDTO>();
                        leads.Add(leadListFirst);
                        leads.Add(leadListSecond);

                        // Act
                        var service = new LeadService(db);
                        var results = await service.AssignLeadListRandomAsync(project.ID, leads);
                        Trace.WriteLine(JsonConvert.SerializeObject(results));

                        tran.Rollback();
                    }
                });
            }
        }

    }
}
