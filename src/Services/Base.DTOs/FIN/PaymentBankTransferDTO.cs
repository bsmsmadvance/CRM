using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วยการโอนเงินผ่านธนาคาร
    /// Model: PaymentBankTransferDTO
    /// </summary>
    public class PaymentBankTransferDTO : BaseDTO
    {
        /// <summary>
        /// บัญชีธนาคาร
        /// Master/api/BankAccounts/DropdownList?bankAccountTypeKey="2"
        /// </summary>
        [Description("บัญชีธนาคาร")]
        public MST.BankAccountDropdownDTO BankAccount { get; set; }
        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        public bool IsWrongAccount { get; set; }

        public static PaymentBankTransferDTO CreateFromModel(PaymentBankTransfer model)
        {
            if (model != null)
            {
                var result = new PaymentBankTransferDTO();
                result.Id = model.ID;
                result.BankAccount = MST.BankAccountDropdownDTO.CreateFromModel(model.BankAccount);
                result.IsWrongAccount = model.IsWrongAccount;
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
