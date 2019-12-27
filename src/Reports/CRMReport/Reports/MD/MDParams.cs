using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.MD
{
    public class MDParams
    {
        //Parameters
        public string token { get; set; }
        public string param { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectNameEN { get; set; }
        public string BrandID { get; set; }
        public string CompanyID { get; set; }
        public string ProductTypeKey { get; set; }
        public string ProjectStatusKey { get; set; }
        public string IsActive { get; set; }

        //Common
        public string dbConnection { get; set; }
        public string dbUsername { get; set; }
        public string dbPassword { get; set; }
        public string reportName { get; set; }
        public string fullReportPath { get; set; }
        public string downloadAs { get; set; }
    }
}