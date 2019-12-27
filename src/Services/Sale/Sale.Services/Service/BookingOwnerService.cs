using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using Database.Models.Helpers;

namespace Sale.Services.Service
{
    public class BookingOwnerService : IBookingOwnerService
    {
        private readonly DatabaseContext DB;

        public BookingOwnerService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<List<BookingOwnerDTO>> GetBookingOwnersAsync(Guid bookingID)
        {
            var model = await DB.BookingOwners
            .Include(o => o.ContactType)
            .Include(o => o.ContactTitleTH)
            .Include(o => o.ContactTitleEN)
            .Include(o => o.National)
            .Include(o => o.Gender)
            .Include(o => o.Booking)
            .Where(o => o.BookingID == bookingID)
            .OrderBy(o => o.Order).ToListAsync();

            var result = model.Select(o => BookingOwnerDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();
            return result;
        }

        public async Task<BookingOwnerDTO> GetBookingOwnersDraftAsync(Guid bookingID, Guid contactID)
        {
            var contact = await DB.Contacts.Where(o => o.ID == contactID).FirstAsync();
            var booking = BookingOwner.CreateFromContact(contact);
            booking.BookingID = bookingID;
            var result = await BookingOwnerDTO.CreateFromModelDraftAsync(booking, DB);
            return result;
        }

        public async Task<BookingOwnerDTO> CreateBookingOwnerAsync(Guid bookingID, BookingOwnerDTO input)
        {
            await input.ValidateAsync(DB);

            var model = new BookingOwner();
            input.ToModel(ref model);
            model.BookingID = bookingID;
            model.IsAgreementOwner = input.IsAgreementOwner;
            if (input.National != null)
            {
                var nationID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                model.IsThaiNationality = (input.National.Id == nationID) ? true : false;
            }
            else
            {
                model.IsThaiNationality = false;
            }

            #region Phone
            List<BookingOwnerPhone> phones = new List<BookingOwnerPhone>();
            foreach (var phone in input.ContactPhones)
            {
                var phoneModel = new BookingOwnerPhone()
                {
                    BookingOwnerID = model.ID,
                    IsMain = phone.IsMain,
                    PhoneNumber = phone.PhoneNumber,
                    PhoneNumberExt = phone.PhoneNumberExt,
                    PhoneTypeMasterCenterID = phone.PhoneType?.Id,
                    CountryCode = phone.CountryCode,
                    FromContactPhoneID = phone.FromContactPhoneID
                };
                phones.Add(phoneModel);
            }

            if (phones.Count > 0)
            {
                var isMain = phones.Where(o => o.IsMain == true).Any();
                if (isMain == false)
                {
                    phones.First(o => o.IsMain = true);
                }

                await DB.BookingOwnerPhones.AddRangeAsync(phones);
            }

            #endregion

            #region Email
            List<BookingOwnerEmail> emails = new List<BookingOwnerEmail>();
            foreach (var email in input.ContactEmails)
            {
                var emailModel = new BookingOwnerEmail()
                {
                    BookingOwnerID = model.ID,
                    IsMain = email.IsMain,
                    Email = email.Email,
                    FromContactEmailID = email.FromContactEmailID
                };
                emails.Add(emailModel);
            }

            if (emails.Count > 0)
            {
                var isMain = emails.Where(o => o.IsMain == true).Any();
                if (isMain == false)
                {
                    emails.First(o => o.IsMain = true);
                }

                await DB.BookingOwnerEmails.AddRangeAsync(emails);
            }
            #endregion

            #region Address
            List<BookingOwnerAddress> addresses = new List<BookingOwnerAddress>();
            foreach (var address in input.BookingOwnerAddresses)
            {
                // ต้องเพิ่ม validate require address
                await address.ValidateAsync(DB);

                var addressModel = new BookingOwnerAddress();
                address.ToModel(ref addressModel);
                addressModel.BookingOwnerID = model.ID;
                addresses.Add(addressModel);
            }

            if (addresses.Count > 0)
                await DB.BookingOwnerAddresses.AddRangeAsync(addresses);
            #endregion

            if (input.IsMainOwner)
            {
                var mainOwner = await DB.BookingOwners.Where(o => o.BookingID == bookingID && o.IsMainOwner == true && o.ID != model.ID).FirstOrDefaultAsync();
                if (mainOwner != null)
                {
                    mainOwner.IsMainOwner = false;
                    DB.Entry(mainOwner).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
            else
            {
                var isMainOwner = await DB.BookingOwners.Where(o => o.BookingID == bookingID && o.IsMainOwner == true).AnyAsync();
                if (!isMainOwner)
                {
                    model.IsMainOwner = true;
                }
            }

            var lastOrder = await DB.BookingOwners.Where(o => o.BookingID == bookingID).Select(o => o.Order).FirstOrDefaultAsync();
            model.Order = lastOrder + 1;
            await DB.BookingOwners.AddAsync(model);

            await DB.SaveChangesAsync();

            if (input.FromContactID != null)
            {
                var contactModel = await DB.Contacts.Where(o => o.ID == input.FromContactID).FirstAsync();
                model.ContactNo = contactModel.ContactNo;
                model.IsVIP = contactModel.IsVIP;
                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();

                var sync = new SyncContactOwner(DB);
                await sync.SyncOwnerAsync(contactModel, model, typeof(BookingOwner));
            }
            else
            {
                var bookingModel = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
                var contact = new Contact();
                input.ToContactModel(ref contact);

                model.FromContactID = contact.ID;
                model.IsVIP = false;
                var keyCode = string.Empty;
                var contactTypeID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType && o.Key == "0").Select(o => o.ID).FirstAsync();
                keyCode = (model.ContactTypeMasterCenterID == contactTypeID) ? keyCode + "1" : keyCode + "2";
                keyCode = (model.IsThaiNationality) ? keyCode + "0" : keyCode + "1";

                string year = Convert.ToString(DateTime.Today.Year);
                var runningKey = keyCode + year[2] + year[3];
                var runningNumber = await DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "CTM.Contact").FirstOrDefaultAsync();
                if (runningNumber == null)
                {
                    var runningModel = new Database.Models.MST.RunningNumberCounter()
                    {
                        Key = runningKey,
                        Type = "CTM.Contact",
                        Count = 1
                    };

                    await DB.RunningNumberCounters.AddAsync(runningModel);
                    model.ContactNo = runningKey + runningModel.Count.ToString("00000");
                    contact.ContactNo = runningKey + runningModel.Count.ToString("00000");
                }
                else
                {
                    runningNumber.Count = runningNumber.Count + 1;
                    model.ContactNo = runningKey + runningNumber.Count.ToString("00000");
                    contact.ContactNo = runningKey + runningNumber.Count.ToString("00000");
                    DB.Entry(runningNumber).State = EntityState.Modified;
                }

                await DB.Contacts.AddAsync(contact);
                await DB.SaveChangesAsync();

                #region Phone
                List<ContactPhone> contactPhones = new List<ContactPhone>();
                foreach (var phone in phones)
                {
                    var phoneModel = new ContactPhone()
                    {
                        ContactID = contact.ID,
                        IsMain = phone.IsMain,
                        PhoneNumber = phone.PhoneNumber,
                        PhoneNumberExt = phone.PhoneNumberExt,
                        PhoneTypeMasterCenterID = phone.PhoneTypeMasterCenterID,
                        CountryCode = phone.CountryCode
                    };
                    contactPhones.Add(phoneModel);

                    var bookingOwnerPhoneModel = await DB.BookingOwnerPhones.Where(o => o.ID == phone.ID).FirstAsync();
                    bookingOwnerPhoneModel.FromContactPhoneID = phoneModel.ID;
                    DB.Entry(bookingOwnerPhoneModel).State = EntityState.Modified;
                }

                if (contactPhones.Count > 0)
                {
                    var isMain = contactPhones.Where(o => o.IsMain == true).Any();
                    if (isMain == false)
                    {
                        contactPhones.First(o => o.IsMain = true);
                    }

                    await DB.ContactPhones.AddRangeAsync(contactPhones);
                }
                #endregion

                #region Email
                List<ContactEmail> contactEmails = new List<ContactEmail>();
                foreach (var email in emails)
                {
                    var emailModel = new ContactEmail()
                    {
                        ContactID = contact.ID,
                        IsMain = email.IsMain,
                        Email = email.Email
                    };
                    contactEmails.Add(emailModel);

                    var bookingOwnerEmailModel = await DB.BookingOwnerEmails.Where(o => o.ID == email.ID).FirstAsync();
                    bookingOwnerEmailModel.FromContactEmailID = emailModel.ID;
                    DB.Entry(bookingOwnerEmailModel).State = EntityState.Modified;
                }

                if (contactEmails.Count > 0)
                {
                    var isMain = contactEmails.Where(o => o.IsMain == true).Any();
                    if (isMain == false)
                    {
                        contactEmails.First(o => o.IsMain = true);
                    }

                    await DB.ContactEmails.AddRangeAsync(contactEmails);
                }
                #endregion

                #region Address
                foreach (var address in addresses)
                {
                    var addressMasterCenterId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == "0").Select(o => o.ID).FirstAsync();
                    var contactAddressModel = new ContactAddress();
                    contactAddressModel.HouseNoTH = address.HouseNoTH;
                    contactAddressModel.MooTH = address.MooTH;
                    contactAddressModel.VillageTH = address.VillageTH;
                    contactAddressModel.SoiTH = address.SoiTH;
                    contactAddressModel.RoadTH = address.RoadTH;
                    contactAddressModel.CountryID = address.CountryID;
                    contactAddressModel.ProvinceID = address.ProvinceID;
                    contactAddressModel.DistrictID = address.DistrictID;
                    contactAddressModel.SubDistrictID = address.SubDistrictID;
                    contactAddressModel.HouseNoEN = address.HouseNoEN;
                    contactAddressModel.MooEN = address.MooEN;
                    contactAddressModel.VillageEN = address.VillageEN;
                    contactAddressModel.SoiEN = address.SoiEN;
                    contactAddressModel.RoadEN = address.RoadEN;
                    contactAddressModel.PostalCode = address.PostalCode;
                    contactAddressModel.ContactAddressTypeMasterCenterID = address.ContactAddressTypeMasterCenterID;
                    contactAddressModel.ForeignSubDistrict = address.ForeignSubDistrict;
                    contactAddressModel.ForeignDistrict = address.ForeignDistrict;
                    contactAddressModel.ForeignProvince = address.ForeignProvince;
                    contactAddressModel.ContactID = contact.ID;

                    await DB.ContactAddresses.AddAsync(contactAddressModel);

                    if (address.ContactAddressTypeMasterCenterID == addressMasterCenterId)
                    {
                        var addressProject = new ContactAddressProject()
                        {
                            ContactAddressID = contactAddressModel.ID,
                            ProjectID = bookingModel.ProjectID
                        };

                        await DB.ContactAddressProjects.AddAsync(addressProject);
                    }

                    var bookingOwnerAdressModel = await DB.BookingOwnerAddresses.Where(o => o.ID == address.ID).FirstAsync();
                    bookingOwnerAdressModel.FromContactAddressID = contactAddressModel.ID;
                    DB.Entry(bookingOwnerAdressModel).State = EntityState.Modified;
                }

                await DB.SaveChangesAsync();
                #endregion
            }

            var bookingOwnerModel = await DB.BookingOwners
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.National)
                .Include(o => o.Gender)
                .Include(o => o.Booking)
                .Where(o => o.ID == model.ID).FirstOrDefaultAsync();

