using System;
using Database.Models.SAL;

namespace Base.DTOs.SAL
{
    public class BookingOwnerDropdownDTO : BaseDTO
    {
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

        public static BookingOwnerDropdownDTO CreateFromModel(BookingOwner model)
        {
            if (model != null)
            {
                BookingOwnerDropdownDTO result = new BookingOwnerDropdownDTO
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    FirstNameTH = model.FirstNameTH,
                    LastNameTH = model.LastNameTH,
                    MiddleNameTH = model.MiddleNameTH
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
