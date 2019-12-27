using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class DirectCreditDebitExportHeaderFilter
    {
        /// <summary>
        /// เลขที่  BatchID
        /// </summary>
        public string BatchID { get; set; }

        /// <summary>
        /// ธนาคาร 
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// บัญชีบริษัท 
        /// </summary>
        public string BankAccountNo { get; set; }

        /// <summary>
        /// บริษัท 
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// รอบการตัดเงิน 
        /// </summary>
        public DateTime? PeriodDateFrom { get; set; }
        public DateTime? PeriodDateTo { get; set; }

        /// <summary>
        /// วันที่ตัดเงิน 
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// ประเภท 
        /// </summary>
        public Guid? DirectFormTypeMasterCenterID { get; set; }

        /// <summary>
        /// จำนวนรายการทั้งหมด 
        /// </summary>
        public int? TotalRecord { get; set; }

        /// <summary>
        /// จำนวนรายการผิดพลาด 
        /// </summary>
        public int? TotalErrorRecord { get; set; }

        /// <summary>
        /// ยอดรวมเงิน 
        /// </summary>
        public decimal? TotalAmountFrom { get; set; }
        /// <summary>
        /// ยอดรวมเงิน 
        /// </summary>
        public decimal? TotalAmountTo { get; set; }
        /// <summary>
        /// วันที่ Import 
        /// </summary>
        public DateTime? ImportDateFrom { get; set; }
        public DateTime? ImportDateTo { get; set; }

        /// <summary>
        /// Import โดย 
        /// </summary>
        public string ImportBy { get; set; }

    }
}
