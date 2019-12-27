using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class MemoMoveMoneyFilter : BaseFilter
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
        /// เลขที่ใบเสร็จรับเงิน
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// วันที่รับเงิน
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// ประเภทการรับชำระเงิน
        /// </summary>
        public Guid? PaymentMethodID { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public Guid? BankAccountID { get; set; }

        /// <summary>
        /// บริษัทที่รับเงินผิดจากลูกค้า
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// บริษัทที่รับย้ายเงินใน Memo
        /// </summary>
        public Guid? DestinationCompanyID { get; set; }

        /// <summary>
        /// สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์
        /// </summary>
        public bool? IsPrint { get; set; }

        /// <summary>
        /// ผู้พิมพ์
        /// </summary>
        public string PrintBy { get; set; }

        /// <summary>
        /// วันที่พิมพ์ ล่าสุด
        /// </summary>
        public DateTime? PrintDateFrom { get; set; }
        public DateTime? PrintDateTo { get; set; }
    }
}
