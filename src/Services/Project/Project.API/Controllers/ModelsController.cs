using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Base.DTOs.PRJ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace Project.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ModelsController : Controller
    {
        private readonly IModelService ModelService;

        public ModelsController(IModelService modelService)
        {
            this.ModelService = modelService;
        }

        /// <summary>
        /// ลิสข้อมูลแบบบ้านทั้งหมด
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<ModelDropdownDTO>))]
        public async Task<IActionResult> GetModelDropdownList([FromQuery]string name)
        {
            try
            {
                var results = await ModelService.GetModelDropdownListAsync(null, name);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
