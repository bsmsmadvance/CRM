using System;
using Database.Models.PRJ;

namespace Base.DTOs.PRJ
{
    public class ProjectDropdownDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโครงการ (TH)
        /// </summary>
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// ชื่อโครงการ (EN)
        /// </summary>
        public string ProjectNameEN { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProjectStatus { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProductType
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProductType { get; set; }
        /// <summary>
        /// BG
        /// </summary>
        public MST.BGDropdownDTO BG { get; set; }

        public static ProjectDropdownDTO CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectDropdownDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    ProjectNameEN = model.ProjectNameEN,
                    ProjectNameTH = model.ProjectNameTH,
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BG = MST.BGDropdownDTO.CreateFromModel(model.BG)
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
