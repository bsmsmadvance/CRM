using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class CommissionHighRiseTransferVeiwFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public int? PeriodYear { get; set; }
        public int? PeriodMonth { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? LCTransferID { get; set; }
        public decimal? CommissionPercentRate { get; set; }
        public string CommissionPercentType { get; set; }
        public decimal? NetSellPriceForm { get; set; }
        public decimal? NetSellPriceTo { get; set; }
        public DateTime? TransferDateForm { get; set; }
        public DateTime? TransferDateTo { get; set; }
        public decimal? LCTransferPaidForm { get; set; }
        public decimal? LCTransferPaidTo { get; set; }
        public decimal? CommissionForThisMonthForm { get; set; }
        public decimal? CommissionForThisMonthTo { get; set; }
    }
}
