using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.ACC;
using Base.DTOs.FIN;
using Base.DTOs.USR;

namespace Finance.Params.Filters
{
    public class DirectCreditDebitApprovalFormFilter
    {
        /// <summary>
        /// บริษัท
        /// </summary>
        public Guid? Company { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? Project { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public Guid? Bank { get; set; }

        // <summary>
        /// เลขที่บัญชีลูกค้า
        /// </summary>
        public string AccountNO { get; set; }

        // <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// วันที่หมดอายุบัตรเครดิต เริ่มต้น
        /// </summary>
        public DateTime? ExpireDateFrom { get; set; }

        /// <summary>
        /// วันที่หมดอายุบัตรเครดิต สิ้นสุด
        /// </summary>
        public DateTime? ExpireDateTo { get; set; }

        /// <summary>
        /// วันที่เริ่มตัดบัตรเครดิต เริ่มต้น
        /// </summary>
        public DateTime? StartDateFrom { get; set; }

        /// <summary>
        /// วันที่เริ่มตัดบัตรเครดิต สิ้นสุด
        /// </summary>
        public DateTime? StartDateTo { get; set; }

        /// <summary>
        /// ประเภท
        /// </summary>
        public Guid? DirectApprovalFormType { get; set; }

        /// <summary>
        /// งวดวันที่
        /// </summary>
        public int? DirectPeriod { get; set; }

        /// <summary>
        /// วันที่เริ่มตัดบัตรเครดิต เริ่มต้น
        /// </summary>
        public DateTime? CreateDateFrom { get; set; }

        /// <summary>
        /// วันที่เริ่มตัดบัตรเครดิต สิ้นสุด
        /// </summary>
        public DateTime? CreateDateTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? DirectApprovalFormStatus { get; set; }

    }
}
