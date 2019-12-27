using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class OfflinePaymentFilter : BaseFilter
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// วันที่รับเงิน
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// ชื่อผู้รับเงิน
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string TempReceiptNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงิน
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        public Guid? OfflinePaymentStatusMasterCenter { get; set; }
               
        /// <summary>
        /// วันที่ยืนยัน
        /// </summary>
        public DateTime? ConfirmedDateFrom { get; set; }
        public DateTime? ConfirmedDateTo { get; set; }

        /// <summary>
        /// ผู้ยืนยัน
        /// </summary>
        public string ConfirmedByName { get; set; }
    }
}
