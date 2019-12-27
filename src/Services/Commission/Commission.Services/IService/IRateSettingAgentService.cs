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
using Commission.Params.Inputs;

namespace Commission.Services
{
    public interface IRateSettingAgentService
    {
        /// <summary>
        /// ดึงรายการ Agent Rate
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148978/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingAgentPaging> GetRateSettingAgentListAsync(RateSettingAgentFilter filter, PageParam pageParam, RateSettingAgentSortByParam sortByParam);

        /// <summary>
        /// ดึงรายการ Agent Rate ตามโครงการ สำหรับสร้างใหม่
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148980/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingAgentDTO>> GetRateSettingAgentProjectListForNewAsync();

        /// <summary>
        /// ดึงรายการ Agent Rate ตามโครงการ สำหรับแก้ไข
        /// <param name="ProjectID"></param>  
        /// <param name="ActiveDate"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148980/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingAgentDTO>> GetRateSettingAgentProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate);

        /// <summary>
        /// สร้างข้อมูล Agent Rate พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/372189544/preview
        /// <returns></returns>
        Task CreateRateSettingAgentListAsync(RateSettingAgentInput inputModel);

        /// <summary>
        /// แก้ไขข้อมูล Agent Rate พร้อมกันหลายรายการ
        /// </summary>
        /// <param name="ListInput"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/372189544/preview
        /// <returns></returns>
        Task UpdateRateSettingAgentListAsync(List<RateSettingAgentDTO> ListInput);

        /*
        /// <summary>
        /// ดึงข้อมูล Agent Rate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingAgentDTO> GetRateSettingAgentAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูล Agent Rate
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        Task<RateSettingAgentDTO> CreateRateSettingAgentAsync(RateSettingAgentDTO input);

        /// <summary>
        /// แก้ไขข้อมูล Agent Rate
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        Task<RateSettingAgentDTO> UpdateRateSettingAgentAsync(Guid id, RateSettingAgentDTO input);

        /// <summary>
        /// ลบข้อมูล Agent Rate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingAgent> DeleteRateSettingAgentAsync(Guid id);
        */
    }
}
