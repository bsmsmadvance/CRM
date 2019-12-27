using System;
namespace Sale.Params.Filters
{
    public class QuotationTransferPromotionFilter
    {
        public string NameTH { get; set; }
        public decimal? PricePerUnitFrom { get; set; }
        public decimal? PricePerUnitTo { get; set; }
        public decimal? TotalPriceFrom { get; set; }
        public decimal? TotalPriceTo { get; set; }
        public string UnitTH { get; set; }
    }
}
