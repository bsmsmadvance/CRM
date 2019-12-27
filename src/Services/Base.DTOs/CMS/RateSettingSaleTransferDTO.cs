using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class RateSettingSaleTransferDTO : BaseDTO
    {
        /// <summary>
        /// Effective Month
        /// </summary>
        [Description("Effective Month")]
        public DateTime? ActiveDate { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// จำนวนเงินเริ่มต้น
        /// </summary>
        [Description("จำนวนเงินเริ่มต้น")]
        public decimal StartRange { get; set; }

        /// <summary>
        /// จำนวนเงินสิ้นสุด
        /// </summary>
        [Description("จำนวนเงินสิ้นสุด")]
        public decimal EndRange { get; set; }

        /// <summary>
        /// จำนวนเปอร์เซ็น
        /// </summary>
        [Description("จำนวนเปอร์เซ็น")]
        public double Amount { get; set; }

        /// <summary>
        /// ผู้สร้าง
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("ผู้สร้าง")]
        public USR.UserListDTO CreatedByUser { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        [Description("วันที่สร้าง")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public bool IsActive { get; set; }

        public static RateSettingSaleTransferDTO CreateFromSaleModel(RateSettingSale model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleTransferDTO()
                {
                    Id = model.ID,
                    ActiveDate = model.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.StartRange,
                    EndRange = model.EndRange,
                    Amount = model.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CreateDate = model.Created,
                    IsActive = model.IsActive
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static RateSettingSaleTransferDTO CreateFromDistinctSaleQueryResult(RateSettingSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleTransferDTO()
                {
                    ActiveDate = model.RateSettingSale.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = 0,
                    EndRange = 0,
                    Amount = 0,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.RateSettingSale.CreatedBy),
                    CreateDate = new DateTime(model.RateSettingSale.Created.Value.Year, model.RateSettingSale.Created.Value.Month, model.RateSettingSale.Created.Value.Day, model.RateSettingSale.Created.Value.Hour, model.RateSettingSale.Created.Value.Minute, 0),
                    IsActive = model.RateSettingSale.IsActive
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static RateSettingSaleTransferDTO CreateFromSaleQueryResult(RateSettingSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleTransferDTO()
                {
                    Id = model.RateSettingSale?.ID,
                    ActiveDate = model.RateSettingSale.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.RateSettingSale.StartRange,
                    EndRange = model.RateSettingSale.EndRange,
                    Amount = model.RateSettingSale.Amount == 0 && model.RateSale != null ? model.RateSale.Rate : model.RateSettingSale.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.RateSettingSale.CreatedBy),
                    CreateDate = model.RateSettingSale.Created,
                    IsActive = model.RateSettingSale.IsActive
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBySale(RateSettingSaleSortByParam sortByParam, ref IQueryable<RateSettingSaleQueryResult> query)
        {
            IOrderedQueryable<RateSettingSaleQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingSaleSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.ActiveDate);
                        break;
                    case RateSettingSaleSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.ProjectID);
                        break;
                    case RateSettingSaleSortBy.StartRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.StartRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.StartRange);
                        break;
                    case RateSettingSaleSortBy.EndRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.EndRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.EndRange);
                        break;
                    case RateSettingSaleSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.Amount);
                        break;
                    case RateSettingSaleSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.IsActive);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingSale.ID);
            query = orderQuery;
        }

        public async Task SaleValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.StartRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.StartRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.EndRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.EndRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToSaleModel(ref RateSettingSale model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.StartRange = this.StartRange;
            model.EndRange = this.EndRange;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }

        public static RateSettingSaleTransferDTO CreateFromTransferModel(RateSettingTransfer model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleTransferDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.StartRange,
                    EndRange = model.EndRange,
                    Amount = model.Amount,
                    IsActive = model.IsActive,
                    ActiveDate = model.ActiveDate
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static RateSettingSaleTransferDTO CreateFromTransferQueryResult(RateSettingTransferQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleTransferDTO()
                {
                    Id = model.RateSettingTransfer.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.RateSettingTransfer.StartRange,
                    EndRange = model.RateSettingTransfer.EndRange,
                    Amount = model.RateSettingTransfer.Amount == 0 && model.RateTransfer != null ? model.RateTransfer.Rate : model.RateSettingTransfer.Amount,
                    IsActive = model.RateSettingTransfer.IsActive,
                    ActiveDate = model.RateSettingTransfer.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortByTransfer(RateSettingTransferSortByParam sortByParam, ref IQueryable<RateSettingTransferQueryResult> query)
        {
            IOrderedQueryable<RateSettingTransferQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingTransferSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.ActiveDate);
                        break;
                    case RateSettingTransferSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.ProjectID);
                        break;
                    case RateSettingTransferSortBy.StartRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.StartRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.StartRange);
                        break;
                    case RateSettingTransferSortBy.EndRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.EndRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.EndRange);
                        break;
                    case RateSettingTransferSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.Amount);
                        break;
                    case RateSettingTransferSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingTransfer.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingTransfer.IsActive);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingTransfer.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingTransfer.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingTransfer.ID);
            query = orderQuery;
        }

        public async Task TransferValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.StartRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.StartRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.EndRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.EndRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleTransferDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToTransferModel(ref RateSettingTransfer model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.StartRange = this.StartRange;
            model.EndRange = this.EndRange;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingSaleQueryResult
    {
        public RateSettingSale RateSettingSale { get; set; }
        public models.PRJ.Project Project { get; set; }
        public RateSale RateSale { get; set; }
    }

    public class RateSettingTransferQueryResult
    {
        public RateSettingTransfer RateSettingTransfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public RateTransfer RateTransfer { get; set; }
    }
}
