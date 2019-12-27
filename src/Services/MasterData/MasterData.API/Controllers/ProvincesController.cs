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

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : BaseController
    {
        private IProvinceService ProvinceService;
        private readonly DatabaseContext DB;

        public ProvincesController(IProvinceService provinceService, DatabaseContext db)
        {
            this.ProvinceService = provinceService;
            this.DB = db;
        }

        /// <summary>
        /// หาจังหวัดจากชื่อ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Find")]
        [ProducesResponseType(200, Type = typeof(ProvinceListDTO))]
        public async Task<IActionResult> FindProvince([FromQuery]string name)
        {
            try
            {
                var result = await ProvinceService.FindProvinceAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ลิส ข้อมูลจังหวัด Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ProvinceListDTO>))]
        public async Task<IActionResult> GetProvinceDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await ProvinceService.GetProvinceDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิส ข้อมูลจังหวัด
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ProvinceDTO>))]
        public async Task<IActionResult> GetProvinceList([FromQuery]ProvinceFilter filter
            , [FromQuery]PageParam pageParam
            , [FromQuery]ProvinceSortByParam sortByParam)
        {
            try
            {
                var result = await ProvinceService.GetProvinceListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Provinces);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProvinceDTO))]
        public async Task<IActionResult> GetProvince([FromRoute] Guid id)
        {
            try
            {
                var result = await ProvinceService.GetProvinceAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ข้อมูลจังหวัด
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [HttpGet("PostalCode")]
        [ProducesResponseType(200, Type = typeof(ProvinceDTO))]
        public async Task<IActionResult> GetProvincePostalCodeAsync([FromQuery] string postalCode)
        {
            try
            {
                var result = await ProvinceService.GetProvincePostalCodeAsync(postalCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างข้อมูลจังหวัด
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProvinceDTO))]
        public async Task<IActionResult> CreateProvince([FromBody]ProvinceDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProvinceService.CreateProvinceAsync(input);
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
        /// แก้ไขข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ProvinceDTO))]
        public async Task<IActionResult> EditProvince([FromRoute] Guid id, [FromBody]ProvinceDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProvinceService.UpdateProvinceAsync(id, input);
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
        /// ลบข้อมูลจังหวัด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvince([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProvinceService.DeleteProvinceAsync(id);
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