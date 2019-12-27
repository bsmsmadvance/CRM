using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วยการโอนเงินต่างประเทศ
    /// Model: PaymentForeignBankTransferDTO
    /// </summary>
    public class PaymentForeignBankTransferDTO : BaseDTO
    {
        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// บัญชีที่โอนเข้า
        /// Master/api/BankAccounts/DropdownList?bankAccountTypeKey="2"
        /// </summary>
        public MST.BankAccountDropdownDTO BankAccount { get; set; }
        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        public bool IsWrongAccount { get; set; }

        /// <summary>
        /// ธนาคารที่รับเงินก่อนเข้า AP
        /// </summary>
        public MST.BankDropdownDTO ForeignBank { get; set; }
        /// <summary>
        /// ประเภทการโอนเงินต่างประเทศ
        /// </summary>
        public MST.MasterCenterDropdownDTO ForeignTransferType { get; set; }
        /// <summary>
        /// IR
        /// </summary>
        public string IR { get; set; }
        /// <summary>
        /// ชื่อผู้โอน
        /// </summary>
        [Description("ชื่อผู้โอน")]
        public string TransferorName { get; set; }
        /// <summary>
        /// ต้องขอ FET หรือไม่
        /// </summary>
        public bool IsRequestFET { get; set; }
        /// <summary>
        /// แจ้งแก้ไข FET
        /// </summary>
        public bool IsNotifyFET { get; set; }
        /// <summary>
        /// ข้อความแจ้งเตือน
        /// </summary>
        public string NotifyFETMemo { get; set; }

        public static PaymentForeignBankTransferDTO CreateFromModel(PaymentForeignBankTransfer model)
        {
            if (model != null)
            {
                var result = new PaymentForeignBankTransferDTO();
                result.Id = model.ID;
                result.Fee = model.Fee;
                result.BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model.BankAccount);
                result.IsWrongAccount = model.IsWrongAccount;
                result.ForeignBank = MST.BankDropdownDTO.CreateFromModel(model.ForeignBank);
                result.ForeignTransferType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ForeignTransferType);
                result.IR = model.IR;
                result.TransferorName = model.TransferorName;
                result.IsRequestFET = model.IsRequestFET;
                result.IsNotifyFET = model.IsNotifyFET;
                result.NotifyFETMemo = model.NotifyFETMemo;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
