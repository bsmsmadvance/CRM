using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Database.Models;
using Finance.Params.Filters;
using Finance.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;

namespace Finance.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DepositsController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IDepositService DepositService;
        public DepositsController(IDepositService depositService, DatabaseContext db)
        {
            DepositService = depositService;
            DB = db;
        }

        /// <summary>
        /// /Deposits/GetDepositListAsync
        /// ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> GetDepositListAsync([FromQuery]DepositFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DepositSortByParam sortByParam)
        {
            var result = await DepositService.GetDepositListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.DepositDetails);
        }

        /// <summary>
        /// /Deposits/GetDepositListAsync
        /// ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        [HttpGet("ListForUpdate")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> GetDepositListForUpdateAsync([FromQuery]Guid id, [FromQuery]List<Guid> listNewId, [FromQuery]DepositFilter filter, [FromQuery]PageParam pageParam, [FromQuery]DepositSortByParam sortByParam)
        {
            var result = await DepositService.GetDepositListForUpdateAsync(id, listNewId, filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.DepositDetails);
        }


        /// <summary>
        /// /Deposits/GetDepositAsync
        /// ดึงข้อมูลรายละเอียดการนำฝาก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367435/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> GetDepositAsync([FromRoute]Guid id)
        {
            var result = await DepositService.GetDepositAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// /Deposits/CreateDepositAsync
        /// เพิ่มข้อมูลการนำฝาก #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> CreateDepositAsync([FromBody]List<DepositDetailDTO> input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DepositService.CreateDepositAsync(input);
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
        /// /Deposits/UpdateDepositAsync
        /// บันทึกการแก้ไข รายการนำฝาก #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(List<DepositDetailDTO>))]
        public async Task<IActionResult> UpdateDepositAsync([FromRoute]Guid id, [FromBody]List<DepositDetailDTO> input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await DepositService.UpdateDepositAsync(id, input);
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
        /// /Deposits/DeleteDepositAsync
        /// ลบข้อมูลการนำฝาก #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232402/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepositAsync([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await DepositService.DeleteDepositAsync(id);
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
