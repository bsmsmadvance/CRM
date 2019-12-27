using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;

namespace Base.DTOs.ACC
{
    public class PostGLDetailDTO : BaseDTO
    {
        /// <summary>
        /// ประเภทรายการ Credit/Debit
        /// </summary>
        [Description("ประเภทรายการ Credit/Debit")]
        public string PostGLType { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่ GL และ ชื่อ GL
        /// </summary>
        [Description("เลขที่ GL และ ชื่อ GL")]
        public BankAccountDropdownDTO GLAccount { get; set; }




    }
}
