using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using Database.Models;
using Database.Models.Helpers;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using CTM = Database.Models.CTM;

namespace Customer.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly DatabaseContext DB;

        public ContactService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ContactPaging> GetContactListAsync(ContactFilter filter, PageParam pageParam, ContactListSortByParam sortByParam)
        {
            var query = from contact in DB.Contacts
                        join phone in DB.ContactPhones on contact.ID equals phone.ContactID into g
                        from t in g.Where(o => o.IsMain == true).DefaultIfEmpty()
                        join opp in DB.Opportunities on contact.LastOpportunityID equals opp.ID into op
                        from o in op.DefaultIfEmpty()
                        select new ContactQueryResult
                        {
                            Contact = contact,
                            ContactPhone = t,
                            Opportunity = o
                        };

            #region Filter
            if (!string.IsNullOrEmpty(filter.ContactNo))
                query = query.Where(q => q.Contact.ContactNo.Contains(filter.ContactNo));

            if (!string.IsNullOrEmpty(filter.FirstNameTH))
                query = query.Where(q => q.Contact.FirstNameTH.Contains(filter.FirstNameTH));

            if (!string.IsNullOrEmpty(filter.LastNameTH))
                query = query.Where(q => q.Contact.LastNameTH.Contains(filter.LastNameTH));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(q => q.ContactPhone.PhoneNumber.Contains(filter.PhoneNumber));

            if (!string.IsNullOrEmpty(filter.CitizenIdentityNo))
                query = query.Where(q => q.Contact.CitizenIdentityNo.Contains(filter.CitizenIdentityNo));

            if (filter.CreatedDateFrom != null && filter.CreatedDateTo != null)
                query = query.Where(q => q.Contact.Created >= filter.CreatedDateFrom && q.Contact.Created <= filter.CreatedDateTo);

            if (filter.UpdatedDateFrom != null && filter.UpdatedDateTo != null)
                query = query.Where(q => q.Contact.Updated >= filter.UpdatedDateFrom && q.Contact.Updated <= filter.UpdatedDateTo);
            #endregion

            ContactListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<ContactQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => ContactListDTO.CreateFromQueryResult(o)).ToList();

            return new ContactPaging()
            {
                PageOutput = pageOutput,
                Contacts = result
            };
        }

        public async Task<ContactDTO> GetContactAsync(Guid id)
        {
            var model = await DB.Contacts
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.Gender)
                .Include(o => o.National)
                .Where(c => c.ID == id).FirstOrDefaultAsync();
            if (model == null)
                return null;

            var result = await ContactDTO.CreateFromModelAsync(model, DB);

            return result;
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO input, Guid? similarContactID = null, Guid? fromVisitorID = null)
        {
            var contactID = new Guid();
            if (similarContactID == null)
            {
                await input.ValidateAsync(DB);

                #region Validate
                ValidateException ex = new ValidateException();

                if (!string.IsNullOrEmpty(input.CitizenIdentityNo))
                {
                    var isExitsCustomer = await DB.Contacts.Where(o => o.CitizenIdentityNo == input.CitizenIdentityNo).AnyAsync();
                    if (isExitsCustomer)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                        string desc = typeof(ContactDTO).GetProperty(nameof(ContactDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                if (ex.HasError)
                {
                    throw ex;
                }
                #endregion

                var model = new CTM.Contact();
                input.ToModel(ref model);

                model.Order = 0;

                if (input.IsVIP != null)
                {
                    model.IsVIP = input.IsVIP.Value;
                }
                else
                {
                    model.IsVIP = false;
                }

                if (input.National != null)
                {
                    var nationID = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).First();
                    model.IsThaiNationality = (input.National.Id == nationID) ? true : false;
                }
                else
                {
                    model.IsThaiNationality = false;
                }

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
                }
                else
                {
                    runningNumber.Count = runningNumber.Count + 1;
                    model.ContactNo = runningKey + runningNumber.Count.ToString("00000");
                    DB.Entry(runningNumber).State = EntityState.Modified;
                }

                await DB.Contacts.AddAsync(model);
                await DB.SaveChangesAsync();

                #region Phone
                List<CTM.ContactPhone> phoneList = new List<CTM.ContactPhone>();
                foreach (var phone in input.ContactPhones)
                {
                    var phoneModel = new CTM.ContactPhone()
                    {
                        ContactID = model.ID,
                        IsMain = phone.IsMain,
                        PhoneNumber = phone.PhoneNumber,
                        PhoneNumberExt = phone.PhoneNumberExt,
                        PhoneTypeMasterCenterID = phone.PhoneType?.Id,
                        CountryCode = phone.CountryCode
                    };
                    phoneList.Add(phoneModel);
                }

                if (phoneList.Count > 0)
                {
                    var isMain = phoneList.Where(o => o.IsMain == true).Any();
                    if (isMain == false)
                    {
                        phoneList.First(o => o.IsMain = true);
                    }

                    await DB.ContactPhones.AddRangeAsync(phoneList);
                    await DB.SaveChangesAsync();
                }
                #endregion

                #region Email
                List<CTM.ContactEmail> emailList = new List<CTM.ContactEmail>();
                foreach (var email in input.ContactEmails)
                {
                    if (!string.IsNullOrEmpty(email.Email))
                    {
                        var emailModel = new CTM.ContactEmail()
                        {
                            ContactID = model.ID,
                            IsMain = email.IsMain,
                            Email = email.Email
                        };
                        emailList.Add(emailModel);
                    }
                }

                if (emailList.Count > 0)
                {
                    var isMain = emailList.Where(o => o.IsMain == true).Any();
                    if (isMain == false)
                    {
                        emailList.First(o => o.IsMain = true);
                    }

                    await DB.ContactEmails.AddRangeAsync(emailList);
                    await DB.SaveChangesAsync();
                }
                #endregion

                contactID = model.ID;
            }
            else
            {
                var similarModel = await DB.Contacts.Where(o => o.ID == similarContactID).FirstAsync();
                contactID = similarModel.ID;
            }

            if (fromVisitorID != null)
            {
                var visitor = await DB.Visitors.Where(o => o.ID == fromVisitorID).FirstAsync();
                visitor.ContactID = contactID;
                DB.Entry(visitor).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            var result = await this.GetContactAsync(contactID);

            return result;
        }

        public async Task<ContactDTO> UpdateContactAsync(Guid id, ContactDTO input)
        {
            await input.ValidateAsync(DB);

            #region Validate
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(input.CitizenIdentityNo))
            {
                var isExitsCustomer = await DB.Contacts.Where(o => o.CitizenIdentityNo == input.CitizenIdentityNo && o.ID != id).AnyAsync();
                if (isExitsCustomer)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                    string desc = typeof(ContactDTO).GetProperty(nameof(ContactDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
            #endregion

            var model = await DB.Contacts.Where(o => o.ID == id).FirstAsync();
            if (input.IsVIP != null)
            {
                model.IsVIP = input.IsVIP.Value;
            }
            else
            {
                model.IsVIP = false;
            }
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Email
            var emailModel = await DB.ContactEmails.Where(e => e.ContactID == id).ToListAsync();
            List<CTM.ContactEmail> emailAddModel = new List<CTM.ContactEmail>();
            List<CTM.ContactEmail> emailUpdateModel = new List<CTM.ContactEmail>();
            List<CTM.ContactEmail> emailDeleteModel = new List<CTM.ContactEmail>();

            foreach (var emailInput in input.ContactEmails)
            {
                if (emailModel.Any(e => e.ID == emailInput.Id))
                {
                    var emailItemModel = await DB.ContactEmails.Where(e => e.ContactID == id && e.ID == emailInput.Id).FirstAsync();
                    emailItemModel.Email = emailInput.Email;
                    emailItemModel.IsMain = emailInput.IsMain;
                    emailUpdateModel.Add(emailItemModel);
                }
                else
                {
                    if (!string.IsNullOrEmpty(emailInput.Email))
                    {
                        emailAddModel.Add(new CTM.ContactEmail
                        {
                            ContactID = id,
                            Email = emailInput.Email,
                            IsMain = emailInput.IsMain
                        });
                    }
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
                await DB.ContactEmails.AddRangeAsync(emailAddModel);
                await DB.SaveChangesAsync();
            }
            if (emailUpdateModel.Count() > 0)
            {
                foreach (var emailItem in emailUpdateModel)
                {
                    DB.Entry(emailItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }
            if (emailDeleteModel.Count() > 0)
            {
                foreach (var emailItem in emailDeleteModel)
                {
                    emailItem.IsDeleted = true;
                    DB.Entry(emailItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }

            var isEmailMain = await DB.ContactEmails.Where(o => o.IsMain == true && o.ContactID == id).AnyAsync();
            if (isEmailMain == false)
            {
                var mainEmailModel = await DB.ContactEmails.Where(o => o.ContactID == id).FirstOrDefaultAsync();
                if (mainEmailModel != null)
                {
                    mainEmailModel.IsMain = true;
                    DB.Entry(mainEmailModel).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
            #endregion

            #region Phone
            var phoneModel = await DB.ContactPhones.Where(p => p.ContactID == id).ToListAsync();
            List<CTM.ContactPhone> phoneAddModel = new List<CTM.ContactPhone>();
            List<CTM.ContactPhone> phoneUpdateModel = new List<CTM.ContactPhone>();
            List<CTM.ContactPhone> phoneDeleteModel = new List<CTM.ContactPhone>();

            foreach (var phoneInput in input.ContactPhones)
            {
                if (phoneModel.Any(e => e.ID == phoneInput.Id))
                {
                    var phoneItemModel = await DB.ContactPhones.Where(o => o.ContactID == id && o.ID == phoneInput.Id).FirstAsync();
                    phoneItemModel.PhoneTypeMasterCenterID = phoneInput.PhoneType?.Id;
                    phoneItemModel.PhoneNumber = phoneInput.PhoneNumber;
                    phoneItemModel.PhoneNumberExt = phoneInput.PhoneNumberExt;
                    phoneItemModel.IsMain = phoneInput.IsMain;
                    phoneItemModel.CountryCode = phoneInput.CountryCode;
                    phoneUpdateModel.Add(phoneItemModel);
                }
                else
                {
                    phoneAddModel.Add(new CTM.ContactPhone
                    {
                        ContactID = id,
                        PhoneTypeMasterCenterID = phoneInput.PhoneType?.Id,
                        PhoneNumber = phoneInput.PhoneNumber,
                        PhoneNumberExt = phoneInput.PhoneNumberExt,
                        IsMain = phoneInput.IsMain,
                        CountryCode = phoneInput.CountryCode
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
                await DB.ContactPhones.AddRangeAsync(phoneAddModel);
                await DB.SaveChangesAsync();
            }
            if (phoneUpdateModel.Count() > 0)
            {
                foreach (var phoneItem in phoneUpdateModel)
                {
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }
            if (phoneDeleteModel.Count() > 0)
            {
                foreach (var phoneItem in phoneDeleteModel)
                {
                    phoneItem.IsDeleted = true;
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
                await DB.SaveChangesAsync();
            }

            var isphoneMain = await DB.ContactPhones.Where(o => o.IsMain == true && o.ContactID == id).AnyAsync();
            if (isphoneMain == false)
            {
                var mainPhoneModel = await DB.ContactPhones.Where(o => o.ContactID == id).FirstOrDefaultAsync();
                if (mainPhoneModel != null)
                {
                    mainPhoneModel.IsMain = true;
                    DB.Entry(mainPhoneModel).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
            #endregion

            #region Booking Owner
            var bookingOwners = await DB.BookingOwners.Where(o => o.FromContactID == id).ToListAsync();
            var sync = new SyncContactOwner(DB);
            foreach (var owner in bookingOwners)
            {

                await sync.SyncOwnerAsync(model, owner, typeof(CTM.Contact));
            }
            #endregion

            #region Agreement Owner
            var agreementOwners = await DB.AgreementOwners.Where(o => o.FromContactID == id).ToListAsync();
            foreach (var owner in agreementOwners)
            {
                await sync.SyncOwnerAsync(model, owner, typeof(CTM.Contact));
            }
            #endregion

            var data = await DB.Contacts
                .Include(o => o.ContactType)
                .Include(o => o.ContactTitleTH)
                .Include(o => o.ContactTitleEN)
                .Include(o => o.Gender)
                .Where(o => o.ID == model.ID)
                .FirstOrDefaultAsync();

            var result = await ContactDTO.CreateFromModelAsync(data, DB);

            return result;
        }

        public async Task DeleteContactAsync(Guid id)
        {
            var model = await DB.Contacts.Where(c => c.ID == id).FirstAsync();

            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;

            var emailModel = await DB.ContactEmails.Where(e => e.ContactID == id).ToListAsync();
            foreach (var email in emailModel)
            {
                email.IsDeleted = true;
                DB.Entry(email).State = EntityState.Modified;
            }

            var phoneModel = await DB.ContactPhones.Where(e => e.ContactID == id).ToListAsync();
            foreach (var phone in phoneModel)
            {
                phone.IsDeleted = true;
                DB.Entry(phone).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
        }

        public async Task<AddressDTO> GetContactAddressListAsync(Guid contactId)
        {
            var addressModel = await DB.ContactAddresses
                .Include(o => o.Country)
                .Include(o => o.Province)
                .Include(o => o.District)
                .Include(o => o.SubDistrict)
                .Include(o => o.ContactAddressType)
                .Where(a => a.ContactID == contactId).ToListAsync();

            var result = await AddressDTO.CreateFromModelAsync(addressModel, DB);

            return result;
        }

        public async Task<ContactAddressDTO> GetContactAddressAsync(Guid contactId, Guid id)
        {

            var model = await DB.ContactAddresses
                .Include(o => o.Country)
                .Include(o => o.Province)
                .Include(o => o.District)
                .Include(o => o.SubDistrict)
                .Include(o => o.ContactAddressType)
                .Where(o => o.ContactID == contactId && o.ID == id)
                .FirstOrDefaultAsync();
            var result = await ContactAddressDTO.CreateFromModelAsync(model, DB);
            return result;
        }

        public async Task<ContactAddressDTO> CreateContactAddressAsync(Guid contactId, ContactAddressDTO input)
        {
            await input.ValidateAsync(DB);

            #region Validate
            ValidateException ex = new ValidateException();

            var contactAddressMasterID = await DB.MasterCenters.Where(o => o.Key == "0" && o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).Select(o => o.ID).FirstAsync();
            if (input.ContactAddressType.Id == contactAddressMasterID)
            {
                if (input.Project != null)
                {
                    var isExitsProject = await DB.ContactAddressProjects
                        .Include(o => o.ContactAddress)
                        .Where(o => o.ContactAddress.ContactID == contactId && o.ProjectID == input.Project.Id).AnyAsync();

                    if (isExitsProject)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                        string desc = typeof(ContactAddressDTO).GetProperty(nameof(ContactAddressDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                else
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = typeof(ContactAddressDTO).GetProperty(nameof(ContactAddressDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (ex.HasError)
                {
                    throw ex;
                }
            }
            #endregion

            var model = new CTM.ContactAddress();
            input.ToModel(ref model);
            model.ContactID = contactId;
            model.ContactAddressTypeMasterCenterID = input.ContactAddressType.Id;

            await DB.ContactAddresses.AddAsync(model);

            if (input.Project != null && input.ContactAddressType.Id == contactAddressMasterID)
            {
                var addressProject = new CTM.ContactAddressProject()
                {
                    ContactAddressID = model.ID,
                    ProjectID = input.Project?.Id
                };
                await DB.ContactAddressProjects.AddAsync(addressProject);
            }

            await DB.SaveChangesAsync();

            var contactModel = await DB.ContactAddresses
                .Include(o => o.Country)
                .Include(o => o.Province)
                .Include(o => o.District)
                .Include(o => o.SubDistrict)
                .Include(o => o.ContactAddressType)
                .Where(o => o.ID == model.ID)
                .FirstOrDefaultAsync();
            var result = await ContactAddressDTO.CreateFromModelAsync(contactModel, DB);
            return result;
        }

        public async Task<ContactAddressDTO> UpdateContactAddressAsync(Guid contactId, Guid id, ContactAddressDTO input)
        {
            await input.ValidateAsync(DB);

            var model = await DB.ContactAddresses.Include(o => o.ContactAddressType).Where(c => c.ID == id).FirstAsync();
            var contactMasterID = await DB.MasterCenters.Where(o => o.Key == ContactAddressTypeKeys.Contact && o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).Select(o => o.ID).FirstAsync();
            var contactCitizenMasterID = await DB.MasterCenters.Where(o => o.Key == ContactAddressTypeKeys.Citizen && o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).Select(o => o.ID).FirstAsync();

            #region Validate
            ValidateException ex = new ValidateException();

            if (model.ContactAddressTypeMasterCenterID == contactMasterID)
            {
                if (input.Project != null)
                {
                    var isExitsProject = await DB.ContactAddressProjects
                        .Include(o => o.ContactAddress)
                        .Where(o => o.ContactAddress.ContactID == contactId && o.ProjectID == input.Project.Id && o.ContactAddressID != id).AnyAsync();

                    if (isExitsProject)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                        string desc = typeof(ContactAddressDTO).GetProperty(nameof(ContactAddressDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                else
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = typeof(ContactAddressDTO).GetProperty(nameof(ContactAddressDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (ex.HasError)
                {
                    throw ex;
                }
            }

            #endregion

            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            if (model.ContactAddressTypeMasterCenterID == contactMasterID)
            {
                if (input.Project != null)
                {
                    var projectModel = await DB.ContactAddressProjects.Where(o => o.ContactAddressID == model.ID).FirstOrDefaultAsync();
                    if (projectModel != null)
                    {
                        projectModel.ProjectID = input.Project.Id;
                        DB.Entry(projectModel).State = EntityState.Modified;
                    }
                    else
                    {
                        var addressProject = new CTM.ContactAddressProject()
                        {
                            ContactAddressID = model.ID,
                            ProjectID = input.Project?.Id
                        };
                        await DB.ContactAddressProjects.AddAsync(addressProject);
                    }
                }
                else
                {
                    var projectModel = await DB.ContactAddressProjects.Where(o => o.ContactAddressID == model.ID).FirstOrDefaultAsync();
                    if (projectModel != null)
                    {
                        projectModel.IsDeleted = true;
                        DB.Entry(projectModel).State = EntityState.Modified;
                    }
                }
            }
            await DB.SaveChangesAsync();

            #region Booking Owner
            var contact = await DB.Contacts.Where(o => o.ID == contactId).FirstAsync(); ;
            var bookingOwners = await DB.BookingOwners.Where(o => o.FromContactID == contactId).ToListAsync();
            var sync = new SyncContactOwner(DB);
            foreach (var owner in bookingOwners)
            {

                await sync.SyncOwnerAsync(contact, owner, typeof(CTM.Contact));
            }
            #endregion

            #region Agreement Owner
            var agreementOwners = await DB.AgreementOwners.Where(o => o.FromContactID == contactId).ToListAsync();
            foreach (var owner in agreementOwners)
            {
                await sync.SyncOwnerAsync(contact, owner, typeof(CTM.Contact));
            }
            #endregion

            var contactModel = await DB.ContactAddresses
                .Include(o => o.Country)
                .Include(o => o.Province)
                .Include(o => o.District)
                .Include(o => o.SubDistrict)
                .Include(o => o.ContactAddressType)
                .Where(o => o.ID == model.ID)
                .FirstOrDefaultAsync();
            var result = await ContactAddressDTO.CreateFromModelAsync(contactModel, DB);

            return result;
        }

        public async Task DeleteContactAddressAsync(Guid id)
        {
            var model = await DB.ContactAddresses.Where(c => c.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;

            var projectModel = await DB.ContactAddressProjects.Where(o => o.ContactAddressID == model.ID).FirstOrDefaultAsync();
            if (projectModel != null)
            {
                projectModel.IsDeleted = true;
                DB.Entry(projectModel).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
        }

        public async Task<ContactSimilarPopupDTO> GetContactSimilarAsync(ContactDTO input)
        {
            ContactSimilarPopupDTO result = new ContactSimilarPopupDTO();
            List<ContactSimilarDTO> contactSimilars = new List<ContactSimilarDTO>();

            var contactType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactType).ToListAsync();
            var individualId = contactType.Where(o => o.Key == ContactTypeKeys.Individual).Select(o => o.ID).First();
            var legalId = contactType.Where(o => o.Key == ContactTypeKeys.Legal).Select(o => o.ID).First();

            if (input.ContactType.Id == individualId)
            {
                #region Individual
                var finished = false;
                if (!string.IsNullOrEmpty(input.CitizenIdentityNo))
                {
                    var citizenModel = await DB.Contacts.Where(o => o.CitizenIdentityNo == input.CitizenIdentityNo && o.ContactTypeMasterCenterID == individualId).FirstOrDefaultAsync();
                    if (citizenModel != null)
                    {
                        var similar = await ContactSimilarDTO.CreateFromModelAsync(citizenModel, DB);
                        result.CanCreateNewContact = false;
                        contactSimilars.Add(similar);
                        finished = true;
                    }
                }

                if (!finished)
                {
                    var contactPhone = input.ContactPhones.Where(o => o.IsMain == true).FirstOrDefault();
                    if (contactPhone != null)
                    {
                        var phoneModel = await DB.ContactPhones
                            .Include(o => o.Contact)
                            .Where(o => o.PhoneNumber == contactPhone.PhoneNumber
                            && o.Contact.FirstNameTH == input.FirstNameTH
                            && o.Contact.LastNameTH == input.LastNameTH
                            && o.Contact.ContactTypeMasterCenterID == individualId
                            && o.IsMain == true)
                            .ToListAsync();

                        foreach (var phone in phoneModel)
                        {
                            var similar = await ContactSimilarDTO.CreateFromModelAsync(phone.Contact, DB);
                            result.CanCreateNewContact = false;
                            contactSimilars.Add(similar);
                            finished = true;
                        }
                    }
                }

                if (!finished)
                {
                    if (!string.IsNullOrEmpty(input.FirstNameTH))
                    {
                        var nameModels = await DB.Contacts.Where(o => o.FirstNameTH == input.FirstNameTH && o.LastNameTH == input.LastNameTH && o.ContactTypeMasterCenterID == individualId).ToListAsync();
                        foreach (var name in nameModels)
                        {
                            var similar = await ContactSimilarDTO.CreateFromModelAsync(name, DB);
                            result.CanCreateNewContact = true;
                            contactSimilars.Add(similar);
                            finished = true;
                        }
                    }
                }

                if (!finished)
                {
                    List<CTM.Contact> contacts = new List<CTM.Contact>();
                    var contactPhone = input.ContactPhones.Where(o => o.IsMain == true).FirstOrDefault();
                    if (contactPhone != null)
                    {
                        var phoneModels = await DB.ContactPhones
                            .Include(o => o.Contact)
                            .Where(o => o.PhoneNumber == contactPhone.PhoneNumber && o.Contact.ContactTypeMasterCenterID == individualId && o.IsMain == true)
                            .ToListAsync();

                        foreach (var phone in phoneModels)
                        {
                            contacts.Add(phone.Contact);
                        }

                        var namePhoneModels = await DB.ContactPhones
                            .Include(o => o.Contact)
                            .Where(o => o.PhoneNumber == contactPhone.PhoneNumber && o.Contact.FirstNameTH == input.FirstNameTH && o.Contact.ContactTypeMasterCenterID == individualId && o.IsMain == true)
                            .ToListAsync();

                        foreach (var phone in namePhoneModels)
                        {
                            contacts.Add(phone.Contact);
                        }

                        var lastNamePhoneModels = await DB.ContactPhones
                            .Include(o => o.Contact)
                            .Where(o => o.PhoneNumber == contactPhone.PhoneNumber && o.Contact.LastNameTH == input.LastNameTH && o.Contact.ContactTypeMasterCenterID == individualId && o.IsMain == true)
                            .ToListAsync();

                        foreach (var phone in lastNamePhoneModels)
                        {
                            contacts.Add(phone.Contact);
                        }
                    }

                    var listResult = contacts.Distinct().ToList();
                    foreach (var item in listResult)
                    {
                        var similar = await ContactSimilarDTO.CreateFromModelAsync(item, DB);
                        result.CanCreateNewContact = true;
                        contactSimilars.Add(similar);
                        finished = true;
                    }
                }

                #endregion
            }
            else if (input.ContactType.Id == legalId)
            {
                #region Legal
                var finished = false;
                if (!string.IsNullOrEmpty(input.TaxID))
                {
                    var citizenModel = await DB.Contacts.Where(o => o.TaxID == input.TaxID && o.ContactTypeMasterCenterID == legalId).FirstOrDefaultAsync();
                    if (citizenModel != null)
                    {
                        var similar = await ContactSimilarDTO.CreateFromModelAsync(citizenModel, DB);
                        result.CanCreateNewContact = false;
                        contactSimilars.Add(similar);
                        finished = true;
                    }
                }

                if (!finished)
                {
                    if (!string.IsNullOrEmpty(input.FirstNameTH))
                    {
                        var contact = await DB.Contacts.Where(o => o.FirstNameTH == input.FirstNameTH && o.ContactTypeMasterCenterID == legalId).FirstOrDefaultAsync();
                        if (contact != null)
                        {
                            var similar = await ContactSimilarDTO.CreateFromModelAsync(contact, DB);
                            result.CanCreateNewContact = false;
                            contactSimilars.Add(similar);
                            finished = true;
                        }
                    }
                }

                if (!finished)
                {
                    var contactPhone = input.ContactPhones.Where(o => o.IsMain == true).FirstOrDefault();
                    if (contactPhone != null)
                    {
                        var phoneModel = await DB.ContactPhones
                            .Include(o => o.Contact)
                            .Where(o => o.PhoneNumber == contactPhone.PhoneNumber && o.Contact.ContactTypeMasterCenterID == legalId && o.IsMain == true)
                            .ToListAsync();

                        foreach (var phone in phoneModel)
                        {
                            var similar = await ContactSimilarDTO.CreateFromModelAsync(phone.Contact, DB);
                            result.CanCreateNewContact = true;
                            contactSimilars.Add(similar);
                            finished = true;
                        }
                    }
                }
                #endregion
            }

            result.ContactSimilars = new List<ContactSimilarDTO>();
            if (contactSimilars.Count > 0)
                result.ContactSimilars.AddRange(contactSimilars);

            return result;
        }
    }
}
