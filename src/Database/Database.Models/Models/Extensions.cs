using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Database.Models
{
    public static class Extensions
    {
        public static InternalEntityEntry GetInternalEntityEntry(this EntityEntry entityEntry)
        {
            var internalEntry = (InternalEntityEntry)entityEntry
                .GetType()
                .GetProperty("InternalEntry", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entityEntry);

            return internalEntry;
        }

        public async static Task<PRJ.PriceList> GetActivePriceListAsync(this DbSet<PRJ.PriceList> models, Guid unitID)
        {
            var result = await (from p in models.Include(o => o.PriceListItems)
                                where p.ActiveDate <= DateTime.Now
                                && p.UnitID == unitID
                                orderby p.ActiveDate descending
                                select p).FirstOrDefaultAsync();
            return result;
        }

        public async static Task<Guid> GetIDAsync(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = await models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o.ID).FirstOrDefaultAsync();
            return result;
        }

        public static Guid GetID(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o.ID).FirstOrDefault();
            return result;
        }

        public async static Task<MST.MasterCenter> GetAsync(this DbSet<MST.MasterCenter> models, string masterCenterGroupKey, string key)
        {
            var result = await models.Where(o => o.MasterCenterGroupKey == masterCenterGroupKey && o.Key == key).Select(o => o).FirstOrDefaultAsync();
            return result;
        }

        public async static Task<MST.ErrorMessage> GetAsync(this DbSet<MST.ErrorMessage> models, string key)
        {
            var result = await models.Where(o => o.Key == key).Select(o => o).FirstOrDefaultAsync();
            return result;
        }

        public async static Task CreateBudgetPromotionSyncJobAsync(this List<PRJ.BudgetPromotion> budgetPromotions, DatabaseContext db)
        {
            var masterCenterBudgetPromotionTypeSaleID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Sale).Select(o => o.ID).FirstAsync();
            var masterCenterBudgetPromotionTypeTransferID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionType && o.Key == BudgetPromotionTypeKeys.Transfer).Select(o => o.ID).FirstAsync();

            var model = new PRJ.BudgetPromotionSyncJob();

            model.Status = BackgroundJobStatus.Waiting;

            var syncItems = new List<PRJ.BudgetPromotionSyncItem>();
            var temp = budgetPromotions.GroupBy(o => o.UnitID).Select(o => new
            {
                Unit = db.Units.Where(p => p.ID == o.Key).FirstOrDefault(),
                BudgetPromotions = o.Select(p => p).ToList()
            }).ToList();

            var data = temp.Select(o => new
            {
                Unit = o.Unit,
                BudgetPromotionSale = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeSaleID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault(),
                BudgetPromotionTransfer = o.BudgetPromotions.Where(p => p.BudgetPromotionTypeMasterCenterID == masterCenterBudgetPromotionTypeTransferID && p.ActiveDate <= DateTime.Now).OrderByDescending(p => p.ActiveDate).FirstOrDefault()
            }).ToList();


            var budgetPromotionSyncStatusSyncingMasterCenterID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.BudgetPromotionSyncStatus && o.Key == BudgetPromotionSyncStatusKeys.Syncing).Select(o => o.ID).FirstAsync();

            foreach (var item in data)
            {
                var unit = await db.Units.Where(o => o.ID == item.Unit.ID).FirstOrDefaultAsync();
                var synItem = new PRJ.BudgetPromotionSyncItem();
                synItem.BudgetPromotionSyncJobID = model.ID;
                synItem.SAPWBSObject_P = unit.SAPWBSObject_P;
                synItem.SaleBudgetPromotionID = item.BudgetPromotionSale?.ID;
                synItem.TransferBudgetPromotionID = item.BudgetPromotionTransfer?.ID;
                synItem.Amount = Convert.ToDecimal(item.BudgetPromotionSale?.Budget + item.BudgetPromotionTransfer?.Budget);
                synItem.BudgetPromotionSyncStatusMasterCenterID = budgetPromotionSyncStatusSyncingMasterCenterID;
                synItem.Retry = 0;
                synItem.Currency = "THB";
                syncItems.Add(synItem);
            }

            await db.BudgetPromotionSyncJobs.AddAsync(model);

            await db.BudgetPromotionSyncItems.AddRangeAsync(syncItems);

            await db.SaveChangesAsync();
        }

        public async static Task<bool> CheckCalendarAsync(this DatabaseContext db, Guid gID, DateTime ChkdDate)
        {
            var chkCalendar = await db.CalendarLocks.Where(o => o.CompanyID == gID && o.LockDate == ChkdDate && !o.IsDeleted).FirstOrDefaultAsync() ?? new ACC.CalendarLock();
            bool resCalendar = chkCalendar.IsLocked;
            return resCalendar;
        }
        public async static Task<bool> CheckDepositAsync(this DatabaseContext db, Guid PaymentMethodID)
        {
            var DepositDetail = await db.DepositDetails.Include(o => o.DepositHeader).Where(x => x.PaymentMethodID == PaymentMethodID && !x.IsDeleted).FirstOrDefaultAsync() ?? new FIN.DepositDetail();
            bool resDeposit = false;
            if (DepositDetail?.DepositHeader?.DepositNo != null)
                resDeposit = true;

            return resDeposit;
        }
    }
}
