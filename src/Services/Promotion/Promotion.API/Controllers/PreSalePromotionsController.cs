using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Inputs;
using Promotion.Params.Outputs;
using Promotion.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    public class PreSalePromotionsController : BaseController
    {
        private readonly IPreSalePromotionService PreSalePromotionService;
        private readonly ILogger<PreSalePromotionsController> Logger;
        private readonly DatabaseContext DB;

        public PreSalePromotionsController(IPreSalePromotionService preSalePromotionService, ILogger<PreSalePromotionsController> logger, DatabaseContext db)
        {
            this.PreSalePromotionService = preSalePromotionService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// ดึงโปรก่อนขายที่เสนอให้แปลง
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionDTO))]
        public async Task<IActionResult> GetPreSalePromotionAsync([FromQuery]Guid unitID)
        {
            var result = await PreSalePromotionService.GetPreSalePromotionAsync(unitID);
            return Ok(result);
        }

        /// <summary>
        /// รายการเบิกโปรโมชั่นก่อนขาย (Paging, Sort, Filter)
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362358689/preview
        /// </summary>
        [HttpGet("Requests")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<PreSalePromotionRequestListDTO>))]
        public async Task<IActionResult> GetPreSalePromotionRequestListAsync([FromQuery]PreSalePromotionRequestListFilter filter, [FromQuery]PageParam pageParam, [FromQuery]PreSalePromotionRequestListSortByParam sortByParam)
        {
            try
            {
                var result = await PreSalePromotionService.GetPreSalePromotionRequestListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.PreSalePromotionRequestLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายละเอียดเบิกโปรโมชั่นก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204541/preview
        /// </summary>
        /// <param name="requestID">PreSalePromotionRequest.ID</param>
        /// <returns></returns>
        [HttpGet("Requests/{requestID}")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionRequestDTO))]
        public async Task<IActionResult> GetPreSalePromotionRequestAsync([FromRoute]Guid requestID)
        {
            try
            {
                var result = await PreSalePromotionService.GetPreSalePromotionRequestAsync(requestID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายละเอียดแปลงเบิกโปรก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204542/preview
        /// </summary>
        /// <param name="requestUnitID">PreSalePromotionRequestUnit.ID</param>
        /// <returns></returns>
        [HttpGet("Requests/{requestID}/Units/{requestUnitID}")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionRequestUnitDTO))]
        public async Task<IActionResult> GetPreSalePromotionRequestUnitAsync([FromRoute]Guid requestID, [FromRoute]Guid requestUnitID)
        {
            try
            {
                var result = await PreSalePromotionService.GetPreSalePromotionRequestUnitAsync(requestUnitID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายการโปรก่อนขายที่ Active อยู่ทั้งหมดจาก Master
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092477/preview
        /// </summary>
        /// <returns></returns>
        [HttpGet("MasterItems")]
        [ProducesResponseType(200, Type = typeof(List<PreSalePromotionRequestItemDTO>))]
        public async Task<IActionResult> GetAllActivePreSalePromotionItemsFormMasterAsync([FromQuery]Guid masterPreSalePromotionID)
        {
            try
            {
                var result = await PreSalePromotionService.GetPreSalePromotionItemsFormMasterAsync(masterPreSalePromotionID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// บันทึกใบเบิกโปรก่อนขาย และสร้าง PR
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092474/preview
        /// </summary>
        /// <returns></returns>
        [HttpPost("Requests/SavePR")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionRequestDTO))]
        public async Task<IActionResult> SaveRequestAndCreatePRAsync([FromQuery]Guid masterPreSalePromotionID, [FromBody]PreSaleRequestSavePRParam input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PreSalePromotionService.SaveRequestAndCreatePRAsync(masterPreSalePromotionID, input.Units, input.Items);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// สร้าง PR ใหม่ (จากที่ Failed)
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        [HttpPost("Requests/{requestID}/Units/{requestUnitID}/RetryCreate")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionRequestUnitDTO))]
        public async Task<IActionResult> RetryCreatePRAsync([FromRoute]Guid requestID, [FromRoute]Guid requestUnitID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PreSalePromotionService.RetryCreatePRAsync(requestUnitID);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// ยกเลิก PR
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        [HttpPost("Requests/{requestID}/Units/{requestUnitID}/Cancel")]
        [ProducesResponseType(200, Type = typeof(PreSalePromotionRequestUnitDTO))]
        public async Task<IActionResult> CancelPRAsync([FromRoute]Guid requestID, [FromRoute]Guid requestUnitID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await PreSalePromotionService.CancelPRAsync(requestUnitID);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ยกเลิก PR ทีละหลายรายการ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092509/preview
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        [HttpPost("Requests/{requestID}/Units/MultipleCancel")]
        [ProducesResponseType(200, Type = typeof(List<PreSalePromotionRequestUnitDTO>))]
        public async Task <IActionResult> CancelMultiplePRAsync([FromRoute]Guid requestID, [FromBody]List<PreSalePromotionRequestUnitListDTO> units)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await PreSalePromotionService.CancelMultiplePRAsync(units);
                    tran.Commit();
                    return Ok(results);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// พิมพ์ใบเบิกโปรก่อนขาย
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        [HttpPost("Requests/{requestID}/Units/{requestUnitID}/ExportPrintForm")]
        [ProducesResponseType(200, Type = typeof(StringResult))]
        public async Task<IActionResult> ExportPreSaleRequestPrintFormUrlAsync([FromRoute]Guid requestID, [FromRoute]Guid requestUnitID)
        {
            var result = await PreSalePromotionService.ExportPreSaleRequestPrintFormUrlAsync(requestUnitID);
            return Ok(result);
        }

    }
}
