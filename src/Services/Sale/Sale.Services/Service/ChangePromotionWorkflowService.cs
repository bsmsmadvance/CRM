using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Base.DTOs.SAL;
using Common;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using Sale.Params.Outputs;
using Sale.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services.Service
{
    public class ChangePromotionWorkflowService : IChangePromotionWorkflowService
    {
        private readonly DatabaseContext DB;

        public ChangePromotionWorkflowService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<ChangePromotionWorkflowDTO> GetChangeBookingPromotionAsync(Guid bookingID)
        {
            var promotion = await DB.BookingPromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == bookingID && o.ChangePromotionWorkflowID != null)
                .OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            if (promotion != null)
            {
                var model = await DB.ChangePromotionWorkflows
                    .Include(o => o.RequestByUser)
                    .Include(o => o.PromotionType)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.ID == promotion.ChangePromotionWorkflowID).FirstOrDefaultAsync();

                var result = await ChangePromotionWorkflowDTO.CreateFromModelAsync(model, DB);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<ChangePromotionWorkflowDTO> CreateChangePromotionWorkflow(Guid bookingID, ChangePromotionWorkflowDTO changePromotionWorkflow)
        {
            var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            var promotion = await DB.BookingPromotions.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();

            #region Workflow
            var workflow = new ChangePromotionWorkflow()
            {
                RequestByUserID = changePromotionWorkflow.RequestByUser?.Id,
                RequestDate = DateTime.Today,
                PromotionTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionType && o.Key == PromotionTypeKeys.Booking).Select(o => o.ID).FirstAsync(),
                IsApproved = null
            };
            #endregion

            #region Booking Promotion
            var bookingPromotionModel = new BookingPromotion()
            {
                BookingID = booking.ID,
                MasterBookingPromotionID = promotion.MasterBookingPromotionID,
                BookingPromotionNo = promotion.BookingPromotionNo,
                TransferDateBefore = changePromotionWorkflow.BookingPromotion.TransferDateBefore,
                TotalAmount = promotion.TotalAmount,
                TransferDiscount = promotion.TransferDiscount,
                ContractDiscount = promotion.ContractDiscount,
                FGFDiscount = promotion.FGFDiscount,
                BudgetAmount = promotion.BudgetAmount,
                PresentByUserID = promotion.PresentByUserID,
                BookingPromotionStageMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingPromotionStage && o.Key == BookingPromotionStageKeys.Booking).Select(o => o.ID).FirstAsync(),
                IsActive = false,
                ChangePromotionWorkflowID = workflow.ID
            };

            await DB.ChangePromotionWorkflows.AddAsync(workflow);
            await DB.BookingPromotions.AddAsync(bookingPromotionModel);
            await DB.SaveChangesAsync();

            #region Item
            var itemPromotion = changePromotionWorkflow.BookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item).ToList();
            foreach (var item in itemPromotion)
            {
                var promotionItemModel = new BookingPromotionItem()
                {
                    BookingPromotionID = bookingPromotionModel.ID,
                    Quantity = item.Quantity,
                    PricePerUnit = item.PricePerUnit,
                    TotalPrice = item.Quantity * item.PricePerUnit,
                    MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID,
                    QuotationBookingPromotionItemID = item.FromQuotationBookingPromotionItemID
                };
                await DB.BookingPromotionItems.AddAsync(promotionItemModel);

                List<BookingPromotionItem> subItems = new List<BookingPromotionItem>();
                foreach (var sub in item.SubItems)
                {
                    var promotionSubItemModel = new BookingPromotionItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        PricePerUnit = item.PricePerUnit,
                        TotalPrice = item.Quantity * item.PricePerUnit,
                        MainBookingPromotionItemID = promotionItemModel.ID,
                        MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingPromotionItemID = item.FromQuotationBookingPromotionItemID
                    };

                    subItems.Add(promotionSubItemModel);
                }

                if (subItems.Count >= 0)
                {
                    await DB.BookingPromotionItems.AddRangeAsync(subItems);
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Free
            var freeItemPromotion = changePromotionWorkflow.BookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem).ToList();
            foreach (var item in freeItemPromotion)
            {
                var promotionItemModel = new BookingPromotionFreeItem()
                {
                    BookingPromotionID = bookingPromotionModel.ID,
                    Quantity = item.Quantity,
                    MasterBookingPromotionFreeItemID = item.FromMasterBookingPromotionItemID,
                    QuotationBookingPromotionFreeItemID = item.FromQuotationBookingPromotionItemID
                };
                await DB.BookingPromotionFreeItems.AddAsync(promotionItemModel);
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Credit
            var creditItemPromotion = changePromotionWorkflow.BookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard).ToList();
            foreach (var item in creditItemPromotion)
            {
                var promotionItemModel = new BookingCreditCardItem()
                {
                    BookingPromotionID = bookingPromotionModel.ID,
                    MasterBookingCreditCardItemID = item.FromMasterBookingPromotionItemID,
                    QuotationBookingCreditCardItemID = item.FromQuotationBookingPromotionItemID
                };
                await DB.BookingCreditCardItems.AddAsync(promotionItemModel);
            }

            await DB.SaveChangesAsync();
            #endregion
            #endregion

            #region Expense
            foreach (var expense in changePromotionWorkflow.Expenses)
            {
                BookingPromotionExpense expenseModel = new BookingPromotionExpense()
                {
                    BookingPromotionID = bookingPromotionModel.ID,
                    ExpenseReponsibleByMasterCenterID = expense.ExpenseReponsibleBy.Id,
                    MasterPriceItemID = expense.MasterPriceItem.Id,
                    Amount = expense.Amount
                };

                switch (expense.ExpenseReponsibleBy.Key)
                {
                    case "0":
                        expenseModel.SellerAmount = expense.Amount;
                        break;
                    case "1":
                        expenseModel.BuyerAmount = expense.Amount;
                        break;
                    case "2":
                        expenseModel.SellerAmount = expense.Amount / 2;
                        expenseModel.BuyerAmount = expense.Amount / 2;
                        break;
                }

                await DB.BookingPromotionExpenses.AddAsync(expenseModel);
            }

            await DB.SaveChangesAsync();
            #endregion

            await this.CreateMinPriceBudgetWorkflowFromChangePromotionAsync(booking.ID, bookingPromotionModel.ID, workflow.ID, changePromotionWorkflow.MinPriceRequestReason?.Id, changePromotionWorkflow.OtherMinPriceRequestReason);

            var result = await this.GetChangeBookingPromotionAsync(bookingID);
            return result;
        }

        public async Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangePromotionAsync(Guid bookingID, UnitInfoBookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses)
        {
            var result = new MinPriceBudgetWorkflowTypeDTO();
            result.IsMinPriceWorkflow = false;
            result.IsBudgetPromotionWorkflow = false;

            var model = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();
            var unitPriceItem = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
            var discount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();

            #region Promotion
            var totalPreSalePrice = await DB.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == model.ID).SumAsync(o => o.TotalPrice);
            var totalPromotionPrice = bookingPromotion.Items.Sum(o => o.TotalPrice);
            var totalExpenseByAP = expenses.Where(o => o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).Sum(o => o.SellerPayAmount);
            var totalExpenseByHalf = expenses.Where(o => o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).Sum(o => o.SellerPayAmount);
            var totalBudgetPromotion = discount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf;

            var query = await DB.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                .Select(o => new
                {
                    BudgetPromotion = o,
                    Unit = o.Unit
                }).ToListAsync();

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();

            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var bugetPromotion = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).FirstOrDefault();

            var isWaitingPromotion = (totalBudgetPromotion > bugetPromotion.BudgetPromotionSale.Budget) ? true : false;
            if (isWaitingPromotion)
            {
                result.IsBudgetPromotionWorkflow = true;
            }
            #endregion

            #region Minimum Price
            var minPrice = await DB.MinPrices.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == model.UnitID).OrderByDescending(o => o.ActiveDate).FirstAsync();
            var minPriceValue = (minPrice.ROIMinprice != null || minPrice.ROIMinprice != 0) ? minPrice.ROIMinprice : minPrice.ApprovedMinPrice;
            var minPricePercent = minPriceValue * 0.05M;

            var today = DateTime.Now;
            var budgetMinPrice = await DB.BudgetMinPrices
                .Include(o => o.BudgetMinPriceType)
                .Where(o => o.ProjectID == model.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                .OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();

            if (budgetMinPrice == null)
            {
                var booking = await DB.Bookings.Where(o => o.ID == bookingID).Include(o => o.Unit).FirstOrDefaultAsync();
                var project = await DB.Projects.Where(o => o.ID == booking.ProjectID).FirstAsync();

                var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
                var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstAsync();

                var data = new BudgetMinPriceDTO
                {
                    Project = ProjectDropdownDTO.CreateFromModel(project),
                    Quarter = today.GetQuarter(),
                    Year = today.Year,
                    TransferTotalAmount = 0,
                    TransferTotalUnit = 0,
                    QuarterlyTotalAmount = 0
                };

                var quarterly = new BudgetMinPrice();
                data.ToModelQuarterly(ref quarterly);
                quarterly.ActiveDate = today;
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                var transfer = new BudgetMinPrice();
                data.ToModelTransfer(ref transfer);
                transfer.ActiveDate = today;
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

                await DB.BudgetMinPrices.AddAsync(quarterly);
                await DB.BudgetMinPrices.AddAsync(transfer);
                await DB.SaveChangesAsync();
                budgetMinPrice = await DB.BudgetMinPrices
                    .Include(o => o.BudgetMinPriceType)
                    .Where(o => o.ProjectID == model.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                    .OrderByDescending(o => o.ActiveDate)
                    .FirstOrDefaultAsync();
            }

            var budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPrice.ID && o.UnitID == model.UnitID).FirstOrDefaultAsync();
            if (budgetMinPriceUnit == null)
            {
                var booking = await DB.Bookings.Where(o => o.ID == bookingID).Include(o => o.Unit).FirstOrDefaultAsync();
                var newBudgetMinPriceUnit = new BudgetMinPriceUnit();
                newBudgetMinPriceUnit.Amount = 0;
                newBudgetMinPriceUnit.BudgetMinPriceID = budgetMinPrice.ID;
                newBudgetMinPriceUnit.UnitID = booking.UnitID;
                await DB.BudgetMinPriceUnits.AddAsync(newBudgetMinPriceUnit);
                await DB.SaveChangesAsync();

                budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.ID == newBudgetMinPriceUnit.ID).FirstAsync();
            }

            var sellPrice = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var cashDiscount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var transferDiscount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var totalMinprice = minPriceValue - (sellPrice - (cashDiscount + transferDiscount + bugetPromotion.BudgetPromotionSale?.Budget));

            if ((totalMinprice <= budgetMinPriceUnit.Amount) && (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount)))
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.Quarterly).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                result.IsMinPriceWorkflow = true;
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice <= minPricePercent)
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocLessThan5Percent).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                result.IsMinPriceWorkflow = true;
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice > minPricePercent)
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                result.IsMinPriceWorkflow = true;
            }
            #endregion

            return result;
        }

        private async Task CreateMinPriceBudgetWorkflowFromChangePromotionAsync(Guid bookingID, Guid promotionID, Guid changePromotionID, Guid? minPriceRequestReasonID, string OtherMinPriceRequestReason)
        {
            var model = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();
            var unitPriceItem = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
            var discount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();

            #region Promotion
            var promotion = await DB.BookingPromotions.Where(o => o.ID == promotionID).FirstAsync();
            var totalPreSalePrice = await DB.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == bookingID).SumAsync(o => o.TotalPrice);
            var totalPromotionPrice = await DB.BookingPromotionItems.Where(o => o.BookingPromotionID == promotion.ID).SumAsync(o => o.TotalPrice);
            var totalExpenseByAP = await DB.BookingPromotionExpenses
                .Include(o => o.ExpenseReponsibleBy)
                .Where(o => o.BookingPromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).SumAsync(o => o.SellerAmount);
            var totalExpenseByHalf = await DB.BookingPromotionExpenses
                .Include(o => o.ExpenseReponsibleBy)
                .Where(o => o.BookingPromotionID == promotion.ID && o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).SumAsync(o => o.SellerAmount);

            var totalBudgetPromotion = discount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf;

            var query = await DB.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                .Select(o => new
                {
                    BudgetPromotion = o,
                    Unit = o.Unit
                }).ToListAsync();

            var temp = query.GroupBy(o => o.Unit).Select(o => new TempBudgetPromotionQueryResult
            {
                Unit = o.Key,
                BudgetPromotions = o.Select(p => p.BudgetPromotion).ToList()
            }).ToList();
            var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var bugetPromotion = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).FirstOrDefault();

            var isWaitingPromotion = (totalBudgetPromotion > bugetPromotion.BudgetPromotionSale?.Budget) ? true : false;
            #endregion

            #region Minimum Price
            var minPrice = await DB.MinPrices.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == model.UnitID).OrderByDescending(o => o.ActiveDate).FirstAsync();
            var minPriceValue = (minPrice.ROIMinprice != null && minPrice.ROIMinprice != 0) ? minPrice.ROIMinprice : minPrice.ApprovedMinPrice;
            var minPricePercent = minPriceValue * 0.05M;

            var today = DateTime.Now;
            var budgetMinPrice = await DB.BudgetMinPrices
                .Include(o => o.BudgetMinPriceType)
                .Where(o => o.ProjectID == model.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                .OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();
            if (budgetMinPrice == null)
            {
                var booking = await DB.Bookings.Where(o => o.ID == bookingID).Include(o => o.Unit).FirstOrDefaultAsync();
                var project = await DB.Projects.Where(o => o.ID == booking.ProjectID).FirstAsync();

                var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
                var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.TransferPromotion).Select(o => o.ID).FirstAsync();

                var data = new BudgetMinPriceDTO
                {
                    Project = ProjectDropdownDTO.CreateFromModel(project),
                    Quarter = today.GetQuarter(),
                    Year = today.Year,
                    TransferTotalAmount = 0,
                    TransferTotalUnit = 0,
                    QuarterlyTotalAmount = 0
                };

                var quarterly = new BudgetMinPrice();
                data.ToModelQuarterly(ref quarterly);
                quarterly.ActiveDate = today;
                quarterly.BudgetMinPriceTypeMasterCenterID = masterCenterQuarterlyID;

                var transfer = new BudgetMinPrice();
                data.ToModelTransfer(ref transfer);
                transfer.ActiveDate = today;
                transfer.BudgetMinPriceTypeMasterCenterID = masterCenterTransferID;

                await DB.BudgetMinPrices.AddAsync(quarterly);
                await DB.BudgetMinPrices.AddAsync(transfer);
                await DB.SaveChangesAsync();
                budgetMinPrice = await DB.BudgetMinPrices
                    .Include(o => o.BudgetMinPriceType)
                    .Where(o => o.ProjectID == model.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                    .OrderByDescending(o => o.ActiveDate)
                    .FirstOrDefaultAsync();
            }

            var budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPrice.ID && o.UnitID == model.UnitID).FirstOrDefaultAsync();
            if (budgetMinPriceUnit == null)
            {
                var booking = await DB.Bookings.Where(o => o.ID == bookingID).Include(o => o.Unit).FirstOrDefaultAsync();
                var newBudgetMinPriceUnit = new BudgetMinPriceUnit();
                newBudgetMinPriceUnit.Amount = 0;
                newBudgetMinPriceUnit.BudgetMinPriceID = budgetMinPrice.ID;
                newBudgetMinPriceUnit.UnitID = booking.UnitID;
                await DB.BudgetMinPriceUnits.AddAsync(newBudgetMinPriceUnit);
                await DB.SaveChangesAsync();

                budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.ID == newBudgetMinPriceUnit.ID).FirstAsync();
            }

            var sellPrice = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var cashDiscount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var transferDiscount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var totalSellPrice = (sellPrice - (cashDiscount + transferDiscount + bugetPromotion.BudgetPromotionSale?.Budget));
            var totalMinprice = minPriceValue - totalSellPrice;
            var isWaitingMinprice = false;
            #endregion

            var workflow = new MinPriceBudgetWorkflow()
            {
                ProjectID = model.ProjectID,
                BookingID = model.ID,
                IsRequestBudgetPromotion = isWaitingPromotion,
                MinPriceBudgetWorkflowStageMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceBudgetWorkflowStage && o.Key == MinPriceBudgetWorkflowStageKeys.Booking).Select(o => o.ID).FirstAsync(),
                SellingPrice = sellPrice,
                CashDiscount = cashDiscount,
                TransferDiscount = transferDiscount,
                MasterMinPrice = minPriceValue.Value,
                IsApproved = false,
                ChangePromotionWorkflowID = changePromotionID,
                BudgetMinPriceID = budgetMinPrice.ID,
                BudgetMinPriceUnitID = budgetMinPriceUnit.ID,
                MinPriceRequestReasonMasterCenterID = minPriceRequestReasonID,
                OtherMinPriceRequestReason = OtherMinPriceRequestReason
            };

            List<MinPriceBudgetApproval> minPriceBudgetApprovals = new List<MinPriceBudgetApproval>();
            var lcmRoleId = await DB.Roles.Where(o => o.Code == "LCM").Select(o => o.ID).FirstAsync();
            var hoRoleId = await DB.Roles.Where(o => o.Code == "HO").Select(o => o.ID).FirstAsync();

            if (isWaitingPromotion)
            {
                workflow.RequestBudgetPromotion = totalBudgetPromotion;
                workflow.BudgetPromotionTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
                workflow.BookingPromotionID = promotion.ID;
                workflow.MasterBudgetPromotion = bugetPromotion.BudgetPromotionSale.Budget.Value;
                workflow.FromMasterBudgetPromotionID = bugetPromotion.BudgetPromotionSale.ID;

                var lcmApproval = new MinPriceBudgetApproval()
                {
                    MinPriceBudgetWorkflowID = workflow.ID,
                    Order = 1,
                    RoleID = lcmRoleId,
                    IsApproved = null
                };

                var headApproval = new MinPriceBudgetApproval()
                {
                    MinPriceBudgetWorkflowID = workflow.ID,
                    Order = 2,
                    RoleID = hoRoleId,
                    IsApproved = null
                };

                minPriceBudgetApprovals.Add(lcmApproval);
                minPriceBudgetApprovals.Add(headApproval);
            }

            if ((totalMinprice <= budgetMinPriceUnit.Amount) && (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount)))
            {
                workflow.IsRequestMinPrice = true;
                workflow.RequestMinPrice = totalSellPrice.Value;
                workflow.MinPriceWorkflowTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
                workflow.FromMasterMinPriceID = minPrice.ID;
                isWaitingMinprice = true;

                if (!isWaitingPromotion)
                {
                    var lcmApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 1,
                        RoleID = lcmRoleId,
                        IsApproved = null
                    };

                    var headApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 2,
                        RoleID = hoRoleId,
                        IsApproved = null
                    };

                    minPriceBudgetApprovals.Add(lcmApproval);
                    minPriceBudgetApprovals.Add(headApproval);
                }
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice <= minPricePercent)
            {
                workflow.IsRequestMinPrice = true;
                workflow.RequestMinPrice = totalSellPrice.Value;
                workflow.MinPriceWorkflowTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocLessThan5Percent).Select(o => o.ID).FirstAsync();
                workflow.FromMasterMinPriceID = minPrice.ID;
                isWaitingMinprice = true;

                if (!isWaitingPromotion)
                {
                    var lcmApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 1,
                        RoleID = lcmRoleId,
                        IsApproved = null
                    };

                    var headApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 2,
                        RoleID = hoRoleId,
                        IsApproved = null
                    };

                    minPriceBudgetApprovals.Add(lcmApproval);
                    minPriceBudgetApprovals.Add(headApproval);
                }

                var subApproval = new MinPriceBudgetApproval()
                {
                    MinPriceBudgetWorkflowID = workflow.ID,
                    Order = 3,
                    RoleID = await DB.Roles.Where(o => o.Code == "SBG").Select(o => o.ID).FirstAsync(),
                    IsApproved = null
                };

                minPriceBudgetApprovals.Add(subApproval);
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice > minPricePercent)
            {
                workflow.IsRequestMinPrice = true;
                workflow.RequestMinPrice = totalSellPrice.Value;
                workflow.MinPriceWorkflowTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent).Select(o => o.ID).FirstAsync();
                workflow.FromMasterMinPriceID = minPrice.ID;
                isWaitingMinprice = true;

                if (!isWaitingPromotion)
                {
                    var lcmApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 1,
                        RoleID = lcmRoleId,
                        IsApproved = null
                    };

                    var headApproval = new MinPriceBudgetApproval()
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 2,
                        RoleID = hoRoleId,
                        IsApproved = null
                    };

                    minPriceBudgetApprovals.Add(lcmApproval);
                    minPriceBudgetApprovals.Add(headApproval);
                }

                var subApproval = new MinPriceBudgetApproval()
                {
                    MinPriceBudgetWorkflowID = workflow.ID,
                    Order = 3,
                    RoleID = await DB.Roles.Where(o => o.Code == "SBG").Select(o => o.ID).FirstAsync(),
                    IsApproved = null
                };

                var mdApproval = new MinPriceBudgetApproval()
                {
                    MinPriceBudgetWorkflowID = workflow.ID,
                    Order = 4,
                    RoleID = await DB.Roles.Where(o => o.Code == "MD").Select(o => o.ID).FirstAsync(),
                    IsApproved = null
                };

                minPriceBudgetApprovals.Add(subApproval);
                minPriceBudgetApprovals.Add(mdApproval);
            }

            if (isWaitingMinprice || isWaitingPromotion)
            {
                await DB.MinPriceBudgetWorkflows.AddAsync(workflow);
                await DB.MinPriceBudgetApprovals.AddRangeAsync(minPriceBudgetApprovals);

                await DB.SaveChangesAsync();
            }
            else
            {
                var oldPromotion = await DB.BookingPromotions.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstOrDefaultAsync();
                if (oldPromotion != null)
                {
                    oldPromotion.IsActive = false;
                    DB.BookingPromotions.Update(oldPromotion);
                }

                var changePromotion = await DB.ChangePromotionWorkflows.Where(o => o.ID == changePromotionID).FirstAsync();
                changePromotion.IsApproved = true;
                promotion.IsActive = true;

                DB.Entry(promotion).State = EntityState.Modified;
                DB.Entry(changePromotion).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
        }
    }
}
