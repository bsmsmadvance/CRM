using Base.DTOs.FIN;
using Base.DTOs.PRJ;
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
    public class TransferPriceListDTO
    {
        /// <summary>
        /// ราคาบ้านในสัญญา
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลด ณ​ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// ค่าเนื้อที่เพิ่ม-ลด
        /// </summary>
        public decimal IncreasingAreaPrice { get; set; }
        /// <summary>
        /// ราคาบ้านสุทธิ
        /// </summary>
        public decimal NetSellingPrice { get; set; }
        /// <summary>
        /// รวมเงินที่ชำระมาแล้ว
        /// </summary>
        public decimal TotalPaidAmount { get; set; }
        /// <summary>
        /// มี FreeDown หรือไม่
        /// </summary>
        public bool IsFreeDown { get; set; }
        /// <summary>
        /// ส่วนลด FreeDown
        /// </summary>
        public decimal FreeDownDiscount { get; set; }
        /// <summary>
        /// ค่าส่วนกลาง
        /// </summary>
        public decimal CommonFeeCharge { get; set; }
        /// <summary>
        /// ค่ามิเตอร์
        /// </summary>
        public decimal MeterAmount { get; set; }
        /// <summary>
        /// ค่่าจ่ายทีดิน
        /// </summary>
        public decimal LandAmount { get; set; }
        /// <summary>
        /// ค่าส่วนกลาง(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerCommonFeeCharge { get; set; }
        /// <summary>
        /// ค่ามิเตอร์(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerMeterAmount { get; set; }
        /// <summary>
        /// ค่่าจ่ายทีดิน(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerLandAmount { get; set; }
        /// <summary>
        /// รวมเงินที่เก็บจากลูกค้า
        /// </summary>
        public decimal TotalCustomerPayAmount { get; set; }
        /// <summary>
        /// ค่าใช้จ่ายที่เก็บจากลูกค้า
        /// </summary>
        public decimal CustomerPayAmount { get; set; }
        /// <summary>
        /// ค่าใช้จ่ายที่ไม่เก็บจากลูกค้า
        /// </summary>
        public decimal CustomerNoPayAmount { get; set; }
        /// <summary>
        /// ยอดจดจำนองจาก
        /// </summary>
        public decimal FromMortgageFee { get; set; }
        /// <summary>
        /// ยอดจดจำนองเป็น
        /// </summary>
        public decimal ToMortgageFee { get; set; }
        /// <summary>
        /// ยอดจดจำนอง
        /// </summary>
        public decimal MortgageFee { get; set; }


        public async static Task<TransferPriceListDTO> CreateFromModelAsync(Guid bookingID, DatabaseContext db)
        {
            var agreement = await db.Agreements
                        .Include(o => o.Booking)
                        .Include(o => o.Unit)
                        .Where(o => o.BookingID == bookingID).FirstOrDefaultAsync();

            var mortgageValue = await db.CreditBankings
                            .Include(o => o.LoanStatus)
                            .Where(o => o.BookingID == bookingID && o.IsUseBank.Value && o.LoanStatus.Key == "1").Select(o => o.ApprovedAmount).FirstOrDefaultAsync();

            var transferFeeRate = await db.BOConfigurations.Select(o => o.TransferFeeRate).FirstOrDefaultAsync();

            var unitPriceModel = await db.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                var result = new TransferPriceListDTO();
                result.TransferDiscount = 0;

                var unitPriceModelTransfer = await db.UnitPrices
                   .Include(o => o.Booking)
                   .ThenInclude(o => o.ReferContact)
                   .Include(o => o.UnitPriceStage)
                   .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Transfer).FirstOrDefaultAsync();

                if (unitPriceModelTransfer != null)
                {
                    var unitPriceItemModelTransfer = await db.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModelTransfer.ID).ToListAsync();
                    var transferDiscount = unitPriceItemModelTransfer.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Amount).FirstOrDefault();

                    if (transferDiscount > 0)
                    {
                        result.TransferDiscount += transferDiscount;
                    }
                }

                var promotion = await db.BookingPromotions
                                    .Include(o => o.BookingPromotionStage)
                                    .Where(o => o.BookingID == bookingID && o.BookingPromotionStage.Key == BookingPromotionStageKeys.Agreement && o.IsActive == true && o.IsDeleted == false).FirstOrDefaultAsync();

                if (promotion != null)
                {
                    var expense = await db.BookingPromotionExpenses.Where(o => o.BookingPromotionID == promotion.ID).ToListAsync();

                    result.CommonFeeCharge = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CommonFee).Select(o => o.BuyerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FirstSinkingFund).Select(o => o.BuyerAmount).FirstOrDefault();

                    result.MeterAmount = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.EletrictMeter).Select(o => o.BuyerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.WaterMeter).Select(o => o.BuyerAmount).FirstOrDefault();

                    result.LandAmount = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferFee).Select(o => o.BuyerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.MortgageFee).Select(o => o.BuyerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DocumentFee).Select(o => o.BuyerAmount).FirstOrDefault();

                    //
                    result.SellerCommonFeeCharge = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CommonFee).Select(o => o.SellerAmount).FirstOrDefault()
                                           + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FirstSinkingFund).Select(o => o.SellerAmount).FirstOrDefault();

                    result.SellerMeterAmount = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.EletrictMeter).Select(o => o.SellerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.WaterMeter).Select(o => o.SellerAmount).FirstOrDefault();

                    result.SellerLandAmount = expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferFee).Select(o => o.SellerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.MortgageFee).Select(o => o.SellerAmount).FirstOrDefault()
                                            + expense.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DocumentFee).Select(o => o.SellerAmount).FirstOrDefault();
                }

                var cashDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).Select(o => o.Amount).FirstOrDefault();
                result.FreeDownDiscount = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FreeDownDiscount).Select(o => o.Amount).FirstOrDefault();
                result.TransferDiscount += unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Amount).FirstOrDefault();
                result.SellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
                result.NetSellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();

                var unit = UnitDTO.CreateFromModel(agreement.Unit, null, null);
                result.IncreasingAreaPrice = decimal.Parse(unit.AddOnArea.ToString() ?? "0") * agreement.AreaPricePerUnit;

                result.NetSellingPrice = result.SellingPrice - cashDiscount - result.TransferDiscount - result.IncreasingAreaPrice;

                var listMasterPriceItemKeys = new List<Guid>();
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.DownAmount);

                var results = new List<PaymentUnitPriceItemDTO>();

                var payments = await db.Payments.Where(o => o.BookingID == bookingID).ToListAsync();
                var paymentIDs = await db.Payments.Where(o => o.BookingID == bookingID).OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();
                var sumPayAmount = await db.PaymentItems.Where(o => paymentIDs.Contains(o.PaymentID) && listMasterPriceItemKeys.Contains((Guid)o.MasterPriceItemID)).SumAsync(o => o.PayAmount);

                result.TotalPaidAmount = sumPayAmount;
                result.TotalCustomerPayAmount = result.NetSellingPrice - sumPayAmount + result.CommonFeeCharge + result.MeterAmount + result.LandAmount;
                result.CustomerPayAmount = result.CommonFeeCharge + result.MeterAmount + result.LandAmount;
                result.CustomerNoPayAmount = result.SellerCommonFeeCharge + result.SellerMeterAmount + result.SellerLandAmount;

                if (result.SellingPrice > mortgageValue)
                {
                    result.FromMortgageFee = result.SellingPrice;
                    result.ToMortgageFee = mortgageValue;
                }
                else
                {
                    result.FromMortgageFee = mortgageValue;
                    result.ToMortgageFee = mortgageValue;
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
