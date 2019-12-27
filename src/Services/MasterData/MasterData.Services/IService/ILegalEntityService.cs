using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterData.Services
{
    public interface ILegalEntityService
    {
        Task<List<LegalEntityDropdownDTO>> GetLegalEntityDropdownListAsync(string name);
        Task<LegalEntityPaging> GetLegalEntityListAsync(LegalFilter filter, PageParam pageParam, LegalEntitySortByParam sortByParam);
        Task<LegalEntityDTO> GetLegalEntityAsync(Guid id);
        Task<LegalEntityDTO> CreateLegalEntityAsync(LegalEntityDTO input);
        Task<LegalEntityDTO> UpdateLegalEntityAsync(Guid id, LegalEntityDTO input);
        Task<LegalEntity> DeleteLegalEntityAsync(Guid id);
    }
}
