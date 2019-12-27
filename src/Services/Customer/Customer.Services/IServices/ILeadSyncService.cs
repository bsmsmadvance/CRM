using Customer.Params.Outputs;
using System;
using System.Threading.Tasks;

namespace Customer.Services.LeadSyncService
{
    public interface ILeadSyncService
    {
        Task<LeadSyncResponse> SyncLeadsFromCRMAfterSale(DateTime startTime, DateTime endTime);
        Task<LeadSyncResponse> SyncLeadsFromAPThaiWeb(DateTime startTime, DateTime endTime);
    }
}
