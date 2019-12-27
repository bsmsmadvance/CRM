using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class IncreaseMoneyFilter : BaseFilter
    {
        public DateTime? ActiveDateForm { get; set; }
        public DateTime? ActiveDateTo { get; set; }
        public Guid? ProjectID { get; set; }
        public string SaleUserID { get; set; }
        public string SaleUserName { get; set; }
        public decimal? AmountForm { get; set; }
        public decimal? AmountTo { get; set; }
        public string Remark { get; set; }
    }
}
