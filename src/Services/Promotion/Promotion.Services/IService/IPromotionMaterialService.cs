using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRM;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public interface IPromotionMaterialService
    {
        Task<PromotionMaterialPaging> GetPromotionMaterialListAsync(PromotionMaterialFilter filter, PageParam pageParam, PromotionMaterialSortByParam sortByParam);
        Task ReadMaterialMasterFromSAPAsync(string[] sapTextContent);
        Task ReadMaterialAgreementFromSAPAsync(string[] sapTextContent);

        Task<MaterialSyncJobPaging> GetMaterialSyncJobListAsync(MaterialSyncJobFilter filter, PageParam pageParam, MaterialSyncJobSortByParam sortByParam);
    }
}
