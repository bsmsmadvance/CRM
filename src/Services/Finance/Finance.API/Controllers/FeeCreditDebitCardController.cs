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
using System.Linq;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class FeeCreditDebitCardController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IFeeCreditDebitCardService FeeCreditDebitCardService;
 
        public FeeCreditDebitCardController(IFeeCreditDebitCardService feeCreditDebitCardService, DatabaseContext db, IConfiguration configuration)
        {
            this.FeeCreditDebitCardService = feeCreditDebitCardService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงข้อมูลรายการการรับชำระเงินบัตรเครดิต & บัตรเดบิต มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFeeCreditDebitCardList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<FeeCreditDebitCardDTO>))]
        public async Task<IActionResult> GetFeeCreditDebitCardList(FeeCreditDebitCardFilter filter, PageParam pageParam, FeeCreditDebitCardSortByParam sortByParam)
        {
            var result = await FeeCreditDebitCardService.GetFeeCreditDebitCardListAsync( filter, pageParam, sortByParam);
            AddPagingResponse(result.PageOutput);
            return Ok(result.FeeCreditDebitCards);
        }
        /// <summary>
        /// บันทึกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateFeeConfirm")]
        public async Task<IActionResult> UpdateFeeConfirm([FromBody]List<FeeCreditDebitCardDTO> input)
        {

            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }
                    await FeeCreditDebitCardService.UpdateFeeConfirmAsync(input, userID);
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
        /// บันทึกยกเลิกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        [HttpPut("CancelFeeConfirm")]
        public async Task<IActionResult> CancelFeeConfirm([FromBody]List<FeeCreditDebitCardDTO> input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    //Get user ID
                    Guid? userID = null;
                    Guid parsedUserID;
                    if (Guid.TryParse(HttpContext?.User?.Claims.Where(x => x.Type == "userid").Select(o => o.Value).SingleOrDefault(), out parsedUserID))
                    {
                        userID = parsedUserID;
                    }

                    await FeeCreditDebitCardService.CancelFeeConfirmAsync(input, userID);
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
    }
}
