using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class ChangeLCSaleFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string BookingNo { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ActiveDateForm { get; set; }
        public DateTime? ActiveDateTo { get; set; }
        public Guid? OldSaleOfficerTypeMasterCenterID { get; set; }
        public Guid? OldAgentID { get; set; }
        public Guid? OldAgentEmployeeID { get; set; }
        public Guid? OldSaleUserID { get; set; }
        public Guid? OldProjectSaleUserID { get; set; }
    }
}
