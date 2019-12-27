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
    public interface IRateSettingFixSaleModelService
    {
        /// <summary>
        /// ดึงรายการFix ตามแบบบ้านขาย
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148965/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingFixSaleModelPaging> GetRateSettingFixSaleModelListAsync(RateSettingFixSaleModelFilter filter, PageParam pageParam, RateSettingFixSaleModelSortByParam sortByParam);

        /// <summary>
        /// ดึงรายการFix ตามแบบบ้านขาย ตามโครงการ
        /// <param name="รหัสโครงการ"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148966/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingFixSaleTransferModelDTO>> GetRateSettingFixSaleModelProjectListAsync(Guid? ProjectID, DateTime? ActiveDate);

        /// <summary>
        /// แก้ไขข้อมูลFix ตามแบบบ้านขาย พร้อมกันหลายแบบบ้าน
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/372189275/preview
        /// <returns></returns>
        Task CreateRateSettingFixSaleModelListAsync(List<RateSettingFixSaleTransferModelDTO> ListInput);

        /// <summary>
        /// ดึงข้อมูลFix ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> GetRateSettingFixSaleModelAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลFix ตามแบบบ้านขาย
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> CreateRateSettingFixSaleModelAsync(RateSettingFixSaleTransferModelDTO input);

        /// <summary>
        /// แก้ไขข้อมูลFix ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleTransferModelDTO> UpdateRateSettingFixSaleModelAsync(Guid id, RateSettingFixSaleTransferModelDTO input);

        /// <summary>
        /// ลบข้อมูลFix ตามแบบบ้านขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingFixSaleModel> DeleteRateSettingFixSaleModelAsync(Guid id);
    }
}
