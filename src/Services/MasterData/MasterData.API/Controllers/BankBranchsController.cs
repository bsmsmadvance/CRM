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
using Microsoft.Extensions.Logging;
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
    public class BankBranchsController : BaseController
    {
        private readonly IBankBranchService BankBranchService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly DatabaseContext DB;

        public BankBranchsController(IBankBranchService bankBranchService, ILogger<BankBranchsController> logger, DatabaseContext db)
        {
            this.BankBranchService = bankBranchService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// ดึง Dropdown สาขาธนาคาร
        /// </summary>
        /// <returns>The bank branch list.</returns>
        /// <param name="bankID">Bank identifier.</param>
        /// <param name="name">Name.</param>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BankBranchDropdownDTO>))]
        public async Task<IActionResult> GetBankBranchDropdownList([FromQuery]Guid bankID, [FromQuery]string name, [FromQuery]Guid? provinceID = null)
        {
            try
            {
                var results = await BankBranchService.GetBankBrachDropdownListAsync(bankID, name, provinceID);
                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสของสาขาธนาคาร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BankBranchDTO>))]
        public async Task<IActionResult> GetBankBranchList([FromQuery]BankBranchFilter filter, 
            [FromQuery]PageParam pageParam,
            [FromQuery]BankBranchSortByParam sortByParam)
        {
            try
            {
                var result = await BankBranchService.GetBankBranchListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.BankBranches);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// ข้อมูลสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankBranchDTO))]
        public async Task<IActionResult> GetBankBranch([FromRoute]Guid id)
        {
            try
            {
                var result = await BankBranchService.GetBankBranchAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// สร้างสาขาธนาคาร
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BankBranchDTO))]
        public async Task<IActionResult> CreateBankBranch([FromBody]BankBranchDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankBranchService.CreateBankBranchAsync(input);
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
        /// แก้ไขสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BankBranchDTO))]
        public async Task<IActionResult> EditBankBranch([FromRoute]Guid id, [FromBody]BankBranchDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankBranchService.UpdateBankBranchAsync(id, input);
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
        /// ลบสาขาธนาคาร
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankBranch([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BankBranchService.DeleteBankBranchAsync(id);
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