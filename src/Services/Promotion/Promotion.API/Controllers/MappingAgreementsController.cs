using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Promotion.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.API.Controllers
{
    [Route("api/[controller]")]
    public class MappingAgreementsController : Controller
    {
        private readonly IMappingAgreementService MappingAgreementService;
        private readonly DatabaseContext DB;

        public MappingAgreementsController(IMappingAgreementService mappingAgreementService, DatabaseContext db)
        {
            this.MappingAgreementService = mappingAgreementService;
            this.DB = db;
        }

        [HttpPost("Import")]
        [ProducesResponseType(200, Type = typeof(List<MappingAgreementDTO>))]
        public async Task<IActionResult> ImportToGetMappingAgreementsDataFromExcel([FromBody]FileDTO input)
        {
            try
            {
                var results = await MappingAgreementService.GetMappingAgreementsDataFromExcelAsync(input);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Confirm")]
        [ProducesResponseType(200, Type = typeof(List<MappingAgreementDTO>))]
        public async Task<IActionResult> ConfirmImportMappingAgreements([FromBody]List<MappingAgreementDTO> inputs)
        {
            using(var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MappingAgreementService.ConfirmImportMappingAgreementsAsync(inputs);
                    tran.Commit();
                    return Ok(results);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        [HttpGet("Export")]
        [ProducesResponseType(200, Type = typeof(FileDTO))]
        public async Task<IActionResult> ExportMappingAgreements()
        {
            try
            {
                var result = await MappingAgreementService.ExportMappingAgreementsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
