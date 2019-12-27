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
    public interface IFeeCreditDebitCardService
    {
        /// <summary>
        /// ดึงข้อมูลรายการการรับชำระเงินบัตรเครดิต & บัตรเดบิต มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        Task<FeeCreditDebitCardPaging> GetFeeCreditDebitCardListAsync(FeeCreditDebitCardFilter filter, PageParam pageParam, FeeCreditDebitCardSortByParam sortByParam);

        /// <summary>
        /// บันทึกการแก้ไข ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367428/preview
        /// </summary>
        /// <returns></returns>
       // Task<List<FeeCreditDebitCardDTO>> UpdateFeeCreditDebitCardAsync(List<FeeCreditDebitCardDTO> input);

        /// <summary>
        /// บันทึกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        Task UpdateFeeConfirmAsync(List<FeeCreditDebitCardDTO> input, Guid? userID);

        /// <summary>
        /// บันทึกยกเลิกสถานะตรวจสอบ ค่าธรรมเนียมบัตรเครดิต #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367427/preview
        /// </summary>
        /// <returns></returns>
        Task  CancelFeeConfirmAsync(List<FeeCreditDebitCardDTO> input, Guid? userID);
    }
}
