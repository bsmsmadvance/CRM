using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Filters
{
    public class ContactFilter
    {
        public string ContactNo { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public DateTime? UpdatedDateFrom { get; set; }
        public DateTime? UpdatedDateTo { get; set; }
        public string CitizenIdentityNo { get; set; }
    }
}
