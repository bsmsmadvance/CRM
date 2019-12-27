using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.TR
{
    public class TRParams : CommonParams
    {
        public string token { get; set; }
        public string param { get; set; }
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string UserName { get; set; }
        public string LandStatus { get; set; }
        public string UnitStatus { get; set; }
        public string LoanStatus { get; set; }
        public string LoanStatus1 { get; set; }
        public string QCStatus { get; set; }
        public string CurrentUserId { get; set; }
        public string WorkTransferStatus { get; set; }
        public string Projects { get; set; }
        public string StatusAG { get; set; }
        public string BankOnly { get; set; }
        public string BankCheque { get; set; }
        public string HomeType { get; set; }
        public string StatusProject { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string EmpCode { get; set; }

        //DateTime
        public string DateStart { get; set; } 
        public string DateEnd { get; set; } 
        public string DateStart2 { get; set; } 
        public string DateEnd2 { get; set; } 
        public string DateStart3 { get; set; } 
        public string DateEnd3 { get; set; } 
        public string DateStart4 { get; set; } 
        public string DateEnd4 { get; set; } 
        public string WorkTransferDate { get; set; } 
        public DateTime? actualDS { get; set; }
        public DateTime? actualDE { get; set; }
        public DateTime? actualDS2 { get; set; }
        public DateTime? actualDE2 { get; set; }
        public DateTime? actualDS3 { get; set; }
        public DateTime? actualDE3 { get; set; }
        public DateTime? actualDS4 { get; set; }
        public DateTime? actualDE4 { get; set; }
        public DateTime? actualWT { get; set; }

        //Int
        public string Quarter { get; set; } 
        public string QuarterYear { get; set; } 

    }
}