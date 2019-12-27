using Database.Models;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class BookingPriceListDTO
    {
        /// <summary>
        /// ผู้แนะนำ
        /// </summary>
        public CTM.ContactListDTO ReferContact { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด (ส่วนลดหน้าสัญญา)
        /// </summary>
        public decimal CashDiscount { get; set; }
        /// <summary>
        /// ส่วนลด ณ​ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// มี FreeDown หรือไม่
        /// </summary>
        public bool IsFreeDown { get; set; }
        /// <summary>
        /// ส่วนลด FreeDown
        /// </summary>
        public decimal FreeDownDiscount { get; set; }
        /// <summary>
        /// ส่วนลด FGF
        /// </summary>
        public decimal FGFDiscount { get; set; }
        /// <summary>
        /// ราคาขายสุทธิ (ราคาขายหักส่วนลด)
        /// </summary>
        public decimal NetSellingPrice { get; set; }
        /// <summary>
        /// เงินจอง
        /// </summary>
        public decimal BookingAmount { get; set; }
        /// <summary>
        /// เงินสัญญา
        /// </summary>
        public decimal ContractAmount { get; set; }
        /// <summary>
        /// เงินดาวน์
        /// </summary>
        public decimal DownAmount { get; set; }
        /// <summary>
        /// เงินโอนกรรมสิทธิ์
        /// </summary>
        public decimal TransferAmount { get; set; }
        /// <summary>
        /// จำนวนผ่อนดาวน์รวม
        /// </summary>
        public int Installment { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์ปกติ
        /// </summary>
        public int NormalInstallment { get; set; }
        /// <summary>
        /// เงินงวดดาวน์ปกติ
        /// </summary>
        public decimal InstallmentAmount { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์พิเศษ
        /// </summary>
        public int SpecialInstallment { get; set; }
        /// <summary>
        /// รวมมูลค่าโปรโมชั่น
        /// </summary>
        public decimal? TotalPromotionAmount { get; set; }
        /// <summary>
        /// รวมใช้ Budget Promotion
        /// </summary>
        public decimal? TotalBudgetPromotionAmount { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }

        public async static Task<BookingPriceListDTO> CreateFromModelAsync(Guid bookingID, DatabaseContext db)
        {
            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking).FirstOrDefaultAsync();
            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                var result = new BookingPriceListDTO();

                result.CashDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).Select(o => o.Amount).FirstOrDefault();
                result.TransferDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Amount).FirstOrDefault();
                result.SellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                result.BookingAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
                result.ContractAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
                result.FreeDownDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FreeDownDiscount).Select(o => o.Amount).FirstOrDefault();
                result.FGFDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Amount).FirstOrDefault();
                result.DownAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
                result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
                result.Installment = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault() ?? 0;
                result.ReferContact = CTM.ContactListDTO.CreateFromModel(unitPriceModel.Booking.ReferContact, db);

                var downAmountItemId = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.ID).FirstOrDefault();
                var installments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == downAmountItemId).ToListAsync();
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (installments.Count > 0)
                {
                    var normalInstallment = installments.Where(o => o.IsSpecialInstallment == false).ToList();
                    result.NormalInstallment = normalInstallment.Count;
                    result.InstallmentAmount = normalInstallment.Sum(o => o.Amount);

                    foreach (var item in installments.Where(o => o.IsSpecialInstallment == true).ToList())
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = item.Period,
                            Amount = item.Amount
                        });
                    }
                }

                var query = await db.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == unitPriceModel.Booking.ProjectID && o.UnitID == unitPriceModel.Booking.UnitID)
                .Select(o => new
                {
                    BudgetPromotion = o,
                    Unit = o.Unit
                }).ToListAsync();

                var temp = query.GroupBy(o => o.Unit).Select(o => new PRJ.TempBudgetPromotionQueryResult
                {
                    Unit = o.Key,
                    BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
                }).ToList();
                var masterCenterBudgetPromotionTypeSaleID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
                var masterCenterBudgetPromotionTypeTransferID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
                var bugetPromotion = temp.Select(o => new PRJ.BudgetPromotionQueryResult
                {
                    Unit = o.Unit,
                    BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                    BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
                }).FirstOrDefault();

                result.TotalPromotionAmount = result.CashDiscount + result.TransferDiscount + bugetPromotion.BudgetPromotionSale?.Budget;

                var promotion = await db.BookingPromotions.Where(o => o.BookingID == bookingID).FirstAsync();
                var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == bookingID).SumAsync(o => o.TotalPrice);
                var totalPromotionPrice = await db.BookingPromotionItems.Where(o => o.BookingPromotionID == promotion.ID).SumAsync(o => o.TotalPrice);
                var totalExpenseByAP = await db.BookingPromotionExpenses
                    .Include(o => o.ExpenseReponsibleBy)
                    .Where(o => o.BookingPromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
                var totalExpenseByHalf = await db.BookingPromotionExpenses
                    .Include(o => o.ExpenseReponsibleBy)
                    .Where(o => o.BookingPromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);
                result.TotalBudgetPromotionAmount = result.FGFDiscount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
