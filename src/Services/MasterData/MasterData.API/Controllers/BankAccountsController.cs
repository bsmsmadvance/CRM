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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    public class BankAccountsController : BaseController
    {
        private readonly IBankAccountService BankAccountService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly DatabaseContext DB;

        public BankAccountsController(IBankAccountService bankAccountService, ILogger<BankBranchsController> logger, DatabaseContext db)
        {
            this.BankAccountService = bankAccountService;
            this.Logger = logger;
            this.DB = db;
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<BankAccountDropdownDTO>))]
        public async Task<IActionResult> GetBankAccountDropdownList([FromQuery]string displayName,[FromQuery]string bankAccountTypeKey, [FromQuery]Guid? companyID)
        {
            try
            {
                var results = await BankAccountService.GetBankAccountDropdownListAsync(displayName, bankAccountTypeKey, companyID);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงข้อมูลบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<BankAccountDTO>))]
        public async Task<IActionResult> GetBankAccountList([FromQuery]BankAccountFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]BankAccountSortByParam sortByParam)
        {
            try
            {
                var result = await BankAccountService.GetBankAccountListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.BankAccounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายละเอียดบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account detail.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(List<BankAccountDTO>))]
        public async Task<IActionResult> GetBankAccountDetail([FromRoute]Guid id)
        {
            try
            {
                var result = await BankAccountService.GetBankAccountDetailAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BankAccountDTO))]
        public async Task<IActionResult> CreateBankAccount([FromBody]BankAccountDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankAccountService.CreateBankAccountAsync(input);
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
        /// สร้าง ข้อมูลคู่บัญชี
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="input">Input.</param>
        [HttpPost("ChartOfAccount")]
        [ProducesResponseType(200, Type = typeof(BankAccountDTO))]
        public async Task<IActionResult> CreateChartOfAccount([FromBody]BankAccountDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankAccountService.CreateChartOfAccountAsync(input);
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
        /// แก้ไขบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccountDTO))]
        public async Task<IActionResult> UpdateBankAccount([FromRoute]Guid id, [FromBody]BankAccountDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankAccountService.UpdateBankAccountAsync(id, input);
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
        /// แก้ไข ข้อมูลคู่บัญชี
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("ChartOfAccount/{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccountDTO))]
        public async Task<IActionResult> UpdateChartOfAccount([FromRoute]Guid id, [FromBody]BankAccountDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await BankAccountService.UpdateChartOfAccountAsync(id, input);
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
        /// ลบบัญชีธนาคาร
        /// </summary>
        /// <returns>The bank account.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BankAccountService.DeleteBankAccountAsync(id);
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
        /// <summary>
        /// ลบบัญชีธนาคาร ทีละหลายรายการ
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("MultipleDelete")]
        public async Task<IActionResult> DeleteBankAccountList([FromBody]List<BankAccountDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await BankAccountService.DeleteBankAccountListAsync(inputs);
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
