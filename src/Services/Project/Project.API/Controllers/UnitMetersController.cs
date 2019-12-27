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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagingExtensions;
using Project.Params.Filters;
using Project.Services;

namespace Project.API.Controllers
{
#if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class UnitMetersController : BaseController
    {
        private IUnitService UnitService;
        private IWaterElectricMeterPriceService WaterElectricMeterPriceService;
        private readonly DatabaseContext DB;
        public UnitMetersController(
            IUnitService unitService,
            IWaterElectricMeterPriceService waterElectricMeterPriceService,
            DatabaseContext db)
        {
            this.UnitService = unitService;
            this.WaterElectricMeterPriceService = waterElectricMeterPriceService;
            this.DB = db;

        }
#region UnitMeter
        /// <summary>
        /// ลิสราคา มิเตอร์น้ำ
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/WaterMeterPriceDropdownList")]
        [ProducesResponseType(200, Type = typeof(List<WaterMeterPriceDropdownDTO>))]
        public async Task<IActionResult> GetWaterMeterPriceDropdown([FromRoute]Guid unitID)
        {
            try
            {
                var result = await WaterElectricMeterPriceService.GetWaterMeterPriceDropdownListAsync(unitID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสราคา มิเตอร์ไฟ
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}/ElectricMeterPriceDropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ElectricMeterPriceDropdownDTO>))]
        public async Task<IActionResult> GetElectricMeterPriceDropdown([FromRoute]Guid unitID)
        {
            try
            {
                var result = await WaterElectricMeterPriceService.GetElectricMeterPriceDropdownListAsync(unitID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ลิสข้อมูล UnitMeter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageParam"></param>
        /// <param name="sortByParam"></param>
        /// <returns></returns>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<ProjectUnitMeterListDTO>))]
        public async Task<IActionResult> GetProjectUnitMeterList([FromQuery]UnitMeterFilter filter, [FromQuery]PageParam pageParam, [FromQuery]UnitMeterListSortByParam sortByParam)
        {
            try
            {
                var result = await UnitService.GetUnitMeterListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.ProjectUnitMeterLists);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ข้อมูล UnitMeter
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpGet("{unitID}")]
        [ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        public async Task<IActionResult> GetUnitMeter([FromRoute]Guid unitID)
        {
                try
                {
                    var result = await UnitService.GetUnitMeterAsync(unitID);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
        /// <summary>
        /// ลบ (Reset field Unitmeter)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        [HttpDelete("{unitID}")]
        [ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        public async Task<IActionResult> DeleteUnitMeter([FromRoute]Guid unitID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.DeleteUnitMeterAsync(unitID);
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
        /// แก้ไข ข้อมูล UnitMeter
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{unitID}")]
        [ProducesResponseType(200, Type = typeof(UnitMeterDTO))]
        public async Task<IActionResult> EditUnitMeter([FromRoute]Guid unitID, [FromBody] UnitMeterDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.UpdateUnitMeterAsync(unitID, input);
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
        /// Import เลขมิเตอร์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UnitMeter/Import")]
        public async Task<IActionResult> ImportUnitMeterNo([FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.ImportUnitMeterExcelAsync(input);
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
        /// Export เลขมิเตอร์
        /// </summary>
        /// <returns></returns>
        [HttpGet("UnitMeter/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportUnitMeterNo([FromQuery]UnitMeterFilter filter, [FromQuery]UnitMeterListSortByParam sortByParam)
        {
            try
            {
                var result = await UnitService.ExportUnitMeterExcelAsync(filter,sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Import สถานะมิเตอร์
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UnitMeterStatus/Import")]
        public async Task<IActionResult> ImportUnitMeterStatus([FromBody]FileDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await UnitService.ImportUnitMeterStatusExcelAsync(input);
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
        /// Export สถานะมิเตอร์
        /// </summary>
        /// <returns></returns>
        [HttpGet("UnitMeterStatus/Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportUnitMeterStatus([FromQuery]UnitMeterFilter filter, [FromQuery]UnitMeterListSortByParam sortByParam)
        {
            try
            {
                var result = await UnitService.ExportUnitMeterStatusExcelAsync(filter, sortByParam);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#endregion
    }
}