using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using MasterData.API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MasterData.Services;
using Database.Models;
using PagingExtensions;
using Base.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : BaseController
    {
        private IDistrictService DistrictService;
        private readonly DatabaseContext DB;

        public DistrictsController(IDistrictService counterService, DatabaseContext db)
        {
            this.DistrictService = counterService;
            this.DB = db;
        }
        /// <summary>
        /// หาอำเภอจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinceID"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(DistrictListDTO))]
        public async Task<IActionResult> FindDistrict([FromQuery]string name, [FromQuery]Guid provinceID)
        {
            try
            {
                var result = await DistrictService.FindDistrictAsync(provinceID, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ลิสของอำเภอ dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinceID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<DistrictListDTO>))]
        public async Task<IActionResult> GetDistrictDropdownList([FromQuery]string name, [FromQuery]Guid? provinceID = null)
        {
            try
            {
                var result = await DistrictService.GetDistrictDropdownListAsync(name, provinceID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของอำเภอ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DistrictDTO>))]
        public async Task<IActionResult> GetDistrictList([FromQuery]DistrictFilter filter, [FromQuery]PageParam pageParam,[FromQuery]DistrictSortByParam sortByParam)
        {
            try
            {
                var result = await DistrictService.GetDistrictListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Districts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(DistrictDTO))]
        public async Task<IActionResult> GetDistrict([FromRoute] Guid id)
        {
            try
            {
                var result = await DistrictService.GetDistrictAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างอำเภอ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(DistrictDTO))]
        public async Task<IActionResult> CreateDistrict([FromBody]DistrictDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DistrictService.CreateDistrictAsync(input);
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
        /// แก้ไขข้อมูล อำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(DistrictDTO))]
        public async Task<IActionResult> EditDistrict([FromRoute] Guid id, [FromBody]DistrictDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DistrictService.UpdateDistrictAsync(id, input);
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
        /// ลบ ข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DistrictService.DeleteDistrictAsync(id);
                    tran.Commit();
                    return Ok();
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