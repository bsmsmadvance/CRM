using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Project.Services;

namespace Project.API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetMinPricesController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IBudgetMinPriceService BudgetMinPriceService;
        public BudgetMinPricesController(IBudgetMinPriceService budgetMinPriceService, DatabaseContext db)
        {
            this.BudgetMinPriceService = budgetMinPriceService;
            this.DB = db;
        }

        /// <summary>
        /// ดึงค่า Budget Min Price ตามโครงการและควอเตอร์
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(BudgetMinPriceDTO))]
        //public async Task<IActionResult> GetBudgetMinPriceAsync([FromQuery]BudgetMinPriceFilter filter)
        //{
        //    try
        //    {
        //        var result = await BudgetMinPriceService.GetBudgetMinPriceAsync(filter);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// ดึงรายการ Unit จากโครงการและควอเตอร์
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="unitFilter"></param>
        /// <returns></returns>
        //[HttpGet("Units")]
        //[ProducesResponseType(200, Type = typeof(List<BudgetMinPriceUnitDTO>))]
        //public async Task<IActionResult> GetBudgetMinPriceUnitListAsync([FromQuery]BudgetMinPriceFilter filter, [FromQuery]PageParam pageParam, [FromQuery]BudgetMinPriceSortByParam sortByParam)
        //{
        //    try
        //    {
        //        var results = await BudgetMinPriceService.GetBudgetMinPriceUnitListAsync(filter, pageParam, sortByParam);

        //        return Ok(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}





        /// <summary>
        /// ดึงค่า Budget Min Price ตามโครงการและควอเตอร์
        /// ดึงรายการ Unit จากโครงการและควอเตอร์
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetBudgetMinPriceList")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(BudgetMinPriceListDTO))]
        public async Task<IActionResult> GetBudgetMinPriceListAsync([FromQuery]BudgetMinPriceFilter filter, [FromQuery]PageParam pageParam, [FromQuery]BudgetMinPriceSortByParam sortByParam)
        {
            try
            {
                var results = await BudgetMinPriceService.GetBudgetMinPriceListAsync(filter, pageParam, sortByParam);
                AddPagingResponse(results.PageOutput);
                return Ok(results.BudgetMinPriceListDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        /// <summary>
        /// บันทึก หรือ แก้ไข Budget Min Price
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [ProducesResponseType(200, Type = typeof(BudgetMinPriceDTO))]
        public async Task<IActionResult> SaveBudgetMinPriceAsync([FromQuery]BudgetMinPriceFilter filter, [FromBody]BudgetMinPriceDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetMinPriceService.SaveBudgetMinPriceAsync(filter, input);
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
        /// แก้ไข Budget Min Price Unit
        /// สร้าง Budget Min Price Unit หากยังไม่มีข้อมูล
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPut("SaveBudgetMinPriceUnitList")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SaveBudgetMinPriceUnitListAsync([FromBody]BudgetMinPriceListDTO inputs)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BudgetMinPriceService.SaveBudgetMinPriceUnitListAsync(inputs);
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
        /// แก้ไข Budget Min Price Unit
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Units/Save")]
        [ProducesResponseType(200, Type = typeof(BudgetMinPriceUnitDTO))]
        public async Task<IActionResult> SaveBudgetMinPriceUnitAsync([FromQuery]BudgetMinPriceFilter filter, [FromBody]BudgetMinPriceUnitDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await BudgetMinPriceService.SaveBudgetMinPriceUnitAsync(filter, input);
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
        /// Import Quarterly Budget Excel เพื่อดึงรายการไปแสดง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("QuarterlyBudgets/Import")]
        [ProducesResponseType(200, Type = typeof(BudgetMinPriceQuarterlyDTO))]
        public async Task<IActionResult> ImportQuarterlyBudgetAsync([FromBody]FileDTO input)
        {
            try
            {
                var results = await BudgetMinPriceService.ImportQuarterlyBudgetAsync(input);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ยืนยัน Import Quarterly Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("QuarterlyBudgets/ConfirmImport")]
        public async Task<IActionResult> ConfirmImportQuarterlyBudgetAsync([FromBody]BudgetMinPriceQuarterlyDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                { 
                    await BudgetMinPriceService.ConfirmImportQuarterlyBudgetAsync(input);
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
        /// Export Quarterly Budget Excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("QuarterlyBudgets/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportQuarterlyBudgetAsync([FromQuery]BudgetMinPriceFilter filter)
        {
            try
            {
                var result = await BudgetMinPriceService.ExportQuarterlyBudgetAsync(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Import Transfer Budget Excel เพื่อดึงรายการไปแสดง
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("TransferBudgets/Import")]
        [ProducesResponseType(200, Type = typeof(BudgetMinPriceTransferDTO))]
        public async Task<IActionResult> ImportTransferBudgetAsync([FromBody]FileDTO input)
        {
            try
            {
                var results = await BudgetMinPriceService.ImportTransferBudgetAsync(input);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ยืนยัน Import Transfer Budget
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("TransferBudgets/ConfirmImport")]
        public async Task<IActionResult> ConfirmImportTransferBudgetAsync([FromBody]BudgetMinPriceTransferDTO input)
        {
            using (var tran = await DB.Database.BeginTransactionAsync())
            {
                try
                {
                    await BudgetMinPriceService.ConfirmImportTransferBudgetAsync(input);
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
        /// Export Transfer Budget Excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("TransferBudgets/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportTransferBudgetAsync([FromQuery]BudgetMinPriceFilter filter)
        {
            try
            {
                var result = await BudgetMinPriceService.ExportTransferBudgetAsync(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
