using Database.Models;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class BudgetPromotionDTO
    {
        /// <summary>
        ///  เลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units
        /// </summary>
        public UnitDTO Unit { get; set; }
        /// <summary>
        ///  โปรขาย
        /// </summary>
        public decimal? PromotionPrice { get; set; }
        /// <summary>
        ///  โปรโอน
        /// </summary>
        public decimal? PromotionTransferPrice { get; set; }
        /// <summary>
        ///  รวม
        /// </summary>
        public decimal? TotalPrice { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// แก้ไขเมื่อ
        /// </summary>
        public DateTime? Updated { get; set; }
        /// <summary>
        /// สถานะการ Sync
        /// </summary>
        public BudgetPromotionSyncItemDTO SyncJob { get; set; }

        public static BudgetPromotionDTO CreateFromModel(BudgetPromotion model)
        {
            if (model != null)
            {
                var result = new BudgetPromotionDTO()
                {
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    //PromotionPrice = model.PromotionPrice,
                    //PromotionTransferPrice = model.PromotionTransferPrice,
                    //TotalPrice = (model.PromotionPrice ?? 0) + (model.PromotionTransferPrice ?? 0),
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

        public async static Task<BudgetPromotionDTO> CreateFromQueryResultAsync(BudgetPromotionQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new BudgetPromotionDTO()
                {
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    PromotionPrice = model.BudgetPromotionSale?.Budget ?? 0,
                    PromotionTransferPrice = model.BudgetPromotionTransfer?.Budget ?? 0,
                    TotalPrice = (model.BudgetPromotionSale?.Budget ?? 0) + (model.BudgetPromotionTransfer?.Budget ?? 0),

                    Updated = model.BudgetPromotionSale.Updated,
                    UpdatedBy = model.BudgetPromotionSale.UpdatedBy?.DisplayName
                };
                BudgetPromotionSyncItem budgetPromotionSyncItem = null;
                if (model.BudgetPromotionTransfer != null && model.BudgetPromotionSale == null)
                {
                    budgetPromotionSyncItem = await db.BudgetPromotionSyncItems.Where(p => p.TransferBudgetPromotionID == model.BudgetPromotionTransfer.ID)
                                                                                .Include(p => p.UpdatedBy)
                                                                                .Include(p => p.BudgetPromotionSyncStatus)
                                                                                .OrderBy(p => p.Created)
                                                                                .FirstOrDefaultAsync();
                }
                else if (model.BudgetPromotionSale != null && model.BudgetPromotionTransfer == null)
                {
                    budgetPromotionSyncItem = await db.BudgetPromotionSyncItems.Where(p => p.SaleBudgetPromotionID == model.BudgetPromotionSale.ID)
                                                                              .Include(p => p.UpdatedBy)
                                                                              .Include(p => p.BudgetPromotionSyncStatus)
                                                                              .OrderBy(p => p.Created)
                                                                              .FirstOrDefaultAsync();
                }
                else if (model.BudgetPromotionSale != null && model.BudgetPromotionTransfer != null)
                {
                    budgetPromotionSyncItem = await db.BudgetPromotionSyncItems.Where(p => p.SaleBudgetPromotionID == model.BudgetPromotionSale.ID && p.TransferBudgetPromotionID == model.BudgetPromotionTransfer.ID)
                                                                              .Include(p => p.UpdatedBy)
                                                                              .Include(p => p.BudgetPromotionSyncStatus)
                                                                              .OrderBy(p => p.Created)
                                                                              .FirstOrDefaultAsync();
                }

                result.SyncJob = await BudgetPromotionSyncItemDTO.CreateFromModelAsync(budgetPromotionSyncItem, db);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BudgetPromotionSortByParam sortByParam, ref List<BudgetPromotionDTO> llistDtos)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BudgetPromotionSortBy.Unit_UnitNo:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.UnitNo).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.UnitNo).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_HouseNo:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.HouseNo).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.HouseNo).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsObject:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.SapwbsObject).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.SapwbsObject).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsObject_P:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.SapwbsObject_P).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.SapwbsObject_P).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsNo:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.SapwbsNo).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.SapwbsNo).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_SapwbsNo_P:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.SapwbsNo_P).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.SapwbsNo_P).ToList();
                        break;
                    case BudgetPromotionSortBy.Unit_SaleArea:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Unit.SaleArea).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Unit.SaleArea).ToList();
                        break;
                    case BudgetPromotionSortBy.PromotionPrice:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.PromotionPrice).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.PromotionPrice).ToList();
                        break;
                    case BudgetPromotionSortBy.PromotionTransferPrice:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.PromotionTransferPrice).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.PromotionTransferPrice).ToList();
                        break;
                    case BudgetPromotionSortBy.SyncJob_Status:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.SyncJob?.Status?.Name).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.SyncJob?.Status?.Name).ToList();
                        break;
                    case BudgetPromotionSortBy.TotalPrice:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.TotalPrice).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.TotalPrice).ToList();
                        break;
                    case BudgetPromotionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.UpdatedBy).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    case BudgetPromotionSortBy.Updated:
                        if (sortByParam.Ascending) llistDtos = llistDtos.OrderBy(o => o.Updated).ToList();
                        else llistDtos = llistDtos.OrderByDescending(o => o.Updated).ToList();
                        break;
                    default:
                        llistDtos = llistDtos.OrderBy(o => o.Unit.UnitNo).ToList();
                        break;
                }
            }
            else
            {
                llistDtos = llistDtos.OrderBy(o => o.Unit.UnitNo).ToList();
            }
        }

        public void ToModelSale(ref BudgetPromotion model)
        {
            model.UnitID = this.Unit.Id.Value;
            model.Budget = this.PromotionPrice;
            model.ActiveDate = DateTime.Now;
            //model.BudgetPromotionType = BudgetPromotionType.Sale;
        }
        public void ToModelTransfer(ref BudgetPromotion model)
        {
            model.UnitID = this.Unit.Id.Value;
            model.Budget = this.PromotionTransferPrice;
            model.ActiveDate = DateTime.Now;
            //model.BudgetPromotionType = BudgetPromotionType.Transfer;
        }
    }
    public class TempBudgetPromotionQueryResult
    {
        public Unit Unit { get; set; }
        public List<BudgetPromotion> BudgetPromotions { get; set; }
    }

    public class BudgetPromotionQueryResult
    {
        public Unit Unit { get; set; }
        public BudgetPromotion BudgetPromotionSale { get; set; }
        public BudgetPromotion BudgetPromotionTransfer { get; set; }
    }
}
