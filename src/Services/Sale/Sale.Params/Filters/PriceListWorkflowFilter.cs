using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class PriceListWorkflowFilter
    {
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string UnitStatusKey { get; set; }
        public string PriceListWorkflowStageKey { get; set; }
    }
}
