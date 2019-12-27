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
    public class LandOfficesController : BaseController
    {
        private ILandOfficeService LandOfficeService;
        private readonly DatabaseContext DB;

        public LandOfficesController(ILandOfficeService landOfficeService,DatabaseContext db)
        {
            this.LandOfficeService = landOfficeService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<LandOfficeListDTO>))]
        public async Task<IActionResult> GetLandOfficeDropdownList([FromQuery]string name = null, [FromQuery] Guid? provinceID = null)
        {
            try
            {
                var result = await LandOfficeService.GetLandOfficeDropdownListAsync(name, provinceID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของข้อมูล สำนักงานที่ดิน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LandOfficeDTO>))]
        public async Task<IActionResult> GetLandOfficeList([FromQuery]LandOfficeFilter filter, [FromQuery]PageParam pageParam,[FromQuery]LandOfficeSortByParam sortByParam)
        {
            var result = await LandOfficeService.GetLandOfficeListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.LandOffices);
        }
        /// <summary>
        /// ข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LandOfficeDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetLandOffice([FromRoute] Guid id)
        {
            try
            {
                var result = await LandOfficeService.GetLandOfficeAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(LandOfficeDTO))]
        public async Task<IActionResult> CreateLandOffice([FromBody]LandOfficeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LandOfficeService.CreateLandOfficeAsync(input);
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
        /// แก้ไขข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(LandOfficeDTO))]
        public async Task<IActionResult> EditLandOffice([FromRoute] Guid id,[FromBody] LandOfficeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LandOfficeService.UpdateLandOfficeAsync(id, input);
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
        /// ลบข้อมูลสำนักงานที่ดิน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandOffice([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await LandOfficeService.DeleteLandOfficeAsync(id);
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