using AutoFixture;
using Base.DTOs.MST;
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
    public class AgreementOwnerServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public async void GetAgreementOwnersAsync()
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
                            var agreement = new Agreement()
                            {
                                BookingID = booking.ID,
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };

                            var agreementOwner = AgreementOwner.CreateFromContact(contact);
                            agreementOwner.AgreementID = agreement.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.Agreements.AddAsync(agreement);
                            await db.AgreementOwners.AddAsync(agreementOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new AgreementOwnerService(db);
                            var result = await service.GetAgreementOwnersAsync(agreement.ID);
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
        public async void GetAgreementOwnerDropdownAsync()
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

                            var agreement = new Agreement()
                            {
                                BookingID = booking.ID,
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };

                            var agreementOwner = AgreementOwner.CreateFromContact(contact);
                            agreementOwner.AgreementID = agreement.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.Agreements.AddAsync(agreement);
                            await db.AgreementOwners.AddAsync(agreementOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new AgreementOwnerService(db);
                            var result = await service.GetAgreementOwnerDropdownAsync(booking.ID);
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
        public async void GetAgreementOwnersDraftAsync()
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

                        var agreement = new Agreement()
                        {
                            BookingID = booking.ID,
                            UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                        };

                        await db.Bookings.AddAsync(booking);
                        await db.Agreements.AddAsync(agreement);
                        await db.SaveChangesAsync();

                        // Act
                        var service = new AgreementOwnerService(db);
                        var results = await service.GetAgreementOwnersDraftAsync(agreement.ID, contact.ID);
                        tran.Rollback();
                    }
                });
            }
        }

        [Fact]
        public async void CreateAgreementOwnerAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var contact = await db.Contacts.FirstAsync(o => o.ID == new Guid("60634210-9F09-4698-97C2-00020C8A5E16"));
                        var agreement = await db.Agreements.OrderByDescending(o => o.ID == new Guid("832ACE49-E008-434B-977A-A30E9B2FF879")).FirstAsync();

                        var service = new AgreementOwnerService(db);
                        var agreementOwner = await service.GetAgreementOwnersDraftAsync(agreement.ID, contact.ID);
                        agreementOwner.IsMainOwner = true;
                        agreementOwner.National = new MasterCenterDropdownDTO();
                        agreementOwner.National.Id = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();

                        var result = await service.CreateAgreementOwnerAsync(agreement.ID, agreementOwner);
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
                            var agreement = new Agreement()
                            {
                                BookingID = booking.ID,
                                UnitID = await db.Units.Select(o => o.ID).FirstAsync()
                            };

                            var agreementOwner = AgreementOwner.CreateFromContact(contact);
                            agreementOwner.AgreementID = agreement.ID;

                            await db.Bookings.AddAsync(booking);
                            await db.Agreements.AddAsync(agreement);
                            await db.AgreementOwners.AddAsync(agreementOwner);
                            await db.SaveChangesAsync();

                            // Act
                            var service = new AgreementOwnerService(db);
                            await service.DeleteAgreementOwnerAsync(agreementOwner.ID, "ทดสอบ");
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
