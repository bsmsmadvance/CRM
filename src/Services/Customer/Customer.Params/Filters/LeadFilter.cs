using Database.Models.CTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Filters
{
    public class LeadFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string LeadTypeKey { get; set; }
        public Guid? OwnerID { get; set; }
        public string LeadStatusKey { get; set; }
        public Guid? ProjectID { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public string ExcludeIDs { get; set; }
    }
}
