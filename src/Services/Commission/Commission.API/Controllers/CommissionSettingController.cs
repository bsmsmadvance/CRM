using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commission.Params.Filters;
using Commission.Params.Inputs;
using Base.DTOs.CMS;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Commission.API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commission.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Commission.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionSettingController : BaseController
    {
        private ICommissionSettingService CommissionSettingService;
        private readonly DatabaseContext DB;

        public CommissionSettingController(ICommissionSettingService service, DatabaseContext db)
        {
            this.CommissionSettingService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของ Commission Setting
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CommissionSettingDTO>))]
        public async Task<IActionResult> GetCommissionSettingList([FromQuery]CommissionSettingFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CommissionSettingSortByParam sortByParam)
        {
            try
            {
                var result = await CommissionSettingService.GetCommissionSettingListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.CommissionSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงข้อมูลพนักงายขานประจำโครงการ
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="SaleUserFullName"></param>
        /// <returns></returns>
        [HttpGet("GetSaleUserProject")]
        [ProducesResponseType(200, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetSaleUserProject([FromQuery]Guid ProjectID, [FromQuery]string SaleUserFullName)
        {
            var result = await CommissionSettingService.GetSaleUserProjectAsync(ProjectID, SaleUserFullName);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลพนักงานขายประจำโครงการ
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSaleUserAll")]
        [ProducesResponseType(200, Type = typeof(List<UserListDTO>))]
        public async Task<IActionResult> GetSaleUserAll()
        {
            var result = await CommissionSettingService.GetSaleUserAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโครงการตาม bg
        /// </summary>
        /// <param name="BgId"></param>
        /// <returns></returns>
        [HttpGet("GetProjectDropdownListByBG")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdownListByBG([FromQuery]Guid BgId)
        {
            var result = await CommissionSettingService.GetProjectDropdownListByBGAsync(BgId);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโครงการตามโครงการที่เลือก
        /// </summary>
        /// <param name="ListProject"></param>
        /// <returns></returns>
        [HttpPost("GetProjectDropdownListByProject")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdownListByProject([FromBody]ListProjectInput ListProject)
        {
            var result = await CommissionSettingService.GetProjectDropdownListByProjectAsync(ListProject);
            return Ok(result);
        }

        /// <summary>
        /// ดึงข้อมูลโครงการตามแนวราบหรือสูง
        /// </summary>
        /// <param name="ProductType">
        /// 1=แนวราบ/2=แนวสูง        
        /// </param>
        /// <returns></returns>
        [HttpGet("GetProjectDropdownListByProductType")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdownListByProductType([FromQuery]string ProductType)
        {
            var result = await CommissionSettingService.GetProjectDropdownListByProductTypeAsync(ProductType);
            return Ok(result);
        }
    }
}
