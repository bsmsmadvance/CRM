using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadActivityDTO
    {
        /// <summary>
        /// ID ของ Lead Activity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Lead
        /// </summary>
        public Guid LeadID { get; set; }
        /// <summary>
        /// ประเภทของ Activity เช่น โทรติดตามลูกค้าครั้งที่ 3
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityType
        /// </summary>
        [Description("ประเภท Activity Lead")]
        public MST.MasterCenterDropdownDTO ActivityType { get; set; }
        /// <summary>
        /// วันที่ต้องทำ
        /// </summary>
        [Description("วันที่ต้องทำ")]
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// วันที่ทำจริง
        /// </summary>
        [Description("วันที่ทำจริง")]
        public DateTime? ActualDate { get; set; }
        /// <summary>
        /// เวลาที่สะดวกติดต่อกลับ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConvenientTime
        /// </summary>
        public MST.MasterCenterDropdownDTO ConvenientTime { get; set; }
        /// <summary>
        /// วันที่นัดหมาย
        /// </summary>
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// รายละเอียดการติดต่อ (ผลการติดตาม Status)
        /// </summary>
        [Description("ผลการติดตาม")]
        public List<LeadActivityStatusDTO> ActivityStatuses { get; set; }
        /// <summary>
        /// รายละเอียดเพิ่มเติม
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// สร้างโดย
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Activity สำเร็จแล้วหรือไม่
        /// </summary>
        public bool? IsCompleted { get; set; }
        /// <summary>
        /// ผลการติดตาม
        /// </summary>
        [Description("ผลการติดตาม")]
        public Guid? SelectedActivityStatusID { get; set; }
        /// <summary>
        /// วันที่ต้องทำ สำหรับกรณีเลือกผลการติดตามที่เป็น Follow UP
        /// </summary>
        [Description("วันที่ต้องทำ (Follow Up)")]
        public DateTime? FollowUpDueDate { get; set; }
        /// <summary>
        /// เป็น Activity ที่สร้างจากการ FollowUp โดยอัตโนมัติ
        /// </summary>
        public bool? IsFollowUpActivity { get; set; }

        public static async Task<LeadActivityDTO> CreateFromModelAsync(models.CTM.LeadActivity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                LeadActivityDTO result = new LeadActivityDTO()
                {
                    Id = model.ID,
                    LeadID = model.LeadID,
                    DueDate = model.DueDate,
                    ActualDate = model.ActualDate,
                    AppointmentDate = model.AppointmentDate,
                    Description = model.Description,
                    CreatedDate = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    UpdatedDate = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadActivityType),
                    ConvenientTime = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConvenientTime),
                    IsCompleted = model.IsCompleted,
                    SelectedActivityStatusID = model.StatusID,
                    FollowUpDueDate = model.StatusDueDate,
                    IsFollowUpActivity = model.IsFollowUpActivity
                };

                var activityStatusResults = await DB.LeadActivityStatuses
                    .Include(o => o.LeadActivityFollowUpType)
                    .Include(o => o.LeadActivityStatusType)
                    .Where(o => o.IsActive)
                    .OrderBy(o => o.Order)
                    .ToListAsync();

                result.ActivityStatuses = activityStatusResults.Select(o => LeadActivityStatusDTO.CreateFromModel(o)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<LeadActivityDTO> CreateDraftFromModelAsync(models.CTM.LeadActivity latestModel, models.DatabaseContext DB)
        {
            LeadActivityDTO result = new LeadActivityDTO();

            if (latestModel != null)
            {
                var lastActivityType = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == "LeadActivityType")
                    .OrderByDescending(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .FirstOrDefaultAsync();

                var activityTypeOrder = await DB.MasterCenters
                .Where(x => x.Key == latestModel.LeadActivityType.Key && x.MasterCenterGroupKey == "LeadActivityType")
                .Select(x => x.Order).FirstOrDefaultAsync();

                var activityType = await DB.MasterCenters
                .Where(x => x.Order == (activityTypeOrder + 1) && x.MasterCenterGroupKey == "LeadActivityType")
                .FirstOrDefaultAsync();

                if (activityType != null)
                {
                    result.ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(activityType);
                }
                else
                {
                    result.ActivityType = lastActivityType;
                }
            }
            else
            {
                var firstActivityType = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == "LeadActivityType")
                    .OrderBy(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .FirstOrDefaultAsync();

                result.ActivityType = firstActivityType;
            }

            var activityStatusResults = await DB.LeadActivityStatuses
                    .Include(o => o.LeadActivityFollowUpType)
                    .Include(o => o.LeadActivityStatusType)
                    .Where(o => o.IsActive)
                    .OrderBy(o => o.Order)
                    .ToListAsync();

            result.ActivityStatuses = activityStatusResults.Select(o => LeadActivityStatusDTO.CreateFromModel(o)).ToList();

            return result;

        }

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            if (this.ActivityType == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadActivityDTO.ActivityType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.DueDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LeadActivityDTO.DueDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if(this.SelectedActivityStatusID != null)
            {
                if (this.ActualDate == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadActivityDTO.ActualDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (this.ActualDate != null)
            {
                if (this.SelectedActivityStatusID == null)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LeadActivityDTO.SelectedActivityStatusID)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.LeadActivity model)
        {
            model.LeadActivityTypeMasterCenterID = this.ActivityType?.Id;
            model.DueDate = this.DueDate;
            model.ActualDate = this.ActualDate;
            model.ConvenientTimeMasterCenterID = this.ConvenientTime?.Id;
            model.AppointmentDate = this.AppointmentDate;
            model.Description = this.Description;
            model.StatusID = this.SelectedActivityStatusID;
            model.StatusDueDate = this.FollowUpDueDate;

            if (this.ActualDate != null)
            {
                model.IsCompleted = true;
            }
            else
            {
                model.IsCompleted = false;
            }
        }
    }
}
