using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Inputs;
using Customer.Services.VisitorService;
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
    public class VisitorsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IVisitorService VisitorService;

        public VisitorsController(DatabaseContext db, IVisitorService visitorService)
        {
            this.DB = db;
            this.VisitorService = visitorService;
        }

        /// <summary>
        /// Get Visitor ทั้งหมด
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>List VisitorListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<VisitorListDTO>))]
        public async Task<IActionResult> GetVisitorList([FromQuery]VisitorFilter filter, [FromQuery]PageParam pageParam, [FromQuery]VisitorListSortByParam sortByParam)
        {
            try
            {
                var result = await VisitorService.GetVisitorListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);
                return Ok(result.Visitors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// พิมพ์ประวัติการเข้าออกโครงการ
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>FileDTO</returns>
        [HttpGet("Exports")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportVisitor([FromQuery]VisitorFilter filter)
        {
            var result = new FileDTO();
            return Ok(result);
        }

        /// <summary>
        /// Get visitor ทีละรายการ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>VisitorDTO</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(VisitorDTO))]
        public async Task<IActionResult> GetVisitor([FromRoute]Guid id)
        {
            try
            {
                var result = await VisitorService.GetVisitorAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง Visitor
        /// </summary>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(VisitorDTO))]
        public async Task<IActionResult> CreateVisitor([FromBody]VisitorCreateDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await VisitorService.CreateVisitorAsync(input);
                    if (result == null)
                    {
                        return BadRequest("Visitor ซ้ำ");
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
        /// แก้ไขสถานะลูกค้า
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        [HttpPut("{id}/UpdateVisitorTypes")]
        [ProducesResponseType(200, Type = typeof(VisitorDTO))]
        public async Task<IActionResult> EditVisitorType([FromRoute]Guid id, [FromBody]VisitorDTO input)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await VisitorService.UpdateVisitorTypeAsync(id, input);
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
        /// Get จำนวนผู้เข้าออกโครงการ
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("Projects")]
        [ProducesResponseType(200, Type = typeof(VisitorProjectDTO))]
        public async Task<IActionResult> GetVisitorProject([FromQuery]VisitorFilter filter)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await VisitorService.GetVisitorProjectAsync(filter);
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
        /// บันทึกต้อนรับลูกค้า
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>VisitorDTO</returns>
        [HttpPost("{id}/Welcomes")]
        [ProducesResponseType(200, Type = typeof(VisitorDTO))]
        public async Task<IActionResult> SubmitVisitorWelcome([FromRoute]Guid id, [FromBody]VisitorWelcomeInput input)
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

                    var result = await VisitorService.SubmitOrUnSubmitVisitorWelcomeAsync(id, input.IsWelcome, userID);
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
        /// Get ประวัติการเยี่ยมชมโครงการ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns>VisitorDTO</returns>
        [HttpGet("{id}/VisitHistories")]
        [ProducesResponseType(200, Type = typeof(List<VisitorHistoryDTO>))]
        public async Task<IActionResult> GetVisitorHistory([FromRoute]Guid id, [FromQuery]VisitorHistoryListSortByParam sortByParam)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await VisitorService.GetVisitorHistoryListAsync(id, sortByParam);

                    transaction.Commit();
                    return Ok(results);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get ประวัติการ Call/Web/Facebook
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns>VisitorDTO</returns>
        [HttpGet("{id}/Advertisements")]
        [ProducesResponseType(200, Type = typeof(List<LeadListDTO>))]
        public async Task<IActionResult> GetVisitorAdvertisement([FromRoute]Guid id, [FromQuery]VisitorLeadListSortByParam sortByParam)
        {
            try
            {
                var result = await VisitorService.GetVisitorLeadListAsync(id, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ประวัติการซื้อโครงการ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{id}/PurchaseHistories")]
        [ProducesResponseType(200, Type = typeof(List<VisitorPurchaseHistoryDTO>))]
        public async Task<IActionResult> GetVisitorPurchaseHistoryListAsync(Guid id, [FromQuery]VisitorPurchaseHistoryListSortByParam sortByParam)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await VisitorService.GetVisitorPurchaseHistoryListAsync(id, sortByParam);

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
        /// ประวัติการตอบแบบสอบถาม
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet("{id}/QuestionnaireHistories")]
        [ProducesResponseType(200, Type = typeof(List<VisitorQuestionnaireHistoryDTO>))]
        public async Task<IActionResult> GetVisitorQuestionnaireHistoryListAsync(Guid id, [FromQuery]VisitorQuestionnaireHistoryListSortByParam sortByParam)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await VisitorService.GetVisitorQuestionnaireHistoryListAsync(id, sortByParam);

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