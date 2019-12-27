using System;
using System.Threading.Tasks;
using Identity.Params.Outputs;

namespace Identity.Services
{
    public interface IUserSyncService
    {
        Task<UserSyncResponse> SyncUserDataAsync();
        Task<RoleSyncResponse> SyncRoleDataAsync();
        Task SyncRoleOfUserDataAsync();

    }
}
