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
    public class BGsController : BaseController
    {
        private readonly DatabaseContext DB;
        private IBGService BGService;

        public BGsController(IBGService bGService, DatabaseContext db)
        {
            this.BGService = bGService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสของBG Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BGDropdownDTO>))]
        public async Task<IActionResult> GetBGDropdownList([FromQuery]string productTypeKey = null, [FromQuery]string name = null)
        {
            try
            {
                var results = await BGService.GetBGDropdownListAsync(productTypeKey, name);
                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของ BG
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BGDTO>))]
        public async Task<IActionResult> GetBGList([FromQuery]BGFilter filter, [FromQuery] PageParam pageParam, [FromQuery]BGSortByParam sortByParam)
        {
            try
            {
                var result = await BGService.GetBGListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.BGs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล BG
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BGDTO))]
        public async Task<IActionResult> GetBG([FromRoute] Guid id)
        {
            try
            {
                var result = await BGService.GetBGAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้าง BG
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BGDTO))]
        public async Task<IActionResult> CreateBG([FromBody]BGDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BGService.CreateBGAsync(input);
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
        /// แก้ไข BG
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BGDTO))]
        public async Task<IActionResult> EditBG([FromRoute] Guid id, [FromBody]BGDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BGService.UpdateBGAsync(id, input);
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
        /// ลบ BG
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBG([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BGService.DeleteBGAsync(id);
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