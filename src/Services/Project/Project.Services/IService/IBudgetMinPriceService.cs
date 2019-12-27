using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;

namespace Project.Services
{
    public interface IBudgetMinPriceService
    {
        //Task<BudgetMinPriceDTO> GetBudgetMinPriceAsync(BudgetMinPriceFilter filter);
        //Task<List<BudgetMinPriceUnitDTO>> GetBudgetMinPriceUnitListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam);
        Task<BudgetMinPricePaging> GetBudgetMinPriceListAsync(BudgetMinPriceFilter filter, PageParam pageParam, BudgetMinPriceSortByParam sortByParam);
        Task<BudgetMinPriceDTO> SaveBudgetMinPriceAsync(BudgetMinPriceFilter filter, BudgetMinPriceDTO input);
        Task SaveBudgetMinPriceUnitListAsync(BudgetMinPriceListDTO inputs);
        Task<BudgetMinPriceUnitDTO> SaveBudgetMinPriceUnitAsync(BudgetMinPriceFilter filter, BudgetMinPriceUnitDTO input);
        Task<BudgetMinPriceQuarterlyDTO> ImportQuarterlyBudgetAsync(FileDTO input);
        Task ConfirmImportQuarterlyBudgetAsync(BudgetMinPriceQuarterlyDTO input);
        Task<FileDTO> ExportQuarterlyBudgetAsync(BudgetMinPriceFilter filter);
        Task<BudgetMinPriceTransferDTO> ImportTransferBudgetAsync(FileDTO input);
        Task ConfirmImportTransferBudgetAsync(BudgetMinPriceTransferDTO inputs);
        Task<FileDTO> ExportTransferBudgetAsync(BudgetMinPriceFilter filter);
    }
}
