using Base.DTOs;
using Base.DTOs.CTM;
using Base.DTOs.USR;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTM = Database.Models.CTM;

namespace Customer.Services.LeadService
{
    public interface ILeadService
    {
        Task<LeadPaging> GetLeadListAsync(LeadFilter filter, PageParam pageParam, LeadListSortByParam sortByParam);
        Task<LeadDTO> GetLeadAsync(Guid id);
        Task<LeadDTO> CreateLeadAsync(LeadDTO input, Guid? userID);
        Task<LeadDTO> UpdateLeadAsync(Guid id, LeadDTO input);
        Task<LeadListDTO> AssignLeadAsync(Guid id, UserListDTO input);
        Task DeleteLeadAsync(Guid id);
        Task<List<LeadActivityListDTO>> GetLeadActivityListAsync(Guid leadID);
        Task<LeadActivityDTO> GetLeadActivityDraftAsync(Guid leadID);
        Task<LeadActivityDTO> GetLeadActivityAsync(Guid id);
        Task<LeadActivityDTO> CreateLeadActivityAsync(Guid leadID, LeadActivityDTO input);
        Task<LeadActivityDTO> UpdateLeadActivityAsync(Guid id, LeadActivityDTO input);
        Task DeleteLeadActivityAsync(Guid id);
        Task<List<LeadQualifyDTO>> GetLeadQualify(Guid id);
        Task<LeadDTO> SubmitLeadQualify(Guid id, Guid? contactID);
        Task<LeadDTO> UnSubmitLeadQualify(Guid id);
        Task<LeadAssignDTO> AssignLeadListAsync(LeadAssignDTO input);
        Task<List<LeadListDTO>> AssignLeadListRandomAsync(Guid projectID, List<LeadListDTO> inputs);
    }
}
