using System;

namespace Finance.Params.Filters
{
    public class ReceiptInfoFilter : BaseFilter
    {
        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public Guid? UnitID { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// บัญชีธนาคาร
        /// </summary>
        public Guid? BankAccountID { get; set; }

        /// <summary>
        /// ประเภทการชำระ
        /// </summary>
        public Guid? PaymentMethodTypeID { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public string ReceiptDescription { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        public string RVNumber { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary>
        public bool? ReceiptStatus { get; set; }
    }
}
