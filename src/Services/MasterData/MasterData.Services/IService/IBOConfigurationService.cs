using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public interface IBOConfigurationService
    {
        Task<BOConfigurationDTO> GetBOConfigurationAsync();
        Task<BOConfigurationDTO> UpdateBOConfigurationAsync(Guid id, BOConfigurationDTO input);
    }
}
