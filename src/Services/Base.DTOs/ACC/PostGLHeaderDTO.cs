using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;
namespace Base.DTOs.ACC
{
    public class PostGLHeaderDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ Doc ของการ PostGL PI,RV,JV,CA
        /// </summary>
        [Description("เลขที่เอกสาร ของการ PostGL PI,RV,JV,CA")]
        public string DocumentNo { get; set; }

        /// <summary>
        /// ประเภท Doc RV,JV,PI,CA
        /// </summary>
        [Description("ประเภท Doc RV,JV,PI,CA")]
        public MasterCenterDropdownDTO PostGLDocumentTypeMasterCenter { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        [Description("Company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        [Description("Unit")]
        public UnitDropdownDTO Unit { get; set; }

        /// <summary>
        /// บัญชีบริษัท
        /// </summary>
        [Description("บัญชีบริษัท")]
        public BankAccountDropdownDTO BankAccount { get; set; }

        /// <summary>
        /// วันที่เอกสาร
        /// </summary>
        [Description("วันที่เอกสาร/วันที่ทำรายการ")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// รายละเอียด
        /// </summary>
        [Description("รายละเอียด")]
        public string Description { get; set; }

        /// <summary>
        /// วันที่ Posting date
        /// </summary>
        [Description("วันที่ Posting date")]
        public DateTime PostingDate { get; set; }

        /// <summary>
        /// ID ของรายการที่อ้างอิงการ Post GL
        /// </summary>
        [Description("ID ของรายการที่อ้างอิงการ Post GL")]
        public Guid? ReferentID { get; set; }

        /// <summary>
        /// แหล่งข้อมูลของ ReferentID = DepositHeader,PaymentMethod,UnknowPayment,PostGLHeader(กรณี Type CA)
        /// </summary>
        [Description("แหล่งข้อมูลของ ReferentID = DepositHeader,PaymentMethod,UnknowPayment,CancelMemo,ChangeUnitWorkflow,PostGLHeader(กรณี Type CA)")]
        public string ReferentType { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        /// <summary>
        /// จำนวนเงินที่ Post
        /// </summary>
        [Description("จำนวนเงินที่ Post")]
        public decimal Amount{ get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        [Description("ค่าธรรมเนียม")]
        public decimal Fee { get; set; }

        /// <summary>
        /// คงเหลือ
        /// </summary>
        [Description("คงเหลือ")]
        public decimal RemainAmount { get; set; }

        /// <summary>
        /// โพสโดย
        /// </summary>
        [Description("โพสโดย")]
        public User CreatedByUser { get; set; }

        /// <summary>
        /// วันที่โพส
        /// </summary>
        [Description("วันที่โพส")]
        public DateTime Created { get; set; }

        /// <summary>
        /// 1=Active  0=cancel
        /// </summary>
        [Description("1=Active  0=cancel")]
        public bool PostedStatus { get; set; }

        /// <summary>
        /// รายละเอียด Credit,Debit
        /// </summary>
        [Description("รายละเอียด Credit,Debit")]
        public List<PostGLDetailDTO> PostGLDetails { get; set; }
    }
}
