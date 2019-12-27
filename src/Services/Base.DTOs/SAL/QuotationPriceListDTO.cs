using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    public class QuotationPriceListDTO
    {
        /// <summary>
        /// ID ของ Price List
        /// </summary>
        public Guid? FromPriceListID { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// โอนกรรมสิทธิ์ภายในวันที่
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// ผู้แนะนำ
        /// </summary>
        public CTM.ContactListDTO ReferContact { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด
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
        /// ส่วนลด FGF (ในใบเสนอราคาจะไม่มี แต่จะมีค่าตอนที่ตั้งเรื่องย้ายแปลง)
        /// </summary>
        public decimal? FGFDiscount { get; set; }
        /// <summary>
        /// ราคาขายสุทธิ
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
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }
        /// <summary>
        /// รวมมูลล่าโปรโมชั่น
        /// </summary>
        public decimal? TotalPromotionAmount { get; set; }
        /// <summary>
        /// รวมใช้ budgetPromotion
        /// </summary>
        public decimal? TotalBudgetPromotionAmount { get; set; }

        public async static Task<QuotationPriceListDTO> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var priceList = await db.PriceLists.GetActivePriceListAsync(unitID);
            var result = new QuotationPriceListDTO();
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
            if(specialDownInstallmentAmountStrings != null)
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
            result.ContractDate = DateTime.Today.AddDays(7);
            result.TransferOwnershipDate = DateTime.Today;
            result.FromPriceListID = priceList.ID;

            return result;

        }

        public async static Task<QuotationPriceListDTO> CreateFromModelAsync(Guid quotationID, DatabaseContext db)
        {
            var unitPriceModel = await db.QuotationUnitPrices.Include(o => o.Quotation).Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();
            if(unitPriceModel != null)
            {
                var unitPriceItemModel = await db.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == unitPriceModel.ID).ToListAsync();
                var result = new QuotationPriceListDTO();

                result.SellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                result.BookingAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
                result.ContractAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
                result.DownAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
                result.TransferDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Amount).FirstOrDefault();
                result.CashDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).Select(o => o.Amount).FirstOrDefault();
                result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
                result.Installment = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault() ?? 0;
                result.InstallmentAmount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault() ?? 0;
                

                var specialDownInstallmentStrings = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault();
                var specialDownInstallmentAmountStrings = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();
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
                result.ContractDate = unitPriceModel.Quotation.ContractDate;
                result.TransferOwnershipDate = unitPriceModel.Quotation.TransferOwnershipDate;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
