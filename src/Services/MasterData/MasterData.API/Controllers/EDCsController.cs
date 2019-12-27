using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using MasterData.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Report.Integration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterData.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    public class EDCsController : BaseController
    {
        private readonly IEDCService EDCService;
        private readonly ILogger<BankBranchsController> Logger;
        private readonly DatabaseContext DB;

        public EDCsController(IEDCService edcService, ILogger<BankBranchsController> logger, DatabaseContext db)
        {
            this.EDCService = edcService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตรแบบ dropdown
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="bankName"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<EDCDropdownDTO>))]
        public async Task<IActionResult> GetEDCDropdownList([FromQuery]Guid? projectID = null, [FromQuery]string bankName = null)
        {
            try
            {
                var results = await EDCService.GetEDCDropdownListUrlAsync(projectID, bankName);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงข้อมูลเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCL ist.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<EDCDTO>))]
        public async Task<IActionResult> GetEDCList([FromQuery]EDCFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]EDCSortByParam sortByParam)
        {
            try
            {
                var result = await EDCService.GetEDCListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.EDCs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงรายละเอียดเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCD etail.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(List<EDCDTO>))]
        public async Task<IActionResult> GetEDCDetail([FromRoute]Guid id)
        {
            try
            {
                var result = await EDCService.GetEDCDetailAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(EDCDTO))]
        public async Task<IActionResult> CreateEDC([FromBody]EDCDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await EDCService.CreateEDCAsync(input);
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
        /// แก้ไขเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(EDCDTO))]
        public async Task<IActionResult> UpdateEDC([FromRoute]Guid id, [FromBody]EDCDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await EDCService.UpdateEDCAsync(id, input);
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
        /// ลบเครื่องรูดบัตร
        /// </summary>
        /// <returns>The edc.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEDC([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await EDCService.DeleteEDCAsync(id);
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
        /// ดึงธนาคารเครื่องรูดบัตร
        /// </summary>
        /// <returns>The EDCB ank list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("Banks")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<EDCBankDTO>))]
        public async Task<IActionResult> GetEDCBankList([FromQuery]EDCBankFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]EDCBankSortByParam sortByParam)
        {
            try
            {
                var result = await EDCService.GetEDCBankListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.EDCBanks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("Fees")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<EDCFeeDTO>))]
        public async Task<IActionResult> GetEDCFeeList([FromQuery]EDCFeeFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]EDCFeeSortByParam sortByParam)
        {
            try
            {
                var result = await EDCService.GetEDCFeeListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.EDCFees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="input">Input.</param>
        [HttpPost("Fees")]
        [ProducesResponseType(200, Type = typeof(EDCFeeDTO))]
        public async Task<IActionResult> CreateEDCFee([FromBody]EDCFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await EDCService.CreateEDCFeeAsync(input);
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
        /// แก้ไขค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("Fees/{id}")]
        [ProducesResponseType(200, Type = typeof(EDCFeeDTO))]
        public async Task<IActionResult> UpdateEDCFee([FromRoute]Guid id, [FromBody]EDCFeeDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await EDCService.UpdateEDCFeeAsync(id, input);
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
        /// ลบค่าธรรมเนียมบัตร
        /// </summary>
        /// <returns>The EDCF ee.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("Fees/{id}")]
        public async Task<IActionResult> DeleteEDCFee([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await EDCService.DeleteEDCFeeAsync(id);
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
        /// คำนวณค่าธรรมเนียม
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="creditCardTypeMasterCenterID"></param>
        /// <param name="creditCardPaymentTypeMasterCenterID"></param>
        /// <param name="paymentCardTypeMasterCenterID"></param>
        /// <param name="payAmount"></param>
        /// <returns></returns>
        [HttpGet("{id}/Fees/Calculate")]
        [ProducesResponseType(200, Type = typeof(DecimalResult))]
        public async Task<IActionResult> GetFee([FromRoute]Guid id, [FromQuery]Guid bankID, [FromQuery]Guid creditCardTypeMasterCenterID, [FromQuery]Guid creditCardPaymentTypeMasterCenterID, [FromQuery]Guid paymentCardTypeMasterCenterID, [FromQuery]decimal payAmount)
        {
            var result = await EDCService.GetFeeAsync(id, bankID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID, payAmount);
            return Ok(new DecimalResult()
            {
                Result = result
            });
        }

        /// <summary>
        /// คำนวณค่าธรรมเนียม
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="creditCardTypeMasterCenterID"></param>
        /// <param name="creditCardPaymentTypeMasterCenterID"></param>
        /// <param name="paymentCardTypeMasterCenterID"></param>
        /// <returns></returns>
        [HttpGet("{id}/Fees/Percent")]
        [ProducesResponseType(200, Type = typeof(DecimalResult))]
        public async Task<IActionResult> GetFeePercent([FromRoute]Guid id, [FromQuery]Guid bankID, [FromQuery]Guid creditCardTypeMasterCenterID, [FromQuery]Guid creditCardPaymentTypeMasterCenterID, [FromQuery]Guid paymentCardTypeMasterCenterID)
        {
            var result = await EDCService.GetFeePercentAsync(id, bankID, creditCardTypeMasterCenterID, creditCardPaymentTypeMasterCenterID, paymentCardTypeMasterCenterID);
            return Ok(new DoubleResult()
            {
                Result = result
            });
        }

        /// <summary>
        /// ดึง url สำหรับ export ตารางเครื่องรูดบัตร
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="downloadAs">0=excel, 1=showpdf</param>
        /// <returns></returns>
        [HttpGet("ExportEDCListUrl")]
        [ProducesResponseType(200, Type = typeof(ReportResult))]
        public async Task<IActionResult> ExportEDCListUrl([FromQuery]EDCFilter filter, [FromQuery]ShowAs downloadAs)
        {
            var result = await EDCService.ExportEDCListUrlAsync(filter, downloadAs);
            return Ok(result);
        }

    }
}
