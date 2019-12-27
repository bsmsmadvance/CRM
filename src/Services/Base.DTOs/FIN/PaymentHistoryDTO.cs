using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    public class PaymentHistoryDTO : BaseDTO
    {
        /// <summary>
        /// ขอ Fet
        /// </summary>
        public bool? IsFET { get; set; }
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวนเงินรวม
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// ชนิดของช่องทางชำระ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentMethod
        /// </summary>
        [Description("ชนิดของช่องทางชำระ")]
        public MST.MasterCenterDropdownDTO PaymentMethodType { get; set; }
        /// <summary>
        /// สถานะบันทึกการนำฝาก
        /// </summary>
        public DepositHeaderDTO DepositHeader { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public ReceiptTempListDTO Receipt { get; set; }


        public UserDTO PaymentCreated { get; set; }

        /// <summary>
        /// ข้อมูล Post GL
        /// สถานะ "Post GL แล้ว" = Object นี้มีค่า
        /// สถานะ "ยังไม่ Post GL" = Object เป็นค่า NULL
        /// </summary>
        [Description("ข้อมูล Post GL")]
        public ACC.PostGLHeaderDTO PostGL { get; set; }

    }
}
