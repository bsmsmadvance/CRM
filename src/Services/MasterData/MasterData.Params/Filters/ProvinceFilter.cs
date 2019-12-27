using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterData.Params.Filters
{
    public class ProvinceFilter : BaseFilter
    {
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public bool? IsShow { get; set; }
    }
}
