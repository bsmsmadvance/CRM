using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Services.ContactServices;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace Customer.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IContactService ContactService;

        public ContactsController(DatabaseContext db, IContactService contactService)
        {
            this.DB = db;
            this.ContactService = contactService;
        }

        /// <summary>
        /// Get ข้อมูล List Contact
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>ContactListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ContactListDTO>))]
        public async Task<IActionResult> GetContactList([FromQuery]ContactFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ContactListSortByParam sortByParam)
        {
            try
            {
                var result = await ContactService.GetContactListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Contacts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContactDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ContactDTO))]
        public async Task<IActionResult> GetContact([FromRoute]Guid id)
        {
            try
            {
                var result = await ContactService.GetContactAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง Contact
        /// </summary>
        /// <param name="input"></param>
        /// <param name="similarContactID"></param>
        /// <param name="fromVisitorID"></param>
        /// <returns>ContactDTO</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ContactDTO))]
        public async Task<IActionResult> CreateContact([FromBody]ContactDTO input, [FromQuery]Guid? similarContactID = null, [FromQuery]Guid? fromVisitorID = null)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ContactService.CreateContactAsync(input, similarContactID, fromVisitorID);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไข Contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>ContactDTO</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ContactDTO))]
        public async Task<IActionResult> EditContact([FromRoute]Guid id, [FromBody]ContactDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ContactService.UpdateContactAsync(id, input);

                    if (result == null)
                    {
                        return NotFound();
                    }

                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบ Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status OK/Internal Error</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await ContactService.DeleteContactAsync(id);

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get ข้อมูลที่อยู่ทั้งหมด
        /// </summary>
        /// <returns>AddressDTO</returns>
        [HttpGet("{contactID}/Addresses")]
        [ProducesResponseType(200, Type = typeof(AddressDTO))]
        public async Task<IActionResult> GetContactAddressList([FromRoute]Guid contactID)
        {
            try
            {
                var result = await ContactService.GetContactAddressListAsync(contactID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูลที่อยู่
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <returns>ContactAddressDTO</returns>
        [HttpGet("{contactID}/Addresses/{id}")]
        [ProducesResponseType(200, Type = typeof(ContactAddressDTO))]
        public async Task<IActionResult> GetContactAddress([FromRoute]Guid contactID, [FromRoute]Guid id)
        {
            try
            {
                var result = await ContactService.GetContactAddressAsync(contactID, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// สร้างที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน)
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="input"></param>
        /// <returns>ContactAddressDTO</returns>
        [HttpPost("{contactID}/Addresses")]
        [ProducesResponseType(200, Type = typeof(ContactAddressDTO))]
        public async Task<IActionResult> CreateContactAddress([FromRoute]Guid contactID, [FromBody]ContactAddressDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ContactService.CreateContactAddressAsync(contactID, input);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// แก้ไขที่อยู่ (ติดต่อได้/ที่ทำงาน/ทะเบียนบ้าน/บัตรประชาชน)
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>ContactAddressDTO</returns>
        [HttpPut("{contactID}/Addresses/{id}")]
        [ProducesResponseType(200, Type = typeof(ContactAddressDTO))]
        public async Task<IActionResult> EditContactAddress([FromRoute]Guid contactID, [FromRoute]Guid id, [FromBody]ContactAddressDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ContactService.UpdateContactAddressAsync(contactID, id, input);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ลบที่อยู่ที่ติดต่อได้
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{contactID}/Addresses/{id}")]
        public async Task<IActionResult> DeleteContactAddress([FromRoute]Guid contactID, [FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await ContactService.DeleteContactAddressAsync(id);

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get ข้อมูล Contact เดิมที่มีอยู่ในระบบ
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ContactDTO</returns>
        [HttpPost("SimilarContacts")]
        [ProducesResponseType(200, Type = typeof(ContactSimilarPopupDTO))]
        public async Task<IActionResult> GetContactSimilar([FromBody]ContactDTO input)
        {
            try
            {
                var result = await ContactService.GetContactSimilarAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}