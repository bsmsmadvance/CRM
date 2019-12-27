using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs.ACC;
using Database.Models.ACC;
using PagingExtensions;
using Accounting.Params.Filters;
using Base.DTOs.MST;


namespace Accounting.Services.IService
{
    public interface ICalendarLockService
    {
        /// <summary>
        /// ดึงข้อมูลวันที่ปิดบัญชีมาแสดงบนหน้าจอ
        /// Table : ACC.CalendarLock
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362366778/preview
        /// </summary>
        /// <returns></returns>
        Task<string> GetCalendarLockListAsync(CalendarLockFilter filter);


        /// <summary>
        /// บันทึกสถานะการ Lock Lock/Unlock
        /// Table : ACC.CalendarLock
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input">ข้อมูลการ Lock</param>
        /// <returns></returns>
        Task<string> AddUpdateCalendarLockAsync(List<CalendarLockReq> input);

        /// <summary>
        /// ดึงข้อมูลวันที่ปิดบัญชีมาแสดงบนหน้าจอ
        /// Table : ACC.CalendarLock
        /// UI: https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367434/preview
        /// </summary>
        /// <param name="input">ข้อมูลการ Lock</param>
        /// <returns></returns>
        Task<List<CalendarLockHistoryDTO>> GetCalendarLockHistoryAsync(CalendarLockReq input);
  
    }
}
