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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : BaseController
    {
        private IBankService BankService;
        private readonly DatabaseContext DB;

        public BanksController(IBankService bankService,DatabaseContext db)
        {
            this.BankService = bankService;
            this.DB=db;
        }
        /// <summary>
        /// ลิสข้อมูลธนาคาร Dropdown
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BankDropdownDTO>))]
        public async Task<IActionResult> GetBankDropdownList([FromQuery]string name)
        {
            try
            {
                var result = await BankService.GetBankDropdownListAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของธนาคาร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200,Type =typeof(List<BankDTO>))]
        public async Task<IActionResult> GetBankList([FromQuery]BankFilter filter, [FromQuery]PageParam pageParam, [FromQuery]BankSortByParam sortByParam)
        {
            try
            {
                var result = await BankService.GetBankListAsync(filter, pageParam,sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.Banks);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูลธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankDTO))]
        public async Task<IActionResult> GetBank([FromRoute] Guid id)
        {
            try
            {
                var result = await BankService.GetBankAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างธนาคาร
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BankDTO))]
        public async Task<IActionResult> CreateBank([FromBody]BankDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankService.CreateBankAsync(input);
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
        /// แก้ไขข้อมูลธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BankDTO))]
        public async Task<IActionResult> EditBank([FromRoute] Guid id,[FromBody]BankDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankService.UpdateBankAsync(id, input);
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
        /// ลบธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BankService.DeleteBankAsync(id);
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