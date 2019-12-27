using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class BookingPromotionFilter
    {
        public string NameTH { get; set; }
        public decimal? PricePerUnitFrom { get; set; }
        public decimal? PricePerUnitTo { get; set; }
        public decimal? TotalPriceFrom { get; set; }
        public decimal? TotalPriceTo { get; set; }
        public string UnitTH { get; set; }
    }
}
