using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.FI
{
    public class FIParams : CommonParams
    {
        public string token { get; set; }
        public string param { get; set; }
        public string ProductID { get; set; }
        public string UserName { get; set; }
        public string Discount { get; set; }
        public string StatusAG { get; set; }
        public string UnitStatus { get; set; }
        public string SBUID { get; set; }
        public string BankAccount { get; set; }
        public string UnitNumber { get; set; }
        public string Deposit3 { get; set; }
        public string Method { get; set; }
        public string PaymentType { get; set; }
        public string PaymentType2 { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public string ReceivedStart { get; set; }
        public string ReceivedEnd { get; set; }
        public string PrintingIDStart { get; set; }
        public string PrintingIDEnd { get; set; }
        public string BookingOfflineStatus { get; set; }
        public string LandStatus { get; set; }
        public string LoanStatus1 { get; set; }
        public string CurrentUserID { get; set; }
        public string WorkTransferStatus { get; set; }
        public string Status3 { get; set; }
        public string Status4 { get; set; }
        public string Status5 { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
        public string HomeType { get; set; }

        public string Code { get; set; }
        public string CardMachineTypeKey { get; set; }
        public string BankAccountID { get; set; }
        public string CompanyID { get; set; }
        public string ProjectID { get; set; }
        public string ProjectStatusKey { get; set; }
        public string ReceiveBy { get; set; }
        public string ReceiveDateFrom { get; set; }
        public string ReceiveDateTo { get; set; }
        public string CardMachineStatusKey { get; set; }

        //DateTime
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string DateStart2 { get; set; }
        public string DateEnd2 { get; set; }
        public string DateStart3 { get; set; }
        public string DateEnd3 { get; set; }
        public string WorkTransferDate { get; set; }
        public DateTime? actualDS { get; set; }
        public DateTime? actualDE { get; set; }
        public DateTime? actualDS2 { get; set; }
        public DateTime? actualDE2 { get; set; }
        public DateTime? actualDS3 { get; set; }
        public DateTime? actualDE3 { get; set; }
        public DateTime? actualWT { get; set; }
    }
}