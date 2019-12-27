using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// Model=PreSalePromotionRequest
    /// </summary>
    public class PreSalePromotionRequestListDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// รายการเลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public List<PRJ.UnitDropdownDTO> Units { get; set; }
        /// <summary>
        /// Master Promotion ก่อนขาย
        /// Promotion/api/MasterPreSalePromotions/DropdownList
        /// </summary>
        public MasterPreSalePromotionDropdownDTO MasterPreSalePromotion { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? PRCompletedDate { get; set; }
        /// <summary>
        /// สถานะ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PromotionRequestPRStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionRequestPRStatus { get; set; }
        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? RequestDate { get; set; }

        public async static Task<PreSalePromotionRequestListDTO> CreateFromModelAsync(PreSalePromotionRequest model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestListDTO
                {
                    Id = model.ID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Units = await DB.PreSalePromotionRequestUnits.Where(o => o.PreSalePromotionRequestID == model.ID).Select(o => UnitDropdownDTO.CreateFromModel(o.Unit)).ToListAsync(),
                    MasterPreSalePromotion = MasterPreSalePromotionDropdownDTO.CreateFromModel(model.MasterPreSalePromotion),
                    PRCompletedDate = model.PRCompletedDate,
                    PromotionRequestPRStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionRequestPRStatus),
                    RequestDate = model.RequestDate,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(PreSalePromotionRequestListSortByParam sortByParam, ref List<PreSalePromotionRequestListDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PreSalePromotionRequestListSortBy.Project:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Project?.ProjectNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Project?.ProjectNo).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.Unit:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Units?.Select(p => p.UnitNo).FirstOrDefault()).ToList().OrderBy(q=>q.Units.Select(r=>r.UnitNo).FirstOrDefault()).ToList();
                        else listDTOs = listDTOs.OrderBy(o => o.Units?.Select(p => p.UnitNo).FirstOrDefault()).ToList().OrderByDescending(q => q.Units.Select(r => r.UnitNo).FirstOrDefault()).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.MasterPreSalePromotions_PromotionNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.MasterPreSalePromotion.PromotionNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.MasterPreSalePromotion.PromotionNo).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.PRCompletedDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PRCompletedDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PRCompletedDate).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.PromotionRequestPRStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionRequestPRStatus?.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionRequestPRStatus?.Name).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.RequestDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.RequestDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.RequestDate).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case PreSalePromotionRequestListSortBy.UpdatedBy:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UpdatedBy).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                listDTOs = listDTOs.OrderBy(o => o.RequestDate).ToList();
            }
        }

    }
}

