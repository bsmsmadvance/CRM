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
    public interface IChangeLCSaleService
    {
        /// <summary>
        /// ดึงรายการเพิ่ม Commission
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/362366573/preview
        /// </summary>
        /// <returns></returns>
        Task<ChangeLCSalePaging> GetChangeLCSaleListAsync(ChangeLCSaleFilter filter, PageParam pageParam, ChangeLCSaleSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChangeLCSaleDTO> GetChangeLCSaleAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/362366574/preview
        /// <returns></returns>
        Task<ChangeLCSaleDTO> CreateChangeLCSaleAsync(ChangeLCSaleDTO input);

        /// <summary>
        /// แก้ไขข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id, input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/362366574/preview
        /// <returns></returns>
        Task<ChangeLCSaleDTO> UpdateChangeLCSaleAsync(Guid id, ChangeLCSaleDTO input);

        /// <summary>
        /// ลบข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChangeLCSale> DeleteChangeLCSaleAsync(Guid id);
    }
}
