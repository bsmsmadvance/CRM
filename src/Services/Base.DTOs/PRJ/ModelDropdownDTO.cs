using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class ModelDropdownDTO
    {
        /// <summary>
        /// Identity Tower ID
        /// </summary>
        /// <example></example>
        public Guid Id { get; set; }
        /// <summary>
        ///  รหัสแบบบ้าน
        /// </summary>
        /// <example></example>
        public string Code { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (EN)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ประเภทบ้าน
        /// Master/api/TypeOfRealEstates/DropdownList
        /// </summary>
        public MST.TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }

        public static ModelDropdownDTO CreateFromModel(Model model)
        {
            if (model != null)
            {
                var result = new ModelDropdownDTO
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    TypeOfRealEstate=MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate)
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
