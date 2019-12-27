using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Base.DTOs.SAL.Sortings;
using Common;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using ErrorHandling;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;

namespace Sale.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private IQuotationService QuotationService;
        private FileHelper FileHelper;

        public BookingService(IConfiguration configuration, IQuotationService quotationService, DatabaseContext db)
        {
            this.DB = db;
            this.Configuration = configuration;
            this.QuotationService = quotationService;
            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<BookingListPaging> GetBookingListAsync(BookingListFilter filter, PageParam pageParam, BookingListSortByParam sortByParam)
        {
            var query = from o in DB.Bookings
                        join ow in DB.BookingOwners on o.ID equals ow.BookingID into g
                        from owner in g.Where(p => p.IsMainOwner == true).DefaultIfEmpty()
                        select new BookingQueryResult
                        {
                            Booking = o,
                            Unit = o.Unit,
                            BookingStatus = o.BookingStatus,
                            Owner = owner,
                            CreateBookingFrom = o.CreateBookingFrom,
                            ConfirmBy = o.ConfirmBy
                        };

            #region Filter
            if (filter.ProjectID != null)
            {
                query = query.Where(o => o.Booking.ProjectID == filter.ProjectID);
            }

            if (!string.IsNullOrEmpty(filter.BookingNo))
            {
                query = query.Where(o => o.Booking.BookingNo.Contains(filter.BookingNo));
            }

            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                query = query.Where(o => o.Unit.UnitNo.Contains(filter.UnitNo));
            }

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                query = query.Where(o => (o.Owner.FirstNameTH + o.Owner.LastNameTH).Trim().Contains(filter.FullName.Trim()));
            }

            if (filter.BookingDateFrom != null && filter.BookingDateTo != null)
            {
                query = query.Where(o => o.Booking.BookingDate >= filter.BookingDateFrom && o.Booking.BookingDate <= filter.BookingDateTo);
            }

            if (filter.ApproveDateFrom != null && filter.ApproveDateTo != null)
            {
                query = query.Where(o => o.Booking.ApproveDate >= filter.ApproveDateFrom && o.Booking.ApproveDate <= filter.ApproveDateTo);
            }

            if (filter.ContractDateFrom != null && filter.ContractDateTo != null)
            {
                query = query.Where(o => o.Booking.ContractDate >= filter.ContractDateFrom && o.Booking.ContractDate <= filter.ContractDateTo);
            }

            if (!string.IsNullOrEmpty(filter.BookingStatusKey))
            {
                var BookingStatusMasterCenterID = await DB.MasterCenters
                   .Where(x => x.Key == filter.BookingStatusKey && x.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus)
                   .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(o => o.Booking.BookingStatusMasterCenterID == BookingStatusMasterCenterID);
            }

            if (!string.IsNullOrEmpty(filter.BookingStatusKeys))
            {
                var keys = filter.BookingStatusKey.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.Booking.BookingStatusMasterCenterID ?? Guid.Empty));
            }

            if (!string.IsNullOrEmpty(filter.CreateBookingFromKeys))
            {
                var keys = filter.CreateBookingFromKeys.Split(',').ToList();
                var topicMasterCenterIDs = await DB.MasterCenters
                    .Where(x => keys.Contains(x.Key) && x.MasterCenterGroupKey == MasterCenterGroupKeys.CreateBookingFrom)
                    .Select(x => x.ID).ToListAsync();
                query = query.Where(q => topicMasterCenterIDs.Contains(q.Booking.CreateBookingFromMasterCenterID ?? Guid.Empty));
            }

            if (filter.ConfirmDateFrom != null && filter.ConfirmDateTo != null)
            {
                query = query.Where(o => o.Booking.ConfirmDate >= filter.ConfirmDateFrom && o.Booking.ConfirmDate <= filter.ConfirmDateTo);
            }

            if (filter.ConfirmByID != null)
            {
                query = query.Where(o => o.ConfirmBy.ID == filter.ConfirmByID);
            }

            if (filter.IsCancelled != null)
            {
                query = query.Where(o => o.Booking.IsCancelled == filter.IsCancelled);
            }
            #endregion

            BookingListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<BookingQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();
            var results = queryResults.Select(o => BookingListDTO.CreateFromQueryResult(o, DB)).ToList();

            return new BookingListPaging()
            {
                PageOutput = pageOutput,
                Bookings = results
            };
        }

        public async Task<BookingDTO> GetBookingAsync(Guid id)
        {
            var model = await DB.Bookings
                .Include(o => o.Quotation)
                .Include(o => o.BookingStatus)
                .Include(o => o.Project)
                .ThenInclude(o => o.ProductType)
                .Include(o => o.Model)
                .Include(o => o.SaleOfficerType)
                .Include(o => o.SaleUser)
                .Include(o => o.Agent)
                .Include(o => o.AgentEmployee)
                .Include(o => o.ProjectSaleUser)
                .Include(o => o.CreateBookingFrom)
                .Include(o => o.Unit)
                .ThenInclude(o => o.Floor)
                .Include(o => o.Unit)
                .ThenInclude(o => o.Tower)
                .Where(o => o.ID == id)
                .FirstAsync();

            return await BookingDTO.CreateFromModelAsync(model, DB);
        }

        public async Task<BookingPriceListDTO> GetPriceListAsync(Guid bookingID)
        {
            var result = await BookingPriceListDTO.CreateFromModelAsync(bookingID, DB);
            return result;
        }

        public async Task<BookingPreSalePromotionDTO> GetBookingPreSalePromotionAsync(Guid bookingID)
        {
            var model = await DB.PreSalePromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == bookingID).FirstOrDefaultAsync();

            var result = await BookingPreSalePromotionDTO.CreateFromModelAsync(model, DB);

            return result;
        }

        public async Task<BookingPromotionDTO> GetBookingPromotionAsync(Guid bookingID, BookingPromotionFilter filter = null)
        {
            var model = await DB.BookingPromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.BookingID == bookingID && o.IsActive == true).FirstOrDefaultAsync();

            var result = await BookingPromotionDTO.CreateFromModelAsync(model, DB);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.NameTH))
                {
                    result.Items = result.Items.Where(o => o.NameTH?.Contains(filter.NameTH) ?? false).ToList();
                }
                if (filter.PricePerUnitFrom != null)
                {
                    result.Items = result.Items.Where(o => o.PricePerUnit >= filter.PricePerUnitFrom).ToList();
                }
                if (filter.PricePerUnitTo != null)
                {
                    result.Items = result.Items.Where(o => o.PricePerUnit <= filter.PricePerUnitTo).ToList();
                }
                if (filter.TotalPriceFrom != null)
                {
                    result.Items = result.Items.Where(o => o.TotalPrice >= filter.TotalPriceFrom).ToList();
                }
                if (filter.TotalPriceTo != null)
                {
                    result.Items = result.Items.Where(o => o.TotalPrice <= filter.TotalPriceTo).ToList();
                }
                if (!string.IsNullOrEmpty(filter.UnitTH))
                {
                    result.Items = result.Items.Where(o => o.UnitTH?.Contains(filter.UnitTH) ?? false).ToList();
                }
            }

            return result;
        }

        public async Task<List<BookingPromotionExpenseDTO>> GetPromotionExpensesAsync(Guid bookingID)
        {
            var promotion = await DB.BookingPromotions.Where(o => o.BookingID == bookingID).FirstAsync();
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID).Select(o => o.ID).FirstAsync();

            var models = await DB.BookingPromotionExpenses
                .Include(o => o.MasterPriceItem)
                .Include(o => o.ExpenseReponsibleBy)
                .Where(o => o.BookingPromotionID == promotion.ID).ToListAsync();

            var results = models.Select(o => BookingPromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
            results = results.OrderBy(o => o.Order).ToList();
            return results;
        }

        public async Task<BookingDTO> UpdateBookingAsync(Guid bookingID, BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses, bool isBookingNo = true, bool isMinPriceWorkflow = true)
        {
            var model = await DB.Bookings
                .Include(o => o.Project)
                .ThenInclude(o => o.ProductType)
                .Where(o => o.ID == bookingID).FirstAsync();

            model.ContractDueDate = booking.ContractDueDate;
            model.BookingDate = booking.BookingDate;
            model.ReferContactID = priceList.ReferContact?.Id;

            var bookingStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.Booking).Select(o => o.ID).FirstAsync();

            var minPriceStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.WaitingForApproveMinPrice).Select(o => o.ID).FirstAsync();
            if (model.BookingStatusMasterCenterID != minPriceStatusId)
            {
                if (model.BookingStatusMasterCenterID != bookingStatusId)
                {
                    model.BookingStatusMasterCenterID = bookingStatusId;

                    var unit = await DB.Units.Where(o => o.ID == model.UnitID).FirstAsync();
                    unit.UnitStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForAgreement).Select(o => o.ID).FirstAsync();
                    DB.Entry(unit).State = EntityState.Modified;
                }
            }

            #region Agency
            var agencies = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SaleOfficerType).ToListAsync();
            model.SaleOfficerTypeMasterCenterID = booking.SaleOfficerType.Id;
            model.ProjectSaleUserID = booking.ProjectSaleUser?.Id;

            if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.AP).Select(o => o.ID).First())
            {
                model.SaleUserID = booking.SaleUser?.Id;
            }
            else if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.Agency).Select(o => o.ID).First())
            {
                model.AgentID = booking.Agent?.Id;
                model.AgentEmployeeID = booking.AgentEmployee?.Id;

            }
            else if (model.SaleOfficerTypeMasterCenterID == agencies.Where(o => o.Key == SaleOfficerTypeKeys.Referal).Select(o => o.ID).First())
            {
                model.SaleUserID = booking.SaleUser?.Id;
                model.AgentID = booking.Agent?.Id;
            }
            #endregion

            if (isBookingNo)
            {
                #region BookingNo
                if (model.BookingNo == null)
                {

                    string year = Convert.ToString(DateTime.Today.Year);
                    string month = DateTime.Today.ToString("MM");
                    var key = "BK" + model.Project.ProjectNo + year[2] + year[3] + month;
                    var type = "SAL.Booking";
                    var runningno = await DB.RunningNumberCounters.Where(o => o.Key == key && o.Type == type).FirstOrDefaultAsync();
                    if (runningno == null)
                    {
                        var runningNumberCounter = new RunningNumberCounter
                        {
                            Key = key,
                            Type = type,
                            Count = 1
                        };
                        await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                        await DB.SaveChangesAsync();

                        model.BookingNo = key + runningNumberCounter.Count.ToString("0000") + "00";
                        runningNumberCounter.Count++;
                        DB.Entry(runningNumberCounter).State = EntityState.Modified;
                    }
                    else
                    {
                        model.BookingNo = key + runningno.Count.ToString("0000") + "00";
                        runningno.Count++;
                        DB.Entry(runningno).State = EntityState.Modified;
                    }

                }
                #endregion
            }

            #region Unit Price
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();
            var UnitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
            var lastOrder = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).OrderByDescending(o => o.Order).Select(o => o.Order).FirstAsync();
            var discount = await DB.UnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).FirstOrDefaultAsync();

            if (discount == null)
            {
                var fgfModel = new UnitPriceItem()
                {
                    UnitPriceID = unitPrice.ID,
                    Order = lastOrder + 1,
                    Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Detail).FirstAsync(),
                    Amount = priceList.FGFDiscount,
                    IsToBePay = false,
                    PricePerUnitAmount = priceList.FGFDiscount,
                    PriceUnitAmount = 1,
                    MasterPriceItemID = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.ID).FirstAsync(),
                    PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == PriceTypeKeys.Discount).Select(o => o.ID).FirstAsync(),
                    PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                };

                await DB.UnitPriceItems.AddAsync(fgfModel);
            }
            else
            {
                discount.Amount = priceList.FGFDiscount;
                discount.IsToBePay = false;
                discount.PricePerUnitAmount = priceList.FGFDiscount;
                DB.Entry(discount).State = EntityState.Modified;
            }

            #endregion

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            #region Booking Promotion
            var bookingPromotionModel = await DB.BookingPromotions.Where(o => o.BookingID == bookingID).FirstAsync();
            bookingPromotionModel.TransferDateBefore = bookingPromotion.TransferDateBefore;
            DB.Entry(bookingPromotionModel).State = EntityState.Modified;

            #region Item
            var itemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item && o.IsSelected == true).ToList();
            var itemPromotionModel = await DB.BookingPromotionItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in itemPromotion)
            {
                if (!itemPromotionModel.Any(o => o.ID == item.Id))
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
            }

            foreach (var item in itemPromotionModel)
            {
                var existed = itemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Free
            var freeItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected == true).ToList();
            var freeItemPromotionModel = await DB.BookingPromotionFreeItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in freeItemPromotion)
            {
                if (!freeItemPromotionModel.Any(o => o.ID == item.Id))
                {
                    var freePromotionItemModel = new BookingPromotionFreeItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterBookingPromotionFreeItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingPromotionFreeItemID = item.FromQuotationBookingPromotionItemID
                    };

                    await DB.BookingPromotionFreeItems.AddAsync(freePromotionItemModel);
                }
            }

            foreach (var item in freeItemPromotionModel)
            {
                var existed = freeItemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Credit
            var creditItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected == true).ToList();
            var creditItemPromotionModel = await DB.BookingCreditCardItems.Where(o => o.BookingPromotionID == bookingPromotionModel.ID).ToListAsync();
            foreach (var item in creditItemPromotion)
            {
                if (!creditItemPromotionModel.Any(o => o.ID == item.Id))
                {
                    var creditPromotionItemModel = new BookingCreditCardItem()
                    {
                        BookingPromotionID = bookingPromotionModel.ID,
                        MasterBookingCreditCardItemID = item.FromMasterBookingPromotionItemID,
                        QuotationBookingCreditCardItemID = item.FromQuotationBookingPromotionItemID
                    };

                    await DB.BookingCreditCardItems.AddAsync(creditPromotionItemModel);
                }
            }

            foreach (var item in creditItemPromotionModel)
            {
                var existed = creditItemPromotion.Where(o => o.Id == item.ID).FirstOrDefault();
                if (existed == null)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #endregion

            #region Expense
            foreach (var expense in expenses)
            {
                var expenseModel = await DB.BookingPromotionExpenses.Where(o => o.BookingPromotionID == bookingPromotionModel.ID && o.MasterPriceItemID == expense.MasterPriceItem.Id).FirstAsync();
                expenseModel.SellerAmount = expense.SellerPayAmount;
                expenseModel.BuyerAmount = expense.BuyerPayAmount;
                expenseModel.ExpenseReponsibleByMasterCenterID = expense.ExpenseReponsibleBy.Id;
                DB.Entry(expenseModel).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
            #endregion

            if (isMinPriceWorkflow)
            {
                await this.CreateMinPriceBudgetWorkflowAsync(bookingID, booking.MinPriceRequestReason?.Id, booking.OtherMinPriceRequestReason);
            }

            var result = await this.GetBookingAsync(bookingID);
            return result;
        }

        public async Task<CancelMemoDTO> GetCancelMemoFormAsync(Guid bookingID)
        {
            var result = new CancelMemoDTO();
            result.CancelReturn = MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.GetAsync(MasterCenterGroupKeys.CancelReturnType, CancelReturnTypeKeys.ReturnAll));
            result.TotalReceivedAmount = await DB.Payments.Where(o => o.BookingID == bookingID).SumAsync(o => o.TotalAmount);
            var bookingDeliverAmount = await DB.BookingPromotionDeliveryItems.Where(o => o.BookingPromotionDelivery.BookingPromotion.Booking.ID == bookingID).SumAsync(o => o.BookingPromotionItem.TotalPrice);
            var transferDeliverAmount = await DB.TransferPromotionDeliveryItems.Where(o => o.TransferPromotionDelivery.TransferPromotion.Booking.ID == bookingID).SumAsync(o => o.TransferPromotionItem.TotalPrice);
            result.TotalPromotionDeliverAmount = bookingDeliverAmount + transferDeliverAmount;
            return result;
        }

        public async Task CancelBookingAsync(Guid bookingID, CancelMemoDTO input, Guid userID)
        {
            await input.ValidateAsync(this.DB);
            var booking = await DB.Bookings.FirstAsync(o => o.ID == bookingID);

            //ยังไม่ชำระเงินจอง
            if (!(await DB.Payments.AnyAsync(o => o.BookingID == bookingID)))
            {
                var errMsg = await DB.ErrorMessages.GetAsync("ERR9999");
                ValidateException ex = new ValidateException();
                var msg = errMsg.Message.Replace("[message]", "ไม่สามารถยกเลิกได้เนื่องจากเป็นแปลงที่ยังไม่มีการชำระเงิน");
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                throw ex;
            }

            //ยังไม่นำฝาก
            var hasNoDeposit = await DB.PaymentMethods
                .Where(o => o.Payment.BookingID == bookingID &&
                PaymentMethodKeys.NeedToDepositKeys.Contains(o.PaymentMethodType.Key) &&
                o.DepositDetails.Sum(m => m.PayAmount) != o.PayAmount)
                .AnyAsync();
            if (hasNoDeposit)
            {
                var errMsg = await DB.ErrorMessages.GetAsync("ERR0087");
                ValidateException ex = new ValidateException();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                throw ex;
            }

            //ยกเลิกอนุมัติ Min Price ทั้งหมด
            var minPriceWorkflows = await DB.MinPriceBudgetWorkflows
                .Where(o => o.BookingID == bookingID)
                .ToListAsync();
            minPriceWorkflows.ForEach(o => o.IsCancelled = true);
            minPriceWorkflows.ForEach(o =>
            {
                o.IsCancelled = true;
                if (o.IsApproved == null)
                {
                    o.IsRecalled = true;
                    o.RecalledByUserID = userID;
                    o.RecalledTime = DateTime.Now;
                }
            });
            DB.UpdateRange(minPriceWorkflows);

            input.Booking = new BookingDropdownDTO() { Id = booking.ID };

            var cancelMemo = new CancelMemo();
            input.ToModel(ref cancelMemo);
            if (input.BankRejectDocument != null && input.BankRejectDocument.IsTemp)
            {
                string bankRejectDocumentName = $"{booking.ID}/BankRejectDocument/{input.BankRejectDocument.Name}";
                await FileHelper.MoveTempFileAsync(input.BankRejectDocument.Name, bankRejectDocumentName);
                cancelMemo.BankRejectDocument = bankRejectDocumentName;
            }
            if (input.ReturnBookBankFile != null && input.ReturnBookBankFile.IsTemp)
            {
                string returnBookingFileName = $"{booking.ID}/ReturnBookBank/{input.ReturnBookBankFile.Name}";
                await FileHelper.MoveTempFileAsync(input.ReturnBookBankFile.Name, returnBookingFileName);
                cancelMemo.ReturnBookBankFile = returnBookingFileName;
            }

            cancelMemo.CancelByUserID = userID;
            await DB.AddAsync(cancelMemo);

            booking.IsCancelled = true;
            booking.CancelDate = DateTime.Now;
            booking.CancelType = BookingCancelType.Cancel;
            DB.Update(booking);

            await DB.SaveChangesAsync();
        }

        public async Task<BooleanResult> IsWaitingMinPriceApproveAsync(Guid bookingID)
        {
            var minPriceBudget = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == bookingID && o.IsRecalled == false && o.IsApproved == null).FirstOrDefaultAsync();

            var result = new BooleanResult
            {
                Result = (minPriceBudget != null) ? true : false
            };

            return result;
        }

        public async Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses)
        {
            var result = new MinPriceBudgetWorkflowTypeDTO();
            result.IsMinPriceWorkflow = false;
            result.IsBudgetPromotionWorkflow = false;
            var model = await DB.Bookings.Where(o => o.ID == booking.Id).FirstAsync();

            #region Promotion
            var discount = priceList.FGFDiscount;
            var totalPreSalePrice = await DB.PreSalePromotionItems.Include(o => o.PreSalePromotion).Where(o => o.PreSalePromotion.BookingID == booking.Id).SumAsync(o => o.TotalPrice);
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
                var bookingUnit = await DB.Bookings.Where(o => o.ID == model.ID).Include(o => o.Unit).FirstOrDefaultAsync();
                var project = await DB.Projects.Where(o => o.ID == bookingUnit.ProjectID).FirstAsync();

                var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
                var masterCenterTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();

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
                var bookingUnit = await DB.Bookings.Where(o => o.ID == model.ID).Include(o => o.Unit).FirstOrDefaultAsync();
                var newBudgetMinPriceUnit = new BudgetMinPriceUnit();
                newBudgetMinPriceUnit.Amount = 0;
                newBudgetMinPriceUnit.BudgetMinPriceID = budgetMinPrice.ID;
                newBudgetMinPriceUnit.UnitID = bookingUnit.UnitID;
                await DB.BudgetMinPriceUnits.AddAsync(newBudgetMinPriceUnit);
                await DB.SaveChangesAsync();

                budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.ID == newBudgetMinPriceUnit.ID).FirstAsync();
            }

            var totalMinprice = minPriceValue - (priceList.SellingPrice - (priceList.CashDiscount + priceList.TransferDiscount + bugetPromotion.BudgetPromotionSale.Budget));

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

        private async Task CreateMinPriceBudgetWorkflowAsync(Guid bookingID, Guid? minPriceRequestReasonID, string OtherMinPriceRequestReason)
        {
            var model = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            var minPriceStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.WaitingForApproveMinPrice).Select(o => o.ID).FirstAsync();

            if (model.BookingStatusMasterCenterID == minPriceStatusId)
            {
                var minPriceBudget = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == bookingID && o.IsRecalled == false && o.IsApproved == null).FirstAsync();
                minPriceBudget.IsRecalled = true;
                minPriceBudget.IsApproved = false;
                minPriceBudget.RecalledByUserID = model.UpdatedByUserID;
                minPriceBudget.RecalledTime = model.Updated;
                DB.Entry(minPriceBudget).State = EntityState.Modified;
            }

            #region Promotion
            var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingID && o.IsActive == true).FirstAsync();
            var unitPriceItem = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPrice.ID).ToListAsync();
            var discount = unitPriceItem.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == unitPrice.ID).Select(o => o.Amount).First();
            var promotion = await DB.BookingPromotions.Where(o => o.BookingID == bookingID).FirstAsync();
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
                IsApproved = null,
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

                if (minPriceBudgetApprovals.Count > 0)
                    await DB.MinPriceBudgetApprovals.AddRangeAsync(minPriceBudgetApprovals);

                var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
                booking.BookingStatusMasterCenterID = minPriceStatusId;
                DB.Bookings.Update(booking);
                await DB.SaveChangesAsync();
            }
        }

        public async Task DeleteBookingAsync(Guid bookingID)
        {
            //validate
            var hasMinPriceWorkflow = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == bookingID && o.IsApproved == null && !o.IsCancelled && !o.IsRecalled).AnyAsync();
            if (hasMinPriceWorkflow)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.GetAsync("ERR9999");
                var msg = errMsg.Message.Replace("[message]", "ไม่สามารถลบได้ เนื่องจากติดการอนุมัติ Min Price");
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                throw ex;
            }

            var hasPayment = await DB.Payments.AnyAsync(o => o.BookingID == bookingID);
            if (hasPayment)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.GetAsync("ERR9999");
                var msg = errMsg.Message.Replace("[message]", "ไม่สามารถลบได้ เนื่องจากมีการชำระเงินแล้ว");
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                throw ex;
            }

            var booking = await DB.Bookings.Include(o => o.Unit).FirstAsync(o => o.ID == bookingID);
            booking.IsDeleted = true;
            DB.Update(booking);
            booking.Unit.UnitStatusMasterCenterID = await DB.MasterCenters.GetIDAsync(MasterCenterGroupKeys.UnitStatus, UnitStatusKeys.Available);
            DB.Update(booking.Unit);
            await DB.SaveChangesAsync();
        }

        public async Task ConfirmBookingAsync(List<BookingListDTO> input, Guid? userID)
        {
            foreach(var booking in input)
            {
                var model = await DB.Bookings
                    .Include(o => o.Project)
                    .ThenInclude(o => o.ProductType)
                    .Where(o => o.ID == booking.Id).FirstAsync();

                model.BookingStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == BookingStatusKeys.Booking).Select(o => o.ID).FirstAsync();
                model.ConfirmByUserID = userID;
                model.ConfirmDate = DateTime.Today;
                DB.Entry(model).State = EntityState.Modified;
                await DB.SaveChangesAsync();

                await this.CreateMinPriceBudgetWorkflowAsync(booking.Id.Value, null, null);
            }
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingDTO booking, BookingPriceListDTO priceList, BookingPromotionDTO bookingPromotion, List<BookingPromotionExpenseDTO> expenses)
        {
            var unit = await DB.Units
                .Include(o => o.Project)
                .ThenInclude(o => o.ProductType)
                .Where(o => o.ID == booking.Unit.Id).FirstAsync();

            #region Quotation
            #region Running
            string year = Convert.ToString(DateTime.Today.Year);
            string month = DateTime.Today.ToString("MM");
            var runningKey = "QT" + unit.Project.ProjectNo + year[2] + year[3] + month;
            var quotationNo = string.Empty;

            var runningNumber = await DB.RunningNumberCounters.Where(o => o.Key == runningKey && o.Type == "SAL.Quotation").FirstOrDefaultAsync();
            if (runningNumber == null)
            {
                var runningModel = new Database.Models.MST.RunningNumberCounter()
                {
                    Key = runningKey,
                    Type = "SAL.Quotation",
                    Count = 1
                };

                await DB.RunningNumberCounters.AddAsync(runningModel);
                quotationNo = runningKey + runningModel.Count.ToString("0000");
            }
            else
            {
                runningNumber.Count = runningNumber.Count + 1;
                quotationNo = runningKey + runningNumber.Count.ToString("0000");
                DB.Entry(runningNumber).State = EntityState.Modified;
            }
            #endregion

            var quotationModel = new Quotation()
            {
                QuotationNo = quotationNo,
                IssueDate = DateTime.Today,
                QuotationStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.QuotationStatus && o.Key == QuotationStatusKeys.Booking).Select(o => o.ID).FirstAsync(),
                ProjectID = unit.ProjectID,
                UnitID = unit.ID,
                ContractDate = booking.ContractDate,
                TransferOwnershipDate = booking.TransferOwnershipDate
            };
            await DB.Quotations.AddAsync(quotationModel);
            await DB.SaveChangesAsync();

            #region Unit Price
            var unitPriceModel = new QuotationUnitPrice()
            {
                QuotationID = quotationModel.ID,
                //FromPriceListID = priceList.FromPriceListID
            };
            await DB.QuotationUnitPrices.AddAsync(unitPriceModel);

            var orderItem = 1;

            #region SellPrice
            var sellPriceModel = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.SellPrice).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.SellingPrice,
                IsToBePay = false,
                PricePerUnitAmount = priceList.SellingPrice,
                PriceUnitAmount = 1,
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.SellPrice
            };
            orderItem = orderItem + 1;
            await DB.QuotationUnitPriceItems.AddAsync(sellPriceModel);
            #endregion

            #region NetSellPrice
            var netSellPriceModel = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.NetSellingPrice,
                IsToBePay = false,
                PricePerUnitAmount = priceList.NetSellingPrice,
                PriceUnitAmount = 1,
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.NetSellPrice
            };
            orderItem = orderItem + 1;
            await DB.QuotationUnitPriceItems.AddAsync(netSellPriceModel);
            #endregion

            #region BookingAmount
            var bookingAmountModel = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.BookingAmount).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.BookingAmount,
                IsToBePay = true,
                PricePerUnitAmount = priceList.NetSellingPrice,
                PriceUnitAmount = (double)((priceList.BookingAmount * 100) / priceList.NetSellingPrice / 100),
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.BookingAmount
            };

            await DB.QuotationUnitPriceItems.AddAsync(bookingAmountModel);
            orderItem = orderItem + 1;
            #endregion

            #region ContractAmount
            var contractAmount = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.ContractAmount).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.ContractAmount,
                IsToBePay = true,
                PricePerUnitAmount = priceList.NetSellingPrice,
                PriceUnitAmount = (double)((priceList.ContractAmount * 100) / priceList.NetSellingPrice / 100),
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.ContractAmount
            };

            await DB.QuotationUnitPriceItems.AddAsync(contractAmount);
            orderItem = orderItem + 1;
            #endregion

            #region DownAmount
            var downAmount = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.DownAmount).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.DownAmount,
                IsToBePay = true,
                PricePerUnitAmount = priceList.NetSellingPrice,
                PriceUnitAmount = (double)((priceList.DownAmount * 100) / priceList.NetSellingPrice / 100),
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.DownAmount,
                Installment = priceList.Installment,
                InstallmentAmount = priceList.InstallmentAmount
            };

            string specialInstallments = string.Empty;
            string specialInstallmentAmounts = string.Empty;
            if (priceList.SpecialInstallmentPeriods != null)
            {
                foreach (var spacial in priceList.SpecialInstallmentPeriods)
                {
                    specialInstallments += spacial.Period + ",";
                    specialInstallmentAmounts += spacial.Amount + ",";
                }
            }

            if (specialInstallments != string.Empty && specialInstallmentAmounts != string.Empty)
            {
                downAmount.SpecialInstallments = specialInstallments.TrimEnd(',');
                downAmount.SpecialInstallmentAmounts = specialInstallmentAmounts.TrimEnd(',');
            }

            await DB.QuotationUnitPriceItems.AddAsync(downAmount);
            orderItem = orderItem + 1;
            #endregion

            #region FreeDownDiscount
            if (priceList.IsFreeDown)
            {
                var freeDownModel = new QuotationUnitPriceItem()
                {
                    QuotationUnitPriceID = unitPriceModel.ID,
                    Order = orderItem,
                    Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FreeDownDiscount).Select(o => o.Detail).FirstAsync(),
                    Amount = priceList.FreeDownDiscount,
                    IsToBePay = false,
                    PricePerUnitAmount = priceList.FreeDownDiscount,
                    PriceUnitAmount = 1,
                    PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "2").Select(o => o.ID).FirstAsync(),
                    PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                    MasterPriceItemID = MasterPriceItemKeys.FreeDownDiscount,
                };

                await DB.QuotationUnitPriceItems.AddAsync(freeDownModel);
                orderItem = orderItem + 1;
            }
            #endregion

            #region CashDiscount
            var cashDiscountModel = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.CashDiscount).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.CashDiscount,
                IsToBePay = false,
                PricePerUnitAmount = priceList.CashDiscount,
                PriceUnitAmount = 1,
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "2").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.CashDiscount,
            };

            await DB.QuotationUnitPriceItems.AddAsync(cashDiscountModel);
            orderItem = orderItem + 1;
            #endregion

            #region TransferDiscount
            var transferDiscount = new QuotationUnitPriceItem()
            {
                QuotationUnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Detail).FirstAsync(),
                Amount = priceList.TransferDiscount,
                IsToBePay = false,
                PricePerUnitAmount = priceList.TransferDiscount,
                PriceUnitAmount = 1,
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "2").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.TransferDiscount,
            };

            await DB.QuotationUnitPriceItems.AddAsync(transferDiscount);
            orderItem = orderItem + 1;
            #endregion

            await DB.SaveChangesAsync();
            #endregion

            #region Booking Promotion
            var bookingPromotionModel = new QuotationBookingPromotion()
            {
                QuotationID = quotationModel.ID,
            };
            await DB.QuotationBookingPromotions.AddAsync(bookingPromotionModel);
            await DB.SaveChangesAsync();

            if (bookingPromotion != null)
            {
                var masterPromotion = await DB.MasterBookingPromotions.Where(o => o.PromotionNo == bookingPromotion.PromotionNo).FirstAsync();
                bookingPromotionModel.MasterBookingPromotionID = masterPromotion.ID;
                DB.QuotationBookingPromotions.Update(bookingPromotionModel);

                if (priceList.CashDiscount > masterPromotion.CashDiscount)
                {
                    ValidateException ex = new ValidateException();
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0052").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    throw ex;
                }

                #region Item
                var itemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item && o.IsSelected).ToList();
                foreach (var item in itemPromotion)
                {
                    var promotionItemModel = new QuotationBookingPromotionItem()
                    {
                        QuotationBookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID
                    };
                    await DB.QuotationBookingPromotionItems.AddAsync(promotionItemModel);

                    List<QuotationBookingPromotionItem> subItems = new List<QuotationBookingPromotionItem>();
                    foreach (var sub in item.SubItems)
                    {
                        var promotionSubItemModel = new QuotationBookingPromotionItem()
                        {
                            QuotationBookingPromotionID = bookingPromotionModel.ID,
                            Quantity = item.Quantity,
                            MainQuotationBookingPromotionID = promotionItemModel.ID,
                            MasterBookingPromotionItemID = item.FromMasterBookingPromotionItemID
                        };

                        subItems.Add(promotionSubItemModel);
                    }

                    if (subItems.Count >= 0)
                    {
                        await DB.QuotationBookingPromotionItems.AddRangeAsync(subItems);
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Free
                var freeItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected).ToList();
                foreach (var item in freeItemPromotion)
                {
                    var promotionItemModel = new QuotationBookingPromotionFreeItem()
                    {
                        QuotationBookingPromotionID = bookingPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterBookingPromotionFreeItemID = item.FromMasterBookingPromotionItemID
                    };
                    await DB.QuotationBookingPromotionFreeItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Credit
                var creditItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected).ToList();
                foreach (var item in creditItemPromotion)
                {
                    var promotionItemModel = new QuotationBookingCreditCardItem()
                    {
                        QuotationBookingPromotionID = bookingPromotionModel.ID,
                        MasterBookingCreditCardItemID = item.FromMasterBookingPromotionItemID
                    };
                    await DB.QuotationBookingCreditCardItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion
            }
            #endregion

            #region Transfer Promotion
            var transferPromotionModel = new QuotationTransferPromotion()
            {
                QuotationID = quotationModel.ID
            };
            await DB.QuotationTransferPromotions.AddAsync(transferPromotionModel);
            await DB.SaveChangesAsync();
            #endregion

            #region Expense
            if (unit.Project.ProductType.Key == "1")
            {
                #region Water
                var waterMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.WaterMeter).FirstOrDefault();
                if (waterMeter != null)
                {
                    var waterModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.WaterMeter).Select(o => o.Detail).FirstAsync(),
                        Amount = (decimal)waterMeter.PricePerUnitAmount * (decimal)waterMeter.PriceUnitAmount,
                        IsToBePay = true,
                        PricePerUnitAmount = waterMeter.PricePerUnitAmount,
                        PriceUnitAmount = waterMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.WaterMeter
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(waterModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Eletrict
                var eletrictMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.EletrictMeter).FirstOrDefault();
                if (eletrictMeter != null)
                {
                    var eletrictModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.EletrictMeter).Select(o => o.Detail).FirstAsync(),
                        Amount = (decimal)eletrictMeter.PricePerUnitAmount * (decimal)eletrictMeter.PriceUnitAmount,
                        IsToBePay = true,
                        PricePerUnitAmount = eletrictMeter.PricePerUnitAmount,
                        PriceUnitAmount = eletrictMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.EletrictMeter
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(eletrictModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Mortgage
                var mortgageMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageMeter != null)
                {
                    var mortgageModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.MortgageFee).Select(o => o.Detail).FirstAsync(),
                        Amount = mortgageMeter.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = mortgageMeter.PricePerUnitAmount,
                        PriceUnitAmount = mortgageMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.MortgageFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(mortgageModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Transfer
                var transferMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferMeter != null)
                {
                    var transferModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferFee).Select(o => o.Detail).FirstAsync(),
                        Amount = transferMeter.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = transferMeter.PricePerUnitAmount,
                        PriceUnitAmount = transferMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.TransferFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(transferModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Common
                var commonMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (commonMeter != null)
                {
                    var commonModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.CommonFee).Select(o => o.Detail).FirstAsync(),
                        Amount = commonMeter.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = commonMeter.PricePerUnitAmount,
                        PriceUnitAmount = commonMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "3").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.CommonFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(commonModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Document
                var documentMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentMeter != null)
                {
                    var documentModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.DocumentFee).Select(o => o.Detail).FirstAsync(),
                        Amount = documentMeter.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = documentMeter.PricePerUnitAmount,
                        PriceUnitAmount = documentMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.DocumentFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(documentModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                await DB.SaveChangesAsync();
            }
            else if (unit.Project.ProductType.Key == "2")
            {
                #region Electrict
                var eletrictMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.EletrictMeter).FirstOrDefault();
                if (eletrictMeter != null)
                {
                    var eletrictModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.EletrictMeter).Select(o => o.Detail).FirstAsync(),
                        Amount = (decimal)eletrictMeter.PricePerUnitAmount * (decimal)eletrictMeter.PriceUnitAmount,
                        IsToBePay = true,
                        PricePerUnitAmount = eletrictMeter.PricePerUnitAmount,
                        PriceUnitAmount = eletrictMeter.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.EletrictMeter
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(eletrictModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Mortgage
                var mortgageFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageFee != null)
                {
                    var mortgageModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.MortgageFee).Select(o => o.Detail).FirstAsync(),
                        Amount = mortgageFee.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = mortgageFee.PricePerUnitAmount,
                        PriceUnitAmount = mortgageFee.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.MortgageFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(mortgageModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Transfer
                var transferFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferFee != null)
                {
                    var transferModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferFee).Select(o => o.Detail).FirstAsync(),
                        Amount = transferFee.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = transferFee.PricePerUnitAmount,
                        PriceUnitAmount = transferFee.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.TransferFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(transferModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Common
                var CommonFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (CommonFee != null)
                {
                    var commonModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.CommonFee).Select(o => o.Detail).FirstAsync(),
                        Amount = CommonFee.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = CommonFee.PricePerUnitAmount,
                        PriceUnitAmount = CommonFee.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "3").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.CommonFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(commonModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Document
                var documentFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentFee != null)
                {
                    var documentModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.DocumentFee).Select(o => o.Detail).FirstAsync(),
                        Amount = documentFee.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = documentFee.PricePerUnitAmount,
                        PriceUnitAmount = documentFee.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.DocumentFee
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(documentModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region First Sinking
                var first = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.FirstSinkingFund).FirstOrDefault();
                if (first != null)
                {
                    var firstModel = new QuotationUnitPriceItem()
                    {
                        QuotationUnitPriceID = unitPriceModel.ID,
                        Order = orderItem,
                        Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FirstSinkingFund).Select(o => o.Detail).FirstAsync(),
                        Amount = first.Amount,
                        IsToBePay = true,
                        PricePerUnitAmount = first.PricePerUnitAmount,
                        PriceUnitAmount = first.PriceUnitAmount,
                        PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "1").Select(o => o.ID).FirstAsync(),
                        PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "4").Select(o => o.ID).FirstAsync(),
                        MasterPriceItemID = MasterPriceItemKeys.FirstSinkingFund
                    };

                    await DB.QuotationUnitPriceItems.AddAsync(firstModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                await DB.SaveChangesAsync();
            }

            foreach (var expense in expenses)
            {
                QuotationPromotionExpense expenseModel = new QuotationPromotionExpense()
                {
                    QuotationBookingPromotionID = bookingPromotionModel.ID,
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

                await DB.QuotationPromotionExpenses.AddAsync(expenseModel);
                await DB.SaveChangesAsync();
            }
            #endregion
            #endregion

            #region Booking
            await QuotationService.ConvertToBookingAsync(quotationModel.ID, false);
            var bookingModel = await DB.Bookings.Where(o => o.QuotationID == quotationModel.ID).FirstAsync();
            bookingModel.BookingNo = booking.BookingNo;
            bookingModel.ContractDueDate = booking.ContractDueDate;
            bookingModel.BookingDate = booking.BookingDate;
            bookingModel.ReferContactID = priceList.ReferContact?.Id;
            bookingModel.CreateBookingFromMasterCenterID = booking.CreateBookingFrom.Id;
            DB.Entry(bookingModel).State = EntityState.Modified;

            var bookingUnitPrice = await DB.UnitPrices.Where(o => o.BookingID == bookingModel.ID && o.IsActive == true).FirstAsync();
            var bookingUnitPriceItems = await DB.UnitPriceItems.Where(o => o.UnitPriceID == bookingUnitPrice.ID).ToListAsync();
            var lastOrder = await DB.UnitPriceItems.Where(o => o.UnitPriceID == bookingUnitPrice.ID).OrderByDescending(o => o.Order).Select(o => o.Order).FirstAsync();
            var discount = await DB.UnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FGFDiscount && o.UnitPriceID == bookingUnitPrice.ID).FirstOrDefaultAsync();
            if (discount == null)
            {
                var fgfModel = new UnitPriceItem()
                {
                    UnitPriceID = bookingUnitPrice.ID,
                    Order = lastOrder + 1,
                    Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.Detail).FirstAsync(),
                    Amount = priceList.FGFDiscount,
                    IsToBePay = false,
                    PricePerUnitAmount = priceList.FGFDiscount,
                    PriceUnitAmount = 1,
                    MasterPriceItemID = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FGFDiscount).Select(o => o.ID).FirstAsync(),
                    PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == PriceTypeKeys.Discount).Select(o => o.ID).FirstAsync(),
                    PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                };

                await DB.UnitPriceItems.AddAsync(fgfModel);
            }
            else
            {
                discount.Amount = priceList.FGFDiscount;
                discount.IsToBePay = false;
                discount.PricePerUnitAmount = priceList.FGFDiscount;
                DB.Entry(discount).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();
            #endregion

            var result = await this.GetBookingAsync(quotationModel.ID);
            return result;
        }
    }
}
