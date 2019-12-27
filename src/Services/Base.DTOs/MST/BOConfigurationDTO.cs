using Database.Models;
using Database.Models.MST;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class BOConfigurationDTO : BaseDTO
    {
        /// <summary>
        /// ภาษีมูลค่าเพิ่ม (%)
        /// </summary>
        [Description("ภาษีมูลค่าเพิ่ม (%)")]
        public double? Vat { get; set; }
        /// <summary>
        /// BOI
        /// </summary>
        public double? BOIAmount { get; set; }
        /// <summary>
        /// ภาษีเงินได้ (%)
        /// </summary>
        public double? IncomeTaxPercent { get; set; }
        /// <summary>
        /// ภาษีธุรกิจเฉพาะ (%)
        /// </summary>
        public double? BusinessTaxPercent { get; set; }
        /// <summary>
        /// ภาษีท้องถิ่น (%)
        /// </summary>
        public double? LocalTaxPercent { get; set; }
        /// <summary>
        /// เบี้ยปรับย้ายห้อง
        /// </summary>
        public double? UnitTransferFee { get; set; }
        /// <summary>
        /// บัญชีเงินขาดเกิด
        /// </summary>
        public double? AdjustAccount { get; set; }
        /// <summary>
        /// บัญชีภาษีขาย
        /// </summary>
        public double? TaxAccount { get; set; }
        /// <summary>
        /// อัตราค่าเสื่อมราคาต่อปี
        /// </summary>
        public double? DepreciationYear { get; set; }
        /// <summary>
        /// ยกเลิกแบบคืนเงิน
        /// </summary>
        public double? VoidRefund { get; set; }
        /// <summary>
        /// อัตราค่าธรรมเนียมโอน
        /// </summary>
        public double? TransferFeeRate { get; set; }

        public static BOConfigurationDTO CreateFromModel(BOConfiguration model)
        {
            if (model != null)
            {
                var result = new BOConfigurationDTO()
                {
                    Id = model.ID,
                    Vat = model.Vat,
                    BOIAmount = model.BOIAmount,
                    IncomeTaxPercent = model.IncomeTaxPercent,
                    BusinessTaxPercent = model.BusinessTaxPercent,
                    LocalTaxPercent = model.LocalTaxPercent,
                    UnitTransferFee = model.UnitTransferFee,
                    AdjustAccount = model.AdjustAccount,
                    TaxAccount = model.TaxAccount,
                    DepreciationYear = model.DepreciationYear,
                    VoidRefund = model.VoidRefund,
                    TransferFeeRate = model.TransferFeeRate,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (this.Vat == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BOConfigurationDTO.Vat)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref BOConfiguration model)
        {
            model.Vat = this.Vat.Value;
            model.BOIAmount = this.BOIAmount;
            model.IncomeTaxPercent = this.IncomeTaxPercent;
            model.BusinessTaxPercent = this.BusinessTaxPercent;
            model.LocalTaxPercent = this.LocalTaxPercent;
            model.UnitTransferFee = this.UnitTransferFee;
            model.AdjustAccount = this.AdjustAccount;
            model.TaxAccount = this.TaxAccount;
            model.DepreciationYear = this.DepreciationYear;
            model.VoidRefund = this.VoidRefund;
            model.TransferFeeRate = this.TransferFeeRate;
        }
    }
}
