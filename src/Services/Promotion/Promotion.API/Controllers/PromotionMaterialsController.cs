using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.API.Controllers
{
    [Route("api/[controller]")]
    public class PromotionMaterialsController : BaseController
    {
        private readonly IPromotionMaterialService PromotionMaterialService;
        private readonly ILogger<PromotionMaterialsController> Logger;
        private readonly DatabaseContext DB;

        public PromotionMaterialsController(IPromotionMaterialService promotionMaterialService, ILogger<PromotionMaterialsController> logger, DatabaseContext db)
        {
            this.PromotionMaterialService = promotionMaterialService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูล Material ที่มาจาก SAP
        /// </summary>
        /// <returns>The promotion material list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<PromotionMaterialDTO>))]
        public async Task<IActionResult> GetPromotionMaterialList([FromQuery]PromotionMaterialFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]PromotionMaterialSortByParam sortByParam)
        {
            try
            {
                var result = await PromotionMaterialService.GetPromotionMaterialListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.PromotionMaterialDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
