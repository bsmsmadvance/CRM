using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;
using Database.Models.FIN;
using Base.DTOs.PRJ;

namespace Finance.Services.IService
{
    public interface IReceiptInfoService
    {

        /// ดึงข้อมูลรายการที่ต้องขอ ReceiptInfo มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367423/preview
        Task<ReceiptInfoPaging> GetReceiptInfoListAsync(ReceiptInfoFilter filter, PageParam pageParam, ReceiptInfoSortByParam sortByParam);

        /// บันทึกการแก้ไข รายการ ReceiptInfo #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367425/preview
        Task<ReceiptTempHeader> UpdateReceiptInfoAsync(ReceiptInfoDTO input);

        /// ลบข้อมูล ReceiptInfo #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367424/preview
        Task<ReceiptTempHeader> DeleteReceiptInfoAsync(ReceiptInfoDTO input);
        
    }
}
