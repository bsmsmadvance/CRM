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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : BaseController
    {
        private IAgentsService AgentsService;
        private readonly DatabaseContext DB;
        public AgentsController(DatabaseContext db, IAgentsService agentsService)
        {
            this.AgentsService = agentsService;
            this.DB = db;
        }
        /// <summary>
        /// ข้อมูล Agent Drodown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<AgentDropdownDTO>))]
        public async Task<IActionResult> GetAgentsDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await AgentsService.GetAgentDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้องข้อมูล Agent
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<AgentDTO>))]
        public async Task<IActionResult> GetAgentsList([FromQuery]AgentFilter filter, [FromQuery]PageParam pageParam, [FromQuery]AgentSortByParam sortByParam)
        {
            try
            {
                var result = await AgentsService.GetAgentListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Agents);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public async Task<IActionResult> GetAgent([FromRoute] Guid id)
        {
            try
            {
                var result = await AgentsService.GetAgentAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       /// <summary>
       /// สร้างข้อมูล Agent
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public async Task<IActionResult> CreateAgent([FromBody]AgentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgentsService.CreateAgentAsync(input);
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
        /// แก้ไขข้อมูล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public async Task<IActionResult> EditAgent([FromRoute] Guid id, [FromBody]AgentDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgentsService.UpdateAgentAsync(id, input);
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
        /// ลบข้อมุล Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await AgentsService.DeleteAgentAsync(id);
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