using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Services.ContactServices;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace Customer.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IActivityService ActivityService;

        public ActivitiesController(DatabaseContext db, IActivityService activityService)
        {
            this.DB = db;
            this.ActivityService = activityService;
        }

        /// <summary>
        /// Get List ข้อมูล Activity (My world)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>List LeadListDTO</returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ActivityListDTO>))]
        public async Task<IActionResult> GetActivityList([FromQuery]ActivityFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ActivityListSortByParam sortByParam)
        {
            try
            {
                var result = await ActivityService.GetActivityListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(result.PageOutput);

                return Ok(result.Activities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}