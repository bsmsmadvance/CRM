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
    public class PostGLResultDTO : BaseDTO
    {
        /// <summary>
        /// Company
        /// </summary>
        [Description("Company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// จำนวนรายการที่ Post
        /// </summary>
        [Description("จำนวนรายการที่ Post")]
        public int SuccessRecord { get; set; }
    }
}
