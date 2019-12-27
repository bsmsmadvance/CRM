﻿using System;
namespace Report.Integration.Reports.LC
{
    public class RP_LC_007
    {
        /// <summary>
        /// รายงานการเปลี่ยนรายละเอียดใบจอง
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public string UserName { get; set; }
        public string UnitNumber { get; set; }
        public string StatusTransfer { get; set; }
        public string Change2 { get; set; }
    }
}