using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports
{
    public class CommonParams
    {
        public string dbConnection { get; set; }
        public string dbUsername { get; set; }
        public string dbPassword { get; set; }
        public string reportName { get; set; }
        public string fullReportPath { get; set; }
        public string downloadAs { get; set; }
    }
}