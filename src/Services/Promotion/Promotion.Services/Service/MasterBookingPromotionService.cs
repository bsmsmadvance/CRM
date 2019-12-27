using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public class MasterBookingPromotionService : IMasterBookingPromotionService
    {
        private readonly DatabaseContext DB;

        public MasterBookingPromotionService(DatabaseContext db)
        {
            this.DB = db;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358677/preview
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterBookingPromotionDTO> CreateMasterBookingPromotionAsync(MasterBookingPromotionDTO input)
        {
            await input.ValidateAsync(DB);
            var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            MasterBookingPromotion model = new MasterBookingPromotion();
            input.ToModel(ref model);

            string year = Convert.ToString(DateTime.Today.Year);
            var key = "PS" + input.Project?.ProjectNo + year[2] + year[3];
            var type = "PRM.MasterBookingPromotion";
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

                model.PromotionNo = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                model.PromotionNo = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }

            if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
            {
                var allPromotionStatusActive = await DB.MasterBookingPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != model.ID && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                                             .ToListAsync();

                allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
                DB.MasterBookingPromotions.UpdateRange(allPromotionStatusActive);
                await DB.SaveChangesAsync();
            }

            await DB.MasterBookingPromotions.AddAsync(model);
            await DB.SaveChangesAsync();
            var result = await this.GetMasterBookingPromotionDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358679/preview
        /// </summary>
        /// <returns>The master booking promotion detail async.</returns>
        /// <param name="id">MasterBookingPromotion.ID</param>
        public async Task<MasterBookingPromotionDTO> GetMasterBookingPromotionDetailAsync(Guid id)
        {
            var model = await DB.MasterBookingPromotions.Where(o => o.ID == id)
                                                        .Include(o => o.Project)
                                                        .ThenInclude(o => o.Company)
                                                        .Include(o => o.PromotionStatus)
                                                        .Include(o => o.UpdatedBy)
                                                        .FirstAsync();
            var result = MasterBookingPromotionDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358677/preview
        /// </summary>
        /// <returns>The master booking promotion list async.</returns>
        /// <param name="filter">Filter.</param>
        public async Task<MasterBookingPromotionPaging> GetMasterBookingPromotionListAsync(MasterBookingPromotionListFilter filter, PageParam pageParam, MasterBookingPromotionSortByParam sortByParam)
        {
            IQueryable<MasterBookingPromotionQueryResult> query = DB.MasterBookingPromotions.Select(o =>
                                                                 new MasterBookingPromotionQueryResult
                                                                 {
                                                                     Project = o.Project,
                                                                     MasterBookingPromotion = o,
                                                                     PromotionStatus = o.PromotionStatus,
                                                                     UpdatedBy = o.UpdatedBy
                                                                 });

            #region Filter
            if (!string.IsNullOrEmpty(filter.PromotionNo))
            {
                query = query.Where(o => o.MasterBookingPromotion.PromotionNo.Contains(filter.PromotionNo));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(o => o.MasterBookingPromotion.Name.Contains(filter.Name));
            }
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                query = query.Where(o => o.Project.ID == filter.ProjectID);
            }
            if (!string.IsNullOrEmpty(filter.PromotionStatusKey))
            {
                var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == filter.PromotionStatusKey
                                                                      && x.MasterCenterGroupKey == "PromotionStatus")
                                                                     .Select(x => x.ID).FirstAsync();
                query = query.Where(x => x.MasterBookingPromotion.PromotionStatusMasterCenterID == promotionStatusMasterCenterID);
            }
            if (filter.StartDateFrom != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.StartDate >= filter.StartDateFrom);
            }
            if (filter.StartDateTo != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.StartDate <= filter.StartDateTo);
            }
            if (filter.StartDateFrom != null && filter.StartDateTo != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.StartDate >= filter.StartDateFrom
                                    && o.MasterBookingPromotion.StartDate <= filter.StartDateTo);
            }
            if (filter.IsUsed != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.IsUsed == filter.IsUsed);
            }
            if (filter.EndDateFrom != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.EndDate >= filter.EndDateFrom);
            }
            if (filter.EndDateTo != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.EndDate <= filter.EndDateTo);
            }
            if (filter.EndDateFrom != null && filter.EndDateTo != null)
            {
                query = query.Where(o => o.MasterBookingPromotion.EndDate >= filter.EndDateFrom
                                    && o.MasterBookingPromotion.EndDate <= filter.EndDateTo);
            }
            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.MasterBookingPromotion.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterBookingPromotion.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.MasterBookingPromotion.Updated >= filter.UpdatedFrom && x.MasterBookingPromotion.Updated <= filter.UpdatedTo);
            }


            #endregion

            MasterBookingPromotionDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterBookingPromotionQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterBookingPromotionDTO.CreateFromQueryResult(o)).ToList();

            return new MasterBookingPromotionPaging()
            {
                PageOutput = pageOutput,
                MasterBookingPromotionDTOs = results
            };
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358679/preview
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterBookingPromotionDTO> UpdateMasterBookingPromotionAsync(Guid id, MasterBookingPromotionDTO input)
        {
            await input.ValidateAsync(DB, true);
            var model = await DB.MasterBookingPromotions.Where(o => o.ID == id).FirstAsync();
            var masterCenterPromotionStatusActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "1")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();
            var masterCenterPromotionStatusInActiveID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == "0")
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();

            var countPromotionStatusActive = await DB.MasterBookingPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                                             .CountAsync();
            ValidateException ex = new ValidateException();
            if (input.StartDate != null && input.EndDate != null)
            {
                int totalDay = input.EndDate.Value.Subtract(input.StartDate.Value).Days;
                if (totalDay > 180)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0038").FirstAsync();
                    string desc = "วันที่สิ้นสุดการใช้งาน";
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (input.PromotionStatus?.Id == masterCenterPromotionStatusActiveID)
            {
                if (countPromotionStatusActive < 1)
                {
                    input.ToModel(ref model);
                }
                else
                {
                    var allPromotionStatusActive = await DB.MasterBookingPromotions.Where(o => o.ProjectID == model.ProjectID && o.ID != id && o.PromotionStatusMasterCenterID == masterCenterPromotionStatusActiveID)
                                                                             .ToListAsync();

                    allPromotionStatusActive.ForEach(o => o.PromotionStatusMasterCenterID = masterCenterPromotionStatusInActiveID);
                    DB.MasterBookingPromotions.UpdateRange(allPromotionStatusActive);
                    await DB.SaveChangesAsync();
                    input.ToModel(ref model);
                }
            }
            else
            {
                input.ToModel(ref model);
            }

            if (ex.HasError)
            {
                throw ex;
            }

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
            var result = await this.GetMasterBookingPromotionDetailAsync(model.ID);
            return result;
        }

        /// <summary>
        /// ลบ Master โปรขาย
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterBookingPromotionAsync(Guid id)
        {
            var model = await DB.MasterBookingPromotions.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion item list async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterBookingPromotionItemPaging> GetMasterBookingPromotionItemListAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingPromotionItemSortByParam sortByParam)
        {
            IQueryable<MasterBookingPromotionItemQueryResult> query = DB.MasterBookingPromotionItems
                                                                     .Where(o => o.MasterBookingPromotionID == masterBookingPromotionID)
                                                                     .Select(o =>
                                                                     new MasterBookingPromotionItemQueryResult
                                                                     {
                                                                         PromotionMaterialItem = o.PromotionMaterialItem,
                                                                         MasterBookingPromotionItem = o,
                                                                         PromotionItemStatus = o.PromotionItemStatus,
                                                                         WhenPromotionReceive = o.WhenPromotionReceive,
                                                                         UpdatedBy = o.UpdatedBy
                                                                     });

            MasterBookingPromotionItemDTO.SortBy(sortByParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterBookingPromotionItemDTO.CreateFromQueryResult(o)).OrderBy(o => o.MainPromotionItemID).ToList();

            List<MasterBookingPromotionItemDTO> subItems = new List<MasterBookingPromotionItemDTO>();
            foreach (var item in results.ToList())
            {
                if (item.MainPromotionItemID != null)
                {
                    subItems.Add(item);
                    results.Remove(item);
                }
            }

            var i = 0;
            foreach (var item in results.ToList())
            {
                var subs = subItems.Where(o => o.MainPromotionItemID == item.Id).ToList();
                if (subs.Count() > 0)
                {
                    MasterBookingPromotionItemDTO.SortByDTO(sortByParam, ref subs);
                    results.InsertRange(i + 1, subs);
                    i++;
                    i += subs.Count();
                }
                else
                {
                    i++;
                }
            }

            var pageOutput = PagingHelper.PagingList<MasterBookingPromotionItemDTO>(pageParam, ref results);

            return new MasterBookingPromotionItemPaging()
            {
                PageOutput = pageOutput,
                MasterBookingPromotionItemDTOs = results
            };
        }

        /// <summary>
        /// เรียกใช้ตอนกดปุ่ม "บันทึก"
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterBookingPromotionItemDTO>> UpdateMasterBookingPromotionItemListAsync(Guid masterBookingPromotionID, List<MasterBookingPromotionItemDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }
            var listMasterBookingPromotionItemUpdate = new List<MasterBookingPromotionItem>();
            var allMasterBookingPromotionItem = await DB.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                var existingItem = allMasterBookingPromotionItem.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    //if (existingItem.MainPromotionItemID == null)
                    //{
                    existingItem.TotalPrice = existingItem.Quantity * existingItem.PricePerUnit;
                    //}
                    listMasterBookingPromotionItemUpdate.Add(existingItem);
                }
            }

            DB.UpdateRange(listMasterBookingPromotionItemUpdate);
            await DB.SaveChangesAsync();

            var queryResults = await DB.MasterBookingPromotionItems
                                        .Where(o => o.MasterBookingPromotionID == masterBookingPromotionID)
                                        .Include(o => o.PromotionMaterialItem)
                                        .Include(o => o.WhenPromotionReceive)
                                        .Include(o => o.PromotionItemStatus)
                                        .Include(o => o.UpdatedBy)
                                        .ToListAsync();

            var results = queryResults.Select(o => MasterBookingPromotionItemDTO.CreateFromModel(o)).ToList();

            return results;
        }

        /// <summary>
        /// แก้ไขรายการโปร ทีละอัน
        /// </summary>
        /// <returns>The master booking promotion item async.</returns>
        /// <param name="masterBookingPromotionID">Master booking promotion identifier.</param>
        /// <param name="input">Inputs.</param>
        public async Task<MasterBookingPromotionItemDTO> UpdateMasterBookingPromotionItemAsync(Guid masterBookingPromotionID, Guid masterBookingPromotionItemID, MasterBookingPromotionItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID && o.ID == masterBookingPromotionItemID).FirstAsync();
            input.ToModel(ref model);
            //if (model.MainPromotionItemID == null)
            //{
            model.TotalPrice = model.Quantity * model.PricePerUnit;
            //}
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataResult = await DB.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID
                                                                        && o.ID == masterBookingPromotionItemID)
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                  .Include(o => o.WhenPromotionReceive)
                                                                  .Include(o => o.PromotionItemStatus)
                                                                  .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();

            var result = MasterBookingPromotionItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบทีละอัน ตรงปุ่ม ถังขยะ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterBookingPromotionItemAsync(Guid id)
        {
            var model = await DB.MasterBookingPromotionItems.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// เพิ่มรายการโปรโมชั่นโดยเลือกจาก Material ที่ดึงจาก SAP
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
        /// </summary>
        /// <returns>The master booking promotion item from material async.</returns>
        /// <param name="inputs">ส่งเฉพาะ Item ที่เลือก</param>
        public async Task<List<MasterBookingPromotionItemDTO>> CreateMasterBookingPromotionItemFromMaterialAsync(Guid masterBookingPromotionID, List<PromotionMaterialDTO> inputs)
        {
            var listMasterBookingPromotionItemCreate = new List<MasterBookingPromotionItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
            foreach (var item in inputs)
            {
                await ValidatePromotionMaterial(masterBookingPromotionID, item);
                MasterBookingPromotionItem model = new MasterBookingPromotionItem();
                item.ToMasterBookingPromotionItemModel(ref model);
                model.MasterBookingPromotionID = masterBookingPromotionID;
                model.MainPromotionItemID = null;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;
                var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                // SapInfo
                if (promotionMaterialItem != null)
                {
                    model.StartDate = promotionMaterialItem.StartDate;
                    model.ExpireDate = promotionMaterialItem.ExpireDate;
                    model.ItemNo = promotionMaterialItem.ItemNo;
                    model.BrandEN = promotionMaterialItem.BrandEN;
                    model.BrandTH = promotionMaterialItem.BrandTH;
                    model.RemarkTH = promotionMaterialItem.RemarkTH;
                    model.RemarkEN = promotionMaterialItem.RemarkEN;
                    model.UnitEN = promotionMaterialItem.UnitEN;
                    model.SpecEN = promotionMaterialItem.SpecEN;
                    model.SpecTH = promotionMaterialItem.SpecTH;
                    model.Plant = promotionMaterialItem.Plant;
                    model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
                    model.AgreementNo = promotionMaterialItem.AgreementNo;
                    model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
                    model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
                    model.DocType = promotionMaterialItem.DocType;
                    model.GLAccountNo = promotionMaterialItem.GLAccountNo;
                    model.MaterialPrice = promotionMaterialItem.Price;
                    model.MaterialBasePrice = promotionMaterialItem.BasePrice;
                    model.Vat = promotionMaterialItem.Vat;
                    model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
                    model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
                    model.SAPVendor = promotionMaterialItem.SAPVendor;
                    model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
                    model.MaterialCode = promotionMaterialItem.MaterialCode;
                    model.TotalPrice = promotionMaterialItem.Price;
                    model.PricePerUnit = promotionMaterialItem.Price;
                }
                listMasterBookingPromotionItemCreate.Add(model);
            }
            await DB.AddRangeAsync(listMasterBookingPromotionItemCreate);
            await DB.SaveChangesAsync();
            var createdIDs = listMasterBookingPromotionItemCreate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterBookingPromotionItems.Where(o => createdIDs.Contains(o.ID))
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                  .Include(o => o.WhenPromotionReceive)
                                                                  .Include(o => o.PromotionItemStatus)
                                                                  .Include(o => o.UpdatedBy)
                                                                  .ToListAsync();

            var result = dataResult.Select(o => MasterBookingPromotionItemDTO.CreateFromModel(o)).ToList();
            return result;
        }

        /// <summary>
        /// เพิ่มรายการย่อยจากการเลือก Material 
        /// รายการหลักกับรายการย่อยใช้ DTO เดียวกัน เหมือนกับ Model
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358682/preview
        /// </summary>
        /// <returns>The sub master booking promotion item from material async.</returns>
        /// <param name="inputs">ส่งมาเฉพาะรายการที่เลือกมาเท่านั้น</param>
        public async Task<List<MasterBookingPromotionItemDTO>> CreateSubMasterBookingPromotionItemFromMaterialAsync(Guid masterBookingPromotionID, Guid mainMasterBookingPromotionItemID, List<PromotionMaterialDTO> inputs)
        {
            var listMasterBookingPromotionItemCreate = new List<MasterBookingPromotionItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            var whenPromotionReceiveAfterContract = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "WhenPromotionReceive" && o.Key == "1").FirstAsync();
            foreach (var item in inputs)
            {
                await ValidatePromotionMaterial(masterBookingPromotionID, item);
                MasterBookingPromotionItem model = new MasterBookingPromotionItem();
                item.ToMasterBookingPromotionItemModel(ref model);
                model.MasterBookingPromotionID = masterBookingPromotionID;
                model.MainPromotionItemID = mainMasterBookingPromotionItemID;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                model.WhenPromotionReceiveMasterCenterID = whenPromotionReceiveAfterContract.ID;
                var promotionMaterialItem = await DB.PromotionMaterialItems.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                // SapInfo
                if (promotionMaterialItem != null)
                {
                    model.StartDate = promotionMaterialItem.StartDate;
                    model.ExpireDate = promotionMaterialItem.ExpireDate;
                    model.ItemNo = promotionMaterialItem.ItemNo;
                    model.BrandEN = promotionMaterialItem.BrandEN;
                    model.BrandTH = promotionMaterialItem.BrandTH;
                    model.RemarkTH = promotionMaterialItem.RemarkTH;
                    model.RemarkEN = promotionMaterialItem.RemarkEN;
                    model.UnitEN = promotionMaterialItem.UnitEN;
                    model.SpecEN = promotionMaterialItem.SpecEN;
                    model.SpecTH = promotionMaterialItem.SpecTH;
                    model.Plant = promotionMaterialItem.Plant;
                    model.SAPCompanyID = promotionMaterialItem.SAPCompanyID;
                    model.AgreementNo = promotionMaterialItem.AgreementNo;
                    model.SAPPurchasingOrg = promotionMaterialItem.SAPPurchasingOrg;
                    model.MaterialGroupKey = promotionMaterialItem.MaterialGroupKey;
                    model.DocType = promotionMaterialItem.DocType;
                    model.GLAccountNo = promotionMaterialItem.GLAccountNo;
                    model.MaterialPrice = promotionMaterialItem.Price;
                    model.MaterialBasePrice = promotionMaterialItem.BasePrice;
                    model.Vat = promotionMaterialItem.Vat;
                    model.SAPSaleTaxCode = promotionMaterialItem.SAPSaleTaxCode;
                    model.SAPBaseUnit = promotionMaterialItem.SAPBaseUnit;
                    model.SAPVendor = promotionMaterialItem.SAPVendor;
                    model.SAPPurchasingGroup = promotionMaterialItem.SAPPurchasingGroup;
                    model.MaterialCode = promotionMaterialItem.MaterialCode;
                    model.TotalPrice = promotionMaterialItem.Price;
                    model.PricePerUnit = promotionMaterialItem.Price;
                }
                listMasterBookingPromotionItemCreate.Add(model);
            }
            await DB.AddRangeAsync(listMasterBookingPromotionItemCreate);
            await DB.SaveChangesAsync();

            var createdIDs = listMasterBookingPromotionItemCreate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterBookingPromotionItems.Where(o => createdIDs.Contains(o.ID))
                                                                  .Include(o => o.PromotionMaterialItem)
                                                                  .Include(o => o.WhenPromotionReceive)
                                                                  .Include(o => o.PromotionItemStatus)
                                                                  .Include(o => o.UpdatedBy)
                                                                  .ToListAsync();

            var result = dataResult.Select(o => MasterBookingPromotionItemDTO.CreateFromModel(o)).ToList();
            return result;
        }

        /// <summary>
        /// ดึงแบบบ้านจาก Item โปรขาย
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master booking promotion item model list async.</returns>
        /// <param name="masterBookingPromotionItemID">Master booking promotion item identifier.</param>
        public async Task<List<ModelListDTO>> GetMasterBookingPromotionItemModelListAsync(Guid masterBookingPromotionItemID)
        {
            var listModel = await DB.MasterBookingHouseModelItems.Where(o => o.MasterBookingPromotionItemID == masterBookingPromotionItemID).ToListAsync();
            var results = new List<ModelListDTO>();
            foreach (var item in listModel)
            {
                ModelQueryResult model = await DB.Models.Where(o => o.ID == item.ModelID)
                                                        .Select(o =>
                                                                new ModelQueryResult
                                                                {
                                                                    Model = o,
                                                                    ModelShortName = o.ModelShortName,
                                                                    ModelType = o.ModelType,
                                                                    ModelUnitType = o.ModelUnitType,
                                                                    TypeOfRealEstate = o.TypeOfRealEstate,
                                                                    WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(p => p.ModelID == o.ID).OrderByDescending(p => p.Version).FirstOrDefault()
                                                                })
                                                        .FirstOrDefaultAsync();
                if (model != null)
                {
                    results.Add(ModelListDTO.CreateFromQueryResult(model));
                }
            }
            return results;
        }

        /// <summary>
        /// เพิ่มแบบบ้านเข้าไปใน Item โปรขาย
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master booking promotion item model list async.</returns>
        /// <param name="masterBookingPromotionItemID">Master booking promotion item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<ModelListDTO>> AddMasterBookingPromotionItemModelListAsync(Guid masterBookingPromotionItemID, List<ModelListDTO> inputs)
        {
            var masterBookingHouseModelItemsCreate = new List<MasterBookingHouseModelItem>();
            var masterBookingHouseModelItemsUpdate = new List<MasterBookingHouseModelItem>();
            var masterBookingHouseModelItemsDelete = new List<MasterBookingHouseModelItem>();
            var masterBookingHouseModelItems = await DB.MasterBookingHouseModelItems.Where(o => o.MasterBookingPromotionItemID == masterBookingPromotionItemID).ToListAsync();

            foreach (var item in inputs)
            {
                var existingItem = masterBookingHouseModelItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
                if (existingItem == null)
                {
                    MasterBookingHouseModelItem model = new MasterBookingHouseModelItem();
                    model.MasterBookingPromotionItemID = masterBookingPromotionItemID;
                    model.ModelID = item.Id;
                    masterBookingHouseModelItemsCreate.Add(model);
                }
                else
                {
                    masterBookingHouseModelItemsUpdate.Add(existingItem);
                }
            }
            foreach (var item in masterBookingHouseModelItems)
            {
                var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    masterBookingHouseModelItemsDelete.Add(item);
                }
            }
            DB.UpdateRange(masterBookingHouseModelItemsUpdate);
            DB.UpdateRange(masterBookingHouseModelItemsDelete);
            await DB.AddRangeAsync(masterBookingHouseModelItemsCreate);
            await DB.SaveChangesAsync();

            var results = await this.GetMasterBookingPromotionItemModelListAsync(masterBookingPromotionItemID);
            return results;
        }

        /// <summary>
        /// ดึงรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion free item list async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterBookingPromotionFreeItemPaging> GetMasterBookingPromotionFreeItemListAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingPromotionFreeItemSortByParam sortByParam)
        {
            IQueryable<MasterBookingPromotionFreeItemQueryResult> query = DB.MasterBookingPromotionFreeItems
                                                                     .Where(o => o.MasterBookingPromotionID == masterBookingPromotionID)
                                                                     .Select(o =>
                                                                     new MasterBookingPromotionFreeItemQueryResult
                                                                     {
                                                                         MasterBookingPromotionFreeItem = o,
                                                                         WhenPromotionReceive = o.WhenPromotionReceive,
                                                                         UpdatedBy = o.UpdatedBy
                                                                     });

            MasterBookingPromotionFreeItemDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterBookingPromotionFreeItemQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterBookingPromotionFreeItemDTO.CreateFromQueryResult(o)).ToList();

            return new MasterBookingPromotionFreeItemPaging()
            {
                PageOutput = pageOutput,
                MasterBookingPromotionFreeItemDTOs = results
            };
        }

        /// <summary>
        /// สร้างรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion free item async.</returns>
        /// <param name="input">Input.</param>
        public async Task<MasterBookingPromotionFreeItemDTO> CreateMasterBookingPromotionFreeItemAsync(Guid masterBookingPromotionID, MasterBookingPromotionFreeItemDTO input)
        {
            await input.ValidateAsync(DB);
            MasterBookingPromotionFreeItem model = new MasterBookingPromotionFreeItem();
            input.ToModel(ref model);
            model.MasterBookingPromotionID = masterBookingPromotionID;
            await DB.MasterBookingPromotionFreeItems.AddAsync(model);
            await DB.SaveChangesAsync();

            model = await DB.MasterBookingPromotionFreeItems
                .Include(o => o.WhenPromotionReceive)
                .Include(o => o.UpdatedBy)
                .FirstAsync(o => o.ID == model.ID);

            var result = MasterBookingPromotionFreeItemDTO.CreateFromModel(model);
            return result;
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละหลายรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion free item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterBookingPromotionFreeItemDTO>> UpdateMasterBookingPromotionFreeItemListAsync(Guid masterBookingPromotionID, List<MasterBookingPromotionFreeItemDTO> inputs)
        {
            var listMasterBookingPromotionFreeItemUpdate = new List<MasterBookingPromotionFreeItem>();
            var allMasterBookingPromotionFreeItem = await DB.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }

            foreach (var item in inputs)
            {
                var existingItem = allMasterBookingPromotionFreeItem.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    listMasterBookingPromotionFreeItemUpdate.Add(existingItem);
                }
            }

            DB.UpdateRange(listMasterBookingPromotionFreeItemUpdate);
            await DB.SaveChangesAsync();

            var updatedIDs = listMasterBookingPromotionFreeItemUpdate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterBookingPromotionFreeItems
                .Include(o => o.WhenPromotionReceive)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID)).ToListAsync();

            var listresult = dataResult.Select(o => MasterBookingPromotionFreeItemDTO.CreateFromModel(o)).ToList();
            return listresult;
        }

        /// <summary>
        /// แก้ไขรายการที่ไม่ต้องจัดซื้อ (แบบที่ละรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion free item async.</returns>
        /// <param name="masterBookingPromotionID">Master booking promotion identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<MasterBookingPromotionFreeItemDTO> UpdateMasterBookingPromotionFreeItemAsync(Guid masterBookingPromotionID, Guid masterBookingPromotionFreeItemID, MasterBookingPromotionFreeItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID && o.ID == masterBookingPromotionFreeItemID).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataResult = await DB.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID
                                                                        && o.ID == masterBookingPromotionFreeItemID)
                                                                    .Include(o => o.WhenPromotionReceive)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();
            var result = MasterBookingPromotionFreeItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบรายการที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358681/preview
        /// </summary>
        /// <returns>The master booking promotion free item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterBookingPromotionFreeItemAsync(Guid id)
        {
            var model = await DB.MasterBookingPromotionFreeItems.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// ดึงแบบบ้านของ Item ที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master booking promotion free item model list async.</returns>
        /// <param name="masterBookingPromotionItemID">Master booking promotion item identifier.</param>
        public async Task<List<ModelListDTO>> GetMasterBookingPromotionFreeItemModelListAsync(Guid masterBookingPromotionFreeItemID)
        {

            var listModel = await DB.MasterBookingHouseModelFreeItems.Where(o => o.MasterBookingPromotionFreeItemID == masterBookingPromotionFreeItemID).ToListAsync();
            var results = new List<ModelListDTO>();
            foreach (var item in listModel)
            {
                ModelQueryResult model = await DB.Models.Where(o => o.ID == item.ModelID)
                                                        .Select(o =>
                                                                new ModelQueryResult
                                                                {
                                                                    Model = o,
                                                                    ModelShortName = o.ModelShortName,
                                                                    ModelType = o.ModelType,
                                                                    ModelUnitType = o.ModelUnitType,
                                                                    TypeOfRealEstate = o.TypeOfRealEstate,
                                                                    WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(p => p.ModelID == o.ID).OrderByDescending(p => p.Version).FirstOrDefault()
                                                                })
                                                        .FirstOrDefaultAsync();
                if (model != null)
                {
                    results.Add(ModelListDTO.CreateFromQueryResult(model));
                }
            }
            return results;
        }

        /// <summary>
        /// เพิ่มแบบบ้านไปใน Item ที่ไม่ต้องจัดซื้อ
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358684/preview
        /// </summary>
        /// <returns>The master booking promotion free item model list async.</returns>
        /// <param name="masterBookingPromotionFreeItemID">Master booking promotion free item identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<ModelListDTO>> AddMasterBookingPromotionFreeItemModelListAsync(Guid masterBookingPromotionFreeItemID, List<ModelListDTO> inputs)
        {
            var masterBookingHouseModelFreeItemsCreate = new List<MasterBookingHouseModelFreeItem>();
            var masterBookingHouseModelFreeItemsUpdate = new List<MasterBookingHouseModelFreeItem>();
            var masterBookingHouseModelFreeItemsDelete = new List<MasterBookingHouseModelFreeItem>();
            var masterBookingHouseModelFreeItems = await DB.MasterBookingHouseModelFreeItems.Where(o => o.MasterBookingPromotionFreeItemID == masterBookingPromotionFreeItemID).ToListAsync();

            foreach (var item in inputs)
            {
                var existingItem = masterBookingHouseModelFreeItems.Where(o => o.ModelID == item.Id).FirstOrDefault();
                if (existingItem == null)
                {
                    MasterBookingHouseModelFreeItem model = new MasterBookingHouseModelFreeItem();
                    model.MasterBookingPromotionFreeItemID = masterBookingPromotionFreeItemID;
                    model.ModelID = item.Id;
                    masterBookingHouseModelFreeItemsCreate.Add(model);
                }
                else
                {
                    masterBookingHouseModelFreeItemsUpdate.Add(existingItem);
                }
            }
            foreach (var item in masterBookingHouseModelFreeItems)
            {
                var existingInput = inputs.Where(o => o.Id == item.ModelID).FirstOrDefault();
                if (existingInput == null)
                {
                    item.IsDeleted = true;
                    masterBookingHouseModelFreeItemsDelete.Add(item);
                }
            }
            DB.UpdateRange(masterBookingHouseModelFreeItemsUpdate);
            DB.UpdateRange(masterBookingHouseModelFreeItemsDelete);
            await DB.AddRangeAsync(masterBookingHouseModelFreeItemsCreate);
            await DB.SaveChangesAsync();

            var results = await this.GetMasterBookingPromotionFreeItemModelListAsync(masterBookingPromotionFreeItemID);
            return results;
        }

        /// <summary>
        /// ดึงรายการค่าธรรมเนียมรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master booking credit card item async.</returns>
        /// <param name="pageParam">Page parameter.</param>
        /// <param name="sortByParam">Sort by parameter.</param>
        public async Task<MasterBookingCreditCardItemPaging> GetMasterBookingCreditCardItemAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingCreditCardItemSortByParam sortByParam)
        {
            IQueryable<MasterBookingCreditCardItemQueryResult> query = DB.MasterBookingCreditCardItems
                                                                     .Where(o => o.MasterBookingPromotionID == masterBookingPromotionID)
                                                                     .Select(o =>
                                                                     new MasterBookingCreditCardItemQueryResult
                                                                     {
                                                                         MasterBookingCreditCardItem = o,
                                                                         Bank = o.Bank,
                                                                         EDCFee = o.EDCFee,
                                                                         PromotionItemStatus = o.PromotionItemStatus,
                                                                         UpdatedBy = o.UpdatedBy
                                                                     });

            MasterBookingCreditCardItemDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<MasterBookingCreditCardItemQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var results = queryResults.Select(o => MasterBookingCreditCardItemDTO.CreateFromQueryResult(o)).ToList();

            return new MasterBookingCreditCardItemPaging()
            {
                PageOutput = pageOutput,
                MasterBookingCreditCardItemDTOs = results
            };
        }

        /// <summary>
        /// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละหลายรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master booking credit card item list async.</returns>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterBookingCreditCardItemDTO>> UpdateMasterBookingCreditCardItemListAsync(Guid masterBookingPromotionID, List<MasterBookingCreditCardItemDTO> inputs)
        {
            foreach (var item in inputs)
            {
                await item.ValidateAsync(DB);
            }
            var masterBookingCreditCardItems = new List<MasterBookingCreditCardItem>();
            var existingMasterBookingCreditCardItems = await DB.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID).ToListAsync();
            foreach (var item in inputs)
            {
                var existingItem = existingMasterBookingCreditCardItems.Where(o => o.ID == item.Id).FirstOrDefault();
                if (existingItem != null)
                {
                    item.ToModel(ref existingItem);
                    masterBookingCreditCardItems.Add(existingItem);
                }
            }

            DB.UpdateRange(masterBookingCreditCardItems);
            await DB.SaveChangesAsync();

            var updatedIDs = masterBookingCreditCardItems.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterBookingCreditCardItems
                .Include(o => o.Bank)
                .Include(o => o.EDCFee)
                .Include(o => o.PromotionItemStatus)
                .Include(o => o.UpdatedBy)
                .Where(o => updatedIDs.Contains(o.ID)).ToListAsync();

            var listresult = dataResult.Select(o => MasterBookingCreditCardItemDTO.CreateFromModel(o)).ToList();
            return listresult;
        }

        /// <summary>
        /// แก้ไขรายการค่าธรรมเนียมรูดบัตร (ทีละรายการ)
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master booking credit card item async.</returns>
        /// <param name="masterBookingPromotionID">Master booking promotion identifier.</param>
        /// <param name="masterBookingCreditCardItemID">Master booking promotion credit card item identifier.</param>
        /// <param name="input">Input.</param>
        public async Task<MasterBookingCreditCardItemDTO> UpdateMasterBookingCreditCardItemAsync(Guid masterBookingPromotionID, Guid masterBookingCreditCardItemID, MasterBookingCreditCardItemDTO input)
        {
            await input.ValidateAsync(DB);
            var model = await DB.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID && o.ID == masterBookingCreditCardItemID).FirstAsync();
            input.ToModel(ref model);
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            var dataResult = await DB.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == masterBookingPromotionID
                                                                        && o.ID == masterBookingCreditCardItemID)
                                                                    .Include(o => o.Bank)
                                                                    .Include(o => o.EDCFee)
                                                                    .Include(o => o.PromotionItemStatus)
                                                                    .Include(o => o.UpdatedBy)
                                                                  .FirstAsync();
            var result = MasterBookingCreditCardItemDTO.CreateFromModel(dataResult);
            return result;
        }

        /// <summary>
        /// ลบรายการค่าธรรมเนียมรูดบัตร
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358685/preview
        /// </summary>
        /// <returns>The master booking credit card item async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task DeleteMasterBookingCreditCardItemAsync(Guid id)
        {
            var model = await DB.MasterBookingCreditCardItems.Where(x => x.ID == id).FirstAsync();
            model.IsDeleted = true;
            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// UI: https://projects.invisionapp.com/d/main#/console/17482068/362358686/preview
        /// EDCFee จะดึงมาจาก GET EDCFees API โดยมีเงื่อนไขดังนี้
        /// บัตรที่รูด เป็นธนาคารเดียวกัน, รูปแบบการรูดเฉพาะการผ่อน
        /// </summary>
        /// <returns>The master booking credit card items.</returns>
        /// <param name="masterBookingPromotionID">Master booking promotion identifier.</param>
        /// <param name="inputs">Inputs.</param>
        public async Task<List<MasterBookingCreditCardItemDTO>> CreateMasterBookingCreditCardItemsAsync(Guid masterBookingPromotionID, List<EDCFeeDTO> inputs)
        {
            var masterBookingCreditCardItemsCreate = new List<MasterBookingCreditCardItem>();
            var promotionItemStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == "1").FirstAsync();
            foreach (var item in inputs)
            {
                MasterBookingCreditCardItem model = new MasterBookingCreditCardItem();
                item.ToMasterBookingCreditCardItemModel(ref model);
                model.MasterBookingPromotionID = masterBookingPromotionID;
                model.PromotionItemStatusMasterCenterID = promotionItemStatusActive.ID;
                masterBookingCreditCardItemsCreate.Add(model);
            }
            await DB.AddRangeAsync(masterBookingCreditCardItemsCreate);
            await DB.SaveChangesAsync();

            var createdIDs = masterBookingCreditCardItemsCreate.Select(o => o.ID).ToList();
            var dataResult = await DB.MasterBookingCreditCardItems
                .Include(o => o.Bank)
                .Include(o => o.EDCFee)
                .Include(o => o.PromotionItemStatus)
                .Include(o => o.UpdatedBy)
                .Where(o => createdIDs.Contains(o.ID)).ToListAsync();

            var results = dataResult.Select(o => MasterBookingCreditCardItemDTO.CreateFromModel(o)).ToList();
            return results;
        }

        /// <summary>
        /// Clone Promotion ให้ Copy ทุกอย่างใน MasterPromotion สร้างเป็น Promotion ใหม่
        /// </summary>
        /// <returns>The master booking promotion async.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<MasterBookingPromotionDTO> CloneMasterBookingPromotionAsync(Guid id)
        {
            var masterBookingPromotion = await DB.MasterBookingPromotions.Where(o => o.ID == id).Include(o => o.Project).FirstAsync();
            var masterBookingPromotionItems = await DB.MasterBookingPromotionItems.Where(o => o.MasterBookingPromotionID == id && o.ExpireDate >= DateTime.Now).ToListAsync();
            var masterBookingPromotionFreeItems = await DB.MasterBookingPromotionFreeItems.Where(o => o.MasterBookingPromotionID == id).ToListAsync();
            var masterBookingPromotionCreditCardItems = await DB.MasterBookingCreditCardItems.Where(o => o.MasterBookingPromotionID == id).ToListAsync();


            var promotionStatusMasterCenterID = await DB.MasterCenters.Where(x => x.Key == "0"
                                                                    && x.MasterCenterGroupKey == "PromotionStatus")
                                                                   .Select(x => x.ID).FirstAsync();


            var newMasterBookingPromotion = new MasterBookingPromotion
            {
                Name = masterBookingPromotion.Name,
                ProjectID = masterBookingPromotion.ProjectID,
                StartDate = masterBookingPromotion.StartDate,
                EndDate = masterBookingPromotion.EndDate,
                CashDiscount = masterBookingPromotion.CashDiscount,
                FGFDiscount = masterBookingPromotion.FGFDiscount,
                TransferDiscount = masterBookingPromotion.TransferDiscount,
                PromotionStatusMasterCenterID = promotionStatusMasterCenterID,
                IsUsed = false
            };

            string year = Convert.ToString(DateTime.Today.Year);
            var key = "PS" + masterBookingPromotion.Project?.ProjectNo + year[2] + year[3];
            var type = "PRM.MasterBookingPromotion";
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

                newMasterBookingPromotion.PromotionNo = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                newMasterBookingPromotion.PromotionNo = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }

            var newMasterBookingPromotionItems = new List<MasterBookingPromotionItem>();
            var newMasterBookingHouseModelItems = new List<MasterBookingHouseModelItem>();
            var newMasterBookingPromotionFreeItems = new List<MasterBookingPromotionFreeItem>();
            var newMasterBookingHouseModelFreeItems = new List<MasterBookingHouseModelFreeItem>();
            var newMasterBookingCreditCardItem = new List<MasterBookingCreditCardItem>();
            foreach (var item in masterBookingPromotionItems.Where(o => o.MainPromotionItemID == null))
            {
                var model = new MasterBookingPromotionItem
                {
                    MasterBookingPromotionID = newMasterBookingPromotion.ID,
                    NameTH = item.NameTH,
                    NameEN = item.NameEN,
                    Quantity = item.Quantity,
                    UnitTH = item.UnitTH,
                    UnitEN = item.UnitEN,
                    PricePerUnit = item.PricePerUnit,
                    TotalPrice = item.TotalPrice,
                    ReceiveDays = item.ReceiveDays,
                    WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
                    IsPurchasing = item.IsPurchasing,
                    IsShowInContract = item.IsShowInContract,
                    PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
                    ExpireDate = item.ExpireDate,
                    MainPromotionItemID = null,
                    PromotionMaterialItemID = item.PromotionMaterialItemID,
                };
                //Sap info
                model.StartDate = item.StartDate;
                model.ItemNo = item.ItemNo;
                model.BrandEN = item.BrandEN;
                model.BrandTH = item.BrandTH;
                model.UnitEN = item.UnitEN;
                model.RemarkTH = item.RemarkTH;
                model.RemarkEN = item.RemarkEN;
                model.SpecEN = item.SpecEN;
                model.SpecTH = item.SpecTH;
                model.Plant = item.Plant;
                model.SAPCompanyID = item.SAPCompanyID;
                model.AgreementNo = item.AgreementNo;
                model.SAPPurchasingOrg = item.SAPPurchasingOrg;
                model.MaterialGroupKey = item.MaterialGroupKey;
                model.DocType = item.DocType;
                model.GLAccountNo = item.GLAccountNo;
                model.MaterialPrice = item.MaterialPrice;
                model.MaterialBasePrice = item.MaterialBasePrice;
                model.Vat = item.Vat;
                model.SAPSaleTaxCode = item.SAPSaleTaxCode;
                model.SAPBaseUnit = item.SAPBaseUnit;
                model.SAPVendor = item.SAPVendor;
                model.SAPPurchasingGroup = item.SAPPurchasingGroup;
                model.MaterialCode = item.MaterialCode;
                var masterBookingHouseItem = await DB.MasterBookingHouseModelItems.Where(o => o.MasterBookingPromotionItemID == item.ID).ToListAsync();
                foreach (var house in masterBookingHouseItem)
                {
                    var newhouse = new MasterBookingHouseModelItem
                    {
                        MasterBookingPromotionItemID = model.ID,
                        ModelID = house.ModelID
                    };
                    newMasterBookingHouseModelItems.Add(newhouse);
                }
                newMasterBookingPromotionItems.Add(model);
                var listSub = masterBookingPromotionItems.Where(o => o.MainPromotionItemID == item.ID).ToList();
                foreach (var item1 in listSub)
                {
                    var modelSub = new MasterBookingPromotionItem
                    {
                        MasterBookingPromotionID = newMasterBookingPromotion.ID,
                        NameTH = item1.NameTH,
                        NameEN = item1.NameEN,
                        Quantity = item1.Quantity,
                        UnitTH = item1.UnitTH,
                        UnitEN = item1.UnitEN,
                        PricePerUnit = item1.PricePerUnit,
                        TotalPrice = item1.TotalPrice,
                        ReceiveDays = item1.ReceiveDays,
                        WhenPromotionReceiveMasterCenterID = item1.WhenPromotionReceiveMasterCenterID,
                        IsPurchasing = item1.IsPurchasing,
                        IsShowInContract = item1.IsShowInContract,
                        PromotionItemStatusMasterCenterID = item1.PromotionItemStatusMasterCenterID,
                        ExpireDate = item1.ExpireDate,
                        MainPromotionItemID = model.ID,
                        PromotionMaterialItemID = item1.PromotionMaterialItemID,
                    };
                    //Sap info
                    modelSub.StartDate = item1.StartDate;
                    modelSub.ItemNo = item1.ItemNo;
                    modelSub.BrandEN = item1.BrandEN;
                    modelSub.BrandTH = item1.BrandTH;
                    modelSub.UnitEN = item1.UnitEN;
                    modelSub.RemarkTH = item1.RemarkTH;
                    modelSub.RemarkEN = item1.RemarkEN;
                    modelSub.SpecEN = item1.SpecEN;
                    modelSub.SpecTH = item1.SpecTH;
                    modelSub.Plant = item1.Plant;
                    modelSub.SAPCompanyID = item1.SAPCompanyID;
                    modelSub.AgreementNo = item1.AgreementNo;
                    modelSub.SAPPurchasingOrg = item1.SAPPurchasingOrg;
                    modelSub.MaterialGroupKey = item1.MaterialGroupKey;
                    modelSub.DocType = item1.DocType;
                    modelSub.GLAccountNo = item1.GLAccountNo;
                    modelSub.MaterialPrice = item1.MaterialPrice;
                    modelSub.MaterialBasePrice = item1.MaterialBasePrice;
                    modelSub.Vat = item1.Vat;
                    modelSub.SAPSaleTaxCode = item1.SAPSaleTaxCode;
                    modelSub.SAPBaseUnit = item1.SAPBaseUnit;
                    modelSub.SAPVendor = item1.SAPVendor;
                    modelSub.SAPPurchasingGroup = item1.SAPPurchasingGroup;
                    modelSub.MaterialCode = item1.MaterialCode;
                    newMasterBookingPromotionItems.Add(modelSub);
                    var masterBookingHouseItemSub = await DB.MasterBookingHouseModelItems.Where(o => o.MasterBookingPromotionItemID == item1.ID).ToListAsync();
                    foreach (var house1 in masterBookingHouseItemSub)
                    {
                        var newhousesub = new MasterBookingHouseModelItem
                        {
                            MasterBookingPromotionItemID = modelSub.ID,
                            ModelID = house1.ModelID
                        };
                        newMasterBookingHouseModelItems.Add(newhousesub);
                    }
                }
            }
            foreach (var item in masterBookingPromotionFreeItems)
            {
                var model = new MasterBookingPromotionFreeItem
                {
                    MasterBookingPromotionID = newMasterBookingPromotion.ID,
                    NameTH = item.NameTH,
                    NameEN = item.NameEN,
                    Quantity = item.Quantity,
                    UnitTH = item.UnitTH,
                    UnitEN = item.UnitEN,
                    ReceiveDays = item.ReceiveDays,
                    WhenPromotionReceiveMasterCenterID = item.WhenPromotionReceiveMasterCenterID,
                    IsShowInContract = item.IsShowInContract,
                };
                newMasterBookingPromotionFreeItems.Add(model);
                var masterBookingHouseFreeItem = await DB.MasterBookingHouseModelFreeItems.Where(o => o.MasterBookingPromotionFreeItemID == item.ID).ToListAsync();
                foreach (var item1 in masterBookingHouseFreeItem)
                {
                    var house = new MasterBookingHouseModelFreeItem
                    {
                        MasterBookingPromotionFreeItemID = model.ID,
                        ModelID = item1.ModelID
                    };
                    newMasterBookingHouseModelFreeItems.Add(house);
                }
            }
            foreach (var item in masterBookingPromotionCreditCardItems)
            {
                var model = new MasterBookingCreditCardItem()
                {
                    MasterBookingPromotionID = newMasterBookingPromotion.ID,
                    BankID = item.BankID,
                    NameTH = item.NameTH,
                    NameEN = item.NameEN,
                    Fee = item.Fee,
                    UnitTH = item.UnitTH,
                    UnitEN = item.UnitEN,
                    PromotionItemStatusMasterCenterID = item.PromotionItemStatusMasterCenterID,
                    Quantity = item.Quantity,
                    EDCFeeID = item.EDCFeeID
                };

                newMasterBookingCreditCardItem.Add(model);
            }
            await DB.MasterBookingPromotions.AddAsync(newMasterBookingPromotion);

            if (newMasterBookingPromotionItems.Count() > 0)
            {
                await DB.MasterBookingPromotionItems.AddRangeAsync(newMasterBookingPromotionItems);
            }
            if (newMasterBookingHouseModelItems.Count() > 0)
            {
                await DB.MasterBookingHouseModelItems.AddRangeAsync(newMasterBookingHouseModelItems);
            }
            if (newMasterBookingPromotionFreeItems.Count() > 0)
            {
                await DB.MasterBookingPromotionFreeItems.AddRangeAsync(newMasterBookingPromotionFreeItems);
            }
            if (newMasterBookingHouseModelFreeItems.Count() > 0)
            {
                await DB.MasterBookingHouseModelFreeItems.AddRangeAsync(newMasterBookingHouseModelFreeItems);
            }
            if (newMasterBookingCreditCardItem.Count() > 0)
            {
                await DB.MasterBookingCreditCardItems.AddRangeAsync(newMasterBookingCreditCardItem);
            }
            await DB.SaveChangesAsync();

            return await this.GetMasterBookingPromotionDetailAsync(newMasterBookingPromotion.ID);
        }

        public async Task<CloneMasterPromotionConfirmDTO> GetCloneMasterBookingPromotionConfirmAsync(Guid id)
        {
            CloneMasterPromotionConfirmDTO result = new CloneMasterPromotionConfirmDTO();

            result.CloneItemCount = await DB.MasterBookingPromotionItems
                .Where(o => o.MasterBookingPromotionID == id && o.ExpireDate != null && o.ExpireDate > DateTime.Now).CountAsync();
            result.ExpiredItemCount = await DB.MasterBookingPromotionItems
                .Where(o => o.MasterBookingPromotionID == id && o.ExpireDate != null && o.ExpireDate <= DateTime.Now).CountAsync();

            return result;
        }

        private async Task ValidatePromotionMaterial(Guid masterBookingPromotionID, PromotionMaterialDTO input)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(input.AgreementNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.AgreementNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(input.ItemNo))
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = input.GetType().GetProperty(nameof(PromotionMaterialDTO.ItemNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

    }
}
