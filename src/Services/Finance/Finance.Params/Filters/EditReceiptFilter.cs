using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class EditReceiptFilter : BaseFilter
    {
        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ 1=Active  0=Inactive  null=ALL
        /// </summary>
        public bool? ReceiptStatus { get; set; }

        /// <summary>
        /// เลขที่บัญชีธนาคาร
        /// </summary>
        public Guid? BankAccountID { get; set; }

        /// <summary>
        /// ประเภทการชำระเงิน/ชนิดของช่องทางการชำระเงิน
        /// </summary>
        public Guid? PaymentMethodTypeMasterCenterID { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        public string RVNumber { get; set; }
    }
}
