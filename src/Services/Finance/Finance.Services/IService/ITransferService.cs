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
    public interface ITransferService
    {
        /// <summary>
        /// ดึงข้อมูลรายการการโอนกรรมสิทธิ์ มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367479/preview
        /// </summary>
        /// <returns></returns>
        Task<TransferPaging> GetTransferListAsync(TransferFilter filter, PageParam pageParam, TransferSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลรายการเช็คที่บันทึกไว้ จากการโอนกรรมสิทธิ์ มาแสดงบน Popup
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367480/preview
        /// </summary>
        /// <returns></returns>
        Task<List<TransferChequeDTO>> GetTransferChequeListAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูลรายการเงินโอนผ่านที่บันทึกไว้ จากการโอนกรรมสิทธิ์ มาแสดงบน Popup
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367481/preview
        /// </summary>
        /// <returns></returns>
        Task<TransferBankTransferDTO> GetTransferBankTransferListAsync(Guid id);

        /// <summary>
        /// Preview รายการยืนยันชำระเงินที่เลือกไว้
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367482/preview
        /// </summary>
        /// <returns></returns>
        Task<List<TransferDTO>> PreviewTransferConfirmAsync(List<TransferDTO> input);

        /// <summary>
        /// ยืนยันรายการชำระเงินที่เลือกไว้
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367483/preview
        /// </summary>
        /// <returns></returns>
        Task<List<TransferDTO>> ConfirmTransferAsync(List<TransferDTO> input);

        /// <summary>
        /// บันทึกยกเลิกการยืนยันชำระเงิน
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370982039/preview
        /// </summary>
        /// <returns></returns>
        Task CancelConfirmAsync(Guid id);
    }
}
