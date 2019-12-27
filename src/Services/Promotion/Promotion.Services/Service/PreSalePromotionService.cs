using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public class PreSalePromotionService : IPreSalePromotionService
    {
        private readonly DatabaseContext DB;
        IConfiguration Configuration;
        private PRRequestJobService PRRequestJobService;
        private PRCancelJobService PRCancelJobService;
        public PreSalePromotionService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;
            this.PRRequestJobService = new PRRequestJobService(configuration, db);
            this.PRCancelJobService = new PRCancelJobService(configuration, db);
        }

        /// <summary>
        /// ดึงข้อมูลโปรก่อนขาย
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public async Task<PreSalePromotionDTO> GetPreSalePromotionAsync(Guid unitID)
        {
            var result = await PreSalePromotionDTO.CreateFromUnitAsync(unitID, DB);

            return result;
        }

        /// <summary>
        /// รายการเบิกโปรโมชั่นก่อนขาย (Paging, Sort, Filter)
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362358689/preview
        /// </summary>
        public async Task<PreSalePromotionRequestListPaging> GetPreSalePromotionRequestListAsync(PreSalePromotionRequestListFilter filter, PageParam pageParam, PreSalePromotionRequestListSortByParam sortByParam)
        {
            var query = await DB.PreSalePromotionRequests.Include(o => o.Project)
                                                         .Include(o => o.PromotionRequestPRStatus)
                                                         .Include(o => o.MasterPreSalePromotion)
                                                         .Include(o => o.UpdatedBy)
                                                         .ToListAsync();

            var results = query.Select(o => PreSalePromotionRequestListDTO.CreateFromModelAsync(o, DB)).Select(o => o.Result).ToList();

            #region Filter
            if (filter.RequestDateFrom != null)
            {
                results = results.Where(o => o.RequestDate >= filter.RequestDateFrom).ToList();
            }
            if (filter.RequestDateTo != null)
            {
                results = results.Where(o => o.RequestDate <= filter.RequestDateTo).ToList();
            }
            if (filter.ProjectID != null && filter.ProjectID != Guid.Empty)
            {
                results = results.Where(o => o.Project.Id == filter.ProjectID).ToList();
            }
            if (filter.PRCompletedDateFrom != null)
            {
                results = results.Where(o => o.PRCompletedDate <= filter.PRCompletedDateFrom).ToList();
            }
            if (filter.PRCompletedDateTo != null)
            {
                results = results.Where(o => o.PRCompletedDate <= filter.PRCompletedDateTo).ToList();
            }
            if (!string.IsNullOrEmpty(filter.PromotionRequestPRStatusKey))
            {
                var promotionRequestPRStatus = await DB.MasterCenters.Where(x => x.Key == filter.PromotionRequestPRStatusKey
                                                                     && x.MasterCenterGroupKey == "PromotionRequestPRStatus")
                                                                    .Select(x => x.ID).FirstAsync();
                results = results.Where(o => o.PromotionRequestPRStatus.Id == promotionRequestPRStatus).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UnitNo))
            {
                results = results.Where(o => o.Units.Select(p => p.UnitNo).Contains(filter.UnitNo)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.MasterPreSalePromotion_PromotionNo))
            {
                results = results.Where(o => o.MasterPreSalePromotion.PromotionNo.Contains(filter.MasterPreSalePromotion_PromotionNo)).ToList();
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
            #endregion


            PreSalePromotionRequestListDTO.SortBy(sortByParam, ref results);

            var pageOutput = PagingHelper.PagingList<PreSalePromotionRequestListDTO>(pageParam, ref results);

            return new PreSalePromotionRequestListPaging()
            {
                PageOutput = pageOutput,
                PreSalePromotionRequestLists = results
            };
        }

        /// <summary>
        /// ดึงรายละเอียดเบิกโปรโมชั่นก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204541/preview
        /// </summary>
        /// <param name="requestID">PreSalePromotionRequest.ID</param>
        /// <returns></returns>
        public async Task<PreSalePromotionRequestDTO> GetPreSalePromotionRequestAsync(Guid requestID)
        {
            var model = await DB.PreSalePromotionRequests.Where(o => o.ID == requestID)
                                                         .Include(o => o.Project)
                                                         .Include(o => o.PromotionRequestPRStatus)
                                                         .Include(o => o.MasterPreSalePromotion)
                                                         .Include(o => o.UpdatedBy)
                                                         .FirstOrDefaultAsync();
            var result = await PreSalePromotionRequestDTO.CreateFromModelAsync(model, DB);
            return result;
        }
        /// <summary>
        /// ดึงรายละเอียดแปลงเบิกโปรก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204542/preview
        /// </summary>
        /// <param name="requestUnitID">PreSalePromotionRequestUnit.ID</param>
        /// <returns></returns>
        public async Task<PreSalePromotionRequestUnitDTO> GetPreSalePromotionRequestUnitAsync(Guid requestUnitID)
        {
            var model = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == requestUnitID)
                                                             .Include(o => o.PromotionRequestPRJobType)
                                                             .Include(o => o.PreSalePromotionRequest)
                                                             .ThenInclude(o => o.MasterPreSalePromotion)
                                                             .Include(o => o.SAPPRStatus)
                                                             .Include(o => o.Unit)
                                                             .ThenInclude(o => o.Project)
                                                             .Include(o => o.UpdatedBy)
                                                             .FirstAsync();
            var result = await PreSalePromotionRequestUnitDTO.CreateFromModelAsync(model, DB);
            return result;
        }
        /// <summary>
        /// ดึงรายการโปรก่อนขายที่ Active อยู่ทั้งหมดจาก Master
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092477/preview
        /// </summary>
        /// <returns></returns>
        public async Task<List<PreSalePromotionRequestItemDTO>> GetPreSalePromotionItemsFormMasterAsync(Guid masterPreSalePromotionID)
        {
            var query = await DB.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == masterPreSalePromotionID && o.ExpireDate > DateTime.Now).ToListAsync();
            var results = query.Select(o => PreSalePromotionRequestItemDTO.CreateFromMasterModel(o)).ToList();
            return results;
        }
        /// <summary>
        /// บันทึกใบเบิกโปรก่อนขาย และสร้าง PR
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092474/preview
        /// </summary>
        /// <param name="units"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<PreSalePromotionRequestDTO> SaveRequestAndCreatePRAsync(Guid masterPreSalePromotionID, List<UnitDropdownSellPriceDTO> units, List<PreSalePromotionRequestItemDTO> items)
        {
            var masterPromotion = await DB.MasterPreSalePromotions.Include(o => o.Project).FirstAsync(o => o.ID == masterPreSalePromotionID);
            PreSalePromotionRequest model = new PreSalePromotionRequest();
            var promotionRequestPRStatusWaiting = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRStatus" && o.Key == "3").FirstOrDefaultAsync();

            string year = Convert.ToString(DateTime.Today.Year);
            var key = "RP" + masterPromotion.Project.ProjectNo + year[2] + year[3];
            var type = "PRM.PreSalePromotionRequest";
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

                model.RequestNo = key + runningNumberCounter.Count.ToString("0000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
            }
            else
            {
                model.RequestNo = key + runningno.Count.ToString("0000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
            }

            model.RequestDate = DateTime.Now;
            model.ProjectID = masterPromotion.ProjectID.Value;
            model.PromotionRequestPRStatusMasterCenterID = promotionRequestPRStatusWaiting.ID;
            model.MasterPreSalePromotionID = masterPreSalePromotionID;

            var promotionStatusActive = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).FirstOrDefaultAsync();
            var sAPPRStatusInProgress = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.InProgress).FirstOrDefaultAsync();
            var promotionRequestPRJobTypeCreate = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRJobType" && o.Key == PromotionRequestPRJobTypeKeys.CreatePR).FirstOrDefaultAsync();


            List<PreSalePromotionRequestItem> listCreateItems = new List<PreSalePromotionRequestItem>();
            List<PreSalePromotionRequestUnit> listCreateUnits = new List<PreSalePromotionRequestUnit>();
            foreach (var item in units)
            {
                PreSalePromotionRequestUnit preUnit = new PreSalePromotionRequestUnit();
                preUnit.PromotionRequestPRJobTypeMasterCenterID = promotionRequestPRJobTypeCreate.ID;
                preUnit.SAPPRStatusMasterCenterID = sAPPRStatusInProgress.ID;
                preUnit.PreSalePromotionRequestID = model.ID;
                preUnit.UnitID = item.Id;
                foreach (var item1 in items)
                {
                    PreSalePromotionRequestItem preItem = new PreSalePromotionRequestItem();
                    preItem.MasterPreSalePromotionItemID = item1.MasterPreSalePromotionItemID;
                    preItem.NameTH = item1.NameTH;
                    preItem.PreSalePromotionRequestUnitID = preUnit.ID;
                    preItem.PricePerUnit = item1.PricePerUnit;
                    preItem.Quantity = item1.Quantity;
                    preItem.TotalPrice = item1.TotalPrice;
                    preItem.ReceiveDate = item1.ReceiveDate;
                    preItem.Remark = item1.Remark;
                    listCreateItems.Add(preItem);
                }
                listCreateUnits.Add(preUnit);
            }

            await DB.PreSalePromotionRequests.AddAsync(model);
            await DB.PreSalePromotionRequestUnits.AddRangeAsync(listCreateUnits);
            await DB.PreSalePromotionRequestItems.AddRangeAsync(listCreateItems);
            await DB.SaveChangesAsync();
            var listCreateItemIds = listCreateItems.Select(o => o.ID).ToList();
            var listCreatePRRequestItems = await DB.PreSalePromotionRequestItems.Where(o => listCreateItemIds.Contains(o.ID))
                                                                                .Include(o => o.UpdatedBy)
                                                                                .Include(o => o.MasterPreSalePromotionItem)
                                                                                .Include(o => o.MasterPreSalePromotionItem)
                                                                                    .ThenInclude(o => o.MasterPreSalePromotion)
                                                                                        .ThenInclude(o => o.Project)
                                                                                .Include(o => o.PreSalePromotionRequestUnit)
                                                                                    .ThenInclude(o => o.Unit)
                                                                                .ToListAsync();
            await PRRequestJobService.CreateNewPreSalePRRequestJobAsync(listCreatePRRequestItems);
            var result = await this.GetPreSalePromotionRequestAsync(model.ID);
            return result;
        }

        /// <summary>
        /// สร้าง PR ใหม่ (จากที่ Failed)
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        public async Task<PreSalePromotionRequestUnitDTO> RetryCreatePRAsync(Guid requestUnitID)
        {
            var model = await DB.PreSalePromotionRequestUnits
                .Include(o => o.SAPPRStatus)
                .Include(o => o.PromotionRequestPRJobType)
                .Where(o => o.ID == requestUnitID).FirstOrDefaultAsync();
            //if not fail create task
            if (!(model.SAPPRStatus?.Key == "2" && model.PromotionRequestPRJobType?.Key == "1"))
            {
                ValidateException validateEx = new ValidateException();
                var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR9999");
                errMsg.Message = errMsg.Message.Replace("[message]", "ขอสร้าง PR ใหม่ได้เฉพาะแปลงที่ขอ PR Failed เท่านั้น");
                validateEx.AddError(errMsg.Key, errMsg.Message, (int)ErrorMessageType.PopupAlert);
                throw validateEx;
            }

            await PRRequestJobService.CreateRetrySyncJobAsync(requestUnitID);

            var result = await this.GetPreSalePromotionRequestUnitAsync(requestUnitID);
            return result;

        }

        /// <summary>
        /// ยกเลิก PR
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        public async Task<PreSalePromotionRequestUnitDTO> CancelPRAsync(Guid requestUnitID)
        {
            var model = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == requestUnitID).FirstOrDefaultAsync();

            var sAPPRStatusInProgress = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.InProgress).FirstOrDefaultAsync();
            var promotionRequestPRJobTypeCancel = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRJobType" && o.Key == PromotionRequestPRJobTypeKeys.CancelPR).FirstOrDefaultAsync();

            model.PromotionRequestPRJobTypeMasterCenterID = promotionRequestPRJobTypeCancel.ID;
            model.SAPPRStatusMasterCenterID = sAPPRStatusInProgress.ID;

            DB.Entry(model).State = EntityState.Modified;
            await DB.SaveChangesAsync();

            // PRCancel
            var preSaleRequestItems = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == model.ID)
                                                                           .Include(o => o.UpdatedBy)
                                                                           .Include(o => o.MasterPreSalePromotionItem)
                                                                           .ToListAsync();

            await PRCancelJobService.CreateNewPreSalePRCancelJobAsync(preSaleRequestItems);

            var result = await this.GetPreSalePromotionRequestUnitAsync(requestUnitID);
            return result;
        }
        /// <summary>
        /// ยกเลิก PR ทีละหลายรายการ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092509/preview
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task<List<PreSalePromotionRequestUnitDTO>> CancelMultiplePRAsync(List<PreSalePromotionRequestUnitListDTO> units)
        {
            var sAPPRStatusInProgress = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == SAPPRStatusKeys.InProgress).FirstOrDefaultAsync();
            var promotionRequestPRJobTypeCancel = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRJobType" && o.Key == PromotionRequestPRJobTypeKeys.CancelPR).FirstOrDefaultAsync();
            List<PreSalePromotionRequestUnit> listUpdate = new List<PreSalePromotionRequestUnit>();
            var preSalePromotionRequestUnitID = new List<Guid?>();
            foreach (var item in units)
            {
                var model = await DB.PreSalePromotionRequestUnits.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                model.PromotionRequestPRJobTypeMasterCenterID = promotionRequestPRJobTypeCancel.ID;
                model.SAPPRStatusMasterCenterID = sAPPRStatusInProgress.ID;
                preSalePromotionRequestUnitID.Add(model.ID);
                listUpdate.Add(model);
            }
            DB.PreSalePromotionRequestUnits.UpdateRange(listUpdate);
            await DB.SaveChangesAsync();
            // PRCancel
            var preSaleRequestItems = await DB.PreSalePromotionRequestItems.Include(o => o.MasterPreSalePromotionItem).Include(o => o.UpdatedBy).Where(o => preSalePromotionRequestUnitID.Contains(o.PreSalePromotionRequestUnitID)).ToListAsync();
            await PRCancelJobService.CreateNewPreSalePRCancelJobAsync(preSaleRequestItems);

            var result = listUpdate.Select(o => this.GetPreSalePromotionRequestUnitAsync(o.ID)).Select(o => o.Result).ToList();
            return result;
        }

        public async Task<StringResult> ExportPreSaleRequestPrintFormUrlAsync(Guid requestUnitID)
        {
            throw new NotImplementedException();
        }
    }
}
