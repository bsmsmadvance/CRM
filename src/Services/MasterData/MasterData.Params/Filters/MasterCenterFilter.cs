using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterData.Params.Filters
{
    public class MasterCenterFilter : BaseFilter
    {
        public string MasterCenterGroupKey { get; set; }
        public int? Order { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Key { get; set; }
        public bool? IsActive { get; set; }
    }
}
