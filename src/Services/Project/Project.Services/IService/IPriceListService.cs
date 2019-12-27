using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IPriceListService
    {
        Task<PriceListPaging> GetPriceListsAsync(Guid projectID, PriceListFilter request, PageParam pageParam, PriceListSortByParam sortByParam);
        Task<PriceListDTO> CreatePriceListAsync(Guid projectid, PriceListDTO input);
        Task<PriceListDTO> UpdatePriceListAsync(Guid projectID, Guid id, PriceListDTO input);
        Task<PriceList> DeletePriceListAsync(Guid id);
        Task<PriceListDTO> GetPriceListAsync(Guid projectID, Guid id);
        Task<PriceListExcelDTO> ImportProjectPriceListAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportExcelPriceListAsync(Guid projectID);
        Task<DataTable> ConvertExcelToDataTable(FileDTO input);
    }
}
