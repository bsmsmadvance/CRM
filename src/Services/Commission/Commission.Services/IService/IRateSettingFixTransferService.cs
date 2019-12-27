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
    public interface IRateSettingFixTransferService
    {
        /// <summary>
        /// ดึงรายการFix ขาย
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148967/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingFixTransferPaging> GetRateSettingFixTransferListAsync(RateSettingFixTransferFilter filter, PageParam pageParam, RateSettingFixTransferSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลFix ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferDTO> GetRateSettingFixTransferAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลFix ขาย
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148968/preview
        Task CreateRateSettingFixTransferAsync(RateSettingFixSaleTransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูลFix ขาย
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148968/preview
        Task<RateSettingFixSaleTransferDTO> UpdateRateSettingFixTransferAsync(Guid id, RateSettingFixSaleTransferDTO input);

        /// <summary>
        /// ลบข้อมูลFix ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixTransfer> DeleteRateSettingFixTransferAsync(Guid id);
    }
}
