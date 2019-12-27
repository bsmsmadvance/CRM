using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการโปรโมชั่นขาย
    /// Model = MasterBookingPromotionItem
    /// </summary>
    public class MasterBookingPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        [Description("ชื่อผลิตภัณฑ์ (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (EN)
        /// </summary>
        [Description("ชื่อผลิตภัณฑ์ (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// หน่วย (TH)
        /// </summary>
        [Description("หน่วย (TH)")]
        public string UnitTH { get; set; }
        /// <summary>
        /// หน่วย (EN)
        /// </summary>
        [Description("หน่วย (EN)")]
        public string UnitEN { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// วันที่ได้รับ
        /// </summary>
        public int? ReceiveDays { get; set; }
        /// <summary>
        /// ลูกค้าได้รับเมื่อ?
        /// Master/api/MasterCenters?masterCenterGroupKey=WhenPromotionReceive
        /// </summary>
        [Description("ลูกค้าได้รับเมื่อ?")]
        public MST.MasterCenterDropdownDTO WhenPromotionReceive { get; set; }
        /// <summary>
        /// การจัดซื้อ?
        /// </summary>
        public bool IsPurchasing { get; set; }
        /// <summary>
        /// แสดงในสัญญา
        /// </summary>
        public bool IsShowInContract { get; set; }
        /// <summary>
        /// สถานะ
        ///  Master/api/MasterCenters?masterCenterGroupKey=PromotionItemStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionItemStatus { get; set; }
        /// <summary>
        /// วันที่หมดอายุ
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)
        /// </summary>
        public Guid? MainPromotionItemID { get; set; }
        /// <summary>
        /// PromotionMaterial
        /// </summary>
        public PromotionMaterialDTO PromotionMaterial { get; set; }

        public static MasterBookingPromotionItemDTO CreateFromModel(MasterBookingPromotionItem model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionItemDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Quantity = model.Quantity,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    ReceiveDays = model.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsPurchasing = model.IsPurchasing,
                    IsShowInContract = model.IsShowInContract,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    ExpireDate = model.ExpireDate,
                    MainPromotionItemID = model.MainPromotionItemID,
                    PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem),
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
        public static MasterBookingPromotionItemDTO CreateFromQueryResult(MasterBookingPromotionItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionItemDTO()
                {
                    Id = model.MasterBookingPromotionItem.ID,
                    NameTH = model.MasterBookingPromotionItem.NameTH,
                    NameEN = model.MasterBookingPromotionItem.NameEN,
                    Quantity = model.MasterBookingPromotionItem.Quantity,
                    UnitTH = model.MasterBookingPromotionItem.UnitTH,
                    UnitEN = model.MasterBookingPromotionItem.UnitEN,
                    PricePerUnit = model.MasterBookingPromotionItem.PricePerUnit,
                    TotalPrice = model.MasterBookingPromotionItem.TotalPrice,
                    ReceiveDays = model.MasterBookingPromotionItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsPurchasing = model.MasterBookingPromotionItem.IsPurchasing,
                    IsShowInContract = model.MasterBookingPromotionItem.IsShowInContract,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    ExpireDate = model.MasterBookingPromotionItem.ExpireDate,
                    MainPromotionItemID = model.MasterBookingPromotionItem.MainPromotionItemID,
                    PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem),
                    Updated = model.MasterBookingPromotionItem.Updated,
                    UpdatedBy = model.MasterBookingPromotionItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(MasterBookingPromotionItemSortByParam sortByParam, ref IQueryable<MasterBookingPromotionItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterBookingPromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.NameTH);
                        break;
                    case MasterBookingPromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.NameEN);
                        break;
                    case MasterBookingPromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.Quantity);
                        break;
                    case MasterBookingPromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.UnitTH);
                        break;
                    case MasterBookingPromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.UnitEN);
                        break;
                    case MasterBookingPromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.PricePerUnit);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.PricePerUnit);
                        break;
                    case MasterBookingPromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.TotalPrice);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.TotalPrice);
                        break;
                    case MasterBookingPromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.ReceiveDays);
                        break;
                    case MasterBookingPromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterBookingPromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.IsPurchasing);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.IsPurchasing);
                        break;
                    case MasterBookingPromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.IsShowInContract);
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterBookingPromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.ExpireDate);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.ExpireDate);
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.AgreementNo);
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ItemNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.ItemNo);
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameTH);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameTH);
                        break;
                    case MasterBookingPromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionItem.Updated);
                        break;
                    case MasterBookingPromotionItemSortBy.UpdatedBy:
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
        public static void SortByDTO(MasterBookingPromotionItemSortByParam sortByParam, ref List<MasterBookingPromotionItemDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterBookingPromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameTH).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameEN).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Quantity).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Quantity).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitTH).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitEN).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PricePerUnit).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PricePerUnit).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.TotalPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.TotalPrice).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ReceiveDays).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ReceiveDays).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.WhenPromotionReceive.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.WhenPromotionReceive.Name).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsPurchasing).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsPurchasing).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsShowInContract).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsShowInContract).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionItemStatus.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionItemStatus.Name).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ExpireDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ExpireDate).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.AgreementNo).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.ItemNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.ItemNo).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.NameTH).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case MasterBookingPromotionItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UpdatedBy).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                        break;
                }
            }
            else
            {
                listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckAllLang(true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckAllLang(true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.UnitTH.CheckAllLang(false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.UnitEN.CheckAllLang(false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterBookingPromotionItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = this.Quantity;
            model.UnitTH = this.UnitTH;
            model.UnitEN = this.UnitEN;
            model.ReceiveDays = this.ReceiveDays;
            model.WhenPromotionReceiveMasterCenterID = this.WhenPromotionReceive?.Id;
            model.IsPurchasing = this.IsPurchasing;
            model.IsShowInContract = this.IsShowInContract;
            model.PromotionItemStatusMasterCenterID = this.PromotionItemStatus?.Id;
        }
    }
    public class MasterBookingPromotionItemQueryResult
    {
        public MasterBookingPromotionItem MasterBookingPromotionItem { get; set; }
        public PromotionMaterialItem PromotionMaterialItem { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
    }
}
