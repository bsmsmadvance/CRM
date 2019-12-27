using System;
namespace Sale.Params.Filters
{
    public class QuotationBookingPromotionFilter
    {
        public string NameTH { get; set; }
        public decimal? PricePerUnitFrom { get; set; }
        public decimal? PricePerUnitTo { get; set; }
        public decimal? TotalPriceFrom { get; set; }
        public decimal? TotalPriceTo { get; set; }
        public string UnitTH { get; set; }
        public string PRNo { get; set; }
    }
}
