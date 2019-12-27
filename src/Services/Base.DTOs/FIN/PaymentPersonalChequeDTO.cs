using Database.Models.FIN;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายละเอียดการชำระเงินด้วยเช็คส่วนตัว
    /// Model: PaymentPersonalCheque
    /// </summary>
    public class PaymentPersonalChequeDTO : BaseDTO
    {
        /// <summary>
        /// วันที่หน้าเช็ค
        /// </summary>
        [Description("วันที่หน้าเช็ค")]
        public DateTime ChequeDate { get; set; }
        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        [Description("เลขที่เช็ค")]
        public string ChequeNo { get; set; }
        /// <summary>
        /// สั่งจ่ายบริษัท
        /// </summary>
        [Description("สั่งจ่ายบริษัท")]
        public MST.CompanyDropdownDTO PayToCompany { get; set; }
        /// <summary>
        /// สั่งจ่ายผิดบริษัท
        /// </summary>
        public bool IsWrongCompany { get; set; }
        /// <summary>
        /// ธนาคารของเช็ค
        /// </summary>
        [Description("ธนาคารของเช็ค")]
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// สาขาธนาคาร
        /// </summary>
        [Description("สาขาธนาคาร")]
        public MST.BankBranchDropdownDTO BankBranch { get; set; }
        public static PaymentPersonalChequeDTO CreateFromModel(PaymentPersonalCheque model)
        {
            if (model != null)
            {
                var result = new PaymentPersonalChequeDTO();
                result.Id = model.ID;
                result.ChequeDate = model.ChequeDate;
                result.ChequeNo = model.ChequeNo;
                result.PayToCompany = MST.CompanyDropdownDTO.CreateFromModel(model.PayToCompany);
                result.IsWrongCompany = model.IsWrongCompany;
                result.Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank);
                result.BankBranch = MST.BankBranchDropdownDTO.CreateFromModel(model.BankBranch);
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
