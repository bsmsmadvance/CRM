using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTM = Database.Models.CTM;

namespace Customer.Services.VisitorService
{
    public interface IVisitorService
    {
        Task<VisitorPaging> GetVisitorListAsync(VisitorFilter filter, PageParam pageParam, VisitorListSortByParam sortByParam);
        Task<VisitorDTO> GetVisitorAsync(Guid id);
        Task<VisitorDTO> CreateVisitorAsync(VisitorCreateDTO input);
        Task<VisitorDTO> UpdateVisitorTypeAsync(Guid id, VisitorDTO input);
        Task<VisitorProjectDTO> GetVisitorProjectAsync(VisitorFilter filter);
        Task<VisitorDTO> SubmitOrUnSubmitVisitorWelcomeAsync(Guid id, bool isWelcome, Guid? UserID);
        Task<List<VisitorHistoryDTO>> GetVisitorHistoryListAsync(Guid id, VisitorHistoryListSortByParam sortByParam);
        Task<List<LeadListDTO>> GetVisitorLeadListAsync(Guid id, VisitorLeadListSortByParam sortByParam);
        Task<List<VisitorPurchaseHistoryDTO>> GetVisitorPurchaseHistoryListAsync(Guid id, VisitorPurchaseHistoryListSortByParam sortByParam);
        Task<List<VisitorQuestionnaireHistoryDTO>> GetVisitorQuestionnaireHistoryListAsync(Guid id, VisitorQuestionnaireHistoryListSortByParam sortByParam);
    }
}
