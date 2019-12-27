using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// โปรโอนในใบเสนอราคา
    /// </summary>
    public class QuotationTransferPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<QuotationTransferPromotionItemDTO> Items { get; set; }

        public async static Task<QuotationTransferPromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).Select(o => o.ProjectID).FirstAsync();
            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterTransferPromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID).FirstOrDefaultAsync();

            if (model != null)
            {
                QuotationTransferPromotionDTO result = new QuotationTransferPromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<QuotationTransferPromotionItemDTO>();

                var itemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterTransferCreditCardItems.Where(o => o.MasterTransferPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<QuotationTransferPromotionDTO> CreateFromQuotationAsync(QuotationTransferPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new QuotationTransferPromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Remark = model.Remark,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var promotionItems = new List<QuotationTransferPromotionItemDTO>();
                var itemModels = await db.QuotationTransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID && o.MainQuotationTransferPromotionID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.QuotationTransferPromotionFreeItems
                    .Include(o => o.MasterPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.QuotationTransferCreditCardItems
                    .Include(o => o.MasterTransferCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationTransferPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterTansferPromotionItemID).ToList());
                }

                var itemResults = new List<QuotationTransferPromotionItemDTO>();
                if(model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<QuotationTransferPromotionItemDTO>();
                    var masterItemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterFreeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterTransferCreditCardItems.Where(o => o.MasterTransferPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => QuotationTransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterTansferPromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<QuotationTransferPromotionItemDTO>();
                if(itemResults.Count > 0)
                {
                    result.Items.AddRange(itemResults);
                }
                
                result.Items.AddRange(promotionItems);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
