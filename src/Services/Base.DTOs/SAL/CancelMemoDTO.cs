using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    public class CancelMemoDTO : BaseDTO
    {
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }
        /// <summary>
        /// รูปแบบการยกเลิก
        /// Master/api/MasterCenters?masterCenterGroupKey=CancelReturnType
        /// </summary>
        public MST.MasterCenterDropdownDTO CancelReturn { get; set; }
        /// <summary>
        /// เหตุผลที่ยกเลิก
        /// Master/api/CancelReasons/DropdownList
        /// </summary>
        [Description("เหตุผลที่ยกเลิก")]
        public MST.CancelReasonDropdownDTO CancelReason { get; set; }
        /// <summary>
        /// หลักฐานกรณีกู้เงินไม่ผ่าน
        /// </summary>
        public FileDTO BankRejectDocument { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string CancelRemark { get; set; }
        /// <summary>
        /// รับเงินจากลูกค้า
        /// </summary>
        public decimal? TotalReceivedAmount { get; set; }
        /// <summary>
        /// มูลค่ารายการของที่ส่งมอบไปแล้ว
        /// </summary>
        public decimal? TotalPromotionDeliverAmount { get; set; }
        /// <summary>
        /// เบื้ยปรับ
        /// </summary>
        public decimal? PenaltyAmount { get; set; }
        /// <summary>
        /// เงินคืนลูกค้า
        /// </summary>
        public decimal? ReturnAmount { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - ธนาคาร
        /// </summary>
        public MST.BankDropdownDTO ReturnBank { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - บัญชีธนาคาร
        /// </summary>
        public string ReturnBankAccount { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/api/Provinces/DropdownList
        /// </summary>
        public MST.ProvinceListDTO Province { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - สาขา
        /// </summary>
        public MST.BankBranchDropdownDTO ReturnBankBranch { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - ชื่อบัญชี
        /// </summary>
        public string ReturnBankAccountName { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - เลขบัตรประชาชน
        /// </summary>
        public string ReturnCitizenIdentityNo { get; set; }
        /// <summary>
        /// ข้อมูลการจ่ายเงินคืนลูกค้า - สำเนา Book Bank
        /// </summary>
        public FileDTO ReturnBookBankFile { get; set; }

        public void ToModel(ref CancelMemo model)
        {
            model.BookingID = this.Booking?.Id;
            model.CancelReturnMasterCenterID = this.CancelReturn?.Id;
            model.CancelReasonID = this.CancelReason?.Id;
            model.CancelRemark = this.CancelRemark;
            model.TotalReceivedAmount = this.TotalReceivedAmount;
            model.TotalPromotionDeliverAmount = this.TotalPromotionDeliverAmount;
            model.PenaltyAmount = this.PenaltyAmount;
            model.ReturnAmount = this.ReturnAmount;
            model.ReturnBankID = this.ReturnBank?.Id;
            model.ReturnBankAccount = this.ReturnBankAccount;
            model.ReturnBankBranchID = this.ReturnBankBranch?.Id;
            model.ReturnBankAccountName = this.ReturnBankAccountName;
            model.ReturnCitizenIdentityNo = this.ReturnCitizenIdentityNo;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            //TODO: [NP] Final Validate CancelReason
            ValidateException ex = new ValidateException();
            if (this.CancelReason == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CancelMemoDTO.CancelReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

    }
}
