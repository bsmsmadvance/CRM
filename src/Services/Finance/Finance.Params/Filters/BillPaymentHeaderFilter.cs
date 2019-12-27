using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class BillPaymentHeaderFilter : BaseFilter
    {//https://projects.invisionapp.com/d/?origin=v7#/console/17482171/362367446/preview
        /// <summary>
        /// เลขที่ BatchID
        /// </summary>
        public string BatchID { get; set; }

        /// <summary>
        /// วันที่ Load/Import
        /// </summary>
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public Guid? BankAccountID { get; set; }


        /// <summary>
        /// ธนาคาร
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// จำนวนรายการทั้งหมด
        /// </summary>
        public int? TotalRecordFrom { get; set; }
        public int? TotalRecordTo { get; set; }

        /// <summary>
        /// จำนวนรายการทั้งหมดที่ทำแล้ว
        /// </summary>
        public int? TotalSucessRecordFrom { get; set; }
        public int? TotalSucessRecordTo { get; set; }

        /// <summary>
        /// จำนวนรายการทั้งหมดที่รอดำเนินการ
        /// </summary>
        public int? TotalWaitingRecordFrom { get; set; }
        public int? TotalWaitingRecordTo { get; set; }

        /// <summary>
        /// ยอดรวม
        /// </summary>
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }    

        /// <summary>
        /// ผู้โหลด
        /// </summary>
        public string CreatedUserText { get; set; }
    }
}
