using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วยบัตรเครดิต
    /// Model: PaymentCreditCard
    /// </summary>
    public class PaymentCreditCardDTO : BaseDTO
    {

        /// <summary>
        /// เป็นบัตรต่างประเทศหรือไม่
        /// </summary>
        public bool IsForeignCreditCard { get; set; }
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
        /// รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardPaymentType
        /// </summary>
        [Description("รูปแบบการจ่ายเงิน (รูดเต็ม หรือ ผ่อน)")]
        public MST.MasterCenterDropdownDTO CreditCardPaymentType { get; set; }
        /// <summary>
        /// ประเภทบัตร (Visa, Master, JCB)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CreditCardType
        /// </summary>
        [Description("ประเภทบัตร (Visa, Master, JCB)")]
        public MST.MasterCenterDropdownDTO CreditCardType { get; set; }

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

        public static PaymentCreditCardDTO CreateFromModel(PaymentCreditCard model)
        {
            if (model != null)
            {
                var result = new PaymentCreditCardDTO();
                result.Id = model.ID;
                result.IsForeignCreditCard = model.IsForeignCreditCard;
                result.Fee = model.Fee;
                result.CardNo = model.CardNo;
                result.CreditCardPaymentType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditCardPaymentType);
                result.CreditCardType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CreditCardType);
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
