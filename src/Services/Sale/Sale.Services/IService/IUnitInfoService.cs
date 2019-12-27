using Base.DTOs.SAL;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public interface IUnitInfoService
    {
        Task<UnitInfoListPaging> GetUnitInfoListAsync(UnitInfoListFilter filter, PageParam pageParam, UnitInfoListSortByParam sortByParam);
        Task<UnitInfoDTO> GetUnitInfoAsync(Guid unitID);
        Task<UnitInfoPreSalePromotionDTO> GetUnitInfoPreSalePromotionAsync(Guid unitID);
        Task<UnitInfoBookingPromotionDTO> GetUnitInfoBookingPromotionAsync(Guid unitID);
        Task<List<UnitInfoPromotionExpenseDTO>> GetUnitInfoPromotionExpensesAsync(Guid unitID);
        Task<UnitInfoPriceListDTO> GetPriceListAsync(Guid unitID);
        Task<UnitInfoStatusCountDTO> GetUnitInfoCountAsync(Guid? projectID);

        Task<UnitInfoSumPaymentDTO> GetUnitInfoPaymentAsync(Guid unitID);
    }
}
