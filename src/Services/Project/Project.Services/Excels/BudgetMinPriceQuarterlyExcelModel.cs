using ErrorHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Database.Models;

namespace Project.Services.Excels
{
    public class BudgetMinPriceQuarterlyExcelModel
    {
        public const int _unitNoIndex = 0;
        public const int _budgetAmountIndex = 1;
        public const int _unitStatusIndex = 2;

        /// <summary>
        /// รหัสแปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// ราคา
        /// </summary>
        public double BudgetAmount { get; set; }
        /// <summary>
        /// ควอเตอร์
        /// </summary>
        public string UnitStatus { get; set; }
        public static BudgetMinPriceQuarterlyExcelModel CreateFromDataRow(DataRow dr, DatabaseContext DB)
        {
            var result = new BudgetMinPriceQuarterlyExcelModel();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.UnitStatus = dr[_unitStatusIndex]?.ToString();
            ValidateException ex = new ValidateException();

            double budgetAmount;
            if (double.TryParse(dr[_budgetAmountIndex]?.ToString(), out budgetAmount))
            {
                if (budgetAmount <= 0)
                {
                    var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                    ex.AddError(errMsg.Key, errMsg.Message + "- UnitNo " + result.UnitNo, (int)errMsg.Type);
                    throw ex;
                }
                result.BudgetAmount = budgetAmount;
            }
            else
            {
                var errMsg = DB.ErrorMessages.Where(o => o.Key == "ERR0102").FirstOrDefault();
                ex.AddError(errMsg.Key, errMsg.Message + "- UnitNo " + result.UnitNo , (int)errMsg.Type);
                throw ex;
            }
            return result;
        }
    }
}
