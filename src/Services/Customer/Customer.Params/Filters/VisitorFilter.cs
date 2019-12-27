using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Filters
{
    public class VisitorFilter
    {
        public string ReceiveNumber { get; set; }
        public string ContactNo { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? OwnerID { get; set; }
        public string VehicleDescription { get; set; }
        public Guid? ProjectID { get; set; }
        public string VisitByKey { get; set; }
        public string VehicleKey { get; set; }
        public bool? IsContact { get; set; }
        public DateTime? VisitDateInFrom { get; set; }
        public DateTime? VisitDateInTo { get; set; }
    }
}
