using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public static class PaymentMethodKeys
    {
        /// <summary>
        /// เงินสด
        /// </summary>
        public static string Cash = "1";
        /// <summary>
        /// แคชเชียร์เช็ค
        /// </summary>
        public static string CashierCheque = "2";
        /// <summary>
        /// บัตรเครดิต
        /// </summary>
        public static string CreditCard = "3";
        /// <summary>
        /// เช็คส่วนตัว
        /// </summary>
        public static string PersonalCheque = "4";
        /// <summary>
        /// เช็ค(ล่วงหน้า)
        /// </summary>
        public static string PostDateCheque = "5";
        /// <summary>
        /// โอนผ่านธนาคาร
        /// </summary>
        public static string BankTransfer = "6";
        /// <summary>
        /// การ์ดลูกหนี้
        /// </summary>
        public static string BillPayment = "7";
        /// <summary>
        /// หักผ่านบัญชี
        /// </summary>
        public static string DirectDebit = "8";
        /// <summary>
        /// หักผ่านบัตรเครดิต
        /// </summary>
        public static string DirectCredit = "9";
        /// <summary>
        /// เงินจากสัญญาเดิม
        /// </summary>
        public static string ChangeContract = "10";
        /// <summary>
        /// เงินพักลูกค้า
        /// </summary>
        public static string CustomerWallet = "11";
        /// <summary>
        /// เงินโอนต่างประเทศ
        /// </summary>
        public static string ForeignBankTransfer = "12";
        /// <summary>
        /// บัตรเดบิต
        /// </summary>
        public static string DebitCard = "13";
        /// <summary>
        /// QR Code
        /// </summary>
        public static string QRCode = "14";
        /// <summary>
        /// UnknowPayment
        /// </summary>
        public static string UnknowPayment = "15";

        /// <summary>
        /// การชำระเงินที่ต้องนำฝาก
        /// </summary>
        public static List<string> NeedToDepositKeys = new List<string> { PaymentMethodKeys.Cash, PaymentMethodKeys.CreditCard, PaymentMethodKeys.DebitCard, PaymentMethodKeys.BankTransfer, PaymentMethodKeys.ForeignBankTransfer, PaymentMethodKeys.PersonalCheque, PaymentMethodKeys.CashierCheque, PaymentMethodKeys.QRCode };
    }
}
