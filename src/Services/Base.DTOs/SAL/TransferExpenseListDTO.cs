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
    public class TransferExpenseListDTO
    {
        /// <summary>
        /// ข้อมูลโอน
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }
        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// </summary>
        public string ExpenseOwner { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string ExpenseName { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย (บาท)
        /// </summary>
        public decimal BuyerAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย (บาท)
        /// </summary>
        public decimal SellerAmount { get; set; }

        public async static Task<List<TransferExpenseListDTO>> CreateFromModelAsync(Guid TransferID, DatabaseContext DB)
        {
            var result = new List<TransferExpenseListDTO>();
            var unitPriceModel = new UnitPrice();
            var unitPriceItemModel = new List<UnitPriceItem>();

            var transfer = await DB.Transfers.Where(o => o.ID == TransferID)
                                .Include(o => o.Agreement)
                                .FirstOrDefaultAsync() ?? new Database.Models.SAL.Transfer();

            var bookingID = transfer.Agreement.BookingID;

            unitPriceModel = await DB.UnitPrices
                           .Include(o => o.Booking)
                           .Include(o => o.UnitPriceStage)
                           .Where(o => o.BookingID == bookingID
                                   && o.UnitPriceStage.Key == UnitPriceStageKeys.Transfer
                                   && o.IsActive)
                           .FirstOrDefaultAsync() ?? new UnitPrice();

            if (unitPriceModel != null)
            {
                unitPriceItemModel = await DB.UnitPriceItems
                                            .Include(o => o.PriceUnit)
                                            .Where(o => o.UnitPriceID == unitPriceModel.ID)
                                            .ToListAsync() ?? new List<UnitPriceItem>();
            }
            else
            {
                unitPriceModel = await DB.UnitPrices
                         .Include(o => o.Booking)
                         .Include(o => o.UnitPriceStage)
                         .Where(o => o.BookingID == bookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                && o.IsActive
                            )
                         .FirstOrDefaultAsync() ?? new UnitPrice();

                unitPriceItemModel = await DB.UnitPriceItems
                                            .Include(o => o.PriceUnit)
                                            .Where(o => o.UnitPriceID == unitPriceModel.ID)
                                            .ToListAsync() ?? new List<UnitPriceItem>();
            }

            var listTransferExpense = await DB.BookingPromotionExpenses
                .Include(o => o.BookingPromotion)
                .Include(o => o.MasterPriceItem)
                .Where(o => o.BookingPromotion.BookingID == bookingID && o.BookingPromotion.IsActive)
                .ToListAsync() ?? new List<Database.Models.PRM.BookingPromotionExpense>();

            if (listTransferExpense != null && listTransferExpense.Count > 0)
            {
                foreach (var item in listTransferExpense)
                {
                    var expenseReponsibleBy = await DB.MasterCenters.Where(o => o.ID == item.ExpenseReponsibleByMasterCenterID).FirstOrDefaultAsync();
                    var expense = unitPriceItemModel.Where(o => o.MasterPriceItemID == item.MasterPriceItemID).FirstOrDefault();

                    var exps = new TransferExpenseListDTO();

                    exps.Transfer = TransferDropdownDTO.CreateFromModel(transfer);
                    exps.ExpenseOwner = expenseReponsibleBy.Name;
                    exps.ExpenseName = item.MasterPriceItem.Detail;
                    exps.Amount = (int)expense.PriceUnitAmount.Value;
                    exps.UnitName = expense.PriceUnit.Name;
                    exps.PricePerUnit = expense.PricePerUnitAmount.Value;
                    exps.TotalPrice = item.Amount;
                    exps.BuyerAmount = item.BuyerAmount;
                    exps.SellerAmount = item.SellerAmount;

                    result.Add(exps);
                }


                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<List<TransferExpenseListDTO>> CreateFromDrafModelAsync(Guid AgreementID, DatabaseContext DB)
        {
            var result = new List<TransferExpenseListDTO>();
            var unitPriceModel = new UnitPrice();
            var unitPriceItemModel = new List<UnitPriceItem>();

            var agreement = await DB.Agreements.Where(o => o.ID == AgreementID)
                                .FirstOrDefaultAsync() ?? new Database.Models.SAL.Agreement();

            var bookingID = agreement.BookingID;

            unitPriceModel = await DB.UnitPrices
                       .Include(o => o.Booking)
                       .Include(o => o.UnitPriceStage)
                       .Where(o => o.BookingID == bookingID
                               && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                               && o.IsActive)
                       .FirstOrDefaultAsync() ?? new UnitPrice();

            if (unitPriceModel != null)
            {
                unitPriceItemModel = await DB.UnitPriceItems
                                            .Include(o => o.PriceUnit)
                                            .Where(o => o.UnitPriceID == unitPriceModel.ID)
                                            .ToListAsync() ?? new List<UnitPriceItem>();
            }
            else
            {
                unitPriceModel = await DB.UnitPrices
                     .Include(o => o.Booking)
                     .Include(o => o.UnitPriceStage)
                     .Where(o => o.BookingID == bookingID
                            && o.UnitPriceStage.Key == UnitPriceStageKeys.Booking
                            && o.IsActive
                        )
                     .FirstOrDefaultAsync() ?? new UnitPrice();

                unitPriceItemModel = await DB.UnitPriceItems
                                            .Include(o => o.PriceUnit)
                                            .Where(o => o.UnitPriceID == unitPriceModel.ID)
                                            .ToListAsync() ?? new List<UnitPriceItem>();
            }

            var listTransferExpense = await DB.BookingPromotionExpenses
                .Include(o => o.BookingPromotion)
                .Include(o => o.MasterPriceItem)
                .Where(o => o.BookingPromotion.BookingID == bookingID 
                                && o.BookingPromotion.IsActive)
                .ToListAsync() ?? new List<Database.Models.PRM.BookingPromotionExpense>();

            if (listTransferExpense != null && listTransferExpense.Count > 0)
            {
                foreach (var item in listTransferExpense)
                {
                    var expenseReponsibleBy = await DB.MasterCenters.Where(o => o.ID == item.ExpenseReponsibleByMasterCenterID).FirstOrDefaultAsync();
                    var expense = unitPriceItemModel.Where(o => o.MasterPriceItemID == item.MasterPriceItemID).FirstOrDefault();

                    var exps = new TransferExpenseListDTO();

                    exps.Transfer = new TransferDropdownDTO();
                    exps.ExpenseOwner = expenseReponsibleBy.Name;
                    exps.ExpenseName = item.MasterPriceItem.Detail;
                    exps.Amount = (int)expense.PriceUnitAmount.Value;
                    exps.UnitName = expense.PriceUnit.Name;
                    exps.PricePerUnit = expense.PricePerUnitAmount.Value;
                    exps.TotalPrice = item.Amount;
                    exps.BuyerAmount = item.BuyerAmount;
                    exps.SellerAmount = item.SellerAmount;

                    result.Add(exps);
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
