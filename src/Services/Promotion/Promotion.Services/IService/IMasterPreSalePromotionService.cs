using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public interface IMasterPreSalePromotionService
    {
        Task<MasterPreSalePromotionDTO> CreateMasterPreSalePromotionAsync(MasterPreSalePromotionDTO input);
        Task<MasterPreSalePromotionPaging> GetMasterPreSalePromotionListAsync(MasterPreSalePromotionListFilter filter, PageParam pageParam, MasterPreSalePromotionSortByParam sortByParam);
        Task<List<MasterPreSalePromotionDropdownDTO>> GetMasterPreSalePromotionDropdownAsync(string promotionNo = null, string name = null);
        Task<MasterPreSalePromotionDTO> GetMasterPreSalePromotionDetailAsync(Guid id);
        Task<MasterPreSalePromotionDTO> GetActiveMasterPreSalePromotionDetailAsync(Guid projectID);
        Task<MasterPreSalePromotionDTO> UpdateMasterPreSalePromotionAsync(Guid id, MasterPreSalePromotionDTO input);
        Task DeleteMasterPreSalePromotionAsync(Guid id);

        Task<MasterPreSalePromotionItemPaging> GetMasterPreSalePromotionItemListAsync(Guid masterPreSalePromotionID,PageParam pageParam, MasterPreSalePromotionItemSortByParam sortByParam);
        Task<List<MasterPreSalePromotionItemDTO>> UpdateMasterPreSalePromotionItemListAsync(Guid masterPreSalePromotionID, List<MasterPreSalePromotionItemDTO> inputs);
        Task<MasterPreSalePromotionItemDTO> UpdateMasterPreSalePromotionItemAsync(Guid masterPreSalePromotionID, Guid masterPreSalePromotionItemID, MasterPreSalePromotionItemDTO input);
        Task DeleteMasterPreSalePromotionItemAsync(Guid id);
        Task<List<MasterPreSalePromotionItemDTO>> CreateMasterPreSalePromotionItemFromMaterialAsync(Guid masterPreSalePromotionID, List<PromotionMaterialDTO> inputs);
        Task<List<MasterPreSalePromotionItemDTO>> CreateSubMasterPreSalePromotionItemFromMaterialAsync(Guid masterPreSalePromotionID, Guid mainMasterPreSalePromotionItemID, List<PromotionMaterialDTO> inputs);

        Task<List<ModelListDTO>> GetMasterPreSalePromotionItemModelListAsync(Guid masterPreSalePromotionItemID);
        Task<List<ModelListDTO>> AddMasterPreSalePromotionItemModelListAsync(Guid masterPreSalePromotionItemID, List<ModelListDTO> inputs);

        Task<MasterPreSalePromotionDTO> CloneMasterPreSalePromotionAsync(Guid id);
        Task<CloneMasterPromotionConfirmDTO> GetCloneMasterPreSalePromotionConfirmAsync(Guid id);

    }
}
