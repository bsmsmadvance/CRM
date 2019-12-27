using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// ช่องทางชำระ
    /// Model: PaymentMethod
    /// </summary>
    public class PaymentMethodDTO : BaseDTO
    {
        /// <summary>
        /// ชนิดของช่องทางชำระ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentMethod
        /// </summary>
        [Description("ชนิดของช่องทางชำระ")]
        public MST.MasterCenterDropdownDTO PaymentMethodType { get; set; }

        /// <summary>
        /// จำนวนเงินที่จ่าย
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// รายละเอียดบัตรเครดิต
        /// </summary>
        public PaymentCreditCardDTO CreditCard { get; set; }
        /// <summary>
        /// รายละเอียดบัตรเดบิต
        /// </summary>
        public PaymentDebitCardDTO DebitCard { get; set; }
        /// <summary>
        /// รายละเอียดเช็คส่วนตัว
        /// </summary>
        public PaymentPersonalChequeDTO PersonalCheque { get; set; }
        /// <summary>
        /// รายละเอียดแคชเชียร์เช็ค
        /// </summary>
        public PaymentCashierChequeDTO CashierCheque { get; set; }
        /// <summary>
        /// รายละเอียดการโอนเงินผ่านธนาคาร
        /// </summary>
        public PaymentBankTransferDTO BankTransfer { get; set; }
        /// <summary>
        /// รายละเอียดการชำระเงินผ่าน QR Code
        /// </summary>
        public PaymentQRCodeDTO QRCode { get; set; }
        /// <summary>
        /// รายละเอียดการโอนเงินต่างประเทศ
        /// </summary>
        public PaymentForeignBankTransferDTO ForeignBankTransfer { get; set; }

        public void ToCreditCardModel(ref PaymentCreditCard model)
        {
            model.IsForeignCreditCard = this.CreditCard.IsForeignCreditCard;
            model.Fee = this.CreditCard.Fee.Value;
            model.CardNo = this.CreditCard.CardNo;
            model.CreditCardPaymentTypeMasterCenterID = this.CreditCard.CreditCardPaymentType?.Id;
            model.CreditCardTypeMasterCenterID = this.CreditCard.CreditCardType?.Id;
            model.BankID = this.CreditCard.Bank?.Id;
            model.EDCID = this.CreditCard.EDC?.Id;
            model.IsWrongAccount = this.CreditCard.IsWrongAccount;
        }

        public void ToDebitModel(ref PaymentDebitCard model)
        {
            model.Fee = this.DebitCard.Fee.Value;
            model.CardNo = this.DebitCard.CardNo;
            model.BankID = this.DebitCard.Bank?.Id;
            model.EDCID = this.DebitCard.EDC?.Id;
            model.IsWrongAccount = this.DebitCard.IsWrongAccount;
        }

        public void ToPaymentPersonalChequeModel(ref PaymentPersonalCheque model)
        {
            model.ChequeDate = this.PersonalCheque.ChequeDate;
            model.ChequeNo = this.PersonalCheque.ChequeNo;
            model.PayToCompanyID = this.PersonalCheque.PayToCompany?.Id;
            model.IsWrongCompany = this.PersonalCheque.IsWrongCompany;
            model.BankID = this.PersonalCheque.Bank?.Id;
            model.BankBranchID = this.PersonalCheque.BankBranch?.Id;
        }

        public void ToPaymentCashierChequeModel(ref PaymentCashierCheque model)
        {
            model.ChequeDate = this.CashierCheque.ChequeDate;
            model.ChequeNo = this.CashierCheque.ChequeNo;
            model.PayToCompanyID = this.CashierCheque.PayToCompany?.Id;
            model.IsWrongCompany = this.CashierCheque.IsWrongCompany;
            model.BankID = this.CashierCheque.Bank?.Id;
            model.BankBranchID = this.CashierCheque.BankBranch?.Id;
        }

        public void ToPaymentBankTransferModel(ref PaymentBankTransfer model)
        {
            model.BankAccountID = this.BankTransfer.BankAccount?.Id;
            model.IsWrongAccount = this.BankTransfer.IsWrongAccount;
        }

        public void ToPaymentQRCodeModel(ref PaymentQRCode model)
        {
            model.BankAccountID = this.QRCode.BankAccount?.Id;
            model.IsWrongAccount = this.QRCode.IsWrongAccount;
        }

        public void ToPaymentMethodModel(ref PaymentMethod model)
        {
            model.PayAmount = this.PayAmount;
        }

        public void ToPaymentForeignBankTransferModel(ref PaymentForeignBankTransfer model)
        {
            model.Fee = this.ForeignBankTransfer.Fee ?? 0;
            model.BankAccountID = this.ForeignBankTransfer?.BankAccount?.Id;
            model.IsWrongAccount = this.ForeignBankTransfer.IsWrongAccount;
            model.ForeignBankID = this.ForeignBankTransfer?.ForeignBank?.Id;
            model.ForeignTransferTypeMasterCenterID = this.ForeignBankTransfer?.ForeignTransferType?.Id;
            model.IR = this.ForeignBankTransfer?.IR;
            model.TransferorName = this.ForeignBankTransfer?.TransferorName;
            model.IsRequestFET = this.ForeignBankTransfer.IsRequestFET;
            model.IsNotifyFET = this.ForeignBankTransfer.IsNotifyFET;
            model.NotifyFETMemo = this.ForeignBankTransfer?.NotifyFETMemo;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.PaymentMethodType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(PaymentMethodDTO.PaymentMethodType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                #region CreditCard

                if (this.PaymentMethodType.Key == PaymentMethodKeys.CreditCard)
                {
                    if (this.CreditCard.Fee == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.CreditCard.CardNo))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.CardNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.CreditCard.CardNo.IsOnlyNumberWithMaxLength(16,16))
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                            string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.CardNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.CreditCard.CreditCardPaymentType == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.CreditCardPaymentType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.CreditCard.CreditCardType == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.CreditCardType)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.CreditCard.IsForeignCreditCard == false)
                    {
                        if (this.CreditCard.Bank == null)
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.CreditCard.EDC == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CreditCard.GetType().GetProperty(nameof(PaymentCreditCardDTO.EDC)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                #endregion

                #region DebitCard
                if (this.PaymentMethodType.Key == PaymentMethodKeys.DebitCard)
                {
                    if (this.DebitCard.Fee == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.DebitCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.Fee)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.DebitCard.CardNo))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.DebitCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.CardNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.DebitCard.CardNo.IsOnlyNumberWithMaxLength(16,16))
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0035").FirstAsync();
                            string desc = this.DebitCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.CardNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.DebitCard.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.DebitCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.DebitCard.EDC == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.DebitCard.GetType().GetProperty(nameof(PaymentDebitCardDTO.EDC)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region PaymentPersonalCheque
                if (this.PaymentMethodType.Key == PaymentMethodKeys.PersonalCheque)
                {
                    if (this.PersonalCheque.ChequeDate == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.ChequeDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.PersonalCheque.ChequeNo))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.ChequeNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.PersonalCheque.ChequeNo.IsOnlyNumber())
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                            string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.ChequeNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.PersonalCheque.PayToCompany == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.PayToCompany)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.PersonalCheque.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.PersonalCheque.BankBranch == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.PersonalCheque.GetType().GetProperty(nameof(PaymentPersonalChequeDTO.BankBranch)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region CashierCheque
                if (this.PaymentMethodType.Key == PaymentMethodKeys.CashierCheque)
                {
                    if (this.CashierCheque.ChequeDate == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.ChequeDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.CashierCheque.ChequeNo))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.ChequeNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.CashierCheque.ChequeNo.IsOnlyNumber())
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                            string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.ChequeNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (this.CashierCheque.PayToCompany == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.PayToCompany)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.CashierCheque.Bank == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.CashierCheque.BankBranch == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.CashierCheque.GetType().GetProperty(nameof(PaymentCashierChequeDTO.BankBranch)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region BankTransfer
                if (this.PaymentMethodType.Key == PaymentMethodKeys.BankTransfer)
                {
                    if (this.BankTransfer.BankAccount == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.BankTransfer.GetType().GetProperty(nameof(PaymentBankTransferDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region QRCode
                if (this.PaymentMethodType.Key == PaymentMethodKeys.QRCode)
                {
                    if (this.QRCode.BankAccount == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.QRCode.GetType().GetProperty(nameof(PaymentQRCodeDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion

                #region ForeignBankTransfer
                if (this.PaymentMethodType.Key == PaymentMethodKeys.ForeignBankTransfer)
                {
                    if (this.ForeignBankTransfer.BankAccount == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.ForeignBankTransfer.GetType().GetProperty(nameof(PaymentForeignBankTransferDTO.BankAccount)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (string.IsNullOrEmpty(this.ForeignBankTransfer.TransferorName))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.ForeignBankTransfer.GetType().GetProperty(nameof(PaymentForeignBankTransferDTO.TransferorName)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                #endregion
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
    }
}
