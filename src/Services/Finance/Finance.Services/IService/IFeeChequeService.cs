using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;
using Database.Models.FIN;
using Base.DTOs;
using Base.DTOs.PRJ;

namespace Finance.Services.IService
{
    public interface IFeeChequeService
    {
        /// <summary>
        /// ดึงข้อมูลรายการการรับชำระเงินบัตรเครดิต & บัตรเดบิต มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        Task<FeeChequePaging> GetFeeChequeListAsync(FeeChequeFilter filter, PageParam pageParam, FeeChequeSortByParam sortByParam);

        /// <summary>
        /// บันทึกการแก้ไข ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367428/preview
        /// </summary>
        /// <returns></returns>
        Task UpdateFeeChequeAsync(List<FeeChequeDTO> input, Guid? userID);

        /// <summary>
        /// บันทึกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        //Task<List<FeeChequeDTO>> UpdateFeeConfirmAsync(List<FeeChequeDTO> input);

        /// <summary>
        /// บันทึกยกเลิกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        Task CancelFeeConfirmAsync(List<FeeChequeDTO> input, Guid? userID);
    }
}
