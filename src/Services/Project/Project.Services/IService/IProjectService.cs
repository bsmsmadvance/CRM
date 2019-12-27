using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Outputs;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string name, Guid? companyID, bool isActive, string projectStatusKey);
        Task<ProjectPaging> GetProjectListAsync(ProjectsFilter filter, PageParam pageParam, ProjectSortByParam sortByParam);
        Task<ProjectDTO> CreateProjectAsync(ProjectDTO input);
        Task<Database.Models.PRJ.Project> DeleteProjectAsync(Guid id, string reason);
        Task<ProjectDataStatusDTO> GetProjectDataStatusAsync(Guid id);
        Task<ProjectCountDTO> GetProjectCountAsync();
        Task UpdateProjectStatus(Guid projectID, MasterCenterDropdownDTO projectStatus);
        Task<ProjectDTO> GetProjectAsync(Guid id);
        Task<ReportResult> GetExportBookingTemplateUrlAsync(Guid projectID);
        Task<ReportResult> GetExportAgreementTemplateUrlAsync(Guid projectID);
        Task<ReportResult> GetExportProjectListUrlAsync(ProjectsFilter filter, ShowAs downloadAs);

    }
}
