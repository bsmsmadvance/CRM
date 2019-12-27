using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class CompanyDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// รหัสริษัท
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อบริษัทภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// รหัสบริษัท SAP
        /// </summary>
        public string SAPCompanyID { get; set; }

        public static CompanyDropdownDTO CreateFromModel(Company model)
        {
            if (model != null)
            {
                var result = new CompanyDropdownDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    SAPCompanyID = model.SAPCompanyID
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
