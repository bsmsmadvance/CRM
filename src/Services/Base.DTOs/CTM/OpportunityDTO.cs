using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityDTO
    {
        /// <summary>
        /// ID ของ Opportunity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ข้อมูล Contact
        /// </summary>
        public ContactListDTO Contact { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// ประเมินโอกาสการขาย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=EstimateSalesOpportunity
        /// </summary>
        [Description("ประเมินโอกาสการขาย")]
        public MST.MasterCenterDropdownDTO EstimateSalesOpportunity { get; set; }
        /// <summary>
        /// โอกาสการขาย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
        /// </summary>
        public MST.MasterCenterDropdownDTO SalesOpportunity { get; set; }
        /// <summary>
        /// สินค้าที่สนใจ 1
        /// </summary>
        public string InterestedProduct1 { get; set; }
        /// <summary>
        /// สินค้าที่สนใจ 2
        /// </summary>
        public string InterestedProduct2 { get; set; }
        /// <summary>
        /// สินค้าที่สนใจ 3
        /// </summary>
        public string InterestedProduct3 { get; set; }
        /// <summary>
        /// สถานะทำแบบสอบถาม
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
        /// </summary>
        public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }
        /// <summary>
        /// จำนวนแปลงที่สนใจ
        /// </summary>
        public int ProductQTY { get; set; }
        /// <summary>
        /// โครงการเปรียบเทียบ
        /// </summary>
        public string ProjectCompare { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// เหตุผลที่ซื้อ
        /// </summary>
        public string BuyReason { get; set; }
        /// <summary>
        /// วันที่เยี่ยมชม
        /// </summary>
        [Description("วันที่เยี่ยมชม")]
        public DateTime? ArriveDate { get; set; }
        /// <summary>
        /// จำนวนคำถามที่ตอบคำถามแล้ว
        /// </summary>
        public int? AnsweredQuestionaire { get; set; }
        /// <summary>
        /// จำนวนคำถามทั้งหมด
        /// </summary>
        public int? TotalQuestionaire { get; set; }
        /// <summary>
        /// ผู้ดูแล
        /// </summary>
        public USR.UserListDTO Owner { get; set; }

        public static OpportunityDTO CreateFromModel(models.CTM.Opportunity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                OpportunityDTO result = new OpportunityDTO()
                {
                    Id = model.ID,
                    EstimateSalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.EstimateSalesOpportunity),
                    SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.SalesOpportunity),
                    InterestedProduct1 = model.InterestedProduct1,
                    InterestedProduct2 = model.InterestedProduct2,
                    InterestedProduct3 = model.InterestedProduct3,
                    StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateFromModel(model.StatusQuestionaire),
                    ProductQTY = model.ProductQTY,
                    ProjectCompare = model.ProjectCompare,
                    Remark = model.Remark,
                    BuyReason = model.BuyReason,
                    ArriveDate = model.ArriveDate,
                    Owner = USR.UserListDTO.CreateFromModel(model.Owner),
                };

                if(model.Contact != null)
                    result.Contact = ContactListDTO.CreateFromModel(model.Contact, DB);
                if (model.Project != null)
                    result.Project = PRJ.ProjectDTO.CreateFromModel(model.Project);

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();
            if (this.Project == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(OpportunityDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.EstimateSalesOpportunity == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(OpportunityDTO.EstimateSalesOpportunity)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.ArriveDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(OpportunityDTO.ArriveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.Opportunity model)
        {
            model.InterestedProduct1 = this.InterestedProduct1;
            model.InterestedProduct2 = this.InterestedProduct2;
            model.InterestedProduct3 = this.InterestedProduct3;
            model.EstimateSalesOpportunityMasterCenterID = this.EstimateSalesOpportunity?.Id;
            model.SalesOpportunityMasterCenterID = this.SalesOpportunity?.Id;
            model.ArriveDate = this.ArriveDate;
        }
    }
}
