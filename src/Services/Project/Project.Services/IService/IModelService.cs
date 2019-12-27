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
    public interface IModelService
    {
        Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null);
        Task<ModelPaging> GetModelListAsync(Guid projectID,ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam);
        Task<ModelDTO> GetModelAsync(Guid projectID, Guid id);
        Task<ModelDTO> CreateModelAsync(Guid projectID,ModelDTO input);
        Task<ModelDTO> UpdateModelAsync(Guid projectID, Guid id, ModelDTO input);
        Task<Model> DeleteModelAsync(Guid projectID,Guid id);
    }
}
