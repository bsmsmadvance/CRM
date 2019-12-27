using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoPromotionExpenseDTO : BaseDTO
    {
        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ExpenseReponsibleBy
        /// </summary>
        public MST.MasterCenterDropdownDTO ExpenseReponsibleBy { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวน (หน่วย)
        /// </summary>
        public double? PriceUnitAmount { get; set; }
        /// <summary>
        /// หน่วย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PriceUnit
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceUnit { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal? PricePerUnitAmount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย
        /// </summary>
        public decimal BuyerPayAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย
        /// </summary>
        public decimal SellerPayAmount { get; set; }
        /// <summary>
        /// รายการชำระเงิน
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        public async static Task<List<UnitInfoPromotionExpenseDTO>> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            var unit = await db.Units
                .Include(o => o.Project)
                .Include(o => o.Project.ProductType)
                .Include(o => o.WaterMeterPrice)
                .Where(o => o.ID == unitID).FirstAsync();

            var masterPriceItems = await db.MasterPriceItems.ToListAsync();
            var priceUnitMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit).ToListAsync();
            var reponsibleByCustomerMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Customer).FirstAsync();
            var reponsibleByHalfMasterCenters = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == ExpenseReponsibleByKeys.Half).FirstAsync();
            var priceList = await db.PriceLists.GetActivePriceListAsync(unitID);
            if (priceList == null)
                return null;

            var results = new List<UnitInfoPromotionExpenseDTO>();
            var order = 0;

            switch (unit.Project.ProductType.Key)
            {
                case "1":
                    {
                        var waterMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.WaterMeter).First();
                        var electricMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.EletrictMeter).First();
                        var mortgageMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.MortgageFee).First();
                        var transferMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferFee).First();
                        var commonMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.CommonFee).First();
                        var documentMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.DocumentFee).First();

                        #region Meter
                        if (unit.WaterMeterPriceID != null)
                        {
                            var water = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice : 0,
                                Amount = (unit.WaterMeterPrice.WaterMeterPrice != null) ? unit.WaterMeterPrice.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(water);
                        }
                        else
                        {
                            var latestWater = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var water = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = waterMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice : 0,
                                Amount = (latestWater.WaterMeterPrice != null) ? latestWater.WaterMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(waterMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(water);
                        }

                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                        var mortgage = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = mortgageMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                            PricePerUnitAmount = netSellingPrice,
                            Amount = (netSellingPrice / 100),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(mortgage);
                        #endregion

                        #region Transfer
                        var titledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == unit.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();
                        var estimatePriceArea = await db.LowRiseFees.Where(o => o.UnitID == unit.ID).Select(o => o.EstimatePriceArea).FirstOrDefaultAsync();
                        var usedArea = unit.UsedArea;
                        var price = await db.LowRiseBuildingPriceFees.Where(o => o.UnitID == unit.ID).Select(o => o.Price).FirstOrDefaultAsync();
                        var bo = await db.BOConfigurations.FirstOrDefaultAsync();
                        var depreciationYear = 0M;
                        if (bo != null)
                        {
                            if (bo.DepreciationYear != null)
                            {
                                depreciationYear = (decimal)bo.DepreciationYear;
                            }
                        }

                        var titledeedCal = (decimal)((titledeedArea != null) ? titledeedArea : 0) * ((estimatePriceArea != null) ? estimatePriceArea : 0);
                        var usedAreaCal = (decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0);
                        var depreciationCal = ((depreciationYear != 0) ? depreciationYear / 100 : 0) * ((decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0));
                        var total = titledeedCal + usedAreaCal - depreciationCal;

                        var transfer = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = transferMaster.Detail,
                            PriceUnitAmount = 2,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                            PricePerUnitAmount = total.Value,
                            Amount = total.Value * 0.02M,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                        };

                        results.Add(transfer);
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "3").FirstOrDefault()),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(document);
                        #endregion

                        break;
                    }
                case "2":
                    {
                        var electricMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.EletrictMeter).First();
                        var mortgageMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.MortgageFee).First();
                        var transferMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.TransferFee).First();
                        var commonMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.CommonFee).First();
                        var firstSinkingMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.FirstSinkingFund).First();
                        var documentMaster = masterPriceItems.Where(o => o.ID == MasterPriceItemKeys.DocumentFee).First();

                        #region Meter
                        if (unit.ElectricMeterPriceID != null)
                        {
                            var electric = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice : 0,
                                Amount = (unit.ElectricMeterPrice.ElectricMeterPrice != null) ? unit.ElectricMeterPrice.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        else
                        {
                            var latestElectric = await db.WaterElectricMeterPrices.Where(o => o.ModelID == unit.ModelID).OrderByDescending(o => o.Version).FirstOrDefaultAsync();
                            var electric = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = electricMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                                PricePerUnitAmount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice : 0,
                                Amount = (latestElectric.ElectricMeterPrice != null) ? latestElectric.ElectricMeterPrice.Value : 0,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(electricMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(electric);
                        }
                        #endregion

                        #region Mortgage
                        var netSellingPrice = priceList.PriceListItems.Where(o => o.MasterPriceItemID == MasterPriceItemKeys.NetSellPrice).Select(o => o.Amount).FirstOrDefault();
                        if (netSellingPrice >= 1000000)
                        {
                            var mortgage = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = (netSellingPrice / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            results.Add(mortgage);
                        }
                        else
                        {
                            var mortgage = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = mortgageMaster.Detail,
                                PriceUnitAmount = 1,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                                PricePerUnitAmount = netSellingPrice,
                                Amount = netSellingPrice * (0.01M / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(mortgageMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };
                            results.Add(mortgage);
                        }
                        #endregion

                        #region Transfer
                        var titledeedArea = await db.TitledeedDetails.Where(o => o.UnitID == unit.ID).Select(o => o.TitledeedArea).FirstOrDefaultAsync();
                        var estimatePriceArea = await db.LowRiseFees.Where(o => o.UnitID == unit.ID).Select(o => o.EstimatePriceArea).FirstOrDefaultAsync();
                        var usedArea = unit.UsedArea;
                        var price = await db.LowRiseBuildingPriceFees.Where(o => o.UnitID == unit.ID).Select(o => o.Price).FirstOrDefaultAsync();
                        var bo = await db.BOConfigurations.FirstOrDefaultAsync();
                        var depreciationYear = 0M;
                        if (bo != null)
                        {
                            if (bo.DepreciationYear != null)
                            {
                                depreciationYear = (decimal)bo.DepreciationYear;
                            }
                        }

                        var titledeedCal = (decimal)((titledeedArea != null) ? titledeedArea : 0) * ((estimatePriceArea != null) ? estimatePriceArea : 0);
                        var usedAreaCal = (decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0);
                        var depreciationCal = ((depreciationYear != 0) ? depreciationYear / 100 : 0) * ((decimal)((usedArea != null) ? usedArea : 0) * ((price != null) ? price : 0));
                        var total = titledeedCal + usedAreaCal - depreciationCal;

                        if (netSellingPrice >= 1000000)
                        {
                            var transfer = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * 0.02M,
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByHalfMasterCenters)
                            };

                            results.Add(transfer);
                        }
                        else
                        {
                            var transfer = new UnitInfoPromotionExpenseDTO()
                            {
                                Name = transferMaster.Detail,
                                PriceUnitAmount = 2,
                                PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "0").FirstOrDefault()),
                                PricePerUnitAmount = total.Value,
                                Amount = total.Value * (0.01M / 100),
                                MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(transferMaster),
                                Order = order++,
                                ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                            };

                            results.Add(transfer);
                        }
                        #endregion

                        #region Common
                        var agreeConfig = await db.AgreementConfigs.Where(o => o.ProjectID == unit.ProjectID).FirstOrDefaultAsync();

                        var common = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = commonMaster.Detail,
                            PriceUnitAmount = agreeConfig.PublicFundMonths,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "3").FirstOrDefault()),
                            PricePerUnitAmount = agreeConfig.PublicFundRate,
                            Amount = agreeConfig.PublicFundRate.Value * agreeConfig.PublicFundMonths.Value,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(commonMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(common);
                        #endregion

                        #region Document
                        var document = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = documentMaster.Detail,
                            PriceUnitAmount = 1,
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "1").FirstOrDefault()),
                            PricePerUnitAmount = 300,
                            Amount = 300,
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(documentMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(document);
                        #endregion

                        #region First Sinking
                        var first = new UnitInfoPromotionExpenseDTO()
                        {
                            Name = firstSinkingMaster.Detail,
                            PriceUnitAmount = ((usedArea != null) ? usedArea : 0),
                            PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(priceUnitMasterCenters.Where(o => o.Key == "4").FirstOrDefault()),
                            PricePerUnitAmount = agreeConfig.CondoFundRate,
                            Amount = agreeConfig.CondoFundRate.Value * (decimal)((usedArea != null) ? usedArea : 0),
                            MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(firstSinkingMaster),
                            Order = order++,
                            ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(reponsibleByCustomerMasterCenters)
                        };
                        results.Add(first);
                        #endregion

                        break;
                    }
            }

            return (results.Count > 0) ? results : null;
        }

        public async static Task<UnitInfoPromotionExpenseDTO> CreateFromModelAsync(BookingPromotionExpense model, DatabaseContext db)
        {
            if (model != null)
            {
                var unitPrice = await db.UnitPrices.Where(o => o.BookingID == model.BookingPromotion.BookingID).FirstAsync();
                var item = await db.UnitPriceItems.Include(o => o.PriceUnit).Where(o => o.UnitPriceID == unitPrice.ID && o.MasterPriceItemID == model.MasterPriceItemID).FirstAsync();
                UnitInfoPromotionExpenseDTO result = new UnitInfoPromotionExpenseDTO()
                {
                    Id = model.ID,
                    ExpenseReponsibleBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.ExpenseReponsibleBy),
                    Name = model.MasterPriceItem.Detail,
                    Amount = model.Amount,
                    BuyerPayAmount = model.BuyerAmount,
                    SellerPayAmount = model.SellerAmount,
                    MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem),
                    PriceUnitAmount = item.PriceUnitAmount,
                    PricePerUnitAmount = item.PricePerUnitAmount,
                    PriceUnit = MST.MasterCenterDropdownDTO.CreateFromModel(item.PriceUnit),
                    Order = item.Order,
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
