using Base.DTOs.PRM;
using Base.DTOs.CTM;
using Database.Models;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Database.Models.MasterKeys;

namespace Base.DTOs.SAL
{
    public class TransferPromotionDTO : BaseDTO
    {
        #region project other

        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        #endregion

        #region Contact
        public AgreementOwnerDTO AgreementOwner { get; set; }
        #endregion

        #region TransferPromotion
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }
        /// <summary>
        /// ผลรวมใช้ budget
        /// </summary>
        public decimal BudgetAmount { get; set; }
        /// <summary>
        /// ผลรวมโปรโมชั่น
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// กำหนดวันที่โอน
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public MasterTransferPromotionDTO MasterTransferPromotion { get; set; }
        /// <summary>
        /// ส่วนลด ณ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// ฟรีค่าจดจำนองกรณีกู้เกิน
        /// </summary>
        public bool IsFreeMortgageCharge { get; set; }
        /// <summary>
        /// Min Price Workflow
        /// </summary>
        public MinPriceBudgetWorkflowDTO MinPriceBudgetWorkflow { get; set; }
        #endregion

        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<TransferPromotionItemDTO> Items { get; set; }

        public async static Task<TransferPromotionDTO> CreateFromModelAsync(TransferPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new TransferPromotionDTO()
                {
                    Id = model.ID,
                    PromotionNo = model.MasterPromotion?.PromotionNo,
                    Remark = model.Remark,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Booking.Project),
                    Unit = UnitDTO.CreateFromModel(model.Booking.Unit),
                    Booking = await BookingDTO.CreateFromModelAsync(model.Booking, db),
                    BudgetAmount = model.BudgetAmount,
                    TotalAmount = model.TotalAmount,
                    TransferDateBefore = model.TransferDateBefore,
                    MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(model.MasterPromotion),
                    TransferDiscount = model.TransferDiscount.Value,
                    IsFreeMortgageCharge = model.IsFreeMortgageCharge
                };

                result.Items = new List<TransferPromotionItemDTO>();
                var itemModels = await db.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.QuotationTransferPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID && o.MainTransferPromotionItemID == null).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => TransferPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.TransferPromotionFreeItems
                    .Include(o => o.MasterTransferPromotionFreeItem)
                    .Include(o => o.QuotationTransferPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.TransferCreditCardItems
                    .Include(o => o.MasterTransferCreditCardItem)
                    .Include(o => o.QuotationTransferCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                var workflow = await db.MinPriceBudgetWorkflows
                   .Include(o => o.Project)
                   .Include(o => o.MinPriceBudgetWorkflowStage)
                   .Include(o => o.MinPriceWorkflowType)
                   .Include(o => o.BudgetPromotionType)
                   .Include(o => o.Booking)
                   .ThenInclude(o => o.Unit)
                   .Where(o => o.BookingID == model.BookingID && 
                            o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.PromotionTransfer)
                   .OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferPromotionDTO> CreateFromModelDrafAsync(Booking model, DatabaseContext db)
        {
            if (model != null)
            {
                var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
                var promotion = await db.MasterTransferPromotions.Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == model.ProjectID).FirstOrDefaultAsync();

                var result = new TransferPromotionDTO()
                {
                    //Id = model.ID,
                    PromotionNo = promotion.PromotionNo,
                    Remark = "",
                    //Updated = model.Updated,
                    //UpdatedBy = model.UpdatedBy?.DisplayName,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Project),
                    Unit = UnitDTO.CreateFromModel(model.Unit),
                    Booking = await BookingDTO.CreateFromModelAsync(model, db),
                    BudgetAmount = 0,
                    TotalAmount = 0,
                    //TransferDateBefore = null,
                    MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(promotion),
                    TransferDiscount = 0,
                    IsFreeMortgageCharge = false
                };


                result.Items = new List<TransferPromotionItemDTO>();

                var itemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == promotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == promotion.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterTransferCreditCardItems.Where(o => o.MasterTransferPromotionID == promotion.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferPromotion model)
        {
            model = model ?? new TransferPromotion();

            model.BookingID = this.Booking.Id.Value;
            model.BudgetAmount = this.BudgetAmount;
            model.TotalAmount = this.TotalAmount;
            model.TransferDateBefore = this.TransferDateBefore;
            model.MasterTransferPromotionID = this.MasterTransferPromotion.Id;
            model.TransferDiscount = this.TransferDiscount;
            model.TransferPromotionNo = this.PromotionNo;
            model.IsFreeMortgageCharge = this.IsFreeMortgageCharge;
            model.Remark = this.Remark;
        }
    }
}