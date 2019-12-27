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
    public interface IDeductMoneyService
    {
        /// <summary>
        /// ดึงรายการหัก Commission
        /// Paging, Sort, Filter
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148956/preview
        /// </summary>
        /// <returns></returns>
        Task<DeductMoneyPaging> GetDeductMoneyListAsync(DeductMoneyFilter filter, PageParam pageParam, DeductMoneySortByParam sortByParam);

        /// <summary>
        /// ดึงข้อมูลหัก Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> GetDeductMoneyAsync(Guid id);

        /// <summary>
        /// สร้างข้อมูลหัก Commission
        /// </summary>
        /// <param name="input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148957/preview
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> CreateDeductMoneyAsync(IncreaseDeductMoneyDTO input);

        /// <summary>
        /// แก้ไขข้อมูลหัก Commission
        /// </summary>
        /// <param name="id, input"></param>
        /// https://projects.invisionapp.com/d/main?origin=v7#/console/17482068/368148957/preview
        /// <returns></returns>
        Task<IncreaseDeductMoneyDTO> UpdateDeductMoneyAsync(Guid id, IncreaseDeductMoneyDTO input);

        /// <summary>
        /// ลบข้อมูลหัก Commission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeductMoney> DeleteDeductMoneyAsync(Guid id);
    }
}
