using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class TransferFilter : BaseFilter
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
        /// ชื่อ - นามสกุล ลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// วันที่ยืนยันโอนจริง
        /// </summary>
        public DateTime? TransferConfirmedDateFrom { get; set; }
        public DateTime? TransferConfirmedDateTo { get; set; }

        /// <summary>
        /// ยอดคงเหลือ AP
        /// </summary>
        public decimal? APBalanceFrom { get; set; }
        public decimal? APBalanceTo { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย ณ วันโอน
        /// </summary>
        public decimal? CostAmountFrom { get; set; }
        public decimal? CostAmountTo { get; set; }

        /// <summary>
        /// Free down
        /// </summary>
        public decimal? FreeDownAmountFrom { get; set; }
        public decimal? FreeDownAmountTo { get; set; }

        /// <summary>
        /// Cheque
        /// </summary>
        public decimal? ChequeAmountFrom { get; set; }
        public decimal? ChequeAmountTo { get; set; }

        /// <summary>
        /// เงินโอนผ่านธนาคาร
        /// </summary>
        public decimal? BankTransferAmountFrom { get; set; }
        public decimal? BankTransferAmountTo { get; set; }

        /// <summary>
        /// เงินสด
        /// </summary>
        public decimal? CashAmountFrom { get; set; }
        public decimal? CashAmountTo { get; set; }

        /// <summary>
        /// เงินทอน
        /// </summary>
        public decimal? APChangeAmountFrom { get; set; }
        public decimal? APChangeAmountTo { get; set; }

        /// <summary>
        /// สถานะ confirm เงิน
        /// </summary>
        public bool? IsPaymentConfirmed { get; set; }

        /// <summary>
        /// สถานะ confirm จากบัญชี
        /// </summary>
        public bool? IsAccountApproved { get; set; }
    }
}
