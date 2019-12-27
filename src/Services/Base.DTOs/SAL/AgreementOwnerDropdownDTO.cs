using System;
using System.Collections.Generic;
using System.Text;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class AgreementOwnerDropdownDTO : BaseDTO
    {
        /// <summary>
        /// รหัสลูกค้า
        /// </summary>
        public string ContactNo { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }

        public static AgreementOwnerDropdownDTO CreateFromModel(AgreementOwner model)
        {
            if (model != null)
            {
                AgreementOwnerDropdownDTO result = new AgreementOwnerDropdownDTO
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    FirstNameTH = model.FirstNameTH,
                    LastNameTH = model.LastNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    ContactNo = model.FromContact?.ContactNo
                    
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
