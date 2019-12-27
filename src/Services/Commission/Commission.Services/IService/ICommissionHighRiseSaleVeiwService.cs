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
    public interface ICommissionHighRiseSaleVeiwService
    {
        /// <summary>
        /// ดึงรายการผลการคำนวณ Commission ขาย (โครงการแนวสูง)
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148985/preview
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148986/preview
        /// </summary>
        /// <returns></returns>
        Task<CommissionHighRiseSaleVeiwPaging> GetCommissionHighRiseSaleVeiwListAsync(CommissionHighRiseSaleVeiwFilter filter, PageParam pageParam, CommissionHighRiseSaleVeiwSortByParam sortByParam);

    }
}
