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
    public interface ICalculatePerMonthHighRiseSaleService
    {
        /// <summary>
        /// ดึงรายการ Commission ขาย โครงการแนวสูง
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148984/preview
        /// </summary>
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseSalePaging> GetCalculatePerMonthHighRiseSaleListAsync(CalculatePerMonthHighRiseSaleFilter filter, PageParam pageParam, CalculatePerMonthHighRiseSaleSortByParam sortByParam);

        /// <summary>
        /// คำนวณ Commission ขาย โครงการแนวสูง
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="CalculateMonth"></param>
        /// <param name="CalculateUserID"></param>
        /// <returns></returns>
        bool CalculatePerMonthHighRiseSale(Guid? ProjectID, DateTime? CalculateMonth, Guid? CalculateUserID);

        /// <summary>
        /// Approve Commission ขาย โครงการแนวสูง
        /// <param name="id"></param>
        /// <param name="ApproveUserID"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148985/preview
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseSaleDTO> ApproveCalculatePerMonthHighRiseSaleAsync(Guid id, Guid? ApproveUserID);

        /// <summary>
        /// Cancel Approve Commission ขาย โครงการแนวสูง
        /// <param name="id"></param>
        /// <paramref name="ApproveUserID"/param>
        /// https://projects.invisionapp.com/d/main?origin=v7?origin=v7#/console/17482068/368148986/preview
        /// <returns></returns>
        Task<CalculatePerMonthHighRiseSaleDTO> CancelApproveCalculatePerMonthHighRiseSaleAsync(Guid id, Guid? ApproveUserID);
    }
}
