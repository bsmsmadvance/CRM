using Database.Models.PRM;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// Item ที่มาจาก SAP สำหรับเลือกลงรายการโปร
    /// </summary>
    public class PromotionMaterialDTO : BaseDTO
    {
        /// <summary>
        /// Agreement No.
        /// </summary>
        [Description("Agreement No.")]
        public string AgreementNo { get; set; }
        /// <summary>
        /// เลขที่สิ่งของ
        /// </summary>
        [Description("Item No.")]
        public string ItemNo { get; set; }
        /// <summary>
        /// Plant
        /// </summary>
        public string Plant { get; set; }
        /// <summary>
        /// ชื่อสิ่งของ (ภาษาไทย)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อสิ่งของ (ภาษาอังกฤษ)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// Material Code
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// ราคา
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// วันหมดอายุ
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        public static PromotionMaterialDTO CreateFromModel(PromotionMaterialItem model)
        {
            if (model != null)
            {
                var result = new PromotionMaterialDTO()
                {
                    Id = model.ID,
                    AgreementNo = model.AgreementNo,
                    ItemNo = model.ItemNo,
                    Plant = model.Plant,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    MaterialCode = model.MaterialCode,
                    Price = model.Price,
                    Unit = model.UnitTH,
                    ExpireDate = model.ExpireDate,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static PromotionMaterialDTO CreateFromQueryResult(PromotionMaterialQueryResult model)
        {
            if (model != null)
            {
                var result = new PromotionMaterialDTO()
                {
                    Id = model.PromotionMaterialItem.ID,
                    AgreementNo = model.PromotionMaterialItem.AgreementNo,
                    ItemNo = model.PromotionMaterialItem.ItemNo,
                    Plant = model.PromotionMaterialItem.Plant,
                    NameTH = model.PromotionMaterialItem.NameTH,
                    NameEN = model.PromotionMaterialItem.NameEN,
                    MaterialCode = model.PromotionMaterialItem.MaterialCode,
                    Price = model.PromotionMaterialItem.Price,
                    Unit = model.PromotionMaterialItem.UnitTH,
                    ExpireDate = model.PromotionMaterialItem.ExpireDate,
                    Updated = model.PromotionMaterialItem.Updated,
                    UpdatedBy = model.PromotionMaterialItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(PromotionMaterialSortByParam sortByParam, ref IQueryable<PromotionMaterialQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PromotionMaterialSortBy.AgreementNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.AgreementNo);
                        break;
                    case PromotionMaterialSortBy.ItemNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ItemNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.ItemNo);
                        break;
                    case PromotionMaterialSortBy.Plant:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Plant);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.Plant);
                        break;
                    case PromotionMaterialSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameTH);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameTH);
                        break;
                    case PromotionMaterialSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameEN);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameEN);
                        break;
                    case PromotionMaterialSortBy.MaterialCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.MaterialCode);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.MaterialCode);
                        break;
                    case PromotionMaterialSortBy.Price:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Price);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.Price);
                        break;
                    case PromotionMaterialSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.UnitTH);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.UnitTH);
                        break;
                    case PromotionMaterialSortBy.ExpireDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ExpireDate);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.ExpireDate);
                        break;
                    case PromotionMaterialSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Updated);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.Updated);
                        break;
                    case PromotionMaterialSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
            }
        }
        public void ToMasterBookingPromotionItemModel(ref MasterBookingPromotionItem model)
        {
            model.PromotionMaterialItemID = this.Id;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = 1;
            model.UnitTH = this.Unit;
            model.PricePerUnit = this.Price;
            model.TotalPrice = this.Price;
            model.ReceiveDays = 45;
            model.IsPurchasing = true;
            model.IsShowInContract = true;
            model.ExpireDate = this.ExpireDate;
        }
        public void ToMasterTransferPromotionItemModel(ref MasterTransferPromotionItem model)
        {
            model.PromotionMaterialItemID = this.Id;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = 1;
            model.UnitTH = this.Unit;
            model.PricePerUnit = this.Price;
            model.TotalPrice = this.Price;
            model.ReceiveDays = 45;
            model.IsPurchasing = true;
            model.IsShowInContract = true;
            model.ExpireDate = this.ExpireDate;
        }

        public void ToMasterPreSalePromotionItemModel(ref MasterPreSalePromotionItem model)
        {
            model.PromotionMaterialItemID = this.Id;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = 1;
            model.UnitTH = this.Unit;
            model.PricePerUnit = this.Price;
            model.TotalPrice = this.Price;
            model.ReceiveDays = 45;
            model.IsPurchasing = true;
            model.IsShowInContract = true;
            model.ExpireDate = this.ExpireDate;
        }
    }
    public class PromotionMaterialQueryResult
    {
        public PromotionMaterialItem PromotionMaterialItem { get; set; }
        public User UpdatedBy { get; set; }
    }
}
