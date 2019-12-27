using Database.Models.PRM;
using System;
namespace Base.DTOs.PRM
{
    public class MappingAgreementDTO : BaseDTO
    {
        /// <summary>
        /// Running Number
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// สถานะความถูกต้องชองข้อมูล
        /// ถ้ามี Field ครบ ถือว่าถูกต้อง (ยกเว้น Remark)
        /// </summary>
        public bool IsValidData { get; set; }
        /// <summary>
        /// เลขที่ Agreement เดิม
        /// </summary>
        public string OldAgreement { get; set; }
        /// <summary>
        /// เลขที่ Item เดิม
        /// </summary>
        public string OldItem { get; set; }
        /// <summary>
        /// เลขที่ Material Code เดิม
        /// </summary>
        public string OldMaterialCode { get; set; }
        /// <summary>
        /// เลขที่ Agreement ใหม่
        /// </summary>
        public string NewAgreement { get; set; }
        /// <summary>
        /// เลขที่ Item ใหม่
        /// </summary>
        public string NewItem { get; set; }
        /// <summary>
        /// เลขที่ Material Code ใหม่
        /// </summary>
        public string NewMaterialCode { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public static MappingAgreementDTO CreateFromModel(MappingAgreement model)
        {
            if (model != null)
            {
                var result = new MappingAgreementDTO()
                {
                    Id = model.ID,
                    OldAgreement = model.OldAgreement,
                    OldItem = model.OldItem,
                    OldMaterialCode = model.OldMaterialCode,
                    NewAgreement = model.NewAgreement,
                    NewItem = model.NewItem,
                    NewMaterialCode = model.NewMaterialCode,
                    Remark = model.Remark,
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
        public void ToModel(ref MappingAgreement model)
        {
            model.OldAgreement = this.OldAgreement;
            model.OldItem = this.OldItem;
            model.OldMaterialCode = this.OldMaterialCode;
            model.NewAgreement = this.NewAgreement;
            model.NewItem = this.NewItem;
            model.NewMaterialCode = this.NewMaterialCode;
            model.Remark = this.Remark;
        }
    }
}
