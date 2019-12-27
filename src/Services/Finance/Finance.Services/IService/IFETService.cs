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
    public interface IFETService
    {

        Task<List<ProjectDropdownDTO>> GetProjectDropdownListForFETAsync(string displayName, Guid? projectID);

        Task<List<FETUnitDropdownDTO>> GetUnitDropdowListForFETAsync(string displayName, Guid? projectID, Guid? unitID);


        /// ดึงข้อมูลรายการที่ต้องขอ FET มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367423/preview
        Task<FETPaging> GetFETListAsync(FETFilter filter, PageParam pageParam, FETSortByParam sortByParam);

        Task<FETPaging> GetFETProjectListAsync(FETFilterViewProject filter, PageParam pageParam, FETSortProjectListByParam sortByParam);

        Task<FETPaging> GetFETUnitListAsync(FETFilterViewUnit filter, PageParam pageParam, FETSortUnitListByParam sortByParam);

        /// เพิ่มข้อมูล FET #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367425/preview
        Task<FET> CreateFETAsync(FETDTO input);

        /// บันทึกการแก้ไข รายการ FET #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367425/preview
        Task<FET> UpdateFETAsync(FETDTO input);

        /// บันทึกการแก้ไข Status รายการ FET #UpdateStatus
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367425/preview
        Task<FET> UpdateFETStatusAsync(Guid Id);

        /// เพื่อส่งกลับให้ LC ผู้รับผิดชอบดูแลแปลงดังกล่าว แก้ไข หรือขอเอกสาร CREDIT ADVICE เพิ่มเติม โดยเมื่อคลิกที่ icon “ส่งกลับ LC” ระบบสามารถให้ผู้ใช้งานระบุข้อความที่ต้องการ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367424/preview
        Task<FET> RejectFETAsync(FETDTO input);

        /// ลบข้อมูล FET #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367424/preview
        Task<FET> DeleteFETAsync(FETDTO input);


        /// Export ข้อมูลรายการ FET มาเป็น Excel
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367424/preview
        Task<string> ExportFETAsync();

        /// Download เอกสาร Credit Advice
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367424/preview
        Task<string> DownloadCreditAdviceAsync(Guid id);

        /// Download การขอ FET 
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367426/preview
        Task<string> DownloadFETFormAsync(Guid id, Guid bankID);
    }
}
