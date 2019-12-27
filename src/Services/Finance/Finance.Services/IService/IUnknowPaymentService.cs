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
    public interface IUnknownPaymentService
    {

        Task<List<ProjectDropdownDTO>> GetProjectDropdownListAsync(string displayName, Guid? companyID);
        Task<List<SoldUnitDropdownDTO>> GetSoldUnitDropdowListAsync(string displayName, Guid? projectID);


        /// <summary>
        /// ดึงข้อมูลรายละเอียดการบันทึกเงินโอนไม่ทราบผู้โอน
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367456/preview
        /// </summary>
        /// <param name="UnknownPaymentID"></param>
        Task<UnknownPaymentDTO> GetUnknownPaymentAsync(Guid id);
        /// <summary>
        /// ดึงข้อมูลรายการบันทึกเงินโอนไม่ทราบผู้โอน มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367454/preview
        /// </summary>
        /// <returns></returns>
        Task<UnknownPaymentPaging> GetUnknownPaymentListAsync(UnknownPaymentFilter filter, PageParam pageParam, UnknownPaymentSortByParam sortByParam);


        /// <summary>
        /// เพิ่มข้อมูลการบันทึกเงินโอนไม่ทราบผู้โอน #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367455/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UnknownPayment> CreateUnknownPaymentAsync(UnknownPaymentDTO input);

        Task<UnknownPaymentDTO> ValidateBeforeUpdateAsync(Guid id, int UpdateType);

        /// <summary>
        /// บันทึกการแก้ไข บันทึกเงินโอนไม่ทราบผู้โอน #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367456/preview
        /// </summary>
        /// <param name="UnknownPaymentID"></param>
        /// <returns></returns>
        Task<UnknownPayment> UpdateUnknownPaymentAsync(UnknownPaymentDTO input);

        /// <summary>
        /// บันทึกรายการด้าน SAP #Update
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367458/preview
        /// </summary>
        /// <param name="UnknownPaymentID"></param>
        /// <returns></returns>
        Task<UnknownPayment> UpdateUnknownPaymentForSAPAsync(UnknownPaymentDTO input);

        /// <summary>
        /// ลบข้อมูลการบันทึกเงินโอนไม่ทราบผู้โอน #Delete
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367460/preview
        /// </summary>
        /// <param name="UnknownPaymentID"></param>
        /// <returns></returns>
        Task<UnknownPayment> DeleteUnknownPaymentAsync(UnknownPaymentDTO input);


        /// <summary>
        /// ดึงข้อมูลรายละเอียดการบันทึกเงินโอนไม่ทราบผู้โอน เพื่อกลับรายการ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367457/preview
        /// </summary>
        /// <param name="UnknownPaymentID"></param>
        /// <returns></returns>
        Task<UnknownPaymentDetailDTO> GetUnknownPaymentForReverseAsync(Guid id);
    }
}
