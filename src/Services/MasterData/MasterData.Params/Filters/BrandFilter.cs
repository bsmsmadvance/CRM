using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterData.Params.Filters
{
    public class BrandFilter : BaseFilter
    {
        public string BrandNo { get; set; }
        public string Name { get; set; }
        public string UnitNumberFormatKey { get; set; }
    }
}
