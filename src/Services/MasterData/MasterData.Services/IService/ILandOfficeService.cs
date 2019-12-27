using Database.Models.MST;
using MasterData.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MasterData.Params.Outputs;
using Base.DTOs;

namespace MasterData.Services
{
    public interface ILandOfficeService
    {
        Task<List<LandOfficeListDTO>> GetLandOfficeDropdownListAsync(string name, Guid? provinceID = null);
        Task<LandOfficePaging> GetLandOfficeListAsync(LandOfficeFilter filter, PageParam pageParam, LandOfficeSortByParam sortByParam);
        Task<LandOfficeDTO> GetLandOfficeAsync(Guid id);
        Task<LandOfficeDTO> CreateLandOfficeAsync(LandOfficeDTO input);
        Task<LandOfficeDTO> UpdateLandOfficeAsync(Guid id, LandOfficeDTO input);
        Task<LandOffice> DeleteLandOfficeAsync(Guid id);
    }
}
