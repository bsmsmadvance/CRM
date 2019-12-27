using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// รายการโปรขายในใบเสนอราคา
    /// </summary>
    public class QuotationBookingPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterBookingPromotionItem/MasterBookingPromotionFreeItem/MasterBookingCreditCardItem
        /// </summary>
        public Guid? FromMasterBookingPromotionItemID { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitTH { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// ชนิดของรายการโปรโมชั่น
        /// Item = 0,
        /// FreeItem = 1,
        /// CreditCard = 2
        /// </summary>
        public PromotionItemType ItemType { get; set; }
        /// <summary>
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<QuotationBookingPromotionItemDTO> SubItems { get; set; }

        public static QuotationBookingPromotionItemDTO CreateFromModel(QuotationBookingPromotionItem itemModel, QuotationBookingPromotionFreeItem freeItemModel, QuotationBookingCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationBookingPromotionItemDTO result = new QuotationBookingPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterBookingPromotionItemID = itemModel.MasterBookingPromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.MasterPromotionItem.PricePerUnit;
                result.TotalPrice = itemModel.MasterPromotionItem.PricePerUnit * itemModel.Quantity;
                result.UnitTH = itemModel.MasterPromotionItem.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;

                var subItems = db.QuotationBookingPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainQuotationBookingPromotionID == itemModel.ID).ToList();
                result.SubItems = new List<QuotationBookingPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationBookingPromotionItemDTO()
                        {
                            FromMasterBookingPromotionItemID = item.MasterBookingPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.MasterPromotionItem.PricePerUnit,
                            TotalPrice = item.MasterPromotionItem.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = itemModel.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName,
                            IsSelected = true
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterBookingPromotionItemID = freeItemModel.MasterBookingPromotionFreeItemID;
                result.NameTH = freeItemModel.MasterPromotionFreeItem.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.MasterPromotionFreeItem.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.Id = freeItemModel.ID;
                result.Updated = freeItemModel.Updated;
                result.UpdatedBy = freeItemModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterBookingPromotionItemID = creditModel.MasterBookingCreditCardItemID;
                result.NameTH = creditModel.MasterBookingCreditCardItem.NameTH;
                result.Quantity = creditModel.MasterBookingCreditCardItem.Quantity;
                result.UnitTH = creditModel.MasterBookingCreditCardItem.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.Id = creditModel.ID;
                result.Updated = creditModel.Updated;
                result.UpdatedBy = creditModel.UpdatedBy?.DisplayName;
                result.IsSelected = true;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static QuotationBookingPromotionItemDTO CreateFromMasterModel(MasterBookingPromotionItem itemModel, MasterBookingPromotionFreeItem freeItemModel, MasterBookingCreditCardItem creditModel, DatabaseContext db)
        {
            QuotationBookingPromotionItemDTO result = new QuotationBookingPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterBookingPromotionItemID = itemModel.ID;
                result.NameTH = itemModel.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.IsSelected = false;

                var subItems = db.MasterBookingPromotionItems.Where(o => o.MainPromotionItemID == itemModel.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<QuotationBookingPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new QuotationBookingPromotionItemDTO()
                        {
                            FromMasterBookingPromotionItemID = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.UnitTH,
                            ItemType = PromotionItemType.Item,
                            IsSelected = false
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterBookingPromotionItemID = freeItemModel.ID;
                result.NameTH = freeItemModel.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.IsSelected = false;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterBookingPromotionItemID = creditModel.ID;
                result.NameTH = creditModel.NameTH;
                result.Quantity = creditModel.Quantity;
                result.UnitTH = creditModel.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.IsSelected = false;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
