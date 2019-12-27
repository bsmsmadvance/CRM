using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;
using Base.DTOs.MST;
using Base.DTOs;
using Database.Models.FIN;

namespace Finance.Services.IService
{
    public interface IDirectCreditDebitExportService
    {
        /// <summary>
        /// ดึงข้อมูลรายการ ส่งตัด Direct Credit/Debit มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367440/preview
        /// </summary>
        /// <returns></returns>
        Task<DirectCreditDebitExportHeaderPaging> GetDirectCreditDebitExportListAsync(DirectCreditDebitExportHeaderFilter filter, PageParam pageParam, DirectCreditDebitExportHeaderSortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลรายละเอียดการ ส่งตัด Direct Credit/Debit
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367435/preview
        /// </summary>
        /// <param name="DirectCreditDebitExportHeaderID"></param>
        /// <returns></returns>
        Task<DirectCreditDebitExportDetailPaging> GetDirectCreditDebitExportAsync(Guid id, DirectCreditDebitExportDetailFilter filter, PageParam pageParam, DirectCreditDebitExportDetailSortByParam sortByParam);

        /// <summary>
        /// เพิ่มข้อมูลการ ส่งตัด Direct Credit/Debit #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367441/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FileDTO> CreateDirectCreditDebitExportAsync(DirectCreditDebitExportDTO input);

        /// <summary>
        /// ลบข้อมูลการส่งตัด Direct Credit/Debit #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367440/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      //  Task DeleteDirectCreditDebitExportAsync(Guid id);

        /// <summary>
        /// Import Text file ที่ได้จากธนาคาร เพื่อทำการตัดหนี้ในระบบ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367442/preview
        /// </summary>
        /// <returns></returns>
        Task<List<DirectCreditDebitExportHeaderDTO>> ImportDirectCreditDebitAsync(FileWithIDDTO fileDTO);

        /// <summary>
        /// Load ข้อมูลรายละเอียด Header+Detail มาแสดง บนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367444/preview
        /// </summary>
        /// <param name="DirectCreditDebitExportHeaderID"></param>
        /// <returns></returns>
        Task<List<DirectCreditDebitExportHeaderDTO>> GetDirectCreditDebitDetailAsync(Guid id);

        /// <summary>
        /// ยินยันการรับเงินจากการตัดส่งตัดไปที่ธนาคาร
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367444/preview
        /// </summary>
        /// <param name="DirectCreditDebitExportHeaderID"></param>
        /// <returns></returns>
        Task ConfirmPaymentAsync(List<DirectCreditDebitExportHeaderDTO> input);

        /// <summary>
        /// พิมพ์รายงาน FR-128
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367445/preview
        /// </summary>
        /// <param name="DirectCreditDebitExportHeaderID"></param>
        /// <returns></returns>
        Task<string> PrintAsync(Guid id);


        /// <summary>
        /// ลบ DirectCreditDebitExportHeader
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367445/preview
        /// </summary>
        /// <param name="DirectCreditDebitExportHeaderID"></param>
        /// <returns></returns>
        Task DeleteDirectCreditDebitExportAsync(Guid id);




        /// <summary>
        /// DropdowList BankAccount 
        /// </summary>
        /// <param name="ComID"></param>
        /// <param name="BankID"></param>
        /// <param name="IsDirectCredit"></param>
        /// <returns></returns>
        Task<List<BankAccountDropdownDTO>> GetBankAccountDropdowListAsync(Guid ComID, Guid BankID, string ChkKeyDirectCreditDebit);
    }
}
