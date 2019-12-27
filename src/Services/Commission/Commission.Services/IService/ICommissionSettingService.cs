using Database.Models.CMS;
using Database.Models.PRJ;
using Commission.Params.Filters;
using Commission.Params.Inputs;
using Base.DTOs.CMS;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public interface ICommissionSettingService
    {
        /// <summary>
        /// ดึงรายการตั้งค่าของโครงการแนวราบ/สูง
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148960/preview
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148976/preview
        /// </summary>
        /// <returns></returns>
        Task<CommissionSettingPaging> GetCommissionSettingListAsync(CommissionSettingFilter filter, PageParam pageParam, CommissionSettingSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลพนักงานขายประจำโครงการ
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="SaleUserFullName"></param>
        /// <returns></returns>
        Task<List<UserListDTO>> GetSaleUserProjectAsync(Guid ProjectId, string SaleUserFullName);

        /// <summary>
        /// ดึงข้อมูลพนักงานขายทั้งหมด
        /// </summary>
        /// <returns></returns>
        Task<List<UserListDTO>> GetSaleUserAllAsync();

        /// <summary>
        /// ดึงข้อมูลโครงการตาม bg
        /// </summary>
        /// <param name="BgId"></param>
        /// <returns></returns>
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListByBGAsync(Guid BgId);

        /// <summary>
        /// ดึงข้อมูลโครงการตามโครงการที่เลือก
        /// </summary>
        /// <param name="ListProject"></param>
        /// <returns></returns>
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListByProjectAsync(ListProjectInput ListProject);

        /// <summary>
        /// ดึงข้อมูลโครงการตามแนวราบหรือสูง
        /// </summary>
        /// <param name="ProductType">
        /// 1=แนวราบ/2=แนวสูง        
        /// </param>
        /// <returns></returns>
        Task<List<ProjectDropdownDTO>> GetProjectDropdownListByProductTypeAsync(string ProductType);
    }
}
