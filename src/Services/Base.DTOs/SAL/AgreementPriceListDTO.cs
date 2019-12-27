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
    public class AgreementPriceListDTO
    {
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDTO Agreement { get; set; }
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
        /// <summary>
        /// วันที่เริ่มต้น
        /// </summary>
        public DateTime? InstallmentStartDate { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด
        /// </summary>
        public DateTime? InstallmentEndDate { get; set; }
        /// <summary>
        /// วันที่กำหนดชำระเวินดาวน์
        /// </summary>
        public int DownDueDate { get; set; }
        /// <summary>
        /// รับเงินก่อนทำสัญญา
        /// </summary>
        public decimal? PreContractAmount { get; set; }
        /// <summary>
        /// % เงินดาวน์
        /// </summary>
        public double? PercentDown { get; set; }
        /// <summary>
        /// วันที่งวดสุดท้าย
        /// </summary>
        public DateTime? SellerPayDueDate { get; set; }
        /// <summary>
        /// เงินงวดสุดท้าย
        /// </summary>
        public decimal? SellerPayAmount { get; set; }
        /// <summary>
        /// AP จ่ายงวดสุดท้าย
        /// </summary>
        public bool? IsSellerPay { get; set; }

        public async static Task<AgreementPriceListDTO> CreateFromModelAsync(Guid AgreementID, DatabaseContext db)
        {
            var agree = await db.Agreements
               .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();     

            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == agree.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                var result = new AgreementPriceListDTO();

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

                var downAmountItem = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).FirstOrDefault();
                var installments = await db.UnitPriceInstallments.Where(o => o.InstallmentOfUnitPriceItemID == downAmountItem.ID).ToListAsync();
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

                    result.SpecialInstallment = installments.Where(o => o.IsSpecialInstallment == true).Count();
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(agree, null, db);

                result.InstallmentStartDate = agree.ContractDate.HasValue ? agree.ContractDate.Value.AddMonths(1) : (DateTime?) null;
                result.InstallmentEndDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments.Count) : (DateTime?)null;
                result.DownDueDate = downAmountItem.DueDate.HasValue ? downAmountItem.DueDate.Value.Day : 0;
                result.PercentDown = downAmountItem.PriceUnitAmount;
                result.SellerPayDueDate = installments.Where(o => o.IsSellerPay == true).Select(o => o.DueDate).FirstOrDefault();
                result.SellerPayAmount= installments.Where(o => o.IsSellerPay == true).Select(o => o.Amount).FirstOrDefault();
                result.IsSellerPay = installments.Where(o => o.IsSellerPay == true).Select(o => o.IsSellerPay).FirstOrDefault();

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

                var promotion = await db.BookingPromotions.Where(o => o.BookingID == agree.BookingID).FirstAsync();
                var totalPreSalePrice = await db.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == agree.BookingID).SumAsync(o => o.TotalPrice);
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

        public async static Task<AgreementPriceListDTO> CreateFromModelPriceListAsync(Guid AgreementID, DatabaseContext db)
        {
            var agree = await db.Agreements
               .Where(o => o.ID == AgreementID).FirstOrDefaultAsync();

            var priceList = await db.PriceLists
                .Where(o => o.UnitID == agree.UnitID).OrderByDescending(o=>o.ActiveDate)
                .FirstOrDefaultAsync();

            if (priceList != null)
            {
                var priceListItemModel = await db.PriceListItems.Where(o => o.PriceListID == priceList.ID).ToListAsync();
                var result = new AgreementPriceListDTO();

               // result.CashDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).Select(o => o.Amount).FirstOrDefault();
                //result.TransferDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Amount).FirstOrDefault();
                result.SellingPrice = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                //result.BookingAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
                //result.ContractAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
                //result.FreeDownDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FreeDownDiscount).Select(o => o.Amount).FirstOrDefault();
                //result.FGFDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Amount).FirstOrDefault();
                result.DownAmount = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
                //result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
                //result.Installment = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault() ?? 0;
                //result.ReferContact = CTM.ContactListDTO.CreateFromModel(unitPriceModel.Booking.ReferContact, db);       

                var downAmountItem = priceListItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).FirstOrDefault();

                var installments = downAmountItem.Installment??0;
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

                if (installments > 0)
                {
                    var specialInstallment = downAmountItem.SpecialInstallments.Split(",");
                    var specialInstallmentAmounts = downAmountItem.SpecialInstallmentAmounts.Split(",");

                    result.NormalInstallment = installments - specialInstallment.Length;
                    result.InstallmentAmount = downAmountItem.PricePerUnitAmount ?? 0;
                    result.SpecialInstallment = specialInstallment.Length;

                    for (int i = 0; i < specialInstallment.Length; i++)
                    {
                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = int.Parse(specialInstallment[i]),
                            Amount = decimal.Parse(specialInstallmentAmounts[i])
                        });
                    }
                }

                result.Agreement = await AgreementDTO.CreateFromModelAsync(agree, null, db);

                result.InstallmentStartDate = agree.ContractDate.HasValue ? agree.ContractDate.Value.AddMonths(1) : (DateTime?) null;
                result.InstallmentEndDate = result.InstallmentStartDate.HasValue ? result.InstallmentStartDate.Value.AddMonths(installments) : (DateTime?) null;
                result.DownDueDate = 1; //defluat วันที่ 1     
                result.PercentDown = downAmountItem.PriceUnitAmount;
                

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
