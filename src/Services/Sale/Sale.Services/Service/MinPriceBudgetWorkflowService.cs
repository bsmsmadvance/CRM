using Base.DTOs.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Base.DTOs.USR;
using Common;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.SAL;
using Finance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sale.Params.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public class MinPriceBudgetWorkflowService : IMinPriceBudgetWorkflowService
    {
        private readonly DatabaseContext DB;
        private IPaymentService PaymentService;
        private IBookingService BookingService;

        public MinPriceBudgetWorkflowService(DatabaseContext db, IPaymentService paymentService, IBookingService bookingService)
        {
            this.DB = db;
            this.PaymentService = paymentService;
            this.BookingService = bookingService;
        }
        public async Task<AdhocDTO> GetAdhocAsync(BudgetMinPriceWorkflowFilter filter)
        {
            var minPriceWorkFlowTypeAdhocMoreThan5PercentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType
                                                                                            && o.Key == MinPriceWorkflowTypeKeys.AdhocMoreThan5Percent)
                                                                                  .Select(o => o.ID)
                                                                                  .FirstAsync();
            var minPriceWorkFlowTypeAdhocLessThan5PercentMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType
                                                                                && o.Key == MinPriceWorkflowTypeKeys.AdhocLessThan5Percent)
                                                                      .Select(o => o.ID)
                                                                      .FirstAsync();

            var minPriceBudgetWorkflowStageBooking = new List<string>
                {
                    MinPriceBudgetWorkflowStageKeys.Booking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitBooking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitContract
                    , MinPriceBudgetWorkflowStageKeys.Contract
                };

            var achocBookingUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                                .Where(o => o.ProjectID == filter.ProjectID
                                                                                       && minPriceBudgetWorkflowStageBooking.Contains(o.MinPriceBudgetWorkflowStage.Key)
                                                                                       && (o.MinPriceWorkflowTypeMasterCenterID == minPriceWorkFlowTypeAdhocMoreThan5PercentMasterCenterID
                                                                                        || o.MinPriceWorkflowTypeMasterCenterID == minPriceWorkFlowTypeAdhocLessThan5PercentMasterCenterID)
                                                                                       && o.IsApproved == true
                                                                                       && o.IsCancelled == false)
                                                                                .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);

            var achocTransferUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                               .Where(o => o.ProjectID == filter.ProjectID
                                                                                      && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.PromotionTransfer
                                                                                      && (o.MinPriceWorkflowTypeMasterCenterID == minPriceWorkFlowTypeAdhocMoreThan5PercentMasterCenterID
                                                                                       || o.MinPriceWorkflowTypeMasterCenterID == minPriceWorkFlowTypeAdhocLessThan5PercentMasterCenterID)
                                                                                      && o.IsApproved == true
                                                                                      && o.IsCancelled == false)
                                                                               .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);
            var result = new AdhocDTO
            {
                AdhocChargeBooking = achocBookingUsedBudget,
                AdhocChargeTransfer = achocTransferUsedBudget,
                AdhocChargeTotal = achocBookingUsedBudget + achocTransferUsedBudget,
                UsedBooking = 0,
                UsedTotal = 0,
                UsedTransfer = 0
            };

            return result;
        }

        public async Task<BudgetQuarterlyDTO> GetBudgetQuarterlyAsync(BudgetMinPriceWorkflowFilter filter)
        {
            if (filter.ProjectID != null)
            {
                var budgetMinPriceTypeQuarterlyMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType
                                                                                                && o.Key == BudgetMinPriceTypeKeys.Quarterly)
                                                                                      .Select(o => o.ID)
                                                                                      .FirstAsync();
                var budgetMinPriceTypeTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType
                                                                                                && o.Key == BudgetMinPriceTypeKeys.Quarterly)
                                                                                     .Select(o => o.ID)
                                                                                     .FirstAsync();

                var budgetMinPriceQuarterly = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                                    && o.Year == filter.Year
                                                                    && o.Quarter == filter.Quarter
                                                                    && o.BudgetMinPriceTypeMasterCenterID == budgetMinPriceTypeQuarterlyMasterCenterID
                                                             ).OrderByDescending(o => o.ActiveDate)
                                                             .FirstOrDefaultAsync();

                var budgetMinPriceTransfer = await DB.BudgetMinPrices.Where(o => o.ProjectID == filter.ProjectID
                                                                    && o.Year == filter.Year
                                                                    && o.Quarter == filter.Quarter
                                                                    && o.BudgetMinPriceTypeMasterCenterID == budgetMinPriceTypeTransferMasterCenterID
                                                             ).OrderByDescending(o => o.ActiveDate)
                                                             .FirstOrDefaultAsync();

                var minPriceTypeQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType
                                                                            && o.Key == MinPriceWorkflowTypeKeys.Quarterly)
                                                                    .Select(o => o.ID)
                                                                    .FirstAsync();

                var minPriceBudgetWorkflowStageBooking = new List<string>
                {
                    MinPriceBudgetWorkflowStageKeys.Booking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitBooking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitContract
                    , MinPriceBudgetWorkflowStageKeys.Contract
                };

                var quarterlyBookingUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                                 .Where(o => o.ProjectID == filter.ProjectID
                                                                                        && minPriceBudgetWorkflowStageBooking.Contains(o.MinPriceBudgetWorkflowStage.Key)
                                                                                        && o.MinPriceWorkflowTypeMasterCenterID == minPriceTypeQuarterlyID
                                                                                        && o.IsApproved == true
                                                                                        && o.IsCancelled == false)
                                                                                 .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);
                var quarterlyTransferUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                                  .Where(o => o.ProjectID == filter.ProjectID
                                                                                          && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.PromotionTransfer
                                                                                          && o.MinPriceWorkflowTypeMasterCenterID == minPriceTypeQuarterlyID
                                                                                          && o.IsApproved == true
                                                                                          && o.IsCancelled == false)
                                                                                  .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);

                var result = new BudgetQuarterlyDTO
                {
                    TotalBookingBudget = budgetMinPriceQuarterly?.TotalAmount ?? 0,
                    CurrentBookingBudget = 0,
                    UsedBookingBudget = quarterlyBookingUsedBudget ?? 0,
                    RemainBookingBudget = (budgetMinPriceQuarterly?.TotalAmount - quarterlyBookingUsedBudget) ?? 0,
                    TotalTransferBudget = budgetMinPriceTransfer?.TotalAmount ?? 0,
                    RemainTransferBudget = (budgetMinPriceTransfer?.TotalAmount - quarterlyTransferUsedBudget) ?? 0,
                    UsedTransferBudget = quarterlyTransferUsedBudget ?? 0,
                    CurrentTransferBudget = 0,
                };
                return result;
            }
            else
            {
                var budgetMinPriceTypeQuarterlyMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType
                                                                                                               && o.Key == BudgetMinPriceTypeKeys.Quarterly)
                                                                                      .Select(o => o.ID)
                                                                                      .FirstAsync();
                var budgetMinPriceTypeTransferMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType
                                                                                                && o.Key == BudgetMinPriceTypeKeys.Quarterly)
                                                                                     .Select(o => o.ID)
                                                                                     .FirstAsync();

                var budgetMinPriceQuarterlys = await DB.BudgetMinPrices.Where(o => o.Year == filter.Year
                                                                    && o.Quarter == filter.Quarter
                                                                    && o.BudgetMinPriceTypeMasterCenterID == budgetMinPriceTypeQuarterlyMasterCenterID
                                                                     ).OrderByDescending(o => o.ActiveDate)
                                                                     .ToListAsync();

                var budgetMinPriceTransfers = await DB.BudgetMinPrices.Where(o => o.Year == filter.Year
                                                                    && o.Quarter == filter.Quarter
                                                                    && o.BudgetMinPriceTypeMasterCenterID == budgetMinPriceTypeTransferMasterCenterID
                                                                     ).OrderByDescending(o => o.ActiveDate)
                                                                     .ToListAsync();

                var minPriceTypeQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.MinPriceWorkflowType
                                                                            && o.Key == MinPriceWorkflowTypeKeys.Quarterly)
                                                                    .Select(o => o.ID)
                                                                    .FirstAsync();

                var minPriceBudgetWorkflowStageBooking = new List<string>
                {
                    MinPriceBudgetWorkflowStageKeys.Booking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitBooking
                    , MinPriceBudgetWorkflowStageKeys.ChangeUnitContract
                    , MinPriceBudgetWorkflowStageKeys.Contract
                };

                var quarterlyBookingUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                                 .Where(o => minPriceBudgetWorkflowStageBooking.Contains(o.MinPriceBudgetWorkflowStage.Key)
                                                                                        && o.MinPriceWorkflowTypeMasterCenterID == minPriceTypeQuarterlyID
                                                                                        && o.IsApproved == true
                                                                                        && o.IsCancelled == false)
                                                                                 .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);
                var quarterlyTransferUsedBudget = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage)
                                                                                  .Where(o => !minPriceBudgetWorkflowStageBooking.Contains(o.MinPriceBudgetWorkflowStage.Key)
                                                                                          && o.MinPriceWorkflowTypeMasterCenterID == minPriceTypeQuarterlyID
                                                                                          && o.IsApproved == true
                                                                                          && o.IsCancelled == false)
                                                                                  .SumAsync(o => (decimal?)o.MasterMinPrice - o.RequestMinPrice);

                var result = new BudgetQuarterlyDTO
                {
                    TotalBookingBudget = budgetMinPriceQuarterlys.Sum(o => o.TotalAmount),
                    CurrentBookingBudget = 0,
                    UsedBookingBudget = quarterlyBookingUsedBudget ?? 0,
                    RemainBookingBudget = (budgetMinPriceQuarterlys.Sum(o => o.TotalAmount) - quarterlyBookingUsedBudget) ?? 0,
                    TotalTransferBudget = budgetMinPriceTransfers.Sum(o => o.TotalAmount),
                    RemainTransferBudget = (budgetMinPriceTransfers.Sum(o => o.TotalAmount) - quarterlyTransferUsedBudget) ?? 0,
                    UsedTransferBudget = quarterlyTransferUsedBudget ?? 0,
                    CurrentTransferBudget = 0,
                };
                return result;
            }
        }

        public async Task<List<MinPriceBudgetWorkflowDTO>> GetMinPriceBudgetWorkFlowsAsync(BudgetMinPriceWorkflowFilter filter, Guid? userID)
        {
            var queryMnBgWf = DB.MinPriceBudgetWorkflows.AsQueryable();
            if (filter.ProjectID != null)
            {
                queryMnBgWf = queryMnBgWf.Where(o => o.ProjectID == filter.ProjectID);
            }
            var minWfs = await queryMnBgWf.Include(o => o.Project)
                                          .Include(o => o.Booking)
                                               .ThenInclude(o => o.Unit)
                                                   .ThenInclude(o => o.UnitStatus)
                                          .Include(o => o.MinPriceBudgetWorkflowStage)
                                          .Include(o => o.MinPriceWorkflowType)
                                          .Include(o => o.BudgetPromotionType)
                                          .Where(o => o.IsApproved == null)
                                          .ToListAsync();
            var minWfIds = minWfs.Select(o => o.ID).ToList();

            var approvalMinWfs = await DB.MinPriceBudgetApprovals.Include(o => o.Role)
                                                            .Where(o => minWfIds.Contains(o.MinPriceBudgetWorkflowID))
                                                            .ToListAsync();

            approvalMinWfs = approvalMinWfs.GroupBy(o => o.MinPriceBudgetWorkflowID)
                                                            .Select(o =>
                                                                 o.Where(p => p.IsApproved == null).Select(p => p).OrderBy(p => p.Order).FirstOrDefault()
                                                                    )
                                                            .ToList();

            var query = from minWf in minWfs
                        join approve in approvalMinWfs
                        on minWf.ID equals approve?.MinPriceBudgetWorkflowID
                        select new BudgetMinPriceWorkFlowQueryResult
                        {
                            MinPriceBudgetWorkflow = minWf,
                            MinPriceBudgetApproval = approve
                        };

            var queryResult = query.ToList();

            var result = queryResult.Select(o => MinPriceBudgetWorkflowDTO.CreateFromQueryResultAsync(o, DB, userID).Result).ToList();

            return result;
        }

        public async Task<List<MinPriceBudgetApprovalDTO>> GetMinPriceBudgetApprovalAsync(MinPriceBudgetApprovalFilter filter)
        {
            var workflow = new MinPriceBudgetWorkflow();

            if (filter.BookingID != null)
                workflow = await DB.MinPriceBudgetWorkflows.Where(o => o.BookingID == filter.BookingID).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            if (filter.ChangePromotionWorkflowID != null)
                workflow = await DB.MinPriceBudgetWorkflows.Where(o => o.ChangePromotionWorkflowID == filter.ChangePromotionWorkflowID).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            var results = new List<MinPriceBudgetApprovalDTO>();
            if (workflow != null)
            {
                var approvals = await DB.MinPriceBudgetApprovals.Include(o => o.Role)
                                                                .Include(o => o.User)
                                                                .Include(o => o.UpdatedBy)
                                                                .Where(o => o.MinPriceBudgetWorkflowID == workflow.ID)
                                                                .OrderBy(o => o.Order)
                                                                .ToListAsync();

                results = approvals.Select(o => MinPriceBudgetApprovalDTO.CreateFromModel(o)).ToList();

                if (workflow.CreatedByUserID != null)
                {
                    var user = await DB.UserRoles
                        .Include(o => o.Role)
                        .Include(o => o.User)
                        .Where(o => o.UserID == workflow.CreatedByUserID).FirstAsync();

                    var requester = new MinPriceBudgetApprovalDTO
                    {
                        MinPriceBudgetWorkflowID = workflow.ID,
                        Order = 0,
                        RoleName = user.Role?.Name,
                        User = UserListDTO.CreateFromModel(user.User),
                        ApprovedDate = workflow.Created,
                        IsRequest = true
                    };

                    results.Add(requester);
                    results.OrderBy(o => o.Order);
                }
            }

            return results;
        }

        public async Task ApproveMinPriceBudgetWorkFlowAsync(List<MinPriceBudgetWorkflowDTO> minPriceBudgetWorkFlows)
        {
            foreach (var item in minPriceBudgetWorkFlows)
            {
                var minPriceBudgetWorkflow = await DB.MinPriceBudgetWorkflows.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                var minPriceApproval = await DB.MinPriceBudgetApprovals.Where(o => o.MinPriceBudgetWorkflowID == minPriceBudgetWorkflow.ID
                                                                                && o.IsApproved == null)
                                                                       .OrderBy(o => o.Order)
                                                                       .FirstOrDefaultAsync();
                minPriceApproval.IsApproved = true;
                DB.MinPriceBudgetApprovals.Update(minPriceApproval);
                await DB.SaveChangesAsync();

                // UpdateStatusMinPriceBudgetWorkflow
                await UpdateStatusMinPriceBudgetWorkFlow(minPriceBudgetWorkflow.ID);
            }
        }

        public async Task RejectMinPriceBudgetWorkFlowAsync(List<MinPriceBudgetWorkflowDTO> minPriceBudgetWorkFlows, string rejectComment)
        {
            var unitStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus).ToListAsync();
            var bookingStatusBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                                    && o.Key == BookingStatusKeys.Booking)
                                                                           .Select(o => o.ID)
                                                                           .FirstAsync();
            foreach (var item in minPriceBudgetWorkFlows)
            {
                var minPriceBudgetWorkflow = await DB.MinPriceBudgetWorkflows.Where(o => o.ID == item.Id).FirstOrDefaultAsync();
                var minPriceApproval = await DB.MinPriceBudgetApprovals.Where(o => o.MinPriceBudgetWorkflowID == minPriceBudgetWorkflow.ID
                                                                                && o.IsApproved == null)
                                                                       .OrderBy(o => o.Order)
                                                                       .FirstOrDefaultAsync();
                minPriceApproval.IsApproved = false;
                DB.MinPriceBudgetApprovals.Update(minPriceApproval);
                await DB.SaveChangesAsync();


                var fromChangeUnit = await CheckChangeUnitWorkFlow(minPriceBudgetWorkflow.BookingID.Value);
                if (fromChangeUnit)
                {
                    var newBooking = await DB.Bookings.Where(o => o.ID == minPriceBudgetWorkflow.BookingID.Value).FirstAsync();
                    var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ToBookingID == newBooking.ID)
                                                                    .FirstAsync();
                    var oldBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.FromBookingID)
                                                      .FirstAsync();

                    var oldUnit = await DB.Units.Where(o => o.ID == oldBooking.UnitID)
                                                .FirstAsync();
                    var newUnit = await DB.Units.Where(o => o.ID == newBooking.UnitID)
                                                .FirstAsync();

                    newUnit.UnitStatusMasterCenterID = unitStatusMasterCenters.Where(o => o.Key == UnitStatusKeys.Available).Select(o => o.ID).FirstOrDefault();
                    oldBooking.BookingStatusMasterCenterID = bookingStatusBookingMasterCenterID;
                    changeUnitWorkflow.IsApproved = false;

                    // Delete New Booking
                    await BookingService.DeleteBookingAsync(newBooking.ID);

                    DB.Units.Update(newUnit);
                    DB.Bookings.Update(oldBooking);
                    DB.ChangeUnitWorkflows.Update(changeUnitWorkflow);
                    await DB.SaveChangesAsync();
                }

                // UpdateStatusMinPriceBudgetWorkflow
                await UpdateStatusMinPriceBudgetWorkFlow(minPriceBudgetWorkflow.ID, rejectComment, minPriceApproval.UpdatedByUserID);
            }
        }

        private async Task UpdateStatusMinPriceBudgetWorkFlow(Guid minPriceBudgetWorkFlowID, string rejectComment = null, Guid? updateByUserID = null)
        {
            var minPriceBudgetWorkflow = await DB.MinPriceBudgetWorkflows.Where(o => o.ID == minPriceBudgetWorkFlowID)
                                                                         .Include(o => o.Booking)
                                                                         .FirstAsync();
            var minPriceApprovals = await DB.MinPriceBudgetApprovals.Where(o => o.MinPriceBudgetWorkflowID == minPriceBudgetWorkFlowID).ToListAsync();
            if (minPriceApprovals.TrueForAll(o => o.IsApproved == true))
            {
                minPriceBudgetWorkflow.IsApproved = true;

                if (minPriceBudgetWorkflow.ChangePromotionWorkflowID != null)
                {
                    var oldPromotion = await DB.BookingPromotions.Where(o => o.BookingID == minPriceBudgetWorkflow.BookingID && o.IsActive == true).FirstOrDefaultAsync();
                    if (oldPromotion != null)
                    {
                        oldPromotion.IsActive = false;
                        DB.BookingPromotions.Update(oldPromotion);
                    }

                    var newPromotion = await DB.BookingPromotions.Where(o => o.BookingID == minPriceBudgetWorkflow.BookingID && o.ChangePromotionWorkflowID == minPriceBudgetWorkflow.ChangePromotionWorkflowID).FirstAsync();
                    newPromotion.IsActive = true;
                    DB.BookingPromotions.Update(newPromotion);

                    var changePromotionWorkflow = await DB.ChangePromotionWorkflows.Where(o => o.ID == minPriceBudgetWorkflow.ChangePromotionWorkflowID).FirstAsync();
                    changePromotionWorkflow.IsApproved = true;
                    DB.ChangePromotionWorkflows.Update(changePromotionWorkflow);
                }
            }
            else if (minPriceApprovals.TrueForAll(o => o.IsApproved == null))
            {
                minPriceBudgetWorkflow.IsApproved = null;
            }
            else if (minPriceApprovals.Any(o => o.IsApproved == false))
            {
                minPriceBudgetWorkflow.IsApproved = false;
                minPriceBudgetWorkflow.RejectComment = rejectComment;
                minPriceBudgetWorkflow.RejectedTime = DateTime.Now;
                minPriceBudgetWorkflow.RejectedByUserID = updateByUserID;

                if (minPriceBudgetWorkflow.ChangePromotionWorkflowID != null)
                {
                    var ChangePromotionWorkflow = await DB.ChangePromotionWorkflows.Where(o => o.ID == minPriceBudgetWorkflow.ChangePromotionWorkflowID).FirstAsync();
                    ChangePromotionWorkflow.IsApproved = false;
                    DB.ChangePromotionWorkflows.Update(ChangePromotionWorkflow);
                }
            }
            DB.MinPriceBudgetWorkflows.Update(minPriceBudgetWorkflow);
            await DB.SaveChangesAsync();

            if (minPriceBudgetWorkflow.IsApproved == true)
            {
                await UpdateBookingStatus((Guid)minPriceBudgetWorkflow.BookingID, updateByUserID);
                if (minPriceBudgetWorkflow.IsRequestBudgetPromotion)
                {
                    #region CreateBudgetPromotion
                    var masterCenterBudgetPromotionTypeSaleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
                    var masterCenterBudgetPromotionTypeTransferID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

                    var budgetPromotions = new List<BudgetPromotion>();
                    if (minPriceBudgetWorkflow.BookingPromotionID != null)
                    {
                        BudgetPromotion modelSale = new BudgetPromotion();
                        modelSale.UnitID = minPriceBudgetWorkflow.Booking.UnitID;
                        modelSale.ProjectID = (Guid)minPriceBudgetWorkflow.ProjectID;
                        modelSale.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeSaleID;
                        modelSale.ActiveDate = DateTime.Now;
                        modelSale.Budget = minPriceBudgetWorkflow.RequestBudgetPromotion;

                        await DB.BudgetPromotions.AddAsync(modelSale);
                        budgetPromotions.Add(modelSale);
                    }
                    if (minPriceBudgetWorkflow.TransferPromotionID != null)
                    {
                        BudgetPromotion modelTransfer = new BudgetPromotion();
                        modelTransfer.UnitID = minPriceBudgetWorkflow.Booking.UnitID;
                        modelTransfer.ProjectID = (Guid)minPriceBudgetWorkflow.ProjectID;
                        modelTransfer.BudgetPromotionTypeMasterCenterID = masterCenterBudgetPromotionTypeTransferID;
                        modelTransfer.ActiveDate = DateTime.Now;
                        modelTransfer.Budget = minPriceBudgetWorkflow.RequestBudgetPromotion;

                        await DB.BudgetPromotions.AddAsync(modelTransfer);
                        budgetPromotions.Add(modelTransfer);
                    }
                    await DB.SaveChangesAsync();
                    #endregion

                    #region GetLastedBudgetPromotion
                    var query = await DB.BudgetPromotions.Include(o => o.UpdatedBy).Where(o => o.ProjectID == minPriceBudgetWorkflow.ProjectID && o.UnitID == minPriceBudgetWorkflow.Booking.UnitID)
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

                    var data = temp.Select(o => new BudgetPromotionQueryResult
                    {
                        Unit = o.Unit,
                        BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                        BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
                    }).FirstOrDefault();
                    #endregion

                    budgetPromotions.Add(data.BudgetPromotionSale);
                    budgetPromotions.Add(data.BudgetPromotionTransfer);

                    await budgetPromotions.CreateBudgetPromotionSyncJobAsync(DB);
                }
                if (minPriceBudgetWorkflow.IsRequestMinPrice)
                {
                    #region GetOldMinPrice
                    var query = await DB.MinPrices.GroupJoin(DB.TitledeedDetails, minprice => minprice.UnitID, titledeed => titledeed.UnitID,
                                        (minprice, titledeed) => new { Minprice = minprice, TitleDeed = titledeed })
                                        .Where(o => o.Minprice.ProjectID == minPriceBudgetWorkflow.ProjectID && o.Minprice.UnitID == minPriceBudgetWorkflow.Booking.UnitID)
                                        .Select(o => new MinPriceQueryResult
                                        {
                                            MinPrice = o.Minprice,
                                            MinPriceType = o.Minprice.MinPriceType,
                                            DocType = o.Minprice.DocType,
                                            Unit = o.Minprice.Unit,
                                            UpdatedBy = o.Minprice.UpdatedBy,
                                            Titledeed = o.TitleDeed.FirstOrDefault()
                                        }).ToListAsync();

                    var data = query.GroupBy(o => o.Unit).Select(o => new MinPriceQueryResult
                    {
                        Unit = o.Key,
                        Titledeed = o.Select(p => p.Titledeed).FirstOrDefault(),
                        MinPrice = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                        DocType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.DocType).FirstOrDefault(),
                        MinPriceType = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.MinPriceType).FirstOrDefault(),
                        UpdatedBy = o.Where(p => p.MinPrice.ActiveDate <= DateTime.Now).Select(p => p.MinPrice).OrderByDescending(p => p.ActiveDate).Select(p => p.UpdatedBy).FirstOrDefault()
                    }).FirstOrDefault();

                    #endregion

                    #region CreateMinPrice
                    MinPrice model = new MinPrice();
                    model.UnitID = minPriceBudgetWorkflow.Booking.UnitID;
                    model.Cost = data.MinPrice.Cost;
                    model.MinPriceTypeMasterCenterID = data.MinPriceType?.ID;
                    model.ROIMinprice = data.MinPrice.ROIMinprice;
                    model.SalePrice = data.MinPrice.SalePrice;
                    model.DocTypeMasterCenterID = data.DocType?.ID;
                    model.ProjectID = minPriceBudgetWorkflow.ProjectID;
                    model.ActiveDate = DateTime.Now;
                    model.ApprovedMinPrice = minPriceBudgetWorkflow.RequestMinPrice;

                    await DB.MinPrices.AddAsync(model);
                    await DB.SaveChangesAsync();
                    #endregion
                }
            }
            else if (minPriceBudgetWorkflow.IsApproved == false)
            {
                var quarter = minPriceBudgetWorkflow.Created.Value.GetQuarter();
                await UpdateBudgetMinPriceQuarterly(minPriceBudgetWorkflow, (Guid)minPriceBudgetWorkflow.ProjectID, minPriceBudgetWorkflow.Created.Value.Year, quarter);
            }
        }

        private async Task UpdateBookingStatus(Guid bookingID, Guid? userID = null)
        {
            var bookingStatusBookingMasterCenterID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BookingStatus
                                                                                    && o.Key == BookingStatusKeys.Booking)
                                                                           .Select(o => o.ID)
                                                                           .FirstAsync();
            var unitStatusMasterCenters = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.UnitStatus).ToListAsync();
            var booking = await DB.Bookings.Where(o => o.ID == bookingID).FirstAsync();
            var fromChangeUnit = await CheckChangeUnitWorkFlow(booking.ID);
            if (fromChangeUnit)
            {
                var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ToBookingID == bookingID)
                                                                     .FirstAsync();
                var oldBooking = await DB.Bookings.Where(o => o.ID == changeUnitWorkflow.FromBookingID)
                                                  .FirstAsync();
                var oldPayments = await DB.Payments.Where(o => o.BookingID == oldBooking.ID)
                                                   .ToListAsync();
                if (oldPayments.Count() > 0)
                {
                    #region Create Payments
                    booking.BookingStatusMasterCenterID = bookingStatusBookingMasterCenterID;
                    DB.Bookings.Update(booking);
                    await DB.SaveChangesAsync();

                    var oldPaymentIds = oldPayments.Select(o => o.ID).ToList();
                    var oldPaymentsMethod = await DB.PaymentMethods.Where(o => oldPaymentIds.Contains(o.PaymentID)).ToListAsync();

                    var sumOldPayments = oldPayments.Sum(o => o.TotalAmount);

                    var paymentMethodTypeChangeUnit = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PaymentMethod
                                                                                    && o.Key == PaymentMethodKeys.ChangeContract)
                                                                            .FirstAsync();

                    var paymentNewBooking = await PaymentService.GetPaymentFormAsync(booking.ID);

                    paymentNewBooking.ReceiveDate = DateTime.Now;
                    paymentNewBooking.PaymentFormType = PaymentFormType.ChangeUnit;
                    paymentNewBooking.RefID = oldBooking.ID;
                    paymentNewBooking.PaymentMethods = new List<PaymentMethodDTO>()
                {
                    new PaymentMethodDTO
                            {
                                PayAmount= sumOldPayments,
                                PaymentMethodType = MasterCenterDropdownDTO.CreateFromModel(paymentMethodTypeChangeUnit),
                            },
                };
                    await PaymentService.SubmitPaymentFormAsync(booking.ID, paymentNewBooking);
                    #endregion

                    //Cancel OldBooking
                    var cancelMemo = await BookingService.GetCancelMemoFormAsync(oldBooking.ID);
                    await BookingService.CancelBookingAsync(oldBooking.ID, cancelMemo, userID.Value);
                }
                else
                {
                    await BookingService.DeleteBookingAsync(oldBooking.ID);
                }
            }
            booking.BookingStatusMasterCenterID = bookingStatusBookingMasterCenterID;
            DB.Bookings.Update(booking);
            await DB.SaveChangesAsync();
        }

        private async Task UpdateBudgetMinPriceQuarterly(MinPriceBudgetWorkflow minPriceBudgetWorkflow, Guid projectID, int year, int quarter)
        {
            var masterCenterQuarterlyID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetMinPriceType && o.Key == BudgetMinPriceTypeKeys.Quarterly).Select(o => o.ID).FirstAsync();
            var budgetMinPriceQuarterly = await DB.BudgetMinPrices
                                                .Include(o => o.BudgetMinPriceType)
                                                .Where(o => o.ProjectID == projectID
                                                           && o.Quarter == quarter
                                                           && o.Year == year
                                                           && o.BudgetMinPriceType.Key == BudgetMinPriceTypeKeys.Quarterly
                                                           && o.ActiveDate <= DateTime.Now
                                                ).OrderByDescending(o => o.ActiveDate)
                                                .FirstAsync();

            var newBudgetMinPriceQuarter = new BudgetMinPrice();
            newBudgetMinPriceQuarter.Quarter = quarter;
            newBudgetMinPriceQuarter.Year = year;
            newBudgetMinPriceQuarter.UsedAmount = budgetMinPriceQuarterly.UsedAmount - minPriceBudgetWorkflow.RequestMinPrice;
            newBudgetMinPriceQuarter.TotalAmount = budgetMinPriceQuarterly.TotalAmount;
            newBudgetMinPriceQuarter.ActiveDate = DateTime.Now;
            newBudgetMinPriceQuarter.ProjectID = projectID;
            newBudgetMinPriceQuarter.UnitAmount = budgetMinPriceQuarterly.UnitAmount;
            await DB.BudgetMinPrices.AddAsync(newBudgetMinPriceQuarter);
            await DB.SaveChangesAsync();
        }

        private async Task<bool> CheckChangeUnitWorkFlow(Guid bookingID)
        {
            var changeUnitWorkflow = await DB.ChangeUnitWorkflows.Where(o => o.ToBookingID == bookingID && o.IsApproved == null).FirstOrDefaultAsync();
            return changeUnitWorkflow != null ? true : false;
        }
    }
}
