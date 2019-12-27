using System;

namespace Finance.Params.Filters
{
    public class DepositFilter
    {
        /// <summary>
        /// ประเภทการรับชำระเงิน 
        /// key = PaymentMethod
        /// </summary>
        public Guid? PaymentMethodTypeID { get; set; }

        /// <summary>
        /// วันที่นำฝาก
        /// </summary>
        public DateTime? DepositDateFrom { get; set; }
        public DateTime? DepositDateTo { get; set; }

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
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// วันที่เงินเข้า
        /// </summary>
        public DateTime? PaymentDateFrom { get; set; }
        public DateTime? PaymentDateTo { get; set; }

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
        /// ชำระค่า
        /// </summary>
        public Guid? MethodTypeID { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal? FeeAmountFrom { get; set; }
        public decimal? FeeAmountTo { get; set; }

        /// <summary>
        /// VAT
        /// </summary>
        public decimal? VATAmountFrom { get; set; }
        public decimal? VATAmountTo { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// null = ทั้งหมด
        /// true = นำฝากแล้ว
        /// false = รอนำฝาก
        /// </summary>
        public bool? IsDeposit { get; set; }

        /// <summary>
        /// สถานะ Post
        /// </summary>
        public bool? IsPostGL { get; set; }

        /// <summary>
        /// เลขที่ PI
        /// </summary>
        public string BatchID { get; set; }


        ///// <summary>
        ///// วันที่ใบเสร็จ,เลขที่ใบเสร็จ,ชำระค่า,จำนวนเงิน
        ///// </summary>
        //public ReceiptTempListDTO ReceiptTemp { get; set; }  

        ///// <summary>
        ///// สถานะ Post GL,เลขที่ PI
        ///// </summary>
        //public PostGLDTO PostGL { get; set; }
    }
}
