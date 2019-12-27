using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการโปรโมชั่นที่ไม่ต้องจัดซื้อ
    /// Model = MasterBookingPromotionFreeItem
    /// </summary>
    public class MasterBookingPromotionFreeItemDTO : BaseDTO
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
        /// วันที่ได้รับ
        /// </summary>
        [Description("วันที่ได้รับ")]
        public int? ReceiveDays { get; set; }
        /// <summary>
        /// ลูกค้าได้รับเมื่อ?
        /// Master/api/MasterCenters?masterCenterGroupKey=WhenPromotionReceive
        /// </summary>
        [Description("ลูกค้าได้รับเมื่อ?")]
        public MST.MasterCenterDropdownDTO WhenPromotionReceive { get; set; }
        /// <summary>
        /// แสดงในสัญญา
        /// </summary>
        public bool IsShowInContract { get; set; }


        public static MasterBookingPromotionFreeItemDTO CreateFromQueryResult(MasterBookingPromotionFreeItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionFreeItemDTO()
                {
                    Id = model.MasterBookingPromotionFreeItem.ID,
                    NameTH = model.MasterBookingPromotionFreeItem.NameTH,
                    NameEN = model.MasterBookingPromotionFreeItem.NameEN,
                    Quantity = model.MasterBookingPromotionFreeItem.Quantity,
                    UnitTH = model.MasterBookingPromotionFreeItem.UnitTH,
                    UnitEN = model.MasterBookingPromotionFreeItem.UnitEN,
                    ReceiveDays = model.MasterBookingPromotionFreeItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.MasterBookingPromotionFreeItem.IsShowInContract,
                    Updated = model.MasterBookingPromotionFreeItem.Updated,
                    UpdatedBy = model.MasterBookingPromotionFreeItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterBookingPromotionFreeItemDTO CreateFromModel(MasterBookingPromotionFreeItem model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionFreeItemDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Quantity = model.Quantity,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    ReceiveDays = model.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.IsShowInContract,
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

        public static void SortBy(MasterBookingPromotionFreeItemSortByParam sortByParam, ref IQueryable<MasterBookingPromotionFreeItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterBookingPromotionFreeItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.NameTH);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.NameEN);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.Quantity);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.UnitTH);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.UnitEN);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.ReceiveDays);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.IsShowInContract);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotionFreeItem.Updated);
                        break;
                    case MasterBookingPromotionFreeItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterBookingPromotionFreeItem.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckAllLang(true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.UnitTH.CheckAllLang(false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.UnitEN.CheckLang(false, false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.ReceiveDays == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.ReceiveDays)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterBookingPromotionFreeItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = this.Quantity;
            model.UnitTH = this.UnitTH;
            model.UnitEN = this.UnitEN;
            model.ReceiveDays = this.ReceiveDays;
            model.WhenPromotionReceiveMasterCenterID = this.WhenPromotionReceive?.Id;
            model.IsShowInContract = this.IsShowInContract;
        }
    }
    public class MasterBookingPromotionFreeItemQueryResult
    {
        public MasterBookingPromotionFreeItem MasterBookingPromotionFreeItem { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
    }
}
