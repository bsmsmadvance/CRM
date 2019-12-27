using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using MasterData.API.Extensions;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        private readonly DatabaseContext DB;
        private IBrandService BrandService;

        public BrandsController(IBrandService brandService, DatabaseContext db)
        {
            this.BrandService = brandService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสของแบรนด์ Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BrandDropdownDTO>))]
        public async Task<IActionResult> GetBrandDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await BrandService.GetBrandDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของแบรนด์
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BrandDTO>))]
        public async Task<IActionResult> GetBrandList([FromQuery]BrandFilter filter, [FromQuery]PageParam pageParam , [FromQuery]BrandSortByParam sortByParam)
        {
            try
            {
                var result = await BrandService.GetBrandListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Brands);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BrandDTO))]
        public async Task<IActionResult> GetBrand([FromRoute] Guid id)
        {
            try
            {
                var result = await BrandService.GetBrandAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างแบรนด์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BrandDTO))]
        public async Task<IActionResult> CreateBrand([FromBody]BrandDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BrandService.CreateBrandAsync(input);
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
        /// แก้ไขแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BrandDTO))]
        public async Task<IActionResult> EditBrand([FromRoute] Guid id,[FromBody]BrandDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BrandService.UpdateBrandAsync(id, input);
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
        /// ลบแบรนด์
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BrandService.DeleteBrandAsync(id);
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
