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
    public class FETController : BaseController
    {
        private readonly DatabaseContext DB;
        private readonly IFETService FETService;
        public FETController(IFETService fetService, DatabaseContext db)
        {
            FETService = fetService;
            DB = db;
        }

        /// <summary>
        /// โครงการ filter บริษัท
        /// </summary>
        [HttpGet("ProjectDropdownListForFET")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDropdownDTO>))]
        public async Task<IActionResult> GetProjectDropdownListAsync([FromQuery]string displayName = null, [FromQuery]Guid? companyID = null)
        {
            try
            {
                var result = await FETService.GetProjectDropdownListForFETAsync(displayName, companyID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unit
        /// </summary>
        [HttpGet("UnitDropdowListForFET")]
        [ProducesResponseType(200, Type = typeof(List<SoldUnitDropdownDTO>))]
        public async Task<IActionResult> GetUnitDropdowListForFETAsync([FromQuery]string displayName = null, [FromQuery]Guid? projectID = null, [FromQuery]Guid? unitID = null)
        {
            try
            {
                var result = await FETService.GetUnitDropdowListForFETAsync(displayName, projectID, unitID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// /FET/GetFETListAsync
        /// FET
        /// </summary>
        [HttpGet("GetFETListAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<FETDTO>))]
        public async Task<IActionResult> GetFETListAsync([FromQuery]FETFilter filter, [FromQuery]PageParam pageParam, [FromQuery]FETSortByParam sortByParam)
        {
            var result = await FETService.GetFETListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.FETs);
        }

        [HttpGet("GetFETListByProjectAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<FETDTO>))]
        public async Task<IActionResult> GetFETListByProjectAsync([FromQuery]FETFilterViewProject filter, [FromQuery]PageParam pageParam, [FromQuery]FETSortProjectListByParam sortByParam)
        {
            var result = await FETService.GetFETProjectListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.FETs);
        }

        [HttpGet("GetFETListByUnitAsync")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<FETDTO>))]
        public async Task<IActionResult> GetFETListByUnitAsync([FromQuery]FETFilterViewUnit filter, [FromQuery]PageParam pageParam, [FromQuery]FETSortUnitListByParam sortByParam)
        {
            var result = await FETService.GetFETUnitListAsync(filter, pageParam, sortByParam);

            AddPagingResponse(result.PageOutput);

            return Ok(result.FETs);
        }

        /// <summary>
        /// /FET/CreateFetAsync
        /// เพิ่ม FET
        /// </summary>
        /// <param name="input">FETDTO</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(FET))]
        public async Task<IActionResult> CreateFetAsync([FromBody]FETDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FETService.CreateFETAsync(input);
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
        /// /FET/UpdateFETAsync
        /// แก้ไขรายการ FET
        /// </summary>
        [HttpPost("UpdateFET")]
        public async Task<IActionResult> UpdateFETAsync([FromBody]FETDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FETService.UpdateFETAsync(input);
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
        /// /FET/UpdateFet
        /// แก้ไขสถานะ FET
        /// </summary>
        [HttpPut("UpdateFETStatus")]
        public async Task<IActionResult> UpdateFETStatusAsync([FromQuery]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FETService.UpdateFETStatusAsync(id);
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
        /// /FET/RejectFETAsync
        /// Reject รายการ FET
        /// </summary>
        [HttpPost("RejectFETAsync")]
        public async Task<IActionResult> RejectFETAsync([FromBody]FETDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FETService.RejectFETAsync(input);
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
        /// /FET/DeleteFETAsync
        /// ลบรายการ  FET
        /// </summary>
        [HttpPost("DeleteFETAsync")]
        public async Task<IActionResult> DeleteFETAsync([FromBody]FETDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await FETService.DeleteFETAsync(input);
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
