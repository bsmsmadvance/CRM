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
    public interface IUnitService
    {
        Task<List<UnitDropdownDTO>> GetUnitDropdownListAsync(Guid projectID, Guid? towerID = null, Guid? floorID = null, string name = null, string unitStatusKey = null);
        Task<List<UnitDropdownSellPriceDTO>> GetUnitDropdownWithSellPriceListAsync(Guid projectID, string name, string unitStatusKey = null);
        Task<UnitPaging> GetUnitListAsync(Guid projectID, UnitFilter request, PageParam pageParam, UnitListSortByParam sortByParam);
        Task<UnitDTO> GetUnitAsync(Guid projectID, Guid id);
        Task<UnitDTO> CreateUnitAsync(Guid projectID, UnitDTO input);
        Task<UnitDTO> UpdateUnitAsync(Guid projectID, Guid id, UnitDTO input);
        Task<Unit> DeleteUnitAsync(Guid projectID, Guid id);
        Task<UnitInfoDTO> GetUnitInfoAsync(Guid projectID, Guid id);
        Task<UnitInitialExcelDTO> ImportUnitInitialAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportExcelUnitInitialAsync(Guid projectID);
        Task<UnitGeneralExcelDTO> ImportUnitGeneralAsync(Guid projectID, FileDTO input);
        Task<FileDTO> ExportExcelUnitGeneralAsync(Guid projectID);
        Task<FileDTO> ExportExcelUnitFenceAreaAsync(Guid projectID);
        Task<UnitFenceAreaExcelDTO> ImportUnitFenceAreaAsync(Guid projectID, FileDTO input);
        Task<UnitMeterDTO> UpdateUnitMeterAsync(Guid unitID, UnitMeterDTO input);
        Task<UnitMeterDTO> GetUnitMeterAsync(Guid unitID);

        Task<Unit> DeleteUnitMeterAsync(Guid unitID);

        Task<UnitMeterPaging> GetUnitMeterListAsync(UnitMeterFilter request, PageParam pageParam, UnitMeterListSortByParam sortByParam);

        Task<UnitMeterExcelDTO> ImportUnitMeterExcelAsync(FileDTO input);
        Task<FileDTO> ExportUnitMeterExcelAsync(UnitMeterFilter request, UnitMeterListSortByParam sortByParam);
        Task<UnitMeterExcelDTO> ImportUnitMeterStatusExcelAsync(FileDTO input);
        Task<FileDTO> ExportUnitMeterStatusExcelAsync(UnitMeterFilter filter, UnitMeterListSortByParam sortByParam);

        //SAP Sync
        Task<UnitSyncResponse> ReadSAPWBSPromotionTextFileAsync(string[] text);
    }
}