            var result = await BookingOwnerDTO.CreateFromModelAsync(bookingOwnerModel, DB);
            return result;
        }

        public async Task DeleteBookingOwnerAsync(Guid bookingOwnerID, string reason)
        {
            var model = await DB.BookingOwners.Where(o => o.ID == bookingOwnerID).FirstAsync();
            model.IsDeleted = true;
            model.DeleteReason = reason;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        public async Task<BookingOwnerDTO> EditBookingOwnerAsync(Guid bookingOwnerID, BookingOwnerDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.BookingOwners.Where(o => o.ID == bookingOwnerID).FirstAsync();
            input.ToModel(ref model);

            if (input.National != null)
            {
                var nationID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                model.IsThaiNationality = (input.National.Id == nationID) ? true : false;
            }
            else
            {
                model.IsThaiNationality = false;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Email
            var emailModel = await DB.BookingOwnerEmails.Where(e => e.BookingOwnerID == bookingOwnerID).ToListAsync();
            List<BookingOwnerEmail> emailAddModel = new List<BookingOwnerEmail>();
            List<BookingOwnerEmail> emailUpdateModel = new List<BookingOwnerEmail>();
            List<BookingOwnerEmail> emailDeleteModel = new List<BookingOwnerEmail>();

            foreach (var emailInput in input.ContactEmails)
            {
                if (emailModel.Any(e => e.ID == emailInput.Id))
                {
                    var emailItemModel = await DB.BookingOwnerEmails.Where(e => e.BookingOwnerID == bookingOwnerID && e.ID == emailInput.Id).FirstAsync();
                    emailItemModel.Email = emailInput.Email;
                    emailItemModel.IsMain = emailInput.IsMain;
                    emailUpdateModel.Add(emailItemModel);
                }
                else
                {
                    emailAddModel.Add(new BookingOwnerEmail
                    {
                        BookingOwnerID = model.ID,
                        IsMain = emailInput.IsMain,
                        Email = emailInput.Email,
                        FromContactEmailID = emailInput.FromContactEmailID
                    });
                }
            }
            foreach (var emailItem in emailModel)
            {
                var existed = input.ContactEmails.Where(e => e.Id == emailItem.ID).FirstOrDefault();
                if (existed == null)
                {
                    emailDeleteModel.Add(emailItem);
                }
            }

            if (emailAddModel.Count() > 0)
            {
                await DB.BookingOwnerEmails.AddRangeAsync(emailAddModel);
            }

            if (emailUpdateModel.Count() > 0)
            {
                foreach (var emailItem in emailUpdateModel)
                {
                    DB.Entry(emailItem).State = EntityState.Modified;
                }
            }

            if (emailDeleteModel.Count() > 0)
            {
                foreach (var emailItem in emailDeleteModel)
                {
                    emailItem.IsDeleted = true;
                    DB.Entry(emailItem).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Phone
            var phoneModel = await DB.BookingOwnerPhones.Where(p => p.BookingOwnerID == bookingOwnerID).ToListAsync();
            List<BookingOwnerPhone> phoneAddModel = new List<BookingOwnerPhone>();
            List<BookingOwnerPhone> phoneUpdateModel = new List<BookingOwnerPhone>();
            List<BookingOwnerPhone> phoneDeleteModel = new List<BookingOwnerPhone>();

            foreach (var phoneInput in input.ContactPhones)
            {
                if (phoneModel.Any(e => e.ID == phoneInput.Id))
                {
                    var phoneItemModel = await DB.BookingOwnerPhones.Where(o => o.BookingOwnerID == bookingOwnerID && o.ID == phoneInput.Id).FirstAsync();
                    phoneItemModel.PhoneTypeMasterCenterID = phoneInput.PhoneType?.Id;
                    phoneItemModel.PhoneNumber = phoneInput.PhoneNumber;
                    phoneItemModel.PhoneNumberExt = phoneInput.PhoneNumberExt;
                    phoneItemModel.IsMain = phoneInput.IsMain;
                    phoneItemModel.CountryCode = phoneInput.CountryCode;
                    phoneUpdateModel.Add(phoneItemModel);
                }
                else
                {
                    phoneAddModel.Add(new BookingOwnerPhone
                    {
                        BookingOwnerID = model.ID,
                        IsMain = phoneInput.IsMain,
                        PhoneNumber = phoneInput.PhoneNumber,
                        PhoneNumberExt = phoneInput.PhoneNumberExt,
                        PhoneTypeMasterCenterID = phoneInput.PhoneType?.Id,
                        CountryCode = phoneInput.CountryCode,
                        FromContactPhoneID = phoneInput.FromContactPhoneID
                    });
                }
            }
            foreach (var phoneItem in phoneModel)
            {
                var existed = input.ContactPhones.Where(p => p.Id == phoneItem.ID).FirstOrDefault();
                if (existed == null)
                {
                    phoneDeleteModel.Add(phoneItem);
                }
            }

            if (phoneAddModel.Count() > 0)
            {
                await DB.BookingOwnerPhones.AddRangeAsync(phoneAddModel);
            }

            if (phoneUpdateModel.Count() > 0)
            {
                foreach (var phoneItem in phoneUpdateModel)
                {
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
            }
            if (phoneDeleteModel.Count() > 0)
            {
                foreach (var phoneItem in phoneDeleteModel)
                {
                    phoneItem.IsDeleted = true;
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Address
            foreach (var address in input.BookingOwnerAddresses)
            {
                await address.ValidateAsync(DB);

                var addressModel = await DB.BookingOwnerAddresses.Where(o => o.ID == address.Id).FirstAsync();
                address.ToModel(ref addressModel);
                DB.Entry(addressModel).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
            #endregion

            var contactModel = await DB.Contacts.Where(o => o.ID == model.FromContactID).FirstAsync();
            var sync = new SyncContactOwner(DB);
            await sync.SyncOwnerAsync(contactModel, model, typeof(BookingOwner));

            var bookingOwnerModel = await DB.BookingOwners
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.National)
                .Include(o => o.Gender)
                .Include(o => o.Booking)
                .Where(o => o.ID == model.ID).FirstOrDefaultAsync();

            var result = await BookingOwnerDTO.CreateFromModelAsync(bookingOwnerModel, DB);
            return result;
        }

        public async Task<BookingOwnerDTO> ReOrderBookingOwnerAsync(Guid bookingOwnerID, BookingOwnerDTO input)
        {
            var model = await DB.BookingOwners.Where(o => o.ID == bookingOwnerID).FirstAsync();
            var oldOrder = model.Order;
            model.Order = input.Order.Value;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var isMore = (input.Order.Value > oldOrder) ? true : false;
            var allOwners = await DB.BookingOwners.Where(o => o.BookingID == model.BookingID && o.ID != model.ID).ToListAsync();
            if (allOwners.Count > 0)
            {
                if (isMore)
                {
                    var owners = allOwners.Where(o => o.Order > oldOrder).ToList();
                    foreach (var owner in owners)
                    {
                        if (owner.Order <= model.Order)
                        {
                            owner.Order = owner.Order - 1;
                            DB.Entry(owner).State = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    var owners = allOwners.Where(o => o.Order < oldOrder).ToList();
                    foreach (var owner in owners)
                    {
                        if (owner.Order >= model.Order)
                        {
                            owner.Order = owner.Order + 1;
                            DB.Entry(owner).State = EntityState.Modified;
                        }
                    }
                }
            }

            await DB.SaveChangesAsync();

            var bookingModel = await DB.BookingOwners
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.National)
                .Include(o => o.Gender)
                .Include(o => o.Booking)
                .Where(o => o.ID == model.ID).FirstOrDefaultAsync();

            var result = await BookingOwnerDTO.CreateFromModelAsync(bookingModel, DB);
            return result;
        }

        public async Task<BookingOwnerDTO> SetMainBookingOwnerAsync(Guid bookingOwnerID)
        {
            var model = await DB.BookingOwners.Where(o => o.ID == bookingOwnerID).FirstAsync();
            var oldOrder = model.Order;
            model.Order = 1;
            model.IsMainOwner = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Order
            var isMore = (model.Order > oldOrder) ? true : false;
            var allOwners = await DB.BookingOwners.Where(o => o.BookingID == model.BookingID && o.ID != model.ID).ToListAsync();
            if (allOwners.Count > 0)
            {
                if (isMore)
                {
                    var owners = allOwners.Where(o => o.Order > oldOrder).ToList();
                    foreach (var owner in owners)
                    {
                        if (owner.Order <= model.Order)
                        {
                            owner.Order = owner.Order - 1;
                            DB.Entry(owner).State = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    var owners = allOwners.Where(o => o.Order < oldOrder).ToList();
                    foreach (var owner in owners)
                    {
                        if (owner.Order >= model.Order)
                        {
                            owner.Order = owner.Order + 1;
                            DB.Entry(owner).State = EntityState.Modified;
                        }
                    }
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            var mainOwner = await DB.BookingOwners.Where(o => o.ID != bookingOwnerID && o.IsMainOwner == true).FirstOrDefaultAsync();
            if (mainOwner != null)
            {
                mainOwner.IsMainOwner = false;
                DB.Entry(mainOwner).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            var bookingModel = await DB.BookingOwners
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.National)
                .Include(o => o.Gender)
                .Include(o => o.Booking)
                .Where(o => o.ID == model.ID).FirstOrDefaultAsync();

            var result = await BookingOwnerDTO.CreateFromModelAsync(bookingModel, DB);
            return result;
        }

        public async Task<List<BookingOwnerDropdownDTO>> GetBookingOwnerDropdownAsync(Guid bookingID)
        {
            var models = await DB.BookingOwners.Where(o => o.BookingID == bookingID).ToListAsync();
            var result = models.Select(o => BookingOwnerDropdownDTO.CreateFromModel(o)).ToList();
            return result;
        }
    }
}
