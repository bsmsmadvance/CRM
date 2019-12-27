using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Services.OpportunityService;
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
    public class OpportunitiesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IOpportunityService OpportunityService;

        public OpportunitiesController(DatabaseContext db, IOpportunityService opportunityService)
        {
            this.DB = db;
            this.OpportunityService = opportunityService;
        }

        /// <summary>
        /// Get Opportunity ทั้งหมด
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>List OpportunityListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<OpportunityListDTO>))]
        public async Task<IActionResult> GetOpportunityList([FromQuery]OpportunityFilter filter, [FromQuery]PageParam pageParam, [FromQuery]OpportunityListSortByParam sortByParam)
        {
            try
            {
                var result = await OpportunityService.GetOpportunityListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Opportunities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Opportunity ทีละรายการ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OpportunityDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OpportunityDTO))]
        public async Task<IActionResult> GetOpportunity([FromRoute]Guid id)
        {
            try
            {
                var result = await OpportunityService.GetOpportunityAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง Opportunity
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fromVisitorID"></param>
        /// <returns>OpportunityDTO</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(OpportunityDTO))]
        public async Task<IActionResult> CreateOpportunity([FromBody]OpportunityDTO input, [FromQuery]Guid? fromVisitorID = null)
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

                    var result = await OpportunityService.CreateOpportunityAsync(input, userID, fromVisitorID);
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
        /// แก้ไข Opportunity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityDTO</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(OpportunityDTO))]
        public async Task<IActionResult> EditOpportunity([FromRoute]Guid id, [FromBody]OpportunityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.UpdateOpportunityAsync(id, input);
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
        /// ลบ Opportunity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpportunity([FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await OpportunityService.DeleteOpportunityAsync(id);

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
        /// Get ข้อมูล Activity ทั้งหมด
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>List OpportunityActivityListDTO</returns>
        [HttpGet("{opportunityID}/Activities")]
        [ProducesResponseType(200, Type = typeof(List<OpportunityActivityListDTO>))]
        public async Task<IActionResult> GetOpportunityActivityList([FromRoute]Guid opportunityID)
        {
            try
            {
                var result = await OpportunityService.GetOpportunityActivityListAsync(opportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Activity
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns>OpportunityActivityDTO</returns>
        [HttpGet("{opportunityID}/Activities/{id}")]
        [ProducesResponseType(200, Type = typeof(OpportunityActivityDTO))]
        public async Task<IActionResult> GetOpportunityActivity([FromRoute]Guid opportunityID, [FromRoute]Guid id)
        {
            try
            {
                var result = await OpportunityService.GetOpportunityActivityAsync(id);
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
        /// <param name="opportunityID"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityActivityDTO</returns>
        [HttpPost("{opportunityID}/Activities")]
        [ProducesResponseType(200, Type = typeof(OpportunityActivityDTO))]
        public async Task<IActionResult> CreateOpportunityActivity([FromRoute]Guid opportunityID, [FromBody]OpportunityActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.CreateOpportunityActivityAsync(opportunityID, input);
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
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>OpportunityActivityDTO</returns>
        [HttpPut("{opportunityID}/Activities/{id}")]
        [ProducesResponseType(200, Type = typeof(OpportunityActivityDTO))]
        public async Task<IActionResult> EditOpportunityActivity([FromRoute]Guid opportunityID, [FromRoute]Guid id, [FromBody]OpportunityActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.UpdateOpportunityActivityAsync(id, input);
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
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{opportunityID}/Activities/{id}")]
        public async Task<IActionResult> DeleteOpportunityActivity([FromRoute]Guid opportunityID, [FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await OpportunityService.DeleteOpportunityActivityAsync(id);

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
        /// Get Draft Activity
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>OpportunityActivityDTO</returns>
        [HttpGet("{opportunityID}/Activities/Draft")]
        [ProducesResponseType(200, Type = typeof(OpportunityActivityDTO))]
        public async Task<IActionResult> GetOpportunityActivityDraft([FromRoute]Guid opportunityID)
        {
            try
            {
                var result = await OpportunityService.GetOpportunityActivityDraftAsync(opportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Revisit ทั้งหมด
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>List RevisitActivityListDTO</returns>
        [HttpGet("{opportunityID}/Revisits")]
        [ProducesResponseType(200, Type = typeof(List<RevisitActivityListDTO>))]
        public async Task<IActionResult> GetRevisitList([FromRoute]Guid opportunityID)
        {
            try
            {
                var result = await OpportunityService.GetRevisitActivityListAsync(opportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get ข้อมูล Revisit
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns>RevisitActivityDTO</returns>
        [HttpGet("{opportunityID}/Revisits/{id}")]
        [ProducesResponseType(200, Type = typeof(RevisitActivityDTO))]
        public async Task<IActionResult> GetRevisit([FromRoute]Guid opportunityID, [FromRoute]Guid id)
        {
            try
            {
                var result = await OpportunityService.GetRevisitActivityAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง Revisit
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="input"></param>
        /// <returns>RevisitActivityDTO</returns>
        [HttpPost("{opportunityID}/Revisits")]
        [ProducesResponseType(200, Type = typeof(RevisitActivityDTO))]
        public async Task<IActionResult> CreateRevisit([FromRoute]Guid opportunityID, [FromBody]RevisitActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.CreateRevisitActivityAsync(opportunityID, input);
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
        /// แก้ไข Revisit
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>RevisitActivityDTO</returns>
        [HttpPut("{opportunityID}/Revisits/{id}")]
        [ProducesResponseType(200, Type = typeof(RevisitActivityDTO))]
        public async Task<IActionResult> EditOpportunityRevisit([FromRoute]Guid opportunityID, [FromRoute]Guid id, [FromBody]RevisitActivityDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.UpdateRevisitActivityAsync(id, input);
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
        /// ลบ Revisit
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{opportunityID}/Revisits/{id}")]
        public async Task<IActionResult> DeleteRevisitActivity([FromRoute]Guid opportunityID, [FromRoute]Guid id)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    await OpportunityService.DeleteRevisitActivityAsync(id);

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
        /// Get Draft Revisit
        /// </summary>
        /// <param name="opportunityID"></param>
        /// <returns>RevisitActivityDTO</returns>
        [HttpGet("{opportunityID}/Revisits/Draft")]
        [ProducesResponseType(200, Type = typeof(RevisitActivityDTO))]
        public async Task<IActionResult> GetRevisitDraft([FromRoute]Guid opportunityID)
        {
            try
            {
                var result = await OpportunityService.GetRevisitActivityDraftAsync(opportunityID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign ผู้ดูแลหลายคน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Assigns")]
        [ProducesResponseType(200, Type = typeof(OpportunityAssignDTO))]
        public async Task<IActionResult> AssignOpportunityList([FromBody]OpportunityAssignDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.AssignOpportunityListAsync(input);
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

        [HttpPost("RandomAssigns")]
        [ProducesResponseType(200, Type = typeof(OpportunityAssignDTO))]
        public async Task<IActionResult> RandomAssignOpportunityList([FromQuery]Guid projectID, [FromBody]List<OpportunityListDTO> inputs)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await OpportunityService.AssignOpportunityListRandomAsync(projectID, inputs);
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