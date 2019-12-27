using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายละเอียดแปลงเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequestUnit
    /// </summary>
    public class PreSalePromotionRequestUnitDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// แปลง
        /// GET masterdata/Projects/{projectID}/Units/DropdownListSellPrice
        /// </summary>
        public PRJ.UnitDropdownSellPriceDTO Unit { get; set; }
        /// <summary>
        /// สถานะการสร้าง/ยกเลิก PR
        /// </summary>
        public MST.MasterCenterDropdownDTO SAPPRStatus { get; set; }
        /// <summary>
        /// ชนิดของการสร้าง/ยกเลิก PR
        /// </summary>
        public MST.MasterCenterDropdownDTO PRJobType { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// รายการสิ่งของ
        /// </summary>
        public List<PreSalePromotionRequestItemDTO> RequestItems { get; set; }

        public async static Task<PreSalePromotionRequestUnitDTO> CreateFromModelAsync(PreSalePromotionRequestUnit model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestUnitDTO
                {
                    Project = ProjectDropdownDTO.CreateFromModel(model.Unit?.Project),
                    Unit = await UnitDropdownSellPriceDTO.CreateFromModelAsync(model.Unit, DB),
                    SAPPRStatus = MasterCenterDropdownDTO.CreateFromModel(model.SAPPRStatus),
                    PRJobType = MasterCenterDropdownDTO.CreateFromModel(model.PromotionRequestPRJobType),
                    Remark = model.Remark,
                    PromotionNo = model.PreSalePromotionRequest?.MasterPreSalePromotion?.PromotionNo
                };

                result.RequestItems = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == model.ID)
                                                                           .Select(p => PreSalePromotionRequestItemDTO.CreateFromModelAsync(p, DB).Result)
                                                                           .ToListAsync();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
