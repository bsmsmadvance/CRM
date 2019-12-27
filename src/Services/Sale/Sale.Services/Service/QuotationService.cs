using System;
using System.Collections.Generic;
using System.Linq;
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
using Report.Integration;
using Report.Integration.PrintForms.MD;
using Sale.Params.Filters;
using Sale.Params.Outputs;

namespace Sale.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IConfiguration Configuration;
        private readonly DatabaseContext DB;
        private FileHelper FileHelper;
        private IPriceListWorkflowService PriceListWorkflowService;

        public QuotationService(IPriceListWorkflowService priceListWorkflowService, IConfiguration configuration, DatabaseContext db)
        {
            this.DB = db;
            this.PriceListWorkflowService = priceListWorkflowService;
            this.Configuration = configuration;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName);
        }

        public async Task<QuotationListPaging> GetQuotationListAsync(QuotationListFilter filter, PageParam pageParam, QuotationListSortByParam sortByParam)
        {
            IQueryable<QuotationQueryResult> query = DB.Quotations
                .Include(o => o.Unit)
                .ThenInclude(o => o.UnitStatus)
                .Select(o => new QuotationQueryResult
                {
                    Quotation = o,
                    Project = o.Project,
                    Unit = o.Unit,
                    QuotationStatus = o.QuotationStatus,
                    User = o.CreatedBy
                });

            #region Filter
            if (!string.IsNullOrEmpty(filter.QuotationNo))
                query = query.Where(q => q.Quotation.QuotationNo.Contains(filter.QuotationNo));

            if (filter.ProjectID != null)
                query = query.Where(q => q.Project.ID == filter.ProjectID);

            if (!string.IsNullOrEmpty(filter.UnitNo))
                query = query.Where(q => q.Unit.UnitNo.Contains(filter.UnitNo));

            if (!string.IsNullOrEmpty(filter.HouseNoFrom) && !string.IsNullOrEmpty(filter.HouseNoTo))
                query = query.Where(q => (String.Compare(q.Unit.HouseNo, filter.HouseNoFrom) >= 0) && (String.Compare(q.Unit.HouseNo, filter.HouseNoTo) <= 0));

            if (filter.IssueDateFrom != null && filter.IssueDateTo != null)
                query = query.Where(q => q.Quotation.IssueDate >= filter.IssueDateFrom && q.Quotation.IssueDate <= filter.IssueDateTo);

            if (!string.IsNullOrEmpty(filter.QuotationStatusKey))
            {
                var QuotationStatusMasterCenterID = await DB.MasterCenters
                    .Where(x => x.Key == filter.QuotationStatusKey && x.MasterCenterGroupKey == "QuotationStatus")
                    .Select(x => x.ID).FirstOrDefaultAsync();
                query = query.Where(q => q.Quotation.QuotationStatusMasterCenterID == QuotationStatusMasterCenterID);
            }

            if (filter.CreatedByUserID != null)
                query = query.Where(q => q.Quotation.CreatedByUserID == filter.CreatedByUserID);
            #endregion

            QuotationListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<QuotationQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => QuotationListDTO.CreateFromQueryResult(o)).ToList();

            return new QuotationListPaging()
            {
                PageOutput = pageOutput,
                Quotations = result
            };
        }

        public async Task<QuotationDTO> GetQuotationAsync(Guid quotationID)
        {
            var quotation = await DB.Quotations
                .Include(o => o.QuotationStatus)
                .Include(o => o.Project)
                .ThenInclude(o => o.ProductType)
                .Include(o => o.CreatedBy)
                .Include(o => o.Unit)
                .ThenInclude(o => o.UnitStatus)
                .Where(o => o.ID == quotationID).FirstOrDefaultAsync();

            var result = QuotationDTO.CreateFromModel(quotation);
            return result;
        }

        public async Task<QuotationPriceListDTO> GetPriceListDraftAsync(Guid unitID)
        {
            var result = await QuotationPriceListDTO.CreateDraftFromUnitAsync(unitID, DB);

            return result;
        }

        public async Task<QuotationBookingPromotionDTO> GetBookingPromotionDraftAsync(Guid unitID, QuotationBookingPromotionFilter filter = null)
        {
            var result = await QuotationBookingPromotionDTO.CreateFromUnitAsync(unitID, DB);

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
                if (!string.IsNullOrEmpty(filter.PRNo))
                {
                    result.Items = result.Items.Where(o => o.PRNo?.Contains(filter.PRNo) ?? false).ToList();
                }
            }

            return result;
        }

        public async Task<QuotationTransferPromotionDTO> GetTransferPromotionDraftAsync(Guid unitID, QuotationTransferPromotionFilter filter = null)
        {
            var result = await QuotationTransferPromotionDTO.CreateFromUnitAsync(unitID, DB);

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

        public async Task<List<QuotationPromotionExpenseDTO>> GetPromotionExpensesDraftAsync(Guid unitID)
        {
            var result = await QuotationPromotionExpenseDTO.CreateDraftFromUnitAsync(unitID, DB);

            return result;
        }

        public async Task<QuotationDTO> CreateQuotationAsync(Guid unitID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, QuotationTransferPromotionDTO transferPromotion, List<QuotationPromotionExpenseDTO> expenses)
        {
            var unit = await DB.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Where(o => o.ID == unitID).FirstAsync();

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
                QuotationStatusMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.QuotationStatus && o.Key == "1").Select(o => o.ID).FirstAsync(),
                ProjectID = unit.ProjectID,
                UnitID = unit.ID,
                ContractDate = priceList.ContractDate,
                TransferOwnershipDate = priceList.TransferOwnershipDate
            };
            await DB.Quotations.AddAsync(quotationModel);
            await DB.SaveChangesAsync();

            #region Unit Price
            var unitPriceModel = new QuotationUnitPrice()
            {
                QuotationID = quotationModel.ID,
                FromPriceListID = priceList.FromPriceListID
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

            if (transferPromotion != null)
            {

                var masterPromotion = await DB.MasterTransferPromotions.Where(o => o.PromotionNo == transferPromotion.PromotionNo).Select(o => o.ID).FirstAsync();
                transferPromotionModel.MasterTransferPromotionID = masterPromotion;
                transferPromotionModel.Remark = transferPromotion.Remark;
                DB.QuotationTransferPromotions.Update(transferPromotionModel);

                #region Item
                var itemPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item && o.IsSelected).ToList();
                foreach (var item in itemPromotion)
                {
                    var promotionItemModel = new QuotationTransferPromotionItem()
                    {
                        QuotationTransferPromotionID = transferPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.QuotationTransferPromotionItems.AddAsync(promotionItemModel);

                    List<QuotationTransferPromotionItem> subItems = new List<QuotationTransferPromotionItem>();
                    foreach (var sub in item.SubItems)
                    {
                        var promotionSubItemModel = new QuotationTransferPromotionItem()
                        {
                            QuotationTransferPromotionID = transferPromotionModel.ID,
                            Quantity = item.Quantity,
                            MainQuotationTransferPromotionID = promotionItemModel.ID,
                            MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                        };

                        subItems.Add(promotionSubItemModel);
                    }

                    if (subItems.Count >= 0)
                    {
                        await DB.QuotationTransferPromotionItems.AddRangeAsync(subItems);
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Free
                var freeItemPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected).ToList();
                foreach (var item in freeItemPromotion)
                {
                    var promotionItemModel = new QuotationTransferPromotionFreeItem()
                    {
                        QuotationTransferPromotionID = transferPromotionModel.ID,
                        Quantity = item.Quantity,
                        MasterTransferPromotionFreeItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.QuotationTransferPromotionFreeItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Credit
                var creditItemPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected).ToList();
                foreach (var item in creditItemPromotion)
                {
                    var promotionItemModel = new QuotationTransferCreditCardItem()
                    {
                        QuotationTransferPromotionID = transferPromotionModel.ID,
                        MasterTransferCreditCardItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.QuotationTransferCreditCardItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion
            }
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

            var result = await this.GetQuotationAsync(quotationModel.ID);
            return result;
        }

        public async Task DeleteQuotationAsync(Guid quotationID)
        {
            var model = await DB.Quotations.Where(o => o.ID == quotationID).FirstAsync();
            model.IsDeleted = true;

            DB.Quotations.Update(model);
            await DB.SaveChangesAsync();
        }

        public async Task<QuotationBookingPromotionDTO> GetBookingPromotionAsync(Guid quotationID, QuotationBookingPromotionFilter filter = null)
        {
            var model = await DB.QuotationBookingPromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();

            var result = await QuotationBookingPromotionDTO.CreateFromQuotationAsync(model, DB);

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
                if (!string.IsNullOrEmpty(filter.PRNo))
                {
                    result.Items = result.Items.Where(o => o.PRNo?.Contains(filter.PRNo) ?? false).ToList();
                }
            }

            return result;
        }

        public async Task<QuotationTransferPromotionDTO> GetTransferPromotionAsync(Guid quotationID, QuotationTransferPromotionFilter filter = null)
        {
            var model = await DB.QuotationTransferPromotions
                .Include(o => o.MasterPromotion)
                .Include(o => o.UpdatedBy)
                .Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();

            var result = await QuotationTransferPromotionDTO.CreateFromQuotationAsync(model, DB);
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

        public async Task<QuotationPriceListDTO> GetPriceListAsync(Guid quotationID)
        {
            var result = await QuotationPriceListDTO.CreateFromModelAsync(quotationID, DB);

            return result;
        }

        public async Task<List<QuotationPromotionExpenseDTO>> GetPromotionExpensesAsync(Guid quotationID)
        {
            var promotion = await DB.QuotationBookingPromotions.Where(o => o.QuotationID == quotationID).FirstAsync();
            var unitPrice = await DB.QuotationUnitPrices.Where(o => o.QuotationID == quotationID).Select(o => o.ID).FirstAsync();

            var models = await DB.QuotationPromotionExpenses
                .Include(o => o.MasterPriceItem)
                .Include(o => o.ExpenseReponsibleBy)
                .Where(o => o.QuotationBookingPromotionID == promotion.ID).ToListAsync();

            var results = models.Select(o => QuotationPromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
            results = results.OrderBy(o => o.Order).ToList();
            return results;
        }

        public async Task<QuotationDTO> SaveQuotationAsync(Guid quotationID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, QuotationTransferPromotionDTO transferPromotion, List<QuotationPromotionExpenseDTO> expenses)
        {
            var quotation = await DB.Quotations
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Where(o => o.ID == quotationID).FirstAsync();
            quotation.ContractDate = priceList.ContractDate;
            quotation.TransferOwnershipDate = priceList.TransferOwnershipDate;
            DB.Entry(quotation).State = EntityState.Modified;

            var unitPriceModel = await DB.QuotationUnitPrices.Where(o => o.QuotationID == quotationID).FirstAsync();
            var UnitPriceItemModels = await DB.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == unitPriceModel.ID).ToListAsync();
            var orderItem = 1;

            #region Unit Price
            #region SellPrice
            var sellPriceModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).First();
            sellPriceModel.Amount = priceList.SellingPrice;
            sellPriceModel.PricePerUnitAmount = priceList.SellingPrice;
            sellPriceModel.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(sellPriceModel).State = EntityState.Modified;
            #endregion

            #region NetSellPrice
            var netSellPriceModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).First();
            netSellPriceModel.Amount = priceList.NetSellingPrice;
            netSellPriceModel.PricePerUnitAmount = priceList.NetSellingPrice;
            netSellPriceModel.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(netSellPriceModel).State = EntityState.Modified;
            #endregion

            #region BookingAmount
            var bookingAmountModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).First();
            bookingAmountModel.Amount = priceList.BookingAmount;
            bookingAmountModel.PricePerUnitAmount = priceList.NetSellingPrice;
            bookingAmountModel.PriceUnitAmount = (double)((priceList.BookingAmount * 100) / priceList.NetSellingPrice / 100);
            bookingAmountModel.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(bookingAmountModel).State = EntityState.Modified;
            #endregion

            #region ContractAmount
            var contractAmount = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).First();
            contractAmount.Amount = priceList.ContractAmount;
            contractAmount.PricePerUnitAmount = priceList.NetSellingPrice;
            contractAmount.PriceUnitAmount = (double)((priceList.ContractAmount * 100) / priceList.NetSellingPrice / 100);
            contractAmount.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(contractAmount).State = EntityState.Modified;
            #endregion

            #region DownAmount
            var downAmount = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).First();
            downAmount.Amount = priceList.DownAmount;
            downAmount.PricePerUnitAmount = priceList.NetSellingPrice;
            downAmount.PriceUnitAmount = (double)((priceList.DownAmount * 100) / priceList.NetSellingPrice / 100);
            downAmount.Installment = priceList.Installment;
            downAmount.InstallmentAmount = priceList.InstallmentAmount;

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

            downAmount.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(downAmount).State = EntityState.Modified;
            #endregion

            #region FreeDownDiscount
            if (priceList.IsFreeDown)
            {
                var freeDownModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FreeDownDiscount).FirstOrDefault();
                if (freeDownModel != null)
                {
                    freeDownModel.Amount = priceList.FreeDownDiscount;
                    freeDownModel.PricePerUnitAmount = priceList.FreeDownDiscount;
                    freeDownModel.Order = orderItem;

                    DB.Entry(freeDownModel).State = EntityState.Modified;
                }
                else
                {
                    var freeDownNewModel = new QuotationUnitPriceItem()
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

                    await DB.QuotationUnitPriceItems.AddAsync(freeDownNewModel);
                }
                orderItem = orderItem + 1;
            }
            else
            {
                var freeDownModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FreeDownDiscount).FirstOrDefault();
                if (freeDownModel != null)
                {
                    freeDownModel.IsDeleted = true;
                    DB.Entry(freeDownModel).State = EntityState.Modified;
                }
            }
            #endregion

            #region CashDiscount
            var cashDiscountModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CashDiscount).First();
            cashDiscountModel.Amount = priceList.CashDiscount;
            cashDiscountModel.PricePerUnitAmount = priceList.CashDiscount;
            cashDiscountModel.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(cashDiscountModel).State = EntityState.Modified;
            #endregion

            #region TransferDiscount
            var transferDiscount = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferDiscount).First();
            transferDiscount.Amount = priceList.TransferDiscount;
            transferDiscount.PricePerUnitAmount = priceList.TransferDiscount;
            transferDiscount.Order = orderItem;
            orderItem = orderItem + 1;
            DB.Entry(transferDiscount).State = EntityState.Modified;
            #endregion

            await DB.SaveChangesAsync();
            #endregion

            #region Booking Promotion
            var bookingPromotionModel = await DB.QuotationBookingPromotions.Where(o => o.QuotationID == quotationID).FirstAsync();
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
                var itemPromotionModel = await DB.QuotationBookingPromotionItems.Where(o => o.QuotationBookingPromotionID == bookingPromotionModel.ID).ToListAsync();
                foreach (var item in itemPromotion)
                {
                    if (!itemPromotionModel.Any(o => o.MasterBookingPromotionItemID == item.FromMasterBookingPromotionItemID))
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
                }

                foreach (var item in itemPromotionModel)
                {
                    var existed = itemPromotion.Where(o => o.FromMasterBookingPromotionItemID == item.MasterBookingPromotionItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Free
                var freeItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected).ToList();
                var freeItemPromotionModel = await DB.QuotationBookingPromotionFreeItems.Where(o => o.QuotationBookingPromotionID == bookingPromotionModel.ID).ToListAsync();
                foreach (var item in freeItemPromotion)
                {
                    if (!freeItemPromotionModel.Any(o => o.MasterBookingPromotionFreeItemID == item.FromMasterBookingPromotionItemID))
                    {
                        var freePromotionItemModel = new QuotationBookingPromotionFreeItem()
                        {
                            QuotationBookingPromotionID = bookingPromotionModel.ID,
                            Quantity = item.Quantity,
                            MasterBookingPromotionFreeItemID = item.FromMasterBookingPromotionItemID
                        };

                        await DB.QuotationBookingPromotionFreeItems.AddAsync(freePromotionItemModel);
                    }
                }

                foreach (var item in freeItemPromotionModel)
                {
                    var existed = freeItemPromotion.Where(o => o.FromMasterBookingPromotionItemID == item.MasterBookingPromotionFreeItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Credit
                var creditItemPromotion = bookingPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected).ToList();
                var creditItemPromotionModel = await DB.QuotationBookingCreditCardItems.Where(o => o.QuotationBookingPromotionID == bookingPromotionModel.ID).ToListAsync();
                foreach (var item in creditItemPromotion)
                {
                    if (!creditItemPromotionModel.Any(o => o.MasterBookingCreditCardItemID == item.FromMasterBookingPromotionItemID))
                    {
                        var creditPromotionItemModel = new QuotationBookingCreditCardItem()
                        {
                            QuotationBookingPromotionID = bookingPromotionModel.ID,
                            MasterBookingCreditCardItemID = item.FromMasterBookingPromotionItemID
                        };

                        await DB.QuotationBookingCreditCardItems.AddAsync(creditPromotionItemModel);
                    }
                }

                foreach (var item in creditItemPromotionModel)
                {
                    var existed = creditItemPromotion.Where(o => o.FromMasterBookingPromotionItemID == item.MasterBookingCreditCardItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion
            }
            #endregion

            #region Transfer Promotion
            var transferPromotionModel = await DB.QuotationTransferPromotions.Where(o => o.QuotationID == quotationID).FirstAsync();
            if (transferPromotion != null)
            {
                var masterPromotion = await DB.MasterTransferPromotions.Where(o => o.PromotionNo == transferPromotion.PromotionNo).FirstAsync();
                transferPromotionModel.MasterTransferPromotionID = masterPromotion.ID;
                DB.QuotationTransferPromotions.Update(transferPromotionModel);

                #region Item
                var itemTransferPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.Item).ToList();
                var itemTransferPromotionModel = await DB.QuotationTransferPromotionItems.Where(o => o.QuotationTransferPromotionID == transferPromotionModel.ID).ToListAsync();
                foreach (var item in itemTransferPromotion)
                {
                    if (!itemTransferPromotionModel.Any(o => o.MasterTransferPromotionItemID == item.FromMasterTansferPromotionItemID))
                    {
                        var promotionTransferItemModel = new QuotationTransferPromotionItem()
                        {
                            QuotationTransferPromotionID = transferPromotionModel.ID,
                            Quantity = item.Quantity,
                            MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                        };

                        await DB.QuotationTransferPromotionItems.AddAsync(promotionTransferItemModel);

                        List<QuotationTransferPromotionItem> subItems = new List<QuotationTransferPromotionItem>();
                        foreach (var sub in item.SubItems)
                        {
                            var promotionTransferSubItemModel = new QuotationTransferPromotionItem()
                            {
                                QuotationTransferPromotionID = transferPromotionModel.ID,
                                Quantity = item.Quantity,
                                MainQuotationTransferPromotionID = promotionTransferItemModel.ID,
                                MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                            };

                            subItems.Add(promotionTransferSubItemModel);
                        }

                        if (subItems.Count >= 0)
                        {
                            await DB.QuotationTransferPromotionItems.AddRangeAsync(subItems);
                        }
                    }
                }

                foreach (var item in itemTransferPromotionModel)
                {
                    var existed = itemTransferPromotion.Where(o => o.FromMasterTansferPromotionItemID == item.MasterTransferPromotionItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Free
                var freeItemTransferPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.FreeItem).ToList();
                var freeItemTransferPromotionModel = await DB.QuotationTransferPromotionFreeItems.Where(o => o.QuotationTransferPromotionID == transferPromotionModel.ID).ToListAsync();
                foreach (var item in freeItemTransferPromotion)
                {
                    if (!freeItemTransferPromotionModel.Any(o => o.MasterTransferPromotionFreeItemID == item.FromMasterTansferPromotionItemID))
                    {
                        var freePromotionItemModel = new QuotationTransferPromotionFreeItem()
                        {
                            QuotationTransferPromotionID = bookingPromotionModel.ID,
                            Quantity = item.Quantity,
                            MasterTransferPromotionFreeItemID = item.FromMasterTansferPromotionItemID
                        };

                        await DB.QuotationTransferPromotionFreeItems.AddAsync(freePromotionItemModel);
                    }
                }

                foreach (var item in freeItemTransferPromotionModel)
                {
                    var existed = freeItemTransferPromotion.Where(o => o.FromMasterTansferPromotionItemID == item.MasterTransferPromotionFreeItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Credit
                var creditItemPromotion = transferPromotion.Items.Where(o => o.ItemType == PromotionItemType.CreditCard).ToList();
                var creditItemPromotionModel = await DB.QuotationTransferCreditCardItems.Where(o => o.QuotationTransferPromotionID == transferPromotionModel.ID).ToListAsync();
                foreach (var item in creditItemPromotion)
                {
                    if (!creditItemPromotionModel.Any(o => o.MasterTransferCreditCardItemID == item.FromMasterTansferPromotionItemID))
                    {
                        var creditPromotionItemModel = new QuotationTransferCreditCardItem()
                        {
                            QuotationTransferPromotionID = bookingPromotionModel.ID,
                            MasterTransferCreditCardItemID = item.FromMasterTansferPromotionItemID
                        };

                        await DB.QuotationTransferCreditCardItems.AddAsync(creditPromotionItemModel);
                    }
                }

                foreach (var item in creditItemPromotionModel)
                {
                    var existed = creditItemPromotion.Where(o => o.FromMasterTansferPromotionItemID == item.MasterTransferCreditCardItemID).FirstOrDefault();
                    if (existed == null)
                    {
                        item.IsDeleted = true;
                        DB.Entry(item).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion
            }
            #endregion

            #region Expense
            if (quotation.Project.ProductType.Key == "1")
            {
                #region Water
                var waterMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.WaterMeter).FirstOrDefault();
                if (waterMeter != null)
                {
                    var waterModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.WaterMeter).First();
                    waterModel.Amount = (decimal)waterMeter.PricePerUnitAmount * (decimal)waterMeter.PriceUnitAmount;
                    waterModel.PricePerUnitAmount = waterMeter.PricePerUnitAmount;
                    waterModel.PriceUnitAmount = waterMeter.PriceUnitAmount;
                    waterModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(waterModel).State = EntityState.Modified;
                }
                #endregion

                #region Eletrict
                var eletrictMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.EletrictMeter).FirstOrDefault();
                if (eletrictMeter != null)
                {
                    var eletrictModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.EletrictMeter).First();
                    eletrictModel.Amount = (decimal)eletrictMeter.PricePerUnitAmount * (decimal)eletrictMeter.PriceUnitAmount;
                    eletrictModel.PricePerUnitAmount = eletrictMeter.PricePerUnitAmount;
                    eletrictModel.PriceUnitAmount = eletrictMeter.PriceUnitAmount;
                    eletrictModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(eletrictModel).State = EntityState.Modified;
                }
                #endregion

                #region Mortgage
                var mortgageMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageMeter != null)
                {
                    var mortgageModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.MortgageFee).First();
                    mortgageModel.Amount = mortgageMeter.Amount;
                    mortgageModel.PricePerUnitAmount = mortgageMeter.PricePerUnitAmount;
                    mortgageModel.PriceUnitAmount = mortgageMeter.PriceUnitAmount;
                    mortgageModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(mortgageModel).State = EntityState.Modified;
                }
                #endregion

                #region Transfer
                var transferMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferMeter != null)
                {
                    var transferModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferFee).First();
                    transferModel.Amount = transferMeter.Amount;
                    transferModel.PricePerUnitAmount = transferMeter.PricePerUnitAmount;
                    transferModel.PriceUnitAmount = transferMeter.PriceUnitAmount;
                    transferModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(transferModel).State = EntityState.Modified;
                }
                #endregion

                #region Common
                var commonMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (commonMeter != null)
                {
                    var commonModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CommonFee).First();
                    commonModel.Amount = commonMeter.Amount;
                    commonModel.PricePerUnitAmount = commonMeter.PricePerUnitAmount;
                    commonModel.PriceUnitAmount = commonMeter.PriceUnitAmount;
                    commonModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(commonModel).State = EntityState.Modified;
                }
                #endregion

                #region Document
                var documentMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentMeter != null)
                {
                    var documentModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DocumentFee).First();
                    documentModel.Amount = documentMeter.Amount;
                    documentModel.PricePerUnitAmount = documentMeter.PricePerUnitAmount;
                    documentModel.PriceUnitAmount = documentMeter.PriceUnitAmount;
                    documentModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(documentModel).State = EntityState.Modified;
                }
                #endregion

                await DB.SaveChangesAsync();
            }
            else if (quotation.Project.ProductType.Key == "2")
            {
                #region Electrict
                var eletrictMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.EletrictMeter).FirstOrDefault();
                if (eletrictMeter != null)
                {
                    var eletrictModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.EletrictMeter).First();
                    eletrictModel.Amount = (decimal)eletrictMeter.PricePerUnitAmount * (decimal)eletrictMeter.PriceUnitAmount;
                    eletrictModel.PricePerUnitAmount = eletrictMeter.PricePerUnitAmount;
                    eletrictModel.PriceUnitAmount = eletrictMeter.PriceUnitAmount;
                    eletrictModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(eletrictModel).State = EntityState.Modified;
                }
                #endregion

                #region Mortgage

                var mortgageMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageMeter != null)
                {
                    var mortgageModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.MortgageFee).First();
                    mortgageModel.Amount = mortgageMeter.Amount;
                    mortgageModel.PricePerUnitAmount = mortgageMeter.PricePerUnitAmount;
                    mortgageModel.PriceUnitAmount = mortgageMeter.PriceUnitAmount;
                    mortgageModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(mortgageModel).State = EntityState.Modified;
                }
                #endregion

                #region Transfer
                var transferFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferFee != null)
                {
                    var transferModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.TransferFee).First();
                    transferModel.Amount = transferFee.Amount;
                    transferModel.PricePerUnitAmount = transferFee.PricePerUnitAmount;
                    transferModel.PriceUnitAmount = transferFee.PriceUnitAmount;
                    transferModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(transferModel).State = EntityState.Modified;
                }
                #endregion

                #region Common
                var CommonFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (CommonFee != null)
                {
                    var commonModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.CommonFee).First();
                    commonModel.Amount = CommonFee.Amount;
                    commonModel.PricePerUnitAmount = CommonFee.PricePerUnitAmount;
                    commonModel.PriceUnitAmount = CommonFee.PriceUnitAmount;
                    commonModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(commonModel).State = EntityState.Modified;
                }
                #endregion

                #region Document
                var documentFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentFee != null)
                {
                    var documentModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DocumentFee).First();
                    documentModel.Amount = documentFee.Amount;
                    documentModel.PricePerUnitAmount = documentFee.PricePerUnitAmount;
                    documentModel.PriceUnitAmount = documentFee.PriceUnitAmount;
                    documentModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(documentModel).State = EntityState.Modified;
                }
                #endregion

                #region First Sinking
                var first = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.FirstSinkingFund).FirstOrDefault();
                if (first != null)
                {
                    var firstModel = UnitPriceItemModels.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.FirstSinkingFund).First();
                    firstModel.Amount = first.Amount;
                    firstModel.PricePerUnitAmount = first.PricePerUnitAmount;
                    firstModel.PriceUnitAmount = first.PriceUnitAmount;
                    firstModel.Order = orderItem;
                    orderItem = orderItem + 1;
                    DB.Entry(firstModel).State = EntityState.Modified;
                }
                #endregion

                await DB.SaveChangesAsync();
            }

            foreach (var expense in expenses)
            {
                var expenseModel = await DB.QuotationPromotionExpenses.Where(o => o.QuotationBookingPromotionID == bookingPromotionModel.ID && o.MasterPriceItemID == expense.MasterPriceItem.Id).FirstAsync();
                expenseModel.SellerAmount = expense.SellerPayAmount;
                expenseModel.BuyerAmount = expense.BuyerPayAmount;
                expenseModel.ExpenseReponsibleByMasterCenterID = expense.ExpenseReponsibleBy.Id;
                DB.Entry(expenseModel).State = EntityState.Modified;
            }

            await DB.SaveChangesAsync();

            #endregion

            var result = await this.GetQuotationAsync(quotationID);
            return result;
        }

        public async Task<BooleanResult> IsWaitingPriceListChangedAsync(QuotationPriceListDTO priceList)
        {
            #region Master
            var masterPriceList = await DB.PriceLists.Where(o => o.ID == priceList.FromPriceListID).FirstOrDefaultAsync();
            var masterPriceListItems = await DB.PriceListItems.Where(o => o.PriceListID == masterPriceList.ID).Include(o => o.MasterPriceItem).ToListAsync();

            var masterSellingPrice = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            var masterBookingAmount = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var masterContractAmount = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var masterDownAmount = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            var masterInstallmentAmount = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var masterInstallmentNormalCount = (masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                                - (masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                    ? masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));

            var masterSpecialInstallmentAmount = masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();
            var masterSpecialInstallmentCount = (masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                ? masterPriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);
            #endregion

            var result = new BooleanResult();
            if (masterSellingPrice != priceList.SellingPrice
                || masterBookingAmount != priceList.BookingAmount
                || masterContractAmount != priceList.ContractAmount
                || masterDownAmount != priceList.DownAmount
                || masterInstallmentNormalCount != priceList.NormalInstallment
                || masterSpecialInstallmentCount != priceList.SpecialInstallment
            )
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }

        public async Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(Guid unitID, QuotationPriceListDTO priceList, QuotationBookingPromotionDTO bookingPromotion, List<QuotationPromotionExpenseDTO> expenses)
        {
            var result = new MinPriceBudgetWorkflowTypeDTO();
            result.IsMinPriceWorkflow = false;
            result.IsBudgetPromotionWorkflow = false;

            var unit = await DB.Units.FirstAsync(o => o.ID == unitID);
            
            #region Promotion
            var discount = priceList.FGFDiscount;

            var createPRTypeId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionRequestPRJobType && o.Key == PromotionRequestPRJobTypeKeys.CreatePR).Select(o => o.ID).FirstAsync();
            var completedPrStatusId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SAPPRStatus && o.Key == SAPPRStatusKeys.Completed).Select(o => o.ID).FirstAsync();
            var requestUnits = await DB.PreSalePromotionRequestUnits
                .Include(o => o.PreSalePromotionRequest).ThenInclude(o => o.MasterPreSalePromotion)
                .Where(o => o.UnitID == unitID && o.PromotionRequestPRJobTypeMasterCenterID == createPRTypeId && o.SAPPRStatusMasterCenterID == completedPrStatusId)
                .ToListAsync();

            var presalePromotionRequestItems = new List<PreSalePromotionRequestItem>();
            foreach (var requestUnit in requestUnits)
            {
                var items = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == requestUnit.ID).ToListAsync();
                presalePromotionRequestItems.AddRange(items);
            }
            var totalPreSalePrice = presalePromotionRequestItems.Sum(o => o.TotalPrice);

            var totalPromotionPrice = bookingPromotion.Items.Sum(o => o.TotalPrice);
            var totalExpenseByAP = expenses.Where(o => o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Company).Sum(o => o.SellerPayAmount);
            var totalExpenseByHalf = expenses.Where(o => o.ExpenseReponsibleBy.Key == ExpenseReponsibleByKeys.Half).Sum(o => o.SellerPayAmount);
            var totalBudgetPromotion = discount + totalPreSalePrice + totalPromotionPrice + totalExpenseByAP + totalExpenseByHalf;

            var query = await DB.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.UnitID == unitID)
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
            #endregion

            #region Minimum Price
            var minPrice = await DB.MinPrices.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == unitID).OrderByDescending(o => o.ActiveDate).FirstAsync();
            var minPriceValue = (minPrice.ROIMinprice != null || minPrice.ROIMinprice != 0) ? minPrice.ROIMinprice : minPrice.ApprovedMinPrice;
            var minPricePercent = minPriceValue * 0.05M;

            var today = DateTime.Now;
            var budgetMinPrice = await DB.BudgetMinPrices
                .Include(o => o.BudgetMinPriceType)
                .Where(o => o.ProjectID == unit.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                .OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();
            if (budgetMinPrice == null)
            {
                var project = await DB.Projects.Where(o => o.ID == unit.ProjectID).FirstAsync();

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
                    .Where(o => o.ProjectID == unit.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                    .OrderByDescending(o => o.ActiveDate)
                    .FirstOrDefaultAsync();
            }

            var budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPrice.ID && o.UnitID == unit.ID).FirstOrDefaultAsync();
            if (budgetMinPriceUnit == null)
            {
                var newBudgetMinPriceUnit = new BudgetMinPriceUnit();
                newBudgetMinPriceUnit.Amount = 0;
                newBudgetMinPriceUnit.BudgetMinPriceID = budgetMinPrice.ID;
                newBudgetMinPriceUnit.UnitID = unit.ID;
                await DB.BudgetMinPriceUnits.AddAsync(newBudgetMinPriceUnit);
                await DB.SaveChangesAsync();

                budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.ID == newBudgetMinPriceUnit.ID).FirstAsync();
            }

            var totalMinprice = minPriceValue - (priceList.SellingPrice - (priceList.CashDiscount + priceList.TransferDiscount + bugetPromotion.BudgetPromotionSale.Budget));
            var isWaitingMinprice = false;

            if ((totalMinprice <= budgetMinPriceUnit.Amount) && (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount)))
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.Quarterly).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                isWaitingMinprice = true;
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice <= minPricePercent)
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocLessThan5Percent).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                isWaitingMinprice = true;
            }
            else if (((totalMinprice > budgetMinPriceUnit.Amount) || (totalMinprice < (budgetMinPrice.TotalAmount - budgetMinPrice.UsedAmount))) && totalMinprice > minPricePercent)
            {
                var minpriceWorkflowType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType && o.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent).FirstAsync();
                result.MinPriceWorkflowType = MasterCenterDropdownDTO.CreateFromModel(minpriceWorkflowType);
                isWaitingMinprice = true;
            }
            #endregion

            result.IsMinPriceWorkflow = isWaitingMinprice;
            result.IsBudgetPromotionWorkflow = isWaitingPromotion;

            return result;
        }

        public async Task<BooleanResult> IsPriceListChangedAsync(Guid unitID, QuotationPriceListDTO input)
        {
            BooleanResult result = new BooleanResult();
            var priceList = await DB.PriceLists.GetActivePriceListAsync(unitID);
            var priceListItems = await DB.PriceListItems.Where(o => o.PriceListID == priceList.ID).Include(o => o.MasterPriceItem).ToListAsync();

            var masterSellingPrice = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            var masterBookingAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var masterContractAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var masterDownAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            var masterInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var masterInstallmentNormalCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                                - (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                    ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));
            var masterSpecialInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();

            var masterSpecialInstallmentCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);

            var listMasterSpecial = new List<Decimal>();
            var listQuotationSpecial = new List<Decimal>();
            string quotationSpecialInstallmentAmount = string.Empty;
            if (!string.IsNullOrEmpty(masterSpecialInstallmentAmount))
            {
                listMasterSpecial = masterSpecialInstallmentAmount.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o))).ToList();
                masterSpecialInstallmentAmount = String.Join(",", listMasterSpecial);
            }
            if (input.SpecialInstallmentPeriods.Count > 0)
            {
                listQuotationSpecial = input.SpecialInstallmentPeriods.Select(o => o.Amount).ToList();
                quotationSpecialInstallmentAmount = String.Join(",", listQuotationSpecial);
            }

            if (masterSellingPrice != input.SellingPrice
                || masterBookingAmount != input.BookingAmount
                || masterContractAmount != input.ContractAmount
                || masterDownAmount != input.DownAmount
                || masterSpecialInstallmentAmount != quotationSpecialInstallmentAmount
                || masterInstallmentNormalCount != input.NormalInstallment
                || masterSpecialInstallmentCount != input.SpecialInstallment
            )
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }

        public async Task<BookingDTO> ConvertToBookingAsync(Guid quotationID, bool isPricelistWorkflow = true)
        {
            #region DataFromQuotation
            var quotation = await DB.Quotations.Where(o => o.ID == quotationID)
                .Include(o => o.Project)
                .Include(o => o.Unit)
                .FirstOrDefaultAsync();
            var quotationUnitPrice = await DB.QuotationUnitPrices.Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();
            var quotationUnitPriceItems = await DB.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == quotationUnitPrice.ID).ToListAsync();
            var quotationBookingPromotion = await DB.QuotationBookingPromotions.Where(o => o.QuotationID == quotationID)
                .Include(o => o.MasterPromotion)
                .FirstOrDefaultAsync();
            var quotationBookingPromotionItems = await DB.QuotationBookingPromotionItems.Where(o => o.QuotationBookingPromotionID == quotationBookingPromotion.ID).Include(o => o.MasterPromotionItem).ToListAsync();
            var quotationBookingPromotionFreeItems = await DB.QuotationBookingPromotionFreeItems.Where(o => o.QuotationBookingPromotionID == quotationBookingPromotion.ID).ToListAsync();
            var quotationBookingPromotionCreditCardItem = await DB.QuotationBookingCreditCardItems.Where(o => o.QuotationBookingPromotionID == quotationBookingPromotion.ID).ToListAsync();
            var quotationBookingPromotionExpenses = await DB.QuotationPromotionExpenses.Where(o => o.QuotationBookingPromotionID == quotationBookingPromotion.ID).ToListAsync();
            var presalePromotionRequest = await DB.PreSalePromotionRequests.Where(o => o.ProjectID == quotation.ProjectID).FirstOrDefaultAsync();

            var quotationTransferPromotion = await DB.QuotationTransferPromotions.Where(o => o.QuotationID == quotationID)
                .Include(o => o.MasterPromotion)
                .FirstOrDefaultAsync();
            List<QuotationTransferPromotionItem> quotationTransferPromotionItems = new List<QuotationTransferPromotionItem>();
            List<QuotationTransferPromotionFreeItem> quotationTransferPromotionFreeItems = new List<QuotationTransferPromotionFreeItem>();
            List<QuotationTransferCreditCardItem> quotationTransferPromotionCreditCardItem = new List<QuotationTransferCreditCardItem>();

            if (quotationTransferPromotion != null)
            {
                quotationTransferPromotionItems = await DB.QuotationTransferPromotionItems.Where(o => o.QuotationTransferPromotionID == quotationTransferPromotion.ID).Include(o => o.MasterPromotionItem).ToListAsync();
                quotationTransferPromotionFreeItems = await DB.QuotationTransferPromotionFreeItems.Where(o => o.QuotationTransferPromotionID == quotationTransferPromotion.ID).ToListAsync();
                quotationTransferPromotionCreditCardItem = await DB.QuotationTransferCreditCardItems.Where(o => o.QuotationTransferPromotionID == quotationTransferPromotion.ID).ToListAsync();                
            }
            
            //สร้าง PR Job
            var createPRTypeID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionRequestPRJobType && o.Key == PromotionRequestPRJobTypeKeys.CreatePR).Select(o => o.ID).FirstAsync();
            //completed pr
            var completedPrStatusID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SAPPRStatus && o.Key == SAPPRStatusKeys.Completed).Select(o => o.ID).FirstAsync();
            //request unit ที่ขอ PR ผ่านทั้งหมดแล้ว
            var requestUnits = await DB.PreSalePromotionRequestUnits
                .Include(o => o.PreSalePromotionRequest).ThenInclude(o => o.MasterPreSalePromotion)
                .Where(o => o.UnitID == quotation.UnitID &&
                            o.PromotionRequestPRJobTypeMasterCenterID == createPRTypeID &&
                            o.SAPPRStatusMasterCenterID == completedPrStatusID)
                .ToListAsync();
            var presalePromotionRequestItems = new List<PreSalePromotionRequestItem>();
            foreach (var requestUnit in requestUnits)
            {
                var items = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == requestUnit.ID).ToListAsync();
                presalePromotionRequestItems.AddRange(items);
            }

            var isPriceListChange = await this.IsPriceListChangedAsync(quotation.ID);
            #endregion

            ValidateException ex = new ValidateException();
            if (DateTime.Now >= quotationBookingPromotion.MasterPromotion?.EndDate)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0076").FirstAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            foreach (var promotion in quotationBookingPromotionItems)
            {
                if (DateTime.Now >= promotion.MasterPromotionItem.ExpireDate)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0077").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (ex.HasError)
                {
                    throw ex;
                }
            }

            #region Booking
            var bookingStatusWaitingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == "1").Select(o => o.ID).FirstAsync();
            var bookingStatusPriceListChangeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus && o.Key == "0").Select(o => o.ID).FirstAsync();
            var bookigUnitPriceStageMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitPriceStage && o.Key == "1").Select(o => o.ID).FirstAsync();
            var booking = new Booking();
            booking.ProjectID = quotation.ProjectID;
            booking.QuotationID = quotation.ID;
            booking.SaleArea = quotation.Unit?.SaleArea;
            booking.UnitID = quotation.UnitID;
            booking.ModelID = quotation.Unit?.ModelID;
            booking.BookingDate = DateTime.Today;
            booking.ContractDueDate = DateTime.Today.AddDays(7);
            booking.TransferOwnershipDate = quotation.TransferOwnershipDate;
            booking.CreateBookingFromMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.CreateBookingFrom && o.Key == CreateBookingFromKeys.CRM).Select(o => o.ID).FirstAsync();

            if (isPricelistWorkflow)
            {
                if (isPriceListChange.Result)
                {
                    booking.BookingStatusMasterCenterID = bookingStatusPriceListChangeMasterCenterID;
                }
                else
                {
                    booking.BookingStatusMasterCenterID = bookingStatusWaitingMasterCenterID;
                }
            }
            #endregion

            #region UnitPirce
            var unitPrice = new UnitPrice();
            unitPrice.BookingID = booking.ID;
            unitPrice.IsActive = true;
            unitPrice.UnitPriceStageMasterCenterID = bookigUnitPriceStageMasterCenterID;
            var unitPriceItems = new List<UnitPriceItem>();
            var unitPriceInstallments = new List<UnitPriceInstallment>();
            foreach (var item in quotationUnitPriceItems)
            {
                var unitPriceItem = new UnitPriceItem();
                unitPriceItem.UnitPriceID = unitPrice.ID;
                unitPriceItem.Order = item.Order;
                unitPriceItem.MasterPriceItemID = item.MasterPriceItemID;
                unitPriceItem.Name = item.Name;
                unitPriceItem.PriceUnitAmount = item.PriceUnitAmount;
                unitPriceItem.PriceUnitMasterCenterID = item.PriceUnitMasterCenterID;
                unitPriceItem.PricePerUnitAmount = item.PricePerUnitAmount;
                unitPriceItem.Amount = item.Amount;
                unitPriceItem.IsToBePay = item.IsToBePay;
                unitPriceItem.Installment = item.Installment;
                unitPriceItem.PriceTypeMasterCenterID = item.PriceTypeMasterCenterID;
                unitPriceItems.Add(unitPriceItem);
            }
            var installment = unitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).FirstOrDefault();
            var quotationUnitPriceItemsDownAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).FirstOrDefault();

            var specialInstallment = quotationUnitPriceItemsDownAmount.SpecialInstallments != null ? quotationUnitPriceItemsDownAmount.SpecialInstallments.Split(',').Select(o => Convert.ToInt32(o)).ToList() : null;
            var specialInstallmentAmount = quotationUnitPriceItemsDownAmount.SpecialInstallments != null ? quotationUnitPriceItemsDownAmount.SpecialInstallmentAmounts.Split(',').Select(o => Convert.ToDecimal(o)).ToList() : null;


            for (int i = 1; i <= installment.Installment; i++)
            {
                var unitPriceInstallment = new UnitPriceInstallment();
                unitPriceInstallment.Period = i;
                unitPriceInstallment.InstallmentOfUnitPriceItemID = installment.ID;
                unitPriceInstallment.IsSpecialInstallment = false;
                unitPriceInstallment.IsPaid = false;
                unitPriceInstallment.Amount = Convert.ToDecimal(quotationUnitPriceItemsDownAmount.InstallmentAmount);
                unitPriceInstallments.Add(unitPriceInstallment);
            }

            if (specialInstallment != null)
            {
                var i = 0;
                foreach (var item in specialInstallment)
                {
                    var unitPriceSpecialInstallment = unitPriceInstallments.Where(o => o.Period == item).FirstOrDefault();
                    if (unitPriceSpecialInstallment == null)
                    {
                        break;
                    }
                    unitPriceSpecialInstallment.IsSpecialInstallment = true;
                    unitPriceSpecialInstallment.Amount = specialInstallmentAmount[i];
                    i++;
                }
            }
            #endregion

            #region Promotion

            #region Booking

            var bookingPromotion = new BookingPromotion();
            var bookingPromotionItems = new List<BookingPromotionItem>();
            var bookingPromotionFreeItems = new List<BookingPromotionFreeItem>();
            var bookingPromotionCreditCardItems = new List<BookingCreditCardItem>();
            var bookingPromotionExpenses = new List<BookingPromotionExpense>();


            bookingPromotion.BookingID = booking.ID;
            bookingPromotion.FGFDiscount = quotationBookingPromotion.MasterPromotion?.FGFDiscount;
            bookingPromotion.MasterBookingPromotionID = quotationBookingPromotion.MasterBookingPromotionID;
            bookingPromotion.TransferDiscount = quotationBookingPromotion.MasterPromotion?.TransferDiscount;
            bookingPromotion.BookingPromotionNo = quotationBookingPromotion.MasterPromotion?.PromotionNo;
            bookingPromotion.IsActive = true;
            //bookingPromotion.TotalAmount = 
            foreach (var item in quotationBookingPromotionItems.Where(o => o.MainQuotationBookingPromotionID == null))
            {
                var bookingPromotionItem = new BookingPromotionItem();
                bookingPromotionItem.BookingPromotionID = bookingPromotion.ID;
                bookingPromotionItem.PricePerUnit = item.MasterPromotionItem.PricePerUnit;
                bookingPromotionItem.Quantity = item.Quantity;
                bookingPromotionItem.MasterBookingPromotionItemID = item.MasterBookingPromotionItemID;
                bookingPromotionItem.QuotationBookingPromotionItemID = item.ID;
                bookingPromotionItem.TotalPrice = item.MasterPromotionItem.PricePerUnit * item.Quantity;
                bookingPromotionItems.Add(bookingPromotionItem);
                //List Sub
                foreach (var item1 in quotationBookingPromotionItems.Where(o => o.MainQuotationBookingPromotionID == item.ID))
                {
                    var bookingPromotionItemSub = new BookingPromotionItem();
                    bookingPromotionItemSub.MainBookingPromotionItemID = bookingPromotionItem.ID;
                    bookingPromotionItemSub.BookingPromotionID = bookingPromotion.ID;
                    bookingPromotionItemSub.PricePerUnit = item.MasterPromotionItem.PricePerUnit;
                    bookingPromotionItemSub.Quantity = item.Quantity;
                    bookingPromotionItemSub.MasterBookingPromotionItemID = item.MasterBookingPromotionItemID;
                    bookingPromotionItemSub.QuotationBookingPromotionItemID = item.ID;
                    bookingPromotionItemSub.TotalPrice = item.MasterPromotionItem.PricePerUnit * item.Quantity;
                    bookingPromotionItems.Add(bookingPromotionItemSub);
                }
            }

            foreach (var item in quotationBookingPromotionFreeItems)
            {
                var bookingPromotionFreeItem = new BookingPromotionFreeItem();
                bookingPromotionFreeItem.BookingPromotionID = bookingPromotion.ID;
                bookingPromotionFreeItem.QuotationBookingPromotionFreeItemID = item.ID;
                bookingPromotionFreeItem.MasterBookingPromotionFreeItemID = item.MasterBookingPromotionFreeItemID;
                bookingPromotionFreeItem.Quantity = item.Quantity;
                bookingPromotionFreeItems.Add(bookingPromotionFreeItem);
            }

            foreach (var item in quotationBookingPromotionCreditCardItem)
            {
                var bookingPromotionCreditCardItem = new BookingCreditCardItem();
                bookingPromotionCreditCardItem.MasterBookingCreditCardItemID = item.MasterBookingCreditCardItemID;
                bookingPromotionCreditCardItem.QuotationBookingCreditCardItemID = item.QuotationBookingPromotionID;
                bookingPromotionCreditCardItem.BookingPromotionID = booking.ID;
                bookingPromotionCreditCardItem.Fee = item.MasterBookingCreditCardItem.Fee;
                bookingPromotionCreditCardItems.Add(bookingPromotionCreditCardItem);
            }

            foreach (var item in quotationBookingPromotionExpenses)
            {
                var bookingPromotionExpense = new BookingPromotionExpense();
                bookingPromotionExpense.BookingPromotionID = bookingPromotion.ID;
                bookingPromotionExpense.ExpenseReponsibleByMasterCenterID = item.ExpenseReponsibleByMasterCenterID;
                bookingPromotionExpense.MasterPriceItemID = item.MasterPriceItemID;
                bookingPromotionExpense.Amount = item.Amount;
                bookingPromotionExpense.SellerAmount = item.SellerAmount;
                bookingPromotionExpense.BuyerAmount = item.BuyerAmount;
                bookingPromotionExpenses.Add(bookingPromotionExpense);
            }
            #endregion

            #region PreSale

            var preSalePromotionItems = new List<PreSalePromotionItem>();
            var preSalePromotion = new PreSalePromotion();
            preSalePromotion.BookingID = booking.ID;

            foreach (var item in presalePromotionRequestItems)
            {
                var preSalePromotionItem = new PreSalePromotionItem();
                preSalePromotionItem.PreSalePromotionID = preSalePromotion.ID;
                preSalePromotionItem.PreSalePromotionRequestItemID = item.ID;
                preSalePromotionItem.Quantity = item.Quantity;
                preSalePromotionItem.Unit = item.UnitTH;
                preSalePromotionItem.PricePerUnit = item.PricePerUnit;
                preSalePromotionItem.TotalPrice = item.TotalPrice;
                preSalePromotionItems.Add(preSalePromotionItem);
            }
            #endregion

            #region Transfer


            if (quotationTransferPromotion != null)
            {
                var transferPromotion = new TransferPromotion();
                var transferPromotionItems = new List<TransferPromotionItem>();
                var transferPromotionFreeItems = new List<TransferPromotionFreeItem>();
                var transferPromotionCreditCardItems = new List<TransferCreditCardItem>();
                transferPromotion.BookingID = booking.ID;
                transferPromotion.MasterTransferPromotionID = quotationTransferPromotion.MasterTransferPromotionID;
                transferPromotion.TransferDiscount = quotationTransferPromotion.MasterPromotion?.TransferDiscount;
                transferPromotion.TransferPromotionNo = quotationTransferPromotion.MasterPromotion?.PromotionNo;
                transferPromotion.Remark = quotationTransferPromotion.Remark;
                transferPromotion.IsActive = false;

                foreach (var item in quotationTransferPromotionItems.Where(o => o.MainQuotationTransferPromotionID == null))
                {
                    var transferPromotionItem = new TransferPromotionItem();
                    transferPromotionItem.TransferPromotionID = transferPromotion.ID;
                    transferPromotionItem.PricePerUnit = item.MasterPromotionItem.PricePerUnit;
                    transferPromotionItem.Quantity = item.Quantity;
                    transferPromotionItem.MasterTransferPromotionItemID = item.MasterTransferPromotionItemID;
                    transferPromotionItem.QuotationTransferPromotionItemID = item.ID;
                    transferPromotionItem.TotalPrice = item.MasterPromotionItem.PricePerUnit * item.Quantity;
                    transferPromotionItems.Add(transferPromotionItem);
                    //List Sub
                    foreach (var item1 in quotationTransferPromotionItems.Where(o => o.MainQuotationTransferPromotionID == item.ID))
                    {
                        var transferPromotionItemSub = new TransferPromotionItem();
                        transferPromotionItemSub.MasterTransferPromotionItemID = transferPromotionItem.ID;
                        transferPromotionItemSub.TransferPromotionID = transferPromotion.ID;
                        transferPromotionItemSub.PricePerUnit = item.MasterPromotionItem.PricePerUnit;
                        transferPromotionItemSub.Quantity = item.Quantity;
                        transferPromotionItemSub.MasterTransferPromotionItemID = item.MasterTransferPromotionItemID;
                        transferPromotionItemSub.QuotationTransferPromotionItemID = item.ID;
                        transferPromotionItemSub.TotalPrice = item.MasterPromotionItem.PricePerUnit * item.Quantity;
                        transferPromotionItems.Add(transferPromotionItemSub);
                    }
                }

                foreach (var item in quotationTransferPromotionFreeItems)
                {
                    var transferPromotionFreeItem = new TransferPromotionFreeItem();
                    transferPromotionFreeItem.TransferPromotionID = transferPromotion.ID;
                    transferPromotionFreeItem.QuotationTransferPromotionFreeItemID = item.ID;
                    transferPromotionFreeItem.MasterTransferPromotionFreeItemID = item.MasterTransferPromotionFreeItemID;
                    transferPromotionFreeItem.Quantity = item.Quantity;
                    transferPromotionFreeItems.Add(transferPromotionFreeItem);
                }

                foreach (var item in quotationTransferPromotionCreditCardItem)
                {
                    var transferCreditCardItem = new TransferCreditCardItem();
                    transferCreditCardItem.MasterTransferCreditCardItemID = item.MasterTransferCreditCardItemID;
                    transferCreditCardItem.QuotationTransferCreditCardItemID = item.QuotationTransferPromotionID;
                    transferCreditCardItem.TransferPromotionID = transferPromotion.ID;
                    transferCreditCardItem.Fee = item.MasterTransferCreditCardItem.Fee;
                    transferPromotionCreditCardItems.Add(transferCreditCardItem);
                }
                await DB.TransferPromotions.AddAsync(transferPromotion);
                await DB.TransferPromotionItems.AddRangeAsync(transferPromotionItems);
                await DB.TransferPromotionFreeItems.AddRangeAsync(transferPromotionFreeItems);
                await DB.TransferCreditCardItems.AddRangeAsync(transferPromotionCreditCardItems);
            }
            
            #endregion

            #endregion

            // เปลี่ยนสถานะของ แปลง
            var unitStatusWatingConfirmBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.WaitingForConfirmBooking).Select(o => o.ID).FirstAsync();
            var unitStatusWatingBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus && o.Key == UnitStatusKeys.Available).Select(o => o.ID).FirstAsync();
            var unit = await DB.Units.Where(o => o.ID == quotation.UnitID).Include(o => o.UnitStatus).FirstOrDefaultAsync();
            if (unit.UnitStatusMasterCenterID == unitStatusWatingBookingMasterCenterID || unit.UnitStatusMasterCenterID == null)
            {
                unit.UnitStatusMasterCenterID = unitStatusWatingConfirmBookingMasterCenterID;
                DB.Entry(unit).State = EntityState.Modified;
            }
            var quotationBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.QuotationStatus && o.Key == QuotationStatusKeys.Booking).Select(o => o.ID).FirstAsync();
            quotation.QuotationStatusMasterCenterID = quotationBookingMasterCenterID;
            DB.Quotations.Update(quotation);
            await DB.Bookings.AddAsync(booking);
            await DB.UnitPrices.AddAsync(unitPrice);
            await DB.UnitPriceItems.AddRangeAsync(unitPriceItems);
            await DB.UnitPriceInstallments.AddRangeAsync(unitPriceInstallments);
            await DB.BookingPromotions.AddAsync(bookingPromotion);
            await DB.BookingPromotionItems.AddRangeAsync(bookingPromotionItems);
            await DB.BookingPromotionFreeItems.AddRangeAsync(bookingPromotionFreeItems);
            await DB.BookingCreditCardItems.AddRangeAsync(bookingPromotionCreditCardItems);
            await DB.BookingPromotionExpenses.AddRangeAsync(bookingPromotionExpenses);
            await DB.PreSalePromotions.AddAsync(preSalePromotion);
            await DB.PreSalePromotionItems.AddRangeAsync(preSalePromotionItems);
            
            await DB.SaveChangesAsync();

            if (isPriceListChange.Result)
            {
                var priceListWorkflowStageBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PriceListWorkflowStage" && o.Key == "1").Select(o => o.ID).FirstAsync();
                var priceListWorkflow = await PriceListWorkflowService.CreatePriceListWorkflowAsync(quotation.ID, booking.ID, priceListWorkflowStageBookingMasterCenterID);
                await DB.PriceListWorkflows.AddAsync(priceListWorkflow);
                await DB.SaveChangesAsync();
            }

            var bookingResult = await BookingDTO.CreateFromModelAsync(booking, DB);
            return bookingResult;
        }

        public async Task<ReportResult> GetPrintQuotationUrlAsync(Guid quotationID)
        {
            var qt = await DB.Quotations.Include(o => o.Project).ThenInclude(o => o.ProductType).Include(o => o.Unit)
                .FirstAsync(o => o.ID == quotationID);
            ReportFactory reportFactory = new ReportFactory(Configuration,  ReportFolder.LC, "PF_LC_008N");
            //string reportUrl = null;
            if (qt.Project.ProductType.Key == ProductTypeKeys.LowRise)
            {
                reportFactory.AddParameter("QuotationNo", qt.QuotationNo);
            }
            else if (qt.Project.ProductType.Key == ProductTypeKeys.HighRise)
            {
                string floorPlan = await FileHelper.GetFileUrlAsync("projects", qt.Unit.FloorPlanFileName);
                string roomPlan = await FileHelper.GetFileUrlAsync("projects", qt.Unit.RoomPlanFileName);
                reportFactory = new ReportFactory(Configuration, ReportFolder.LC, "PF_LC_009N");
                reportFactory.AddParameter("QuotationNo", qt.QuotationNo);
                reportFactory.AddParameter("FloorPlan", floorPlan);
                reportFactory.AddParameter("RoomPlan", roomPlan);
            }

            return reportFactory.CreateUrl();

            //var qt = await DB.Quotations.Include(o => o.Project).ThenInclude(o => o.ProductType).Include(o => o.Unit)
            //    .FirstAsync(o => o.ID == quotationID);
            //ReportFactory reportFactory = new ReportFactory(Configuration["Report:Url"], Configuration["Report:SecretKey"]);
            //string reportUrl = null;
            //if (qt.Project.ProductType.Key == ProductTypeKeys.LowRise)
            //{
            //    reportUrl = reportFactory.CreateUrl<PF_LC_008N>(new PF_LC_008N()
            //    {
            //        QuotationNo = qt.QuotationNo
            //    });
            //}
            //else if (qt.Project.ProductType.Key == ProductTypeKeys.HighRise)
            //{
            //    string floorPlan = await FileHelper.GetFileUrlAsync("projects", qt.Unit.FloorPlanFileName);
            //    string roomPlan = await FileHelper.GetFileUrlAsync("projects", qt.Unit.RoomPlanFileName);
            //    reportUrl = reportFactory.CreateUrl<PF_LC_009N>(new PF_LC_009N()
            //    {
            //        QuotationNo = qt.QuotationNo,
            //        FloorPlan = floorPlan,
            //        RoomPlan = roomPlan
            //    });
            //}

            //return new StringResult()
            //{
            //    Result = reportUrl
            //};
        }

        private async Task<BooleanResult> IsPriceListChangedAsync(Guid quotationID)
        {
            var result = new BooleanResult();
            var quotation = await DB.Quotations.Where(o => o.ID == quotationID).Include(o => o.Unit).FirstOrDefaultAsync();
            var quotationUnitPrice = await DB.QuotationUnitPrices.Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();
            var quotationUnitPriceItems = await DB.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == quotationUnitPrice.ID).ToListAsync();

            var priceList = await DB.PriceLists.Where(o => o.ID == quotationUnitPrice.FromPriceListID).FirstOrDefaultAsync();
            var priceListItems = await DB.PriceListItems.Where(o => o.PriceListID == priceList.ID).Include(o => o.MasterPriceItem).ToListAsync();

            var masterSellingPrice = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            //var masterNetSellingPrice = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            var masterBookingAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var masterContractAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var masterDownAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            //var masterTransferAmount = masterNetSellingPrice - masterBookingAmount - masterContractAmount - masterDownAmount;
            var masterInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var masterInstallmentNormalCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                                - (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                    ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));
            var masterSpecialInstallmentAmount = priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();

            var masterSpecialInstallmentCount = (priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                ? priceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);

            var quotationSellingPrice = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();
            //var quotationNetSellingPrice = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
            var quotationBookingAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.BookingAmount).Select(o => o.Amount).FirstOrDefault();
            var quotationContractAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.ContractAmount).Select(o => o.Amount).FirstOrDefault();
            var quotationDownAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Amount).FirstOrDefault();
            //var quotationTransferAmount = quotationNetSellingPrice - quotationBookingAmount - quotationContractAmount - quotationDownAmount;
            var quotationInstallmentAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.InstallmentAmount).FirstOrDefault();
            var quotationInstallmentNormalCount = (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.Installment).FirstOrDefault()
                                                   - (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                                                       ? quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0));
            var quotationSpecialInstallmentAmount = quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallmentAmounts).FirstOrDefault();

            var quotationSpecialInstallmentCount = (quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments).FirstOrDefault() != null
                ? quotationUnitPriceItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.DownAmount).Select(o => o.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0);

            var listMasterSpecial = new List<Decimal>();
            var listQuotationSpecial = new List<Decimal>();
            if (!string.IsNullOrEmpty(masterSpecialInstallmentAmount))
            {
                listMasterSpecial = masterSpecialInstallmentAmount.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o))).ToList();
                masterSpecialInstallmentAmount = String.Join(",", listMasterSpecial);
            }
            if (!string.IsNullOrEmpty(quotationSpecialInstallmentAmount))
            {
                listQuotationSpecial = quotationSpecialInstallmentAmount.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o))).ToList();
                quotationSpecialInstallmentAmount = String.Join(",", listQuotationSpecial);
            }
            if (masterSellingPrice != quotationSellingPrice
                || masterBookingAmount != quotationBookingAmount
                || masterContractAmount != quotationContractAmount
                || masterDownAmount != quotationDownAmount
                || masterSpecialInstallmentAmount != quotationSpecialInstallmentAmount
                || masterInstallmentNormalCount != quotationInstallmentNormalCount
                || masterSpecialInstallmentCount != quotationSpecialInstallmentCount
            )
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }
    }
}