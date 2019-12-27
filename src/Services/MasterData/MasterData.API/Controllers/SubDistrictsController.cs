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
    public class SubDistrictsController : BaseController
    {
        private ISubDistrictService SubDistrictService;
        private readonly DatabaseContext DB;
        public SubDistrictsController(ISubDistrictService subDistrictService, DatabaseContext db)
        {
            this.SubDistrictService = subDistrictService;
            this.DB = db;
        }

        /// <summary>
        /// หาตำบลจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(SubDistrictListDTO))]
        public async Task<IActionResult> FindSubDistrict([FromQuery]string name, [FromQuery]Guid districtID)
        {
            try
            {
                var result = await SubDistrictService.FindSubDistrictAsync(districtID, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ลิส ข้อมูลอำเภอ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<SubDistrictListDTO>))]
        public async Task<IActionResult> GetSubDistrictDropdownList([FromQuery]string name, [FromQuery]Guid? districtID = null)
        {
            try
            {
                var result = await SubDistrictService.GetSubDistrictDropdownListAsync(name, districtID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูลอำเภอ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<SubDistrictDTO>))]
        public async Task<IActionResult> GetSubDistrictList([FromQuery]SubDistrictFilter filter, [FromQuery]PageParam pageParam, [FromQuery]SubDistrictSortByParam sortByParam)
        {
            try
            {
                var result = await SubDistrictService.GetSubDistrictListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.SubDistricts);
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
        [ProducesResponseType(200, Type = typeof(SubDistrictDTO))]
        public async Task<IActionResult> GetSubDistrict([FromRoute] Guid id)
        {
            try
            {
                var result = await SubDistrictService.GetSubDistrictAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูล อำเภอ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(SubDistrictDTO))]
        public async Task<IActionResult> CreateSubDistrict([FromBody]SubDistrictDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubDistrictService.CreateSubDistrictAsync(input);
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
        /// แก้ไขข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(SubDistrictDTO))]
        public async Task<IActionResult> EditSubDistrict([FromRoute] Guid id,[FromBody]SubDistrictDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubDistrictService.UpdateSubDistrictAsync(id, input);
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
        /// ลบข้อมูลอำเภอ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubDistrict([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubDistrictService.DeleteSubDistrictAsync(id);
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