using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UnitInfosController : BaseController
    {
        private DatabaseContext DB;
        private IUnitInfoService UnitInfoService;

        public UnitInfosController(IUnitInfoService unitInfoService, DatabaseContext db)
        {
            this.DB = db;
            this.UnitInfoService = unitInfoService;
        }

        /// <summary>
        /// ดึงรายการสถานะแปลง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<UnitInfoListDTO>))]
        public async Task<IActionResult> GetUnitInfoListAsync([FromQuery]UnitInfoListFilter filter, [FromQuery]PageParam pageParam, [FromQuery]UnitInfoListSortByParam sortByParam)
        {
            var result = await UnitInfoService.GetUnitInfoListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.Units);
        }

        /// <summary>
        /// ดึงรายละเอียดแปลงของหน้าสถานะแปลง
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}")]
        [ProducesResponseType(200, Type = typeof(UnitInfoDTO))]
        public async Task<IActionResult> GetUnitInfoAsync([FromRoute]Guid unitID)
        {
            var result = await UnitInfoService.GetUnitInfoAsync(unitID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงรายละเอียดโปรก่อนขาย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PreSalePromotions")]
        [ProducesResponseType(200, Type = typeof(UnitInfoPreSalePromotionDTO))]
        public async Task<IActionResult> GetUnitInfoPreSalePromotionAsync([FromRoute]Guid unitID)
        {
            var result = await UnitInfoService.GetUnitInfoPreSalePromotionAsync(unitID);

            return Ok(result);
        }

        /// <summary>
        /// ดึงรายละเอีลดโปรขาย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/BookingPromotions")]
        [ProducesResponseType(200, Type = typeof(UnitInfoBookingPromotionDTO))]
        public async Task<IActionResult> GetUnitInfoBookingPromotionAsync([FromRoute]Guid unitID)
        {
            var result = await UnitInfoService.GetUnitInfoBookingPromotionAsync(unitID);

            return Ok(result);
        }

        /// <summary>
        /// ดึงรายการค่าใช้จ่าย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PromotionExpenses")]
        [ProducesResponseType(200, Type = typeof(List<UnitInfoPromotionExpenseDTO>))]
        public async Task<IActionResult> GetUnitInfoPromotionExpensesAsync([FromRoute]Guid unitID)
        {
            var results = await UnitInfoService.GetUnitInfoPromotionExpensesAsync(unitID);
            return Ok(results);
        }

        /// <summary>
        /// ดึงข้อมูลราคา
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/PriceLists")]
        [ProducesResponseType(200, Type = typeof(UnitInfoPriceListDTO))]
        public async Task<IActionResult> GetPriceListAsync([FromRoute]Guid unitID)
        {
            var result = await UnitInfoService.GetPriceListAsync(unitID);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลจำนวนแปลง
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        [ProducesResponseType(200, Type = typeof(UnitInfoStatusCountDTO))]
        public async Task<IActionResult> GetUnitInfoCount([FromQuery]Guid? projectID)
        {
            try
            {
                var result = await UnitInfoService.GetUnitInfoCountAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ยอดรวมการชำระ คงเหลือ Overdue ของแปลง
        /// </summary>
        /// <returns>UnitInfoStatusCountDTO</returns>
        [HttpGet("SumPayment")]
        [ProducesResponseType(200, Type = typeof(UnitInfoSumPaymentDTO))]
        public async Task<IActionResult> GetUnitInfoPaymentAsync([FromQuery]Guid unitID)
        {
            try
            {
                var result = await UnitInfoService.GetUnitInfoPaymentAsync(unitID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
