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
    public class MemoMoveMoneyDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// เงินตั้งพัก
        /// </summary>
        [Description("เงินตั้งพัก")]
        public decimal Amount { get; set; }


        /// <summary>
        /// ช่องทางการรับเงิน/ประเภทชำระเงิน
        /// </summary>
        [Description("ช่องทางการรับเงิน/ประเภทชำระเงิน")]
        public MST.MasterCenterDropdownDTO PaymentMethod { get; set; }


        ///// <summary>
        ///// ธนาคาร
        ///// </summary>
        //[Description("ธนาคาร")]
        //public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// บัญชีธนาคารที่รับเงินผิด
        /// </summary>
        [Description("บัญชีธนาคารที่รับเงินผิด")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo
        /// </summary>
        [Description("บริษัท ที่สั่งจ่าย/บริษัทที่รับย้ายเงินใน Memo")]
        public MST.CompanyDropdownDTO DestinationCompany { get; set; }

        /// <summary>
        /// วันที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("วันที่พิมพ์ ล่าสุด")]
        public DateTime? PrintDate { get; set; }

        /// <summary>
        /// ผู้ที่พิมพ์ ล่าสุด
        /// </summary>
        [Description("ผู้ที่พิมพ์ ล่าสุด")]
        public USR.UserListDTO PrintBy { get; set; }

        /// <summary>
        /// สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์
        /// </summary>
        [Description("สถานะพิมพ์ 1=พิมพ์แล้ว  0=รอพิมพ์")]
        public bool IsPrint { get; set; }

        /// <summary>
        /// วัตถุประสงค์
        /// </summary>
        [Description("วัตถุประสงค์")]
        public MST.MasterCenterDropdownDTO MoveMoneyReason { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }
    }
}
