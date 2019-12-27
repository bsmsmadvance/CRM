using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotion.API.Controllers
{
    [Route("api/[controller]")]
    public class MasterPreSalePromotionsController : BaseController
    {
        private readonly IMasterPreSalePromotionService MasterPreSalePromotionService;
        private readonly ILogger<MasterPreSalePromotionsController> Logger;
        private readonly DatabaseContext DB;

        public MasterPreSalePromotionsController(IMasterPreSalePromotionService masterPreSalePromotionService, ILogger<MasterPreSalePromotionsController> logger, DatabaseContext db)
        {
            this.MasterPreSalePromotionService = masterPreSalePromotionService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// สร้าง Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionDTO))]
        public async Task<IActionResult> CreateMasterPreSalePromotion([FromBody]MasterPreSalePromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.CreateMasterPreSalePromotionAsync(input);
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
        /// ลิส Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionDTO>))]
        public async Task<IActionResult> GetMasterPreSalePromotionList([FromQuery]MasterPreSalePromotionListFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterPreSalePromotionSortByParam sortByParam)
        {
            try
            {
                var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterPreSalePromotionDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("DropdownList")]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionDropdownDTO>))]
        public async Task<IActionResult> GetMasterPreSalePromotionDropdownList([FromQuery]string promotionNo = null, [FromQuery]string name = null)
        {
            try
            {
                var results = await MasterPreSalePromotionService.GetMasterPreSalePromotionDropdownAsync(promotionNo, name);
                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionDTO))]
        public async Task<IActionResult> GetMasterPreSalePromotionDetail([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionDetailAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ดึงโปรโมชั่นก่อนขายที่ Active ล่าสุดของโครงการนั้น
        /// </summary>
        /// <param name="projectID">รหัสโครงการ</param>
        /// <returns></returns>
        [HttpGet("Active")]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionDTO))]
        public async Task<IActionResult> GetActiveMasterPreSalePromotionDetail([FromQuery]Guid projectID)
        {
            try
            {
                var result = await MasterPreSalePromotionService.GetActiveMasterPreSalePromotionDetailAsync(projectID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionDTO))]
        public async Task<IActionResult> UpdateMasterPreSalePromotion([FromRoute]Guid id, [FromBody]MasterPreSalePromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionAsync(id, input);
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
        /// ลบโปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterPreSalePromotion([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterPreSalePromotionService.DeleteMasterPreSalePromotionAsync(id);
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
        /// ดึงรายการ Master sรโปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionItemDTO>))]
        public async Task<IActionResult> GetMasterPreSalePromotionItemList([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterPreSalePromotionItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterPreSalePromotionService.GetMasterPreSalePromotionItemListAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterPreSalePromotionItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items")]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterPreSalePromotionItemList([FromRoute]Guid id, [FromBody]List<MasterPreSalePromotionItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionItemListAsync(id, inputs);
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

        /// <summary>
        /// แก้ไขรายการ Master โปรก่อนขาย (ทีละรายการ)
        /// </summary>
        /// <returns>The master preSale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master preSale promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterPreSalePromotionItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionItemDTO))]
        public async Task<IActionResult> UpdateMasterPreSalePromotionItem([FromRoute]Guid id, [FromRoute]Guid masterPreSalePromotionItemID, [FromBody]MasterPreSalePromotionItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.UpdateMasterPreSalePromotionItemAsync(id, masterPreSalePromotionItemID, input);
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

        /// <summary>
        /// ลบรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master presale promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterPreSalePromotionItemID}")]
        public async Task<IActionResult> DeleteMasterPreSalePromotionItem([FromRoute]Guid id, [FromRoute]Guid masterPreSalePromotionItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterPreSalePromotionService.DeleteMasterPreSalePromotionItemAsync(masterPreSalePromotionItemID);
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
        /// สร้างรายการ Master โปรก่อนขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master presale promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionItemDTO>))]
        public async Task<IActionResult> CreateMasterPreSalePromotionItems([FromRoute]Guid id, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterPreSalePromotionService.CreateMasterPreSalePromotionItemFromMaterialAsync(id, inputs);
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

        /// <summary>
        /// สร้างรายการย่อย Master โปรก่อนขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master presale promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterPreSalePromotionItemID}/SubItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterPreSalePromotionItemDTO>))]
        public async Task<IActionResult> CreateSubMasterPreSalePromotionItems([FromRoute]Guid id, [FromRoute]Guid masterPreSalePromotionItemID, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterPreSalePromotionService.CreateSubMasterPreSalePromotionItemFromMaterialAsync(id, masterPreSalePromotionItemID, inputs);
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
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรก่อนขาย
        /// </summary>
        /// <returns>The models of master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionItemID">Master presale promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterPreSalePromotionItemID}/Models")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetModelsOfMasterPreSalePromotionItem([FromRoute]Guid id, [FromRoute]Guid masterPreSalePromotionItemID)
        {
            try
            {
                var results = await MasterPreSalePromotionService.GetMasterPreSalePromotionItemModelListAsync(masterPreSalePromotionItemID);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ระบุแบบบ้าน
        /// </summary>
        /// <returns>The models to master presale promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterPreSalePromotionID">Master presale promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterPreSalePromotionItemID}/Models/Save")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> SaveModelsToMasterPreSalePromotionItem([FromRoute]Guid id, [FromRoute]Guid masterPreSalePromotionItemID,
            [FromBody]List<ModelListDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterPreSalePromotionService.AddMasterPreSalePromotionItemModelListAsync(masterPreSalePromotionItemID, inputs);
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
        /// Clone โปรก่อนขาย
        /// </summary>
        /// <returns>The master pre sale promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(200, Type = typeof(MasterPreSalePromotionDTO))]
        public async Task<IActionResult> CloneMasterPreSalePromotionAsync([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterPreSalePromotionService.CloneMasterPreSalePromotionAsync(id);
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
        /// ดึงจำนวน Item ที่ Clone ได้หรือ Expired ไปแล้ว
        /// </summary>
        /// <returns>The clone master pre sale promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(200, Type = typeof(CloneMasterPromotionConfirmDTO))]
        public async Task<IActionResult> GetCloneMasterPreSalePromotionConfirmAsync([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterPreSalePromotionService.GetCloneMasterPreSalePromotionConfirmAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
