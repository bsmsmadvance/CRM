using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Project.Services.Excels
{
    public class TitledeedDetailExcelModel
    {
        public const int _projectNoIndex = 0;
        public const int _wbsNoIndex = 1;
        public const int _unitNoIndex = 2;
        public const int _modelNameIndex = 3;
        public const int _houseNoIndex = 4;
        public const int _houseNoReceivedYear = 5;
        public const int _titledeedNoIndex = 6;
        public const int _landNoIndex = 7;
        public const int _landSurveyAreaIndex = 8;
        public const int _landPortionNoIndex = 9;
        public const int _bookNoIndex = 10;
        public const int _pageNoIndex = 11;
        public const int _titledeedAreaIndex = 12;
        public const int _estimatePriceIndex = 13;
        public const int _remarkIndex = 14;

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// WBS Code
        /// </summary>
        public string WBSNo { get; set; }
        /// <summary>
        /// เลขที่แปลง  
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// ชื่อแบบบ้าน
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        ///  ปีที่ได้บ้านเลขที่
        /// </summary>
        public int HouseNoReceivedYear { get; set; }
        /// <summary>
        /// บ้านเลขที่
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// เลขที่โฉนด
        /// </summary>
        public string TitledeedNo { get; set; }
        /// <summary>
        /// เลขที่ดิน
        /// </summary>
        public string LandNo { get; set; }
        /// <summary>
        /// หน้าสำรวจ
        /// </summary>
        public string LandSurveyArea { get; set; }
        /// <summary>
        /// เลขระวาง
        /// </summary>
        public string LandPortionNo { get; set; }
        /// <summary>
        /// เล่ม
        /// </summary>
        public string BookNo { get; set; }
        /// <summary>
        /// หน้า
        /// </summary>
        public string PageNo { get; set; }
        /// <summary>
        /// พื้นที่โฉนด
        /// </summary>
        public double TitledeedArea { get; set; }
        /// <summary>
        /// %เงินดาวน์
        /// </summary>
        public double EstimatePrice { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public static TitledeedDetailExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new TitledeedDetailExcelModel();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.WBSNo = dr[_wbsNoIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.ModelName = dr[_modelNameIndex]?.ToString();
            result.TitledeedNo = dr[_titledeedNoIndex]?.ToString();
            result.LandNo = dr[_landNoIndex]?.ToString();
            result.HouseNo = dr[_houseNoIndex]?.ToString();
            int houseNoReceivedYear;
            if (int.TryParse(dr[_houseNoReceivedYear]?.ToString(), out houseNoReceivedYear))
            {
                result.HouseNoReceivedYear = houseNoReceivedYear;
            }
            result.LandSurveyArea = dr[_landSurveyAreaIndex]?.ToString();
            result.LandPortionNo = dr[_landPortionNoIndex]?.ToString();
            result.BookNo = dr[_bookNoIndex]?.ToString();
            result.PageNo = dr[_pageNoIndex]?.ToString();
            double titledeedArea;
            if (double.TryParse(dr[_titledeedAreaIndex]?.ToString(), out titledeedArea))
            {
                result.TitledeedArea = titledeedArea;
            }
            double estimatePrice;
            if (double.TryParse(dr[_estimatePriceIndex]?.ToString(), out estimatePrice))
            {
                result.EstimatePrice = estimatePrice;
            }
            result.Remark = dr[_remarkIndex]?.ToString();
            return result;
        }
        public void ToModel(ref TitledeedDetail model)
        {
            model.TitledeedNo = this.TitledeedNo;
            model.LandSurveyArea = this.LandSurveyArea;
            model.LandPortionNo = this.LandPortionNo;
            model.PageNo = this.PageNo;
            model.BookNo = this.BookNo;
            model.TitledeedArea = this.TitledeedArea;
            model.Remark = this.Remark;
            model.EstimatePrice = Convert.ToDecimal(this.EstimatePrice);
        }
    }
}
