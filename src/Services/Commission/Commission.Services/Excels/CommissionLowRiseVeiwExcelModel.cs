using Database.Models.CMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commission.Services.Excels
{
    public class CommissionLowRiseVeiwExcelModel
    {
        public const int _bGNoIndex = 0;
        public const int _projectNoIndex = 1;
        public const int _projectNameIndex = 2;
        public const int _unitNoIndex = 3;
        public const int _leaderNameIndex = 4;
        public const int _saleUserEmpNoIndex = 5;
        public const int _saleUserNameIndex = 6;
        public const int _projectSaleEmpNoIndex = 7;
        public const int _projectSaleNameIndex = 8;
        public const int _customerNameIndex = 9;
        public const int _bookingDateIndex = 10;
        public const int _contractDateIndex = 11;
        public const int _approveDateIndex = 12;
        public const int _signContractApproveDateIndex = 13;
        public const int _transferDateIndex = 14;
        public const int _sellingPriceIndex = 15;
        public const int _rateIndex = 16;
        public const int _saleUserTransferPaidIndex = 17;
        public const int _projectSaleTransferPaidIndex = 18;
        public const int _totalTransferPaidIndex = 19;
        public const int _saleUserNewLaunchPaidIndex = 20;
        public const int _projectSaleNewLaunchPaidIndex = 21;
        public const int _totalNewLaunchPaidIndex = 22;
        public const int _commissionForThisMonthIndex = 23;
        public const int _flagDataIndex = 24;


        /// <summary>
        /// BG
        /// </summary>
        public string BGNo { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string UnitNo { get; set; }
        public string LeaderName { get; set; }
        public string SaleUserEmpNo { get; set; }
        public string SaleUserName { get; set; }
        public string ProjectSaleEmpNo { get; set; }
        public string ProjectSaleName { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime? SignContractApproveDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal? SellingPrice { get; set; }
        public double Rate { get; set; }
        public decimal? SaleUserTransferPaid { get; set; }
        public decimal? ProjectSaleTransferPaid { get; set; }
        public decimal? TotalTransferPaid { get; set; }
        public decimal? SaleUserNewLaunchPaid { get; set; }
        public decimal? ProjectSaleNewLaunchPaid { get; set; }
        public decimal? TotalNewLaunchPaid { get; set; }
        public decimal? CommissionForThisMonth { get; set; }
        public string FlagData { get; set; }


        public static CommissionLowRiseVeiwExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new CommissionLowRiseVeiwExcelModel();
            DateTime bookingDate;
            DateTime contractDate;
            DateTime approveDate;
            DateTime signContractApproveDate;
            DateTime transferDate;
            decimal sellingPrice;
            double rate;
            decimal saleUserTransferPaid;
            decimal projectSaleTransferPaid;
            decimal totalTransferPaid;
            decimal saleUserNewLaunchPaid;
            decimal projectSaleNewLaunchPaid;
            decimal totalNewLaunchPaid;
            decimal commissionForThisMonth;

            result.BGNo = dr[_bGNoIndex]?.ToString();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.ProjectName = dr[_projectNameIndex]?.ToString();
            result.UnitNo = dr[_unitNoIndex]?.ToString();
            result.LeaderName = dr[_leaderNameIndex]?.ToString();
            result.SaleUserEmpNo = dr[_saleUserEmpNoIndex]?.ToString();
            result.SaleUserName = dr[_saleUserNameIndex]?.ToString();
            result.ProjectSaleEmpNo = dr[_projectSaleEmpNoIndex]?.ToString();
            result.ProjectSaleName = dr[_projectSaleNameIndex]?.ToString();
            result.CustomerName = dr[_customerNameIndex]?.ToString();

            if (DateTime.TryParseExact(dr[_bookingDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out bookingDate))
            {
                result.BookingDate = bookingDate;
            }
            if (DateTime.TryParseExact(dr[_contractDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out contractDate))
            {
                result.ContractDate = contractDate;
            }
            if (DateTime.TryParseExact(dr[_approveDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out approveDate))
            {
                result.ApproveDate = approveDate;
            }
            if (DateTime.TryParseExact(dr[_signContractApproveDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out signContractApproveDate))
            {
                result.SignContractApproveDate = signContractApproveDate;
            }
            if (DateTime.TryParseExact(dr[_transferDateIndex]?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out transferDate))
            {
                result.TransferDate = transferDate;
            }
            if (decimal.TryParse(dr[_sellingPriceIndex]?.ToString(), out sellingPrice))
            {
                result.SellingPrice = sellingPrice;
            }
            if (double.TryParse(dr[_rateIndex]?.ToString(), out rate))
            {
                result.Rate = rate;
            }
            if (decimal.TryParse(dr[_saleUserTransferPaidIndex]?.ToString(), out saleUserTransferPaid))
            {
                result.SaleUserTransferPaid = saleUserTransferPaid;
            }
            if (decimal.TryParse(dr[_projectSaleTransferPaidIndex]?.ToString(), out projectSaleTransferPaid))
            {
                result.ProjectSaleTransferPaid = projectSaleTransferPaid;
            }
            if (decimal.TryParse(dr[_totalTransferPaidIndex]?.ToString(), out totalTransferPaid))
            {
                result.TotalTransferPaid = totalTransferPaid;
            }
            if (decimal.TryParse(dr[_saleUserNewLaunchPaidIndex]?.ToString(), out saleUserNewLaunchPaid))
            {
                result.SaleUserNewLaunchPaid = saleUserNewLaunchPaid;
            }
            if (decimal.TryParse(dr[_projectSaleNewLaunchPaidIndex]?.ToString(), out projectSaleNewLaunchPaid))
            {
                result.ProjectSaleNewLaunchPaid = projectSaleNewLaunchPaid;
            }
            if (decimal.TryParse(dr[_totalNewLaunchPaidIndex]?.ToString(), out totalNewLaunchPaid))
            {
                result.TotalNewLaunchPaid = totalNewLaunchPaid;
            }
            if (decimal.TryParse(dr[_commissionForThisMonthIndex]?.ToString(), out commissionForThisMonth))
            {
                result.CommissionForThisMonth = commissionForThisMonth;
            }
            result.FlagData = dr[_flagDataIndex]?.ToString();

            return result;
        }

        public void ToModel(ref CalculateLowRiseSale model)
        {
            model.ProjectNo = this.ProjectNo;
        }
    }
}
