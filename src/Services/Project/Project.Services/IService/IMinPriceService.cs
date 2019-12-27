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
    public interface IMinPriceService
    {
        Task<MinPricePaging> GetMinPriceListAsync(Guid projectID, MinPriceFilter filter, PageParam pageParam, MinPriceSortByParam sortByParam);
        Task<MinPriceDTO> CreateMinPriceAsync(Guid projectID, MinPriceDTO input);
        Task<MinPriceDTO> GetMinPriceAsync(Guid projectID, Guid id);
        Task<MinPriceDTO> UpdateMinPriceAsync(Guid projectID, Guid id, MinPriceDTO input);
        Task<MinPrice> DeleteMinPriceAsync(Guid projectID, Guid id);
        Task<FileDTO> ExportExcelMinPriceAsync(Guid projectID, MinPriceFilter filter, MinPriceSortByParam sortByParam);
        Task<MinPriceExcelDTO> ImportMinPriceAsync(Guid projectID, FileDTO input);
    }
}
