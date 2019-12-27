using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models.PRM;

namespace Promotion.Services
{
    public interface IPRCancelJobService
    {
        Task CreateNewPreSalePRCancelJobAsync(List<PreSalePromotionRequestItem> preSaleRequestItems);
        Task RunWaitingSyncJobAsync();
        Task ReadSyncResultFromSAPAsync();
        Task CreateRetrySyncJobAsync(Guid requestUnitID);
    }
}
