using System;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class CancelReasonDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// รหัส ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Key { get; set; }

        public static CancelReasonDropdownDTO CreateFromModel(CancelReason model)
        {
            if (model != null)
            {
                var result = new CancelReasonDropdownDTO()
                {
                    Id = model.ID,
                    Description = model.Description,
                    Key = model.Key,
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
