using Database.Models.FIN;
using System;
namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วย QR Code
    /// Model: PaymentQRCodeDTO
    /// </summary>
    public class PaymentQRCodeDTO : BaseDTO
    {
        /// <summary>
        /// บัญชีธนาคาร
        /// Master/api/BankAccounts/DropdownList?bankAccountTypeKey="2"
        /// </summary>
        public MST.BankAccountDropdownDTO BankAccount { get; set; }
        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        public bool IsWrongAccount { get; set; }
        public static PaymentQRCodeDTO CreateFromModel(PaymentQRCode model)
        {
            if (model != null)
            {
                var result = new PaymentQRCodeDTO();
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
