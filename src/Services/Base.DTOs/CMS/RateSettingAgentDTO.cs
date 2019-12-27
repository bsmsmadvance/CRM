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
    public class RateSettingAgentDTO : BaseDTO
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
        /// Agency
        /// </summary>
        [Description("Agency")]
        public MST.AgentDTO Agent { get; set; }

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

        public static RateSettingAgentDTO CreateFromModel(RateSettingAgent model)
        {
            if (model != null)
            {
                var result = new RateSettingAgentDTO()
                {
                    Id = model.ID,
                    ActiveDate = model.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Agent = MST.AgentDTO.CreateFromModel(model.Agent),
                    Amount = model.Amount,
                    IsActive = model.IsActive
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static RateSettingAgentDTO CreateFromQueryResult(RateSettingAgentQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingAgentDTO()
                {
                    Id = model.RateSettingAgent?.ID,
                    ActiveDate = model.RateSettingAgent.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Agent = MST.AgentDTO.CreateFromModel(model.RateSettingAgent.Agent ?? model.Agent),
                    Amount = model.RateSettingAgent.Amount,
                    IsActive = model.RateSettingAgent.IsActive
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RateSettingAgentSortByParam sortByParam, ref IQueryable<RateSettingAgentQueryResult> query)
        {
            IOrderedQueryable<RateSettingAgentQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingAgentSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgent.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgent.ActiveDate);
                        break;
                    case RateSettingAgentSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgent.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgent.Project.ProjectNo);
                        break;
                    case RateSettingAgentSortBy.AgentID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgent.Agent.NameTH);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgent.Agent.NameTH);
                        break;
                    case RateSettingAgentSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgent.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgent.Amount);
                        break;
                    case RateSettingAgentSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingAgent.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingAgent.IsActive);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingAgent.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingAgent.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingAgent.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingAgentDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingAgentDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Agent == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingAgentDTO.Agent)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingAgentDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref RateSettingAgent model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.AgentID = this.Agent.Id;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingAgentQueryResult
    {
        public RateSettingAgent RateSettingAgent { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.MST.Agent Agent { get; set; }
    }
}
