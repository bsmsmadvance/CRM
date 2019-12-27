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
    public class MasterBookingPromotionsController : BaseController
    {
        private readonly IMasterBookingPromotionService MasterBookingPromotionService;
        private readonly ILogger<MasterBookingPromotionsController> Logger;
        private readonly DatabaseContext DB;

        public MasterBookingPromotionsController(IMasterBookingPromotionService masterBookingPromotionService, ILogger<MasterBookingPromotionsController> logger, DatabaseContext db)
        {
            this.MasterBookingPromotionService = masterBookingPromotionService;
            this.Logger = logger;
            this.DB = db;
        }

        /// <summary>
        /// สร้าง Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion.</returns>
        /// <param name="input">Input.</param>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionDTO))]
        public async Task<IActionResult> CreateMasterBookingPromotion([FromBody]MasterBookingPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterBookingPromotionService.CreateMasterBookingPromotionAsync(input);
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
        /// ลิส Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="filter">Filter.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionDTO>))]
        public async Task<IActionResult> GetMasterBookingPromotionList([FromQuery]MasterBookingPromotionListFilter filter,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterBookingPromotionSortByParam sortByParam)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetMasterBookingPromotionListAsync(filter, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterBookingPromotionDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionDTO))]
        public async Task<IActionResult> GetMasterBookingPromotionDetail([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetMasterBookingPromotionDetailAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionDTO))]
        public async Task<IActionResult> UpdateMasterBookingPromotion([FromRoute]Guid id, [FromBody]MasterBookingPromotionDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterBookingPromotionService.UpdateMasterBookingPromotionAsync(id, input);
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
        /// ลบโปรขาย
        /// </summary>
        /// <returns>The master booking promotion.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasterBookingPromotion([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.DeleteMasterBookingPromotionAsync(id);
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
        /// ดึงรายการ Master sรโปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/Items")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionItemDTO>))]
        public async Task<IActionResult> GetMasterBookingPromotionItemList([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterBookingPromotionItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetMasterBookingPromotionItemListAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterBookingPromotionItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// แก้ไขรายการ Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion list.</returns>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/Items/")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionItemDTO>))]
        public async Task<IActionResult> UpdateMasterBookingPromotionItemList([FromRoute]Guid id, [FromBody]List<MasterBookingPromotionItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterBookingPromotionService.UpdateMasterBookingPromotionItemListAsync(id, inputs);
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
        /// แก้ไขรายการ Master โปรขาย (ทีละรายการ)
        /// </summary>
        /// <returns>The master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionItemID">Master booking promotion item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/Items/{masterBookingPromotionItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionItemDTO))]
        public async Task<IActionResult> UpdateMasterBookingPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionItemID, [FromBody]MasterBookingPromotionItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterBookingPromotionService.UpdateMasterBookingPromotionItemAsync(id, masterBookingPromotionItemID, input);
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
        /// ลบรายการ Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionItemID">Master booking promotion identifier.</param>
        [HttpDelete("{id}/Items/{masterBookingPromotionItemID}")]
        public async Task<IActionResult> DeleteMasterBookingPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.DeleteMasterBookingPromotionItemAsync(masterBookingPromotionItemID);
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
        /// สร้างรายการ Master โปรขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master booking promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionItemDTO>))]
        public async Task<IActionResult> CreateMasterBookingPromotionItems([FromRoute]Guid id, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterBookingPromotionService.CreateMasterBookingPromotionItemFromMaterialAsync(id, inputs);
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
        /// สร้างรายการย่อย Master โปรขาย โดยใช้ Material ที่ดึงจาก SAP
        /// </summary>
        /// <returns>The master booking promotion items.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterBookingPromotionItemID}/SubItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionItemDTO>))]
        public async Task<IActionResult> CreateSubMasterBookingPromotionItems([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionItemID, [FromBody]List<PromotionMaterialDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var results = await MasterBookingPromotionService.CreateSubMasterBookingPromotionItemFromMaterialAsync(id, masterBookingPromotionItemID, inputs);
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
        /// ดึงแบบบ้านที่ผูกไว้กับรายการ Master โปรขาย
        /// </summary>
        /// <returns>The models of master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionItemID">Master booking promotion item identifier.</param>
        [HttpGet("{id}/Items/{masterBookingPromotionItemID}/Models")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetModelsOfMasterBookingPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionItemID)
        {
            try
            {
                var results = await MasterBookingPromotionService.GetMasterBookingPromotionItemModelListAsync(masterBookingPromotionItemID);

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
        /// <returns>The models to master booking promotion item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionID">Master booking promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/Items/{masterBookingPromotionItemID}/Models/Save")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> SaveModelsToMasterBookingPromotionItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionItemID,
            [FromBody]List<ModelListDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.AddMasterBookingPromotionItemModelListAsync(masterBookingPromotionItemID, inputs);
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
        /// <returns>The master booking promotion free item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/FreeItems")]
        [PagingResponseHeaders]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionFreeItemDTO>))]
        public async Task<IActionResult> GetMasterBookingPromotionFreeItemListAsync([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterBookingPromotionFreeItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetMasterBookingPromotionFreeItemListAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterBookingPromotionFreeItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซืื้อ
        /// </summary>
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPost("{id}/FreeItems")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionFreeItemDTO))]
        public async Task<IActionResult> CreateMasterBookingPromotionFreeItem([FromRoute]Guid id,
            [FromBody]MasterBookingPromotionFreeItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.CreateMasterBookingPromotionFreeItemAsync(id, input);
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
        /// <returns>The master booking promotion free item list.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/FreeItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionFreeItemDTO>))]
        public async Task<IActionResult> UpdateMasterBookingPromotionFreeItemList([FromRoute]Guid id,
            [FromBody]List<MasterBookingPromotionFreeItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.UpdateMasterBookingPromotionFreeItemListAsync(id, inputs);
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
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/FreeItems/{masterBookingPromotionFreeItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionFreeItemDTO))]
        public async Task<IActionResult> UpdateMasterBookingPromotionFreeItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionFreeItemID,
            [FromBody]MasterBookingPromotionFreeItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.UpdateMasterBookingPromotionFreeItemAsync(id, masterBookingPromotionFreeItemID, input);
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
        /// <returns>The master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        [HttpDelete("{id}/FreeItems/{masterBookingPromotionFreeItemID}")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingPromotionFreeItemDTO>))]
        public async Task<IActionResult> DeleteMasterBookingPromotionFreeItem([FromRoute]Guid id,
            [FromRoute]Guid masterBookingPromotionFreeItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.DeleteMasterBookingPromotionFreeItemAsync(masterBookingPromotionFreeItemID);
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
        /// <returns>The models of master booking promotion free item.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        [HttpGet("{id}/FreeItems/{masterBookingPromotionFreeItemID}/Models")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> GetModelsOfMasterBookingPromotionFreeItem([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionFreeItemID)
        {
            try
            {
                var results = await MasterBookingPromotionService.GetMasterBookingPromotionFreeItemModelListAsync(masterBookingPromotionFreeItemID);

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
        /// <returns>The master booking promotion free item model list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPost("{id}/FreeItems/{masterBookingPromotionFreeItemID}/Models/Save")]
        [ProducesResponseType(200, Type = typeof(List<ModelListDTO>))]
        public async Task<IActionResult> AddMasterBookingPromotionFreeItemModelListAsync([FromRoute]Guid id, [FromRoute]Guid masterBookingPromotionFreeItemID,
            [FromBody]List<ModelListDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.AddMasterBookingPromotionFreeItemModelListAsync(masterBookingPromotionFreeItemID, inputs);
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
        /// <returns>The master booking credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        [HttpGet("{id}/CreditCardItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingCreditCardItemDTO>))]
        public async Task<IActionResult> GetMasterBookingCreditCardItemAsync([FromRoute]Guid id,
            [FromQuery]PageParam pageParam,
            [FromQuery]MasterBookingCreditCardItemSortByParam sortByParam)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetMasterBookingCreditCardItemAsync(id, pageParam, sortByParam);

                AddPagingResponse(result.PageOutput);

                return Ok(result.MasterBookingCreditCardItemDTOs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// สร้างรายการโปรค่าธรรมเนียมรูดบัตร
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Master ค่าธรรมเนียมรูดบัตร</param>
        [HttpPost("{id}/CreditCardItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingCreditCardItemDTO>))]
        public async Task<IActionResult> CreateMasterBookingCreditCardItemListAsync([FromRoute]Guid id,
            [FromBody]List<EDCFeeDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.CreateMasterBookingCreditCardItemsAsync(id, inputs);
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
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="inputs">Inputs.</param>
        [HttpPut("{id}/CreditCardItems")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingCreditCardItemDTO>))]
        public async Task<IActionResult> UpdateMasterBookingCreditCardItemListAsync([FromRoute]Guid id,
            [FromBody]List<MasterBookingCreditCardItemDTO> inputs)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.UpdateMasterBookingCreditCardItemListAsync(id, inputs);
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
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingCreditCardItemID">Master booking credit card item identifier.</param>
        /// <param name="input">Input.</param>
        [HttpPut("{id}/CreditCardItems/{masterBookingCreditCardItemID}")]
        [ProducesResponseType(200, Type = typeof(MasterBookingCreditCardItemDTO))]
        public async Task<IActionResult> UpdateMasterBookingCreditCardItemAsync([FromRoute]Guid id, [FromRoute]Guid masterBookingCreditCardItemID,
            [FromBody]MasterBookingCreditCardItemDTO input)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.UpdateMasterBookingCreditCardItemAsync(id, masterBookingCreditCardItemID, input);
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
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="masterBookingCreditCardItemID">Master booking credit card item identifier.</param>
        [HttpDelete("{id}/CreditCardItems/{masterBookingCreditCardItemID}")]
        [ProducesResponseType(200, Type = typeof(List<MasterBookingCreditCardItemDTO>))]
        public async Task<IActionResult> DeleteMasterBookingCreditCardItemListAsync([FromRoute]Guid id,
            [FromRoute]Guid masterBookingCreditCardItemID)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    await MasterBookingPromotionService.DeleteMasterBookingCreditCardItemAsync(masterBookingCreditCardItemID);
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
        /// Clone โปรขาย
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpPost("{id}/Clone")]
        [ProducesResponseType(200, Type = typeof(MasterBookingPromotionDTO))]
        public async Task<IActionResult> CloneMasterBookingPromotionAsync([FromRoute]Guid id)
        {
            using (var tran = DB.Database.BeginTransaction())
            {
                try
                {
                    var result = await MasterBookingPromotionService.CloneMasterBookingPromotionAsync(id);
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
        /// <returns>The clone master booking promotion confirm async.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id}/CloneConfirmPopup")]
        [ProducesResponseType(200, Type = typeof(CloneMasterPromotionConfirmDTO))]
        public async Task<IActionResult> GetCloneMasterBookingPromotionConfirmAsync([FromRoute]Guid id)
        {
            try
            {
                var result = await MasterBookingPromotionService.GetCloneMasterBookingPromotionConfirmAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
