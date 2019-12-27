using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class BillPaymentDetailFilter : BaseFilter
    {
        //https://projects.invisionapp.com/d/?origin=v7#/console/17482171/370567864/preview

        /// <summary>
        /// รหัส Company
        /// </summary>
        public Guid? CompanyID { get; set; }
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Ref 1
        /// </summary>
        public string BankRef1 { get; set; }

        /// <summary>
        /// Ref 2
        /// </summary>
        public string BankRef2 { get; set; }

        /// <summary>
        /// Ref 3
        /// </summary>
        public string BankRef3 { get; set; }

        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// การชำระ
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// สถานะ Bill Payment
        /// </summary>
        public Guid? BillPaymentStatusMasterCenterID { get; set; }
    }
}
