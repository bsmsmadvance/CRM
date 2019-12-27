using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.Services.Excels
{
    public class BudgetMinPriceTransferExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _projectNameIndex = 1;
        public const int _yearIndex = 2;
        public const int _quarterIndex = 3;
        public const int _transferTotalAmountIndex = 4;
        public const int _transferTotalUnitIndex = 5;

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// ปี
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// ควอเตอร์
        /// </summary>
        public int Quarter { get; set; }
        /// <summary>
        /// Total Budget Transfer
        /// </summary>
        public double TransferTotalAmount { get; set; }
        /// <summary>
        /// Budget Transfer/Unit
        /// </summary>
        public double TransferTotalUnit { get; set; }

        public static BudgetMinPriceTransferExcelModel CreateFromDataRow(DataRow dr, DatabaseContext DB)
        {
            var result = new BudgetMinPriceTransferExcelModel();
            ValidateException ex = new ValidateException();
            BudgetMinPriceDTO MsgDTO = new BudgetMinPriceDTO();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.ProjectName = dr[_projectNameIndex]?.ToString();
            int year;
            if (int.TryParse(dr[_yearIndex]?.ToString(), out year))
            {
                if (year >= 2000 || year <= 2300)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    ex.AddError(errMsg.Key, errMsg.Message + result.ProjectNo + "-" + result.ProjectName, (int)errMsg.Type);
                    throw ex;
                }
                result.Year = year;
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            int quarter;
            if (int.TryParse(dr[_quarterIndex]?.ToString(), out quarter))
            {
                if (quarter >= 1 || quarter <= 4)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstOrDefault();
                    string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[message]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    result.Quarter = quarter;
                }
               
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0100").FirstOrDefault();
                string desc = MsgDTO.GetType().GetProperty(nameof(BudgetMinPriceDTO.Quarter)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[message]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            double transferTotalAmount;
            if (double.TryParse(dr[_transferTotalAmountIndex]?.ToString(), out transferTotalAmount))
            {
                if (transferTotalAmount <= 0)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    ex.AddError(errMsg.Key, errMsg.Message + "Transfer Total Amount - " + result.ProjectNo + "-" + result.ProjectName, (int)errMsg.Type);
                    throw ex;
                }
                result.TransferTotalAmount = transferTotalAmount;
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            double transferTotalUnit;
            if (double.TryParse(dr[_transferTotalUnitIndex]?.ToString(), out transferTotalUnit))
            {
                if (transferTotalAmount <= 0)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    ex.AddError(errMsg.Key, errMsg.Message + "Transfer Total Unit - " + result.ProjectNo + "-" + result.ProjectName, (int)errMsg.Type);
                    throw ex;
                }
                result.TransferTotalUnit = transferTotalUnit;
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0101").FirstOrDefault();
                ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
            return result;
        }
    }
}
