﻿using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitDropdownDTO
    {
        /// <summary>
        /// Identity UnitID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// ตึก
        /// Project/api/Projects/{projectID}/Towers/DropdownList
        /// </summary>
        public TowerDropdownDTO Tower { get; set; }
        /// <summary>
        /// ชั้น
        /// Project/api/Projects/{projectID}/Towers/{towerID}/Floors/DropdownList
        /// </summary>
        public FloorDropdownDTO Floor { get; set; }
        /// <summary>
        /// พื้นที่ใช้สอย
        /// </summary>
        public double? UsedArea { get; set; }
        /// <summary>
        /// พื้นที่ขาย
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        public double? TitledeedArea { get; set; }
        /// <summary>
        /// สถานะแปลง
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }


        public static UnitDropdownDTO CreateFromModel(Unit model)
        {
            if (model != null)
            {
                var result = new UnitDropdownDTO
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo,
                    HouseNo = model.HouseNo,
                    Tower = TowerDropdownDTO.CreateFromModel(model.Tower),
                    Floor = FloorDropdownDTO.CreateFromModel(model.Floor),
                    UsedArea = model.UsedArea,
                    SaleArea = model.SaleArea
                };

                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<UnitDropdownDTO> CreateFromModelAsync(Unit model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitDropdownDTO
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo,
                    HouseNo = model.HouseNo,
                    Tower = TowerDropdownDTO.CreateFromModel(model.Tower),
                    Floor = FloorDropdownDTO.CreateFromModel(model.Floor),
                    UsedArea = model.UsedArea,
                    SaleArea = model.SaleArea
                };

                result.TitledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == model.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
