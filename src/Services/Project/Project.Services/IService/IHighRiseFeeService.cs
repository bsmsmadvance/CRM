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
    public interface IHighRiseFeeService
    {
        Task<HighRiseFeePaging> GetHighRiseFeeListAsync(Guid projectID, HighRiseFeeFilter filter, PageParam pageParam, HighRiseFeeSortByParam sortByParam);
        Task<HighRiseFeeDTO> GetHighRiseFeeAsync(Guid projectID, Guid id);
        Task<HighRiseFeeDTO> CreateHighRiseFeeAsync(Guid projectID, HighRiseFeeDTO input);
        Task<HighRiseFeeDTO> UpdateHighRiseFeeAsync(Guid projectID, Guid id, HighRiseFeeDTO input);
        Task<HighRiseFee> DeleteHighRiseFeeAsync(Guid projectID, Guid id);
        Task<HighRiseFeeExcelDTO> ImportHighRiseFeeAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportHighRiseFeeAsync(Guid projectID, HighRiseFeeFilter filter, HighRiseFeeSortByParam sortByParam);
    }
}
