using System;
namespace Base.DTOs.CTM
{
    public class VisitorPurchaseHistoryDTO
    {
        /// <summary>
        /// วันที่
        /// </summary>
        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// มูลค่า (ราคาขายสุทธิ)
        /// </summary>
        public decimal NetSellingPrice { get; set; }
    }
}
