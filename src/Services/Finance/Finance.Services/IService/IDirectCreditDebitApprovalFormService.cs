using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.FIN;
using Finance.Params.Outputs;
using PagingExtensions;
using Finance.Params.Filters;
using Base.DTOs;
using Base.DTOs.MST;

namespace Finance.Services.IService
{
    public interface IDirectCreditDebitApprovalFormService
    {
        /// <summary>
        /// ดึงข้อมูลรายการขออนุมัติตัด Direct Debit/Direct Credit มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367436/preview
        /// </summary>
        /// <returns></returns>
        Task<DirectCreditDebitApprovalFormPaging> GetDirectCreditDebitApprovalFormListAsync(DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam);

        /// <summary>
        /// FR-120 ดึงข้อมูลรายการขออนุมัติตัด Direct Debit/Direct Credit เฉพาะรายการ Direct Credit ที่อายุบัตรเครดิตเหลือ น้อยกว่า 3 เดือน
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367436/preview
        /// </summary>
        /// <returns></returns>
        Task<DirectCreditDebitApprovalFormPaging> GetDirectCreditDebitApprovalFormExpire3MonthsListAsync(DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam);

        /// <summary>
        /// FR-119 เพื่อพิมพ์รายงาน (Report ID: RP_BACKFI_002)
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367436/preview
        /// </summary>
        /// <returns></returns>
        Task<FileDTO> PrintDirectCreditDebitApprovalFormAsync(List<DirectCreditDebitApprovalFormDTO> ListDirectCreditDebitApprovalForm);

        /// <summary>
        /// FR-118 เพื่อดาวน์โหลดไฟล์ Excel Request ขออนุมัติตัดบัตรเครดิตเพื่อนำส่งธนาคาร
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367436/preview
        /// </summary>
        /// <returns></returns>
        Task<FileDTO> ExportRequestAsync(bool Is3Month, DirectCreditDebitApprovalFormFilter filter);

        /// <summary>
        /// ดึงข้อมูลรายละเอียดการขออนุมัติตัด Direct Debit/Direct Credit
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367438/preview
        /// </summary>
        /// <param name="DirectCreditDebitApprovalFormHeaderID"></param>
        /// <returns></returns>
        Task<DirectCreditDebitApprovalFormDTO> GetDirectCreditDebitApprovalFormAsync(Guid? id);

        Task<DirectCreditDebitApprovalFormDTO> getDataCreateAsync(Guid? id);

        

        /// <summary>
        /// เพิ่มข้อมูลการขออนุมัติตัด Direct Debit/Direct Credit #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367438/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DirectCreditDebitApprovalFormDTO> CreateDirectCreditDebitApprovalFormAsync(DirectCreditDebitApprovalFormDTO input);

        /// <summary>
        /// บันทึกการแก้ไข รายการขออนุมัติตัด Direct Debit/Direct Credit #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <param name="DirectCreditDebitApprovalFormHeaderID"></param>
        /// <returns></returns>
        Task<DirectCreditDebitApprovalFormDTO> UpdateDirectCreditDebitApprovalFormAsync(DirectCreditDebitApprovalFormDTO input);

        /// <summary>
        /// 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <returns></returns>
        Task<GetUnitDirectCreditDebitApprovalPaging> GetUnitListAsync(Guid? id, DirectCreditDebitApprovalFormFilter filter, PageParam pageParam, DirectCreditDebitApprovalFormSortByParam sortByParam);

        /// <summary>
        /// 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <returns></returns>
        Task<List<BankAccNameDTO>> GetBankDirectDebitDropdowListAsync(Guid? ComID);

        /// <summary>
        /// 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/367232401/preview
        /// </summary>
        /// <returns></returns>
        Task<List<BankAccNameDTO>> GetBankDirectCreditDropdowListAsync(Guid? ComID);


        Task<List<MasterCenterDropdownDTO>> GetStatusDropdowListAsync();
    }
}
