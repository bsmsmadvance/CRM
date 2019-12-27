using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoBookingPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// โอนกรรมสิทธิ์ภายในวันที่
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<UnitInfoBookingPromotionItemDTO> Items { get; set; }

        public async static Task<UnitInfoBookingPromotionDTO> CreateFromModelAsync(BookingPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitInfoBookingPromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TransferDateBefore = model.TransferDateBefore
                };

                var promotionItems = new List<UnitInfoBookingPromotionItemDTO>();
                var itemModels = await db.BookingPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.QuotationBookingPromotionItem)
                    .Where(o => o.BookingPromotionID == model.ID && o.MainBookingPromotionItemID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.BookingPromotionFreeItems
                    .Include(o => o.MasterBookingPromotionFreeItem)
                    .Include(o => o.QuotationBookingPromotionFreeItem)
                    .Where(o => o.BookingPromotionID == model.ID).ToListAsync();

                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.BookingCreditCardItems
                    .Include(o => o.MasterBookingCreditCardItem)
                    .Include(o => o.QuotationBookingCreditCardItem)
                    .Where(o => o.BookingPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterBookingPromotionItemID).ToList());
                }

                var itemResults = new List<UnitInfoBookingPromotionItemDTO>();
                if (model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<UnitInfoBookingPromotionItemDTO>();
                    var masterItemModels = await db.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterFreeModels = await db.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterBookingPromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<UnitInfoBookingPromotionItemDTO>();
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

        public async static Task<UnitInfoBookingPromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var projectID = await db.Units.Where(o => o.ID == unitID).Select(o => o.ProjectID).FirstAsync();
            var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionStatus && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
            var model = await db.MasterBookingPromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == projectID).FirstOrDefaultAsync();

            if (model != null)
            {
                UnitInfoBookingPromotionDTO result = new UnitInfoBookingPromotionDTO();
                result.PromotionNo = model.PromotionNo;
                result.Items = new List<UnitInfoBookingPromotionItemDTO>();
                var itemModels = await db.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == model.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => UnitInfoBookingPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
