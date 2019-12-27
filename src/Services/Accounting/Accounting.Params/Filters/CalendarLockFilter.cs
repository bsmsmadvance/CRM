using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.ACC;
using Base.DTOs.FIN;

namespace Accounting.Params.Filters
{
    public class CalendarLockFilter
    {
        /// <summary>
        /// บริษัท GUID,GUID,GUID,..
        /// </summary>
        public string Companies { get; set; }

        /// <summary>
        /// ปีที่เลือก
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// เดือนที่เลือก
        /// </summary>
        public string Month { get; set; }
    }

    public class CalendarLockReq
    {
        public string Date { get; set; }
        public string Guid { get; set; }
        public string CalendarLock { get; set; }
        public int status  { get; set; }
    }
}
