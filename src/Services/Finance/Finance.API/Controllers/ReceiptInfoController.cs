using Base.DTOs.FIN;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Finance.Params.Filters;
using Finance.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ReceiptInfoController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IReceiptInfoService ReceiptInfoService;
        public ReceiptInfoController(IReceiptInfoService ReceiptInfoService, DatabaseContext db)
        {
            this.ReceiptInfoService = ReceiptInfoService;
            DB = db;
        }


        /// <summary>
        /// /ReceiptInfo/GetReceiptInfoListAsync
        /// ReceiptInfo
        /// </summary>
        [HttpGet("GetReceiptInfoListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ReceiptInfoDTO>))]
        public async Task<IActionResult> GetReceiptInfoListAsync([FromQuery]ReceiptInfoFilter filter, [FromQuery]PageParam pageParam, [FromQuery]ReceiptInfoSortByParam sortByParam)
        {
            var result = await ReceiptInfoService.GetReceiptInfoListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.ReceiptInfos);
        }


        /// <summary>
        /// /ReceiptInfo/UpdateReceiptInfoAsync
        /// แก้ไขรายการ ReceiptInfo
        /// </summary>
        [HttpPost("UpdateReceiptInfo")]
        public async Task<IActionResult> UpdateReceiptInfoAsync([FromBody]ReceiptInfoDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ReceiptInfoService.UpdateReceiptInfoAsync(input);
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
        /// /ReceiptInfo/DeleteReceiptInfoAsync
        /// ลบรายการ  ReceiptInfo
        /// </summary>
        [HttpPost("DeleteReceiptInfoAsync")]
        public async Task<IActionResult> DeleteReceiptInfoAsync([FromBody]ReceiptInfoDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await ReceiptInfoService.DeleteReceiptInfoAsync(input);
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


    }
}
