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
    public class PostGLFilter
    {
        /// <summary>
        /// บริษัท 
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// เลขที่ Doc PI,RV,JV,CA 
        /// </summary>
        public string DocumentNo { get; set; }        

        /// <summary>
        /// Document Date
        /// </summary>
        public DateTime? DocumentDateFrom { get; set; }
        public DateTime? DocumentDateTo { get; set; }

        /// <summary>
        /// ประเภท Doc RV,JV,PI,CA
        /// </summary>
        public Guid? PostGLDocumentTypeMasterCenterID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public Guid? BankAccountID { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal? FeeFrom { get; set; }
        public decimal? FeeTo { get; set; }

        /// <summary>
        /// คงเหลือ
        /// </summary>
        public decimal? RemainAmountFrom { get; set; }
        public decimal? RemainAmountTo { get; set; }

        /// <summary>
        /// โพสโดย
        /// </summary>
        public string PostedBy { get; set; }

        /// <summary>
        /// วันที่โพส
        /// </summary>
        public DateTime? PostedDateFrom { get; set; }
        public DateTime? PostedDateTo { get; set; }

        /// <summary>
        /// สถานะโพส null=all,1=active ,0=cancel
        /// </summary>
        public bool? ActiveStatus { get; set; }
    }
}
