using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Params.Filters
{
    public class TypeOfRealEstateFilter : BaseFilter
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RealEstateCategoryKey { get; set; }
        public decimal? StandardCostFrom { get; set; }
        public decimal? StandardCostTo { get; set; }
        public decimal? StandardPriceFrom { get; set; }
        public decimal? StandardPriceTo { get; set; }
    }
}
