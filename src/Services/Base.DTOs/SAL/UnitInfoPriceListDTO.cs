using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoPriceListDTO
    {
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
        public decimal? TotalBudgetPromotion { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }

        /// <summary>
        /// ยอดรวม FET
        /// </summary>
        public decimal? SumFETPayment { get; set; }

        /// <summary>
        /// Direct DD/DC Status
        /// </summary>
        public string DirectDDDCType { get; set; }

        /// <summary>
        /// Direct DD/DC Status
        /// </summary>
        public string DirectDDDCStatus { get; set; }

        /// <summary>
        /// วันที่เริ่มตัด Direct DD/DC
        /// </summary>
        public DateTime? DirectDDDCStratDate { get; set; }

        /// <summary>
        /// ยอดเงินรอ Import DD/DC
        /// </summary>
        public decimal? SumWaitingConfirmDirectDDDCAmount { get; set; }

        public async static Task<UnitInfoPriceListDTO> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var priceList = await db.PriceLists.GetActivePriceListAsync(unitID);
            if (priceList == null)
            {
                return null;
            }

            var result = new UnitInfoPriceListDTO();
            result.SellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            result.NetSellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            result.BookingAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            result.ContractAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            result.DownAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
            result.Installment = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault() ?? 0;
            result.InstallmentAmount = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault() ?? 0;

            var specialDownInstallmentStrings = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault();
            var specialDownInstallmentAmountStrings = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();
            var specialDownInstallments = specialDownInstallmentStrings?.Split(',');
            var specialDownInstallmentAmounts = specialDownInstallmentAmountStrings?.Split(',');

            result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();

            if (specialDownInstallmentAmountStrings != null)
            {
                for (int i = 0; i < specialDownInstallments.Length; i++)
                {
                    int periodNo;
                    if (int.TryParse(specialDownInstallments[i], out periodNo))
                    {
                        decimal amount = 0;
                        if (i < specialDownInstallmentAmounts.Length)
                        {
                            decimal.TryParse(specialDownInstallmentAmounts[i], out amount);
                        }

                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = periodNo,
                            Amount = amount
                        });
                    }
                }
            }

            result.SpecialInstallment = result.SpecialInstallmentPeriods.Count;
            result.NormalInstallment = result.Installment - result.SpecialInstallment;



            return result;

        }

        public async static Task<UnitInfoPriceListDTO> CreateFromModelAsync(UnitPrice unitPriceModel, DatabaseContext db)
        {
            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                var result = new UnitInfoPriceListDTO();

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

                result.TotalBudgetPromotion = result.CashDiscount + result.TransferDiscount + bugetPromotion.BudgetPromotionSale?.Budget;

                result.SumFETPayment = await db.FETs.Where(e => e.BookingID == unitPriceModel.BookingID).Select(e => e.Amount).SumAsync();

                var DirectDDDCForm = db.DirectCreditDebitApprovalForms
                    .Include(e => e.DirectApprovalFormStatus)
                    .Include(e => e.DirectApprovalFormType)
                .Where(e => e.BookingID == unitPriceModel.BookingID).OrderByDescending(e => e.Created).Select(e => new { e.StartDate, Status = e.DirectApprovalFormStatus.Key, Type = e.DirectApprovalFormType.Key }).FirstOrDefault();

                var DirectDDDCDetail = db.DirectCreditDebitExportDetails
                    .Include(e=>e.DirectCreditDebitExportDetailStatus)
                    .Include(e => e.UnitPriceInstallment)
                        .ThenInclude(e => e.InstallmentOfUnitPriceItem)
                            .ThenInclude(e => e.UnitPrice)
                                .ThenInclude(e => e.Booking)
                    .Where(e => e.UnitPriceInstallment.InstallmentOfUnitPriceItem.UnitPrice.Booking.ID == unitPriceModel.BookingID && e.DirectCreditDebitExportDetailStatus.Key == DirectCreditDebitExportDetailStatusKeys.Fail);

                result.DirectDDDCType = DirectDDDCForm?.Type;
                result.DirectDDDCStatus = DirectDDDCForm?.Status;
                result.DirectDDDCStratDate = DirectDDDCForm?.StartDate;

                result.SumWaitingConfirmDirectDDDCAmount = await DirectDDDCDetail.Select(e => e.Amount).SumAsync();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
