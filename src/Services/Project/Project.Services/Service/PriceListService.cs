using Base.DTOs;
using Base.DTOs.PRJ;
using Common;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using ErrorHandling;
using ExcelExtensions;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Project.Services.Excels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class PriceListService : IPriceListService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;
        private FileHelper FileHelper;

        public PriceListService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;

            var minioEndpoint = Configuration["Minio:Endpoint"];
            var minioAccessKey = Configuration["Minio:AccessKey"];
            var minioSecretKey = Configuration["Minio:SecretKey"];
            var minioBucketName = Configuration["Minio:DefaultBucket"];
            var minioTempBucketName = Configuration["Minio:TempBucket"];
            var minioWithSSL = Configuration["Minio:WithSSL"];

            this.FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, minioWithSSL == "true");
        }

        public async Task<PriceListPaging> GetPriceListsAsync(Guid projectID, PriceListFilter filter, PageParam pageParam, PriceListSortByParam sortByParam)
        {
            var allUnits = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var allUnitID = allUnits.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToList();
            var allPriceList = await DB.PriceLists.Include(o => o.UpdatedBy)
                              .Where(o => allUnitID.Contains(o.UnitID))
                              .GroupBy(o => o.UnitID)
                              .Select(o =>
                                 o.Where(p => p.ActiveDate <= DateTime.Now).Select(p => p).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
                             ).ToListAsync();
            var allPriceListID = allPriceList.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToList();
            var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.PriceListID))
                                                          .Include(o => o.MasterPriceItem)
                                                          .Include(o => o.PriceUnit)
                                                          .Include(o => o.PriceType)
                                                          .ToListAsync();

            var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();


            var tempData = (from unit in allUnits
                            join priceList in allPriceList on unit.ID equals priceList.UnitID into p
                            from pl in p.DefaultIfEmpty()
                            join titledeed in allTitleDeed on unit.ID equals titledeed.UnitID into t
                            from td in t.DefaultIfEmpty()
                            select new TempPriceListQueryResult
                            {
                                Unit = unit,
                                PriceList = pl,
                                Titledeed = td,
                                PriceListItems = allPriceListItem.Where(o => o.PriceListID == pl?.ID)
                                                    .Select(o => PriceListItemDTO.CreateFromModel(o))
                                                    .OrderBy(o => o.Order)
                                                    .ToList()
                            });

            var data = tempData.Select(o => new PriceListQueryResult
            {
                Unit = o.Unit,
                TitleDeedArea = o.Titledeed?.TitledeedArea,
                BookingAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.BookingAmount).FirstOrDefault(),
                TotalSalePrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.NetSellPrice).FirstOrDefault(),
                ContractAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ContractAmount).FirstOrDefault(),
                DownAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).FirstOrDefault(),
                SpecialDown = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallments).FirstOrDefault(),
                SpecialDownPrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallmentAmounts).FirstOrDefault(),
                DownPaymentPerPeriod = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.InstallmentAmount).FirstOrDefault(),
                PercentDownPayment = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() == 0 ? null : o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() * 100,
                OffsetAreaUnitPrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                OffsetArea = (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) == 0 ? null : (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea),
                OffsetAreaPrice = (Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault()) == 0 ? null : Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                PriceList = o.PriceList,
                UnitStatus = o.Unit.UnitStatus
            }).ToList();

            var results = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).ToList();

            #region Fitler
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                results = results.Where(x => x.UnitNo.Contains(filter.UnitNo)).ToList();
            }

            #region SaleArea
            if (filter.SaleAreaFrom != null)
            {
                results = results.Where(x => x.SaleArea >= filter.SaleAreaFrom).ToList();
            }
            if (filter.SaleAreaTo != null)
            {
                results = results.Where(x => x.SaleArea <= filter.SaleAreaTo).ToList();
            }
            if (filter.SaleAreaFrom != null && filter.SaleAreaTo != null)
            {
                results = results.Where(x => x.SaleArea >= filter.SaleAreaFrom && x.SaleArea <= filter.SaleAreaTo).ToList();
            }
            #endregion

            #region TitleDeedArea
            if (filter.TitleDeedAreaFrom != null)
            {
                results = results.Where(x => x.TitleDeedArea >= filter.TitleDeedAreaFrom).ToList();
            }
            if (filter.TitleDeedAreaTo != null)
            {
                results = results.Where(x => x.TitleDeedArea <= filter.TitleDeedAreaTo).ToList();
            }
            if (filter.TitleDeedAreaFrom != null && filter.TitleDeedAreaTo != null)
            {
                results = results.Where(x => x.TitleDeedArea >= filter.TitleDeedAreaFrom && x.TitleDeedArea <= filter.TitleDeedAreaTo).ToList();
            }
            #endregion

            #region OffsetArea
            if (filter.OffsetAreaFrom != null)
            {
                results = results.Where(x => x.OffsetArea >= filter.OffsetAreaFrom).ToList();
            }
            if (filter.OffsetAreaTo != null)
            {
                results = results.Where(x => x.OffsetArea <= filter.OffsetAreaTo).ToList();
            }
            if (filter.OffsetAreaFrom != null && filter.OffsetAreaTo != null)
            {
                results = results.Where(x => x.OffsetArea >= filter.OffsetAreaFrom && x.OffsetArea <= filter.OffsetAreaTo).ToList();
            }
            #endregion

            #region OffsetAreaUnitPrice
            if (filter.OffsetAreaUnitPriceFrom != null)
            {
                results = results.Where(x => x.OffsetAreaUnitPrice >= filter.OffsetAreaUnitPriceFrom).ToList();
            }
            if (filter.OffsetAreaUnitPriceTo != null)
            {
                results = results.Where(x => x.OffsetAreaUnitPrice <= filter.OffsetAreaUnitPriceTo).ToList();
            }
            if (filter.OffsetAreaUnitPriceFrom != null && filter.OffsetAreaUnitPriceTo != null)
            {
                results = results.Where(x => x.OffsetAreaUnitPrice >= filter.OffsetAreaUnitPriceFrom && x.OffsetAreaUnitPrice <= filter.OffsetAreaUnitPriceTo).ToList();
            }
            #endregion

            #region OffsetAreaPrice
            if (filter.OffsetAreaPriceFrom != null)
            {
                results = results.Where(x => x.OffsetAreaPrice >= filter.OffsetAreaPriceFrom).ToList();
            }
            if (filter.OffsetAreaPriceTo != null)
            {
                results = results.Where(x => x.OffsetAreaPrice <= filter.OffsetAreaPriceTo).ToList();
            }
            if (filter.OffsetAreaPriceFrom != null && filter.OffsetAreaPriceTo != null)
            {
                results = results.Where(x => x.OffsetAreaPrice >= filter.OffsetAreaPriceFrom && x.OffsetAreaPrice <= filter.OffsetAreaPriceTo).ToList();
            }
            #endregion

            #region TotalSalePrice
            if (filter.TotalSalePriceFrom != null)
            {
                results = results.Where(x => x.TotalSalePrice >= filter.TotalSalePriceFrom).ToList();
            }
            if (filter.TotalSalePriceTo != null)
            {
                results = results.Where(x => x.TotalSalePrice <= filter.TotalSalePriceTo).ToList();
            }
            if (filter.TotalSalePriceFrom != null && filter.TotalSalePriceTo != null)
            {
                results = results.Where(x => x.TotalSalePrice >= filter.TotalSalePriceFrom && x.TotalSalePrice <= filter.TotalSalePriceTo).ToList();
            }
            #endregion

            #region PercentDownPayment
            if (filter.PercentDownPaymentFrom != null)
            {
                results = results.Where(x => x.PercentDownPayment >= filter.PercentDownPaymentFrom).ToList();
            }
            if (filter.PercentDownPaymentTo != null)
            {
                results = results.Where(x => x.PercentDownPayment <= filter.PercentDownPaymentTo).ToList();
            }
            if (filter.PercentDownPaymentFrom != null && filter.PercentDownPaymentTo != null)
            {
                results = results.Where(x => x.PercentDownPayment >= filter.PercentDownPaymentFrom && x.PercentDownPayment <= filter.PercentDownPaymentTo).ToList();
            }
            #endregion

            #region BookingAmount
            if (filter.BookingAmountFrom != null)
            {
                results = results.Where(x => x.BookingAmount >= filter.BookingAmountFrom).ToList();
            }
            if (filter.BookingAmountTo != null)
            {
                results = results.Where(x => x.BookingAmount <= filter.BookingAmountTo).ToList();
            }
            if (filter.BookingAmountFrom != null && filter.BookingAmountTo != null)
            {
                results = results.Where(x => x.BookingAmount >= filter.BookingAmountFrom && x.BookingAmount <= filter.BookingAmountTo).ToList();
            }
            #endregion

            #region ContractAmount
            if (filter.ContractAmountFrom != null)
            {
                results = results.Where(x => x.ContractAmount >= filter.ContractAmountFrom).ToList();
            }
            if (filter.ContractAmountTo != null)
            {
                results = results.Where(x => x.ContractAmount <= filter.ContractAmountTo).ToList();
            }
            if (filter.ContractAmountFrom != null && filter.ContractAmountTo != null)
            {
                results = results.Where(x => x.ContractAmount >= filter.ContractAmountFrom
                                    && x.ContractAmount <= filter.ContractAmountTo).ToList();
            }
            #endregion

            #region DownPaymentPeriod
            if (filter.DownPaymentPeriodFrom != null)
            {
                results = results.Where(x => x.DownAmount >= Convert.ToDecimal(filter.DownPaymentPeriodFrom)).ToList();
            }
            if (filter.DownPaymentPeriodTo != null)
            {
                results = results.Where(x => x.DownAmount <= Convert.ToDecimal(filter.DownPaymentPeriodTo)).ToList();
            }
            if (filter.DownPaymentPeriodFrom != null && filter.DownPaymentPeriodTo != null)
            {
                results = results.Where(x => x.DownAmount >= Convert.ToDecimal(filter.DownPaymentPeriodFrom)
                                    && x.DownAmount <= Convert.ToDecimal(filter.DownPaymentPeriodTo)).ToList();
            }
            #endregion

            #region DownPaymentPerPeriod
            if (filter.DownPaymentPerPeriodFrom != null)
            {
                results = results.Where(x => x.DownPaymentPerPeriod >= filter.DownPaymentPerPeriodFrom).ToList();
            }
            if (filter.DownPaymentPerPeriodTo != null)
            {
                results = results.Where(x => x.DownPaymentPerPeriod <= filter.DownPaymentPerPeriodTo).ToList();
            }
            if (filter.DownPaymentPerPeriodFrom != null && filter.DownPaymentPerPeriodTo != null)
            {
                results = results.Where(x => x.DownPaymentPerPeriod >= filter.DownPaymentPerPeriodFrom
                                    && x.DownPaymentPerPeriod <= filter.DownPaymentPerPeriodTo).ToList();
            }
            #endregion

            #region DownAmount
            if (filter.DownAmountFrom != null)
            {
                results = results.Where(x => x.DownAmount >= filter.DownAmountFrom).ToList();
            }
            if (filter.DownAmountTo != null)
            {
                results = results.Where(x => x.DownAmount <= filter.DownAmountTo).ToList();
            }
            if (filter.DownAmountFrom != null && filter.DownAmountTo != null)
            {
                results = results.Where(x => x.DownAmount >= filter.DownAmountFrom && x.DownAmount <= filter.DownAmountTo).ToList();
            }
            #endregion
            if (!string.IsNullOrEmpty(filter.SpecialDown))
            {
                results = results.Where(x => x.SpecialDown != null && x.SpecialDown.Contains(filter.SpecialDown)).ToList();
            }

            #region SpecialDownPrice

            if (!string.IsNullOrEmpty(filter.SpecialDownPrice))
            {
                results = results.Where(x => x.SpecialDownPrice != null && x.SpecialDownPrice.Contains(filter.SpecialDownPrice)).ToList();
            }

            #endregion

            if (!string.IsNullOrEmpty(filter.UnitStatusKey))
            {
                var unitStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.UnitStatusKey
                                                                           && x.MasterCenterGroupKey == "UnitStatus")
                                                                          .Select(x => x.ID).FirstAsync();
                results = results.Where(x => x.UnitStatus?.Id == unitStatusMasterCenterID).ToList();
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                results = results.Where(x => x.UpdatedBy.Contains(filter.UpdatedBy)).ToList();
            }
            if (filter.UpdatedFrom != null)
            {
                results = results.Where(x => x.Updated >= filter.UpdatedFrom).ToList();
            }
            if (filter.UpdatedTo != null)
            {
                results = results.Where(x => x.Updated <= filter.UpdatedTo).ToList();
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                results = results.Where(x => x.Updated >= filter.UpdatedFrom && x.Updated <= filter.UpdatedTo).ToList();
            }

            #endregion

            PriceListDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<PriceListDTO>(pageParam, ref results);

            return new PriceListPaging()
            {
                PageOutput = pageOutput,
                PriceLists = results
            };
        }

        public async Task<PriceListDTO> CreatePriceListAsync(Guid projectID, PriceListDTO input)
        {
            var unit = await DB.Units.Where(o => o.UnitNo == input.UnitNo && o.ProjectID == projectID).FirstOrDefaultAsync();
            List<PriceListItem> listPriceListItem = new List<PriceListItem>();

            PriceList pl = new PriceList();
            pl.UnitID = unit.ID;
            pl.ActiveDate = DateTime.Now;

            var priceUnitPercentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync();
            var priceUnitItemMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "2").Select(o => o.ID).FirstAsync();
            var priceTypeHouseMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync();

            #region PriceListItemSellPrice
            PriceListItem sellpricelistitem = new PriceListItem();
            sellpricelistitem.PriceListID = pl.ID;
            sellpricelistitem.Name = "ราคาขาย";
            sellpricelistitem.Order = 1;
            sellpricelistitem.MasterPriceItemID = MasterPriceItemKeys.SellPrice;
            sellpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            sellpricelistitem.PriceUnitAmount = 1;
            sellpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            sellpricelistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
            sellpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(sellpricelistitem);
            #endregion

            #region PriceListItemNetSell
            PriceListItem netselllistitem = new PriceListItem();
            netselllistitem.PriceListID = pl.ID;
            netselllistitem.Name = "ราคาขายสุทธิ";
            netselllistitem.Order = 2;
            netselllistitem.MasterPriceItemID = MasterPriceItemKeys.NetSellPrice;
            netselllistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            netselllistitem.PriceUnitAmount = 1;
            netselllistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            netselllistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
            netselllistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

            listPriceListItem.Add(netselllistitem);
            #endregion

            #region PriceListItemBooking
            PriceListItem bookingpricelistitem = new PriceListItem();
            bookingpricelistitem.PriceListID = pl.ID;
            bookingpricelistitem.Name = "เงินจอง";
            bookingpricelistitem.Order = 3;
            bookingpricelistitem.MasterPriceItemID = MasterPriceItemKeys.BookingAmount;
            bookingpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.BookingAmount).RoundMoney());
            bookingpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.BookingAmount / input.TotalSalePrice));
            bookingpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            bookingpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            bookingpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(bookingpricelistitem);
            #endregion

            #region PriceListItemContract
            PriceListItem contractpricelistitem = new PriceListItem();
            contractpricelistitem.PriceListID = pl.ID;
            contractpricelistitem.Name = "เงินสัญญา";
            contractpricelistitem.Order = 4;
            contractpricelistitem.MasterPriceItemID = MasterPriceItemKeys.ContractAmount;
            contractpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.ContractAmount).RoundMoney());
            contractpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.ContractAmount / input.TotalSalePrice));
            contractpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            contractpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            contractpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(contractpricelistitem);
            #endregion

            #region PriceListItemDownPrice
            PriceListItem downpricelistitem = new PriceListItem();
            downpricelistitem.PriceListID = pl.ID;
            downpricelistitem.Name = "เงินดาวน์";
            downpricelistitem.Installment = Convert.ToInt32(input.DownPaymentPeriod);
            if (downpricelistitem.Installment == 0)
            {
                downpricelistitem.Installment = null;
            }
            downpricelistitem.Order = 5;
            downpricelistitem.MasterPriceItemID = MasterPriceItemKeys.DownAmount;

            if (input.PercentDownPayment != 0 && input.PercentDownPayment != null)
            {

                downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((input.TotalSalePrice * Convert.ToDecimal(input.PercentDownPayment)) / 100) - input.BookingAmount - input.ContractAmount).RoundMoney());
                downpricelistitem.PriceUnitAmount = input.PercentDownPayment / 100;
                downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
                downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
                if (!string.IsNullOrEmpty(input.SpecialDown) && !string.IsNullOrEmpty(input.SpecialDownPrice))
                {
                    var specialDownPrice = input.SpecialDownPrice.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o).RoundMoney())).ToList();
                    decimal normalAmount = downpricelistitem.Amount;
                    foreach (var item in specialDownPrice)
                    {
                        normalAmount -= item;
                    }

                    var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
                    downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
                    downpricelistitem.SpecialInstallments = input.SpecialDown;
                    downpricelistitem.SpecialInstallmentAmounts = string.Join(",", specialDownPrice);
                }
                else
                {
                    downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
                }
            }
            else
            {
                downpricelistitem.Amount = 0;
            }

            listPriceListItem.Add(downpricelistitem);
            #endregion

            #region PriceListItemEXTRAAREAPRICE
            PriceListItem extrapricelistitem = new PriceListItem();
            extrapricelistitem.PriceListID = pl.ID;
            extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
            extrapricelistitem.Order = 6;
            extrapricelistitem.MasterPriceItemID = MasterPriceItemKeys.ExtraAreaPrice;

            extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice) / Convert.ToDecimal(unit.SaleArea));
            extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            extrapricelistitem.PriceUnitAmount = unit.SaleArea;

            listPriceListItem.Add(extrapricelistitem);

            #endregion

            await DB.PriceLists.AddAsync(pl);
            await DB.PriceListItems.AddRangeAsync(listPriceListItem);
            await DB.SaveChangesAsync();
            await this.PriceListDataStatus(projectID);
            var result = await this.GetPriceListAsync(projectID, pl.ID);
            return result;
        }

        public async Task<PriceListDTO> UpdatePriceListAsync(Guid projectID, Guid id, PriceListDTO input)
        {
            var unit = await DB.Units.Where(x => x.UnitNo == input.UnitNo && x.ProjectID == projectID).FirstOrDefaultAsync();

            List<PriceListItem> listPriceListItem = new List<PriceListItem>();

            PriceList pl = new PriceList();
            pl.UnitID = unit.ID;
            pl.ActiveDate = DateTime.Now;

            var priceUnitPercentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "0").Select(o => o.ID).FirstAsync();
            var priceUnitItemMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceUnit && o.Key == "2").Select(o => o.ID).FirstAsync();
            var priceTypeHouseMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PriceType && o.Key == "0").Select(o => o.ID).FirstAsync();

            #region PriceListItemSellPrice
            PriceListItem sellpricelistitem = new PriceListItem();
            sellpricelistitem.PriceListID = pl.ID;
            sellpricelistitem.Name = "ราคาขาย";
            sellpricelistitem.Order = 1;
            sellpricelistitem.MasterPriceItemID = MasterPriceItemKeys.SellPrice;
            sellpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            sellpricelistitem.PriceUnitAmount = 1;
            sellpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            sellpricelistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
            sellpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(sellpricelistitem);
            #endregion

            #region PriceListItemNetSell
            PriceListItem netselllistitem = new PriceListItem();
            netselllistitem.PriceListID = pl.ID;
            netselllistitem.Name = "ราคาขายสุทธิ";
            netselllistitem.Order = 2;
            netselllistitem.MasterPriceItemID = MasterPriceItemKeys.NetSellPrice;
            netselllistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            netselllistitem.PriceUnitAmount = 1;
            netselllistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            netselllistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
            netselllistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

            listPriceListItem.Add(netselllistitem);
            #endregion

            #region PriceListItemBooking
            PriceListItem bookingpricelistitem = new PriceListItem();
            bookingpricelistitem.PriceListID = pl.ID;
            bookingpricelistitem.Name = "เงินจอง";
            bookingpricelistitem.Order = 3;
            bookingpricelistitem.MasterPriceItemID = MasterPriceItemKeys.BookingAmount;
            bookingpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.BookingAmount).RoundMoney());
            bookingpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.BookingAmount / input.TotalSalePrice));
            bookingpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            bookingpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            bookingpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(bookingpricelistitem);
            #endregion

            #region PriceListItemContract
            PriceListItem contractpricelistitem = new PriceListItem();
            contractpricelistitem.PriceListID = pl.ID;
            contractpricelistitem.Name = "เงินสัญญา";
            contractpricelistitem.Order = 4;
            contractpricelistitem.MasterPriceItemID = MasterPriceItemKeys.ContractAmount;
            contractpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.ContractAmount).RoundMoney());
            contractpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(input.ContractAmount / input.TotalSalePrice));
            contractpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            contractpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            contractpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
            listPriceListItem.Add(contractpricelistitem);
            #endregion

            #region PriceListItemDownPrice
            PriceListItem downpricelistitem = new PriceListItem();
            downpricelistitem.PriceListID = pl.ID;
            downpricelistitem.Name = "เงินดาวน์";
            downpricelistitem.Installment = Convert.ToInt32(input.DownPaymentPeriod);
            if (downpricelistitem.Installment == 0)
            {
                downpricelistitem.Installment = null;
            }
            downpricelistitem.Order = 5;
            downpricelistitem.MasterPriceItemID = MasterPriceItemKeys.DownAmount;

            if (input.PercentDownPayment != 0 && input.PercentDownPayment != null)
            {

                downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((input.TotalSalePrice * Convert.ToDecimal(input.PercentDownPayment)) / 100) - input.BookingAmount - input.ContractAmount).RoundMoney());
                downpricelistitem.PriceUnitAmount = input.PercentDownPayment / 100;
                downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
                downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
                if (!string.IsNullOrEmpty(input.SpecialDown) && !string.IsNullOrEmpty(input.SpecialDownPrice))
                {
                    var specialDownPrice = input.SpecialDownPrice.Split(',').Select(o => Decimal.Round(Convert.ToDecimal(o).RoundMoney())).ToList();
                    decimal normalAmount = downpricelistitem.Amount;
                    foreach (var item in specialDownPrice)
                    {
                        normalAmount -= item;
                    }

                    var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
                    downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
                    downpricelistitem.SpecialInstallments = input.SpecialDown;
                    downpricelistitem.SpecialInstallmentAmounts = string.Join(",", specialDownPrice);
                }
                else
                {
                    downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
                }
            }
            else
            {
                downpricelistitem.Amount = 0;
            }

            listPriceListItem.Add(downpricelistitem);
            #endregion

            #region PriceListItemEXTRAAREAPRICE
            PriceListItem extrapricelistitem = new PriceListItem();
            extrapricelistitem.PriceListID = pl.ID;
            extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
            extrapricelistitem.Order = 6;
            extrapricelistitem.MasterPriceItemID = MasterPriceItemKeys.ExtraAreaPrice;

            extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice).RoundMoney());
            extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(input.TotalSalePrice) / Convert.ToDecimal(unit.SaleArea));
            extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
            extrapricelistitem.PriceUnitAmount = unit.SaleArea;

            listPriceListItem.Add(extrapricelistitem);

            #endregion

            await DB.PriceLists.AddAsync(pl);
            await DB.PriceListItems.AddRangeAsync(listPriceListItem);
            await DB.SaveChangesAsync();
            await this.PriceListDataStatus(projectID);
            var result = await this.GetPriceListAsync(projectID, pl.ID);
            return result;
        }

        public async Task<PriceList> DeletePriceListAsync(Guid id)
        {
            var model = await DB.PriceLists.Where(o => o.ID == id).FirstAsync();
            model.IsDeleted = true;
            await DB.SaveChangesAsync();
            return model;
        }

        public async Task<PriceListDTO> GetPriceListAsync(Guid projectID, Guid id)
        {
            var allUnitID = await DB.Units.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToListAsync();
            var allUnits = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var allPriceList = await DB.PriceLists.Include(o => o.UpdatedBy).Where(o => allUnitID.Contains(o.UnitID)).ToListAsync();
            var allPriceListID = await DB.PriceLists.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToListAsync();
            var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.PriceListID)).Include(o => o.MasterPriceItem).ToListAsync();
            var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();

            var tempdata = (from unit in allUnits
                            join priceList in allPriceList on unit.ID equals priceList.UnitID
                            where priceList.ID == id
                            select new TempPriceListQueryResult
                            {
                                Unit = unit,
                                PriceList = priceList
                            }).ToList();

            tempdata = tempdata.GroupBy(o => o.Unit).Select(o => new TempPriceListQueryResult
            {
                Unit = o.Key,
                Titledeed = allTitleDeed.Where(p => p.UnitID == o.Key.ID).FirstOrDefault(),
                PriceList = o.Where(p => p.PriceList.ActiveDate <= DateTime.Now).Select(p => p.PriceList).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
            }).ToList();

            tempdata.ForEach(o => o.PriceListItems = allPriceListItem.Where(p => p.PriceListID == o.PriceList.ID)
                                                    .Select(p => PriceListItemDTO.CreateFromModel(p))
                                                    .OrderBy(p => p.Order)
                                                    .ToList());

            var data = tempdata.Select(x => new PriceListQueryResult
            {
                Unit = x.Unit,
                TitleDeedArea = x.Titledeed?.TitledeedArea,
                BookingAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.BookingAmount).FirstOrDefault(),
                TotalSalePrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.NetSellPrice).FirstOrDefault(),
                ContractAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ContractAmount).FirstOrDefault(),
                DownAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).FirstOrDefault(),
                SpecialDown = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallments).FirstOrDefault(),
                SpecialDownPrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallmentAmounts).FirstOrDefault(),
                DownPaymentPerPeriod = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.InstallmentAmount).FirstOrDefault(),
                PercentDownPayment = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() == 0 ? null : x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() * 100,
                OffsetAreaUnitPrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                OffsetArea = (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) == 0 ? null : (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea),
                OffsetAreaPrice = (Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault()) == 0 ? null : Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                PriceList = x.PriceList,
                UnitStatus = x.Unit.UnitStatus
            }).ToList();

            var result = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).FirstOrDefault();
            return result;
        }
        public async Task<PriceListExcelDTO> ImportProjectPriceListAsync(Guid projectID, FileDTO input)
        {
            // Require
            var err0061 = await DB.ErrorMessages.Where(o => o.Key == "ERR0061").FirstAsync();
            // Not Found
            var err0062 = await DB.ErrorMessages.Where(o => o.Key == "ERR0062").FirstAsync();
            // Decimal 2 Digit
            var err0065 = await DB.ErrorMessages.Where(o => o.Key == "ERR0065").FirstAsync();
            // Integer
            var err0069 = await DB.ErrorMessages.Where(o => o.Key == "ERR0069").FirstAsync();
            //Format Date
            var err0071 = await DB.ErrorMessages.Where(o => o.Key == "ERR0071").FirstAsync();
            // Format Comma
            var err0085 = await DB.ErrorMessages.Where(o => o.Key == "ERR0085").FirstAsync();

            var units = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var dt = await this.ConvertExcelToDataTable(input);
            /// Valudate Header
            if (dt.Columns.Count != 14)
            {
                throw new Exception("Invalid File Format");
            }

            var row = 2;
            var error = 0;
            //Read Excel Model

            #region Validate
            var checkNullWBSCodes = new List<string>();
            var checkNullUnitNos = new List<string>();
            var chcekNullSalePrices = new List<string>();
            var checkNullAreaPricePerUnits = new List<string>();
            var checkNullLocationPrices = new List<string>();
            var checkNullBookingAmounts = new List<string>();
            var checkNullContractAmounts = new List<string>();
            var checkNullDownPercents = new List<string>();
            var checkNullDownInstallments = new List<string>();
            var checkNullSpecialDownInstallmens = new List<string>();
            var checkNullSpecialDownInstallmentAmounts = new List<string>();
            var checkNullDocumentDates = new List<string>();

            var checkUnitNotFounds = new List<string>();

            var chcekFormatSalePrices = new List<string>();
            var checkFormatAreaPricePerUnits = new List<string>();
            var checkFormatLocationPrices = new List<string>();
            var checkFormatBookingAmounts = new List<string>();
            var checkFormatContractAmounts = new List<string>();
            var checkFormatDownPercents = new List<string>();
            var checkFormatDownInstallments = new List<string>();
            var checkFormatSpecialDownInstallmens = new List<string>();
            var checkFormatSpecialDownInstallmentAmounts = new List<string>();
            var checkFormatDocumentDates = new List<string>();
            #endregion

            var priceListExcelModels = new List<PriceListExcelModel>();
            foreach (DataRow r in dt.Rows)
            {
                var isError = false;
                var excelModel = PriceListExcelModel.CreateFromDataRow(r);
                priceListExcelModels.Add(excelModel);

                #region Validate
                if (string.IsNullOrEmpty(excelModel.WBSCode))
                {
                    checkNullWBSCodes.Add((row).ToString());
                    isError = true;
                }
                if (string.IsNullOrEmpty(excelModel.UnitNo))
                {
                    checkNullUnitNos.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    var unit = units.Where(o => o.SAPWBSNo == excelModel.WBSCode && o.UnitNo == excelModel.UnitNo).FirstOrDefault();
                    if (unit == null)
                    {
                        checkUnitNotFounds.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._sellPriceIndex].ToString()))
                {
                    chcekNullSalePrices.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._sellPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        chcekFormatSalePrices.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._areaPricePerUnitIndex].ToString()))
                {
                    checkNullAreaPricePerUnits.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._areaPricePerUnitIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatAreaPricePerUnits.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._locationPriceIndex].ToString()))
                {
                    checkNullLocationPrices.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._locationPriceIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatLocationPrices.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._bookingAmountIndex].ToString()))
                {
                    checkNullBookingAmounts.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._bookingAmountIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatBookingAmounts.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._contractAmountIndex].ToString()))
                {
                    checkNullContractAmounts.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._contractAmountIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatContractAmounts.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._downPercentIndex].ToString()))
                {
                    checkNullDownPercents.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._downPercentIndex].ToString().IsOnlyNumberWithMaxDigit(2))
                    {
                        checkFormatDownPercents.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._downInstallmentIndex].ToString()))
                {
                    checkNullDownInstallments.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._downInstallmentIndex].ToString().IsOnlyNumber())
                    {
                        checkFormatDownInstallments.Add((row).ToString());
                        isError = true;
                    }
                }

                if (string.IsNullOrEmpty(r[PriceListExcelModel._specialDownInstallmensIndex].ToString()))
                {
                    checkNullSpecialDownInstallmens.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._specialDownInstallmensIndex].ToString().IsOnlyNumberWithSpecialCharacter(false, ","))
                    {
                        checkFormatSpecialDownInstallmens.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(r[PriceListExcelModel._specialDownInstallmentAmountsIndex].ToString()))
                {
                    checkNullDocumentDates.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._specialDownInstallmentAmountsIndex].ToString().IsOnlyNumberWithSpecialCharacter(false, ",."))
                    {
                        checkFormatSpecialDownInstallmentAmounts.Add((row).ToString());
                        isError = true;
                    }
                }
                if (string.IsNullOrEmpty(r[PriceListExcelModel._documentDateIndex].ToString()))
                {
                    checkNullSpecialDownInstallmentAmounts.Add((row).ToString());
                    isError = true;
                }
                else
                {
                    if (!r[PriceListExcelModel._documentDateIndex].ToString().isFormatDate())
                    {
                        checkFormatDocumentDates.Add((row).ToString());
                        isError = true;
                    }
                }

                if (isError)
                {
                    error++;
                }
                #endregion

                row++;
            }
            PriceListExcelDTO result = new PriceListExcelDTO { Error = 0, Success = 0, ErrorMessages = new List<string>() };

            var project = await DB.Projects.Where(o => o.ID == projectID).FirstAsync();
            ValidateException ex = new ValidateException();
            if (priceListExcelModels.Any(o => o.ProjectNo != project.ProjectNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0058").FirstAsync();
                var msg = errMsg.Message.Replace("[column]", "รหัสโครงการ");
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }

            #region Add Validate Result

            #region Required
            if (checkNullWBSCodes.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "WBSCODE");
                    msg = msg.Replace("[row]", String.Join(",", checkNullWBSCodes));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullUnitNos.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullUnitNos));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (chcekNullSalePrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ราคาขาย");
                    msg = msg.Replace("[row]", String.Join(",", chcekNullSalePrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ราคาขาย");
                    msg = msg.Replace("[row]", String.Join(",", chcekNullSalePrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullAreaPricePerUnits.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAreaPricePerUnits));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
                    msg = msg.Replace("[row]", String.Join(",", checkNullAreaPricePerUnits));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullLocationPrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "ค่าทำเล");
                    msg = msg.Replace("[row]", String.Join(",", checkNullLocationPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "ค่าทำเล");
                    msg = msg.Replace("[row]", String.Join(",", checkNullLocationPrices));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullBookingAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เงินจอง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullBookingAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เงินจอง");
                    msg = msg.Replace("[row]", String.Join(",", checkNullBookingAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullContractAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เงินทำสัญญา");
                    msg = msg.Replace("[row]", String.Join(",", checkNullContractAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เงินทำสัญญา");
                    msg = msg.Replace("[row]", String.Join(",", checkNullContractAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullDownPercents.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "%เงินดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDownPercents));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "%เงินดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDownPercents));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullDownInstallments.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "งวดดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDownInstallments));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "งวดดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDownInstallments));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullSpecialDownInstallmens.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "งวดดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmens));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "งวดดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmens));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullSpecialDownInstallmentAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "เงินดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmentAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "เงินดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkNullSpecialDownInstallmentAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkNullDocumentDates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0061.Message.Replace("[column]", "วันที่ Version เอกสาร");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDocumentDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0061.Message.Replace("[column]", "วันที่ Version เอกสาร");
                    msg = msg.Replace("[row]", String.Join(",", checkNullDocumentDates));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region NotFounds
            if (checkUnitNotFounds.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0062.Message.Replace("[column]", "เลขที่แปลง");
                    msg = msg.Replace("[row]", String.Join(",", checkUnitNotFounds));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Format Decimal 2 Digit
            if (chcekFormatSalePrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาขาย");
                    msg = msg.Replace("[row]", String.Join(",", chcekFormatSalePrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาขาย");
                    msg = msg.Replace("[row]", String.Join(",", chcekFormatSalePrices));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatAreaPricePerUnits.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAreaPricePerUnits));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ราคาพื้นที่ต่อหน่วย");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatAreaPricePerUnits));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatLocationPrices.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "ค่าทำเล");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLocationPrices));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "ค่าทำเล");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatLocationPrices));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatBookingAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "เงินจอง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBookingAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "เงินจอง");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatBookingAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }

            if (checkFormatContractAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "เงินทำสัญญา");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatContractAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "เงินทำสัญญา");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatContractAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatDownPercents.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0065.Message.Replace("[column]", "%เงินดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDownPercents));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0065.Message.Replace("[column]", "%เงินดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDownPercents));
                    result.ErrorMessages.Add(msg);
                }
            }

            #endregion

            #region Format Comma
            if (checkFormatSpecialDownInstallmens.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0085.Message.Replace("[column]", "งวดดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmens));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0085.Message.Replace("[column]", "งวดดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmens));
                    result.ErrorMessages.Add(msg);
                }
            }
            if (checkFormatSpecialDownInstallmentAmounts.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0085.Message.Replace("[column]", "เงินดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmentAmounts));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0085.Message.Replace("[column]", "เงินดาวน์พิเศษ");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatSpecialDownInstallmentAmounts));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Integer
            if (checkFormatDownInstallments.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0069.Message.Replace("[column]", "งวดดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDownInstallments));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0069.Message.Replace("[column]", "งวดดาวน์");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDownInstallments));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region Format Date
            if (checkFormatDocumentDates.Count() > 0)
            {
                if (result.ErrorMessages != null)
                {
                    var msg = err0071.Message.Replace("[column]", "วันที่ Version เอกสาร");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDocumentDates));
                    result.ErrorMessages.Add(msg);
                }
                else
                {
                    result.ErrorMessages = new List<string>();
                    var msg = err0071.Message.Replace("[column]", "วันที่ Version เอกสาร");
                    msg = msg.Replace("[row]", String.Join(",", checkFormatDocumentDates));
                    result.ErrorMessages.Add(msg);
                }
            }
            #endregion

            #region rowError
            var rowErrors = new List<string>();
            rowErrors.AddRange(checkNullWBSCodes);
            rowErrors.AddRange(checkNullUnitNos);
            rowErrors.AddRange(chcekNullSalePrices);
            rowErrors.AddRange(checkNullAreaPricePerUnits);
            rowErrors.AddRange(checkNullLocationPrices);
            rowErrors.AddRange(checkNullBookingAmounts);
            rowErrors.AddRange(checkNullContractAmounts);
            rowErrors.AddRange(checkNullDownPercents);
            rowErrors.AddRange(checkNullDownInstallments);
            rowErrors.AddRange(checkNullSpecialDownInstallmens);
            rowErrors.AddRange(checkNullSpecialDownInstallmentAmounts);
            rowErrors.AddRange(checkNullDocumentDates);
            rowErrors.AddRange(checkUnitNotFounds);
            rowErrors.AddRange(chcekFormatSalePrices);
            rowErrors.AddRange(checkFormatAreaPricePerUnits);
            rowErrors.AddRange(checkFormatLocationPrices);
            rowErrors.AddRange(checkFormatBookingAmounts);
            rowErrors.AddRange(checkFormatContractAmounts);
            rowErrors.AddRange(checkFormatDownPercents);
            rowErrors.AddRange(checkFormatDownInstallments);
            rowErrors.AddRange(checkFormatSpecialDownInstallmens);
            rowErrors.AddRange(checkFormatSpecialDownInstallmentAmounts);
            rowErrors.AddRange(checkFormatDocumentDates);
            #endregion

            #endregion
            var rowIntErrors = rowErrors.Distinct().Select(o => Convert.ToInt32(o)).ToList();
            row = 2;
            //Update Data
            List<PriceList> listPriceList = new List<PriceList>();
            List<PriceListItem> listPriceListItem = new List<PriceListItem>();

            var priceUnitPercentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PriceUnit" && o.Key == "0").Select(o => o.ID).FirstAsync();
            var priceUnitItemMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PriceUnit" && o.Key == "2").Select(o => o.ID).FirstAsync();
            var priceTypeHouseMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PriceType" && o.Key == "0").Select(o => o.ID).FirstAsync();

            foreach (var item in priceListExcelModels)
            {
                if (!rowIntErrors.Contains(row))
                {
                    var unit = units.Where(o => o.UnitNo == item.UnitNo && o.ProjectID == projectID && o.SAPWBSNo == item.WBSCode).FirstOrDefault();
                    if (unit != null)
                    {
                        #region PriceList
                        PriceList pl = new PriceList();
                        pl.UnitID = unit.ID;
                        DateTime activeDate;
                        if (DateTime.TryParseExact(item.DocumentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out activeDate))
                        {
                            pl.ActiveDate = activeDate;
                        }

                        listPriceList.Add(pl);
                        #endregion

                        #region PriceListItemSellPrice
                        PriceListItem sellpricelistitem = new PriceListItem();
                        sellpricelistitem.PriceListID = pl.ID;
                        sellpricelistitem.Name = "ราคาขาย";
                        sellpricelistitem.Order = 1;
                        sellpricelistitem.MasterPriceItemID = MasterPriceItemKeys.SellPrice;
                        sellpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        sellpricelistitem.PriceUnitAmount = 1;
                        sellpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.AreaPricePerUnit).RoundMoney());
                        sellpricelistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
                        sellpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;


                        listPriceListItem.Add(sellpricelistitem);
                        #endregion

                        #region PriceListItemNetSell
                        PriceListItem netselllistitem = new PriceListItem();
                        netselllistitem.PriceListID = pl.ID;
                        netselllistitem.Name = "ราคาขายสุทธิ";
                        netselllistitem.Order = 2;
                        netselllistitem.MasterPriceItemID = MasterPriceItemKeys.NetSellPrice;
                        netselllistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        netselllistitem.PriceUnitAmount = 1;
                        netselllistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        netselllistitem.PriceUnitMasterCenterID = priceUnitItemMasterCenterID;
                        netselllistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

                        listPriceListItem.Add(netselllistitem);
                        #endregion

                        #region PriceListItemBooking
                        PriceListItem bookingpricelistitem = new PriceListItem();
                        bookingpricelistitem.PriceListID = pl.ID;
                        bookingpricelistitem.Name = "เงินจอง";
                        bookingpricelistitem.Order = 3;
                        bookingpricelistitem.MasterPriceItemID = MasterPriceItemKeys.BookingAmount;
                        bookingpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.BookingAmount).RoundMoney());
                        bookingpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(item.BookingAmount / item.SellPrice));
                        bookingpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        bookingpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                        bookingpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

                        listPriceListItem.Add(bookingpricelistitem);
                        #endregion

                        #region PriceListItemContract
                        PriceListItem contractpricelistitem = new PriceListItem();
                        contractpricelistitem.PriceListID = pl.ID;
                        contractpricelistitem.Name = "เงินสัญญา";
                        contractpricelistitem.Order = 4;
                        contractpricelistitem.MasterPriceItemID = MasterPriceItemKeys.ContractAmount;
                        contractpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.ContractAmount).RoundMoney());
                        contractpricelistitem.PriceUnitAmount = Math.Round(Convert.ToDouble(item.ContractAmount / item.SellPrice));
                        contractpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        contractpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                        contractpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;

                        listPriceListItem.Add(contractpricelistitem);
                        #endregion

                        #region PriceListItemDownPrice
                        PriceListItem downpricelistitem = new PriceListItem();
                        downpricelistitem.PriceListID = pl.ID;
                        downpricelistitem.Name = "เงินดาวน์";
                        downpricelistitem.Installment = item.DownInstallment;
                        if (downpricelistitem.Installment == 0)
                        {
                            downpricelistitem.Installment = null;
                        }
                        downpricelistitem.Order = 5;
                        downpricelistitem.MasterPriceItemID = MasterPriceItemKeys.DownAmount;

                        if (item.DownPercent != 0)
                        {
                            downpricelistitem.Amount = Decimal.Round(Convert.ToDecimal(((item.SellPrice * item.DownPercent) / 100) - item.ContractAmount - item.BookingAmount).RoundMoney());
                            downpricelistitem.PriceUnitAmount = item.DownPercent / 100;
                            downpricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                            downpricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                            downpricelistitem.PriceTypeMasterCenterID = priceTypeHouseMasterCenterID;
                            if (item.SpecialDownInstallments.Count() > 0 && item.SpecialDownInstallments.Count() > 0)
                            {
                                var specialDownPrice = item.SpecialDownInstallmentAmounts;
                                decimal normalAmount = downpricelistitem.Amount;
                                foreach (var specialPrice in specialDownPrice)
                                {
                                    normalAmount -= Decimal.Round(Convert.ToDecimal(specialPrice));
                                }

                                var normalInstallmentAmount = normalAmount / (downpricelistitem.Installment - specialDownPrice.Count());
                                downpricelistitem.InstallmentAmount = Decimal.Round(normalInstallmentAmount.Value.RoundMoney());
                                downpricelistitem.SpecialInstallments = String.Join(',', item.SpecialDownInstallments);
                                downpricelistitem.SpecialInstallmentAmounts = String.Join(',', item.SpecialDownInstallmentAmounts);
                            }
                            else
                            {
                                downpricelistitem.InstallmentAmount = Decimal.Round(Convert.ToDecimal(downpricelistitem.Amount / downpricelistitem.Installment).RoundMoney());
                            }
                        }
                        else
                        {
                            downpricelistitem.Amount = 0;
                        }



                        listPriceListItem.Add(downpricelistitem);
                        #endregion

                        #region PriceListItemEXTRAAREAPRICE
                        PriceListItem extrapricelistitem = new PriceListItem();
                        extrapricelistitem.PriceListID = pl.ID;
                        extrapricelistitem.Name = "ราคาพื้นที่เพิ่มลด";
                        extrapricelistitem.Order = 6;
                        extrapricelistitem.MasterPriceItemID = MasterPriceItemKeys.ExtraAreaPrice;

                        extrapricelistitem.Amount = Decimal.Round(Convert.ToDecimal(item.SellPrice).RoundMoney());
                        extrapricelistitem.PricePerUnitAmount = Decimal.Round(Convert.ToDecimal(item.SellPrice) / Convert.ToDecimal(unit.SaleArea));
                        extrapricelistitem.PriceUnitMasterCenterID = priceUnitPercentMasterCenterID;
                        extrapricelistitem.PriceUnitAmount = unit.SaleArea;

                        listPriceListItem.Add(extrapricelistitem);

                        #endregion

                    }
                }
                row++;
            }

            await DB.PriceLists.AddRangeAsync(listPriceList);
            await DB.PriceListItems.AddRangeAsync(listPriceListItem);
            await this.PriceListDataStatus(projectID);
            await DB.SaveChangesAsync();

            result.Success = listPriceList.Count();
            result.Error = error;
            return result;
        }

        public async Task<DataTable> ConvertExcelToDataTable(FileDTO input)
        {
            var excelStream = await FileHelper.GetStreamFromUrlAsync(input.Url);
            string fileName = input.Name;
            var fileExtention = fileName != null ? fileName.Split('.').ToList().Last() : null;
            bool hasHeader = true;
            using (Stream stream = new MemoryStream(XLSToXLSXConverter.ReadFully(excelStream)))
            {
                byte[] excelByte;
                if (fileExtention.ToLower() == "xls")
                {
                    excelByte = XLSToXLSXConverter.Convert(stream);
                }
                else
                {
                    excelByte = XLSToXLSXConverter.ReadFully(stream);
                }
                using (System.IO.MemoryStream xlsxStream = new System.IO.MemoryStream(excelByte))
                using (var pck = new OfficeOpenXml.ExcelPackage(xlsxStream))
                {
                    var ws = pck.Workbook.Worksheets.First();
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }

                    return tbl;
                }
            }
        }

        public async Task<FileDTO> ExportExcelPriceListAsync(Guid projectID)
        {
            ExportExcel result = new ExportExcel();
            var allUnits = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var allUnitID = allUnits.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToList();
            var allPriceList = await DB.PriceLists.Include(o => o.UpdatedBy)
                              .Where(o => allUnitID.Contains(o.UnitID))
                              .GroupBy(o => o.UnitID)
                              .Select(o =>
                                 o.Where(p => p.ActiveDate <= DateTime.Now).Select(p => p).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
                             ).ToListAsync();
            var allPriceListID = allPriceList.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToList();
            var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.PriceListID))
                                                          .Include(o => o.MasterPriceItem)
                                                          .Include(o => o.PriceUnit)
                                                          .Include(o => o.PriceType)
                                                          .ToListAsync();
            var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();


            var tempData = (from unit in allUnits
                            join priceList in allPriceList on unit.ID equals priceList.UnitID into p
                            from pl in p.DefaultIfEmpty()
                            join titledeed in allTitleDeed on unit.ID equals titledeed.UnitID into t
                            from td in t.DefaultIfEmpty()
                            select new TempPriceListQueryResult
                            {
                                Unit = unit,
                                PriceList = pl,
                                Titledeed = td,
                                PriceListItems = allPriceListItem.Where(o => o.PriceListID == pl?.ID)
                                                    .Select(o => PriceListItemDTO.CreateFromModel(o))
                                                    .OrderBy(o => o.Order)
                                                    .ToList()
                            });

            var data = tempData.Select(o => new PriceListQueryResult
            {
                Unit = o.Unit,
                TitleDeedArea = o.Titledeed?.TitledeedArea,
                BookingAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.BookingAmount).FirstOrDefault(),
                TotalSalePrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.NetSellPrice).FirstOrDefault(),
                ContractAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ContractAmount).FirstOrDefault(),
                DownAmount = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).FirstOrDefault(),
                SpecialDown = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallments).FirstOrDefault(),
                SpecialDownPrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallmentAmounts).FirstOrDefault(),
                DownPaymentPerPeriod = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.InstallmentAmount).FirstOrDefault(),
                PercentDownPayment = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() == 0 ? null : o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() * 100,
                OffsetAreaUnitPrice = o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                OffsetArea = (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) == 0 ? null : (o.Titledeed?.TitledeedArea - o.Unit?.SaleArea),
                OffsetAreaPrice = (Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault()) == 0 ? null : Convert.ToDecimal(o.Titledeed?.TitledeedArea - o.Unit?.SaleArea) * o.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                PriceList = o.PriceList,
                UnitStatus = o.Unit.UnitStatus
            }).OrderBy(o => o.Unit.UnitNo).ToList();


            var results = data.Select(o => PriceListDTO.CreateFromQueryResult(o)).ToList();

            string path = Path.Combine(FileHelper.GetApplicationRootPath(), "ExcelTemplates", "ProjectID_PriceList.xlsx");
            byte[] tmp = File.ReadAllBytes(path);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(tmp))
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                int _projectNoIndex = PriceListExcelModel._projectNoIndex + 1;
                int _projectNameIndex = PriceListExcelModel._projectNameIndex + 1;
                int _wbsCodeIndex = PriceListExcelModel._wbsCodeIndex + 1;
                int _unitNoIndex = PriceListExcelModel._unitNoIndex + 1;
                int _sellPriceIndex = PriceListExcelModel._sellPriceIndex + 1;
                int _areaPricePerUnitIndex = PriceListExcelModel._areaPricePerUnitIndex + 1;
                int _locationPriceIndex = PriceListExcelModel._locationPriceIndex + 1;
                int _bookingAmount = PriceListExcelModel._bookingAmountIndex + 1;
                int _contractAmountIndex = PriceListExcelModel._contractAmountIndex + 1;
                int _downPercentIndex = PriceListExcelModel._downPercentIndex + 1;
                int _downInstallmentIndex = PriceListExcelModel._downInstallmentIndex + 1;
                int _specialDownInstallmensIndex = PriceListExcelModel._specialDownInstallmensIndex + 1;
                int _specialDownInstallmentAmountsIndex = PriceListExcelModel._specialDownInstallmentAmountsIndex + 1;
                int _documentCodeIndex = PriceListExcelModel._documentDateIndex + 1;

                var project = await DB.Projects.Where(x => x.ID == projectID).FirstOrDefaultAsync();
                for (int c = 2; c < data.Count + 2; c++)
                {
                    var unit = await DB.Units.Where(o => o.ProjectID == project.ID && o.UnitNo == data[c - 2].Unit.UnitNo).FirstOrDefaultAsync();
                    worksheet.Cells[c, _projectNoIndex].Value = project.ProjectNo;
                    worksheet.Cells[c, _projectNameIndex].Value = project.ProjectNameTH;
                    worksheet.Cells[c, _wbsCodeIndex].Value = unit.SAPWBSNo;
                    worksheet.Cells[c, _unitNoIndex].Value = results[c - 2].UnitNo;
                    worksheet.Cells[c, _sellPriceIndex].Value = results[c - 2].TotalSalePrice;
                    worksheet.Cells[c, _areaPricePerUnitIndex].Value = results[c - 2].OffsetAreaUnitPrice;
                    worksheet.Cells[c, _locationPriceIndex].Value = 0;
                    worksheet.Cells[c, _bookingAmount].Value = results[c - 2].BookingAmount;
                    worksheet.Cells[c, _contractAmountIndex].Value = results[c - 2].ContractAmount;
                    worksheet.Cells[c, _downPercentIndex].Value = results[c - 2].PercentDownPayment;
                    worksheet.Cells[c, _downInstallmentIndex].Value = results[c - 2].DownPaymentPeriod;
                    worksheet.Cells[c, _specialDownInstallmensIndex].Value = results[c - 2].SpecialDown;
                    worksheet.Cells[c, _specialDownInstallmentAmountsIndex].Value = results[c - 2].SpecialDownPrice;
                    worksheet.Cells[c, _documentCodeIndex].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                    worksheet.Cells[c, _documentCodeIndex].Value = Convert.ToDateTime(results[c - 2]?.ActiveDate);
                }
                //worksheet.Cells.AutoFitColumns();

                result.FileContent = package.GetAsByteArray();
                result.FileName = project.ID + "_PriceList.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            Stream fileStream = new MemoryStream(result.FileContent);
            string fileName = $"{Guid.NewGuid()}_{result.FileName}";
            string contentType = result.FileType;
            string filePath = $"{projectID}/export-excels/";

            var uploadResult = await this.FileHelper.UploadFileFromStream(fileStream, filePath, fileName, contentType);

            return new FileDTO()
            {
                Name = uploadResult.Name,
                Url = uploadResult.Url
            };
        }

        private async Task<Guid> PriceListDataStatus(Guid projectID)
        {
            var allUnitID = await DB.Units.Where(o => o.ProjectID == projectID).Select(o => o.ID).ToListAsync();
            var allUnits = await DB.Units.Where(o => o.ProjectID == projectID).Include(o => o.UnitStatus).ToListAsync();
            var allPriceList = await DB.PriceLists.Where(o => allUnitID.Contains(o.UnitID)).ToListAsync();
            var allPriceListID = await DB.PriceLists.Where(o => allUnitID.Contains(o.UnitID)).Select(o => o.ID).ToListAsync();
            var allPriceListItem = await DB.PriceListItems.Where(o => allPriceListID.Contains(o.ID)).ToListAsync();
            var allTitleDeed = await DB.TitledeedDetails.Where(o => o.ProjectID == projectID).ToListAsync();

            var priceListDataStatusSaleMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Sale).Select(o => o.ID).FirstAsync();
            var priceListDataStatusPrepareMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProjectDataStatus && o.Key == ProjectDataStatusKeys.Draft).Select(o => o.ID).FirstAsync();
            var priceListDataStatusMasterCenterID = priceListDataStatusPrepareMasterCenterID;

            var tempData = (from unit in allUnits
                            join priceList in allPriceList on unit.ID equals priceList.UnitID
                            select new TempPriceListQueryResult
                            {
                                Unit = unit,
                                PriceList = priceList
                            }).ToList();

            tempData = tempData.GroupBy(o => o.Unit).Select(o => new TempPriceListQueryResult
            {
                Unit = o.Key,
                Titledeed = allTitleDeed.Where(p => p.UnitID == o.Key.ID).FirstOrDefault(),
                PriceList = o.Where(p => p.PriceList.ActiveDate <= DateTime.Now).Select(p => p.PriceList).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
            }).ToList();

            tempData.ForEach(o => o.PriceListItems = allPriceListItem.Where(p => p.PriceListID == o.PriceList.ID)
                                                    .Select(p => PriceListItemDTO.CreateFromModel(p))
                                                    .OrderBy(p => p.Order)
                                                    .ToList());

            var data = tempData.Select(x => new PriceListQueryResult
            {
                Unit = x.Unit,
                TitleDeedArea = x.Titledeed?.TitledeedArea,
                BookingAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.BookingAmount).FirstOrDefault(),
                TotalSalePrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.NetSellPrice).FirstOrDefault(),
                ContractAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ContractAmount).FirstOrDefault(),
                DownAmount = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).FirstOrDefault(),
                SpecialDown = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallments).FirstOrDefault() != null ? string.Join("-", x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(t => t.SpecialInstallments.Split(',').ToList()).FirstOrDefault()) : null,
                SpecialDownPrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(f => f.SpecialInstallmentAmounts).FirstOrDefault() != null ? string.Join("-", x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(t => t.SpecialInstallmentAmounts.Split(',').ToList()).FirstOrDefault()) : null,
                DownPaymentPerPeriod = (x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.Amount).FirstOrDefault()
                                                        - (x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(g => g.SpecialInstallmentAmounts).FirstOrDefault() != null ? x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(g => g.SpecialInstallmentAmounts.Split(',').ToList().Sum(c => Convert.ToDecimal(c))).FirstOrDefault() : 0))
                                                        / (x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.Installment).FirstOrDefault()
                                                        - (x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(g => g.SpecialInstallments).FirstOrDefault() != null ? x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(g => g.SpecialInstallments.Split(',').ToList().Count()).FirstOrDefault() : 0)),
                PercentDownPayment = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault() == 0 ? null : x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.DownAmount).Select(c => c.PriceUnitAmount).FirstOrDefault(),
                OffsetAreaUnitPrice = x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                OffsetArea = (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) == 0 ? null : (x.Titledeed?.TitledeedArea - x.Unit?.SaleArea),
                OffsetAreaPrice = (Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault()) == 0 ? null : Convert.ToDecimal(x.Titledeed?.TitledeedArea - x.Unit?.SaleArea) * x.PriceListItems.Where(c => c.MasterPriceItem.Id == MasterPriceItemKeys.ExtraAreaPrice).Select(c => c.PricePerUnitAmount).FirstOrDefault(),
                PriceList = x.PriceList,
                UnitStatus = x.Unit.UnitStatus
            }).ToList();

            if (data.TrueForAll(o =>
                    !string.IsNullOrEmpty(o.Unit?.UnitNo)
                    && o.Unit?.SaleArea != null
                    && o.BookingAmount?.Amount != 0
                    && o.ContractAmount?.Amount != 0
                    && o.PercentDownPayment != null
                    && o.TotalSalePrice?.Amount != 0
                    && o.DownAmount?.Amount != 0
                    && o.DownAmount?.Installment != null
                    && !string.IsNullOrEmpty(o.SpecialDown)
                    && !string.IsNullOrEmpty(o.SpecialDownPrice)
            ))
            {
                priceListDataStatusMasterCenterID = priceListDataStatusSaleMasterCenterID;
            }
            return priceListDataStatusMasterCenterID;
        }
    }
}
