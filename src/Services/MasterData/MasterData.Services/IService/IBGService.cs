using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;

namespace MasterData.Services
{
    public interface IBGService
    {
        Task<List<BGDropdownDTO>> GetBGDropdownListAsync(string productTypeKey, string name);
        Task<BGPaging> GetBGListAsync(BGFilter filter, PageParam pageParam, BGSortByParam sortByParam);
        Task<BGDTO> GetBGAsync(Guid id);
        Task<BGDTO> CreateBGAsync(BGDTO input);
        Task<BGDTO> UpdateBGAsync(Guid id, BGDTO input);
        Task<BG> DeleteBGAsync(Guid id);
    }
}
