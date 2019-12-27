using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;

namespace Finance.Services.IService
{
    //BR-21
    public interface IDepositService
    {
        /// <summary>
        /// ดึงข้อมูลรายการใบเสร็จมาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367433/preview
        /// </summary>
        /// <returns></returns>
        Task<DepositPaging> GetDepositListAsync(DepositFilter filter, PageParam pageParam, DepositSortByParam sortByParam);

        Task<DepositPaging> GetDepositListForUpdateAsync(Guid id, List<Guid> listNewID, DepositFilter filter, PageParam pageParam, DepositSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลรายละเอียดการนำฝาก
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367435/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        Task<List<DepositDetailDTO>> GetDepositAsync(Guid id);

        /// <summary>
        /// เพิ่มข้อมูลการนำฝาก #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DepositDetailDTO>> CreateDepositAsync(List<DepositDetailDTO> input);

        /// <summary>
        /// บันทึกการแก้ไข รายการนำฝาก #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        Task<List<DepositDetailDTO>> UpdateDepositAsync(Guid id, List<DepositDetailDTO> input);

        /// <summary>
        /// ลบข้อมูลการนำฝาก #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232402/preview
        /// </summary>
        /// <param name="DepositHeaderID"></param>
        /// <returns></returns>
        Task DeleteDepositAsync(Guid id);
    }
}
