using Database.Models.MST;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.Linq;
using Database.Models.ACC;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.FIN;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Base.DTOs.SAL;

namespace Base.DTOs.FIN
{
    public class TransferPaymentDTO : BaseDTO
    {
        /// <summary>
        /// ใบจอง
        /// </summary>
        [Description("ใบจอง")]
        public Guid BookingID { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// ชำระโดย
        /// </summary>
        [Description("ชำระโดย")]
        public MasterCenterDropdownDTO PaymentMethodType { get; set; }
        
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime ReceiveDate { get; set; }


        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        [Description("เลขที่เช็ค")]
        public string ChequeNo { get; set; }
        
        /// <summary>
        /// วันที่เช็ค
        /// </summary>
        [Description("วันที่เช็ค")]
        public DateTime? ChequeDate { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public BankDTO Bank { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        [Description("สถานะนำฝาก")]
        public string DepositStatus { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        [Description("เลขที่นำฝาก")]
        public string DepositNo { get; set; }

        /// <summary>
        /// ชำระรายละเอียด
        /// </summary>
        [Description("ชำระรายละเอียด")]
        public string PaymentBy { get; set; }

        /// <summary>
        /// จำนวนเงินที่ต้องจ่าย
        /// </summary>
        [Description("จำนวนเงินที่ต้องจ่าย")]
        public decimal PayAmount { get; set; }

    }
}
