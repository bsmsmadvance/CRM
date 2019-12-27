using Base.DTOs.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IProjectInfoService
    {
        Task<ProjectInfoDTO> GetProjectInfoAsync(Guid id);
        Task<ProjectInfoDTO> UpdateProjectInfoAsync(Guid id, ProjectInfoDTO input);
    }
}
