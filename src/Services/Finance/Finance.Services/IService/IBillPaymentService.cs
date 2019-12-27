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
    public interface IBillPaymentService
    {
        /// <summary>
        /// ดึงข้อมูลรายการที่เคย Import Bill Payment มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367446/preview
        /// </summary>
        /// <returns></returns>
        Task<BillPaymentHeaderPaging> GetBillPaymentListAsync(BillPaymentHeaderFilter filter, PageParam pageParam, BillPaymentHeaderSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลรายการ Import Bill Payment ที่รอดำเนินการ มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367452/preview
        /// </summary>
        /// <returns></returns>
        Task<BillPaymentHeaderPaging> GetWaitingBillPaymentListAsync(BillPaymentDetailFilter filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam);


        /// <summary>
        /// Import ข้อมูลรายการ BillPayment จาก Text file
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367447/preview
        /// </summary>
        /// <returns></returns>
        Task<Guid> ImportBillPaymentAsync(FileWithBoolDTO fileDTO);

        /// <summary>
        /// ดึงข้อมูลรายละเอียด BillPayment
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview
        /// </summary>
        /// <param name="BillPaymentID"></param>
        /// <returns></returns>
        Task<BookingForBillPaymentDTO> GetBillPaymentSplitAsync(Guid id);

        /// <summary>
        /// ดึงข้อมูลรายละเอียด BillPayment
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview
        /// </summary>
        /// <param name="BillPaymentID"></param>
        /// <returns></returns>
        Task UpdateBillPaymentSplitAsync(BookingForBillPaymentDTO Input);
        
        /// <summary>
        /// เพิ่มข้อมูล BillPayment #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateBillPaymentAsync(Guid PaymentHeaderID);

       
        /// <summary>
        /// ยกเลิกรายการ BillPayment #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview
        /// </summary>
        /// <param name="BillPaymentDetailID"></param>
        /// <returns></returns>
        //Task DeleteBillPaymentDetailAsync(Guid id);

        /// <summary>
        /// แสดงข้อมูล รายการ BillPayment ที่ได้จาก Text file หรือ จากหน้า List ที่ Load มาจาก DB
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367449/preview
        /// </summary>
        /// <param name="BillPaymentDetailID"></param>
        /// <returns></returns>
        Task<BillPaymentHeaderPaging> GetBillPaymentDetailAsync(Guid id, BillPaymentDetailFilter Filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam);


        /// <summary>
        /// แสดงข้อมูล รายการ BillPayment ที่ได้จาก Text file หรือ จากหน้า List ที่ Load มาจาก DB
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367449/preview
        /// </summary>
        /// <param name="BillPaymentDetailID"></param>
        /// <returns></returns>
        Task<BillPaymentHeaderPaging> GetBillPaymentDetailTempAsync(Guid id, BillPaymentDetailFilter Filter, PageParam pageParam, BillPaymentDetailSortByParam sortByParam);

        /// <summary>
        /// บันทึกการแก้ไข รายการ BillPayment #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview
        /// </summary>
        /// <param name="BillPaymentID"></param>
        /// <returns></returns>
        Task UpdateBillPaymentDetailTempAsync(BillPaymentHeaderDTO input);


        /// <summary>
        /// บันทึกการแก้ไข รายการ BillPayment #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367449/preview
        /// </summary>
        /// <param name="BillPaymentDetailID"></param>
        /// <returns></returns>
        Task UpdateBillPaymentDetailAsync(BillPaymentHeaderDTO input);

        /// <summary>
        /// แสดงข้อมูล รายการ BillPayment ที่ได้จาก Text file หรือ จากหน้า List ที่ Load มาจาก DB มาแสดงที่หน้า Split รายการ Bill Payment
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367450/preview
        /// </summary>
        /// <param name="BillPaymentDetailID"></param>
        /// <returns></returns>
       // Task<BillPaymentHeaderPaging> GetBillPaymentDetailForSplitAsync(Guid id);
    }
}
