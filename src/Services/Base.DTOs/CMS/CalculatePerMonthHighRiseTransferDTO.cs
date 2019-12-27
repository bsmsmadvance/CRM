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
    public class CalculatePerMonthHighRiseTransferDTO : BaseDTO
    {
        /// <summary>
        /// Commission For
        /// </summary>
        [Description("Commission For")]
        public DateTime? PeriodMonth { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// คำนวณโดย
        /// </summary>
        [Description("คำนวณโดย")]
        public USR.UserListDTO CalculateUserName { get; set; }

        /// <summary>
        /// วันที่คำนวณ
        /// </summary>
        [Description("วันที่คำนวณ")]
        public DateTime? CalculateDate { get; set; }

        /// <summary>
        /// Commission Type
        /// </summary>
        [Description("Commission Type")]
        public string CommissionType { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public string Status { get; set; }

        /// <summary>
        /// รายละเอียดผลการคำนวณ
        /// </summary>
        [Description("รายละเอียดผลการคำนวณ")]
        public List<CommissionHighRiseTransferVeiwDTO> CommissionHighRiseTransferVeiws { get; set; }

        public static CalculatePerMonthHighRiseTransferDTO CreateFromModel(CalculatePerMonthHighRiseTransfer model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseTransferDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    PeriodMonth = new DateTime(model.PeriodYear, model.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CalculateDate = model.Created,
                    Status = ((model.IsApprove.HasValue && model.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ")
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static CalculatePerMonthHighRiseTransferDTO CreateFromQueryResult(CalculatePerMonthHighRiseTransferQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseTransferDTO()
                {
                    Id = model.CalculatePerMonthHighRiseTransfer?.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.CalculatePerMonthHighRiseTransfer.Project),
                    PeriodMonth = new DateTime(model.CalculatePerMonthHighRiseTransfer.PeriodYear, model.CalculatePerMonthHighRiseTransfer.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CalculatePerMonthHighRiseTransfer.CreatedBy),
                    CalculateDate = model.CalculatePerMonthHighRiseTransfer.Created,
                    Status = ((model.CalculatePerMonthHighRiseTransfer.IsApprove.HasValue && model.CalculatePerMonthHighRiseTransfer.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ")
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthHighRiseTransferSortByParam sortByParam, ref IQueryable<CalculatePerMonthHighRiseTransferQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthHighRiseTransferQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CalculatePerMonthHighRiseTransferSortBy.PeriodMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseTransfer.PeriodYear).ThenBy(o => o.CalculatePerMonthHighRiseTransfer.PeriodMonth);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodMonth); 
                        break;
                    case CalculatePerMonthHighRiseTransferSortBy.CalculateUserName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseTransfer.CreatedBy.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.CreatedBy.DisplayName);
                        break;
                    case CalculatePerMonthHighRiseTransferSortBy.CalculateDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseTransfer.Created);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.Created);
                        break;
                    case CalculatePerMonthHighRiseTransferSortBy.Status:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseTransfer.IsApprove);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.IsApprove);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodMonth);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseTransfer.PeriodMonth);
            }

            orderQuery.ThenBy(o => o.CalculatePerMonthHighRiseTransfer.ID);
            query = orderQuery;
        }

        /*
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
           
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CalculatePerMonthHighRiseTransferDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref CalculatePerMonthHighRiseTransfer model)
        {
            model.PeriodYear = this.PeriodMonth.Value.Year;
            model.PeriodMonth = this.PeriodMonth.Value.Month;
            model.ProjectID = this.Project.Id;
        }
        */
    }

    public class CalculatePerMonthHighRiseTransferQueryResult
    {
        public CalculatePerMonthHighRiseTransfer CalculatePerMonthHighRiseTransfer { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User CalculateUserName { get; set; }
    }
}
