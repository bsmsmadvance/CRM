using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class ReceiptSendPrintingHistoryFilter : BaseFilter
    {
        /// <summary>
        /// เลขที่ Lot
        /// </summary>
        public string LotNo{ get; set; }

        /// <summary>
        /// จำนวนใบเสร็จ
        /// </summary>
        public int? TotalReceiptNumberFrom { get; set; }
        public int? TotalReceiptNumberTo { get; set; }

        /// <summary>
        /// วันที่ Export ส่งให้โรงพิมพ์
        /// </summary>
        public DateTime? ExportDateFrom { get; set; }
        public DateTime? ExportDateTo { get; set; }

        /// <summary>
        /// ผู้ Export
        /// </summary>
        public Guid? ExportBy { get; set; }

        /// <summary>
        /// วันที่ ส่งให้โรงพิมพ์ User
        /// </summary>
        public DateTime? SendDateFrom { get; set; }
        public DateTime? SendDateTo { get; set; }
    }
}
