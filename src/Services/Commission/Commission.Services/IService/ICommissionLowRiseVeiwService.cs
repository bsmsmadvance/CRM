using Database.Models.CMS;
using Commission.Params.Filters;
using Base.DTOs.CMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commission.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace Commission.Services
{
    public interface ICommissionLowRiseVeiwService
    {
        /// <summary>
        /// ดึงรายการผลการคำนวณ Commission ขาย/โอน (โครงการแนวราบ)
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148982/preview
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148983/preview
        /// </summary>
        /// <returns></returns>
        Task<CommissionLowRiseVeiwPaging> GetCommissionLowRiseVeiwListAsync(CommissionLowRiseVeiwFilter filter, PageParam pageParam, CommissionLowRiseVeiwSortByParam sortByParam);

        /// <summary>
        /// Export Excel Commission โครงการแนวราบ
        /// </summary>
        /// <param name="ProjectID"></param
        /// <param name="CommissionMonth"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148981/preview
        /// <returns></returns>
        Task<FileDTO> ExportExcelCommissionLowRiseAsync(Guid? ProjectID, DateTime? CommissionMonth);
    }
}
