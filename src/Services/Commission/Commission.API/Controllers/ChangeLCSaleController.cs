using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commission.Params.Filters;
using Base.DTOs.CMS;
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
    public class ChangeLCSaleController : BaseController
    {
        private IChangeLCSaleService ChangeLCSaleService;
        private readonly DatabaseContext DB;

        public ChangeLCSaleController(IChangeLCSaleService service, DatabaseContext db)
        {
            this.ChangeLCSaleService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของChange LC ขาย
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ChangeLCSaleDTO>))]
        public async Task<IActionResult> GetChangeLCSaleList([FromQuery]ChangeLCSaleFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ChangeLCSaleSortByParam sortByParam)
        {
            try
            {
                var result = await ChangeLCSaleService.GetChangeLCSaleListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.ChangeLCSales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลChange LC ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ChangeLCSaleDTO))]
        public async Task<IActionResult> GetChangeLCSale([FromRoute] Guid id)
        {
            try
            {
                var result = await ChangeLCSaleService.GetChangeLCSaleAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างChange LC ขาย
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ChangeLCSaleDTO))]
        public async Task<IActionResult> CreateChangeLCSale([FromBody]ChangeLCSaleDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ChangeLCSaleService.CreateChangeLCSaleAsync(input);
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
        /// แก้ไขChange LC ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ChangeLCSaleDTO))]
        public async Task<IActionResult> EditChangeLCSale([FromRoute] Guid id, [FromBody]ChangeLCSaleDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ChangeLCSaleService.UpdateChangeLCSaleAsync(id, input);
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
        /// ลบChange LC ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChangeLCSale([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await ChangeLCSaleService.DeleteChangeLCSaleAsync(id);
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
