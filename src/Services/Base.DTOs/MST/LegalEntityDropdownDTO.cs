using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.MST
{
    public class LegalEntityDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อนิติบุคคลภาษา (TH)
        /// </summary>
        [Description("ชื่อนิติบุคคลภาษา (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อนิติบุคคลภาษา (EN)
        /// </summary>
        public string NameEN { get; set; }

        public static LegalEntityDropdownDTO CreateFromModel(LegalEntity model)
        {
            if (model != null)
            {
                var result = new LegalEntityDropdownDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
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
