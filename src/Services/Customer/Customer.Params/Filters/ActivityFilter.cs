using Base.DTOs.CTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Filters
{
    public class ActivityFilter
    {
        public string ActivityTaskTopicKey { get; set; }
        public string ActivityTaskTopicKeys { get; set; }
        public string LeadTypeKey { get; set; }
        public string ActivityTaskTypeKey { get; set; }
        public string ActivityTaskTypeKeys { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectIDs { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public string OverdueStatusKey { get; set; }
        public Guid? OwnerID { get; set; }
        public string ActivityTaskStatusKey { get; set; }
    }
}
