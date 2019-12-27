using System;

namespace Finance.Params.Filters
{
    public class FETFilter
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        /// <summary>
        /// จำนวนแปลงสัญญาต่างชาติ
        /// </summary>
        public int? AgreementNumberFrom { get; set; }
        public int? AgreementNumberTo { get; set; }

        /// <summary>
        /// จำนวนแปลงที่ขอ FET
        /// </summary>
        public int? FETNumberFrom { get; set; }
        public int? FETNumberTo { get; set; }

        /// <summary>
        /// จำนวนเงินที่ขอ FET
        /// </summary>
        public decimal? FETAmountFrom { get; set; }
        public decimal? FETAmountTo { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public Guid? UnitID { get; set; }

        /// <summary>
        /// ชื่อ-สกุล ลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? CompanyID { get; set; }

        /// <summary>
        /// จำนวนรายการที่ขอ FET - Sub Table
        /// </summary>
        public int FETNumberSub { get; set; }

        /// <summary>
        /// จำนวนเงินที่ขอ FET - Sub Table
        /// </summary>
        public decimal? FETAmountSub { get; set; }

        //////////////////////////////////////////////////////////////////
        /// <summary>
        /// ผู้ขอ
        /// </summary>
        public Guid? FETRequesterID { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositCode { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal? ReceiveAmountFrom { get; set; }
        public decimal? ReceiveAmountTo { get; set; }

        /// <summary>
        /// ประเภทการชำระเงิน
        /// </summary>
        //public Guid? FETRequesterID { get; set; }

        /// <summary>
        /// สถานะการขอ FET
        /// </summary>
        public Guid? FETStatusMasterCenterID { get; set; }

        /// <summary>
        /// วันที่แก้ไขล่าสุด
        /// </summary>
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }

        /// <summary>
        /// แก้ไขโดย
        /// </summary>ถ้าอยากปะทะ ถามพระดิว่างไหม
        public Guid? UpdatedByUserID { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ประเภทการชำระเงิน
        /// </summary>
        public Guid? PayStatus { get; set; }

        public string DepositNO { get; set; }

        public Guid? BankID { get; set; }
    }
}
