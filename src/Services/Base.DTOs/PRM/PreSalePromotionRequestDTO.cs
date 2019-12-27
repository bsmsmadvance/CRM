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
    /// รายละเอียดใบเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequest
    /// </summary>
    public class PreSalePromotionRequestDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? RequestDate { get; set; }
        /// <summary>
        /// Master Promotion ก่อนขาย
        /// </summary>
        public MasterPreSalePromotionDTO MasterPreSalePromotion { get; set; }
        /// <summary>
        /// รายการแปลง/ห้อง
        /// </summary>
        public List<PreSalePromotionRequestUnitListDTO> RequestUnits { get; set; }
        public List<PreSalePromotionRequestItemDTO> RequestItems { get; set; }

        public async static Task<PreSalePromotionRequestDTO> CreateFromModelAsync(PreSalePromotionRequest model, DatabaseContext DB)
        {
            if (model != null)
            {
                var promotionStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).FirstOrDefaultAsync();
                var result = new PreSalePromotionRequestDTO
                {
                    Id = model.ID,
                    RequestNo = model.RequestNo,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    MasterPreSalePromotion =await MasterPreSalePromotionDTO.CreateFromModelAsync(model.MasterPreSalePromotion, DB),
                    RequestDate = model.RequestDate,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                var allUnit = await DB.PreSalePromotionRequestUnits
                    .Include(o => o.PromotionRequestPRJobType)
                    .Include(o => o.PreSalePromotionRequest)
                    .Include(o => o.SAPPRStatus)
                    .Include(o => o.Unit)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.PreSalePromotionRequestID == model.ID).ToListAsync();
                result.RequestUnits = allUnit.Select(o => PreSalePromotionRequestUnitListDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();
                var unitIDs = result.RequestUnits.Select(o => o.Id).ToList();

                var masterItemQuery = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == model.MasterPreSalePromotionID).ToListAsync();
                var masterItemResults = masterItemQuery.ToList();
                var itemResults = await DB.PreSalePromotionRequestItems.Where(o => unitIDs.Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
                result.MasterPreSalePromotion.TotalItemPrice = 0;
                result.RequestItems = new List<PreSalePromotionRequestItemDTO>();
                foreach (var item in masterItemResults)
                {
                    var requestItem = itemResults.Where(o => o.MasterPreSalePromotionItemID == item.ID).FirstOrDefault();
                    if (requestItem != null)
                    {
                        result.RequestItems.Add(await PreSalePromotionRequestItemDTO.CreateFromModelAsync(requestItem, DB));
                        result.MasterPreSalePromotion.TotalItemPrice += requestItem.TotalPrice;
                    }
                    else
                    {
                        result.RequestItems.Add(PreSalePromotionRequestItemDTO.CreateFromMasterModel(item));
                    }
                }

                

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
