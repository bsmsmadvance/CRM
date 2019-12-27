using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.SAL
{
    public class TransferPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ID ของ MasterTransferPromotionItem/MasterTransferPromotionFreeItem/MasterTransferCreditCardItem
        /// </summary>
        public Guid? FromMasterTansferPromotionItemID { get; set; }
        /// <summary>
        /// ID ของ TransferPromotionItem/QuotationTransferPromotionFreeItem/QuotationTransferCreditCardItem
        /// </summary>
        public Guid? FromQuotationTansferPromotionItemID { get; set; }
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
        /// ชนิดของรายการโปรโมชั่น
        /// Item = 0,
        /// FreeItem = 1,
        /// CreditCard = 2
        /// </summary>
        public PromotionItemType ItemType { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<TransferPromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }

        public static TransferPromotionItemDTO CreateFromModel(TransferPromotionItem itemModel, TransferPromotionFreeItem freeItemModel, TransferCreditCardItem creditModel, DatabaseContext db)
        {
            TransferPromotionItemDTO result = new TransferPromotionItemDTO();
            if (itemModel != null)
            {
                //itemModel.MasterTransferPromotionItem =  db.MasterTransferPromotionItems.Where(o=> o.ID == itemModel.MasterTransferPromotionItemID).FirstOrDefaultAsync();
                result.FromMasterTansferPromotionItemID = itemModel.MasterTransferPromotionItemID;
                result.FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID;
                result.NameTH = itemModel.MasterPromotionItem.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.MasterPromotionItem.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.Id = itemModel.ID;
                result.Updated = itemModel.Updated;
                result.UpdatedBy = itemModel.UpdatedBy?.DisplayName;

                var subItems = db.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == itemModel.ID).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = itemModel.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = itemModel.ID,
                            Updated = itemModel.Updated,
                            UpdatedBy = itemModel.UpdatedBy?.DisplayName
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else if (freeItemModel != null)
            {
                result.FromMasterTansferPromotionItemID = freeItemModel.MasterTransferPromotionFreeItemID;
                result.FromQuotationTansferPromotionItemID = freeItemModel.QuotationTransferPromotionFreeItemID;
                result.NameTH = freeItemModel.MasterTransferPromotionFreeItem.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.MasterTransferPromotionFreeItem.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.Id = freeItemModel.ID;
                result.Updated = freeItemModel.Updated;
                result.UpdatedBy = freeItemModel.UpdatedBy?.DisplayName;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterTansferPromotionItemID = creditModel.MasterTransferCreditCardItemID;
                result.FromQuotationTansferPromotionItemID = creditModel.QuotationTransferCreditCardItemID;
                result.NameTH = creditModel.MasterTransferCreditCardItem.NameTH;
                result.Quantity = creditModel.MasterTransferCreditCardItem.Quantity;
                result.UnitTH = creditModel.MasterTransferCreditCardItem.UnitTH;
                result.ItemType = PromotionItemType.CreditCard;
                result.Id = creditModel.ID;
                result.Updated = creditModel.Updated;
                result.UpdatedBy = creditModel.UpdatedBy?.DisplayName;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TransferPromotionItemDTO CreateFromMasterModel(MasterTransferPromotionItem itemModel, MasterTransferPromotionFreeItem freeItemModel, MasterTransferCreditCardItem creditModel, DatabaseContext db)
        {
            TransferPromotionItemDTO result = new TransferPromotionItemDTO();
            if (itemModel != null)
            {
                result.FromMasterTansferPromotionItemID = itemModel.ID;
                result.NameTH = itemModel.NameTH;
                result.Quantity = itemModel.Quantity;
                result.PricePerUnit = itemModel.PricePerUnit;
                result.TotalPrice = itemModel.TotalPrice;
                result.UnitTH = itemModel.UnitTH;
                result.ItemType = PromotionItemType.Item;
                result.IsSelected = false;

                var subItems = db.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == itemModel.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.ID,
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
                result.FromMasterTansferPromotionItemID = freeItemModel.ID;
                result.NameTH = freeItemModel.NameTH;
                result.Quantity = freeItemModel.Quantity;
                result.UnitTH = freeItemModel.UnitTH;
                result.ItemType = PromotionItemType.FreeItem;
                result.IsSelected = false;
                return result;
            }
            else if (creditModel != null)
            {
                result.FromMasterTansferPromotionItemID = creditModel.ID;
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
