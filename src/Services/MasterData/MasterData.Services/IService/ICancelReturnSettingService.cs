using Base.DTOs.MST;
using System;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public interface ICancelReturnSettingService
    {
        Task<CancelReturnSettingDTO> GetCancelReturnSettingAsync();
        Task<CancelReturnSettingDTO> UpdateCancelReturnSettingAsync(Guid id, CancelReturnSettingDTO input);
    }
}
