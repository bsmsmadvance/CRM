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
    public interface IRateSettingFixTransferModelService
    {
        /// <summary>
        /// ดึงรายการFix ตามแบบบ้าน โอน
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148965/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingFixTransferModelPaging> GetRateSettingFixTransferModelListAsync(RateSettingFixTransferModelFilter filter, PageParam pageParam, RateSettingFixTransferModelSortByParam sortByParam);

        /// <summary>
        /// ดึงรายการFix ตามแบบบ้าน โอน ตามโครงการ
        /// <param name="รหัสโครงการ"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148966/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingFixSaleTransferModelDTO>> GetRateSettingFixTransferModelProjectListAsync(Guid? ProjectID, DateTime? ActiveDate);

        /// <summary>
        /// แก้ไขข้อมูลFix ตามแบบบ้าน โอน พร้อมกันหลายแบบ้าน
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/372189275/preview
        /// <returns></returns>
        Task CreateRateSettingFixTransferModelListAsync(List<RateSettingFixSaleTransferModelDTO> ListInput);

        /// <summary>
        /// ดึงข้อมูลFix ตามแบบบ้าน โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> GetRateSettingFixTransferModelAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลFix ตามแบบบ้าน โอน
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> CreateRateSettingFixTransferModelAsync(RateSettingFixSaleTransferModelDTO input);

        /// <summary>
        /// แก้ไขข้อมูลFix ตามแบบบ้าน โอน
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> UpdateRateSettingFixTransferModelAsync(Guid id, RateSettingFixSaleTransferModelDTO input);

        /// <summary>
        /// ลบข้อมูลFix ตามแบบบ้าน โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixTransferModel> DeleteRateSettingFixTransferModelAsync(Guid id);
    }
}
