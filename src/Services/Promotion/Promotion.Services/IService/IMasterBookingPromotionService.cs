using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public interface IMasterBookingPromotionService
    {
        Task<MasterBookingPromotionDTO> CreateMasterBookingPromotionAsync(MasterBookingPromotionDTO input);
        Task<MasterBookingPromotionPaging> GetMasterBookingPromotionListAsync(MasterBookingPromotionListFilter filter, PageParam pageParam, MasterBookingPromotionSortByParam sortByParam);
        Task<MasterBookingPromotionDTO> GetMasterBookingPromotionDetailAsync(Guid id);
        Task<MasterBookingPromotionDTO> UpdateMasterBookingPromotionAsync(Guid id, MasterBookingPromotionDTO input);
        Task DeleteMasterBookingPromotionAsync(Guid id);
        Task<MasterBookingPromotionItemPaging> GetMasterBookingPromotionItemListAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingPromotionItemSortByParam sortByParam);
        Task<List<MasterBookingPromotionItemDTO>> UpdateMasterBookingPromotionItemListAsync(Guid masterBookingPromotionID, List<MasterBookingPromotionItemDTO> inputs);
        Task<MasterBookingPromotionItemDTO> UpdateMasterBookingPromotionItemAsync(Guid masterBookingPromotionID, Guid masterBookingPromotionItemID, MasterBookingPromotionItemDTO input);

        Task DeleteMasterBookingPromotionItemAsync(Guid id);
        Task<List<MasterBookingPromotionItemDTO>> CreateMasterBookingPromotionItemFromMaterialAsync(Guid masterBookingPromotionID, List<PromotionMaterialDTO> inputs);
        Task<List<MasterBookingPromotionItemDTO>> CreateSubMasterBookingPromotionItemFromMaterialAsync(Guid masterBookingPromotionID, Guid mainMasterBookingPromotionItemID, List<PromotionMaterialDTO> inputs);

        Task<List<ModelListDTO>> GetMasterBookingPromotionItemModelListAsync(Guid masterBookingPromotionItemID);
        Task<List<ModelListDTO>> AddMasterBookingPromotionItemModelListAsync(Guid masterBookingPromotionItemID, List<ModelListDTO> inputs);

        //Free Item
        Task<MasterBookingPromotionFreeItemPaging> GetMasterBookingPromotionFreeItemListAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingPromotionFreeItemSortByParam sortByParam);
        Task<MasterBookingPromotionFreeItemDTO> CreateMasterBookingPromotionFreeItemAsync(Guid masterBookingPromotionID, MasterBookingPromotionFreeItemDTO input);
        Task<List<MasterBookingPromotionFreeItemDTO>> UpdateMasterBookingPromotionFreeItemListAsync(Guid masterBookingPromotionID, List<MasterBookingPromotionFreeItemDTO> inputs);
        Task<MasterBookingPromotionFreeItemDTO> UpdateMasterBookingPromotionFreeItemAsync(Guid masterBookingPromotionID, Guid masterBookingPromotionFreeItemID, MasterBookingPromotionFreeItemDTO input);
        Task DeleteMasterBookingPromotionFreeItemAsync(Guid id);

        Task<List<ModelListDTO>> GetMasterBookingPromotionFreeItemModelListAsync(Guid masterBookingPromotionFreeItemID);
        Task<List<ModelListDTO>> AddMasterBookingPromotionFreeItemModelListAsync(Guid masterBookingPromotionFreeItemID, List<ModelListDTO> inputs);

        //Credit Card Item
        Task<MasterBookingCreditCardItemPaging> GetMasterBookingCreditCardItemAsync(Guid masterBookingPromotionID, PageParam pageParam, MasterBookingCreditCardItemSortByParam sortByParam);
        Task<List<MasterBookingCreditCardItemDTO>> UpdateMasterBookingCreditCardItemListAsync(Guid masterBookingPromotionID, List<MasterBookingCreditCardItemDTO> inputs);
        Task<MasterBookingCreditCardItemDTO> UpdateMasterBookingCreditCardItemAsync(Guid masterBookingPromotionID, Guid masterBookingCreditCardItemID, MasterBookingCreditCardItemDTO input);
        Task DeleteMasterBookingCreditCardItemAsync(Guid id);
        Task<List<MasterBookingCreditCardItemDTO>> CreateMasterBookingCreditCardItemsAsync(Guid masterBookingPromotionID, List<EDCFeeDTO> inputs);

        Task<MasterBookingPromotionDTO> CloneMasterBookingPromotionAsync(Guid id);
        Task<CloneMasterPromotionConfirmDTO> GetCloneMasterBookingPromotionConfirmAsync(Guid id);

    }
}
