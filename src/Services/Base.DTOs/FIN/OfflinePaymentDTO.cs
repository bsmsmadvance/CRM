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

namespace Base.DTOs.FIN
{
    public class OfflinePaymentDTO : BaseDTO
    {
        /// <summary>
        /// Link ข้อมูลจอง
        /// </summary>
        [Description("Link ข้อมูลจอง")]
        public SAL.BookingDTO Booking { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// วันที่รับเงิน
        /// </summary>
        [Description("วันที่รับเงิน")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// ชื่อผู้รับเงิน
        /// </summary>
        [Description("ชื่อผู้รับเงิน")]
        public USR.UserListDTO CreateBy { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงินชั่วคราว
        /// </summary>
        [Description("เลขที่ใบเสร็จรับเงินชั่วคราว")]
        public string TempReceiptNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จรับเงิน
        /// </summary>
        [Description("เลขที่ใบเสร็จรับเงิน")]
        public string ReceiptNo { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public MST.MasterCenterDropdownDTO OfflinePaymentStatusMaster { get; set; }

        /// <summary>
        /// วันที่ยืนยัน
        /// </summary>
        [Description("วันที่ยืนยัน")]
        public DateTime? ConfirmedDate { get; set; }

        /// <summary>
        /// ผู้ยืนยัน
        /// </summary>
        [Description("ผู้ยืนยัน")]
        public USR.UserListDTO ConfirmedBy { get; set; }

        
    }
}
