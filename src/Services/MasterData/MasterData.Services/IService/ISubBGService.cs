using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterData.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MasterData.Services
{
    public interface ISubBGService
    {
        Task<List<SubBGDropdownDTO>> GetSubBGDropdownListAsync(string name, Guid? bGID);
        Task<SubBGPaging> GetSubBGListAsync(SubBGFilter filter, PageParam pageParam, SubBGSortByParam sortByParam);
        Task<SubBGDTO> GetSubBGAsync(Guid id);
        Task<SubBGDTO> CreateSubBGAsync(SubBGDTO input);
        Task<SubBGDTO> UpdateSubBGAsync(Guid id, SubBGDTO input);
        Task<SubBG> DeleteSubBGAsync(Guid id);
    }
}
