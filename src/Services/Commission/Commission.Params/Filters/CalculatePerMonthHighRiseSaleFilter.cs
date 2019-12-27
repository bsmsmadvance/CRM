using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class CalculatePerMonthHighRiseSaleFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public DateTime? PeriodMonthForm { get; set; }
        public DateTime? PeriodMonthTo { get; set; }
        public string CalculateUserName { get; set; }
        public DateTime? CalculateDateForm { get; set; }
        public DateTime? CalculateDateTo { get; set; }
        public bool? IsApprove { get; set; }
    }
}
