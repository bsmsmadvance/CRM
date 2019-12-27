using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Promotion.Params.Outputs;
using Promotion.Services.IService;
using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Services.Service
{
    public class TransferPromotionService : ITransferPromotionService
    {
        private readonly DatabaseContext DB;

        public TransferPromotionService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<TransferPromotionDTO> GetTransferPromotionDataAsync(Guid transferPromotionId)
        {
            var model = await DB.TransferPromotions
                    .Include(o => o.Booking)
                    .ThenInclude(o => o.Unit)
                    .ThenInclude(o => o.Project)
                    //.ThenInclude(o => o.Unit)
                    .Include(o => o.MasterPromotion)
                    .Where(o => o.ID == transferPromotionId).FirstOrDefaultAsync();

            var result = await TransferPromotionDTO.CreateFromModelAsync(model, DB);

            return result;
        }

        public async Task<TransferPromotionDTO> GetTransferPromotionDrafDataAsync(Guid agreementId)
        {
            var model = await DB.Agreements
                  .Include(o => o.Booking)
                  .ThenInclude(o => o.Project)
                  //.ThenInclude(o => o.Unit)
                  .Where(o => o.ID == agreementId).FirstOrDefaultAsync();

            var result = await TransferPromotionDTO.CreateFromModelDrafAsync(model.Booking, DB);

            return result;
        }

        public async Task<List<TransferPromotionItemDTO>> GetTransferPromotionItemListAsync(Guid transferPromotionId)
        {
            var result = new List<TransferPromotionItemDTO>();

            var itemModels = await DB.TransferPromotionItems
                .Include(o => o.MasterPromotionItem)
                .Where(o => o.TransferPromotionID == transferPromotionId).ToListAsync();


            if (itemModels.Count > 0)
            {
                var items = itemModels.Select(o => TransferPromotionItemDTO.CreateFromModel(o, null, null, DB)).ToList();
                result.AddRange(items);
            }

            var freeItemModels = await DB.TransferPromotionFreeItems.Where(o => o.TransferPromotionID == transferPromotionId).ToListAsync();
            if (freeItemModels.Count > 0)
            {
                var items = freeItemModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, o, null, DB)).ToList();
                result.AddRange(items);
            }

            var creditModels = await DB.TransferCreditCardItems.Where(o => o.TransferPromotionID == transferPromotionId).ToListAsync();
            if (creditModels.Count > 0)
            {
                var items = creditModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, null, o, DB)).ToList();
                result.AddRange(items);
            }

            return result;
        }

        public async Task<List<TransferPromotionExpenseDTO>> GetTransferPromotionExpenseListAsync(Guid transferPromotionId)
        {
            var result = new List<TransferPromotionExpenseDTO>();

            var itemModels = await DB.TransferPromotionExpenses
                                    .Include(o => o.TransferPromotion)
                                    .Include(o => o.MasterPriceItem)
                                    .Where(o => o.TransferPromotionID == transferPromotionId)
                                    .ToListAsync();

            if (itemModels.Count > 0)
            {

                var BookingID = itemModels[0].TransferPromotion.BookingID;

                var unitPriceModel = await DB.UnitPrices
                     .Include(o => o.Booking)
                     .ThenInclude(o => o.ReferContact)
                     .Include(o => o.UnitPriceStage)
                     .Where(o => o.BookingID == BookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.Transfer).FirstOrDefaultAsync() ?? new Database.Models.SAL.UnitPrice();

                var unitPriceID = unitPriceModel.ID;

                return itemModels.Select(async o => await TransferPromotionExpenseDTO.CreateFromModelAsync(o, unitPriceID, DB)).Select(o => o.Result).ToList();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<TransferPromotionExpenseDTO>> GetTransferPromotionExpensesDraftAsync(Guid agreementId)
        {
            var result = await TransferPromotionExpenseDTO.CreateDraftFromUnitAsync(agreementId, DB);

            return result;
        }

        public async Task<TransferPromotionDTO> CreateTransferPromotionDataAsync(TransferPromotionDTO input, List<TransferPromotionExpenseDTO> expenses)
        {
            var unit = await DB.Units
               .Include(o => o.Project)
               .ThenInclude(o => o.ProductType)
               .Where(o => o.ID == input.Booking.Unit.Id).FirstAsync();

            #region Unit Price
            var unitPriceModel = new UnitPrice()
            {
                BookingID = input.Booking.Id.Value,
                UnitPriceStageMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitPriceStage && o.Key == UnitPriceStageKeys.Transfer).Select(o => o.ID).FirstAsync(),
                IsActive = true
            };
            await DB.UnitPrices.AddAsync(unitPriceModel);

            var orderItem = 1;

            #region TransferDiscount
            var transferDiscount = new UnitPriceItem()
            {
                UnitPriceID = unitPriceModel.ID,
                Order = orderItem,
                Name = await DB.MasterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferDiscount).Select(o => o.Detail).FirstAsync(),
                Amount = input.TransferDiscount,
                IsToBePay = false,
                PricePerUnitAmount = input.TransferDiscount,
                PriceUnitAmount = 1,
                PriceTypeMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "2").Select(o => o.ID).FirstAsync(),
                PriceUnitMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "1").Select(o => o.ID).FirstAsync(),
                MasterPriceItemID = MasterPriceItemKeys.TransferDiscount,
            };

            await DB.UnitPriceItems.AddAsync(transferDiscount);
            orderItem = orderItem + 1;
            #endregion

            await DB.SaveChangesAsync();
            #endregion

            #region TransferPromotion

            TransferPromotion model = new TransferPromotion();
            input.ToModel(ref model);
            await DB.TransferPromotions.AddAsync(model);
            await DB.SaveChangesAsync();

            #endregion


            #region "Validate ส่วนลด ณ วันโอน ต้องไม่เกินที่กำหนดไว้ในโปรโมชั่นโอน"
            var masterPromotion = await DB.MasterTransferPromotions.Where(o => o.PromotionNo == input.PromotionNo).FirstAsync();
            if (input.TransferDiscount > masterPromotion.TransferDiscount)
            {
                ValidateException ex = new ValidateException();
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0103").FirstAsync();
                var msg = errMsg.Message;
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                throw ex;
            }
            #endregion

            #region TransferPromotionItem

            if (input != null)
            {
                #region Item
                var itemPromotion = input.Items.Where(o => o.ItemType == PromotionItemType.Item && o.IsSelected).ToList();
                foreach (var item in itemPromotion)
                {
                    var promotionItemModel = new TransferPromotionItem()
                    {
                        TransferPromotionID = model.ID,
                        Quantity = item.Quantity,
                        MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.TransferPromotionItems.AddAsync(promotionItemModel);

                    List<TransferPromotionItem> subItems = new List<TransferPromotionItem>();
                    foreach (var sub in item.SubItems)
                    {
                        var promotionSubItemModel = new TransferPromotionItem()
                        {
                            TransferPromotionID = model.ID,
                            Quantity = item.Quantity,
                            MainTransferPromotionItemID = promotionItemModel.ID,
                            MasterTransferPromotionItemID = item.FromMasterTansferPromotionItemID
                        };

                        subItems.Add(promotionSubItemModel);
                    }

                    if (subItems.Count >= 0)
                    {
                        await DB.TransferPromotionItems.AddRangeAsync(subItems);
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Free
                var freeItemPromotion = input.Items.Where(o => o.ItemType == PromotionItemType.FreeItem && o.IsSelected).ToList();
                foreach (var item in freeItemPromotion)
                {
                    var promotionItemModel = new TransferPromotionFreeItem()
                    {
                        TransferPromotionID = model.ID,
                        Quantity = item.Quantity,
                        MasterTransferPromotionFreeItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.TransferPromotionFreeItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Credit
                var creditItemPromotion = input.Items.Where(o => o.ItemType == PromotionItemType.CreditCard && o.IsSelected).ToList();
                foreach (var item in creditItemPromotion)
                {
                    var promotionItemModel = new TransferCreditCardItem()
                    {
                        TransferPromotionID = model.ID,
                        MasterTransferCreditCardItemID = item.FromMasterTansferPromotionItemID
                    };
                    await DB.TransferCreditCardItems.AddAsync(promotionItemModel);
                }

                await DB.SaveChangesAsync();
                #endregion
            }

            #endregion

            #region TransferPromotionExpense

            #region Expense
            if (unit.Project.ProductType.Key == "1")
            {
                #region Water
                var waterMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.WaterMeter).FirstOrDefault();
                if (waterMeter != null)
                {
                    var waterModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(waterModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Eletrict
                var eletrictMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.EletrictMeter).FirstOrDefault();
                if (eletrictMeter != null)
                {
                    var eletrictModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(eletrictModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Mortgage
                var mortgageMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageMeter != null)
                {
                    var mortgageModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(mortgageModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Transfer
                var transferMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferMeter != null)
                {
                    var transferModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(transferModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Common
                var commonMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (commonMeter != null)
                {
                    var commonModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(commonModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Document
                var documentMeter = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentMeter != null)
                {
                    var documentModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(documentModel);
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
                    var eletrictModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(eletrictModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Mortgage
                var mortgageFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.MortgageFee).FirstOrDefault();
                if (mortgageFee != null)
                {
                    var mortgageModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(mortgageModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Transfer
                var transferFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.TransferFee).FirstOrDefault();
                if (transferFee != null)
                {
                    var transferModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(transferModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Common
                var CommonFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.CommonFee).FirstOrDefault();
                if (CommonFee != null)
                {
                    var commonModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(commonModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region Document
                var documentFee = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.DocumentFee).FirstOrDefault();
                if (documentFee != null)
                {
                    var documentModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(documentModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                #region First Sinking
                var first = expenses.Where(o => o.MasterPriceItem.Id == MasterPriceItemKeys.FirstSinkingFund).FirstOrDefault();
                if (first != null)
                {
                    var firstModel = new UnitPriceItem()
                    {
                        UnitPriceID = unitPriceModel.ID,
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

                    await DB.UnitPriceItems.AddAsync(firstModel);
                    orderItem = orderItem + 1;
                }
                #endregion

                await DB.SaveChangesAsync();
            }

            foreach (var expense in expenses)
            {
                TransferPromotionExpense expenseModel = new TransferPromotionExpense()
                {
                    TransferPromotionID = model.ID,
                    ExpenseReponsibleByMasterCenterID = expense.ExpenseReponsibleBy.Id,
                    MasterPriceItemID = expense.MasterPriceItem.Id,
                    Amount = expense.Amount,
                    PaymentReceiverMasterCenterID = expense.PaymentReceiverMasterCenterID    /////////..........xxxxxx
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

                await DB.TransferPromotionExpenses.AddAsync(expenseModel);
                await DB.SaveChangesAsync();
                
            }
            #endregion

            #endregion

            var result = await TransferPromotionDTO.CreateFromModelAsync(model, DB);
            return result?? new TransferPromotionDTO();
        }

        public async Task<TransferPromotionDTO> UpdateAllowTransferDiscountAsync(Guid transferPromotionId, TransferPromotionDTO input)
        {
            //await input.ValidateAsync(DB);

            var model = await DB.TransferPromotions.Where(o => o.ID == transferPromotionId).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await TransferPromotionDTO.CreateFromModelAsync(model, DB);
            return result?? new TransferPromotionDTO();
        }

        public async Task<TransferPromotionDTO> UpdateAllowTransferDiscountOver3PercentAsync(Guid transferPromotionId, TransferPromotionDTO input)
        {
            //await input.ValidateAsync(DB);

            var model = await DB.TransferPromotions.Where(o => o.ID == transferPromotionId).FirstAsync();
            input.ToModel(ref model);

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var result = await TransferPromotionDTO.CreateFromModelAsync(model, DB);
            return result ?? new TransferPromotionDTO();
        }

        public async Task<BooleanResult> IsWaitingMinPriceApproveAsync(Guid transferPromotionId)
        {
            var bookingID = await DB.TransferPromotions
                                .Where(o => o.ID == transferPromotionId)
                                .Select(o => o.BookingID).FirstOrDefaultAsync();

            var minPriceBudget = await DB.MinPriceBudgetWorkflows
                                        .Include(o => o.MinPriceBudgetWorkflowStage)
                                        .Where(o => o.BookingID == bookingID
                                                && o.IsRecalled == false
                                                && o.IsApproved == null
                                                && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.PromotionTransfer).FirstOrDefaultAsync();

            var result = new BooleanResult
            {
                Result = (minPriceBudget != null) ? true : false
            };

            return result;
        }
        //BookingDTO booking, BookingPriceListDTO priceList, 
        public async Task<MinPriceBudgetWorkflowTypeDTO> IsMinPriceChangedAsync(TransferPromotionDTO transferPromotion, List<TransferPromotionExpenseDTO> expenses)
        {
            var result = new MinPriceBudgetWorkflowTypeDTO();
            result.IsMinPriceWorkflow = false;
            result.IsBudgetPromotionWorkflow = false;

            var bookingID = await DB.TransferPromotions
                               .Where(o => o.ID == transferPromotion.Id)
                               .Select(o => o.BookingID).FirstOrDefaultAsync();
            var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();

            #region Promotion            
            var totalPromotionPrice = await DB.BookingPromotions.Where(o => o.BookingID == bookingID).Select(o => o.TotalAmount).FirstOrDefaultAsync();

            var query = await DB.BudgetPromotions
                .Include(o => o.UpdatedBy)
                .Where(o => o.ProjectID == booking.ProjectID && o.UnitID == booking.UnitID)
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

            //var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();
            var bugetPromotion = temp.Select(o => new BudgetPromotionQueryResult
            {
                Unit = o.Unit,
                //BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).FirstOrDefault();

            //ตรวจสอบ budget โอน
            var isWaitingPromotion = (transferPromotion.BudgetAmount > bugetPromotion.BudgetPromotionTransfer.Budget) ? true : false;
            if (isWaitingPromotion)
            {
                result.IsBudgetPromotionWorkflow = true;
            }
            #endregion

            #region Minimum Price
            var unitPriceModel = await DB.UnitPrices
               //.Include(o => o.Booking)
               //.ThenInclude(o => o.ReferContact)
               .Include(o => o.UnitPriceStage)
               .Where(o => o.BookingID == bookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                var unitPriceItemModel = await DB.UnitPriceItems.Where(o => o.UnitPriceID == unitPriceModel.ID).ToListAsync();
                var priceList = new BookingPriceListDTO();
                priceList.SellingPrice = unitPriceItemModel.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.SellPrice).Select(o => o.Amount).FirstOrDefault();

                var minPrice = await DB.MinPrices.Where(o => o.ActiveDate <= DateTime.Now && o.UnitID == booking.UnitID).OrderByDescending(o => o.ActiveDate).FirstAsync();
                var minPriceValue = (minPrice.ROIMinprice != null || minPrice.ROIMinprice != 0) ? minPrice.ROIMinprice : minPrice.ApprovedMinPrice;
                var minPricePercent = minPriceValue * 0.05M;

                var today = DateTime.Now;
                var budgetMinPrice = await DB.BudgetMinPrices
                    .Include(o => o.BudgetMinPriceType)
                    .Where(o => o.ProjectID == booking.ProjectID && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly && o.ActiveDate <= today && o.Year == today.Year && o.Quarter == today.GetQuarter())
                    .OrderByDescending(o => o.ActiveDate).FirstOrDefaultAsync();
                if (budgetMinPrice == null)
                {
                    var bookingUnit = await DB.Bookings.Where(o => o.ID == booking.ID).Include(o => o.Unit).FirstOrDefaultAsync();
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
                        .Where(o => o.ProjectID == booking.ProjectID
                                    && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                    && o.ActiveDate <= today
                                    && o.Year == today.Year
                                    && o.Quarter == today.GetQuarter())
                        .OrderByDescending(o => o.ActiveDate)
                        .FirstOrDefaultAsync();
                }

                var budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.BudgetMinPriceID == budgetMinPrice.ID && o.UnitID == booking.UnitID).FirstOrDefaultAsync();
                if (budgetMinPriceUnit == null)
                {
                    var bookingUnit = await DB.Bookings.Where(o => o.ID == booking.ID).Include(o => o.Unit).FirstOrDefaultAsync();
                    var newBudgetMinPriceUnit = new BudgetMinPriceUnit();
                    newBudgetMinPriceUnit.Amount = 0;
                    newBudgetMinPriceUnit.BudgetMinPriceID = budgetMinPrice.ID;
                    newBudgetMinPriceUnit.UnitID = bookingUnit.UnitID;
                    await DB.BudgetMinPriceUnits.AddAsync(newBudgetMinPriceUnit);
                    await DB.SaveChangesAsync();

                    budgetMinPriceUnit = await DB.BudgetMinPriceUnits.Where(o => o.ID == newBudgetMinPriceUnit.ID).FirstAsync();
                }

                var totalMinprice = minPriceValue - (priceList.SellingPrice - (priceList.TransferDiscount + transferPromotion.TotalAmount));

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
            }
            #endregion

            return result;
        }

    }
}
