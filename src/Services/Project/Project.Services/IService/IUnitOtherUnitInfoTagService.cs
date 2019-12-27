using Base.DTOs.PRJ;
using Database.Models.PRJ;
using Project.Params.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IUnitOtherUnitInfoTagService
    {
        Task<List<UnitOtherUnitInfoTagDTO>> GetUnitTagListAsync(UnitOtherUnitInfoTagFilter filter);
        Task<UnitOtherUnitInfoTagDTO> GetUnitTagAsync(Guid id);
        Task<UnitOtherUnitInfoTagDTO> CreateUnitTagAsync(UnitOtherUnitInfoTagDTO input);
        Task<UnitOtherUnitInfoTagDTO> UpdateUnitTagAsync(Guid id, UnitOtherUnitInfoTagDTO input);
        Task<UnitOtherUnitInfoTag> DeleteUnitTagAsync(Guid id);
    }
}
