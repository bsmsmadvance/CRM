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
    public class ChangeLCTransferController : BaseController
    {
        private IChangeLCTransferService ChangeLCTransferService;
        private readonly DatabaseContext DB;

        public ChangeLCTransferController(IChangeLCTransferService service, DatabaseContext db)
        {
            this.ChangeLCTransferService = service;
            this.DB = db;
        }

        /// <summary>
        /// ลิสของChange LC โอน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ChangeLCTransferDTO>))]
        public async Task<IActionResult> GetChangeLCTransferList([FromQuery]ChangeLCTransferFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ChangeLCTransferSortByParam sortByParam)
        {
            try
            {
                var result = await ChangeLCTransferService.GetChangeLCTransferListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.ChangeLCTransfers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลChange LC โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ChangeLCTransferDTO))]
        public async Task<IActionResult> GetChangeLCTransfer([FromRoute] Guid id)
        {
            try
            {
                var result = await ChangeLCTransferService.GetChangeLCTransferAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างChange LC โอน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ChangeLCTransferDTO))]
        public async Task<IActionResult> CreateChangeLCTransfer([FromBody]ChangeLCTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ChangeLCTransferService.CreateChangeLCTransferAsync(input);
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
        /// แก้ไขChange LC โอน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ChangeLCTransferDTO))]
        public async Task<IActionResult> EditChangeLCTransfer([FromRoute] Guid id, [FromBody]ChangeLCTransferDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ChangeLCTransferService.UpdateChangeLCTransferAsync(id, input);
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
        /// ลบChange LC โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChangeLCTransfer([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await ChangeLCTransferService.DeleteChangeLCTransferAsync(id);
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
