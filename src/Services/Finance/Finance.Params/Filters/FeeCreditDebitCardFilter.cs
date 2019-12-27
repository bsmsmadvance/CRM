using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class FeeCreditDebitCardFilter : BaseFilter
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
        /// สถานะตรวจสอบ
        /// </summary>
        public bool? FeeConfirmStatus { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        public string ReceiveNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// เครื่องรูดบัตร
        /// </summary>
        public Guid? EDCID { get; set; }

        /// <summary>
        /// ธนาคารเจ้าของบัตร/บัตรที่รูด
        /// </summary>
        public Guid? BankID { get; set; }

        /// <summary>
        /// ประเภทบัตร (Visa, Master, JCB)
        /// </summary>
        public Guid? CreditCardTypeMasterCenterID { get; set; }

        /// <summary>
        /// เลขที่บัตร
        /// </summary>
        public string CardNo { get; set; }


        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// % ค่าธรรมเนียม
        /// </summary>
        public decimal? FeePercentFrom { get; set; }
        public decimal? FeePercentTo { get; set; }

        /// <summary>
        /// มูลค่าธรรมเนียม
        /// </summary>
        public decimal? FeeAmountFrom { get; set; }
        public decimal? FeeAmountTo { get; set; }

        /// <summary>
        /// มูลค่า Vat
        /// </summary>
        public double? VatFrom { get; set; }
        public double? VatTo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? PayAmountFrom { get; set; }
        public decimal? PayAmountTo { get; set; }

        /// <summary>
        /// มูลค่าสุทธิ
        /// </summary>
        public decimal? FeeIncludingVatFrom { get; set; }
        public decimal? FeeIncludingVatTo { get; set; }

        /// <summary>
        /// วันที่แก้ไขล่าสุด
        /// </summary>
        public DateTime? UpdatedDateFrom { get; set; }
        public DateTime? UpdatedDateTo { get; set; }

        /// <summary>
        /// ผู้แก้ไขล่าสุด
        /// </summary>
        public string UpdatedByUser { get; set; }


        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositNo { get; set; }
        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        public bool? DepositStatus { get; set; }

        /// <summary>
        /// สถานะ PostPI
        /// </summary>
        public bool? PostPIStatus { get; set; }
    }
}
