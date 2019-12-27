using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class CommissionLowRiseVeiwFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public int? PeriodYear { get; set; }
        public int? PeriodMonth { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? SaleUserID { get; set; }
        public Guid? ProjectSaleUserID { get; set; }
        public decimal? CommissionPercentRate { get; set; }
        public string CommissionPercentType { get; set; }
        public decimal? TotalContractNetAmountForm { get; set; }
        public decimal? TotalContractNetAmountTo { get; set; }
        public DateTime? ActualTransferDateForm { get; set; }
        public DateTime? ActualTransferDateTo { get; set; }
        public decimal? SaleUserSalePaidForm { get; set; }
        public decimal? SaleUserSalePaidTo { get; set; }
        public decimal? ProjectSaleSalePaidForm { get; set; }
        public decimal? ProjectSaleSalePaidTo { get; set; }
        public decimal? TotalSalePaidForm { get; set; }
        public decimal? TotalSalePaidTo { get; set; }
        public decimal? SaleUserNewLaunchPaidForm { get; set; }
        public decimal? SaleUserNewLaunchPaidTo { get; set; }
        public decimal? ProjectSaleNewLaunchPaidForm { get; set; }
        public decimal? ProjectSaleNewLaunchPaidTo { get; set; }
        public decimal? TotalNewLaunchPaidForm { get; set; }
        public decimal? TotalNewLaunchPaidTo { get; set; }
        public decimal? CommissionForThisMonthForm { get; set; }
        public decimal? CommissionForThisMonthTo { get; set; }
    }
}
