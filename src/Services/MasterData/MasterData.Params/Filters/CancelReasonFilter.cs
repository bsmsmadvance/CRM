using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Params.Filters
{
    public class CancelReasonFilter : BaseFilter
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string GroupOfCancelReasonKey { get; set; }
        public string CancelApproveFlowKey { get; set; }
    }
}
