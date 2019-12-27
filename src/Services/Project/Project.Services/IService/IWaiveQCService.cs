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
    public interface IWaiveQCService
    {
        Task<WaiveQCPaging> GetWaiveQCListAsync(Guid projectID, WaiveQCFilter request ,PageParam pageParam, WaiveQCSortByParam sortByParam);
        Task<WaiveQCDTO> GetWaiveQCAsync(Guid projectID, Guid id);
        Task<WaiveQCDTO> CreateWaiveQCAsync(Guid projectID, WaiveQCDTO input);
        Task<WaiveQCDTO> UpdateWaiveQCAsync(Guid projectID, Guid id, WaiveQCDTO input);
        Task<WaiveQCExcelDTO> ImportWaiveQCAsync(Guid projectID, FileDTO input);
        Task<WaiveQC> DeleteWaiveQCAsync(Guid projectID, Guid id);
        Task<FileDTO> ExportExcelWaiveQCAsync(Guid projectID, WaiveQCFilter filter, WaiveQCSortByParam sortByParam);
    }
}
