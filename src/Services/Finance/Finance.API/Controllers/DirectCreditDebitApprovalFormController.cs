using Base.DTOs;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
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
    public class DirectCreditDebitApprovalFormController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IDirectCreditDebitApprovalFormService DirectCreditDebitApprovalFormService;
        private readonly IConfiguration Configuration;

        public DirectCreditDebitApprovalFormController(IDirectCreditDebitApprovalFormService directCreditDebitApprovalFormService, DatabaseContext db, IConfiguration Configuration, IConfiguration configuration)
        {
            this.DirectCreditDebitApprovalFormService = directCreditDebitApprovalFormService;
            this.Configuration = configuration;
            this.DB = db;
        }

        // <summary>
        // /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormAsync
        // ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("GetDirectCreditDebitApprovalFormAsync")]
        [ProducesResponseType(200, Type = typeof(DirectCreditDebitApprovalFormDTO))]
        public async Task<IActionResult> GetDirectCreditDebitApprovalFormAsync(Guid? id)
        {
            var result = await DirectCreditDebitApprovalFormService.GetDirectCreditDebitApprovalFormAsync(id);
            return Ok(result);
        }

        // <summary>
        // /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormAsync
        // ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("getDataCreateAsync")]
        [ProducesResponseType(200, Type = typeof(DirectCreditDebitApprovalFormDTO))]
        public async Task<IActionResult> getDataCreateAsync(Guid? id)
        {
            var result = await DirectCreditDebitApprovalFormService.getDataCreateAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// /DirectCreditDebitApprovalFormService/GetDirectCreditDebitApprovalFormExpire3MonthsListAsync
        /// ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetDirectCreditDebitApprovalFormExpire3MonthsListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DirectCreditDebitApprovalFormDTO>))]
        public async Task<IActionResult> GetDirectCreditDebitApprovalFormExpire3MonthsListAsync([FromQuery]DirectCreditDebitApprovalFormFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DirectCreditDebitApprovalFormSortByParam sortByParam)
        {
            var result = await DirectCreditDebitApprovalFormService.GetDirectCreditDebitApprovalFormExpire3MonthsListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.DirectCreditDebitApprovalForms);
        }

        /// <summary>
        /// /DirectCreditDebitApprovalFormService/GetDepositListAsync
        /// ดึงข้อมูลรายการมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetDirectCreditDebitApprovalFormListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DirectCreditDebitApprovalFormDTO>))]
        public async Task<IActionResult> GetDirectCreditDebitApprovalFormListAsync([FromQuery]DirectCreditDebitApprovalFormFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DirectCreditDebitApprovalFormSortByParam sortByParam)
        {
            var result = await DirectCreditDebitApprovalFormService.GetDirectCreditDebitApprovalFormListAsync(filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.DirectCreditDebitApprovalForms);
            // return Ok(result);
        }


        /// <summary>
        /// /DirectCreditDebitApprovalFormService/createdepositasync
        /// เพิ่มข้อมูล #insert
        /// ui: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input"></param>
        [HttpPut("CreateDirectCreditDebitApprovalFormAsync")]
        [ProducesResponseType(200, Type = typeof(DirectCreditDebitApprovalFormDTO))]
        public async Task<IActionResult> CreateDirectCreditDebitApprovalFormAsync([FromBody]DirectCreditDebitApprovalFormDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DirectCreditDebitApprovalFormService.CreateDirectCreditDebitApprovalFormAsync(input);
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

        /// <summary>
        /// /DirectCreditDebitApprovalFormService/UpdateDepositAsync
        /// บันทึกการแก้ไข #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("UpdateDirectCreditDebitApprovalFormAsync")]
        [ProducesResponseType(200, Type = typeof(DirectCreditDebitApprovalFormDTO))]
        public async Task<IActionResult> UpdateDirectCreditDebitApprovalFormAsync([FromBody]DirectCreditDebitApprovalFormDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DirectCreditDebitApprovalFormService.UpdateDirectCreditDebitApprovalFormAsync(input);
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
        // 
        // 
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("GetBankDirectDebitDropdowListAsync")]
        [ProducesResponseType(200, Type = typeof(List<BankAccNameDTO>))]
        public async Task<IActionResult> GetBankDirectDebitDropdowListAsync([FromQuery]Guid? ComID)
        {
            var result = await DirectCreditDebitApprovalFormService.GetBankDirectDebitDropdowListAsync(ComID);
            return Ok(result);
        }

        // <summary>
        // 
        // 
        // UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        // </summary>
        [HttpGet("GetBankDirectCreditDropdowListAsync")]
        [ProducesResponseType(200, Type = typeof(List<BankAccNameDTO>))]
        public async Task<IActionResult> GetBankDirectCreditDropdowListAsync([FromQuery]Guid? ComID)
        {
            var result = await DirectCreditDebitApprovalFormService.GetBankDirectCreditDropdowListAsync(ComID);
            return Ok(result);
        }


        [HttpGet("StatusDropdowList")]
        [ProducesResponseType(200, Type = typeof(List<MasterCenterDropdownDTO>))]
        public async Task<IActionResult> GetStatusDropdowListAsync()
        {
            var result = await DirectCreditDebitApprovalFormService.GetStatusDropdowListAsync();
            return Ok(result);
        }


        /// <summary>
        /// /DirectCreditDebitApprovalFormService/GetDepositListAsync
        /// ดึงข้อมูลรายการมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("GetUnitListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<AgreementNoTransferDTO>))]
        public async Task<IActionResult> GetUnitListAsync([FromQuery]Guid? id,[FromQuery]DirectCreditDebitApprovalFormFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DirectCreditDebitApprovalFormSortByParam sortByParam)
        {
            var result = await DirectCreditDebitApprovalFormService.GetUnitListAsync(id, filter,pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.AgreementNoTransfer);
        }


        /// <summary>
        /// /DirectCreditDebitApprovalFormService/GetDepositListAsync
        /// 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpPost("PrintDirectCreditDebitApprovalFormAsync")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> PrintDirectCreditDebitApprovalFormAsync([FromBody]List<DirectCreditDebitApprovalFormDTO> ListDirectCreditDebitApprovalForm)
        {
            var result = await DirectCreditDebitApprovalFormService.PrintDirectCreditDebitApprovalFormAsync(ListDirectCreditDebitApprovalForm);
            return Ok(result);
        }
        /// <summary>
        /// /DirectCreditDebitApprovalFormService/GetDepositListAsync
        /// 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("ExportRequestAsync")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportRequestAsync([FromQuery]bool Is3Month, [FromQuery]DirectCreditDebitApprovalFormFilter filter)
        {
            var result = await DirectCreditDebitApprovalFormService.ExportRequestAsync(Is3Month, filter);
            return Ok(result);
        }





    }
}
