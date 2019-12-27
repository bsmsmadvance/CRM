using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Database.Models;
using MasterData.Params.Filters;
using MasterData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace MasterData.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseController
    {
        private readonly ICountryService CountryService;
        private readonly DatabaseContext DB;
        public CountriesController(ICountryService countryService, DatabaseContext db)
        {
            this.CountryService = countryService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสของประเทศ Dropdown
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<CountryDTO>))]
        public async Task<IActionResult> GetCountryDropdownList([FromQuery]CountryFilter filter)
        {
            try
            {
                var results = await CountryService.GetCountryDropdownListAsync(filter);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ลิสของ ข้อมูลประเทศ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<CountryDTO>))]
        public async Task<IActionResult> GetCountryList([FromQuery]CountryFilter filter, [FromQuery]PageParam pageParam, [FromQuery]CountrySortByParam sortByParam)
        {
            try
            {
                var result = await CountryService.GetCountryListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        public async Task<IActionResult> GetCountry([FromRoute] Guid id)
        {
            try
            {
                var result = await CountryService.GetCountryAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// หาประเทศจาก code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        public async Task<IActionResult> FindCountry([FromQuery] string code)
        {
            try
            {
                var result = await CountryService.FindCountryAsync(code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้าง ข้อมูลประเทศ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        public async Task<IActionResult> CreateCountry([FromBody]CountryDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CountryService.CreateCountryAsync(input);
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
        /// แก้ไข ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        public async Task<IActionResult> EditCountry([FromRoute] Guid id, [FromBody]CountryDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CountryService.UpdateCountryAsync(id, input);
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
        /// ลบ ข้อมูลประเทศ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await CountryService.DeleteCountryAsync(id);
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
