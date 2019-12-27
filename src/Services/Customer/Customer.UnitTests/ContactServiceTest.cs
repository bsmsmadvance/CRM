using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.MST;
using Customer.Params.Filters;
using Customer.Services.ContactServices;
using Database.Models.CTM;
using Database.UnitTestExtensions;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.Models;

namespace Customer.UnitTests
{
    public class ContactServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetContactList()
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
                            OpportunityCount = 0,
                            IsDeleted = false
                        };
                        ContactPhone contactPhone = FixtureFactory.Get().Build<ContactPhone>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.IsMain, true)
                            .With(o => o.IsDeleted, false).Create();
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.IsDeleted, false).Create();

                        await db.AddAsync(contact);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(opportunity);
                        await db.SaveChangesAsync();

                        contact.LastOpportunityID = opportunity.ID;
                        db.Update(contact);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        ContactFilter filter = new ContactFilter();
                        PageParam pageParam = new PageParam { Page = 1, PageSize = 10 };
                        ContactListSortByParam sortByParam = new ContactListSortByParam();
                        var results = await service.GetContactListAsync(filter, pageParam, sortByParam);

                        filter = FixtureFactory.Get().Build<ContactFilter>().Create();
                        results = await service.GetContactListAsync(filter, pageParam, sortByParam);

                        filter = new ContactFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(ContactListSortBy)).Cast<ContactListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new ContactListSortByParam() { SortBy = item };
                            results = await service.GetContactListAsync(filter, pageParam, sortByParam);
                        }

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetContact()
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
                            .With(o => o.ContactTitleTHMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactTitleTH").Select(c => c.ID).First())
                            .With(o => o.ContactTitleENMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactTitleEN").Select(c => c.ID).First())
                            .With(o => o.NationalMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "National").Select(c => c.ID).First())
                            .With(o => o.GenderMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "Gender").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false).Create();
                        ContactPhone contactPhone = FixtureFactory.Get().Build<ContactPhone>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.PhoneTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First())
                            .With(o => o.PhoneNumber, "0979811668")
                            .With(o => o.IsMain, true)
                            .With(o => o.IsDeleted, false).Create();
                        Opportunity opportunity = FixtureFactory.Get().Build<Opportunity>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.IsDeleted, false).Create();
                        ContactEmail contactEmail = FixtureFactory.Get().Build<ContactEmail>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.Email, "test@gmail.com")
                            .With(o => o.IsMain, true)
                            .With(o => o.IsDeleted, false).Create();
                        ContactEmail contactEmailSecond = FixtureFactory.Get().Build<ContactEmail>()
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.Email, "testfalse@gmail.com")
                            .With(o => o.IsMain, false)
                            .With(o => o.IsDeleted, false).Create();

                        await db.AddAsync(contact);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(opportunity);
                        await db.AddAsync(contactEmail);
                        await db.AddAsync(contactEmailSecond);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        var results = await service.GetContactAsync(contact.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateContact()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Arrage
                        ContactDTO contact = new ContactDTO()
                        {
                            PhoneNumber = "111111111",
                            PhoneNumberExt = "123",
                            CitizenIdentityNo = "9999999999991",
                            FirstNameTH = "ริซซี่",
                            LastNameTH = "ดวง"
                        };
                        contact.ContactType = new MasterCenterDropdownDTO();
                        contact.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == ContactTypeKeys.Individual).Select(o => o.ID).First();
                        contact.TitleTH = new MasterCenterDropdownDTO();
                        contact.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                        contact.TitleEN = new MasterCenterDropdownDTO();
                        contact.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                        contact.National = new MasterCenterDropdownDTO();
                        contact.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                        contact.Gender = new MasterCenterDropdownDTO();
                        contact.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                        contact.LeadID = null;
                        contact.IsVIP = null;
                        contact.IsThaiNationality = null;
                        contact.Order = null;
                        contact.MiddleNameTH = "test";
                        contact.MiddleNameEN = "test";
                        contact.Nickname = "test";
                        contact.FirstNameEN = "test";
                        contact.LastNameEN = "test";
                        contact.ContactFirstName = null;
                        contact.ContactLastname = null;
                        contact.TitleExtEN = "Test";
                        contact.TitleExtTH = null;
                        contact.WeChat = null;
                        contact.LineID = null;
                        contact.WhatsApp = null;
                        contact.TaxID = "1111";
                        contact.ContactNo = null;
                        contact.BirthDate = DateTime.Now;
                        contact.IsVIP = true;

                        #region Phone and Email
                        var contactPhone = new ContactPhoneDTO()
                        {
                            PhoneNumber = "011111111",
                            IsMain = true
                        };
                        contactPhone.PhoneType = new MasterCenterDropdownDTO();
                        contactPhone.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType && c.Key == "1").Select(c => c.ID).First();

                        var contactPhone2 = new ContactPhoneDTO()
                        {
                            PhoneNumber = "011111111",
                            CountryCode = "+66",
                            IsMain = true
                        };
                        contactPhone2.PhoneType = new MasterCenterDropdownDTO();
                        contactPhone2.PhoneType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType && o.Key == "3").Select(o => o.ID).First();

                        contact.ContactPhones = new List<ContactPhoneDTO>();
                        contact.ContactPhones.Add(contactPhone);
                        contact.ContactPhones.Add(contactPhone2);

                        var contactEmail2 = new ContactEmailDTO()
                        {
                            Email = "",
                            IsMain = true
                        };
                        contact.ContactEmails = new List<ContactEmailDTO>();
                        contact.ContactEmails.Add(contactEmail2);
                        #endregion

                        var visitor = await db.Visitors.FirstAsync();

                        // Act
                        var service = new ContactService(db);
                        var results = await service.CreateContactAsync(contact, null, visitor.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateContactWithLeadQualify()
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
                            // Arrage
                            ContactDTO contact = new ContactDTO()
                            {
                                PhoneNumber = "111111111",
                                PhoneNumberExt = "123",
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง"
                            };
                            contact.ContactType = new MasterCenterDropdownDTO();
                            contact.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                            contact.TitleTH = new MasterCenterDropdownDTO();
                            contact.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                            contact.TitleEN = new MasterCenterDropdownDTO();
                            contact.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                            contact.National = new MasterCenterDropdownDTO();
                            contact.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                            contact.Gender = new MasterCenterDropdownDTO();
                            contact.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();
                            contact.LeadID = null;
                            contact.IsVIP = null;
                            contact.IsThaiNationality = null;
                            contact.Order = null;
                            contact.MiddleNameTH = "test";
                            contact.MiddleNameEN = "test";
                            contact.Nickname = "test";
                            contact.FirstNameEN = "test";
                            contact.LastNameEN = "test";
                            contact.ContactFirstName = null;
                            contact.ContactLastname = null;
                            contact.TitleExtEN = "Test";
                            contact.TitleExtTH = null;
                            contact.WeChat = null;
                            contact.LineID = null;
                            contact.WhatsApp = null;
                            contact.TaxID = "1111";
                            contact.ContactNo = null;
                            contact.BirthDate = DateTime.Now;

                            #region Phone and Email
                            var contactPhone = new ContactPhoneDTO()
                            {
                                PhoneNumber = "011111111",
                                IsMain = true
                            };
                            contactPhone.PhoneType = new MasterCenterDropdownDTO();
                            contactPhone.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType" && c.Key == "1").Select(c => c.ID).First();

                            var contactPhone2 = new ContactPhoneDTO()
                            {
                                PhoneNumber = "011111111",
                                CountryCode = "+66",
                                IsMain = true
                            };
                            contactPhone2.PhoneType = new MasterCenterDropdownDTO();
                            contactPhone2.PhoneType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "3").Select(o => o.ID).First();

                            contact.ContactPhones = new List<ContactPhoneDTO>();
                            contact.ContactPhones.Add(contactPhone);
                            contact.ContactPhones.Add(contactPhone2);

                            var contactEmail2 = new ContactEmailDTO()
                            {
                                Email = "korrawit.jaijit@gmail.com",
                                IsMain = true
                            };
                            contact.ContactEmails = new List<ContactEmailDTO>();
                            contact.ContactEmails.Add(contactEmail2);
                            #endregion

                            var leadID = await db.Leads.Where(o => o.IsDeleted != true).Select(o => o.ID).FirstOrDefaultAsync();
                            if (leadID != null)
                            {
                                contact.LeadID = leadID;
                            }
                            else
                            {
                                contact.LeadID = null;
                            }

                            // Act
                            var service = new ContactService(db);
                            var results = await service.CreateContactAsync(contact);
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
        public async void UpdateContact()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Create Old Contact Data
                        Contact contact = new Contact()
                        {
                            CitizenIdentityNo = "9999999999991",
                            FirstNameTH = "ริซซี่",
                            LastNameTH = "ดวง",

                        };
                        contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                        contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                        contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                        contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                        contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();

                        ContactEmail contactEmail = new ContactEmail()
                        {
                            ContactID = contact.ID,
                            IsMain = true,
                            Email = "palm@test.com"
                        };
                        ContactPhone contactPhone = new ContactPhone()
                        {
                            ContactID = contact.ID,
                            PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First(),
                            IsMain = true,
                            PhoneNumber = "0998124321"
                        };
                        ContactPhone contactPhone2 = new ContactPhone()
                        {
                            ContactID = contact.ID,
                            PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First(),
                            IsMain = false,
                            PhoneNumber = "0998124567"
                        };

                        await db.AddAsync(contact);
                        await db.AddAsync(contactEmail);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(contactPhone2);
                        await db.SaveChangesAsync();

                        // Arrage
                        ContactDTO contactDTO = new ContactDTO()
                        {
                            PhoneNumber = "111111111",
                            PhoneNumberExt = "123",
                            CitizenIdentityNo = "9999999999991",
                            FirstNameTH = "ริซซี่",
                            LastNameTH = "ดวง"
                        };
                        contactDTO.ContactType = new MasterCenterDropdownDTO();
                        contactDTO.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                        contactDTO.TitleTH = new MasterCenterDropdownDTO();
                        contactDTO.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                        contactDTO.TitleEN = new MasterCenterDropdownDTO();
                        contactDTO.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                        contactDTO.National = new MasterCenterDropdownDTO();
                        contactDTO.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                        contactDTO.Gender = new MasterCenterDropdownDTO();
                        contactDTO.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();
                        contactDTO.LeadID = null;
                        contactDTO.IsVIP = true;
                        contactDTO.IsThaiNationality = null;
                        contactDTO.Order = null;
                        contactDTO.MiddleNameTH = "test";
                        contactDTO.MiddleNameEN = "test";
                        contactDTO.Nickname = "test";
                        contactDTO.FirstNameEN = "test";
                        contactDTO.LastNameEN = "test";
                        contactDTO.ContactFirstName = null;
                        contactDTO.ContactLastname = null;
                        contactDTO.TitleExtEN = "Test";
                        contactDTO.TitleExtTH = null;
                        contactDTO.WeChat = null;
                        contactDTO.LineID = null;
                        contactDTO.WhatsApp = null;
                        contactDTO.TaxID = "1111";
                        contactDTO.ContactNo = null;
                        contactDTO.BirthDate = DateTime.Now;

                        var contactPhoneDTO = new ContactPhoneDTO()
                        {
                            Id = contactPhone.ID,
                            PhoneNumber = "011111111",
                            IsMain = false
                        };
                        contactPhoneDTO.PhoneType = new MasterCenterDropdownDTO();
                        contactPhoneDTO.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First();

                        var contactPhoneDTO2 = new ContactPhoneDTO()
                        {
                            PhoneNumber = "987654321",
                            IsMain = true
                        };
                        contactPhoneDTO2.PhoneType = new MasterCenterDropdownDTO();
                        contactPhoneDTO2.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First();

                        contactDTO.ContactPhones = new List<ContactPhoneDTO>();
                        contactDTO.ContactPhones.Add(contactPhoneDTO);
                        contactDTO.ContactPhones.Add(contactPhoneDTO2);

                        var contactEmailDTO = new ContactEmailDTO()
                        {
                            Id = contactEmail.ID,
                            Email = "testetstets@test.com",
                            IsMain = false
                        };

                        var contactEmailDTO2 = new ContactEmailDTO()
                        {
                            Email = "test@gmail.com",
                            IsMain = true
                        };
                        contactDTO.ContactEmails = new List<ContactEmailDTO>();
                        contactDTO.ContactEmails.Add(contactEmailDTO);
                        contactDTO.ContactEmails.Add(contactEmailDTO2);

                        // Act
                        var service = new ContactService(db);
                        var results = await service.UpdateContactAsync(contact.ID, contactDTO);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteContact()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Create Old Contact Data
                        Contact contact = new Contact()
                        {
                            CitizenIdentityNo = "9999999999991",
                            FirstNameTH = "ริซซี่",
                            LastNameTH = "ดวง",

                        };
                        contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                        contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                        contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                        contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                        contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();

                        ContactEmail contactEmail = new ContactEmail()
                        {
                            ContactID = contact.ID,
                            IsMain = true,
                            Email = "palm@test.com"
                        };
                        ContactPhone contactPhone = new ContactPhone()
                        {
                            ContactID = contact.ID,
                            PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First(),
                            IsMain = true,
                            PhoneNumber = "0998124321"
                        };
                        ContactPhone contactPhone2 = new ContactPhone()
                        {
                            ContactID = contact.ID,
                            PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First(),
                            IsMain = false,
                            PhoneNumber = "0998124567"
                        };

                        await db.AddAsync(contact);
                        await db.AddAsync(contactEmail);
                        await db.AddAsync(contactPhone);
                        await db.AddAsync(contactPhone2);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        await service.DeleteContactAsync(contact.ID);
                        var getResults = await service.GetContactAsync(contact.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetContactAddressList()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var master = await db.MasterCenters.Where(m => m.MasterCenterGroupKey == "ContactAddressType").ToListAsync();
                        var project = await db.Projects.FirstOrDefaultAsync();
                        Contact contact = FixtureFactory.Get().Build<Contact>().With(o => o.IsDeleted, false).Create();
                        Database.Models.MST.Country country = FixtureFactory.Get().Build<Database.Models.MST.Country>()
                            .With(o => o.NameTH, "ไทย")
                            .With(o => o.NameEN, "Thailand")
                            .With(o => o.IsDeleted, false).Create();
                        ContactAddress citizenAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "1").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        ContactAddress workAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "3").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        ContactAddress homeAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "2").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        ContactAddress contactAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "0").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        ContactAddressProject contactProject = FixtureFactory.Get().Build<ContactAddressProject>()
                            .With(o => o.ContactAddressID, contactAddress.ID)
                            .With(o => o.ProjectID, project.ID)
                            .With(o => o.IsDeleted, false)
                            .Create();

                        await db.AddAsync(contact);
                        await db.SaveChangesAsync();
                        await db.AddAsync(country);
                        await db.AddAsync(citizenAddress);
                        await db.AddAsync(workAddress);
                        await db.AddAsync(homeAddress);
                        await db.AddAsync(contactAddress);
                        await db.AddAsync(contactProject);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        var results = await service.GetContactAddressListAsync(contact.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetContactAddress()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync();
                        Contact contact = FixtureFactory.Get().Build<Contact>().With(o => o.IsDeleted, false).Create();
                        Database.Models.MST.Country country = FixtureFactory.Get().Build<Database.Models.MST.Country>()
                            .With(o => o.NameTH, "ไทย")
                            .With(o => o.NameEN, "Thailand")
                            .With(o => o.IsDeleted, false).Create();
                        ContactAddress citizenAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "1").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        citizenAddress.ContactAddressType = null;
                        ContactAddress workAddress = FixtureFactory.Get().Build<ContactAddress>()
                                .With(o => o.CountryID, country.ID)
                                .With(o => o.ProvinceID, db.Provinces.First().ID)
                                .With(o => o.DistrictID, db.Districts.First().ID)
                                .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                                .With(o => o.ContactID, contact.ID)
                                .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "3").Select(c => c.ID).First())
                                .With(o => o.IsDeleted, false)
                                .Create();
                        workAddress.ContactAddressType = null;
                        ContactAddress homeAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "2").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        homeAddress.ContactAddressType = null;
                        ContactAddress contactAddress = FixtureFactory.Get().Build<ContactAddress>()
                            .With(o => o.CountryID, country.ID)
                            .With(o => o.ProvinceID, db.Provinces.First().ID)
                            .With(o => o.DistrictID, db.Districts.First().ID)
                            .With(o => o.SubDistrictID, db.SubDistricts.First().ID)
                            .With(o => o.ContactID, contact.ID)
                            .With(o => o.ContactAddressTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "0").Select(c => c.ID).First())
                            .With(o => o.IsDeleted, false)
                            .Create();
                        contactAddress.ContactAddressType = null;
                        ContactAddressProject contactProject = FixtureFactory.Get().Build<ContactAddressProject>()
                            .With(o => o.ContactAddressID, contactAddress.ID)
                            .With(o => o.ProjectID, project.ID)
                            .With(o => o.IsDeleted, false)
                            .Create();

                        await db.AddAsync(contact);
                        await db.SaveChangesAsync();
                        await db.AddAsync(country);
                        await db.AddAsync(citizenAddress);
                        await db.AddAsync(workAddress);
                        await db.AddAsync(homeAddress);
                        await db.AddAsync(contactAddress);
                        await db.AddAsync(contactProject);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        var results = await service.GetContactAddressAsync(contact.ID, contactAddress.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateContactAddress()
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
                            var project = await db.Projects.OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                            var project2 = await db.Projects.FirstOrDefaultAsync();
                            Contact contact = new Contact()
                            {
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง",

                            };
                            contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                            contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                            contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                            contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                            contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();
                            await db.Contacts.AddAsync(contact);
                            await db.SaveChangesAsync();

                            ContactAddress contactAddress = new ContactAddress()
                            {
                                ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "0").Select(c => c.ID).First(),
                                ContactID = contact.ID
                            };

                            await db.ContactAddresses.AddAsync(contactAddress);
                            await db.SaveChangesAsync();

                            ContactAddressProject contactAddressProject = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddress.ID,
                                ProjectID = project.ID
                            };

                            await db.ContactAddressProjects.AddAsync(contactAddressProject);
                            await db.SaveChangesAsync();

                            ContactAddressDTO input = new ContactAddressDTO();
                            input.ContactAddressType = new MasterCenterDropdownDTO();
                            input.ContactAddressType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "ContactAddressType" && c.Key == "1").Select(c => c.ID).First();
                            input.Province = ProvinceListDTO.CreateFromModel(db.Provinces.First());
                            input.District = DistrictListDTO.CreateFromModel(db.Districts.First());
                            input.SubDistrict = SubDistrictListDTO.CreateFromModel(db.SubDistricts.First());
                            input.Country = CountryDTO.CreateFromModel(db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").First());
                            input.Project = new Base.DTOs.PRJ.ProjectDTO();
                            input.Project.Id = project2.ID;
                            input.contactID = contact.ID;
                            input.MooTH = "1";
                            input.HouseNoTH = "847";
                            input.VillageTH = "test";
                            input.VillageEN = "test";
                            input.RoadTH = "sripom";
                            input.SoiTH = "";
                            input.PostalCode = "50001";
                            input.RoadEN = "test";
                            input.ForeignDistrict = null;
                            input.ForeignProvince = null;
                            input.ForeignSubDistrict = null;

                            // Act
                            var service = new ContactService(db);
                            var results = await service.CreateContactAddressAsync(contact.ID, input);

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
        public async void UpdateContactAddress()
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
                            var project = await db.Projects.OrderBy(o => o.Created).FirstOrDefaultAsync();
                            var project2 = await db.Projects.OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                            Contact contact = new Contact()
                            {
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง",

                            };
                            contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                            contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                            await db.Contacts.AddAsync(contact);
                            await db.SaveChangesAsync();

                            ContactAddress contactAddress = new ContactAddress()
                            {
                                ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First(),
                                ContactID = contact.ID
                            };

                            await db.ContactAddresses.AddAsync(contactAddress);
                            await db.SaveChangesAsync();

                            ContactAddressProject contactAddressProject = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddress.ID,
                                ProjectID = project.ID
                            };

                            await db.ContactAddressProjects.AddAsync(contactAddressProject);
                            await db.SaveChangesAsync();

                            ContactAddress contactAddress2 = new ContactAddress()
                            {
                                ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "0").Select(c => c.ID).First(),
                                ContactID = contact.ID
                            };

                            await db.ContactAddresses.AddAsync(contactAddress2);
                            await db.SaveChangesAsync();

                            ContactAddressProject contactAddressProject2 = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddress2.ID,
                                ProjectID = project2.ID
                            };

                            await db.ContactAddressProjects.AddAsync(contactAddressProject2);
                            await db.SaveChangesAsync();

                            ContactAddressDTO input = new ContactAddressDTO();
                            input.ContactAddressType = new MasterCenterDropdownDTO();
                            input.ContactAddressType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First();
                            input.Province = ProvinceListDTO.CreateFromModel(db.Provinces.First());
                            input.District = DistrictListDTO.CreateFromModel(db.Districts.First());
                            input.SubDistrict = SubDistrictListDTO.CreateFromModel(db.SubDistricts.First());
                            input.Country = CountryDTO.CreateFromModel(db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").First());
                            input.Project = new Base.DTOs.PRJ.ProjectDTO();
                            input.Project.Id = project.ID;
                            input.contactID = contact.ID;
                            input.MooTH = "1";
                            input.HouseNoTH = "847";
                            input.VillageTH = "test";
                            input.VillageEN = "test";
                            input.RoadTH = "sripom";
                            input.SoiTH = "";
                            input.PostalCode = "50001";
                            input.RoadEN = "test";
                            input.ForeignDistrict = null;
                            input.ForeignProvince = null;
                            input.ForeignSubDistrict = null;

                            // Act
                            var service = new ContactService(db);
                            var results = await service.UpdateContactAddressAsync(contact.ID, contactAddress.ID, input);

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
        public async void DeleteContactAddress()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync();
                        Database.Models.MST.Country country = FixtureFactory.Get().Build<Database.Models.MST.Country>()
                            .With(o => o.NameTH, "ไทย")
                            .With(o => o.NameEN, "Thailand")
                            .With(o => o.IsDeleted, false).Create();
                        Contact contact = FixtureFactory.Get().Build<Contact>().With(o => o.IsDeleted, false).Create();
                        ContactAddress contactAddress = FixtureFactory.Get().Build<ContactAddress>().With(o => o.IsDeleted, false).Create();
                        contactAddress.ContactAddressType = null;
                        contactAddress.ContactAddressTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactAddressType" && o.Key == "0").Select(c => c.ID).First();
                        ContactAddressProject contactAddressProject = FixtureFactory.Get().Build<ContactAddressProject>()
                            .With(o => o.ContactAddressID, contactAddress.ID)
                            .With(o => o.ProjectID, project.ID)
                            .With(o => o.IsDeleted, false).Create();

                        await db.AddAsync(contact);
                        await db.AddAsync(country);
                        await db.AddAsync(contactAddress);
                        await db.AddAsync(contactAddressProject);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new ContactService(db);
                        await service.DeleteContactAddressAsync(contactAddress.ID);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void GetContactSimilarAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        // Arrage
                        ContactDTO contact = FixtureFactory.Get().Build<ContactDTO>()
                            .With(o => o.FirstNameTH, "สุเรซ กุมา")
                            .With(o => o.LastNameTH, "เสนานันท์")
                            .With(o => o.CitizenIdentityNo, "3440300619871")
                            .Create();
                        contact.ContactType.Key = ContactTypeKeys.Individual;
                        contact.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == ContactTypeKeys.Individual).Select(o => o.ID).First();
                        contact.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                        contact.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN).Select(o => o.ID).First();
                        contact.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                        contact.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();

                        var contactPhone = new ContactPhoneDTO()
                        {
                            PhoneNumber = "0829219473",
                            IsMain = true
                        };
                        contactPhone.PhoneType = new MasterCenterDropdownDTO();
                        contactPhone.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(c => c.ID).First();
                        contact.ContactPhones = new List<ContactPhoneDTO>();
                        contact.ContactPhones.Add(contactPhone);
                        contact.LeadID = null;
                        contact.IsVIP = null;
                        contact.IsThaiNationality = null;
                        contact.Order = null;

                        // Act
                        var service = new ContactService(db);
                        var results = await service.GetContactSimilarAsync(contact);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateContactWithSimilar()
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
                            // Create Old Contact Data
                            Contact oldContact = FixtureFactory.Get().Build<Contact>()
                                .With(o => o.PhoneNumber, "22222")
                                .With(o => o.PhoneNumberExt, "22222")
                                .With(o => o.CitizenIdentityNo, "9999999999991")
                                .With(o => o.IsDeleted, false).Create();
                            ContactEmail oldContactEmail = FixtureFactory.Get().Build<ContactEmail>()
                                .With(o => o.ContactID, oldContact.ID)
                                .With(o => o.Email, "Test@test.com")
                                .With(o => o.IsMain, true)
                                .With(o => o.IsDeleted, false).Create();
                            ContactPhone oldContactPhone = FixtureFactory.Get().Build<ContactPhone>()
                                .With(o => o.ContactID, oldContact.ID)
                                .With(o => o.PhoneNumber, "0988118484")
                                .With(o => o.PhoneTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First())
                                .With(o => o.IsMain, true)
                                .With(o => o.IsDeleted, false).Create();
                            ContactPhone oldContactPhone2 = FixtureFactory.Get().Build<ContactPhone>()
                                .With(o => o.ContactID, oldContact.ID)
                                .With(o => o.PhoneNumber, "0642992811")
                                .With(o => o.PhoneTypeMasterCenterID, db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType").Select(c => c.ID).First())
                                .With(o => o.IsMain, false)
                                .With(o => o.IsDeleted, false).Create();

                            await db.AddAsync(oldContact);
                            await db.AddAsync(oldContactEmail);
                            await db.AddAsync(oldContactPhone);
                            await db.AddAsync(oldContactPhone2);
                            await db.SaveChangesAsync();

                            // Arrage
                            ContactDTO contact = new ContactDTO()
                            {
                                PhoneNumber = "111111111",
                                PhoneNumberExt = "123",
                                CitizenIdentityNo = "1234123455661",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง"
                            };
                            contact.ContactType = new MasterCenterDropdownDTO();
                            contact.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                            contact.TitleTH = new MasterCenterDropdownDTO();
                            contact.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                            contact.TitleEN = new MasterCenterDropdownDTO();
                            contact.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                            contact.National = new MasterCenterDropdownDTO();
                            contact.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                            contact.Gender = new MasterCenterDropdownDTO();
                            contact.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();
                            contact.LeadID = null;
                            contact.IsVIP = null;
                            contact.IsThaiNationality = null;
                            contact.Order = null;
                            contact.MiddleNameTH = "test";
                            contact.MiddleNameEN = "test";
                            contact.Nickname = "test";
                            contact.FirstNameEN = "test";
                            contact.LastNameEN = "test";
                            contact.ContactFirstName = null;
                            contact.ContactLastname = null;
                            contact.TitleExtEN = "Test";
                            contact.TitleExtTH = null;
                            contact.WeChat = null;
                            contact.LineID = null;
                            contact.WhatsApp = null;
                            contact.TaxID = "1111";
                            contact.ContactNo = null;
                            contact.BirthDate = DateTime.Now;

                            #region Phone and Email
                            var contactPhone = new ContactPhoneDTO()
                            {
                                PhoneNumber = "011111111",
                                IsMain = true
                            };
                            contactPhone.PhoneType = new MasterCenterDropdownDTO();
                            contactPhone.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType" && c.Key == "1").Select(c => c.ID).First();

                            var contactPhone2 = new ContactPhoneDTO()
                            {
                                PhoneNumber = "011111111",
                                CountryCode = "+66",
                                IsMain = true
                            };
                            contactPhone2.PhoneType = new MasterCenterDropdownDTO();
                            contactPhone2.PhoneType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "3").Select(o => o.ID).First();

                            contact.ContactPhones = new List<ContactPhoneDTO>();
                            contact.ContactPhones.Add(contactPhone);
                            contact.ContactPhones.Add(contactPhone2);

                            var contactEmail2 = new ContactEmailDTO()
                            {
                                Email = "korrawit.jaijit@gmail.com",
                                IsMain = true
                            };
                            contact.ContactEmails = new List<ContactEmailDTO>();
                            contact.ContactEmails.Add(contactEmail2);
                            #endregion

                            // Act
                            var service = new ContactService(db);
                            var results = await service.CreateContactAsync(contact, oldContact.ID);
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
        public async void CreateContactWithLeadSimilar()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var project = await db.Projects.FirstOrDefaultAsync();
                        Lead lead = new Lead
                        {
                            FirstName = "ริซซี่",
                            LastName = "ดวง",
                            PhoneNumber = "0891231667",
                            Telephone = "022222222",
                            ProjectID = project.ID,
                            LeadTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadType).Select(o => o.ID).First(),
                            AdvertisementMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Advertisement).Select(o => o.ID).First(),
                            LeadStatusMasterCenterID = await db.MasterCenters.Where(o => o.Key == "1" && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadStatus).Select(o => o.ID).FirstAsync()
                        };

                        await db.Leads.AddAsync(lead);
                        await db.SaveChangesAsync();

                        // Arrage
                        ContactDTO contact = new ContactDTO()
                        {
                            PhoneNumber = "111111111",
                            PhoneNumberExt = "123",
                            CitizenIdentityNo = "1234123455661",
                            FirstNameTH = "ริซซี่",
                            LastNameTH = "ดวง"
                        };
                        contact.ContactType = new MasterCenterDropdownDTO();
                        contact.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "0").Select(o => o.ID).First();
                        contact.TitleTH = new MasterCenterDropdownDTO();
                        contact.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH").Select(o => o.ID).First();
                        contact.TitleEN = new MasterCenterDropdownDTO();
                        contact.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).First();
                        contact.National = new MasterCenterDropdownDTO();
                        contact.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "National" && o.Key == "thai").Select(o => o.ID).First();
                        contact.Gender = new MasterCenterDropdownDTO();
                        contact.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "Gender").Select(o => o.ID).First();
                        contact.LeadID = null;
                        contact.IsVIP = true;
                        contact.IsThaiNationality = null;
                        contact.Order = null;
                        contact.MiddleNameTH = "test";
                        contact.MiddleNameEN = "test";
                        contact.Nickname = "test";
                        contact.FirstNameEN = "test";
                        contact.LastNameEN = "test";
                        contact.ContactFirstName = null;
                        contact.ContactLastname = null;
                        contact.TitleExtEN = "Test";
                        contact.TitleExtTH = null;
                        contact.WeChat = null;
                        contact.LineID = null;
                        contact.WhatsApp = null;
                        contact.TaxID = "1111";
                        contact.ContactNo = null;
                        contact.BirthDate = DateTime.Now;

                        #region Phone and Email
                        var contactPhone = new ContactPhoneDTO()
                        {
                            PhoneNumber = "011111111",
                            IsMain = true
                        };
                        contactPhone.PhoneType = new MasterCenterDropdownDTO();
                        contactPhone.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == "PhoneType" && c.Key == "1").Select(c => c.ID).First();

                        var contactPhone2 = new ContactPhoneDTO()
                        {
                            PhoneNumber = "011111111",
                            CountryCode = "+66",
                            IsMain = true
                        };
                        contactPhone2.PhoneType = new MasterCenterDropdownDTO();
                        contactPhone2.PhoneType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "3").Select(o => o.ID).First();

                        contact.ContactPhones = new List<ContactPhoneDTO>();
                        contact.ContactPhones.Add(contactPhone);
                        contact.ContactPhones.Add(contactPhone2);

                        var contactEmail2 = new ContactEmailDTO()
                        {
                            Email = "korrawit.jaijit@gmail.com",
                            IsMain = true
                        };
                        contact.ContactEmails = new List<ContactEmailDTO>();
                        contact.ContactEmails.Add(contactEmail2);
                        #endregion

                        // Act
                        var service = new ContactService(db);
                        var results = await service.CreateContactAsync(contact, null, lead.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void UpdateContactWithBookingOwner()
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
                            Contact contact = new Contact()
                            {
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง",

                            };
                            contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == "thai").Select(o => o.ID).First();
                            contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();

                            ContactEmail contactEmail = new ContactEmail()
                            {
                                ContactID = contact.ID,
                                IsMain = true,
                                Email = "palm@test.com"
                            };
                            ContactPhone contactPhone = new ContactPhone()
                            {
                                ContactID = contact.ID,
                                PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(c => c.ID).First(),
                                IsMain = true,
                                PhoneNumber = "0998124321"
                            };
                            ContactPhone contactPhone2 = new ContactPhone()
                            {
                                ContactID = contact.ID,
                                PhoneTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(c => c.ID).First(),
                                IsMain = false,
                                PhoneNumber = "0998124567"
                            };

                            await db.AddAsync(contact);
                            await db.AddAsync(contactEmail);
                            await db.AddAsync(contactPhone);
                            await db.AddAsync(contactPhone2);
                            await db.SaveChangesAsync();

                            // Arrage
                            ContactDTO contactDTO = new ContactDTO()
                            {
                                PhoneNumber = "111111111",
                                PhoneNumberExt = "123",
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง"
                            };
                            contactDTO.ContactType = new MasterCenterDropdownDTO();
                            contactDTO.ContactType.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            contactDTO.TitleTH = new MasterCenterDropdownDTO();
                            contactDTO.TitleTH.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            contactDTO.TitleEN = new MasterCenterDropdownDTO();
                            contactDTO.TitleEN.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            contactDTO.National = new MasterCenterDropdownDTO();
                            contactDTO.National.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == "thai").Select(o => o.ID).First();
                            contactDTO.Gender = new MasterCenterDropdownDTO();
                            contactDTO.Gender.Id = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                            contactDTO.LeadID = null;
                            contactDTO.IsVIP = null;
                            contactDTO.IsThaiNationality = null;
                            contactDTO.Order = null;
                            contactDTO.MiddleNameTH = "test";
                            contactDTO.MiddleNameEN = "test";
                            contactDTO.Nickname = "test";
                            contactDTO.FirstNameEN = "test";
                            contactDTO.LastNameEN = "test";
                            contactDTO.ContactFirstName = null;
                            contactDTO.ContactLastname = null;
                            contactDTO.TitleExtEN = "Test";
                            contactDTO.TitleExtTH = null;
                            contactDTO.WeChat = null;
                            contactDTO.LineID = null;
                            contactDTO.WhatsApp = null;
                            contactDTO.TaxID = "1111";
                            contactDTO.ContactNo = null;
                            contactDTO.BirthDate = DateTime.Now;

                            var contactPhoneDTO = new ContactPhoneDTO()
                            {
                                Id = contactPhone.ID,
                                PhoneNumber = "011111111",
                                IsMain = false
                            };
                            contactPhoneDTO.PhoneType = new MasterCenterDropdownDTO();
                            contactPhoneDTO.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(c => c.ID).First();

                            var contactPhoneDTO2 = new ContactPhoneDTO()
                            {
                                PhoneNumber = "987654321",
                                IsMain = true
                            };
                            contactPhoneDTO2.PhoneType = new MasterCenterDropdownDTO();
                            contactPhoneDTO2.PhoneType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.PhoneType).Select(c => c.ID).First();

                            contactDTO.ContactPhones = new List<ContactPhoneDTO>();
                            contactDTO.ContactPhones.Add(contactPhoneDTO);
                            contactDTO.ContactPhones.Add(contactPhoneDTO2);

                            var contactEmailDTO = new ContactEmailDTO()
                            {
                                Id = contactEmail.ID,
                                Email = "testetstets@test.com",
                                IsMain = false
                            };

                            var contactEmailDTO2 = new ContactEmailDTO()
                            {
                                Email = "test@gmail.com",
                                IsMain = true
                            };
                            contactDTO.ContactEmails = new List<ContactEmailDTO>();
                            contactDTO.ContactEmails.Add(contactEmailDTO);
                            contactDTO.ContactEmails.Add(contactEmailDTO2);

                            #region Booking Owner
                            var unitStatusId = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == "2").Select(o => o.ID).FirstAsync();
                            var pricelist = await db.PriceLists
                                .Include(o => o.Unit)
                                .Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusId)
                                .FirstAsync();
                            var booking = new Booking()
                            {
                                UnitID = pricelist.UnitID,
                                ProjectID = pricelist.Unit.ProjectID
                            };
                            await db.Bookings.AddAsync(booking);

                            var unitPrice = new UnitPrice()
                            {
                                BookingID = booking.ID,
                            };

                            var bookingPriceListItem = await db.PriceListItems
                                .Include(o => o.MasterPriceItem)
                                .Where(o => o.PriceListID == pricelist.ID && o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).FirstAsync();
                            var unitPriceItem = new UnitPriceItem()
                            {
                                UnitPriceID = unitPrice.ID,
                                Name = bookingPriceListItem.MasterPriceItem.Detail,
                                Amount = bookingPriceListItem.Amount,
                                IsToBePay = bookingPriceListItem.IsToBePay,
                                PayDate = DateTime.Today,
                                DueDate = DateTime.Today,
                                PricePerUnitAmount = bookingPriceListItem.PricePerUnitAmount,
                                PriceUnitAmount = bookingPriceListItem.PriceUnitAmount,
                                FromMasterPriceListItemID = bookingPriceListItem.ID,
                                IsPaid = false,
                                PriceTypeMasterCenterID = bookingPriceListItem.PriceTypeMasterCenterID,
                                PriceUnitMasterCenterID = bookingPriceListItem.PriceUnitMasterCenterID,
                                MasterPriceItemID = bookingPriceListItem.MasterPriceItemID
                            };

                            await db.UnitPrices.AddAsync(unitPrice);
                            await db.UnitPriceItems.AddAsync(unitPriceItem);


                            var owner = new BookingOwner();
                            owner.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            owner.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            owner.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            owner.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == "thai").Select(o => o.ID).First();
                            owner.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                            owner.IsThaiNationality = contact.IsThaiNationality;
                            owner.MiddleNameTH = "owner";
                            owner.MiddleNameEN = "owner";
                            owner.Nickname = "owner";
                            owner.FirstNameEN = "owner";
                            owner.LastNameEN = "owner";
                            owner.ContactFirstName = null;
                            owner.ContactLastname = null;
                            owner.TitleExtEN = "owner";
                            owner.TitleExtTH = null;
                            owner.ContactNo = contact.ContactNo;
                            owner.BirthDate = contact.BirthDate;
                            owner.BookingID = booking.ID;
                            owner.FromContactID = contact.ID;

                            await db.BookingOwners.AddAsync(owner);
                            await db.SaveChangesAsync();
                            #endregion

                            // Act
                            var service = new ContactService(db);
                            var results = await service.UpdateContactAsync(contact.ID, contactDTO);
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
        public async void UpdateContactAddressWithBookingOwner()
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
                            var project = await db.Projects.OrderBy(o => o.Created).FirstOrDefaultAsync();
                            var project2 = await db.Projects.OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                            Contact contact = new Contact()
                            {
                                CitizenIdentityNo = "9999999999991",
                                FirstNameTH = "ริซซี่",
                                LastNameTH = "ดวง",

                            };
                            contact.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            contact.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            contact.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            contact.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == "thai").Select(o => o.ID).First();
                            contact.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                            await db.Contacts.AddAsync(contact);
                            await db.SaveChangesAsync();

                            ContactAddress contactAddress = new ContactAddress()
                            {
                                ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First(),
                                ContactID = contact.ID
                            };

                            await db.ContactAddresses.AddAsync(contactAddress);
                            await db.SaveChangesAsync();

                            ContactAddressProject contactAddressProject = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddress.ID,
                                ProjectID = project.ID
                            };

                            await db.ContactAddressProjects.AddAsync(contactAddressProject);
                            await db.SaveChangesAsync();

                            ContactAddress contactAddress2 = new ContactAddress()
                            {
                                ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "0").Select(c => c.ID).First(),
                                ContactID = contact.ID
                            };

                            await db.ContactAddresses.AddAsync(contactAddress2);
                            await db.SaveChangesAsync();

                            ContactAddressProject contactAddressProject2 = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddress2.ID,
                                ProjectID = project2.ID
                            };

                            await db.ContactAddressProjects.AddAsync(contactAddressProject2);
                            await db.SaveChangesAsync();

                            ContactAddressDTO input = new ContactAddressDTO();
                            input.ContactAddressType = new MasterCenterDropdownDTO();
                            input.ContactAddressType.Id = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First();
                            input.Province = ProvinceListDTO.CreateFromModel(db.Provinces.First());
                            input.District = DistrictListDTO.CreateFromModel(db.Districts.First());
                            input.SubDistrict = SubDistrictListDTO.CreateFromModel(db.SubDistricts.First());
                            input.Country = CountryDTO.CreateFromModel(db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").First());
                            input.Project = new Base.DTOs.PRJ.ProjectDTO();
                            input.Project.Id = project.ID;
                            input.contactID = contact.ID;
                            input.MooTH = "1";
                            input.HouseNoTH = "847";
                            input.VillageTH = "test";
                            input.VillageEN = "test";
                            input.RoadTH = "sripom";
                            input.SoiTH = "";
                            input.PostalCode = "50001";
                            input.RoadEN = "test";
                            input.ForeignDistrict = null;
                            input.ForeignProvince = null;
                            input.ForeignSubDistrict = null;

                            #region Booking Owner
                            var unitStatusId = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == "2").Select(o => o.ID).FirstAsync();
                            var pricelist = await db.PriceLists
                                .Include(o => o.Unit)
                                .Where(o => o.Unit.UnitStatusMasterCenterID == unitStatusId)
                                .FirstAsync();
                            var booking = new Booking()
                            {
                                UnitID = pricelist.UnitID,
                                ProjectID = pricelist.Unit.ProjectID
                            };
                            await db.Bookings.AddAsync(booking);

                            var unitPrice = new UnitPrice()
                            {
                                BookingID = booking.ID,
                            };

                            var bookingPriceListItem = await db.PriceListItems
                                .Include(o => o.MasterPriceItem)
                                .Where(o => o.PriceListID == pricelist.ID && o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).FirstAsync();
                            var unitPriceItem = new UnitPriceItem()
                            {
                                UnitPriceID = unitPrice.ID,
                                Name = bookingPriceListItem.MasterPriceItem.Detail,
                                Amount = bookingPriceListItem.Amount,
                                IsToBePay = bookingPriceListItem.IsToBePay,
                                PayDate = DateTime.Today,
                                DueDate = DateTime.Today,
                                PricePerUnitAmount = bookingPriceListItem.PricePerUnitAmount,
                                PriceUnitAmount = bookingPriceListItem.PriceUnitAmount,
                                FromMasterPriceListItemID = bookingPriceListItem.ID,
                                IsPaid = false,
                                PriceTypeMasterCenterID = bookingPriceListItem.PriceTypeMasterCenterID,
                                PriceUnitMasterCenterID = bookingPriceListItem.PriceUnitMasterCenterID,
                                MasterPriceItemID = bookingPriceListItem.MasterPriceItemID
                            };

                            await db.UnitPrices.AddAsync(unitPrice);
                            await db.UnitPriceItems.AddAsync(unitPriceItem);


                            var owner = new BookingOwner();
                            owner.ContactTypeMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).First();
                            owner.ContactTitleTHMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleTH).Select(o => o.ID).First();
                            owner.ContactTitleENMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).First();
                            owner.NationalMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == "thai").Select(o => o.ID).First();
                            owner.GenderMasterCenterID = db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.Gender).Select(o => o.ID).First();
                            owner.IsThaiNationality = contact.IsThaiNationality;
                            owner.MiddleNameTH = "owner";
                            owner.MiddleNameEN = "owner";
                            owner.Nickname = "owner";
                            owner.FirstNameEN = "owner";
                            owner.LastNameEN = "owner";
                            owner.ContactFirstName = null;
                            owner.ContactLastname = null;
                            owner.TitleExtEN = "owner";
                            owner.TitleExtTH = null;
                            owner.ContactNo = contact.ContactNo;
                            owner.BirthDate = contact.BirthDate;
                            owner.BookingID = booking.ID;
                            owner.FromContactID = contact.ID;

                            await db.BookingOwners.AddAsync(owner);
                            await db.SaveChangesAsync();
                            #endregion

                            // Act
                            var service = new ContactService(db);
                            var results = await service.UpdateContactAddressAsync(contact.ID, contactAddress.ID, input);

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
    }
}
