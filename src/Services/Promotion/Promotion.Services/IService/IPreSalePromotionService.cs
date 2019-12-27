using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;
using Database.Models.PRM;
using PagingExtensions;
using Promotion.Params.Filters;
using Promotion.Params.Outputs;

namespace Promotion.Services
{
    public interface IPreSalePromotionService
    {
        /// <summary>
        /// ดึงข้อมูลโปรก่อนขาย (ที่เสนอ)
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        Task<PreSalePromotionDTO> GetPreSalePromotionAsync(Guid unitID);

        /// <summary>
        /// รายการเบิกโปรโมชั่นก่อนขาย (Paging, Sort, Filter)
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/362358689/preview
        /// </summary>
        Task<PreSalePromotionRequestListPaging> GetPreSalePromotionRequestListAsync(PreSalePromotionRequestListFilter filter, PageParam pageParam, PreSalePromotionRequestListSortByParam sortByParam);

        /// <summary>
        /// ดึงรายละเอียดเบิกโปรโมชั่นก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204541/preview
        /// </summary>
        /// <param name="requestID">PreSalePromotionRequest.ID</param>
        /// <returns></returns>
        Task<PreSalePromotionRequestDTO> GetPreSalePromotionRequestAsync(Guid requestID);
        /// <summary>
        /// ดึงรายละเอียดแปลงเบิกโปรก่อนขาย
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/364204542/preview
        /// </summary>
        /// <param name="requestUnitID">PreSalePromotionRequestUnit.ID</param>
        /// <returns></returns>
        Task<PreSalePromotionRequestUnitDTO> GetPreSalePromotionRequestUnitAsync(Guid requestUnitID);
        /// <summary>
        /// ดึงรายการโปรก่อนขายที่ Active อยู่ทั้งหมดจาก Master
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092477/preview
        /// </summary>
        /// <returns></returns>
        Task<List<PreSalePromotionRequestItemDTO>> GetPreSalePromotionItemsFormMasterAsync(Guid masterPreSalePromotionID);
        /// <summary>
        /// บันทึกใบเบิกโปรก่อนขาย และสร้าง PR
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092474/preview
        /// </summary>
        /// <param name="units"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<PreSalePromotionRequestDTO> SaveRequestAndCreatePRAsync(Guid masterPreSalePromotionID, List<UnitDropdownSellPriceDTO> units, List<PreSalePromotionRequestItemDTO> items);
        /// <summary>
        /// สร้าง PR ใหม่ (จากที่ Failed)
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        Task<PreSalePromotionRequestUnitDTO> RetryCreatePRAsync(Guid requestUnitID);
        /// <summary>
        /// ยกเลิก PR
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        Task<PreSalePromotionRequestUnitDTO> CancelPRAsync(Guid requestUnitID);
        /// <summary>
        /// ยกเลิก PR ทีละหลายรายการ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482068/376092509/preview
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        Task<List<PreSalePromotionRequestUnitDTO>> CancelMultiplePRAsync(List<PreSalePromotionRequestUnitListDTO> units);
        /// <summary>
        /// พิมพ์ใบเบิกโปรก่อนขาย
        /// </summary>
        /// <param name="requestUnitID"></param>
        /// <returns></returns>
        Task<StringResult> ExportPreSaleRequestPrintFormUrlAsync(Guid requestUnitID);
    }
}
