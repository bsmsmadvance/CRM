using Database.Models.CMS;
using Database.Models.PRJ;
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
    public interface IGeneralSettingService
    {
        /// <summary>
        /// ดึงรายการ ตั้งค่าทั่วไป
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148963/preview
        /// </summary>
        /// <returns></returns>
        Task<GeneralSettingPaging> GetGeneralSettingListAsync(GeneralSettingFilter filter, PageParam pageParam, GeneralSettingSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูล ตั้งค่าทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GeneralSettingDTO> GetGeneralSettingAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูล ตั้งค่าทั่วไป
        /// </summary>
        /// <param name="input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148964/preview
        /// <returns></returns>
        Task CreateGeneralSettingAsync(GeneralSettingDTO input);

        /// <summary>
        /// แก้ไขข้อมูล ตั้งค่าทั่วไป
        /// </summary>
        /// <param name="id, input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148964/preview
        /// <returns></returns>
        Task<GeneralSettingDTO> UpdateGeneralSettingAsync(Guid id, GeneralSettingDTO input);

        /// <summary>
        /// ลบข้อมูล ตั้งค่าทั่วไป
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GeneralSetting> DeleteGeneralSettingAsync(Guid id);
    }
}
