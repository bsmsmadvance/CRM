using Database.Models.MST;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Outputs;
using MasterData.Params.Filters;

namespace MasterData.Services
{
    public interface ICancelReasonService
    {
        Task<List<CancelReasonDropdownDTO>> GetCancelReasonDropdownListAsync();
        Task<CancelReasonPaging> GetCancelReasonListAsync(CancelReasonFilter filter, PageParam pageParam, CancelReasonSortByParam sortByParam);
        Task<CancelReasonDTO> GetCancelReasonAsync(Guid id);
        Task<CancelReasonDTO> CreateCancelReasonAsync(CancelReasonDTO input);
        Task<CancelReasonDTO> UpdateCancelReasonAsync(Guid id, CancelReasonDTO input);
        Task<CancelReason> DeleteCancelReasonAsync(Guid id);
    }
}