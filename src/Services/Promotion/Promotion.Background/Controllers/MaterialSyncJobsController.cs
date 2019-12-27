using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.PRM;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;
using Promotion.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.Background.Controllers
{
    [Route("api/[controller]")]
    public class SyncMaterialJobsController : BaseController
    {
        private readonly IPromotionMaterialService PromotionMaterialService;

        public SyncMaterialJobsController(IPromotionMaterialService promotionMaterialService)
        {
            this.PromotionMaterialService = promotionMaterialService;
        }

        /// <summary>
        /// ดึงรายการ Background Job ของ SAP Material Sync
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MaterialSyncJobDTO>))]
        public async Task<IActionResult> GetMaterialSyncJobListAsync([FromQuery]MaterialSyncJobFilter filter, [FromQuery]PageParam pageParam, [FromQuery]MaterialSyncJobSortByParam sortByParam)
        {
            try
            {
                var result = await PromotionMaterialService.GetMaterialSyncJobListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);
                return Ok(result.MaterialSyncJobDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
