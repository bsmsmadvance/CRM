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
    public interface IMemoMoveMoneyService
    {
        /// <summary>
        /// ดึงข้อมูลรายการ Memoย้ายเงิน มาแสดงบนหน้าจอ
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367484/preview
        /// </summary>
        /// <returns></returns>
        Task<MemoMoveMoneyPaging> GetMemoMoveMoneyListAsync(MemoMoveMoneyFilter filter, PageParam pageParam, MemoMoveMoneySortByParam sortByParam);


        /// <summary>
        /// แสดงรายการที่เลือกสำหรับพิมพ์ Memo
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367485/preview
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task <List<MemoMoveMoneyDTO>> PrintMemoMoveMoneyAsync(List<MemoMoveMoneyDTO> input);

    }
}
