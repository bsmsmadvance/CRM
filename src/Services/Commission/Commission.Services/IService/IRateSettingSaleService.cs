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
    public interface IRateSettingSaleService
    {
        /// <summary>
        /// ดึงรายการ  Rating% ขาย
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148970/preview
        /// </summary>
        /// <returns></returns>
        Task<RateSettingSalePaging> GetRateSettingSaleListAsync(RateSettingSaleFilter filter, PageParam pageParam, RateSettingSaleSortByParam sortByParam);

        /// <summary>
        /// ดึงรายการ Rating% ขาย ตาม bg สำหรับสร้างใหม่
        /// <param name="bg"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148971/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingSaleTransferDTO>> GetRateSettingSaleProjectListForNewAsync(string BgNo);

        /// <summary>
        /// ดึงรายการ Rating% ขาย ตามโครงการ สำหรับแก้ไข
        /// <param name="รหัสโครงการ"></param>  
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148971/preview
        /// </summary>
        /// <returns></returns>
        Task<List<RateSettingSaleTransferDTO>> GetRateSettingSaleProjectListForUpdateAsync(Guid? ProjectID, DateTime? ActiveDate);

        /// <summary>
        /// สร้างข้อมูล Rating% ขาย พร้อมกันหลายเรทติ้ง
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148972/preview
        /// <returns></returns>
        Task CreateRateSettingSaleListAsync(RateSettingSaleInput input);

        /// <summary>
        /// แก้ไขข้อมูล Rating% ขาย พร้อมกันหลายเรทติ้ง
        /// </summary>
        /// <param name="input"></param>   
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148972/preview
        /// <returns></returns>
        Task UpdateRateSettingSaleListAsync(List<RateSettingSaleTransferDTO> ListInput);

        /*
        /// <summary>
        /// ดึงข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> GetRateSettingSaleAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="input"></param>   
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> CreateRateSettingSaleAsync(RateSettingSaleTransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="id, input"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferDTO> UpdateRateSettingSaleAsync(Guid id, RateSettingSaleTransferDTO input);

        /// <summary>
        /// ลบข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RateSettingSale> DeleteRateSettingSaleAsync(Guid id);
        */

        /// <summary>
        /// Import Excel ข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="bg"></param>
        /// <returns></returns>
        Task<RateSettingSaleTransferExcelDTO> ImportRateSettingSaleAsync(Guid BGID, FileDTO input);

        /// <summary>
        /// Export Excel ข้อมูล Rating% ขาย
        /// </summary>
        /// <param name="bg,projectIds"></param>
        /// <returns></returns>
        Task<FileDTO> ExportExcelRateSettingSaleAsync(Guid BGID, RateSettingSaleFilter filter, RateSettingSaleSortByParam sortByParam);
    }
}
