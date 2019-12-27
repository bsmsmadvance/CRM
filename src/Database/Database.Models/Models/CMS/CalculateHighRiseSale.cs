﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("คำนวณ Commission ขายโครงการแนวสูง")]
    [Table("CalculateHighRiseSale", Schema = Schema.COMMISSION)]
    public class CalculateHighRiseSale : BaseEntity
    {
        [Description("ปีคำนวณคอมมิสชั่น")]
        public int PeriodYear { get; set; }
        [Description("เดือนคำนวณคอมมิสชั่น")]
        public int PeriodMonth { get; set; }

        [Description("เลขที่สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        [Description("รหัสแปลง/ห้อง")]
        public string UnitNo { get; set; }

        [Description("ประเภท Rate Commission (Fix/NoFix)")]
        public string CommissionPercentType { get; set; }
        [Description("อัตรา % Commission")]
        public decimal? CommissionPercentRate { get; set; }
        [Description("ค่าคอมมิสชั่น Fix ตามแบบบ้านขาย")]
        [Column(TypeName = "Money")]
        public decimal? RateFixSaleModelAmount { get; set; }
        [Description("ค่าคอมมิสชั่นของพนักงานประจำโครงการเมื่อ Agency ปิดการขาย")]
        [Column(TypeName = "Money")]
        public decimal? RateAgentAmount { get; set; }

        [Description("ประเภทพนักงานปิดการขาย")]
        public Guid? SaleOfficerTypeMasterCenterID { get; set; }
        [ForeignKey("SaleOfficerTypeMasterCenterID")]
        public MST.MasterCenter SaleOfficerType { get; set; }
        [Description("Agency")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public MST.Agent Agent { get; set; }
        [Description("พนักงานขายปิดการขายของAgency")]
        public Guid? AgentEmployeeID { get; set; }
        [ForeignKey("AgentEmployeeID")]
        public MST.AgentEmployee AgentEmployee { get; set; }

        [Description("พนักงานขายปิดการขาย")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }
        [Description("ค่าคอมมิสชั่นได้รับตอนสัญญาของพนักงานขายปิดการขาย")]
        [Column(TypeName = "Money")]
        public decimal? SaleUserSalePaid { get; set; }
 
        [Description("พนักงานประจำโครงการ")]
        public Guid? ProjectSaleUserID { get; set; }
        [ForeignKey("ProjectSaleUserID")]
        public USR.User ProjectSaleUser { get; set; }
        [Description("ค่าคอมมิสชั่นได้รับตอนสัญญาของพนักงานประจำโครงการ")]
        [Column(TypeName = "Money")]
        public decimal? ProjectSaleSalePaid { get; set; }

        [Description("ผลรวมค่าคอมมิสชั่นทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal? TotalCommissionPaid { get; set; }
        //[Description("ค่าคอมมิสชั่นที่จ่ายในเดือนนี้")]
        //[Column(TypeName = "Money")]
        //public decimal? CommissionForThisMonth { get; set; }
    }
}
