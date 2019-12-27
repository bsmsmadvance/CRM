using Database.Models.CMS;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public interface IRateSettingFixSaleService
    {
        /// <summary>
        /// ดึงรายการFix ขาย
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148967/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingFixSalePaging> GetRateSettingFixSaleListAsync(RateSettingFixSaleFilter filter, PageParam pageParam, RateSettingFixSaleSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลFix ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferDTO> GetRateSettingFixSaleAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลFix ขาย
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148968/preview
        Task CreateRateSettingFixSaleAsync(RateSettingFixSaleTransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูลFix ขาย
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148968/preview
        Task<RateSettingFixSaleTransferDTO> UpdateRateSettingFixSaleAsync(Guid id, RateSettingFixSaleTransferDTO input);

        /// <summary>
        /// ลบข้อมูลFix ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSale> DeleteRateSettingFixSaleAsync(Guid id);
    }
}
