using Base.DTOs;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models;
using Finance.Params.Filters;
using Finance.Params.Outputs;
using Finance.Services.IService;
using Finance.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DirectCreditDebitExportController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IDirectCreditDebitExportService DirectCreditDebitExportService;
        private readonly IConfiguration Configuration;
        //public DirectCreditDebitApprovalFormController(IDirectCreditDebitApprovalFormService directCreditDebitApprovalFormService, DatabaseContext db, IConfiguration Configuration, IConfiguration configuration)
        //{
        //    this.DirectCreditDebitApprovalFormService = directCreditDebitApprovalFormService;
        //    this.Configuration = configuration;
        //    this.DB = db;
        //}
        public DirectCreditDebitExportController(IDirectCreditDebitExportService directCreditDebitExportService, DatabaseContext db, IConfiguration configuration)
        {
            this.DirectCreditDebitExportService = directCreditDebitExportService;
            this.Configuration = configuration;
            this.DB = db;
        }

        // <summary>
        // /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormAsync
        // 
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("GetDirectCreditDebitExportListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DirectCreditDebitExportHeaderDTO>))]
        public async Task<IActionResult> GetDirectCreditDebitExportListAsync(DirectCreditDebitExportHeaderFilter filter, PageParam pageParam, DirectCreditDebitExportHeaderSortByParam sortByParam)
        {
            var result = await DirectCreditDebitExportService.GetDirectCreditDebitExportListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.DirectCreditDebitExportHeaders);
        }

        // <summary>
        // /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormAsync
        // 
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("GetDirectCreditDebitExportAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DirectCreditDebitExportHeaderDTO>))]
        public async Task<IActionResult> GetDirectCreditDebitExportAsync(Guid id, DirectCreditDebitExportDetailFilter filter, PageParam pageParam, DirectCreditDebitExportDetailSortByParam sortByParam)
        {
            var result = await DirectCreditDebitExportService.GetDirectCreditDebitExportAsync(id, filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.DirectCreditDebitExportHeader);
        }

        // <summary>
        // /DirectCreditDebitApprovalFormService/CreateDirectCreditDebitExportAsync
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        /// <param name="input">DirectCreditDebitExportDTO</param>

        [HttpPut("CreateDirectCreditDebitExportAsync")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> CreateDirectCreditDebitExportAsync([FromBody]DirectCreditDebitExportDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DirectCreditDebitExportService.CreateDirectCreditDebitExportAsync(input);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        // <summary>
        // /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormAsync
        // ChkKeyDirectCreditDebit 1 = Credit // 2 = Debit
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        /// <param name="ComID">Company ID</param>
        /// <param name="BankID">Bank ID</param>
        /// <param name="ChkKeyDirectCreditDebit">[string] 1 = Credit // 2 = Debit</param>
        [HttpGet("BankID")]
        [ProducesResponseType(200, Type = typeof(List<BankAccountDropdownDTO>))]
        public async Task<IActionResult> GetBankAccountDropdowListAsync(Guid ComID, Guid BankID, string ChkKeyDirectCreditDebit)
        {
            var result = await DirectCreditDebitExportService.GetBankAccountDropdowListAsync(ComID, BankID, ChkKeyDirectCreditDebit);
            return Ok(result);
        }


        /// <summary>
        /// ลบ DirectCreditDebitExport
        /// </summary>
        /// <param name="id">Header ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectCreditDebitExportAsync([FromRoute] Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await DirectCreditDebitExportService.DeleteDirectCreditDebitExportAsync(id);
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
        /// ลบ DirectCreditDebitExport
        /// </summary>
        /// <param name="ID">Header ID</param>
        /// <param name="fileDTO">Link text file</param>
        /// <returns></returns>
        [HttpPut("ImportDirectCreditDebitAsync")]
        [ProducesResponseType(200, Type = typeof(List<DirectCreditDebitExportHeaderDTO>))]
        public async Task<IActionResult> ImportDirectCreditDebitAsync([FromBody]FileWithIDDTO fileDTO)
        {
            var result = await DirectCreditDebitExportService.ImportDirectCreditDebitAsync(fileDTO);
            return Ok(result);
        }

        /// <summary>
        /// List<DirectCreditDebitExportHeaderDTO>
        /// </summary>
        /// <param name="ID">Header ID</param>
        /// <param name="fileDTO">Link text file</param>
        /// <returns></returns>
        [HttpPut("ConfirmPaymentAsync")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ConfirmPaymentAsync([FromBody]List<DirectCreditDebitExportHeaderDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await DirectCreditDebitExportService.ConfirmPaymentAsync(inputs);
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
