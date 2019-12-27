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
    public class AgentEmployeesController : BaseController
    {
        private IAgentEmployeeService AgentEmployeeService;
        private readonly DatabaseContext DB;
        public AgentEmployeesController(DatabaseContext db, IAgentEmployeeService agentEmployeeService)
        {
            this.AgentEmployeeService = agentEmployeeService;
            this.DB = db;
        }
        /// <summary>
        /// ข้อมูล AgentEmployee Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<AgentEmployeeDropdownDTO>))]
        public async Task<IActionResult> GetAgentEmployeesDropdownList([FromQuery]string name, [FromQuery]Guid? agentID)
        {
            try
            {
                var result = await AgentEmployeeService.GetAgentEmployeeDropdownListAsync(name, agentID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้องข้อมูล AgentEmployee
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<AgentEmployeeDTO>))]
        public async Task<IActionResult> GetAgentEmployeesList([FromQuery]AgentEmployeeFilter filter, [FromQuery]PageParam pageParam, [FromQuery]AgentEmployeeSortByParam sortByParam)
        {
            try
            {
                var result = await AgentEmployeeService.GetAgentEmployeeListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.AgentEmployees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AgentEmployeeDTO))]
        public async Task<IActionResult> GetAgentEmployee([FromRoute] Guid id)
        {
            try
            {
                var result = await AgentEmployeeService.GetAgentEmployeeAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูล AgentEmployee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AgentEmployeeDTO))]
        public async Task<IActionResult> CreateAgentEmployee([FromBody]AgentEmployeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgentEmployeeService.CreateAgentEmployeeAsync(input);
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
        /// แก้ไขข้อมูล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(AgentEmployeeDTO))]
        public async Task<IActionResult> EditAgentEmployee([FromRoute] Guid id, [FromBody]AgentEmployeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgentEmployeeService.UpdateAgentEmployeeAsync(id, input);
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
        /// ลบข้อมุล AgentEmployee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentEmployee([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await AgentEmployeeService.DeleteAgentEmployeeAsync(id);
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