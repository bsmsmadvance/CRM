using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Services;
using Promotion.Services.IService;
using Promotion.Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.API.Controllers
{
    [Route("api/[controller]")]
    public class TransferPromotionsController : BaseController
    {
        private readonly ITransferPromotionService TransferPromotionService;
        private readonly ILogger<TransferPromotionsController> Logger;
        private readonly DatabaseContext DB;

        public TransferPromotionsController(ITransferPromotionService transferPromotionService, ILogger<TransferPromotionsController> logger, DatabaseContext db)
        {
            this.TransferPromotionService = transferPromotionService;
            this.Logger = logger;
            this.DB = db;
        }
        /// <summary>
        /// ดึงข้อมูลเบื้องต้นและราคาโปรโมชั่นส่งเสริมการโอน/โครงการ/ลูกค้า/พนักงานขาย
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotionData")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> GetTransferPromotionDataAsync([FromQuery] Guid transferPromotionId)
        {
            var result = await TransferPromotionService.GetTransferPromotionDataAsync(transferPromotionId);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลเบื้องต้นและราคาโปรโมชั่นส่งเสริมการโอน/โครงการ/ลูกค้า/พนักงานขาย ก่อนตั้งเรื่อง
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotionDrafData")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> GetTransferPromotionDrafDataAsync([FromQuery] Guid agreementId)
        {
            var result = await TransferPromotionService.GetTransferPromotionDrafDataAsync(agreementId);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโปรโมชั่นส่งเสริมการโอน
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotion")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> GetTransferPromotionAsync([FromQuery] Guid agreementId)
        {
            var result = new TransferPromotionDTO();
            var transferPromotionId = await TransferPromotionService.GetTransferPromotionIDAsync(agreementId);

            if (transferPromotionId == null)
            {
                result = await TransferPromotionService.GetTransferPromotionDrafDataAsync(agreementId);
            }
            else
            {
                result = await TransferPromotionService.GetTransferPromotionDataAsync(transferPromotionId);
            }

            return Ok(result);
        }


        /// <summary>
        /// ดึงข้อมูลรายการโปรโมชั่นส่งเสริมการโอน
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotionItemList")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionItemDTO))]
        public async Task<IActionResult> GetTransferPromotionItemListAsync([FromQuery] Guid transferPromotionId)
        {
            var result = await TransferPromotionService.GetTransferPromotionItemListAsync(transferPromotionId);
            return Ok(result);
        }


        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotionExpenseList")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionExpenseDTO))]
        public async Task<IActionResult> GetTransferPromotionExpenseListAsync([FromQuery] Guid transferPromotionId)
        {
            var result = await TransferPromotionService.GetTransferPromotionExpenseListAsync(transferPromotionId);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลรายการค่าใช้จ่าย ก่อนตั้งเรื่อง
        /// </summary>
        /// <param name="transferPromotionId"></param>
        /// <returns></returns>
        [HttpGet("GetTransferPromotionExpensesDraft")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionExpenseDTO))]
        public async Task<IActionResult> GetTransferPromotionExpensesDraftAsync([FromQuery] Guid transferPromotionId)
        {
            var result = await TransferPromotionService.GetTransferPromotionExpensesDraftAsync(transferPromotionId);
            return Ok(result);
        }


        /// <summary>
        /// ขออนุมัติการเสนอโปรโมชั่นโอน
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expenses"></param>
        /// <returns></returns>
        [HttpPost("CreateTransferPromotionData")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> CreateTransferPromotionDataAsync([FromBody]TransferPromotionDTO input, List<TransferPromotionExpenseDTO> expenses)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.TransferPromotionService.CreateTransferPromotionDataAsync(input, expenses);
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
        /// ปลดล๊อคส่วนลด ณ วันโอน
        /// </summary>
        /// <param name="transferPromotionID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateAllowTransferDiscount")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> UpdateAllowTransferDiscountAsync([FromQuery] Guid transferPromotionID, [FromBody]TransferPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.TransferPromotionService.UpdateAllowTransferDiscountAsync(transferPromotionID, input);
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

        //Task<TransferPromotionDTO> UpdateAllowTransferDiscountAsync(Guid transferPromotionId, TransferPromotionDTO input);

        /// <summary>
        /// ปลดล๊อคส่วนลดมากกว่า 3%
        /// </summary>
        /// <param name="transferPromotionID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateAllowTransferDiscountOver3Percent")]
        [ProducesResponseType(200, Type = typeof(TransferPromotionDTO))]
        public async Task<IActionResult> UpdateAllowTransferDiscountOver3PercentAsync([FromQuery] Guid transferPromotionID, [FromBody]TransferPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await this.TransferPromotionService.UpdateAllowTransferDiscountOver3PercentAsync(transferPromotionID, input);
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



    }
}
