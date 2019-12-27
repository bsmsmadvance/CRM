using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using Project.Params.Filters;
using Project.Params.Inputs;
using Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface ITitleDeedService
    {
        Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam);
        Task<TitleDeedDTO> GetTitleDeedAsync(Guid id);
        Task<TitleDeedDTO> CreateTitleDeedAsync(Guid projectID, TitleDeedDTO input);
        Task<TitleDeedDTO> UpdateTitleDeedAsync(Guid projectID, Guid id, TitleDeedDTO input);
        Task<TitleDeedDTO> UpdateTitleDeedStatusAsync(Guid id, TitleDeedDTO input);
        Task<TitledeedExcelDTO> ImportTitleDeedAsync(Guid projectID, FileDTO input);

        Task<FileDTO> ExportExcelTitleDeedAsync(Guid projectID);
        Task<TitledeedDetail> DeleteTitleDeedAsync(Guid projectID, Guid id);

        Task<List<TitleDeedDTO>> GetTitleDeedHistoryItemsAsync(Guid id);

        Task UpdateMultipleHouseNosAsync(Guid projectID, UpdateMultipleHouseNoParam input);
        Task UpdateMultipleLandOfficesAsync(Guid projectID, UpdateMultipleLandOfficeParam input);
    }
}
