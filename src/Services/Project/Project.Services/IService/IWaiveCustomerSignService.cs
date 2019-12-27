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
    public interface IWaiveCustomerSignService
    {
        Task<WaiveCustomerSignPaging> GetWaiveCustomerSignListAsync(Guid projectID, WaiveCustomerSignFilter filter, PageParam pageParam, WaiveCustomerSignSortByParam sortByParam);
        Task<WaiveCustomerSignDTO> GetWaiveCustomerSignAsync(Guid projectID, Guid id);
        Task<WaiveCustomerSignDTO> CreateWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignDTO input);
        Task<WaiveCustomerSignDTO> UpdateWaiveCustomerSignAsync(Guid projectID, Guid id, WaiveCustomerSignDTO input);
        Task<WaiveQC> DeleteWaiveCustomerSignAsync(Guid projectID, Guid id);
        Task<WaiveCustomerSignExcelDTO> ImportWaiveCustomerSignAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportExcelWaiveCustomerSignAsync(Guid projectID, WaiveCustomerSignFilter filter, WaiveCustomerSignSortByParam sortByParam);
    }
}
