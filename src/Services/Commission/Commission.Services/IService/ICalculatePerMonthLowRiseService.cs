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
    public interface ICalculatePerMonthLowRiseService
    {
        /// <summary>
        /// ดึงรายการ Commission โครงการแนวราบ
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148981/preview
        /// </summary>
        /// <returns></returns>
        Task<CalculatePerMonthLowRisePaging> GetCalculatePerMonthLowRiseListAsync(CalculatePerMonthLowRiseFilter filter, PageParam pageParam, CalculatePerMonthLowRiseSortByParam sortByParam);

        /// <summary>
        /// คำนวณ Commission
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <param name="CalculateUserID"></param>
        /// <returns></returns>
        bool CalculatePerMonthLowRise(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID);

        /// <summary>
        /// Approve Commission โครงการแนวราบ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148982/preview
        /// <returns></returns>
        Task<CalculatePerMonthLowRiseDTO> ApproveCalculatePerMonthLowRiseAsync(Guid id, Guid? ApproveUserID);

        /// <summary>
        /// Cancel Approve Commission โครงการแนวราบ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148983/preview
        /// <returns></returns>
        Task<CalculatePerMonthLowRiseDTO> CancelApproveCalculatePerMonthLowRiseAsync(Guid id, Guid? ApproveUserID);
    }
}
