using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
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
    /// รายละเอียดของโปรโมชั่นก่อนขาย
    /// Model = MasterBookingPromotion
    /// </summary>
    public class MasterBookingPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น
        /// </summary>
        [Description("ชื่อโปรโมชั่น")]
        public string Name { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่เริ่มต้น
        /// </summary>
        [Description("วันที่เริ่มใช้งาน")]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด
        /// </summary>
        [Description("วันที่สิ้นสุดการใช้งาน")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด
        /// </summary>
        public decimal? CashDiscount { get; set; }
        /// <summary>
        /// ส่วนลด FGF
        /// </summary>
        public decimal? FGFDiscount { get; set; }
        /// <summary>
        /// ส่วนลดวันโอน
        /// </summary>
        public decimal? TransferDiscount { get; set; }
        /// <summary>
        /// สถานะ Active
        /// Master/api/MasterCenters?masterCenterGroupKey=PromotionStatus
        /// </summary>
        [Description("สถานะโปรโมชั่น")]
        public MST.MasterCenterDropdownDTO PromotionStatus { get; set; }
        /// <summary>
        /// สถานะการนำไปใช้งาน
        /// </summary>
        public bool IsUsed { get; set; }

        public static MasterBookingPromotionDTO CreateFromQueryResult(MasterBookingPromotionQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionDTO()
                {
                    Id = model.MasterBookingPromotion.ID,
                    PromotionNo = model.MasterBookingPromotion.PromotionNo,
                    Name = model.MasterBookingPromotion.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.MasterBookingPromotion.StartDate,
                    EndDate = model.MasterBookingPromotion.EndDate,
                    CashDiscount = model.MasterBookingPromotion.CashDiscount,
                    FGFDiscount = model.MasterBookingPromotion.FGFDiscount,
                    TransferDiscount = model.MasterBookingPromotion.TransferDiscount,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.MasterBookingPromotion.IsUsed,
                    Updated = model.MasterBookingPromotion.Updated,
                    UpdatedBy = model.MasterBookingPromotion.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterBookingPromotionDTO CreateFromModel(MasterBookingPromotion model)
        {
            if (model != null)
            {
                var result = new MasterBookingPromotionDTO()
                {
                    Id = model.ID,
                    PromotionNo = model.PromotionNo,
                    Name = model.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CashDiscount = model.CashDiscount,
                    FGFDiscount = model.FGFDiscount,
                    TransferDiscount = model.TransferDiscount,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.IsUsed,
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

        public static void SortBy(MasterBookingPromotionSortByParam sortByParam, ref IQueryable<MasterBookingPromotionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterBookingPromotionSortBy.PromotionNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.PromotionNo);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.PromotionNo);
                        break;
                    case MasterBookingPromotionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.Name);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.Name);
                        break;
                    case MasterBookingPromotionSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case MasterBookingPromotionSortBy.StartDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.StartDate);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.StartDate);
                        break;
                    case MasterBookingPromotionSortBy.EndDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.EndDate);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.EndDate);
                        break;
                    case MasterBookingPromotionSortBy.CashDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.CashDiscount);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.CashDiscount);
                        break;
                    case MasterBookingPromotionSortBy.FGFDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.FGFDiscount);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.FGFDiscount);
                        break;
                    case MasterBookingPromotionSortBy.TransferDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.TransferDiscount);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.TransferDiscount);
                        break;
                    case MasterBookingPromotionSortBy.PromotionStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionStatus.Name);
                        break;
                    case MasterBookingPromotionSortBy.IsUsed:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.IsUsed);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.IsUsed);
                        break;
                    case MasterBookingPromotionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterBookingPromotion.Updated);
                        else query = query.OrderByDescending(o => o.MasterBookingPromotion.Updated);
                        break;
                    case MasterBookingPromotionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterBookingPromotion.PromotionNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterBookingPromotion.PromotionNo);
            }
        }

        public async Task ValidateAsync(DatabaseContext db, bool isEdit = false)
        {
            ValidateException ex = new ValidateException();
            if (isEdit)
            {
                if (string.IsNullOrEmpty(this.Name))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.Name.CheckAllLang(true, false, false, null, " "))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.StartDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.StartDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.EndDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.EndDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.PromotionStatus == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.PromotionStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            else
            {
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterBookingPromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterBookingPromotion model)
        {
            model.Name = this.Name;
            model.ProjectID = this.Project?.Id;
            model.StartDate = this.StartDate;
            model.EndDate = this.EndDate;
            model.CashDiscount = this.CashDiscount;
            model.FGFDiscount = this.FGFDiscount;
            model.TransferDiscount = this.TransferDiscount;
            model.PromotionStatusMasterCenterID = this.PromotionStatus?.Id;
        }
    }
    public class MasterBookingPromotionQueryResult
    {
        public MasterBookingPromotion MasterBookingPromotion { get; set; }
        public Project Project { get; set; }
        public MasterCenter PromotionStatus { get; set; }
        public User UpdatedBy { get; set; }
    }
}
