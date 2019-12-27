using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface ITowerService
    {
        Task<List<TowerDropdownDTO>> GetTowerDropdownListAsync(Guid projectID, string Code);
        Task<TowerPaging> GetTowerListAsync(Guid projectID, TowerFilter filter, PageParam pageParam, TowerSortByParam sortByParam);
        Task<TowerDTO> GetTowerAsync(Guid projectID, Guid id);
        Task<TowerDTO> CreateTowerAsync(Guid projectID, TowerDTO input);
        Task<TowerDTO> UpdateTowerAsync(Guid projectID, Guid id, TowerDTO input);
        Task<Tower> DeleteTowerAsync(Guid projectID, Guid id);
    }
}
