using Database.Models;
using Database.Models.FIN;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.FIN
{
    /// <summary>
    /// รายการรับชำระเงิน
    /// Model: UnitPriceItem
    /// </summary>
    public class PaymentUnitPriceItemDTO : BaseDTO
    {
        /// <summary>
        /// ค่า
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// แสดง
        /// </summary>
        public bool ShowSelect { get; set; }
        /// <summary>
        /// disabled
        /// </summary>
        public bool CanSelect { get; set; }
        /// <summary>
        /// ชื่อรายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวนเงินที่ต้องชำระ
        /// </summary>
        public decimal ItemAmount { get; set; }
        /// <summary>
        /// จำนวนเงินที่ชำระแล้ว
        /// </summary>
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// จำนวนเงินคงเหลือ
        /// </summary>
        public decimal RemainAmount { get; set; }
        /// <summary>
        /// เงินที้จะชำระ
        /// </summary>
        public decimal PayAmount { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Master Price
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }

        /// <summary>
        /// งวดที่
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// กำหนดชำระ
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public List<ReceiptTempListDTO> Receipts { get; set; }

        public static PaymentUnitPriceItemDTO CreateFromUnitPriceItemModel(UnitPriceItem unitPriceItem, decimal amount,int order)
        {
            if (unitPriceItem != null)
            {
                var modelPrices = new List<Guid?>() {MasterPriceItemKeys.BookingAmount,MasterPriceItemKeys.DownAmount,MasterPriceItemKeys.ContractAmount };
                var result = new PaymentUnitPriceItemDTO();
                result.Id = unitPriceItem.ID;
                result.CanSelect = modelPrices.Contains(unitPriceItem.MasterPriceItemID) ? false : true;
                result.ShowSelect = modelPrices.Contains(unitPriceItem.MasterPriceItemID) ? false : true;
                result.Name = unitPriceItem.Name;
                result.ItemAmount = unitPriceItem.Amount;
                result.PaidAmount = amount;
                result.RemainAmount = unitPriceItem.Amount - amount;
                result.Order = order;
                result.MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(unitPriceItem.MasterPriceItem);
                result.Updated = unitPriceItem.Updated;
                result.UpdatedBy = unitPriceItem.UpdatedBy?.DisplayName;
                result.DueDate = unitPriceItem.DueDate;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static PaymentUnitPriceItemDTO CreateFromUnitPriceInstallmentModel(UnitPriceInstallment unitPriceInstallment, decimal amount,int order)
        {
            if (unitPriceInstallment != null)
            {
                var result = new PaymentUnitPriceItemDTO();
                result.Id = unitPriceInstallment.ID;
                result.CanSelect = false;
                result.ShowSelect = false;
                result.Name = unitPriceInstallment.InstallmentOfUnitPriceItem.Name;
                result.ItemAmount = unitPriceInstallment.Amount;
                result.PaidAmount = amount;
                result.RemainAmount = unitPriceInstallment.Amount - amount;
                result.Order = order;
                result.MasterPriceItem = MST.MasterPriceItemDTO.CreateFromModel(unitPriceInstallment.InstallmentOfUnitPriceItem.MasterPriceItem);
                result.Updated = unitPriceInstallment.Updated;
                result.UpdatedBy = unitPriceInstallment.UpdatedBy?.DisplayName;
                result.Period = unitPriceInstallment.Period;
                result.DueDate = unitPriceInstallment.DueDate;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
