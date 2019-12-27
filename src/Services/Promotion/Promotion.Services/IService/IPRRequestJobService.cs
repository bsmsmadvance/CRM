using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Models.PRM;

namespace Promotion.Services
{
    public interface IPRRequestJobService
    {
        Task CreateNewPreSalePRRequestJobAsync(List<PreSalePromotionRequestItem> preSaleRequestItems);
        Task RunWaitingSyncJobAsync();
        Task ReadSyncResultFromSAPAsync();
        Task CreateRetrySyncJobAsync(Guid requestUnitID);
    }
}
