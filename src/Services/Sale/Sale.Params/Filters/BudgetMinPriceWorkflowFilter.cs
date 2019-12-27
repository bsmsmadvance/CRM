using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class BudgetMinPriceWorkflowFilter
    {
        public Guid? ProjectID { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
    }
}
