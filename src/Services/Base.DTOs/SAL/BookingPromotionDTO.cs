using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class BookingPromotionDTO : BaseDTO
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
        public List<BookingPromotionItemDTO> Items { get; set; }

        public async static Task<BookingPromotionDTO> CreateFromModelAsync(BookingPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new BookingPromotionDTO()
                {
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TransferDateBefore = model.TransferDateBefore
                };

                var promotionItems = new List<BookingPromotionItemDTO>();
                var itemModels = await db.BookingPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.QuotationBookingPromotionItem)
                    .Where(o => o.BookingPromotionID == model.ID && o.MainBookingPromotionItemID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => BookingPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var freeModels = await db.BookingPromotionFreeItems
                    .Include(o => o.MasterBookingPromotionFreeItem)
                    .Include(o => o.QuotationBookingPromotionFreeItem)
                    .Where(o => o.BookingPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => BookingPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var creditModels = await db.BookingCreditCardItems
                    .Include(o => o.MasterBookingCreditCardItem)
                    .Include(o => o.QuotationBookingCreditCardItem)
                    .Where(o => o.BookingPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => BookingPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    promotionItems.AddRange(items);
                }

                var promotionItemIDs = new List<Guid?>();
                if (promotionItems.Count > 0)
                {
                    promotionItemIDs.AddRange(promotionItems.Select(o => o.FromMasterBookingPromotionItemID).ToList());
                }

                var itemResults = new List<BookingPromotionItemDTO>();
                if (model.MasterPromotion != null)
                {
                    var masterPromotionItems = new List<BookingPromotionItemDTO>();
                    var masterItemModels = await db.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                    if (masterItemModels.Count > 0)
                    {
                        var items = masterItemModels.Select(o => BookingPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterFreeModels = await db.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterFreeModels.Count > 0)
                    {
                        var items = masterFreeModels.Select(o => BookingPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }

                    var masterCreditModels = await db.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == model.MasterPromotion.ID).ToListAsync();
                    if (masterCreditModels.Count > 0)
                    {
                        var items = masterCreditModels.Select(o => BookingPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        masterPromotionItems.AddRange(items);
                    }


                    if (masterPromotionItems.Count > 0)
                    {
                        var masterItems = masterPromotionItems.Where(o => !promotionItemIDs.Contains(o.FromMasterBookingPromotionItemID)).ToList();
                        itemResults.AddRange(masterItems);
                    }
                }

                result.Items = new List<BookingPromotionItemDTO>();
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
