using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.USR;
using Customer.Params.Filters;
using Customer.Services.LeadService;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LeadsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly ILeadService LeadService;

        public LeadsController(DatabaseContext db, ILeadService leadService)
        {
            this.DB = db;
            this.LeadService = leadService;
        }

        /// <summary>
        /// Get List ข้อมูล Lead
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>List LeadListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LeadListDTO>))]
        public async Task<IActionResult> GetLeadList([FromQuery]LeadFilter filter, [FromQuery]PageParam pageParam, [FromQuery]LeadListSortByParam sortByParam)
        {
            try
            {
                var result = await LeadService.GetLeadListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Leads);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Lead ทีละรายการ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LeadDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LeadDTO))]
        public async Task<IActionResult> GetLead([FromRoute]Guid id)
        {
            try
            {
                var result = await LeadService.GetLeadAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างข้อมูล Lead
        /// </summary>
        /// <param name="input"></param>
        /// <returns>LeadDTO</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(LeadDTO))]
        public async Task<IActionResult> CreateLead([FromBody]LeadDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await LeadService.CreateLeadAsync(input, userID);
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
        /// แก้ไขข้อมูล Lead
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LeadDTO</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(LeadDTO))]
        public async Task<IActionResult> EditLead([FromRoute]Guid id, [FromBody]LeadDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.UpdateLeadAsync(id, input);
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

        [HttpPut("{id}/Assign")]
        [ProducesResponseType(200, Type = typeof(LeadListDTO))]
        public async Task<IActionResult> AssignLead([FromRoute]Guid id, [FromBody]UserListDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.AssignLeadAsync(id, input);
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
        /// ลบ Lead ทีละรายการ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead([FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await LeadService.DeleteLeadAsync(id);
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
        /// Get ข้อมูล Draft ของ Activity ทั้งหมด
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>Lead LeadActivityDraftDTO</returns>
        [HttpGet("{leadID}/Activities/Draft")]
        [ProducesResponseType(200, Type = typeof(LeadActivityDTO))]
        public async Task<IActionResult> GetLeadActivityDraft([FromRoute]Guid leadID)
        {
            try
            {
                var result = await LeadService.GetLeadActivityDraftAsync(leadID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Activity ทั้งหมด
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>Lead LeadActivityListDTO</returns>
        [HttpGet("{leadID}/Activities")]
        [ProducesResponseType(200, Type = typeof(List<LeadActivityListDTO>))]
        public async Task<IActionResult> GetLeadActivityList([FromRoute]Guid leadID)
        {
            try
            {
                var result = await LeadService.GetLeadActivityListAsync(leadID);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Activity
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <returns>LeadActivityDTO</returns>
        [HttpGet("{leadID}/Activities/{id}")]
        [ProducesResponseType(200, Type = typeof(LeadActivityDTO))]
        public async Task<IActionResult> GetLeadActivity([FromRoute]Guid leadID, [FromRoute]Guid id)
        {
            try
            {
                var result = await LeadService.GetLeadActivityAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง Activity
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="input"></param>
        /// <returns>LeadActivityDTO</returns>
        [HttpPost("{leadID}/Activities")]
        [ProducesResponseType(200, Type = typeof(LeadActivityDTO))]
        public async Task<IActionResult> CreateLeadActivity([FromRoute]Guid leadID, [FromBody]LeadActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.CreateLeadActivityAsync(leadID, input);
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
        /// แก้ไข Activity
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>LeadActivityDTO</returns>
        [HttpPut("{leadID}/Activities/{id}")]
        [ProducesResponseType(200, Type = typeof(LeadActivityDTO))]
        public async Task<IActionResult> EditLeadActivity([FromRoute]Guid leadID, [FromRoute]Guid id, [FromBody]LeadActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.UpdateLeadActivityAsync(id, input);
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
        /// ลบ Activity
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{leadID}/Activities/{id}")]
        public async Task<IActionResult> DeleteLeadActivity([FromRoute]Guid leadID, [FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await LeadService.DeleteLeadActivityAsync(id);

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
        /// Get ข้อมูล Contact ที่ใกล้เคียงกับ Lead ทั้งหมด
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns>List LeadQualifyDTO</returns>
        [HttpGet("{leadID}/Qualifies")]
        [ProducesResponseType(200, Type = typeof(List<LeadQualifyDTO>))]
        public async Task<IActionResult> GetLeadQualify([FromRoute]Guid leadID)
        {
            try
            {
                var result = await LeadService.GetLeadQualify(leadID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ยืนยัน Qualify
        /// </summary>
        /// <param name="leadID"></param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        [HttpPost("{leadID}/Qualifies")]
        [ProducesResponseType(200, Type = typeof(LeadDTO))]
        public async Task<IActionResult> SubmitQualify([FromRoute]Guid leadID, [FromQuery]Guid? contactID)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.SubmitLeadQualify(leadID, contactID);

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
        /// ไม่ยืนยัน Qualify (DisQualify)
        /// </summary>
        /// <param name="leadID"></param>
        /// <returns></returns>
        [HttpPost("{leadID}/DisQualifies")]
        [ProducesResponseType(200, Type = typeof(LeadDTO))]
        public async Task<IActionResult> UnSubmitQualify([FromRoute]Guid leadID)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.UnSubmitLeadQualify(leadID);
                    if (result == null)
                    {
                        return NotFound("ID not found.");
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
        /// Assign ผู้ดูแลหลายคน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Assigns")]
        [ProducesResponseType(200, Type = typeof(LeadAssignDTO))]
        public async Task<IActionResult> AssignLeadList([FromBody]LeadAssignDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.AssignLeadListAsync(input);
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
        /// Assign ผู้ดูแลหลายคนแบบ Random
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("RandomAssigns")]
        [ProducesResponseType(200, Type = typeof(LeadAssignDTO))]
        public async Task<IActionResult> RandomAssignLeadList([FromQuery]Guid projectID, [FromBody]List<LeadListDTO> inputs)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LeadService.AssignLeadListRandomAsync(projectID, inputs);
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
    }
}