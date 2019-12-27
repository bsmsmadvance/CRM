using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTM = Database.Models.CTM;

namespace Customer.Services.ContactServices
{
    public interface IContactService
    {
        Task<ContactPaging> GetContactListAsync(ContactFilter filter, PageParam pageParam, ContactListSortByParam sortByParam);
        Task<ContactDTO> GetContactAsync(Guid id);
        Task<ContactSimilarPopupDTO> GetContactSimilarAsync(ContactDTO input);
        Task<ContactDTO> CreateContactAsync(ContactDTO input, Guid? similarContactID = null, Guid? fromVisitorID = null);
        Task<ContactDTO> UpdateContactAsync(Guid id, ContactDTO input);
        Task DeleteContactAsync(Guid id);
        Task<AddressDTO> GetContactAddressListAsync(Guid contactId);
        Task<ContactAddressDTO> GetContactAddressAsync(Guid contactId, Guid id);
        Task<ContactAddressDTO> CreateContactAddressAsync(Guid contactId, ContactAddressDTO input);
        Task<ContactAddressDTO> UpdateContactAddressAsync(Guid contactId, Guid id, ContactAddressDTO input);
        Task DeleteContactAddressAsync(Guid id);
    }
}
