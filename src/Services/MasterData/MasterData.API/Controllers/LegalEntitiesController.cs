using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MasterData.Params.Filters;
using MasterData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class LegalEntitiesController : BaseController
    {
        private ILegalEntityService LegalEntityService;
        private readonly DatabaseContext DB;

        public LegalEntitiesController(ILegalEntityService legalEntityService, DatabaseContext db)
        {
            this.LegalEntityService = legalEntityService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสข้อมูลนิติบุคคล Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<LegalEntityDropdownDTO>))]
        public async Task<IActionResult> GetLegalEntitiyDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await LegalEntityService.GetLegalEntityDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<LegalEntityDTO>))]
        public async Task<IActionResult> GetLegalEntitiyList([FromQuery]LegalFilter filter, [FromQuery]PageParam pageParam,[FromQuery]LegalEntitySortByParam sortByParam)
        {
            try
            {
                var result = await LegalEntityService.GetLegalEntityListAsync(filter, pageParam,sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.LegalEntities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LegalEntityDTO))]
        public async Task<IActionResult> GetLegalEntitiy([FromRoute] Guid id)
        {
            try
            {
                var result = await LegalEntityService.GetLegalEntityAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(LegalEntityDTO))]
        public async Task<IActionResult> CreateLegalEntitiy([FromBody]LegalEntityDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LegalEntityService.CreateLegalEntityAsync(input);
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
        /// แก้ไขข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(LegalEntityDTO))]
        public async Task<IActionResult> EditLegalEntitiy([FromRoute] Guid id, [FromBody]LegalEntityDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await LegalEntityService.UpdateLegalEntityAsync(id, input);
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
        /// ลบข้อมูลนิติบุคคล
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLegalEntitiy([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await LegalEntityService.DeleteLegalEntityAsync(id);
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