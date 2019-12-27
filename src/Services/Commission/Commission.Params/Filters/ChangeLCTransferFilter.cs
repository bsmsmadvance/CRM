using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class ChangeLCTransferFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string TransferNo { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ActiveDateForm { get; set; }
        public DateTime? ActiveDateTo { get; set; }
        public Guid? OldLCTransferID { get; set; }
    }
}
