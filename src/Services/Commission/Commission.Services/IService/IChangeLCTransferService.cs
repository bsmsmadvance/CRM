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
    public interface IChangeLCTransferService
    {
        /// <summary>
        /// ดึงรายการเพิ่ม Commission
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/362366575/preview
        /// </summary>
        /// <returns></returns>
        Task<ChangeLCTransferPaging> GetChangeLCTransferListAsync(ChangeLCTransferFilter filter, PageParam pageParam, ChangeLCTransferSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChangeLCTransferDTO> GetChangeLCTransferAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/362366576/preview
        /// <returns></returns>
        Task<ChangeLCTransferDTO> CreateChangeLCTransferAsync(ChangeLCTransferDTO input);

        /// <summary>
        /// แก้ไขข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id, input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/362366576/preview
        /// <returns></returns>
        Task<ChangeLCTransferDTO> UpdateChangeLCTransferAsync(Guid id, ChangeLCTransferDTO input);

        /// <summary>
        /// ลบข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChangeLCTransfer> DeleteChangeLCTransferAsync(Guid id);
    }
}
