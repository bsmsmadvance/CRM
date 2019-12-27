using Database.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commission.Services.Excels
{
    public class RateSettingSaleExcelModel
    {
        public const int _effectiveMonthIndex = 0;
        public const int _bGIndex = 1;
        public const int _projectIDIndex = 2;
        public const int _projectNameIndex = 3;
        public const int _itemIDIndex = 4;
        public const int _rateIndex = 5;
        public const int _startRangeIndex = 6;
        public const int _endRangeIndex = 7;

        /// <summary>
        /// EffectiveMonth
        /// </summary>
        public DateTime? EffectiveMonth { get; set; }
        /// <summary>
        /// BG
        /// </summary>
        public string BGNo { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโครงการ
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        ///ItemID
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// %Rate
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// StartRange
        /// </summary>
        public decimal StartRange { get; set; }
        /// <summary>
        /// EndRange
        /// </summary>
        public decimal EndRange { get; set; }

        public static RateSettingSaleExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new RateSettingSaleExcelModel();

            DateTime effectiveMonth;
            if (DateTime.TryParseExact(dr[_effectiveMonthIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out effectiveMonth))
            {
                result.EffectiveMonth = effectiveMonth;
            }

            result.BGNo = dr[_bGIndex]?.ToString();
            result.ProjectNo = dr[_projectIDIndex]?.ToString();
            result.ProjectName = dr[_projectNameIndex]?.ToString();

            int itemID;
            if (int.TryParse(dr[_itemIDIndex]?.ToString(), out itemID))
            {
                result.ItemID = itemID;
            }
            else
            {
                result.ItemID = 0;
            }

            double rate;
            if (double.TryParse(dr[_rateIndex]?.ToString(), out rate))
            {
                result.Rate = rate;
            }
            else
            {
                result.Rate = 0;
            }

            decimal startRange;
            if (decimal.TryParse(dr[_startRangeIndex]?.ToString(), out startRange))
            {
                result.StartRange = startRange;
            }
            else
            {
                result.StartRange = 0;
            }

            decimal endRange;
            if (decimal.TryParse(dr[_endRangeIndex]?.ToString(), out endRange))
            {
                result.EndRange = endRange;
            }
            else
            {
                result.EndRange = 0;
            }

            return result;
        }

        public void ToModel(ref RateSettingSale model)
        {
            model.ActiveDate = this.EffectiveMonth;
        }
    }
}
