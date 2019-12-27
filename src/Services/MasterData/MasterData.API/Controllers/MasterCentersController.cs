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
using Database.Models.DbQueries;

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class MasterCentersController : BaseController
    {
        private IMasterCenterService MasterCenterService;
        private readonly DatabaseContext DB;

        public MasterCentersController(IMasterCenterService masterCenterService, DatabaseContext db)
        {
            this.MasterCenterService = masterCenterService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป Dropdown
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<MasterCenterDropdownDTO>))]
        public async Task<IActionResult> GetMasterCenterDropdownList([FromQuery]string masterCenterGroupKey, [FromQuery]string name)
        {
            try
            {
                var results = await MasterCenterService.GetMasterCenterDropdownListAsync(masterCenterGroupKey, name);
                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(MasterCenterDropdownDTO))]
        public async Task<IActionResult> GetFindMasterCenterDropdownItem([FromQuery]string masterCenterGroupKey, [FromQuery]string key)
        {
            try
            {
                var result = await MasterCenterService.GetFindMasterCenterDropdownItemAsync(masterCenterGroupKey, key);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// ลิสข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterCenterDTO>))]
        public async Task<IActionResult> GetMasterCenterList([FromQuery]MasterCenterFilter filter, [FromQuery]PageParam pageParam, [FromQuery]MasterCenterSortByParam sortByParam)
        {
            try
            {
                var result = await MasterCenterService.GetMasterCenterListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterCenters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterCenterDTO))]
        public async Task<IActionResult> GetMasterCenter([FromRoute] Guid id)
        {
            try
            {
                var result = await MasterCenterService.GetMasterCenterAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// เพิ่มข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MasterCenterDTO))]
        public async Task<IActionResult> CreateMasterCenter([FromBody]MasterCenterDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterCenterService.CreateMasterCenterAsync(input);
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
        /// แก้ไข ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterCenterDTO))]
        public async Task<IActionResult> EditMasterCenter([FromRoute] Guid id, [FromBody]MasterCenterDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterCenterService.UpdateMasterCenterAsync(id, input);
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
        /// ลบข้อมูลพื้นฐานทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterCenter([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterCenterService.DeleteMasterCenterAsync(id);
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

        /// <summary>
        /// ทดสอบดึงข้อมูลด้วย DbQuery
        /// </summary>
        /// <param name="masterCenterGroupKey"></param>
        /// <returns></returns>
        [HttpGet("SampleDbQuery")]
        [ProducesResponseType(200, Type = typeof(List<MasterCenterResult>))]
        public async Task<IActionResult> GetDbQuerySamples([FromQuery]string masterCenterGroupKey)
        {
            var results = await MasterCenterService.GetMasterCenterUsingDbQueryAsync(masterCenterGroupKey);

            return Ok(results);
        }
    }
}