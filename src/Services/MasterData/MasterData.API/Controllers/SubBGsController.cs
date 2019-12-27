using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using MasterData.API.Extensions;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using MasterData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SubBGsController : BaseController
    {
        private readonly DatabaseContext DB;
        private ISubBGService SubBGService;

        public SubBGsController(DatabaseContext db, ISubBGService subBGService)
        {
            this.DB = db;
            this.SubBGService = subBGService;
        }
        /// <summary>
        /// ลิสข้อมูล SUBG Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bGID"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<SubBGDropdownDTO>))]
        public async Task<IActionResult> GetSubBGDropdownList([FromQuery]string name, [FromQuery]Guid? bGID = null)
        {
            try
            {
                var result = await SubBGService.GetSubBGDropdownListAsync(name, bGID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูล Subbg
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<SubBGDTO>))]
        public async Task<IActionResult> GetBGSubList([FromQuery]SubBGFilter filter, [FromQuery]PageParam pageParam,[FromQuery]SubBGSortByParam sortByParam)
        {
            try
            {
                var result = await SubBGService.GetSubBGListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.SubBGs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SubBGDTO))]
        public async Task<IActionResult> GetSubBG([FromRoute] Guid id)
        {
            try
            {
                var result = await SubBGService.GetSubBGAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูล Subbg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(SubBGDTO))]
        public async Task<IActionResult> CreateSubBG([FromBody]SubBGDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubBGService.CreateSubBGAsync(input);
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
        /// แก้ไขข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(SubBGDTO))]
        public async Task<IActionResult> EditSubBG([FromRoute] Guid id, [FromBody]SubBGDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubBGService.UpdateSubBGAsync(id, input);
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
        /// ลบข้อมูล Subbg
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubBG([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await SubBGService.DeleteSubBGAsync(id);
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