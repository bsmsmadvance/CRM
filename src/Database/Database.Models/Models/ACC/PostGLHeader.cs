using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ACC
{
    [Description("Header ข้อมูลการ Post GL")]
    [Table("PostGLHeader", Schema = Schema.ACCOUNT)]
    public class PostGLHeader : BaseEntity
    {
        [Description("เลขที่เอกสาร PI,RV,JV,CA")]
        [MaxLength(50)]
        public string DocumentNo { get; set; }

        [Description("ประเภท Doc RV,JV,PI,CA")]
        public Guid? PostGLDocumentTypeMasterCenterID { get; set; }

        [ForeignKey("PostGLDocumentTypeID")]
        public MST.MasterCenter PostGLDocumentType { get; set; }

        [Description("ID Company")]
        public Guid? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("วันที่เอกสาร")]
        public DateTime DocumentDate { get; set; }

        [Description("วันที่ Posting date")]
        public DateTime PostingDate { get; set; }

        //[Description("ประเภท Doc RV,JV,PI,CA")]
        //public Guid? ReferentGLID { get; set; }

        //[ForeignKey("ReferentGLID")]
        //public PostGLHeader ReferentGL { get; set; }

        [Description("ยอดเงินที่ Post")]
        public decimal Amount { get; set; }

        [Description("มูลค่า ค่าธรรมเนียมที่มีของ Doc นี้")]
        public decimal Fee { get; set; }

        [Description("ID ของรายการที่อ้างอิงการ Post GL")]
        public Guid? ReferentID { get; set; }

        [Description("แหล่งข้อมูลของ ReferentID = DepositHeader(PI),PaymentMethod(RV),UnknowPayment(UN),PostGLHeader(กรณี Type CA),ChangeUnitWorkflow(JV)")]
        [MaxLength(50)]
        public string ReferentType { get; set; }

        [Description("จำนวนครั้งที่ Export text file ส่งไป SAP")]
        public int ExportedTimes { get; set; }

        [Description("วันที่ Export text file ส่งไป SAP ครั้งล่าสุด")]
        public int LastExportedDate { get; set; }

        [Description("เหตุผลการยกเลิกรายการ")]
        [MaxLength(1000)]
        public string DeleteReason { get; set; }
    }
}
