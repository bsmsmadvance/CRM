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
    public interface ICalculatePerMonthHighRiseTransferService
    {
        /// <summary>
        /// ดึงรายการ Commission โครงการแนวสูง โอน
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148988/preview
        /// </summary>
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseTransferPaging> GetCalculatePerMonthHighRiseTransferListAsync(CalculatePerMonthHighRiseTransferFilter filter, PageParam pageParam, CalculatePerMonthHighRiseTransferSortByParam sortByParam);

        /// <summary>
        /// คำนวณ Commission
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <returns></returns>
        bool CalculatePerMonthHighRiseTransfer(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID);

        /// <summary>
        /// Approve Commission โครงการแนวสูง โอน
        /// </summary>
        /// <param name="id, ApproveUserID"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148989/preview
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseTransferDTO> ApproveCalculatePerMonthHighRiseTransferAsync(Guid id, Guid? ApproveUserID);

        /// <summary>
        /// Cancel Approve Commission โครงการแนวสูง โอน
        /// </summary>
        /// <param name="id, ApproveUserID"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148991/preview
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseTransferDTO> CancelApproveCalculatePerMonthHighRiseTransferAsync(Guid id, Guid? ApproveUserID);
    }
}
