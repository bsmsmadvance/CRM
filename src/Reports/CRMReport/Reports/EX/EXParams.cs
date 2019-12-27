using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.EX
{
    public class EXParams : CommonParams
    {
        public string token { get; set; }
        public string param { get; set; }
        public string BatchID { get; set; }
        public string UserName { get; set; }
        public string Projects { get; set; }
        public string StatusProject { get; set; }
        public string HomeType { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }

        //DateTime
        public string DateStart { get; set; } 
        public string DateEnd { get; set; } 
        public DateTime? actualDS { get; set; }
        public DateTime? actualDE { get; set; }
    }
}