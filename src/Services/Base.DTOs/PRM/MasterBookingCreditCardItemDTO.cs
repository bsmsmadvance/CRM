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
    /// ค่าธรรมเนียมรูดบัตร
    /// Model = MasterBookingCreditCardItem
    /// </summary>
    public class MasterBookingCreditCardItemDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น (TH)
        /// </summary>
        [Description("ชื่อโปรโมชั่น (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น (EN)
        /// </summary>
        [Description("ชื่อโปรโมชั่น (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม (%)
        /// </summary>
        public double Fee { get; set; }
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
        /// สถานะ
        ///  Master/api/MasterCenters?masterCenterGroupKey=PromotionItemStatus
        /// </summary>
        [Description("สถานะ")]
        public MST.MasterCenterDropdownDTO PromotionItemStatus { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// มาจาก EDC ไหน
        /// </summary>
        public MST.EDCFeeDTO EDCFee { get; set; }

        public static MasterBookingCreditCardItemDTO CreateFromQueryResult(MasterBookingCreditCardItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterBookingCreditCardItemDTO()
                {
                    Id = model.MasterBookingCreditCardItem.ID,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.MasterBookingCreditCardItem.NameTH,
                    NameEN = model.MasterBookingCreditCardItem.NameEN,
                    Fee = model.MasterBookingCreditCardItem.Fee,
                    UnitTH = model.MasterBookingCreditCardItem.UnitTH,
                    UnitEN = model.MasterBookingCreditCardItem.UnitEN,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
                    Quantity = model.MasterBookingCreditCardItem.Quantity,
                    Updated = model.MasterBookingCreditCardItem.Updated,
                    UpdatedBy = model.MasterBookingCreditCardItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterBookingCreditCardItemDTO CreateFromModel(MasterBookingCreditCardItem model)
        {
            if (model != null)
            {
                var result = new MasterBookingCreditCardItemDTO()
                {
                    Id = model.ID,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Fee = model.Fee,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    Quantity = model.Quantity,
                    EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
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

        public static void SortBy(MasterBookingCreditCardItemSortByParam sortByParam, ref IQueryable<MasterBookingCreditCardItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterBookingCreditCardItemSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case MasterBookingCreditCardItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.NameTH);
                        break;
                    case MasterBookingCreditCardItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.NameEN);
                        break;
                    case MasterBookingCreditCardItemSortBy.Fee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.Fee);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.Fee);
                        break;
                    case MasterBookingCreditCardItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.UnitTH);
                        break;
                    case MasterBookingCreditCardItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.UnitEN);
                        break;
                    case MasterBookingCreditCardItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterBookingCreditCardItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.Quantity);
                        break;
                    case MasterBookingCreditCardItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingCreditCardItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterBookingCreditCardItem.Updated);
                        break;
                    case MasterBookingCreditCardItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH);
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
                if (!this.NameTH.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingCreditCardItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingCreditCardItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.PromotionItemStatus == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterBookingCreditCardItemDTO.PromotionItemStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterBookingCreditCardItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.PromotionItemStatusMasterCenterID = this.PromotionItemStatus?.Id;
        }
    }
    public class MasterBookingCreditCardItemQueryResult
    {
        public MasterBookingCreditCardItem MasterBookingCreditCardItem { get; set; }
        public Bank Bank { get; set; }
        public EDCFee EDCFee { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public User UpdatedBy { get; set; }
    }
}
