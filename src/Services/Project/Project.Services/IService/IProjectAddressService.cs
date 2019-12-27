using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using Project.Params.Outputs;

namespace Project.Services
{
    public interface IProjectAddressService
    {
        Task<List<ProjectAddressListDTO>> GetProjectAddressDropdownListAsync(Guid projectID, string name, string projectAddressTypeKey);
        Task<AddressPaging> GetProjectAddressListAsync(Guid projectID, PageParam pageParam,SortByParam sortByParam);
        Task<ProjectAddressDTO> GetProjectAddressAsync(Guid id);
        Task<ProjectAddressDTO> CreateProjectAddressAsync(Guid projectID, ProjectAddressDTO input);
        Task<ProjectAddressDTO> UpdateProjectAddressAsync(Guid projectID, Guid id, ProjectAddressDTO input);
        Task DeleteProjectAddressAsync(Guid id);
    }
}
