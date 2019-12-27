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
    public interface IWaterElectricMeterPriceService
    {
        Task<List<WaterMeterPriceDropdownDTO>> GetWaterMeterPriceDropdownListAsync(Guid unitID);
        Task<List<ElectricMeterPriceDropdownDTO>> GetElectricMeterPriceDropdownListAsync(Guid unitID);
        Task<WaterElectricMeterPricePaging> GetWaterElectricMeterPriceListAsync(Guid modelID, WaterElectricMeterPriceFilter filter, PageParam pageParam, SortByParam sortByParam);
        Task<WaterElectricMeterPriceDTO> GetWaterElectricMeterPriceAsync(Guid modelID, Guid id);
        Task<WaterElectricMeterPriceDTO> CreateWaterElectricMeterPriceAsync(Guid modelID, WaterElectricMeterPriceDTO input);
        Task<WaterElectricMeterPriceDTO> UpdateWaterElectricMeterPriceAsync(Guid modelID, Guid id, WaterElectricMeterPriceDTO input);
        Task<WaterElectricMeterPrice> DeleteWaterElectricMeterPriceAsync(Guid modelID, Guid id);
    }
}
