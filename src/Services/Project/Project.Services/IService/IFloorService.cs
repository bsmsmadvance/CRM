using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IFloorService
    {
        Task<List<FloorDropdownDTO>> GetFloorDropdownListAsync(Guid projectID, Guid? towerID,string name);
        Task<FloorPaging> GetFloorListAsync(Guid projectID, Guid towerID, FloorsFilter filter, PageParam pageParam, FloorSortByParam sortByParam);
        Task<FloorDTO> GetFloorAsync(Guid projectID, Guid towerID, Guid id);
        Task<FloorDTO> CreateFloorAsync(Guid projectID, Guid towerID, FloorDTO input);
        Task<List<FloorDTO>> CreateMultipleFloorAsync(Guid projectID, Guid towerID, CreateMultipleFloorInput input);
        Task<FloorDTO> UpdateFloorAsync(Guid projectID, Guid towerID, Guid id, FloorDTO input);
        Task<Floor> DeleteFloorAsync(Guid projectID, Guid towerID, Guid id);
    }
}
