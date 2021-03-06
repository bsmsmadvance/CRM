﻿using System;
namespace Report.Integration.Reports.LC
{
    public class RP_CommissionByProjectSale_H
    {
        /// <summary>
        /// รายงาน Commission by Project ขาย (แนวสูง)
        /// </summary>

        //param
        public string HomeType { get; set; }
        public string ProductID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string UserName { get; set; }
        public string ProjectGroup { get; set; }
        public string ProjectType2 { get; set; }
    }
}
