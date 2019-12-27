using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class MinPriceBudgetApprovalFilter
    {
        public Guid? BookingID { get; set; }
        public Guid? ChangePromotionWorkflowID { get; set; }
    }
}
