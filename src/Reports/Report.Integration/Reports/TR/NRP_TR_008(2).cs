﻿using System;
namespace Report.Integration.Reports.TR
{
    public class NRP_TR_008_2
    {
        /// <summary>
        /// ใบประมาณการค่าใช้จ่าย ณ วันโอนกรรมสิทธิ์ แบบไม่ระบุสินเชื่อ (สมมติว่าโอนสด)
        /// </summary>

        //param
        public string CompanyID { get; set; }
        public string ProductID { get; set; }
        public string UnitNumber { get; set; }
        public string UserName { get; set; }
    }
}