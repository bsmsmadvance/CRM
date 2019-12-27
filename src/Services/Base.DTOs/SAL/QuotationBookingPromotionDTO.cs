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
    /// โปรขายในใบเสนอราคา
    /// </summary>
    public class QuotationBookingPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<QuotationBookingPromotionItemDTO> Items { get; set; }

        public async static Task<QuotationBookingPromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).Select(o => o.ProjectID).FirstAsync();
            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterBookingPromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID).FirstOrDefaultAsync();

            if(model != null)
            {
                QuotationBookingPromotionDTO result = new QuotationBookingPromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<QuotationBookingPromotionItemDTO>();
                var itemModels = await db.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if(itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<QuotationBookingPromotionDTO> CreateFromQuotationAsync(QuotationBookingPromotion model, DatabaseContext db)
        {
            if(model != null)
            {
                var result = new QuotationBookingPromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var promotionItems = new List<QuotationBookingPromotionItemDTO>();
                var itemModels = await db.QuotationBookingPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationBookingPromotionID == model.ID && o.MainQuotationBookingPromotionID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.QuotationBookingPromotionFreeItems
                    .Include(o => o.MasterPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationBookingPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.QuotationBookingCreditCardItems
                    .Include(o => o.MasterBookingCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.QuotationBookingPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterBookingPromotionItemID).ToList());
                }

                var itemResults = new List<QuotationBookingPromotionItemDTO>();
                if(model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<QuotationBookingPromotionItemDTO>();
                    var masterItemModels = await db.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterFreeModels = await db.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => QuotationBookingPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }


                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterBookingPromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<QuotationBookingPromotionItemDTO>();
                if (itemResults.Count > 0)
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
