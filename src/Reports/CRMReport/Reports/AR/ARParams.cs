using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.AR
{
    public class ARParams : CommonParams
    {
        //Param
        public string token { get; set; }
        public string param { get; set; }
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public string ProductID { get; set; }
        public string StatusAG { get; set; }
        public string UnitNumber { get; set; }
        public string BatchID { get; set; }
        public string Deposit { get; set; }
        public string JV { get; set; }
        public string BankAccount { get; set; }
        public string Receipt { get; set; }
        public string PercentPayment { get; set; }

        //DateTime
        public string DateStart { get; set; } 
        public string DateEnd { get; set; } 
        public string DateStart2 { get; set; } 
        public string DateEnd2 { get; set; } 
        public DateTime? actualDS { get; set; }
        public DateTime? actualDE { get; set; }
        public DateTime? actualDS2 { get; set; }
        public DateTime? actualDE2 { get; set; }

        
    }
}