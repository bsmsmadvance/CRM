using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    public class TitleDeedsController : BaseController
    {
        private readonly ITitleDeedService TitleDeedService;
        private readonly DatabaseContext DB;

        public TitleDeedsController(ITitleDeedService titleDeedService, DatabaseContext db)
        {
            this.TitleDeedService = titleDeedService;
            this.DB = db;
        }

        /// <summary>
        /// ลิส ข้อมูลโฉนด
        /// </summary>
        /// <returns>The title deed list.</returns>
        /// <param name="request">Request.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        /// <param name="projectID">Project identifier.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<TitleDeedListDTO>))]
        public async Task<IActionResult> GetTitleDeedList([FromQuery]TitleDeedFilter request, [FromQuery]PageParam pageParam, [FromQuery]TitleDeedListSortByParam sortByParam, [FromQuery]Guid? projectID = null)
        {
            var result = await TitleDeedService.GetTitleDeedListAsync(projectID, request, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);

            return Ok(result.TitleDeeds);
        }

        /// <summary>
        /// ข้อมูลโฉนด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TitleDeedDTO))]
        public async Task<IActionResult> GetTitleDeed([FromRoute] Guid id)
        {
            try
            {
                var result = await TitleDeedService.GetTitleDeedAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขสถานะโฉนด
        /// </summary>
        /// <returns>The titledeed status.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Status")]
        [ProducesResponseType(200, Type = typeof(TitleDeedDTO))]
        public async Task<IActionResult> UpdateTitledeedStatus([FromRoute]Guid id, [FromBody]TitleDeedDTO input)
        {
            try
            {
                var result = await TitleDeedService.UpdateTitleDeedStatusAsync(id, input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายการประวัติสถานะโฉนด
        /// </summary>
        /// <returns>The titledeed history items.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/History")]
        [ProducesResponseType(200, Type = typeof(List<TitleDeedDTO>))]
        public async Task<IActionResult> GetTitleDeedHistoryItems([FromRoute]Guid id)
        {
            try
            {
                var results = await TitleDeedService.GetTitleDeedHistoryItemsAsync(id);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
