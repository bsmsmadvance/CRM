using System;
namespace Customer.Params.Outputs
{
    public class UpdateOverdueResponse
    {
        public int ActivityTaskUpdated { get; set; }
        public int Overdue { get; set; }
        public int OnPlan { get; set; }
        public int Today { get; set; }
    }
}
