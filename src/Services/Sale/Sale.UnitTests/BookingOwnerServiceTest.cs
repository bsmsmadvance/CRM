using AutoFixture;
using Base.DTOs.MST;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Sale.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sale.UnitTests
{
    public class BookingOwnerServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetBookingOwnersAsync()
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
                            var booking = new Booking()
                            {
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };
                            var bookingOwner = BookingOwner.CreateFromContact(contact);
                            bookingOwner.BookingID = booking.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.BookingOwners.AddAsync(bookingOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new BookingOwnerService(db);
                            var result = await service.GetBookingOwnersAsync(bookingOwner.BookingID.Value);
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
        public async void GetBookingOwnerDropdownAsync()
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
                            var booking = new Booking()
                            {
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };
                            var bookingOwner = BookingOwner.CreateFromContact(contact);
                            bookingOwner.BookingID = booking.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.BookingOwners.AddAsync(bookingOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new BookingOwnerService(db);
                            var result = await service.GetBookingOwnerDropdownAsync(bookingOwner.BookingID.Value);
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
        public async void GetBookingOwnersDraftAsync()
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
                        var booking = new Booking()
                        {
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                        };
                        await db.Bookings.AddAsync(booking);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new BookingOwnerService(db);
                        var results = await service.GetBookingOwnersDraftAsync(booking.ID, contact.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBookingOwnerAsync()
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
                        var booking = await db.Bookings.OrderByDescending(o => o.Created).FirstAsync();

                        var service = new BookingOwnerService(db);
                        var bookingOwner = await service.GetBookingOwnersDraftAsync(booking.ID, contact.ID);
                        bookingOwner.National = new MasterCenterDropdownDTO();
                        bookingOwner.National.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();
                        // Act

                        var isAddress = bookingOwner.BookingOwnerAddresses.Where(o => o.ContactAddressType.Key == "0").Any();
                        if (!isAddress)
                        {
                            var addressMasterCenter = await db.MasterCenters.Where(o => o.Key == "0" && o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).FirstAsync();
                            var address = new BookingOwnerAddressDTO()
                            {
                                MooTH = "1",
                                HouseNoTH = "847",
                                VillageTH = "test",
                                VillageEN = "test",
                                RoadTH = "sripom",
                                SoiTH = "",
                                PostalCode = "50001",
                                RoadEN = "test",
                                Province = ProvinceListDTO.CreateFromModel(db.Provinces.First()),
                                District = DistrictListDTO.CreateFromModel(db.Districts.First()),
                                SubDistrict = SubDistrictListDTO.CreateFromModel(db.SubDistricts.First()),
                                Country = CountryDTO.CreateFromModel(db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").First()),
                            };

                            address.ContactAddressType = new MasterCenterDropdownDTO();
                            address.ContactAddressType.Id = addressMasterCenter.ID;
                            address.Project = new Base.DTOs.PRJ.ProjectDropdownDTO();
                            address.Project.Id = booking.ProjectID;
                            bookingOwner.BookingOwnerAddresses.Add(address);
                        }

                        var result = await service.CreateBookingOwnerAsync(booking.ID, bookingOwner);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateBookingOwnerWithoutContactAsync()
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
                        var booking = await db.Bookings.FirstAsync();
                        // Act
                        var service = new BookingOwnerService(db);
                        var bookingOwner = await service.GetBookingOwnersDraftAsync(booking.ID, contact.ID);
                        bookingOwner.National = new MasterCenterDropdownDTO();
                        bookingOwner.National.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();

                        foreach (var email in bookingOwner.ContactEmails)
                        {
                            email.FromContactEmailID = null;
                        }

                        foreach (var phone in bookingOwner.ContactPhones)
                        {
                            phone.FromContactPhoneID = null;
                        }

                        foreach (var address in bookingOwner.BookingOwnerAddresses)
                        {
                            address.FromContactAddressID = null;
                        }

                        var isAddress = bookingOwner.BookingOwnerAddresses.Where(o => o.ContactAddressType.Key == "0").Any();
                        if (!isAddress)
                        {
                            var addressMasterCenter = await db.MasterCenters.Where(o => o.Key == "0" && o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).FirstAsync();
                            var address = new BookingOwnerAddressDTO()
                            {
                                MooTH = "1",
                                HouseNoTH = "847",
                                VillageTH = "test",
                                VillageEN = "test",
                                RoadTH = "sripom",
                                SoiTH = "",
                                PostalCode = "50001",
                                RoadEN = "test",
                                Province = ProvinceListDTO.CreateFromModel(db.Provinces.First()),
                                District = DistrictListDTO.CreateFromModel(db.Districts.First()),
                                SubDistrict = SubDistrictListDTO.CreateFromModel(db.SubDistricts.First()),
                                Country = CountryDTO.CreateFromModel(db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").First()),
                            };

                            address.ContactAddressType = new MasterCenterDropdownDTO();
                            address.ContactAddressType.Id = addressMasterCenter.ID;
                            address.Project = new Base.DTOs.PRJ.ProjectDropdownDTO();
                            address.Project.Id = booking.ProjectID;
                            bookingOwner.BookingOwnerAddresses.Add(address);
                        }

                        bookingOwner.FromContactID = null;

                        var result = await service.CreateBookingOwnerAsync(booking.ID, bookingOwner);

                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void DeleteBookingOwnerAsync()
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
                            var booking = new Booking()
                            {
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };
                            var bookingOwner = BookingOwner.CreateFromContact(contact);
                            bookingOwner.BookingID = booking.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.BookingOwners.AddAsync(bookingOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new BookingOwnerService(db);
                            await service.DeleteBookingOwnerAsync(bookingOwner.ID, string.Empty);
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
        public async void EditBookingOwnerAsync()
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

                            var owner = new BookingOwner()
                            {
                                ContactTypeMasterCenterID = contact.ContactTypeMasterCenterID,
                                ContactTitleTHMasterCenterID = contact.ContactTitleTHMasterCenterID,
                                ContactTitleENMasterCenterID = contact.ContactTitleENMasterCenterID,
                                NationalMasterCenterID = contact.NationalMasterCenterID,
                                GenderMasterCenterID = contact.GenderMasterCenterID,
                                IsThaiNationality = contact.IsThaiNationality,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact.ContactNo,
                                BirthDate = contact.BirthDate,
                                BookingID = booking.ID,
                                FromContactID = contact.ID
                            };

                            await db.UnitPrices.AddAsync(unitPrice);
                            await db.UnitPriceItems.AddAsync(unitPriceItem);
                            await db.BookingOwners.AddAsync(owner);

                            var contactEmails = await db.ContactEmails.Where(o => o.ContactID == contact.ID).ToListAsync();
                            foreach (var item in contactEmails)
                            {
                                var ownerEmail = new BookingOwnerEmail()
                                {
                                    BookingOwnerID = owner.ID,
                                    IsMain = item.IsMain,
                                    Email = item.Email,
                                    FromContactEmailID = item.ID
                                };
                                await db.BookingOwnerEmails.AddAsync(ownerEmail);
                            }

                            var contactPhones = await db.ContactPhones.Where(o => o.ContactID == contact.ID).ToListAsync();
                            foreach (var item in contactPhones)
                            {
                                var ownerPhone = new BookingOwnerPhone()
                                {
                                    BookingOwnerID = owner.ID,
                                    IsMain = item.IsMain,
                                    PhoneNumber = item.PhoneNumber,
                                    PhoneNumberExt = item.PhoneNumberExt,
                                    PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID,
                                    CountryCode = item.CountryCode,
                                    FromContactPhoneID = item.ID
                                };
                                await db.BookingOwnerPhones.AddAsync(ownerPhone);
                            }

                            var contactAddress = await db.ContactAddressProjects.Include(o => o.ContactAddress).Where(o => o.ProjectID == booking.ProjectID).FirstOrDefaultAsync();
                            if (contactAddress != null)
                            {
                                var ownerAddress = new BookingOwnerAddress();
                                ownerAddress.HouseNoTH = contactAddress.ContactAddress.HouseNoTH;
                                ownerAddress.MooTH = contactAddress.ContactAddress.MooTH;
                                ownerAddress.VillageTH = contactAddress.ContactAddress.VillageTH;
                                ownerAddress.SoiTH = contactAddress.ContactAddress.SoiTH;
                                ownerAddress.RoadTH = contactAddress.ContactAddress.RoadTH;
                                ownerAddress.CountryID = contactAddress.ContactAddress.CountryID;
                                ownerAddress.ProvinceID = contactAddress.ContactAddress.ProvinceID;
                                ownerAddress.DistrictID = contactAddress.ContactAddress.DistrictID;
                                ownerAddress.SubDistrictID = contactAddress.ContactAddress.SubDistrictID;
                                ownerAddress.HouseNoEN = contactAddress.ContactAddress.HouseNoEN;
                                ownerAddress.MooEN = contactAddress.ContactAddress.MooEN;
                                ownerAddress.VillageEN = contactAddress.ContactAddress.VillageEN;
                                ownerAddress.SoiEN = contactAddress.ContactAddress.SoiEN;
                                ownerAddress.RoadEN = contactAddress.ContactAddress.RoadEN;
                                ownerAddress.PostalCode = contactAddress.ContactAddress.PostalCode;
                                ownerAddress.ForeignSubDistrict = contactAddress.ContactAddress.ForeignSubDistrict;
                                ownerAddress.ForeignDistrict = contactAddress.ContactAddress.ForeignDistrict;
                                ownerAddress.ForeignProvince = contactAddress.ContactAddress.ForeignProvince;
                                ownerAddress.FromContactAddressID = contactAddress.ID;
                                ownerAddress.BookingOwnerID = owner.ID;
                                ownerAddress.ContactAddressTypeMasterCenterID = contactAddress.ContactAddress.ContactAddressTypeMasterCenterID;
                                await db.BookingOwnerAddresses.AddAsync(ownerAddress);
                            }
                            else
                            {
                                ContactAddress contactAddressModel = new ContactAddress()
                                {
                                    ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "0").Select(c => c.ID).First(),
                                    ContactID = contact.ID,
                                    ProvinceID = db.Provinces.Select(o => o.ID).First(),
                                    DistrictID = db.Districts.Select(o => o.ID).First(),
                                    SubDistrictID = db.SubDistricts.Select(o => o.ID).First(),
                                    CountryID = db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).First(),
                                    MooTH = "1",
                                    HouseNoTH = "847",
                                    VillageTH = "test",
                                    VillageEN = "test",
                                    RoadTH = "sripom",
                                    SoiTH = "",
                                    PostalCode = "50001",
                                    RoadEN = "test",
                                    ForeignDistrict = null,
                                    ForeignProvince = null,
                                    ForeignSubDistrict = null,
                                };

                                ContactAddressProject contactAddressProject = new ContactAddressProject()
                                {
                                    ContactAddressID = contactAddressModel.ID,
                                    ProjectID = booking.ProjectID
                                };

                                var ownerAddress = new BookingOwnerAddress()
                                {
                                    ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "0").Select(c => c.ID).First(),
                                    BookingOwnerID = owner.ID,
                                    ProvinceID = db.Provinces.Select(o => o.ID).First(),
                                    DistrictID = db.Districts.Select(o => o.ID).First(),
                                    SubDistrictID = db.SubDistricts.Select(o => o.ID).First(),
                                    CountryID = db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).First(),
                                    MooTH = "1",
                                    HouseNoTH = "847",
                                    VillageTH = "test",
                                    VillageEN = "test",
                                    RoadTH = "sripom",
                                    SoiTH = "",
                                    PostalCode = "50001",
                                    RoadEN = "test",
                                    ForeignDistrict = null,
                                    ForeignProvince = null,
                                    ForeignSubDistrict = null,
                                    FromContactAddressID = contactAddressModel.ID
                                };

                                await db.ContactAddresses.AddAsync(contactAddressModel);
                                await db.ContactAddressProjects.AddAsync(contactAddressProject);
                                await db.BookingOwnerAddresses.AddAsync(ownerAddress);
                            }

                            var contactCitizienAddress = await db.ContactAddresses.Include(o => o.ContactAddressType).Where(o => o.ContactID == contact.ID && o.ContactAddressType.Key == "1").FirstOrDefaultAsync();
                            if (contactCitizienAddress != null)
                            {
                                var ownerAddress = new BookingOwnerAddress();
                                ownerAddress.HouseNoTH = contactCitizienAddress.HouseNoTH;
                                ownerAddress.MooTH = contactCitizienAddress.MooTH;
                                ownerAddress.VillageTH = contactCitizienAddress.VillageTH;
                                ownerAddress.SoiTH = contactCitizienAddress.SoiTH;
                                ownerAddress.RoadTH = contactCitizienAddress.RoadTH;
                                ownerAddress.CountryID = contactCitizienAddress.CountryID;
                                ownerAddress.ProvinceID = contactCitizienAddress.ProvinceID;
                                ownerAddress.DistrictID = contactCitizienAddress.DistrictID;
                                ownerAddress.SubDistrictID = contactCitizienAddress.SubDistrictID;
                                ownerAddress.HouseNoEN = contactCitizienAddress.HouseNoEN;
                                ownerAddress.MooEN = contactCitizienAddress.MooEN;
                                ownerAddress.VillageEN = contactCitizienAddress.VillageEN;
                                ownerAddress.SoiEN = contactCitizienAddress.SoiEN;
                                ownerAddress.RoadEN = contactCitizienAddress.RoadEN;
                                ownerAddress.PostalCode = contactCitizienAddress.PostalCode;
                                ownerAddress.ForeignSubDistrict = contactCitizienAddress.ForeignSubDistrict;
                                ownerAddress.ForeignDistrict = contactCitizienAddress.ForeignDistrict;
                                ownerAddress.ForeignProvince = contactCitizienAddress.ForeignProvince;
                                ownerAddress.FromContactAddressID = contactCitizienAddress.ID;
                                ownerAddress.BookingOwnerID = owner.ID;
                                ownerAddress.ContactAddressTypeMasterCenterID = contactCitizienAddress.ContactAddressTypeMasterCenterID;
                                await db.BookingOwnerAddresses.AddAsync(ownerAddress);
                            }
                            else
                            {
                                ContactAddress contactCitizenAddressModel = new ContactAddress()
                                {
                                    ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First(),
                                    ContactID = contact.ID,
                                    ProvinceID = db.Provinces.Select(o => o.ID).First(),
                                    DistrictID = db.Districts.Select(o => o.ID).First(),
                                    SubDistrictID = db.SubDistricts.Select(o => o.ID).First(),
                                    CountryID = db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).First(),
                                    MooTH = "1",
                                    HouseNoTH = "847",
                                    VillageTH = "test",
                                    VillageEN = "test",
                                    RoadTH = "sripom",
                                    SoiTH = "",
                                    PostalCode = "50001",
                                    RoadEN = "test",
                                    ForeignDistrict = null,
                                    ForeignProvince = null,
                                    ForeignSubDistrict = null,
                                };

                                var ownerAddress = new BookingOwnerAddress()
                                {
                                    ContactAddressTypeMasterCenterID = db.MasterCenters.Where(c => c.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && c.Key == "1").Select(c => c.ID).First(),
                                    BookingOwnerID = owner.ID,
                                    ProvinceID = db.Provinces.Select(o => o.ID).First(),
                                    DistrictID = db.Districts.Select(o => o.ID).First(),
                                    SubDistrictID = db.SubDistricts.Select(o => o.ID).First(),
                                    CountryID = db.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).First(),
                                    MooTH = "1",
                                    HouseNoTH = "847",
                                    VillageTH = "test",
                                    VillageEN = "test",
                                    RoadTH = "sripom",
                                    SoiTH = "",
                                    PostalCode = "50001",
                                    RoadEN = "test",
                                    ForeignDistrict = null,
                                    ForeignProvince = null,
                                    ForeignSubDistrict = null,
                                    FromContactAddressID = contactCitizenAddressModel.ID
                                };

                                await db.ContactAddresses.AddAsync(contactCitizenAddressModel);
                                await db.BookingOwnerAddresses.AddAsync(ownerAddress);
                            }

                            await db.SaveChangesAsync();

                            // Act
                            var service = new BookingOwnerService(db);
                            var bookingOwner = await service.GetBookingOwnersDraftAsync(booking.ID, contact.ID);
                            var bookingOwnerPhones = await db.BookingOwnerPhones.Where(o => o.BookingOwnerID == owner.ID).ToListAsync();
                            var bookingOwnerEmails = await db.BookingOwnerEmails.Where(o => o.BookingOwnerID == owner.ID).ToListAsync();
                            var bookingOwnerAddress = await db.BookingOwnerAddresses.Include(o => o.ContactAddressType).Where(o => o.BookingOwnerID == owner.ID).ToListAsync();
                            bookingOwner.ContactPhones = new List<BookingOwnerPhoneDTO>();
                            bookingOwner.ContactPhones = bookingOwnerPhones.Select(o => BookingOwnerPhoneDTO.CreateFromModel(o)).ToList();
                            bookingOwner.ContactEmails = new List<BookingOwnerEmailDTO>();
                            bookingOwner.ContactEmails = bookingOwnerEmails.Select(o => BookingOwnerEmailDTO.CreateFromModel(o)).ToList();
                            bookingOwner.BookingOwnerAddresses = new List<BookingOwnerAddressDTO>();
                            bookingOwner.BookingOwnerAddresses = bookingOwnerAddress.Select(o => BookingOwnerAddressDTO.CreateFromModel(o, db)).ToList();

                            var result = await service.EditBookingOwnerAsync(owner.ID, bookingOwner);

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
        public async void ReOrderBookingOwnerAsync()
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
                            var contact2 = await db.Contacts.OrderByDescending(o => o.Updated).FirstAsync();
                            var booking = new Booking()
                            {
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };
                            await db.Bookings.AddAsync(booking);
                            await db.SaveChangesAsync();

                            var owner = new BookingOwner()
                            {
                                ContactTypeMasterCenterID = contact.ContactTypeMasterCenterID,
                                ContactTitleTHMasterCenterID = contact.ContactTitleTHMasterCenterID,
                                ContactTitleENMasterCenterID = contact.ContactTitleENMasterCenterID,
                                NationalMasterCenterID = contact.NationalMasterCenterID,
                                GenderMasterCenterID = contact.GenderMasterCenterID,
                                IsThaiNationality = contact.IsThaiNationality,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact.ContactNo,
                                BirthDate = contact.BirthDate,
                                BookingID = booking.ID,
                                FromContactID = contact.ID,
                                IsMainOwner = true,
                                Order = 1
                            };

                            var owner2 = new BookingOwner()
                            {
                                ContactTypeMasterCenterID = contact2.ContactTypeMasterCenterID,
                                ContactTitleTHMasterCenterID = contact2.ContactTitleTHMasterCenterID,
                                ContactTitleENMasterCenterID = contact2.ContactTitleENMasterCenterID,
                                NationalMasterCenterID = contact2.NationalMasterCenterID,
                                GenderMasterCenterID = contact2.GenderMasterCenterID,
                                IsThaiNationality = contact2.IsThaiNationality,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact2.ContactNo,
                                BirthDate = contact2.BirthDate,
                                BookingID = booking.ID,
                                FromContactID = contact2.ID,
                                IsMainOwner = false,
                                Order = 2
                            };

                            await db.BookingOwners.AddAsync(owner);
                            await db.BookingOwners.AddAsync(owner2);
                            await db.SaveChangesAsync();

                            var ownerDTO = new BookingOwnerDTO()
                            {
                                Id = owner2.ID,
                                Order = 1,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact2.ContactNo,
                                BirthDate = contact2.BirthDate,
                                FromContactID = contact2.ID,
                                IsMainOwner = false,
                            };
                            // Act
                            var service = new BookingOwnerService(db);

                            var result = await service.ReOrderBookingOwnerAsync(owner2.ID, ownerDTO);

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
        public async void SetMainBookingOwnerAsync()
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
                            var contact2 = await db.Contacts.OrderByDescending(o => o.Updated).FirstAsync();
                            var booking = new Booking()
                            {
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };
                            await db.Bookings.AddAsync(booking);
                            await db.SaveChangesAsync();

                            var owner = new BookingOwner()
                            {
                                ContactTypeMasterCenterID = contact.ContactTypeMasterCenterID,
                                ContactTitleTHMasterCenterID = contact.ContactTitleTHMasterCenterID,
                                ContactTitleENMasterCenterID = contact.ContactTitleENMasterCenterID,
                                NationalMasterCenterID = contact.NationalMasterCenterID,
                                GenderMasterCenterID = contact.GenderMasterCenterID,
                                IsThaiNationality = contact.IsThaiNationality,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact.ContactNo,
                                BirthDate = contact.BirthDate,
                                BookingID = booking.ID,
                                FromContactID = contact.ID,
                                IsMainOwner = true,
                                Order = 1
                            };

                            var owner2 = new BookingOwner()
                            {
                                ContactTypeMasterCenterID = contact2.ContactTypeMasterCenterID,
                                ContactTitleTHMasterCenterID = contact2.ContactTitleTHMasterCenterID,
                                ContactTitleENMasterCenterID = contact2.ContactTitleENMasterCenterID,
                                NationalMasterCenterID = contact2.NationalMasterCenterID,
                                GenderMasterCenterID = contact2.GenderMasterCenterID,
                                IsThaiNationality = contact2.IsThaiNationality,
                                MiddleNameTH = "owner",
                                MiddleNameEN = "owner",
                                Nickname = "owner",
                                FirstNameEN = "owner",
                                LastNameEN = "owner",
                                ContactFirstName = null,
                                ContactLastname = null,
                                TitleExtEN = "owner",
                                TitleExtTH = null,
                                ContactNo = contact2.ContactNo,
                                BirthDate = contact2.BirthDate,
                                BookingID = booking.ID,
                                FromContactID = contact2.ID,
                                IsMainOwner = false,
                                Order = 2
                            };

                            await db.BookingOwners.AddAsync(owner);
                            await db.BookingOwners.AddAsync(owner2);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new BookingOwnerService(db);

                            var result = await service.SetMainBookingOwnerAsync(owner2.ID);

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
