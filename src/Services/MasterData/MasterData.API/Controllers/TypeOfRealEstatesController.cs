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
    public class TypeOfRealEstatesController : BaseController
    {
        private ITypeOfRealEstateService TypeOfRealEstateService;
        private readonly DatabaseContext DB;

        public TypeOfRealEstatesController(ITypeOfRealEstateService typeOfRealEstateService, DatabaseContext db)
        {
            this.TypeOfRealEstateService = typeOfRealEstateService;
            this.DB = db;
        }
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<TypeOfRealEstateDropdownDTO>))]
        public async Task<IActionResult> GetTypeOfRealEstateDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await TypeOfRealEstateService.GetTypeOfRealEstateDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<TypeOfRealEstateDTO>))]
        public async Task<IActionResult> GetTypeOfRealEstateList([FromQuery]TypeOfRealEstateFilter filter
            , [FromQuery]PageParam pageParam
            , [FromQuery]TypeOfRealEstateSortByParam sortByParam)
        {
            try
            {
                var result = await TypeOfRealEstateService.GetTypeOfRealEstateListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.TypeOfRealEstates);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TypeOfRealEstateDTO))]
        public async Task<IActionResult> GetTypeOfRealEstate([FromRoute] Guid id)
        {
            try
            {
                var result = await TypeOfRealEstateService.GetTypeOfRealEstateAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TypeOfRealEstateDTO))]
        public async Task<IActionResult> CreateTypeOfRealEstate([FromBody]TypeOfRealEstateDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TypeOfRealEstateService.CreateTypeOfRealEstateAsync(input);
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
        /// แก้ไขข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(TypeOfRealEstateDTO))]
        public async Task<IActionResult> EditTypeOfRealEstate([FromRoute] Guid id, [FromBody]TypeOfRealEstateDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TypeOfRealEstateService.UpdateTypeOfRealEstateAsync(id, input);
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
        /// ลบข้อมูลประเภทบ้าน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfRealEstate([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await TypeOfRealEstateService.DeleteTypeOfRealEstateAsync(id);
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