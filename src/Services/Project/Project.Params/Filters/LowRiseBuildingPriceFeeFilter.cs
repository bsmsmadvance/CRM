using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Params.Filters
{
    public class LowRiseBuildingPriceFeeFilter : BaseFilter
    {
        public Guid? ModelID { get; set; }
        public Guid? UnitID { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
