using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.AG
{
    public class AGParams : CommonParams
    {
        //Param
        public string token { get; set; }
        public string param { get; set; }
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string Year { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
        public string PrintLabel { get; set; }
        public string StatusAG { get; set; }
        public string StatusAG3 { get; set; }
        public string HomeType { get; set; }
        public string StatusProject { get; set; }
        public string Projects { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
        public string SBUID { get; set; }
        public string ModelTypeName { get; set; }
        public string CurrentPeriod { get; set; }
        public string PercentConstruction { get; set; }
        public string StatusPeriod { get; set; }
        public string CustomerStatus { get; set; }

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

        //Int
        public string MailType1 { get; set; }
        public string MailType2 { get; set; } 
        public string MinPriceType { get; set; }
    }
}