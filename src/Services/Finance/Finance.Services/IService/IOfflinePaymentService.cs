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
    public interface IOfflinePaymentService
    {
        /// <summary>
        /// ดึงข้อมูลรายการ ใบเสร็จจากระบบ Offline,etc มาแสดงบนหน้าจอ
        /// UI: 
        /// </summary>
        /// <returns></returns>
        Task<OfflinePaymentPaging> GetOfflinePaymentListAsync(OfflinePaymentFilter filter, PageParam pageParam, OfflinePaymentSortByParam sortByParam);
        
        /// <summary>
        /// ยืนยันรายการชำระเงินที่เลือกไว้
        /// UI: 
        /// </summary>
        /// <returns></returns>
        Task<List<OfflinePaymentDTO>> ConfirmOfflinePaymentAsync(List<OfflinePaymentDTO> input);

        /// <summary>
        /// บันทึก
        /// </summary>
        /// <returns></returns>
        Task<List<OfflinePaymentDTO>> CancelOfflinePaymentAsync(List<OfflinePaymentDTO> input);
    }
}
