using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.ACC;
using Database.Models.ACC;
using PagingExtensions;
using Accounting.Params.Filters;
using Base.DTOs.MST;
using Accounting.Params.Outputs;

namespace Accounting.Services.IService
{
    public interface IPostGLService
    {

        /// ดึงข้อมูลรายการที่ต้องขอ Post GL มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366772/preview
        Task<PostGLPaging> GetPostGLListAsync(PostGLFilter filter, PageParam pageParam, PostGLHeaderSortByParam sortByParam);

        /// บันทึกข้อมูลการ Post GL จากรายการที่เลือก #Insert
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367425/preview
        /// https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366773/preview
        Task<PostGLResultDTO> CreatePostGLAsync(PostGLHeaderDTO input);

        /// ถอย Post GL
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366775/preview
        Task<int> CancelPostGLAsync(List<PostGLHeaderDTO> input);

        /// Export Text file to SAP
        /// เมื่อ CreatePostGLAsync เสร็จ หรือ กด Export Text file To SAP
        /// ทำการสร้าง text file แล้วไปวางไว้ที่ Share folder เพื่อรอ import เข้า SAP
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366774/preview
        Task<string> GenTextFileToSAPAsync(List<PostGLHeaderDTO> input);

        /// Export ข้อมูลในตารางออกมาเป็น Excel
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366774/preview
        Task<string> ExportExcelAsync(List<PostGLHeaderDTO> input);

        /// Preview รายละเอียดข้อมูลการ Post GL
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366777/preview
        Task<PostGLHeaderDTO> GetPostGLAsync(PostGLHeaderDTO input);

        /// <summary>ตรวจสอบสถานะ Post GL ของ Transaction
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366777/preview        
        /// <summary>
        /// <param name="referentID">ID รายการที่ต้องการตรวจสอบ</param>
        /// <param name="referentType">DepositHeader,PaymentMethod,UnknowPayment,CancelMemo,ChangeUnitWorkflow,PostGLHeader</param>
        /// <returns>true=Post แล้ว   false=ยังไม่ Post</returns>
        Task<bool> CheckPostGLAsync(Guid referentID,string referentType);

        /// <summary>
        /// ถอย Post GL
        /// </summary>
        /// <param name="referentID">ID รายการที่ต้องการ ถอย Post</param>
        /// <param name="referentType">DepositHeader,PaymentMethod,UnknowPayment,CancelMemo,ChangeUnitWorkflow,PostGLHeader</param>
        /// <returns>Document No CA ที่ได้จากการถอย Post</returns>
        Task<string> CancelPostGLAsync(Guid referentID, string referentType);
    }
}
