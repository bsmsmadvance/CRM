using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วยบัตรเดบิต
    /// Model: PaymentDebitCard
    /// </summary>
    public class PaymentDebitCardDTO : BaseDTO
    {
        /// <summary>
        /// ค่าธรรมเนียม
        /// Master/api/EDCs/Fees/Calculate
        /// </summary>
        [Description("ค่าธรรมเนียม")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// เลขที่บัตร
        /// </summary>
        [Description("เลขที่บัตร")]
        public string CardNo { get; set; }
        /// <summary>
        /// ธนาคารเจ้าของบัตร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคารเจ้าของบัตร")]
        public MST.BankDropdownDTO Bank { get; set; }

        /// <summary>
        /// เครื่องรูดบัตร
        /// Master/api/EDCs/DropdownList?projectID={projectID}
        /// </summary>
        [Description("เครื่องรูดบัตร")]
        public MST.EDCDropdownDTO EDC { get; set; }
        /// <summary>
        /// ผิดบัญชี
        /// </summary>
        public bool IsWrongAccount { get; set; }
        public static PaymentDebitCardDTO CreateFromModel(PaymentDebitCard model)
        {
            if (model != null)
            {
                var result = new PaymentDebitCardDTO();
                result.Id = model.ID;
                result.Fee = model.Fee;
                result.CardNo = model.CardNo;
                result.Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank);
                result.EDC = MST.EDCDropdownDTO.CreateFromModel(model.EDC);
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
