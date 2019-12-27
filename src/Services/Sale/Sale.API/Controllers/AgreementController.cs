using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Inputs;
using Sale.Params.Outputs;
using Sale.Services;

namespace Sale.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AgreementController : BaseController
    {
        private readonly IAgreementService AgreementService;
        private readonly DatabaseContext DB;

        public AgreementController(IAgreementService AgreementService, DatabaseContext db)
        {
            this.AgreementService = AgreementService;
            this.DB = db;
        }

        /// <summary>
        /// อัพโหลดเอกสารการทำสัญญา
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns>AgreementFileDTO</returns>
        [HttpPost("CreateAgreementFileAsync")]
        [ProducesResponseType(200, Type = typeof(List<AgreementFileDTO>))]
        public async Task<IActionResult> CreateAgreementFileAsync([FromQuery]Guid id, [FromBody]List<FileDTO> input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    var result = await AgreementService.CreateAgreementFileAsync(id, input, userID);
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
        /// ลบไฟล์อัพโหลดเอกสารการทำสัญญา
        /// </summary>
        /// <param name="agreementFileID"></param>
        [HttpPost("DeleteAgreementFileAsync")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAgreementFileAsync([FromQuery]Guid agreementFileID )
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    AgreementService.DeleteAgreementFileAsync(agreementFileID, userID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// สร้างการแจ้งเตือนการอัพโหลดเอกสารสัญญา
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("CreateNotificationAgreementFileAsync")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateNotificationAgreementFileAsync([FromQuery]Guid id, [FromBody]string message)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    AgreementService.CreateNotificationAgreementFileAsync(id, message, userID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// ดูรายการเอกสารที่อัพโหลด
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of AgreementFileDTO</returns>
        [HttpGet("GetAgreementFileListAsync")]
        [ProducesResponseType(200,Type = typeof(List<AgreementFileDTO>))]
        public async Task<IActionResult> GetAgreementFileListAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementFileListAsync(id);
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
        /// การแสดงข้อมูลสัญญา
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AgreementDTO</returns>
        [HttpGet("GetAgreementAsync")]
        [ProducesResponseType(200,Type = typeof(AgreementDTO))]
        public async Task<IActionResult> GetAgreementAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementAsync(id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// ดึงข้อมูลสัญญาโดย Unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AgreementDTO</returns>
        [HttpGet("GetAgreementByUnitAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementDTO))]
        public async Task<IActionResult> GetAgreementByUnitAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementByUnitAsync(id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// เก็บข้อมูลจำนวนครั้งที่พิมพ์
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("CreateAgreementPrintingHistoryDataAsync")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateAgreementPrintingHistoryDataAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    AgreementService.CreateAgreementPrintingHistoryDataAsync(id, userID);
                    tran.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// การคืนค่างวดดาวน์ตาม Price List
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AgreementPriceListDTO</returns>
        [HttpGet("GetPriceListDataAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementPriceListDTO))]
        public async Task<IActionResult> GetPriceListDataAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {                                                                                                               
                try
                {
                    var result = await AgreementService.GetPriceListDataAsync(id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// การระบุข้อมูลเงื่อนไขการชำระเงิน
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AgreementPriceListDTO</returns>
        [HttpGet("GetAgreementPriceListAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementPriceListDTO))]
        public async Task<IActionResult> GetAgreementPriceListAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementPriceListAsync(id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// การแสดงข้อมูลรายการงวดดาวน์
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of AgreementPriceListDTO</returns>
        [HttpGet("GetAgreementInstallmentDataAsync")]
        [ProducesResponseType(200, Type = typeof(List<AgreementInstallmentDTO>))]
        public async Task<IActionResult> GetAgreementInstallmentDataAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementInstallmentDataAsync(id);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// การคำนวนงวดเงินดาวน์ตามข้อมูลที่ระบุ
        /// </summary>
        /// <param name="agreementPriceListDTO"></param>
        /// <returns>AgreementPriceListDTO</returns>
        [HttpGet("CalculateAgreementInstallmentDataAsync")]
        [ProducesResponseType(200, Type = typeof(AgreementPriceListDTO))]
        public async Task<IActionResult> CalculateAgreementInstallmentDataAsync([FromBody]AgreementPriceListDTO agreementPriceListDTO)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.CalculateAgreementInstallmentDataAsync(agreementPriceListDTO);
                    tran.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// การแสดงรายการสัญญา/คนหารายการสัญญา
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns>AgreementPriceListDTO</returns>
        [HttpGet("GetAgreementListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<AgreementListDTO>))]
        public async Task<IActionResult> GetAgreementListAsync([FromQuery]AgreementListFilter filter, [FromQuery] PageParam pageParam, [FromQuery] AgreementListSortByParam sortByParam)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await AgreementService.GetAgreementListAsync(filter, pageParam, sortByParam);
                    tran.Commit();
                    AddPagingResponse(result.PageOutput);
                    return Ok(result.Agreements);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

    }
}
