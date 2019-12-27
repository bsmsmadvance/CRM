﻿using Database.Models;
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
    public class RateSettingFixSaleTransferModelDTO : BaseDTO
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
        /// แบบบ้าน
        /// </summary>
        [Description("แบบบ้าน")]
        public PRJ.ModelDropdownDTO Model { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }

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

        /// <summary>
        /// List of ProjectID สำหรับบันทึกข้อมูล
        /// </summary>
        [Description("List of ProjectID สำหรับบันทึกข้อมูล")]
        public List<Guid> ListProjectId { get; set; }

        public static RateSettingFixSaleTransferModelDTO CreateFromFixSaleModelModel(RateSettingFixSaleModel model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleTransferModelDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    Amount = model.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CreateDate = model.Created,
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

        public static RateSettingFixSaleTransferModelDTO CreateFromFixSaleModelQueryResult(RateSettingFixSaleModelQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleTransferModelDTO()
                {
                    Id = model.RateSettingFixSaleModel.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    Amount = model.RateSettingFixSaleModel.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.RateSettingFixSaleModel.CreatedBy),
                    CreateDate = model.RateSettingFixSaleModel.Created,
                    IsActive = model.RateSettingFixSaleModel.IsActive,
                    ActiveDate = model.RateSettingFixSaleModel.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortByFixSaleModel(RateSettingFixSaleModelSortByParam sortByParam, ref IQueryable<RateSettingFixSaleModelQueryResult> query)
        {
            IOrderedQueryable<RateSettingFixSaleModelQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingFixSaleModelSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModel.ActiveDate);
                        break;
                    case RateSettingFixSaleModelSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModel.ProjectID);
                        break;
                    case RateSettingFixSaleModelSortBy.ModelID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.ModelID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModel.ModelID);
                        break;
                    case RateSettingFixSaleModelSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModel.Amount);
                        break;
                    case RateSettingFixSaleModelSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSaleModel.IsActive);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingFixSaleModel.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingFixSaleModel.ID);
            query = orderQuery;
        }

        public async Task FixSaleModelValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Model == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Model)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToFixSaleModelModel(ref RateSettingFixSaleModel model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.ModelID = this.Model.Id;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }


        public static RateSettingFixSaleTransferModelDTO CreateFromFixTransferModelModel(RateSettingFixTransferModel model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleTransferModelDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
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

        public static RateSettingFixSaleTransferModelDTO CreateFromFixTransferModelQueryResult(RateSettingFixTransferModelQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleTransferModelDTO()
                {
                    Id = model.RateSettingFixTransferModel.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Model = PRJ.ModelDropdownDTO.CreateFromModel(model.Model),
                    Amount = model.RateSettingFixTransferModel.Amount,
                    IsActive = model.RateSettingFixTransferModel.IsActive,
                    ActiveDate = model.RateSettingFixTransferModel.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortByFixTransferModel(RateSettingFixTransferModelSortByParam sortByParam, ref IQueryable<RateSettingFixTransferModelQueryResult> query)
        {
            IOrderedQueryable<RateSettingFixTransferModelQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingFixTransferModelSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModel.ActiveDate);
                        break;
                    case RateSettingFixTransferModelSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModel.ProjectID);
                        break;
                    case RateSettingFixTransferModelSortBy.ModelID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.ModelID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModel.ModelID);
                        break;
                    case RateSettingFixTransferModelSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModel.Amount);
                        break;
                    case RateSettingFixTransferModelSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransferModel.IsActive);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingFixTransferModel.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingFixTransferModel.ID);
            query = orderQuery;
        }

        public async Task FixTransferModelValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Model == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Model)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleTransferModelDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToFixTransferModelModel(ref RateSettingFixTransferModel model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.ModelID = this.Model.Id;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingFixSaleModelQueryResult
    {
        public RateSettingFixSaleModel RateSettingFixSaleModel { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Model Model { get; set; }
        public User CreatedByUser { get; set; }
    }

    public class RateSettingFixTransferModelQueryResult
    {
        public RateSettingFixTransferModel RateSettingFixTransferModel { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.PRJ.Model Model { get; set; }
        public User CreatedByUser { get; set; }
    }
}
