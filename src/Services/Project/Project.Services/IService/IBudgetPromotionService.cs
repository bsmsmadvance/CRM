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
    public interface IBudgetPromotionService
    {
        Task<BudgetPromotionPaging> GetBudgetPromotionListAsync(Guid projectID, BudgetPromotionFilter filter, PageParam pageParam, BudgetPromotionSortByParam sortByParam);
        Task<BudgetPromotionDTO> GetBudgetPromotionAsync(Guid projectID, Guid unitID);
        Task<BudgetPromotionDTO> CreateBudgetPromotionAsync(Guid projectID, BudgetPromotionDTO input);
        Task<BudgetPromotionDTO> UpdateBudgetPromotionAsync(Guid projectID, Guid unitID, BudgetPromotionDTO input);
        Task DeleteBudgetPromotionAsync(Guid projectID, Guid unitID);

        Task<BudgetPromotionExcelDTO> ImportBudgetPromotionAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportExcelBudgetPromotionAsync(Guid projectID);

        //Sync SAP
        Task CreateNewSyncJobAsync(List<BudgetPromotion> input);
        Task RunWaitingSyncJobAsync();
        Task ReadSyncResultFromSAPAsync();
        Task CreateRetrySyncJobAsync();
    }
}
