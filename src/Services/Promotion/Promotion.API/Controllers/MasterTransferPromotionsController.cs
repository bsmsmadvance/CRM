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
    public class MasterTransferPromotionsController : BaseController
    {
        private readonly IMasterTransferPromotionService MasterTransferPromotionService;
        private readonly ILogger<MasterTransferPromotionsController> Logger;
        private readonly DatabaseContext DB;

        public MasterTransferPromotionsController(IMasterTransferPromotionService masterTransferPromotionService, ILogger<MasterTransferPromotionsController> logger, DatabaseContext db)
        {
            this.MasterTransferPromotionService = masterTransferPromotionService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// สร้าง Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionDTO))]
        public async Task<IActionResult> CreateMasterTransferPromotion([FromBody]MasterTransferPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterTransferPromotionService.CreateMasterTransferPromotionAsync(input);
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
        /// ลิส Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionDTO>))]
        public async Task<IActionResult> GetMasterTransferPromotionList([FromQuery]MasterTransferPromotionListFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterTransferPromotionSortByParam sortByParam)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetMasterTransferPromotionListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterTransferPromotionDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionDTO))]
        public async Task<IActionResult> GetMasterTransferPromotionDetail([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetMasterTransferPromotionDetailAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionDTO))]
        public async Task<IActionResult> UpdateMasterTransferPromotion([FromRoute]Guid id, [FromBody]MasterTransferPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterTransferPromotionService.UpdateMasterTransferPromotionAsync(id, input);
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
        /// ลบโปรโอน
        /// </summary>
        /// <returns>The master transfer promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterTransferPromotion([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionAsync(id);
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
        /// ดึงรายการ Master sรโปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionItemDTO>))]
        public async Task<IActionResult> GetMasterTransferPromotionItemList([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterTransferPromotionItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetMasterTransferPromotionItemListAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterTransferPromotionItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionItemList([FromRoute]Guid id, [FromBody]List<MasterTransferPromotionItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterTransferPromotionService.UpdateMasterTransferPromotionItemListAsync(id, inputs);
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
        /// แก้ไขรายการ Master โปรโอน (ทีละรายการ)
        /// </summary>
        /// <returns>The master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterTransferPromotionItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionItemDTO))]
        public async Task<IActionResult> UpdateMasterTransferPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionItemID, [FromBody]MasterTransferPromotionItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterTransferPromotionService.UpdateMasterTransferPromotionItemAsync(id, masterTransferPromotionItemID, input);
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
        /// ลบรายการ Master โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterTransferPromotionItemID}")]
        public async Task<IActionResult> DeleteMasterTransferPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionItemAsync(masterTransferPromotionItemID);
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
        /// สร้างรายการ Master โปรโอน โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master transfer promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionItemDTO>))]
        public async Task<IActionResult> CreateMasterTransferPromotionItems([FromRoute]Guid id, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterTransferPromotionService.CreateMasterTransferPromotionItemFromMaterialAsync(id, inputs);
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
        /// สร้างรายการย่อย Master โปรโอน โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master transfer promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterTransferPromotionItemID}/SubItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionItemDTO>))]
        public async Task<IActionResult> CreateSubMasterTransferPromotionItems([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionItemID, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterTransferPromotionService.CreateSubMasterTransferPromotionItemFromMaterialAsync(id, masterTransferPromotionItemID, inputs);
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
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรโอน
        /// </summary>
        /// <returns>The models of master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionItemID">Master transfer promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterTransferPromotionItemID}/Models")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetModelsOfMasterTransferPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionItemID)
        {
            try
            {
                var results = await MasterTransferPromotionService.GetMasterTransferPromotionItemModelListAsync(masterTransferPromotionItemID);

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
        /// <returns>The models to master transfer promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionID">Master transfer promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterTransferPromotionItemID}/Models/Save")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> SaveModelsToMasterTransferPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionItemID,
            [FromBody]List<ModelListDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.AddMasterTransferPromotionItemModelListAsync(masterTransferPromotionItemID, inputs);
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
        /// ดึงรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/FreeItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionFreeItemDTO>))]
        public async Task<IActionResult> GetMasterTransferPromotionFreeItemListAsync([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterTransferPromotionFreeItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetMasterTransferPromotionFreeItemListAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterTransferPromotionFreeItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซืื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{id}/FreeItems")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionFreeItemDTO))]
        public async Task<IActionResult> CreateMasterTransferPromotionFreeItem([FromRoute]Guid id,
            [FromBody]MasterTransferPromotionFreeItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.CreateMasterTransferPromotionFreeItemAsync(id, input);
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
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item list.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/FreeItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionFreeItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferPromotionFreeItemList([FromRoute]Guid id,
            [FromBody]List<MasterTransferPromotionFreeItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferPromotionFreeItemListAsync(id, inputs);
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
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (ทีละรายการ)
        /// </summary>
        /// <returns>The master Transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master Transfer promotion free item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/FreeItems/{masterTransferPromotionFreeItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionFreeItemDTO))]
        public async Task<IActionResult> UpdateMasterTransferPromotionFreeItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionFreeItemID,
            [FromBody]MasterTransferPromotionFreeItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferPromotionFreeItemAsync(id, masterTransferPromotionFreeItemID, input);
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
        /// ลบรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        [HttpDelete("{id}/FreeItems/{masterTransferPromotionFreeItemID}")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferPromotionFreeItemDTO>))]
        public async Task<IActionResult> DeleteMasterTransferPromotionFreeItem([FromRoute]Guid id,
            [FromRoute]Guid masterTransferPromotionFreeItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferPromotionFreeItemAsync(masterTransferPromotionFreeItemID);
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
        /// ดึงแบบบ้านของรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The models of master transfer promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        [HttpGet("{id}/FreeItems/{masterTransferPromotionFreeItemID}/Models")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetModelsOfMasterTransferPromotionFreeItem([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionFreeItemID)
        {
            try
            {
                var results = await MasterTransferPromotionService.GetMasterTransferPromotionFreeItemModelListAsync(masterTransferPromotionFreeItemID);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ระบุแบบบ้านให้กับรายการที่ไม่ต้องจัดซื้อ
        /// </summary>
        /// <returns>The master transfer promotion free item model list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferPromotionFreeItemID">Master transfer promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/FreeItems/{masterTransferPromotionFreeItemID}/Models/Save")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> AddMasterTransferPromotionFreeItemModelListAsync([FromRoute]Guid id, [FromRoute]Guid masterTransferPromotionFreeItemID,
            [FromBody]List<ModelListDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.AddMasterTransferPromotionFreeItemModelListAsync(masterTransferPromotionFreeItemID, inputs);
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
        /// ดึงรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/CreditCardItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferCreditCardItemDTO>))]
        public async Task<IActionResult> GetMasterTransferCreditCardItemAsync([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterTransferCreditCardItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetMasterTransferCreditCardItemAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterTransferCreditCardItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Master ค่าธรรมเนียมรูดบัตร</param>
        [HttpPost("{id}/CreditCardItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferCreditCardItemDTO>))]
        public async Task<IActionResult> CreateMasterTransferCreditCardItemListAsync([FromRoute]Guid id,
            [FromBody]List<EDCFeeDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.CreateMasterTransferCreditCardItemsAsync(id, inputs);
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
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/CreditCardItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferCreditCardItemDTO>))]
        public async Task<IActionResult> UpdateMasterTransferCreditCardItemListAsync([FromRoute]Guid id,
            [FromBody]List<MasterTransferCreditCardItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferCreditCardItemListAsync(id, inputs);
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
        /// แก้ไขรายการโปรค่าธรรมเนียมรูดบัตร (ทีละรายการ)
        /// </summary>
        /// <returns>The master Transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferCreditCardItemID">Master Transfer credit card item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/CreditCardItems/{masterTransferCreditCardItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterTransferCreditCardItemDTO))]
        public async Task<IActionResult> UpdateMasterTransferCreditCardItemAsync([FromRoute]Guid id, [FromRoute]Guid masterTransferCreditCardItemID,
            [FromBody]MasterTransferCreditCardItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.UpdateMasterTransferCreditCardItemAsync(id, masterTransferCreditCardItemID, input);
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
        /// ลบรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master transfer credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterTransferCreditCardItemID">Master transfer credit card item identifier.</param>
        [HttpDelete("{id}/CreditCardItems/{masterTransferCreditCardItemID}")]
        [ProducesResponseType(200, Type = typeof(List<MasterTransferCreditCardItemDTO>))]
        public async Task<IActionResult> DeleteMasterTransferCreditCardItemListAsync([FromRoute]Guid id,
            [FromRoute]Guid masterTransferCreditCardItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterTransferPromotionService.DeleteMasterTransferCreditCardItemAsync(masterTransferCreditCardItemID);
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
        /// Clone โปรโอน
        /// </summary>
        /// <returns>The master transfer promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(200, Type = typeof(MasterTransferPromotionDTO))]
        public async Task<IActionResult> CloneMasterTransferPromotionAsync([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterTransferPromotionService.CloneMasterTransferPromotionAsync(id);
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
        /// <returns>The clone master transfer promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(200, Type = typeof(CloneMasterPromotionConfirmDTO))]
        public async Task<IActionResult> GetCloneMasterTransferPromotionConfirmAsync([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterTransferPromotionService.GetCloneMasterTransferPromotionConfirmAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
