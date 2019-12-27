using System;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;
using Base.DTOs.PRJ;
using System.Collections.Generic;
using Database.Models.FIN;

namespace Finance.Services.IService
{
    public interface IEditReceiptService
    {
        /// <summary>
        /// ดึงข้อมูลรายละเอียดใบเสร็จที่ต้องการแก้ไข มาแสดงที่หน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367471/preview
        /// </summary>
        /// <param name="EditReceiptID"></param>
        Task<EditReceiptDTO> GetEditReceiptAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูลรายละเอียดใบเสร็จมาแสดงที่หน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367468/preview
        /// </summary>
        /// <returns></returns>
        Task<EditReceiptPaging> GetEditReceiptListAsync(EditReceiptFilter filter, PageParam pageParam, EditReceiptSortByParam sortByParam);

        /// <summary>
        /// Validate ข้อมูลก่อนการ update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdateType"></param>
        /// <returns></returns>
        Task<EditReceiptDTO> ValidateBeforeUpdateAsync(EditReceiptDTO input);

        /// <summary>
        /// บันทึกการแก้ไข ใบเสร็จ #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367456/preview
        /// </summary>
        /// <returns></returns>
        Task<EditReceiptDTO> UpdateEditReceiptAsync(EditReceiptDTO input);

        /// <summary>
        /// ยกเลิกใบเสร็จ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367478/preview
        /// </summary>
        /// <param name="EditReceiptID"></param>
        /// <returns></returns>
        Task<EditReceiptDTO> DeleteReceiptAsync(EditReceiptDTO input);

    }
}
