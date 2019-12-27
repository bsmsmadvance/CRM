using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using MasterData.Params.Filters;
using MasterData.Params.Outputs;
using PagingExtensions;
using Report.Integration;

namespace MasterData.Services
{
    public interface IEDCService
    {
        Task<List<EDCDropdownDTO>> GetEDCDropdownListUrlAsync(Guid? projectID, string bankName);
        Task<EDCPaging> GetEDCListAsync(EDCFilter filter, PageParam pageParam, EDCSortByParam sortByParam);
        Task<EDCDTO> GetEDCDetailAsync(Guid id);
        Task<EDCDTO> CreateEDCAsync(EDCDTO input);
        Task<EDCDTO> UpdateEDCAsync(Guid id, EDCDTO input);
        Task DeleteEDCAsync(Guid id);

        Task<EDCBankPaging> GetEDCBankListAsync(EDCBankFilter filter, PageParam pageParam, EDCBankSortByParam sortByParam);
        Task<EDCFeePaging> GetEDCFeeListAsync(EDCFeeFilter filter, PageParam pageParam, EDCFeeSortByParam sortByParam);
        Task<EDCFeeDTO> CreateEDCFeeAsync(EDCFeeDTO input);
        Task<EDCFeeDTO> UpdateEDCFeeAsync(Guid id, EDCFeeDTO input);
        Task DeleteEDCFeeAsync(Guid id);

        Task<decimal> GetFeeAsync(Guid edcID, Guid bankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID, decimal payAmount);
        Task<double> GetFeePercentAsync(Guid edcID, Guid bankID, Guid creditCardTypeMasterCenterID, Guid creditCardPaymentTypeMasterCenterID, Guid paymentCardTypeMasterCenterID);
        Task<ReportResult> ExportEDCListUrlAsync(EDCFilter filter, ShowAs showAs);
    }
}
