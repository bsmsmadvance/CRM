﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("ข้อมูลการรับชำระเงินจากระบบ Offline (Detail)")]
    [Table("OfflinePaymentDetail", Schema = Schema.FINANCE)]
    public class OfflinePaymentDetail : BaseEntity
    {
        [Description("Header ID")]
        public Guid? OfflinePaymentHeaderID { get; set; }
        [ForeignKey("OfflinePaymentHeaderID")]
        public OfflinePaymentHeader OfflinePaymentHeader { get; set; }

        [Description("ลำดับรายการ")]
        public int Seq { get; set; }

        [Description("ประเภทการรับชำระเงิน จอง,สัญญา,ดาวน์")]
        public Guid? OfflinePaymentTypeMasterCenterID { get; set; }
        [ForeignKey("OfflinePaymentTypeMasterCenterID")]
        public MST.MasterCenter OfflinePaymentTypeMasterCenter { get; set; }

        [Description("บริษัท")]
        public bool CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("ธนาคาร เช็ค,บัตร,เงินโอน")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("เลขที่เช็ค,บัตร")]
        [MaxLength(50)]
        public string ReferentNO { get; set; }

        [Description("วันที่-เวลา Confirm")]
        public DateTime? ReferentDate { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal PayAmount { get; set; }

        [Description("")]
        [MaxLength(50)]
        public string CreditCardType { get; set; }

        [Description("% ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal CreditCardFeeAmount { get; set; }

        [Description("มูลค่า ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal FeeAmount { get; set; }

        [Description("ลำดับเครื่อง ที่โครงการ")]
        [MaxLength(50)]
        public string Machine { get; set; }

        [Description("เลขที่ ApproveCode จาก Slip")]
        [MaxLength(50)]
        public string ApproveCode { get; set; }
    }
}
