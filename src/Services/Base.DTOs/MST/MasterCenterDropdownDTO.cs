using Database.Models.MST;
using System;
namespace Base.DTOs.MST
{
    public class MasterCenterDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// รหัส ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Key { get; set; }

        public static MasterCenterDropdownDTO CreateFromModel(MasterCenter model)
        {
            if (model != null)
            {
                var result = new MasterCenterDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
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
