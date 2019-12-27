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
using Microsoft.AspNetCore.Cors;
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
    public class CompaniesController : BaseController
    {
        private ICompanyService CompanyService;
        private readonly DatabaseContext DB;
        public CompaniesController(ICompanyService companyService, DatabaseContext db)
        {
            this.CompanyService = companyService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสของบริษัท Dropdown
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<CompanyDropdownDTO>))]
        public async Task<IActionResult> GetCompanyDropdownList([FromQuery]CompanyDropdownFilter filter)
        {
            try
            {
                var results = await CompanyService.GetCompanyDropdownListAsync(filter);
                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของบริษัท
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CompanyDTO>))]
        public async Task<IActionResult> GetCompanyList([FromQuery]CompanyFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CompanySortByParam sortByParam)
        {
            try
            {
                var result = await CompanyService.GetCompanyListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Companies);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลบริษัท
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetCompany([FromRoute] Guid id)
        {
            try
            {
                var result = await CompanyService.GetCompanyAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างบริษัท
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CompanyService.CreateCompanyAsync(input);
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
        /// แก้ไขบริษัท
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        public async Task<IActionResult> EditCompany([FromRoute] Guid id, [FromBody]CompanyDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CompanyService.UpdateCompanyAsync(id, input);
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
        /// ลบบริษัท
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await CompanyService.DeleteCompanyAsync(id);
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