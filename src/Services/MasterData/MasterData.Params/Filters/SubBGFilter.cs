using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Params.Filters
{
    public class SubBGFilter : BaseFilter
    {
        public string SubBGNo { get; set; }
        public string Name { get; set; }
        public Guid? BgID { get; set; }
    }
}
