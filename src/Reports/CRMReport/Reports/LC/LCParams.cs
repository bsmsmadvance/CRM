using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.LC
{
    public class LCParams : CommonParams
    {
        public string token { get; set; }
        public string param { get; set; }
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string Projects { get; set; }
        public string ContractNumber { get; set; }
        public string WaterStatus { get; set; }
        public string ElectricStatus { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string StatusAG { get; set; }
        public string StatusAG2 { get; set; }
        public string StatusDoc { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string TransferStatus { get; set; }
        public string Deposit { get; set; }
        public string Method { get; set; }
        public string BankAccount { get; set; }
        public string ReceivedID { get; set; }
        public string PaymentType { get; set; }
        public string SBUID { get; set; }
        public string LandStatus { get; set; }
        public string StatusTransfer { get; set; }
        public string Change2 { get; set; }
        public string Customer { get; set; }
        public string P_ID { get; set; }
        public string PromotionID { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
        public string HomeType { get; set; }
        public string Mode { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string LCByProduct { get; set; }
        public string FollowUpStatus { get; set; }

        //DateTime
        public string DateStart { get; set; } 
        public string DateEnd { get; set; } 
        public string DateStart2 { get; set; } 
        public string DateEnd2 { get; set; } 
        public string DateStart3 { get; set; } 
        public string DateEnd3 { get; set; } 
        public DateTime? actualDS { get; set; }
        public DateTime? actualDE { get; set; }
        public DateTime? actualDS2 { get; set; }
        public DateTime? actualDE2 { get; set; }
        public DateTime? actualDS3 { get; set; }
        public DateTime? actualDE3 { get; set; }

        //Integer
        public string Month { get; set; }
        public string Year { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public string Lead { get; set; }
        public string FirstWalk { get; set; }
        public string Revisit { get; set; }

    }
}