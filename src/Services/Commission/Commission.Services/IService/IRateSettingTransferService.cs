using Database.Models.CMS;
using Commission.Params.Filters;
using Commission.Params.Inputs;
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
    public interface IRateSettingTransferService
    {
        /// <summary>
        /// ดึงรายการ  Rating% โอน
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148970/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingTransferPaging> GetRateSettingTransferListAsync(RateSettingTransferFilter filter, PageParam pageParam, RateSettingTransferSortByParam sortByParam);

        /// <summary>
        /// ดึงรายการ Rating% ขาย ตาม bg สำหรับสร้างใหม่
        /// <param name="bg"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148971/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingSaleTransferDTO>> GetRateSettingTransferProjectListForNewAsync(string BgNo);

        /// <summary>
        /// ดึงรายการ Rating% โอน ตามโครงการ  สำหรับแก้ไข
        /// <param name="รหัสโครงการ"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148971/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingSaleTransferDTO>> GetRateSettingTransferProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate);

        /// <summary>
        /// สร้างข้อมูล Rating% โอน พร้อมกันหลายเรทติ้ง
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148972/preview
        /// <returns></returns>
        Task CreateRateSettingTransferListAsync(RateSettingTransferInput input);

        /// <summary>
        /// แก้ไขข้อมูล Rating% โอน พร้อมกันหลายเรทติ้ง
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148972/preview
        /// <returns></returns>
        Task UpdateRateSettingTransferListAsync(List<RateSettingSaleTransferDTO> ListInput);

        /*
        /// <summary>
        /// ดึงข้อมูล Rating% โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> GetRateSettingTransferAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูล Rating% โอน
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> CreateRateSettingTransferAsync(RateSettingSaleTransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูล Rating% โอน
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> UpdateRateSettingTransferAsync(Guid id, RateSettingSaleTransferDTO input);

        /// <summary>
        /// ลบข้อมูล Rating% โอน
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingTransfer> DeleteRateSettingTransferAsync(Guid id);
        */

        /// <summary>
        /// Import Excel ข้อมูล Rating% โอน
        /// </summary>
        /// <param name="bg,input"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferExcelDTO> ImportRateSettingTransferAsync(Guid BGID, FileDTO input);

        /// <summary>
        /// Export Excelข้อมูล Rating% โอน
        /// </summary>
        /// <param name="bg,projectIds"></param>
        /// <returns></returns>
        Task<FileDTO> ExportExcelRateSettingTransferAsync(Guid BGID, RateSettingTransferFilter filter, RateSettingTransferSortByParam sortByParam);
    }
}
