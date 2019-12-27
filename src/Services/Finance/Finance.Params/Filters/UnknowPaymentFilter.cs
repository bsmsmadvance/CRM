using System;

namespace Finance.Params.Filters
{
    public class UnknownPaymentFilter : BaseFilter
    {
        /// <summary>
        /// วันที่ตั้งพัก
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// วันที่ตั้งพัก
        /// </summary>
        public DateTime? ReverseDateFrom { get; set; }
        public DateTime? ReverseDateTo { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public Guid? BankAccountID { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public Guid? Unit { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        public Guid? UnknownPaymentStatus { get; set; }

        /// <summary>
        /// เลขที่ตั้งพัก/เลขที่ PI
        /// </summary>
        public string UnknownPaymentCode { get; set; }

        /// <summary>
        /// จำนวนเงินตั้งพัก
        /// </summary>
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }

        /// <summary>
        /// สถานะการบัญทึกบัญชี PI
        /// </summary>
        public bool? IsPostPI { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        public string RVDocumentCode { get; set; }

        /// <summary>
        /// ผู้บันทึก
        /// </summary>
        public string CreatedUserText { get; set; }

        /// <summary>
        /// ผู้กลับรายการ
        /// </summary>
        public string ReversedUserText { get; set; }

    }
}
