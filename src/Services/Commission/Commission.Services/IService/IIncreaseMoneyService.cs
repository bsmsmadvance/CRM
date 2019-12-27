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
    public interface IIncreaseMoneyService
    {
        /// <summary>
        /// ดึงรายการเพิ่ม Commission
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148953/preview
        /// </summary>
        /// <returns></returns>
        Task<IncreaseMoneyPaging> GetIncreaseMoneyListAsync(IncreaseMoneyFilter filter, PageParam pageParam, IncreaseMoneySortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> GetIncreaseMoneyAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148954/preview
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> CreateIncreaseMoneyAsync(IncreaseDeductMoneyDTO input);

        /// <summary>
        /// แก้ไขข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id, input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148954/preview
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> UpdateIncreaseMoneyAsync(Guid id, IncreaseDeductMoneyDTO input);

        /// <summary>
        /// ลบข้อมูลเพิ่ม Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IncreaseMoney> DeleteIncreaseMoneyAsync(Guid id);
    }
}
