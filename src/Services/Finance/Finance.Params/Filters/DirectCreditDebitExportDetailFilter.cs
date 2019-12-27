using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class DirectCreditDebitExportDetailFilter
    {
        /// <summary>
        /// BatchID 
        /// </summary>
        public string BatchID { get; set; }

        /// <summary>
        /// โครงการ 
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// Unit no 
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// เลขที่บัญชี/บัตรเครดิต 
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// งวด 
        /// </summary>
        public int? PeriodDate { get; set; }

        /// <summary>
        /// DueDate จาก 
        /// </summary>
        public DateTime? DueDateFrom { get; set; }

        /// <summary>
        /// DueDate ถึง 
        /// </summary>
        public DateTime? DueDateTo { get; set; }

        /// <summary>
        /// เลขที่ สัญญา
        /// </summary>
        public string AgreementNo { get; set; }

        /// <summary>
        /// ชื่อ ลูกค้า 
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// จำนวนเงินจาก
        /// </summary>
        public decimal? AmountFrom { get; set; }

        /// <summary>
        /// จำนวนเงินถึง
        /// </summary>
        public decimal? AmountTo { get; set; }

        /// <summary>
        /// สถานะนำเข้า
        /// </summary>
        public Guid? DirectCreditDebitExportDetailStatus { get; set; }
    }
}
