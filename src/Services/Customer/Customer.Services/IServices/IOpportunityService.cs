using Base.DTOs;
using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Services.OpportunityService
{
    public interface IOpportunityService
    {
        Task<OpportunityPaging> GetOpportunityListAsync(OpportunityFilter filter, PageParam pageParam, OpportunityListSortByParam sortByParam);
        Task<OpportunityDTO> GetOpportunityAsync(Guid id);
        Task<OpportunityDTO> CreateOpportunityAsync(OpportunityDTO input, Guid? userID, Guid? fromVisitorID = null);
        Task<OpportunityDTO> UpdateOpportunityAsync(Guid id, OpportunityDTO input);
        Task DeleteOpportunityAsync(Guid id);
        Task<List<OpportunityActivityListDTO>> GetOpportunityActivityListAsync(Guid opportunityID);
        Task<OpportunityActivityDTO> GetOpportunityActivityAsync(Guid id);
        Task<OpportunityActivityDTO> CreateOpportunityActivityAsync(Guid opportunityID, OpportunityActivityDTO input);
        Task<OpportunityActivityDTO> UpdateOpportunityActivityAsync(Guid id, OpportunityActivityDTO input);
        Task DeleteOpportunityActivityAsync(Guid id);
        Task<OpportunityActivityDTO> GetOpportunityActivityDraftAsync(Guid opportunityID);
        Task<List<RevisitActivityListDTO>> GetRevisitActivityListAsync(Guid opportunityID);
        Task<RevisitActivityDTO> GetRevisitActivityAsync(Guid id);
        Task<RevisitActivityDTO> CreateRevisitActivityAsync(Guid opportunityID, RevisitActivityDTO input);
        Task<RevisitActivityDTO> UpdateRevisitActivityAsync(Guid id, RevisitActivityDTO input);
        Task DeleteRevisitActivityAsync(Guid id);
        Task<RevisitActivityDTO> GetRevisitActivityDraftAsync(Guid opportunityID);
        Task<OpportunityAssignDTO> AssignOpportunityListAsync(OpportunityAssignDTO input);
        Task<List<OpportunityListDTO>> AssignOpportunityListRandomAsync(Guid projectID, List<OpportunityListDTO> inputs);
    }
}
